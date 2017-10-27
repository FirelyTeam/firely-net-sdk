/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

// [WMR 20171023] TODO
// - Allow configuration of sync/async summary harvesting strategy
// - Allow configuration of duplicate canonical url handling strategy

// Enable for async, disable for sync
#define PREPARE_PARALLEL_FOR

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source.Summary;
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
#if NET_FILESYSTEM
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from directories with individual files
    /// </summary>
    public class DirectorySource : IConformanceSource, IArtifactSource
    {
        // netstandard has no CurrentCultureIgnoreCase comparer
#if DOTNETFW
        private static readonly StringComparer ExtensionComparer = StringComparer.InvariantCultureIgnoreCase;
        private static readonly StringComparison ExtensionComparison = StringComparison.InvariantCultureIgnoreCase;
#else
        private static readonly StringComparer ExtensionComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringComparison ExtensionComparison = StringComparison.OrdinalIgnoreCase;
#endif

        private readonly DirectorySourceSettings _settings;
        private readonly string _contentDirectory;

        private bool _filesPrepared = false;
        private List<string> _artifactFilePaths;

        private bool _resourcesPrepared = false;
        private List<ArtifactSummary> _resourceScanInformation;

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
        /// <list type="bullet">
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
        /// <list type="bullet">
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
        /// <list type="bullet">
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
        /// <list type="bullet">
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

        /// <summary>
        /// Gets or sets a value that determines how to process duplicate files with multiple serialization formats.
        /// </summary>
        public DuplicateFilenameResolution FormatPreference
        {
            get { return _settings.FormatPreference; }
            set { _settings.FormatPreference = value; Refresh(); }
        }

        /// <summary>Request a full re-scan of the specified content directory.</summary>
        public void Refresh()
        {
            _filesPrepared = false;
            _resourcesPrepared = false;
        }

        /// <summary>
        /// Returns a list of summary information describing all valid
        /// FHIR artifacts that exist in the specified content directory.
        /// </summary>
        public IEnumerable<ArtifactSummary> List()
        {
            prepareResources();
            return _resourceScanInformation; //.AsEnumerable();
        }

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with error information.</summary>
        /// <returns></returns>
        public IEnumerable<ArtifactSummary> Errors()
        {
            prepareResources();
            return _resourceScanInformation.Errors();
        }

        /// <summary>
        /// Returns a list of <see cref="ArtifactSummary"/> information for all the available FHIR artifacts.
        /// </summary>
        public IEnumerable<ArtifactSummary> List(ResourceType? filter)
        {
            prepareResources();

            IEnumerable<ArtifactSummary> scan = _resourceScanInformation;
            if (filter != null)
            {
                scan = scan.Where(dsi => dsi.ResourceType == filter);
            }
            return scan;
        }

        /// <summary>Returns a list of artifact filenames.</summary>
        public IEnumerable<string> ListArtifactNames()
        {
            prepareFiles();

            return _artifactFilePaths.Select(path => Path.GetFileName(path));
        }

        /// <summary>Load the artifact with the specified filename.</summary>
        public Stream LoadArtifactByName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));

            prepareFiles();

            var fullFileName = _artifactFilePaths.SingleOrDefault(path => path.EndsWith(Path.DirectorySeparatorChar + name, ExtensionComparison));
            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }


        /// <summary>Returns a list of resource uris.</summary>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            var scan = List(filter);
            return scan.Select(dsi => dsi.ResourceUri);
        }


        /// <summary>Resolve the resource with the specified uri.</summary>
        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation.ResolveByUri(uri);
            if (info == null) return null;

            return getResourceFromScannedSource<Resource>(info);

        }

        /// <summary>Resolve the conformance resource with the specified canonical url.</summary>
        public Resource ResolveByCanonicalUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation.ResolveByCanonicalUri(uri);
            if (info == null) return null;

            return getResourceFromScannedSource<Resource>(info);
        }

        public CodeSystem FindCodeSystemByValueSet(string valueSetUri)
        {
            prepareResources();

            var info = _resourceScanInformation.ResolveCodeSystem(valueSetUri);
            if (info == null) return null;

            return getResourceFromScannedSource<CodeSystem>(info);
        }

        /// <summary>Resolve ConceptMap resources with the specified source and/or target uri(s).</summary>
        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
            {
                throw Error.ArgumentNull(nameof(targetUri), "sourceUri and targetUri cannot both be null");
            }

            prepareResources();

            var infoList = _resourceScanInformation.ConceptMaps(sourceUri, targetUri);
            return infoList.Select(info => getResourceFromScannedSource<ConceptMap>(info)).Where(r => r != null);
        }

        /// <summary>Resolve the NamingSystem resource with the specified uniqueId.</summary>
        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            prepareResources();

            var info = _resourceScanInformation.ResolveNamingSystem(uniqueId);
            if (info == null) return null;

            return getResourceFromScannedSource<NamingSystem>(info);
        }

        #region Private members

        /// <summary>
        /// Prepares the source by reading all files present in the directory (matching the mask, if given)
        /// </summary>
        private void prepareFiles()
        {
            if (_filesPrepared) return;

            var masks = _settings.Masks ?? (new[] { "*.*" });

            // Add files present in the content directory
            var allFiles = new List<string>();

            // [WMR 20170817] NEW
            // Safely enumerate files in specified path and subfolders, recursively
            allFiles.AddRange(safeGetFiles(_contentDirectory, masks, _settings.IncludeSubDirectories));

            // Always remove *.exe" and "*.dll"
            allFiles.RemoveAll(name => Path.GetExtension(name) == ".exe" || Path.GetExtension(name) == ".dll");

            // var unsafeExtensions = new[] { ".exe", ".dll" };
            // allFiles.RemoveAll(name => unsafeExtensions.Contains(Path.GetExtension(name), ExtensionComparer));

            var includes = Includes;
            if (includes?.Length > 0)
            {
                var includeFilter = new FilePatternFilter(includes);
                allFiles = includeFilter.Filter(_contentDirectory, allFiles).ToList();
            }

            var excludes = Excludes;
            if (excludes?.Length > 0)
            {
                var excludeFilter = new FilePatternFilter(excludes, negate: true);
                allFiles = excludeFilter.Filter(_contentDirectory, allFiles).ToList();
            }

            _artifactFilePaths = allFiles;
            _filesPrepared = true;
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
                bool IsValid(FileAttributes attr) => (attr & (FileAttributes.System | FileAttributes.Hidden)) == 0;

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
                            if (IsValid(file.Attributes))
                            {
                                files.Add(file.FullName);
                            }
                        }
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        // Do Nothing
                        Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(scanPaths)}] {ex.GetType().Name} while enumerating files in '{currentFolder}':\r\n{ex.Message}");
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
                            if (IsValid(subFolder.Attributes))
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

        internal static List<string> ResolveDuplicateFilenames(List<string> allFilenames, DuplicateFilenameResolution preference)
        {
            var result = new List<string>();
            var xmlOrJson = new List<string>();

            foreach (var filename in allFilenames.Distinct())
            {
                if (FileFormats.HasXmlOrJsonExtension(filename))
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
                    if (preference == DuplicateFilenameResolution.PreferXml && FileFormats.HasXmlExtension(first))
                        result.Add(first);
                    else if (preference == DuplicateFilenameResolution.PreferJson && FileFormats.HasJsonExtension(first))
                        result.Add(first);
                    else
                        result.Add(group.Skip(1).First());
                }
            }

            return result;

        }

        static string fullPathWithoutExtension(string fullPath) => Path.ChangeExtension(fullPath, null);

        /// <summary>Scan all xml files found by prepareFiles and find conformance resources and their id.</summary>
        private void prepareResources()
        {
            if (_resourcesPrepared) return;
            prepareFiles();

            var settings = _settings;
            var uniqueArtifacts = ResolveDuplicateFilenames(_artifactFilePaths, _settings.FormatPreference);
            var scanInfo = _resourceScanInformation = scanPaths(uniqueArtifacts, settings.StreamFactory, settings.SummaryFactory, settings.SummaryDetailsExtractors);

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var duplicates =
                from cr in scanInfo.ConformanceResources()
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

            _resourcesPrepared = true;
            return;
        }

        private static List<ArtifactSummary> scanPaths(List<string> paths, NavigatorStreamFactory streamFactory, ArtifactSummaryFactory summaryFactory, ArtifactSummaryDetailsExtractor[] extractors)
        {
            // [WMR 20171023] Note: some files may no longer exist

            var cnt = paths.Count;
            var scanResult = new List<ArtifactSummary>(cnt);

            // var sw = new Stopwatch();
            // sw.Start();

            // Optimization: (partially) async I/O
            // Unfortunately, deserialization is still synchronous, so individual tasks are blocking
            // However, we do achieve some performance gain when scanning many files
            // (depending on default TaskScheduler)
            // With proper async deserialization support (Microsoft: TODO), tasks would yield while
            // performing I/O, further increasing multi-threading performance gains

            // [WMR 20171020] TODO:
            // - Expose configuration option to control sync/async behavior
            // - Support TimeOut
            // - Support CancellationToken (how to inject?)

#if PREPARE_PARALLEL_FOR
            // Optimization: use Task.Parallel.For to process files in parallel
            // More efficient then creating task per file (esp. if many files)
            //
            // For netstandard13, add NuGet package System.Threading.Tasks.Parallel
            //
            //   <ItemGroup Condition=" '$(TargetFramework)' != 'net45' ">
            //    <PackageReference Include="System.Threading.Tasks.Parallel" Version="4.3.0" />
            //   </ItemGroup>

            // Pre-allocate results array, one entry per file
            // Each entry receives a list with summaries harvested from a single file (Bundles return 0..*)
            var results = new List<ArtifactSummary>[cnt];
            Exception ex = null;
            try
            {
                // Process files in parallel
                var loopResult = Parallel.For(0, cnt,
                    // new ParallelOptions() {  },
                    i => {
                        // Harvest summaries from single file
                        // Save each result to a separate array entry (no locking required)
                        results[i] = ArtifactSummaryGenerator.Generate(paths[i], streamFactory, summaryFactory, extractors);
                    });
            }
            catch (AggregateException aex)
            {
                // ArtifactSummaryHarvester.HarvestAll catches and returns exceptions using ArtifactSummary.FromException
                // However Parallel.For may still throw, e.g. due to time out or cancel

                // var isCanceled = ex.InnerExceptions.OfType<TaskCanceledException>().Any();
                Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(scanPaths)}] {aex.GetType().Name}: {aex.Message}"
                    + $"{aex.InnerExceptions?.Select(ix => $"\r\n\t{ix.GetType().Name}: {ix.Message}")}");

                // [WMR 20171023] Return exceptions via ArtifactSummary.FromException
                ex = aex;
            }
            // Aggregate completed results into single list
            scanResult.AddRange(results.SelectMany(r => r ?? Enumerable.Empty<ArtifactSummary>()));
            if (ex != null)
            {
                scanResult.Add(ArtifactSummary.FromException(ex));
            }
