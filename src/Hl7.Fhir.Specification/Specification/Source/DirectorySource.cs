/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.IO;
using System.Xml;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
#if NET_FILESYSTEM
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from directories with individual files
    /// </summary>
    public class DirectorySource : IConformanceSource, IArtifactSource
    {
        private readonly string _contentDirectory;
        private readonly bool _includeSubs;

        private string[] _masks;
        private string[] _includes;
        private string[] _excludes;

        private bool _filesPrepared = false;
        private List<string> _artifactFilePaths;

        private bool _resourcesPrepared = false;
        private List<ArtifactSummary> _resourceScanInformation;

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources from the specified content directory.
        /// </summary>
        /// <param name="contentDirectory">The file path of the target directory.</param>
        /// <param name="includeSubdirectories">Specify <c>true</c> to include resources from subdirectories (recursively), or <c>false</c> otherwise.</param>
        public DirectorySource(string contentDirectory, bool includeSubdirectories = false)
        {
            _contentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            _includeSubs = includeSubdirectories;
        }

        /// <summary>
        /// Create a new <see cref="DirectorySource"/> instance to browse and resolve resources from the <see cref="SpecificationDirectory"/>.
        /// </summary>
        /// <param name="includeSubdirectories">Specify <c>true</c> to include resources from subdirectories (recursively), or <c>false</c> otherwise.</param>
        public DirectorySource(bool includeSubdirectories = false) : this(SpecificationDirectory, includeSubdirectories)
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
        /// Gets or sets an array of search strings to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified include mask(s).
        /// The source will ignore all files that don't match the specified include mask(s).
        /// </summary>
        /// <remarks>
        /// Include filters are applied after <see cref="Mask"/> filters and before <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="bullet">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
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
        /// <code>Includes = new string[] { "**/file*.xml", "temp/*/*.json" };</code>
        /// </example>
        public string[] Includes
        {
            get { return _includes; }
            set { _includes = value; Refresh(); }
        }

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of files in the content directory.
        /// The source will ignore all files that match the specified exclude mask(s).
        /// The source will only provide resources from files that don't match the specified exclude mask(s).
        /// </summary>
        /// <remarks>
        /// Exclude filters are applied last, after any <see cref="Mask"/> and <see cref="Includes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="bullet">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
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
        /// <code>Excludes = new string[] { "**/file*.xml", "temp/*/*.json" };</code>
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

        /// <summary>
        /// Request a full re-scan of the specified content directory.
        /// </summary>
        public void Refresh()
        {
            _filesPrepared = false;
            _resourcesPrepared = false;
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

            var searchString = (Path.DirectorySeparatorChar + name).ToLower();

            // NB: uses _artifactFiles (full paths), not ArtifactFiles (which only has public list of names, not full path)
            var fullFileName = _artifactFilePaths.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }


        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            prepareResources();

            IEnumerable<ArtifactSummary> scan = _resourceScanInformation;
            if (filter != null)
            {
                scan = scan.Where(dsi => dsi.ResourceType == filter);
            }

            return scan.Select(dsi => dsi.ResourceUri);
        }


        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.ResourceUri == uri);
            if (info == null) return null;

            return getResourceFromScannedSource(info);

        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            prepareResources();

            var info = _resourceScanInformation
                .OfType<ConformanceResourceSummary>()
                .SingleOrDefault(ci => ci.Canonical == uri);

            if (info == null) return null;

            return getResourceFromScannedSource(info);
        }

        public ValueSet FindValueSetBySystem(string system)
        {
            prepareResources();

            var info = _resourceScanInformation
                .OfType<ValueSetSummary>()
                .SingleOrDefault(ci => ci.ValueSetSystem == system);

            if (info == null) return null;

            return getResourceFromScannedSource(info) as ValueSet;
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

            return infoList.Select(info => getResourceFromScannedSource(info)).Where(r => r != null).Cast<ConceptMap>();
        }

        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            prepareResources();

            var info = _resourceScanInformation
                .OfType<NamingSystemSummary>()
                .SingleOrDefault(ci => ci.UniqueIds.Contains(uniqueId));

            if (info == null) return null;

            return getResourceFromScannedSource(info) as NamingSystem;
        }

        #region Protected members

        // [WMR 20171016] Allow subclasses to override this method
        // and return custom INavigatorStream implementations
        protected virtual INavigatorStream createNavigatorStream(string path)
        {
            if (isXml(path))
            {
                return new XmlNavigatorStream(path);
            }
            if (isJson(path))
            {
                return new JsonNavigatorStream(path);
            }

            // Unsupported extension
            return null;
        }

        #endregion

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
                        Debug.WriteLine($"Error enumerating files in '{currentFolder}': {ex.Message}");
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
                if (isXml(filename) || isJson(filename))
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
                    if (preference == DuplicateFilenameResolution.PreferXml && isXml(first))
                        result.Add(first);
                    else if (preference == DuplicateFilenameResolution.PreferJson && isJson(first))
                        result.Add(first);
                    else
                        result.Add(group.Skip(1).First());
                }
            }

            return result;

        }

        private static string fullPathWithoutExtension(string fullPath)
            => fullPath.Replace(Path.GetFileName(fullPath), Path.GetFileNameWithoutExtension(fullPath));


        // [WMR 20171017] Maybe we should publicly expose the following utility methods?

        private static bool isXml(string fullPath)
            // => Path.GetExtension(fullPath).ToLower() == ".xml";
            => StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(fullPath), ".xml");

        private static bool isJson(string fullPath)
            // => Path.GetExtension(fullPath).ToLower() == ".json";
            => StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(fullPath), ".json");

        /// <summary>Scan all xml files found by prepareFiles and find conformance resources and their id.</summary>
        private void prepareResources()
        {
            if (_resourcesPrepared) return;
            prepareFiles();

            var uniqueArtifacts = ResolveDuplicateFilenames(_artifactFilePaths, FormatPreference);
            (_resourceScanInformation, Errors) = scanPaths(uniqueArtifacts);

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var doubles = from ci in _resourceScanInformation
                          .OfType<ConformanceResourceSummary>()
                          where ci.Canonical != null
                          group ci by ci.Canonical into g
                          where g.Count() > 1
                          select g;

            if (doubles.Any())
                throw new CanonicalUrlConflictException(doubles.Select(d => new CanonicalUrlConflictException.CanonicalUrlConflict(d.Key, d.Select(ci => ci.Origin))));

            _resourcesPrepared = true;
            return;

            (List<ArtifactSummary>, ErrorInfo[]) scanPaths(List<string> paths)
            {
                var scanResult = new List<ArtifactSummary>();
                var errors = new List<ErrorInfo>();

                foreach (var filePath in paths)
                {
                    try
                    {
                        // 1. DirectorySource creates INavigatorStream
                        // 2. DirectorySource initializes ArtifactSummaryHarvester with streamer
                        // 3. DirectorySource calls Harvester to generate summaries

                        var navStream = createNavigatorStream(filePath);
                        // createNavigatorStream returns null for unknown file extensions
                        if (navStream != null)
                        {
                            var harvester = ArtifactSummaryHarvester.Default;
                            var summaryStream = harvester.Enumerate(navStream);
                            scanResult.AddRange(summaryStream);
                        }
                    }
                    catch (XmlException ex)
                    {
                        errors.Add(new ErrorInfo(filePath, ex));    // Log the exception
                    }
                    catch (JsonException ej)
                    {
                        errors.Add(new ErrorInfo(filePath, ej));    // Log the exception
                    }
                    // Don't catch other exceptions (fatal error)
                }

                return (scanResult, errors.ToArray());

            }
        }

        private static Resource getResourceFromScannedSource(ArtifactSummary info)
        {
            // [WMR 20171016] TODO: rewrite obsolete logic

            // var path = info.Origin;
            // var scanner = createScanner(path);
            // return scanner.Retrieve(info);

            throw new NotImplementedException("TODO: Call new overload on deserializers that accept an IElementNavigator.");
        }

        #endregion


    }

#endif

}
