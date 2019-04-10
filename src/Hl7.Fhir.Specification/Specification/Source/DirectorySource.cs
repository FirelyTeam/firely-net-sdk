/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

// [WMR 20171023] TODO
// - Allow configuration of duplicate canonical url handling strategy

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from a directory on disk. Thread-safe.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class DirectorySource : ISummarySource, IConformanceSource, IArtifactSource
    {
        private static readonly StringComparer PathComparer = StringComparer.InvariantCultureIgnoreCase;
        private static readonly StringComparison PathComparison = StringComparison.InvariantCultureIgnoreCase;

        // Files with following extensions are ALWAYS excluded from the result
        private static readonly string[] ExecutableExtensions = { ".exe", ".dll", ".cpl", ".scr" };

        // Instance fields
        private readonly DirectorySourceSettings _settings;
        private readonly ArtifactSummaryGenerator _summaryGenerator;
        private readonly ConfigurableNavigatorStreamFactory _navigatorFactory;

        // [WMR 20180813] NEW
        // Use Lazy<T> to synchronize collection (re-)loading (=> lock-free reading)
        private Lazy<List<ArtifactSummary>> _lazyArtifactSummaries;

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the default <see cref="SpecificationDirectory"/>
        /// and using the default <see cref="DirectorySourceSettings"/>.
        /// <para>
        /// Initialization is thread-safe. The source ensures that only a single thread will
        /// collect the artifact summaries, while any other threads will block.
        /// </para>
        /// </summary>
        public DirectorySource()
            : this(SpecificationDirectory, null, false) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified <paramref name="contentDirectory"/>
        /// and using the default <see cref="DirectorySourceSettings"/>.
        /// </summary>
        /// <para>
        /// Initialization is thread-safe. The source ensures that only a single thread will
        /// collect the artifact summaries, while any other threads will block.
        /// </para>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public DirectorySource(string contentDirectory)
            : this(contentDirectory, null, false) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified content directory and optionally also from subdirectories.
        /// </summary>
        /// <param name="includeSubdirectories">
        /// Determines wether the <see cref="DirectorySource"/> should also
        /// recursively scan all subdirectories of the specified content directory.
        /// </param>
        [Obsolete("Instead, use DirectorySource(DirectorySourceSettings settings)")]
        public DirectorySource(bool includeSubdirectories)
            : this(SpecificationDirectory, new DirectorySourceSettings(includeSubdirectories), false) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified <paramref name="contentDirectory"/> and optionally also from subdirectories.
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="includeSubdirectories">
        /// Determines wether the <see cref="DirectorySource"/> should also
        /// recursively scan all subdirectories of the specified content directory.
        /// </param>
        [Obsolete("Instead, use DirectorySource(string contentDirectory, DirectorySourceSettings settings)")]
        public DirectorySource(string contentDirectory, bool includeSubdirectories)
            : this(contentDirectory, new DirectorySourceSettings(includeSubdirectories), false) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// using the specified <see cref="DirectorySourceSettings"/>.
        /// </summary>
        /// <param name="settings">Configuration settings that control the behavior of the <see cref="DirectorySource"/>.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
        public DirectorySource(DirectorySourceSettings settings)
            : this(SpecificationDirectory, settings, true) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified <paramref name="contentDirectory"/>
        /// and using the specified <see cref="DirectorySourceSettings"/>.
        /// <para>
        /// Initialization is thread-safe. The source ensures that only a single thread will
        /// collect the artifact summaries, while any other threads will block.
        /// </para>
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="settings">Configuration settings that control the behavior of the <see cref="DirectorySource"/>.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
        public DirectorySource(string contentDirectory, DirectorySourceSettings settings)
            : this(contentDirectory, settings, true) { }

        // Internal ctor
        DirectorySource(string contentDirectory, DirectorySourceSettings settings, bool cloneSettings)
        {
            ContentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            // [WMR 20171023] Clone specified settings to prevent shared state
            _settings = settings != null 
                ? (cloneSettings ? new DirectorySourceSettings(settings) : settings)
                : DirectorySourceSettings.CreateDefault();
            _summaryGenerator = new ArtifactSummaryGenerator(_settings.ExcludeSummariesForUnknownArtifacts);
            _navigatorFactory = new ConfigurableNavigatorStreamFactory(_settings.XmlParserSettings, _settings.JsonParserSettings)
            {
                ThrowOnUnsupportedFormat = false
            };
            // Initialize Lazy
            Refresh();
        }

        /// <summary>Returns the content directory as specified to the constructor.</summary>
        public string ContentDirectory { get; }

        /// <summary>
        /// The default directory this artifact source will access for its files.
        /// </summary>
        public static string SpecificationDirectory => DirectorySourceSettings.SpecificationDirectory;

        /// <summary>
        /// Gets or sets a value that determines wether the <see cref="DirectorySource"/> should
        /// also include artifacts from (nested) subdirectories of the specified content directory.
        /// <para>
        /// Returns <c>false</c> by default.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Enabling this setting requires the <see cref="DirectorySource"/> instance
        /// to recursively scan all xml and json files that exist in the target directory
        /// structure, which could unexpectedly turn into a long running operation.
        /// Therefore, consumers should usually try to avoid to enable this setting when
        /// the DirectorySource is targeting:
        /// <list type="bullet">
        /// <item>
        /// <description>Directories with many deeply nested subdirectories</description>
        /// </item>
        /// <item>
        /// <description>Common folders such as Desktop, My Documents etc.</description>
        /// </item>
        /// <item>
        /// <description>Drive root folders such as C:\</description>
        /// </item>
        /// </list>
        /// </remarks>
        public bool IncludeSubDirectories
        {
            get { return _settings.IncludeSubDirectories; }
            set { _settings.IncludeSubDirectories = value; Refresh(); }
        }

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// Multiple masks can be split by '|'.
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before any <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
        /// </item>
        /// <item>
        /// <term>?</term>
        /// <description>Matches any single character</description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Mask = "v2*.*|*.StructureDefinition.*";</code>
        /// </example>
        public string Mask
        {
            get { return _settings.Mask; }
            set { _settings.Mask = value; Refresh(); }
        }

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
        /// </item>
        /// <item>
        /// <term>?</term>
        /// <description>Matches any single character</description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Masks = new string[] { "v2*.*", "*.StructureDefinition.*" };</code>
        /// </example>
        public string[] Masks
        {
            get { return _settings.Masks; }
            set { _settings.Masks = value; Refresh(); }
        }

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of subdirectories of the content directory.
        /// The source will only provide resources from subdirectories that match the specified include mask(s).
        /// The source will ignore all subdirectories that don't match the specified include mask(s).
        /// </summary>
        /// <remarks>
        /// Include filters are applied after <see cref="Mask"/> filters and before <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a directory name.</description>
        /// </item>
        /// <item>
        /// <term>**</term>
        /// <description>
        /// Recursive wildcard.
        /// For example, <c>/hello/**/*</c> matches all descendants of <c>/hello</c>.
        /// </description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Includes = new string[] { "profiles/**/*", "**/valuesets" };</code>
        /// </example>
        public string[] Includes
        {
            get { return _settings.Includes; }
            set { _settings.Includes = value; Refresh(); }
        }

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of subdirectories of the content directory.
        /// The source will ignore all subdirectories that match the specified exclude mask(s).
        /// The source will only provide resources from subdirectories that don't match the specified exclude mask(s).
        /// </summary>
        /// <remarks>
        /// Exclude filters are applied last, after any <see cref="Mask"/> and <see cref="Includes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a directory name.</description>
        /// </item>
        /// <item>
        /// <term>**</term>
        /// <description>
        /// Recursive wildcard.
        /// For example, <c>/hello/**/*</c> matches all descendants of <c>/hello</c>.
        /// </description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Excludes = new string[] { "profiles/**/old", "temp/**/*" };</code>
        /// </example>
        public string[] Excludes
        {
            get { return _settings.Excludes; }
            set { _settings.Excludes = value; Refresh(); }
        }

        // Note: DuplicateFilenameResolution must be in sync with FhirSerializationFormats

        /// <summary>
        /// Specifies how the <see cref="DirectorySource"/> should process duplicate files with multiple serialization formats.
        /// </summary>
        public enum DuplicateFilenameResolution
        {
            /// <summary>Prefer file with ".xml" extension over duplicate file with ".json" extension.</summary>
            PreferXml,
            /// <summary>Prefer file with ".json" extension over duplicate file with ".xml" extension.</summary>
            PreferJson,
            /// <summary>Return all files, do not filter duplicates.</summary>
            KeepBoth
        }

        /// <summary>Gets or sets a value that determines how to process duplicate files with multiple serialization formats.</summary>
        /// <remarks>The default value is <see cref="DirectorySource.DuplicateFilenameResolution.PreferXml"/>.</remarks>
        public DuplicateFilenameResolution FormatPreference
        {
            get { return _settings.FormatPreference; }
            set { _settings.FormatPreference = value; Refresh(); }
        }

        /// <summary>
        /// Determines if the <see cref="DirectorySource"/> instance should harvest artifact
        /// summary information in parallel on the thread pool.
        /// </summary>
        /// <remarks>
        /// By default, the <see cref="DirectorySource"/> harvests artifact summaries serially
        /// on the calling thread. However if this option is enabled, then the DirectorySource
        /// performs summary harvesting in parallel on the thread pool, in order to speed up
        /// the process. This is especially effective when the content directory contains many
        /// (nested) subfolders and files.
        /// </remarks>
        public bool MultiThreaded
        {
            get { return _settings.MultiThreaded; }
            set { _settings.MultiThreaded = value; } // Refresh();
        }

        /// <summary>
        /// Determines wether the <see cref="DirectorySource"/> should exclude
        /// artifact summaries for non-parseable (invalid or non-FHIR) content files.
        /// <para>
        /// By default (<c>false</c>), the source will generate summaries for all files
        /// that exist in the specified content directory and match the specified mask,
        /// including files that cannot be parsed (e.g. invalid or non-FHIR content).
        /// </para>
        /// <para>
        /// If <c>true</c>, then the source will only generate summaries for valid
        /// FHIR artifacts that exist in the specified content directory and match the
        /// specified mask. Unparseable files are ignored and excluded from the list
        /// of artifact summaries.
        /// </para>
        /// </summary>
        public bool ExcludeSummariesForUnknownArtifacts
        {
            get { return _settings.ExcludeSummariesForUnknownArtifacts; }
            set {
                _settings.ExcludeSummariesForUnknownArtifacts = value;
                _summaryGenerator.ExcludeSummariesForUnknownArtifacts = value;
                Refresh();
            }
        }

        /// <summary>Gets the configuration settings that the behavior of the PoCo parser.</summary>
        public ParserSettings ParserSettings => _settings.ParserSettings;

        /// <summary>Gets the configuration settings that control the behavior of the XML parser.</summary>
        public FhirXmlParsingSettings XmlParserSettings => _settings.XmlParserSettings;

        /// <summary>Gets the configuration settings that control the behavior of the JSON parser.</summary>
        public FhirJsonParsingSettings JsonParserSettings => _settings.JsonParserSettings;


        #region Refresh

        /// <summary>
        /// Re-index the specified content directory.
        /// <para>
        /// Clears the internal artifact summary cache.
        /// Re-indexes the current <see cref="ContentDirectory"/> and generates new summaries on demand,
        /// during the next resolving call.
        /// </para>
        /// </summary>
        public void Refresh()
        {
            Refresh(false);
        }

        /// <summary>
        /// Re-index the specified content directory.
        /// <para>
        /// Clears the internal artifact summary cache.
        /// Re-indexes the current <see cref="ContentDirectory"/> and generates new summaries.
        /// </para>
        /// <para>
        /// If <paramref name="force"/> equals <c>true</c>, then the source performs the re-indexing immediately.
        /// Otherwise, if <paramref name="force"/> equals <c>false</c>, then re-indexing is performed on demand
        /// during the next resolving request.
        /// </para>
        /// </summary>
        /// <param name="force">
        /// Determines if the source should perform re-indexing immediately (<c>true</c>) or on demand (<c>false</c>).
        /// </param>
        public void Refresh(bool force)
        {
            // Re-create lazy collection
            // Assignment is atomic, no locking necessary
            // Only single thread can call loadSummaries, any other threads will block
            // Runtime exceptions during initialization are promoted to Value property getter
            _lazyArtifactSummaries = new Lazy<List<ArtifactSummary>>(loadSummaries, LazyThreadSafetyMode.ExecutionAndPublication);
            if (force)
            {
                // [WMR 20180813] Verified: compiler does NOT remove this call in Release build
                var dummy = _lazyArtifactSummaries.Value;
            }
        }

        /// <summary>
        /// Re-index one or more specific artifact file(s).
        /// This method is NOT thread-safe!
        /// <para>
        /// Notifies the <see cref="DirectorySource"/> that specific files in the current
        /// <see cref="ContentDirectory"/> have been created, updated or deleted.
        /// The <paramref name="filePaths"/> argument should specify an array of artifact
        /// file paths that (may) have been deleted, modified or created.
        /// </para>
        /// <para>
        /// The source will:
        /// <list type="number">
        /// <item>remove any existing summary information for the specified artifacts, if available;</item>
        /// <item>try to harvest updated summary information from the specified artifacts, if they still exist.</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="filePaths">An array of artifact file path(s).</param>
        /// <returns>
        /// <c>true</c> if any summary information was updated, or <c>false</c> otherwise.
        /// </returns>
        public bool Refresh(params string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
            {
                // throw Error.ArgumentNullOrEmpty(nameof(filePaths));
                return true; // NOP
            }

            bool result = false;

            // [WMR 20180814] Possible protection:
            // - Save current thread id in ctor
            // - In this method, compare current thread id with saved id; throw if mismatch
            // However this won't detect Refresh on main tread while bg threads are reading

            var summaries = GetSummaries();
            foreach (var filePath in filePaths)
            {
                bool exists = File.Exists(filePath);
                if (!exists)
                {
                    // File was deleted; remove associated summaries
                    result |= summaries.RemoveAll(s => PathComparer.Equals(filePath, s.Origin)) > 0;
                }
                else if (!summaries.Any(s => PathComparer.Equals(filePath, s.Origin)))
                {
                    // File was added; generate and add new summary
                    var newSummaries = _summaryGenerator.Generate(filePath, _settings.SummaryDetailsHarvesters);
                    summaries.AddRange(newSummaries);
                    result |= newSummaries.Count > 0;
                }
            }
            return result;
        }

        #endregion

        #region IArtifactSource

        /// <summary>Returns a list of artifact filenames.</summary>
        public IEnumerable<string> ListArtifactNames()
        {
            return GetFileNames();
        }

        /// <summary>
        /// Load the artifact with the specified file name.
        /// Also accepts relative file paths.
        /// </summary>
        /// <exception cref="InvalidOperationException">More than one file exists with the specified name.</exception>
        public Stream LoadArtifactByName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));
            var fullFileName = GetFilePaths().SingleOrDefault(path => path.EndsWith(Path.DirectorySeparatorChar + name, PathComparison));
            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }

        #endregion

        #region IConformanceSource

        /// <summary>List all resource uris, optionally filtered by type.</summary>
        /// <param name="filter">A <see cref="ResourceType"/> enum value.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of uri strings.</returns>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            // [WMR 20180813] Do not return null values from non-FHIR artifacts (ResourceUri = null)
            // => OfResourceType filters valid FHIR artifacts (ResourceUri != null)
            return GetSummaries().OfResourceType(filter).Select(dsi => dsi.ResourceUri);
        }

        /// <summary>Resolve the <see cref="ValueSet"/> resource with the specified codeSystem system.</summary>
        public ValueSet FindValueSetBySystem(string system)
        {
            // if (system == null) throw Error.ArgumentNull(nameof(system));
            var summary = GetSummaries().ResolveValueSet(system);
            // return summary != null ? getResourceFromScannedSource<ValueSet>(summary) : null;
            return loadResourceInternal<ValueSet>(summary);
        }

        /// <summary>Resolve <see cref="ConceptMap"/> resources with the specified source and/or target uri(s).</summary>
        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
            {
                throw Error.ArgumentNull(nameof(targetUri), $"{nameof(sourceUri)} and {nameof(targetUri)} arguments cannot both be null");
            }
            var summaries = GetSummaries().FindConceptMaps(sourceUri, targetUri);
            return summaries.Select(summary => loadResourceInternal<ConceptMap>(summary)).Where(r => r != null);
        }

        /// <summary>Resolve the <see cref="NamingSystem"/> resource with the specified uniqueId.</summary>
        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            var summary = GetSummaries().ResolveNamingSystem(uniqueId);
            return loadResourceInternal<NamingSystem>(summary);
        }

        #endregion

        #region ISummarySource

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with key information about each FHIR artifact provided by the source.</summary>
        public IEnumerable<ArtifactSummary> ListSummaries()
        {
            return GetSummaries();
        }

        /// <summary>
        /// Load the resource from which the specified summary was generated.
        /// <para>
        /// This implementation annotates returned resource instances with an <seealso cref="OriginAnnotation"/>
        /// that captures the value of the <see cref="ArtifactSummary.Origin"/> property.
        /// The <seealso cref="OriginAnnotationExtensions.GetOrigin(Resource)"/> extension method 
        /// provides access to the annotated location.
        /// </para>
        /// </summary>
        /// <param name="summary">An <see cref="ArtifactSummary"/> instance generated by this source.</param>
        /// <returns>A new <see cref="Resource"/> instance, or <c>null</c>.</returns>
        /// <remarks>
        /// The <see cref="ArtifactSummary.Origin"/> and <see cref="ArtifactSummary.Position"/>
        /// summary properties allow the source to identify and resolve the artifact.
        /// </remarks>
        public Resource LoadBySummary(ArtifactSummary summary)
        {
            if (summary == null) { throw Error.ArgumentNull(nameof(summary)); }
            return loadResourceInternal<Resource>(summary);

        }

        #endregion

        #region IResourceResolver

        /// <summary>Resolve the resource with the specified uri.</summary>
        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            var summary = GetSummaries().ResolveByUri(uri);
            return loadResourceInternal<Resource>(summary);
        }

        /// <summary>Resolve the conformance resource with the specified canonical url.</summary>
        public Resource ResolveByCanonicalUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            var summary = GetSummaries().ResolveByCanonicalUri(uri);
            return loadResourceInternal<Resource>(summary);
        }

        #endregion

        #region Private members

        /// <summary>
        /// List all files present in the directory (matching the mask, if given)
        /// </summary>
        private List<string> discoverFiles()
        {
            var masks = _settings.Masks ?? DirectorySourceSettings.DefaultMasks; // (new[] { "*.*" });

            var contentDirectory = ContentDirectory;

            // Add files present in the content directory
            var filePaths = new List<string>();

            // [WMR 20170817] NEW
            // Safely enumerate files in specified path and subfolders, recursively
            filePaths.AddRange(safeGetFiles(contentDirectory, masks, _settings.IncludeSubDirectories));

            var includes = Includes;
            if (includes?.Length > 0)
            {
                var includeFilter = new FilePatternFilter(includes);
                filePaths = includeFilter.Filter(contentDirectory, filePaths).ToList();
            }

            var excludes = Excludes;
            if (excludes?.Length > 0)
            {
                var excludeFilter = new FilePatternFilter(excludes, negate: true);
                filePaths = excludeFilter.Filter(contentDirectory, filePaths).ToList();
            }

            return filePaths;
        }

        // [WMR 20170817]
        // Safely enumerate files in specified path and subfolders, recursively
        // Ignore files & folders with Hidden and/or System attributes
        // Ignore subfolders with insufficient access permissions
        // https://stackoverflow.com/a/38959208
        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-enumerate-directories-and-files

        private static IEnumerable<string> safeGetFiles(string path, IEnumerable<string> masks, bool searchSubfolders)
        {
            if (File.Exists(path))
            {
                return new string[] { path };
            }

            if (!Directory.Exists(path))
            {
                return Enumerable.Empty<string>();
            }

            // Not necessary; caller prepareFiles() validates the mask
            //if (!masks.Any())
            //{
            //    return Enumerable.Empty<string>();
            //}

            Queue<string> folders = new Queue<string>();
            // Use HashSet to remove duplicates; different masks could match same file(s)
            HashSet<string> files = new HashSet<string>();
            folders.Enqueue(path);

            while (folders.Count != 0)
            {
                string currentFolder = folders.Dequeue();
                var currentDirInfo = new DirectoryInfo(currentFolder);

                // local helper function to validate file/folder attributes, exclude system and/or hidden
                bool isValid(FileAttributes attr) => (attr & (FileAttributes.System | FileAttributes.Hidden)) == 0;
                
                // local helper function to filter executables (*.exe, *.dll)
                bool isExtensionSafe(string extension) => !ExecutableExtensions.Contains(extension, PathComparer);

                foreach (var mask in masks)
                {
                    try
                    {
                        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-enumerate-directories-and-files
                        // "Although you can immediately enumerate all the files in the subdirectories of a
                        // parent directory by using the AllDirectories search option provided by the SearchOption
                        // enumeration, unauthorized access exceptions (UnauthorizedAccessException) may cause the
                        // enumeration to be incomplete. If these exceptions are possible, you can catch them and
                        // continue by first enumerating directories and then enumerating files."

                        // Explicitly ignore system & hidden files
                        var curFiles = currentDirInfo.EnumerateFiles(mask, SearchOption.TopDirectoryOnly);
                        foreach (var file in curFiles)
                        {
                            // Skip system & hidden files
                            // Exclude executables (*.exe, *.dll)
                            if (isValid(file.Attributes) && isExtensionSafe(file.Extension))
                            {
                                files.Add(file.FullName);
                            }
                        }
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        // Do Nothing
                        Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(harvestSummaries)}] {ex.GetType().Name} while enumerating files in '{currentFolder}':\r\n{ex.Message}");
                    }
#else
                    catch { }
#endif
                }

                if (searchSubfolders)
                {
                    try
                    {
                        var subFolders = currentDirInfo.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
                        foreach (var subFolder in subFolders)
                        {
                            // Skip system & hidden folders
                            if (isValid(subFolder.Attributes))
                            {
                                folders.Enqueue(subFolder.FullName);
                            }
                        }
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        // Do Nothing
                        Debug.WriteLine($"Error enumerating subfolders of '{currentFolder}': {ex.Message}");
                    }
#else
                    catch { }
#endif

                }
            }

            return files.AsEnumerable();
        }

        // Internal for unit testing purposes
        internal static List<string> ResolveDuplicateFilenames(List<string> allFilenames, DuplicateFilenameResolution preference)
        {
            var result = new List<string>();
            var xmlOrJson = new List<string>();

            foreach (var filename in allFilenames.Distinct())
            {
                if (FhirFileFormats.HasXmlOrJsonExtension(filename))
                    xmlOrJson.Add(filename);
                else
                    result.Add(filename);
            }

            var groups = xmlOrJson.GroupBy(path => fullPathWithoutExtension(path));

            foreach (var group in groups)
            {
                if (group.Count() == 1 || preference == DuplicateFilenameResolution.KeepBoth)
                    result.AddRange(group);
                else
                {
                    // count must be 2
                    var first = group.First();
                    if (preference == DuplicateFilenameResolution.PreferXml && FhirFileFormats.HasXmlExtension(first))
                        result.Add(first);
                    else if (preference == DuplicateFilenameResolution.PreferJson && FhirFileFormats.HasJsonExtension(first))
                        result.Add(first);
                    else
                        result.Add(group.Skip(1).First());
                }
            }

            return result;

        }

        private static string fullPathWithoutExtension(string fullPath) => Path.ChangeExtension(fullPath, null);

        /// <summary>Scan all xml files found by prepareFiles and find conformance resources and their id.</summary>
        private List<ArtifactSummary> loadSummaries()
        {
            var files = discoverFiles();

            var settings = _settings;
            var uniqueArtifacts = ResolveDuplicateFilenames(files, settings.FormatPreference);
            var summaries = harvestSummaries(uniqueArtifacts);

#if false
            // [WMR 20180914] OBSOLETE
            // Conflict will prevent clients from retrieving list of summaries...
            // Instead, throw in Resolve methods

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var duplicates =
                from cr in summaries.ConformanceResources()
                let canonical = cr.GetConformanceCanonicalUrl()
                where canonical != null
                group cr by canonical into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            if (duplicates.Any())
            {
                // [WMR 20171023] TODO: Allow configuration, e.g. optional callback delegate
                throw new CanonicalUrlConflictException(duplicates.Select(d => new CanonicalUrlConflictException.CanonicalUrlConflict(d.Key, d.Select(ci => ci.Origin))));
            }
#endif

            return summaries;
        }

        private List<ArtifactSummary> harvestSummaries(List<string> paths)
        {
            // [WMR 20171023] Note: some files may no longer exist

            var cnt = paths.Count;
            var scanResult = new List<ArtifactSummary>(cnt);
            var harvesters = _settings.SummaryDetailsHarvesters;

            if (!_settings.MultiThreaded)
            {
                foreach (var filePath in paths)
                {
                    var summaries = _summaryGenerator.Generate(filePath, harvesters);

                    // [WMR 20180423] Generate may return null, e.g. if specified file has unknown extension
                    if (summaries != null)
                    {
                        scanResult.AddRange(summaries);
                    }
                }
            }
            else
            {
                // Optimization: use Task.Parallel.ForEach to process files in parallel
                // More efficient then creating task per file (esp. if many files)
                //
                // For netstandard13, add NuGet package System.Threading.Tasks.Parallel
                //
                //   <ItemGroup Condition=" '$(TargetFramework)' != 'net45' ">
                //    <PackageReference Include="System.Threading.Tasks.Parallel" Version="4.3.0" />
                //   </ItemGroup>
                //
                // TODO:
                // - Support TimeOut
                // - Support CancellationToken (how to inject?)

                // Pre-allocate results array, one entry per file
                // Each entry receives a list with summaries harvested from a single file (Bundles return 0..*)
                var summaries = new List<ArtifactSummary>[cnt];
                try
                {
                    // Process files in parallel
                    var loopResult = Parallel.For(0, cnt,
                        // new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount },
                        i =>
                        {
                            // Harvest summaries from single file
                            // Save each result to a separate array entry (no locking required)
                            summaries[i] = _summaryGenerator.Generate(paths[i], harvesters);
                        });
                }
                catch (AggregateException aex)
                {
                    // ArtifactSummaryHarvester.HarvestAll catches and returns exceptions using ArtifactSummary.FromException
                    // However Parallel.For may still throw, e.g. due to time out or cancel

                    // var isCanceled = ex.InnerExceptions.OfType<TaskCanceledException>().Any();
                    Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(harvestSummaries)}] {aex.GetType().Name}: {aex.Message}"
                        + aex.InnerExceptions?.Select(ix => $"\r\n\t{ix.GetType().Name}: {ix.Message}"));

                    // [WMR 20171023] Return exceptions via ArtifactSummary.FromException
                    // Or unwrap all inner exceptions?
                    // scanResult.Add(ArtifactSummary.FromException(aex));
                    scanResult.AddRange(aex.InnerExceptions.Select(ArtifactSummary.FromException));
                }
                // Aggregate completed results into single list
                scanResult.AddRange(summaries.SelectMany(r => r ?? Enumerable.Empty<ArtifactSummary>()));
            }

            return scanResult;
        }

        /// <summary>Returns <c>null</c> if the specified <paramref name="summary"/> equals <c>null</c>.</summary>
        T loadResourceInternal<T>(ArtifactSummary summary) where T : Resource
        {
            if (summary == null) { return null; }

            // File path of the containing resource file (could be a Bundle)
            var origin = summary.Origin;
            if (string.IsNullOrEmpty(origin))
            {
                throw Error.Argument($"Unable to load resource from summary. The '{nameof(ArtifactSummary.Origin)}' information is unavailable.");
            }

            var pos = summary.Position;
            if (string.IsNullOrEmpty(pos))
            {
                throw Error.Argument($"Unable to load resource from summary. The '{nameof(ArtifactSummary.Position)}' information is unavailable.");
            }

            // Always use the current Xml/Json parser settings
            var settings = _settings;
            var factory = _navigatorFactory;
            settings.XmlParserSettings.CopyTo(factory.XmlParsingSettings);
            settings.JsonParserSettings.CopyTo(factory.JsonParsingSettings);

            // Also use the current PoCo parser settings
            var pocoSettings = PocoBuilderSettings.CreateDefault();
            settings.ParserSettings?.CopyTo(pocoSettings);

            T result = null;

            using (var navStream = factory.Create(origin))
            {
                // Handle exceptions & null return values?
                // e.g. file may have been deleted/renamed since last scan

                // Advance stream to the target resource (e.g. specific Bundle entry)
                if (navStream != null && navStream.Seek(pos))
                {
                    // Create navigator for the target resource
                    // Current property uses the specified Xml/JsonParsingSettings for parsing
                    var nav = navStream.Current;
                    if (nav != null)
                    {
                        // Parse target resource from navigator
                        result = nav.ToPoco<T>(pocoSettings);

                        // Add origin annotation
                        result?.SetOrigin(origin);
                    }
                }
            }

            return result;
        }

        #endregion

        #region Protected members

        /// <summary>
        /// Gets a list of <see cref="ArtifactSummary"/> instances for files in the specified <see cref="ContentDirectory"/>.
        /// The artifact summaries are loaded on demand.
        /// </summary>
        protected List<ArtifactSummary> GetSummaries() => _lazyArtifactSummaries.Value;

        // Note: Need distinct for bundled resources

        /// <summary>
        /// Enumerate distinct file paths in the specified <see cref="ContentDirectory"/>.
        /// The underlying artifact summaries are loaded on demand.
        /// </summary>
        protected IEnumerable<string> GetFilePaths() => GetSummaries().Select(s => s.Origin).Distinct();

        /// <summary>
        /// Enumerate distinct file names in the specified <see cref="ContentDirectory"/>.
        /// The underlying artifact summaries are loaded on demand.
        /// </summary>
        protected IEnumerable<string> GetFileNames() => GetSummaries().Select(s => Path.GetFileName(s.Origin)).Distinct();

        #endregion

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name} for '{ContentDirectory}' : '{Mask}'"
            + (IncludeSubDirectories ? " (with subdirs)" : null)
            + (_lazyArtifactSummaries.IsValueCreated ? $" {_lazyArtifactSummaries.Value.Count} resources" : " (summaries not yet loaded)");
    }

}