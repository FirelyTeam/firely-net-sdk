/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
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
using Newtonsoft.Json;

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

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// Multiple masks can be split by '|'
        /// </summary>
        public string Mask
        {
            get => String.Join("|", Masks);
            set
            {
                Masks = value?.Split('|').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToArray();
            }
        }

        public string[] Masks
        {
            get { return _masks; }
            set { _masks = value; Refresh(); }
        }

        public string[] Includes
        {
            get { return _includes; }
            set { _includes = value; Refresh(); }
        }

        public string[] Excludes
        {
            get { return _excludes; }
            set { _excludes = value; Refresh(); }
        }


        public DirectorySource(string contentDirectory, bool includeSubdirectories = false)
        {
            _contentDirectory = contentDirectory ?? throw Error.ArgumentNull(nameof(contentDirectory));
            _includeSubs = includeSubdirectories;
        }


        public DirectorySource(bool includeSubdirectories = false) : this(SpecificationDirectory, includeSubdirectories)
        {
        }


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

        /// <summary>Returns the content directory as specified to the constructor.</summary>
        public string ContentDirectory => _contentDirectory;

        private bool _filesPrepared = false;
        private List<string> _artifactFilePaths;

        /// <summary>
        /// Prepares the source by reading all files present in the directory (matching the mask, if given)
        /// </summary>
        private void prepareFiles()
        {
            if (_filesPrepared) return;

            var masks = _masks ?? (new[] { "*.*" });

            // Add files present in the content directory
            var allFiles = new List<string>();

            foreach (var mask in masks)
                allFiles.AddRange(Directory.GetFiles(_contentDirectory, mask, _includeSubs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));

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

            string fullPathWithoutExtension(string fullPath) => fullPath.Replace(Path.GetFileName(fullPath), Path.GetFileNameWithoutExtension(fullPath));
            bool isXml(string fullPath) => Path.GetExtension(fullPath).ToLower() == ".xml";
            bool isJson(string fullPath) => Path.GetExtension(fullPath).ToLower() == ".json";
        }

        bool _resourcesPrepared = false;
        private List<ConformanceScanInformation> _resourceScanInformation;

        public enum DuplicateFilenameResolution
        {
            PreferXml,
            PreferJson,
            KeepBoth
        }


        public DuplicateFilenameResolution FormatPreference
        {
            get;
            set;
        } = DuplicateFilenameResolution.PreferXml;


        // [WMR 20170217] Ignore invalid xml files, aggregate parsing errors
        // https://github.com/ewoutkramer/fhir-net-api/issues/301
        public struct ErrorInfo
        {
            public ErrorInfo(string fileName, Exception error) { FileName = fileName; Error = error; }
            public string FileName { get; }
            public Exception Error { get; }
        }

        /// <summary>Returns an array of runtime errors that occured while parsing the resources.</summary>
        public ErrorInfo[] Errors = new ErrorInfo[0];

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
                          where ci.Canonical != null
                          group ci by ci.Canonical into g
                          where g.Count() > 1
                          select g;

            if (doubles.Any())
                throw new CanonicalUrlConflictException(doubles.Select(d => new CanonicalUrlConflictException.CanonicalUrlConflict(d.Key, d.Select(ci => ci.Origin))));

            _resourcesPrepared = true;
            return;

            (List<ConformanceScanInformation>, ErrorInfo[]) scanPaths(List<string> paths)
            {
                var scanResult = new List<ConformanceScanInformation>();
                var errors = new List<ErrorInfo>();

                foreach (var file in paths)
                {
                    try
                    {
                        var scanner = createScanner(file);
                        if (scanner != null)
                            scanResult.AddRange(scanner.List());
                    }
                    catch (XmlException ex)
                    {
                        errors.Add(new ErrorInfo(file, ex));    // Log the exception
                    }
                    catch(JsonException ej)
                    {
                        errors.Add(new ErrorInfo(file, ej));    // Log the exception
                    }
                    // Don't catch other exceptions (fatal error)
                }

                return (scanResult, errors.ToArray());

                IConformanceScanner createScanner(string path)
                {
                    var ext = Path.GetExtension(path).ToLower();
                    return ext == ".xml" ? new XmlFileConformanceScanner(path) :
                                  ext == ".json" ? new JsonFileConformanceScanner(path) : (IConformanceScanner)null;
                }
            }
        }


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
            IEnumerable<ConformanceScanInformation> scan = _resourceScanInformation;

            if (filter != null)
                scan = scan.Where(dsi => dsi.ResourceType == filter);

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

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.Canonical == uri);
            if (info == null) return null;

            return getResourceFromScannedSource(info);
        }

        private static Resource getResourceFromScannedSource(ConformanceScanInformation info)
        {
            var path = info.Origin;

            var scanner = new XmlFileConformanceScanner(path);
            return scanner.Retrieve(info);
        }

        public ValueSet FindValueSetBySystem(string system)
        {
            prepareResources();

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.ValueSetSystem == system);
            if (info == null) return null;

            return getResourceFromScannedSource(info) as ValueSet;
        }

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
                throw Error.ArgumentNull(nameof(targetUri), "sourceUri and targetUri cannot both be null");

            prepareResources();

            IEnumerable<ConformanceScanInformation> infoList = _resourceScanInformation;

            if (sourceUri != null)
                infoList = infoList.Where(ci => ci.ConceptMapSource == sourceUri);

            if (targetUri != null)
                infoList = infoList.Where(ci => ci.ConceptMapTarget == targetUri);

            return infoList.Select(info => getResourceFromScannedSource(info)).Where(r => r != null).Cast<ConceptMap>();
        }

        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            prepareResources();

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.UniqueIds.Contains(uniqueId));
            if (info == null) return null;

            return getResourceFromScannedSource(info) as NamingSystem;
        }
    }
#endif
}
