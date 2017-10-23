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

// PrepareResources strategy configuration
#define PREPARE_PARALLEL_FOR
//#define PREPARE_PARALLEL
//#define PREPARE_ASYNC

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.IO;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
#if NET_FILESYSTEM
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from directories with individual files
    /// </summary>
    public class DirectorySource : IConformanceSource, IArtifactSource
    {
        private readonly NavigatorStreamFactory _streamFactory;
        private readonly ArtifactSummaryHarvester _harvester;
        private readonly string _contentDirectory;
        private readonly bool _includeSubs;

        private string[] _masks;
        private string[] _includes;
        private string[] _excludes;

        private bool _filesPrepared = false;
        private List<string> _artifactFilePaths;

        private bool _resourcesPrepared = false;
        private List<ArtifactSummary> _resourceScanInformation;

        // netstandard has no CurrentCultureIgnoreCase comparer
#if DOTNETFW
        static readonly StringComparer ExtensionComparer = StringComparer.CurrentCultureIgnoreCase;
        static readonly StringComparison ExtensionComparison = StringComparison.CurrentCultureIgnoreCase;
#else
        static readonly StringComparer ExtensionComparer = StringComparer.OrdinalIgnoreCase;
        static readonly StringComparison ExtensionComparison = StringComparison.OrdinalIgnoreCase;
#endif

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources from the specified content directory
        /// using the specified <see cref="ArtifactSummary"/> harvester delegate and <see cref="INavigatorStream"/> factory delegate.
        /// </summary>
        /// <param name="streamFactory">A function that creates an <see cref="INavigatorStream"/> instance for the resource with the specified file path.</param>
        /// <param name="harvester">An <see cref="ArtifactSummaryHarvester"/> delegate to extract summary information from a resource.</param>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="includeSubdirectories">Specify <c>true</c> to include resources from subdirectories (recursively), or <c>false</c> otherwise.</param>
        public DirectorySource(NavigatorStreamFactory streamFactory, ArtifactSummaryHarvester harvester, string contentDirectory, bool includeSubdirectories = false)
        {
            _harvester = harvester ?? throw Error.ArgumentNull(nameof(harvester));
            _streamFactory = streamFactory ?? throw Error.ArgumentNull(nameof(streamFactory));
            _contentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            _includeSubs = includeSubdirectories;
        }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources from the specified content directory.
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="includeSubdirectories">Specify <c>true</c> to include resources from subdirectories (recursively), or <c>false</c> otherwise.</param>
        public DirectorySource(string contentDirectory, bool includeSubdirectories = false)
            : this(DefaultNavigatorStreamFactory.Create, DefaultArtifactSummaryHarvester.Harvest, contentDirectory, includeSubdirectories)
        {
        }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources from the <see cref="SpecificationDirectory"/>.
        /// </summary>
        /// <param name="includeSubdirectories">Specify <c>true</c> to include resources from subdirectories (recursively), or <c>false</c> otherwise.</param>
        public DirectorySource(bool includeSubdirectories = false)
            : this(DefaultNavigatorStreamFactory.Create, DefaultArtifactSummaryHarvester.Harvest, SpecificationDirectory, includeSubdirectories)
        {
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
            get => String.Join("|", Masks);
            set
            {
                Masks = value?.Split('|').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToArray();
            }
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
            get { return _masks; }
            set { _masks = value; Refresh(); }
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
            get { return _includes; }
            set { _includes = value; Refresh(); }
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
            get { return _excludes; }
            set { _excludes = value; Refresh(); }
        }

        /// <summary>Returns the content directory as specified to the constructor.</summary>
        public string ContentDirectory => _contentDirectory;

        /// <summary>
        /// The default directory this artifact source will access for its files.
        /// </summary>
        public static string SpecificationDirectory
        {
            get
            {
#if DOTNETFW
                var codebase = AppDomain.CurrentDomain.BaseDirectory;
#else
                var codebase = AppContext.BaseDirectory;
#endif
                if (Directory.Exists(codebase))
                    return codebase;
                else
                    return Directory.GetCurrentDirectory();
            }
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
            get;
            set;
        } = DuplicateFilenameResolution.PreferXml;

/*
        // [WMR 20171020] OBSOLETE; Use ArtifactSummary.Error

        // [WMR 20170217] Ignore invalid xml files, aggregate parsing errors
        // https://github.com/ewoutkramer/fhir-net-api/issues/301

        /// <summary>
        /// Returns information about a runtime error that occured while parsing a specific resource.
        /// </summary>
        public struct ErrorInfo
        {
            public ErrorInfo(string fileName, Exception error) { FileName = fileName; Error = error; }
            public string FileName { get; }
            public Exception Error { get; }
        }

        /// <summary>Returns an array of runtime errors that occured while parsing the resources.</summary>
        public ErrorInfo[] Errors = new ErrorInfo[0];
*/

        /// <summary>
        /// Request a full re-scan of the specified content directory.
        /// </summary>
        public void Refresh()
        {
            _filesPrepared = false;
            _resourcesPrepared = false;
        }

        /// <summary>
        /// Returns a list of summary information describing all valid
        /// FHIR artifacts that exist in the specified content directory.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArtifactSummary> List()
        {
            prepareResources();
            return _resourceScanInformation.AsEnumerable();
        }

        /// <summary>Returns a list of error information for FHIR artifacts where summary information could not be retrieved.</summary>
        /// <returns></returns>
        public IEnumerable<ArtifactSummary> Errors()
        {
            prepareResources();
            return _resourceScanInformation.Errors();
        }

        /// <summary>
        /// Returns a list of summary information describing all valid
        /// FHIR artifacts that exist in the specified content directory.
        /// </summary>
        /// <returns></returns>
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

        public IEnumerable<string> ListArtifactNames()
        {
            prepareFiles();

            return _artifactFilePaths.Select(path => Path.GetFileName(path));
        }

        public Stream LoadArtifactByName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));

            prepareFiles();

            // var searchString = (Path.DirectorySeparatorChar + name).ToLower();
            var searchString = Path.DirectorySeparatorChar + name;

            // NB: uses _artifactFiles (full paths), not ArtifactFiles (which only has public list of names, not full path)
            // var fullFileName = _artifactFilePaths.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));
            var fullFileName = _artifactFilePaths.SingleOrDefault(fn => fn.EndsWith(searchString, ExtensionComparison));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }


        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            var scan = List(filter);
            return scan.Select(dsi => dsi.ResourceUri);
        }


        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.ResourceUri == uri);
            if (info == null) return null;

            return getResourceFromScannedSource<Resource>(info);

        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation
                .OfType<ConformanceResourceSummary>()
                .SingleOrDefault(ci => ci.Canonical == uri);

            if (info == null) return null;

            return getResourceFromScannedSource<Resource>(info);
        }

        public ValueSet FindValueSetBySystem(string system)
        {
            prepareResources();

            var info = _resourceScanInformation
                .OfType<ValueSetSummary>()
                .SingleOrDefault(ci => ci.ValueSetSystem == system);

            if (info == null) return null;

            return getResourceFromScannedSource<ValueSet>(info);
        }

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
            {
                throw Error.ArgumentNull(nameof(targetUri), "sourceUri and targetUri cannot both be null");
            }

            prepareResources();

            IEnumerable<ConceptMapSummary> infoList = _resourceScanInformation.OfType<ConceptMapSummary>();
            if (sourceUri != null)
            {
                infoList = infoList.Where(ci => ci.ConceptMapSource == sourceUri);
            }

            if (targetUri != null)
            {
                infoList = infoList.Where(ci => ci.ConceptMapTarget == targetUri);
            }

            return infoList.Select(info => getResourceFromScannedSource<ConceptMap>(info)).Where(r => r != null);
        }

        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            prepareResources();

            var info = _resourceScanInformation
                .OfType<NamingSystemSummary>()
                .SingleOrDefault(ci => ci.UniqueIds.Contains(uniqueId));

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

            var masks = _masks ?? (new[] { "*.*" });

            // Add files present in the content directory
            var allFiles = new List<string>();

            // [WMR 20170817] NEW
            // Safely enumerate files in specified path and subfolders, recursively
            allFiles.AddRange(safeGetFiles(_contentDirectory, masks, _includeSubs));

            // Always remove *.exe" and "*.dll"
            allFiles.RemoveAll(name => Path.GetExtension(name) == ".exe" || Path.GetExtension(name) == ".dll");

            // var unsafeExtensions = new[] { ".exe", ".dll" };
            // allFiles.RemoveAll(name => unsafeExtensions.Contains(Path.GetExtension(name), ExtensionComparer));

            if (_includes?.Length > 0)
            {
                var includeFilter = new FilePatternFilter(_includes);
                allFiles = includeFilter.Filter(_contentDirectory, allFiles).ToList();
            }

            if (_excludes?.Length > 0)
            {
                var excludeFilter = new FilePatternFilter(_excludes, negate: true);
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

        private static string fullPathWithoutExtension(string fullPath)
            => fullPath.Replace(Path.GetFileName(fullPath), Path.GetFileNameWithoutExtension(fullPath));


        /// <summary>Scan all xml files found by prepareFiles and find conformance resources and their id.</summary>
        private void prepareResources()
        {
            if (_resourcesPrepared) return;
            prepareFiles();

            var uniqueArtifacts = ResolveDuplicateFilenames(_artifactFilePaths, FormatPreference);
            var scanInfo = _resourceScanInformation = scanPaths(uniqueArtifacts, _streamFactory, _harvester);

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var doubles = from ci in scanInfo.OfType<ConformanceResourceSummary>()
                          where ci.Canonical != null
                          group ci by ci.Canonical into g
                          where g.Count() > 1
                          select g;

            if (doubles.Any())
            {
                // [WMR 20171023] TODO: Allow configuration, e.g. optional callback delegate
                throw new CanonicalUrlConflictException(doubles.Select(d => new CanonicalUrlConflictException.CanonicalUrlConflict(d.Key, d.Select(ci => ci.Origin))));
            }

            _resourcesPrepared = true;
            return;
        }

        private static List<ArtifactSummary> scanPaths(List<string> paths, NavigatorStreamFactory factory, ArtifactSummaryHarvester harvester)
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
            // Optimization: use Task.Parallel.ForEach to process files in parallel
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
                        results[i] = harvester.HarvestAll(factory, paths[i]);
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
                scanResult.Add(ArtifactSummary.FromException(null, ex));
            }

