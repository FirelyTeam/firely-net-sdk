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
using Hl7.Fhir.Serialization;
using System.Xml;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

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

        private string _mask;

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// </summary>
        public string Mask
        {
            get { return _mask; }
            set { _mask = value; _filesPrepared = false; _resourcesPrepared = false; }
        }

        public DirectorySource(string contentDirectory, bool includeSubdirectories = false)
        {
            if (contentDirectory == null) throw Error.ArgumentNull(nameof(contentDirectory));

            _contentDirectory = contentDirectory;
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

            IEnumerable<string> masks;
            if (Mask == null)
                masks = new string[] { "*.*" };
            else
                masks = Mask.Split('|').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s));

            // Add files present in the content directory
            var allFiles = new List<string>();

            foreach (var mask in masks)
            {
                allFiles.AddRange(Directory.GetFiles(_contentDirectory, mask, _includeSubs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            }

            allFiles.RemoveAll(name => Path.GetExtension(name) == ".exe" || Path.GetExtension(name) == ".dll");

            _artifactFilePaths = new List<string>(allFiles);
            _filesPrepared = true;
        }


        bool _resourcesPrepared = false;
        private List<ResourceStreamScanner.ResourceScanInformation> _resourceScanInformation;

        // [WMR 20170217] Ignore invalid xml files, aggregate parsing errors
        // https://github.com/ewoutkramer/fhir-net-api/issues/301
        public struct ErrorInfo
        {
            public ErrorInfo(string fileName, XmlException error) { FileName = fileName; Error = error; }
            public string FileName { get; }
            public XmlException Error { get; }
        }

        ErrorInfo[] _errors = new ErrorInfo[0];

        /// <summary>Returns an array of runtime errors that occured while parsing the resources.</summary>
        public ErrorInfo[] Errors => _errors;

        /// <summary>Scan all xml files found by prepareFiles and find conformance resources and their id.</summary>
        private void prepareResources()
        {
            if (_resourcesPrepared) return;

            prepareFiles();

            _resourceScanInformation = new List<ResourceStreamScanner.ResourceScanInformation>();

            var errors = new List<ErrorInfo>();
            foreach (var file in _artifactFilePaths.Where(af => Path.GetExtension(af) == ".xml"))
            {
                try
                {
                    var scannedInformation = readInformationFromFile(file);
                    _resourceScanInformation.AddRange(scannedInformation);
                }
                catch (XmlException e)
                {
                    // throw;      // Just ignore crappy xml
                    errors.Add(new ErrorInfo(file, e));    // Log the exception
                }
                // Don't catch other exceptions (fatal error)
            }
            _errors = errors.ToArray();

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var doubles = _resourceScanInformation.Where(ci => ci.Canonical != null).GroupBy(ci => ci.Canonical).Where(group => group.Count() > 1);
            if (doubles.Any())
            {
                //throw Error.InvalidOperation("The source has found multiple Conformance Resource artifacts with the same canonical url: {0} appears at {1}"
                //        .FormatWith(doubles.First().Key, String.Join(", ", doubles.First().Select(hit => hit.Origin))));
                throw new CanonicalUrlConflictException(doubles.Select(d => new CanonicalUrlConflictException.CanonicalUrlConflict(d.Key, d.Select(ci => ci.Origin))));
            }

            // var info = doubles.Select(g => new Tuple<string, IEnumerable<string>>(g.Key, g.Select(ci => ci.Origin)));
            // var d = _resourceInformation.Select(ci => new Tuple<string, string>(ci.Url, ci.Origin)).GroupBy(t => t.Item1).Where(g => g.Count() > 1);

            _resourcesPrepared = true;
        }


        private IEnumerable<ResourceStreamScanner.ResourceScanInformation> readInformationFromFile(string path)
        {
            using (var bundleStream = File.OpenRead(path))
            {
                if (bundleStream != null)
                {
                    var scanner = new ResourceStreamScanner(bundleStream, path);
                    return scanner.List().ToList();
                }
                else
                    return Enumerable.Empty<ResourceStreamScanner.ResourceScanInformation>();
            }
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
            IEnumerable<ResourceStreamScanner.ResourceScanInformation> scan = _resourceScanInformation;

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

        private static Resource getResourceFromScannedSource(ResourceStreamScanner.ResourceScanInformation info)
        {
            var path = info.Origin;
            string artifactXml;

            // Note: no exception handling. If the expected bundled file cannot be
            // read, throw the original exception.
            using (var content = File.OpenRead(path))
            {
                if (content == null) throw new FileNotFoundException("Cannot find file " + path);

                var scanner = new ResourceStreamScanner(content, path);
                var entry = scanner.FindResourceByUri(info.ResourceUri);

                artifactXml = entry != null ? entry.ToString() : null;
            }

            if (artifactXml != null)
            {
                var resultResource = new FhirXmlParser().Parse<Resource>(artifactXml);
                resultResource.AddAnnotation(new OriginInformation { Origin = info.Origin });
                return resultResource;
            }
            else
                return null;
        }

        public CodeSystem FindCodeSystemByValueSet(string system)
        {
            prepareResources();

            var info = _resourceScanInformation.SingleOrDefault(ci => ci.CodeSystemValueSet == system);
            if (info == null) return null;

            return getResourceFromScannedSource(info) as CodeSystem;
        }

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri=null, string targetUri=null)
        {
            if (sourceUri == null && targetUri == null)
                throw Error.ArgumentNull(nameof(targetUri), "sourceUri and targetUri cannot both be null");

            prepareResources();

            IEnumerable<ResourceStreamScanner.ResourceScanInformation> infoList = _resourceScanInformation;

            if (sourceUri != null)
                infoList = infoList.Where(ci => ci.ConceptMapSource.Contains(sourceUri));
            
            if(targetUri != null)
                infoList = infoList.Where(ci =>ci.ConceptMapTarget.Contains(targetUri));

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
