/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

// [WMR 20171023] TODO
// - Allow configuration of duplicate canonical url handling strategy

#if NET_FILESYSTEM

// [WMR 20171102] NEW
// Implement thread-safe access
// Only necessary for clients that trigger Refresh() by changing settings at runtime
// Use locking to prevent multiple threads from calling prepareXXX() simultaneously
// Note: without locking, multiple threads could initiate a re-scan, but the results of
// all but one thread would be discarded. Considering that scanning is a costly operation,
// we use locking to avoid unnecessary I/O and processing.
// Performance tests seem to indicate that locking does not add significant overhead to
// simple single-threaded use.

#define THREADSAFE

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
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from a directory on disk. Thread-safe.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class DirectorySource : ISummarySource, IConformanceSource, IArtifactSource
    {
        // netstandard has no CurrentCultureIgnoreCase comparer
#if DOTNETFW
        private static readonly StringComparer ExtensionComparer = StringComparer.InvariantCultureIgnoreCase;
        private static readonly StringComparison ExtensionComparison = StringComparison.InvariantCultureIgnoreCase;
#else
        private static readonly StringComparer ExtensionComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringComparison ExtensionComparison = StringComparison.OrdinalIgnoreCase;
#endif
        // Files with following extensions are ALWAYS excluded from the result
        private static readonly string[] ExecutableExtensions = { ".exe", ".dll", ".cpl", ".scr" };


        private readonly DirectorySourceSettings _settings;
        private readonly string _contentDirectory;

        private List<string> _artifactFilePaths;
        private List<ArtifactSummary> _artifactSummaries;

#if THREADSAFE
        // Shared synchronization object for _artifactFilePaths & _artifactSummaries
        private readonly object _syncRoot = new object();
#endif

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the default <see cref="SpecificationDirectory"/>
        /// and using the default <see cref="DirectorySourceSettings"/>.
        /// </summary>
        public DirectorySource() : this(SpecificationDirectory) { }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified content directory and using the default <see cref="DirectorySourceSettings"/>.
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public DirectorySource(string contentDirectory)
        {
            _contentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            _settings = new DirectorySourceSettings();
        }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified content directory and using the specified <see cref="DirectorySourceSettings"/>.
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="settings">Configuration settings that control the behavior of the <see cref="DirectorySource"/>.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
        public DirectorySource(string contentDirectory, DirectorySourceSettings settings)
        {
            _contentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            // [WMR 20171023] Always copy the specified settings, to prevent shared state
            _settings = new DirectorySourceSettings(settings);
        }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources
        /// from the specified content directory and optionally also from subdirectories.
        /// </summary>
        /// <param name="includeSubdirectories">
        /// Determines wether the <see cref="DirectorySource"/> should also
        /// recursively scan all subdirectories of the specified content directory.
        /// </param>
        [Obsolete("Instead, use DirectorySource(DirectorySourceSettings settings)")]
        public DirectorySource(bool includeSubdirectories) : this(SpecificationDirectory, includeSubdirectories)
        {
            //
        }

        [Obsolete("Instead, use DirectorySource(string contentDirectory, DirectorySourceSettings settings)")]
        public DirectorySource(string contentDirectory, bool includeSubdirectories)
            : this(contentDirectory,
                  new DirectorySourceSettings() { IncludeSubDirectories = includeSubdirectories })
        {
            //
        }

        /// <summary>Returns the content directory as specified to the constructor.</summary>
        public string ContentDirectory => _contentDirectory;

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

        #region Refresh

        /// <summary>
        /// Re-index the specified content directory.
        /// <para>
        /// Clears the internal artifact file path and summary caches.
        /// Re-indexes the current <see cref="ContentDirectory"/> and generates new summaries on demand,
        /// during the next resolving call.
        /// </para>
        /// </summary>
        public void Refresh() => Refresh(false);

        /// <summary>
        /// Re-index the specified content directory.
        /// <para>
        /// Clears the internal artifact file path and summary caches.
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
            // OPTIMIZE: Implement incremental update
            // - Remove file paths & summaries for files that no longer exist
            // - Update summaries for files with new modification date
            // - Add new summaries for new files
#if THREADSAFE
            lock (_syncRoot)
#endif
            {
                _artifactFilePaths = null;
                _artifactSummaries = null;
                if (force)
                {
                    prepareSummaries();
                }
            }
        }

        /// <summary>
        /// Re-index one or more specific  artifact file(s).
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
        /// <param name="filePaths">One or more artifact file path(s).</param>
        /// <returns>
        /// <c>true</c> if succesful, i.e. if matching cache items have been evicted, or <c>false</c> otherwise.
        /// </returns>
        public bool Refresh(params string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
            {
                throw Error.ArgumentNullOrEmpty(nameof(filePaths));
            }

            bool result = false;

#if THREADSAFE
            lock (_syncRoot)
#endif
            {
                var artifactFilePaths = _artifactFilePaths;
                if (artifactFilePaths == null)
                {
                    // Cache is empty, perform full scan on demand
                    return false;
                }
                // Update file paths
                foreach (var filePath in filePaths)
                {
                    // Update file paths
                    bool exists = File.Exists(filePath);
                    if (!exists)
                    {
                        // Return true if existing file path was evicted from cache
                        result = artifactFilePaths.Remove(filePath);
                    }
                    else if (!artifactFilePaths.Contains(filePath))
                    {
                        // Discovered new artifact; cache file path and return true
                        artifactFilePaths.Add(filePath);
                        result = true;
                    }

                    // Update summaries (if cached)
                    var artifactSummaries = _artifactSummaries;
                    if (artifactSummaries != null)
                    {
                        if (artifactSummaries.RemoveAll(s => StringComparer.OrdinalIgnoreCase.Equals(filePath, s.Origin)) > 0)
                        {
                            // Evicted some existing summaries from the cache; return true
                            result = true;
                        }
                        if (exists)
                        {
                            // May fail, e.g. if another thread/process has deleted the target file
                            // Generate will catch exceptions and return empty list
                            var summaries = ArtifactSummaryGenerator.Generate(filePath, _settings.SummaryDetailsHarvesters);
                            artifactSummaries.AddRange(summaries);
                            if (summaries.Count > 0)
                            {
                                // Added some new/updated summaries to the cache; return true
                                result = true;
                            }
                        }
                    }

                }
            }

            return result;
        }

        #endregion

        #region IArtifactSource

        /// <summary>Returns a list of artifact filenames.</summary>
        public IEnumerable<string> ListArtifactNames() => GetFilePaths().Select(path => Path.GetFileName(path));

        /// <summary>Load the artifact with the specified filename.</summary>
        public Stream LoadArtifactByName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));
            var fullFileName = GetFilePaths().SingleOrDefault(path => path.EndsWith(Path.DirectorySeparatorChar + name, ExtensionComparison));
            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }

        #endregion

        #region IConformanceSource

        /// <summary>List all resource uris, optionally filtered by type.</summary>
        /// <param name="filter">A <see cref="ResourceType"/> enum value.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of uri strings.</returns>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
            => ListSummaries().OfResourceType(filter).Select(dsi => dsi.ResourceUri);

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
                throw Error.ArgumentNull(nameof(targetUri), "sourceUri and targetUri cannot both be null");
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
            GetSummaries();
            return _artifactSummaries.AsReadOnly();
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

        // IMPORTANT!
        // prepareFiles & prepareSummaries callers MUST lock on _syncLock

        /// <summary>
        /// Prepares the source by reading all files present in the directory (matching the mask, if given)
        /// </summary>
        private List<string> prepareFiles()
        {
            var filePaths = _artifactFilePaths;
            if (filePaths != null) return filePaths;

            var masks = _settings.Masks ?? (new[] { "*.*" });

            // Add files present in the content directory
            filePaths = new List<string>();

            // [WMR 20170817] NEW
            // Safely enumerate files in specified path and subfolders, recursively
            filePaths.AddRange(safeGetFiles(_contentDirectory, masks, _settings.IncludeSubDirectories));

            var includes = Includes;
            if (includes?.Length > 0)
            {
                var includeFilter = new FilePatternFilter(includes);
                filePaths = includeFilter.Filter(_contentDirectory, filePaths).ToList();
            }

            var excludes = Excludes;
            if (excludes?.Length > 0)
            {
                var excludeFilter = new FilePatternFilter(excludes, negate: true);
                filePaths = excludeFilter.Filter(_contentDirectory, filePaths).ToList();
            }

            _artifactFilePaths = filePaths;
            _artifactSummaries = null;
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
                bool isExtensionSafe(string extension) => !ExecutableExtensions.Contains(extension, ExtensionComparer);

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
        private void prepareSummaries()
        {
            var summaries = _artifactSummaries;
            if (summaries != null) return;
            prepareFiles();

            var settings = _settings;
            var uniqueArtifacts = ResolveDuplicateFilenames(_artifactFilePaths, settings.FormatPreference);
            summaries = harvestSummaries(uniqueArtifacts, settings.SummaryDetailsHarvesters, MultiThreaded);

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

            _artifactSummaries = summaries;
            return;
        }

        private static List<ArtifactSummary> harvestSummaries(List<string> paths, ArtifactSummaryHarvester[] harvesters, bool multiThreaded)
        {
            // [WMR 20171023] Note: some files may no longer exist

            var cnt = paths.Count;
            var scanResult = new List<ArtifactSummary>(cnt);

            if (!multiThreaded)
            {
                foreach (var filePath in paths)
                {
                    var summaries = harvesters == null || harvesters.Length == 0
                        ? ArtifactSummaryGenerator.Generate(filePath)
                        : ArtifactSummaryGenerator.Generate(filePath, harvesters);

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
                var results = new List<ArtifactSummary>[cnt];
                try
                {
                    // Process files in parallel
                    var loopResult = Parallel.For(0, cnt,
                        // new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount },
                        i =>
                        {
                            // Harvest summaries from single file
                            // Save each result to a separate array entry (no locking required)
                            results[i] = ArtifactSummaryGenerator.Generate(paths[i], harvesters);
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
                scanResult.AddRange(results.SelectMany(r => r ?? Enumerable.Empty<ArtifactSummary>()));
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
                throw Error.Argument($"Cannot load resource from summary. The {nameof(ArtifactSummary.Origin)} property value is empty or missing.");
            }

            var pos = summary.Position;
            if (string.IsNullOrEmpty(pos))
            {
                throw Error.Argument($"Cannot load resource from summary. The {nameof(ArtifactSummary.Position)} property value is empty or missing.");
            }

            T result = null;
            using (var navStream = DefaultNavigatorStreamFactory.Create(origin))
            {

                // Handle exceptions & null return values?
                // e.g. file may have been deleted/renamed since last scan

                // Advance stream to the target resource (e.g. specific Bundle entry)
                if (navStream != null && navStream.Seek(pos))
                {
                    // Create navigator for the target resource
                    var nav = navStream.Current;
                    if (nav != null)
                    {
                        // Parse target resource from navigator
                        var parser = new BaseFhirParser();
                        result = parser.Parse<T>(nav);
                        // Add origin annotation
                        result?.SetOrigin(origin);
                    }
                }
            }

            return result;
        }



        #endregion

        // <summary>Provides synchronized access to the list of file paths. May enter lock to re-generate the list on demand.</summary>

        /// <summary>
        /// Provides access to the list of artifact file paths.
        /// Ensures that the list is (re-)initialized on demand.
        /// Lazy initialization is synchronized (thread-safe).
        /// </summary>
        /// <returns>A list of strings.</returns>
        protected List<string> GetFilePaths()
        {
#if THREADSAFE
            lock (_syncRoot)
#endif
            {
                prepareFiles();
                return _artifactFilePaths;
            }
        }

        /// <summary>
        /// Provides access to the list of artifact summaries.
        /// Ensures that the list is (re-)initialized on demand.
        /// Lazy initialization is synchronized (thread-safe).
        /// </summary>
        /// <returns>A list of <see cref="ArtifactSummary"/> instances.</returns>
        protected List<ArtifactSummary> GetSummaries()
        {
#if THREADSAFE
            lock (_syncRoot)
#endif
            {
                prepareSummaries();
                return _artifactSummaries;
            }
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name} for '{_contentDirectory}'"
            + (_artifactFilePaths != null ? $" | {_artifactFilePaths.Count} files" : null)
            + (_artifactSummaries != null ? $" | {_artifactSummaries.Count} resources" : null);

    }

}

#endif