#elif PREPARE_PARALLEL
            // Using PLINQ
            // Less efficient than Task.Parallel
            //
            // For netstandard13, add NuGet package System.Linq.Parallel
            //
            //   <ItemGroup Condition=" '$(TargetFramework)' != 'net45' ">
            //    <PackageReference Include="System.Linq.Parallel" Version="4.3.0" />
            //   </ItemGroup>

            // Pre-allocate results array, one entry per file
            List<ArtifactSummary>[] results = { };
            try
            {
                results = paths.AsParallel().Select(path => harvester.HarvestAll(factory, path)).ToArray();
            }
            catch (AggregateException ex)
            {
                // Failed... timeout or canceled?
                // var isCanceled = ex.InnerExceptions.OfType<TaskCanceledException>().Any();
                Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(scanPaths)}] {ex.GetType().Name}: {ex.Message}");
            }
            scanResult.AddRange(results.SelectMany(r => r ?? Enumerable.Empty<ArtifactSummary>()));

#elif PREPARE_ASYNC
            // Spin a separate async task per file

            var tasks = new List<Task<List<ArtifactSummary>>>(paths.Count);
            foreach (var filePath in paths)
            {
                var task = Task.Run(
                    () => { return harvester.HarvestAll(factory, filePath); }
                );
                tasks.Add(task);
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                // Failed... timeout or canceled?
                // var isCanceled = ex.InnerExceptions.OfType<TaskCanceledException>().Any();
                Debug.WriteLine($"[{nameof(DirectorySource)}.{nameof(scanPaths)}] {ex.GetType().Name}: {ex.Message}");
            }

            // Skip canceled and faulted tasks
            // TODO: Tasks shouldn't fault but return ArtifactSummary with Error info...
            var results = tasks.Where(t => t.IsCompleted /* && !t.IsFaulted */).SelectMany(t => t.Result);
            scanResult.AddRange(results);
#else
            foreach (var filePath in paths)
            {
                var summaries = harvester.HarvestAll(factory, filePath);
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
            var navStream = _streamFactory(path);
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