#else
            foreach (var filePath in paths)
            {
                var summaries = ArtifactSummaryGenerator.Generate(filePath, streamFactory, summaryFactory, extractors);
                scanResult.AddRange(summaries);
            }
#endif
            // sw.Stop();
            // Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(scanPaths)}] Duration: {sw.ElapsedMilliseconds} ms | {cnt} paths | {scanResult.Count} resources");

            return scanResult;
        }

        /// <summary>
        /// Try to deserialize the full resource represented by the specified <see cref="ArtifactSummary"/>.
        /// </summary>
        /// <param name="info">An <see cref="ArtifactSummary"/> instance.</param>
        /// <typeparam name="T">The expected resource type.</typeparam>
        /// <returns>A new instance of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        private T getResourceFromScannedSource<T>(ArtifactSummary info)
            where T : Resource
        {
            // File path of the containing resource file (could be a Bundle)
            var path = info.Origin;

            var navStream = _settings.StreamFactory?.Invoke(path)
                ?? DefaultNavigatorStreamFactory.Create(path);

            // TODO: Handle exceptions & null return values
            // e.g. file may have been deleted/renamed since last scan

            // Advance stream to the target resource (e.g. specific Bundle entry)
            if (navStream.Seek(info.Position))
            {
                // Create navigator for the target resource
                var nav = navStream.Current;
                if (nav != null)
                {
                    // Parse target resource from navigator
                    var parser = new BaseFhirParser();
                    var result = parser.Parse<T>(nav);
                    if (result != null)
                    {
                        // Add origin annotation
                        result.SetOrigin(info.Origin);
                        return result;
                    }
                }
            }
            return null;
        }

        #endregion


    }

#endif

    }
