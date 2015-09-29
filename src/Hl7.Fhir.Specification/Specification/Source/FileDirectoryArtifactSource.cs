/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using Newtonsoft.Json;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Specification.Source
{
#if !PORTABLE45
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from individual files
    /// </summary>
    public class FileDirectoryArtifactSource : IArtifactSource
    {
        private readonly string _contentDirectory;
        private readonly bool _includeSubs;

        private string _mask;

        public string Mask
        { 
            get { return _mask; }
            set { _mask = value; _filesPrepared = false; _resourcesPrepared = false; }
        }

        public FileDirectoryArtifactSource(string contentDirectory, bool includeSubdirectories = false)
        {
            if (contentDirectory == null) throw Error.ArgumentNull("contentDirectory");

            _contentDirectory = contentDirectory;
            _includeSubs = includeSubdirectories;
        }


        public FileDirectoryArtifactSource(bool includeSubdirectories = false) : this(SpecificationDirectory,includeSubdirectories)
        {
        }


        /// <summary>
        /// The default directory this artifact source will access for its files.
        /// </summary>
        public static string SpecificationDirectory
        {
            get
            {
                var codebase = AppDomain.CurrentDomain.BaseDirectory;

                if (Directory.Exists(codebase))
                    return codebase;
                else
                    return Directory.GetCurrentDirectory();
            }
        }



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
        private List<ConformanceInformation> _resourceInformation;


        /// <summary>
        /// Scan all xml files found by prepareFiles and find conformance resources + their id
        /// </summary>
        private void prepareResources()
        {
            if (_resourcesPrepared) return;

            prepareFiles();

            _resourceInformation = new List<ConformanceInformation>();

            foreach (var file in _artifactFilePaths.Where(af => Path.GetExtension(af) == ".xml"))
            {
                if (file.EndsWith("dataelements.xml")) continue;       // buggy file

                try
                {
                    var conformanceResources = readInformationFromFile(file).Where(ci =>
                           ResourceIdentity.IsRestResourceIdentity(ci.Url) &&
                           ModelInfo.IsConformanceResource((new ResourceIdentity(ci.Url)).ResourceType));
                    _resourceInformation.AddRange(conformanceResources);
                }
                catch(XmlException)
                {
                    throw;
                }
            }

            // Check for duplicate canonical urls, this is forbidden within a single source (and actually, universally,
            // but if another source has the same url, the order of polling in the MultiArtifactSource matters)
            var doubles = _resourceInformation.Where(ci=>ci.Url != null).GroupBy(ci => ci.Url).Where(group => group.Count() > 1);
            if (doubles.Any())
            {
                throw Error.InvalidOperation("The source has found multiple Conformance Resource artifacts with the same canonical url: {0} appears at {1}"
                        .FormatWith(doubles.First().Key, String.Join(", ", doubles.First().Select(hit => hit.Origin))));                        
            }

            _resourcesPrepared = true;
        }


        private IEnumerable<ConformanceInformation> readInformationFromFile(string path)
        {
            using (var bundleStream = File.OpenRead(path))
            {
                if (bundleStream != null)
                {
                    var scanner = new ConformanceArtifactScanner(bundleStream, path);
                    return scanner.ListConformanceResourceInformation().ToList();
                }
                else
                    return Enumerable.Empty<ConformanceInformation>();
            }
        }


        public IEnumerable<string> ListArtifactNames()
        {
            prepareFiles();

            return _artifactFilePaths.Select(path => Path.GetFileName(path));
        }

        public Stream LoadArtifactByName(string name)
        {
            if (name == null) throw Error.ArgumentNull("name");

            prepareFiles();

            var searchString = (Path.DirectorySeparatorChar + name).ToLower();

            // NB: uses _artifactFiles (full paths), not ArtifactFiles (which only has public list of names, not full path)
            var fullFileName = _artifactFilePaths.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }

        /// <summary>
        /// Locates the file belonging to the given artifactId on a filesystem (within the store directory given in the constructor)
        /// and reads an artifact with the given id from it.
        /// </summary>
        /// <param name="url">identifying uri of the conformance resource to find</param>
        /// <returns>An artifact (Profile, ValueSet, etc) or null if an artifact with the given uri could not be located. If both an
        /// xml and a json version is available, the xml version is returned</returns>
        public Resource LoadConformanceResourceByUrl(string url)
        {
            if (url == null) throw Error.ArgumentNull("identifier");
            prepareResources();

            var info = _resourceInformation.SingleOrDefault(ci => ci.Url == url);
            
            if(info == null) return null;

            var path = info.Origin;
            string artifactXml;

            // Note: no exception handling. If the expected bundled file cannot be
            // read, throw the original exception.
            using (var content = File.OpenRead(path))
            {
                if (content == null) throw new FileNotFoundException("Cannot find file " + path);

                var scanner = new ConformanceArtifactScanner(content, path);
                var entry = scanner.FindConformanceResourceByUrl(url);

                artifactXml = entry != null ? entry.ToString() : null;
            }

            if (artifactXml != null)
                return FhirParser.ParseResourceFromXml(artifactXml);
            else
                return null;
        }


        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            prepareResources();
            return _resourceInformation;
        }

    }

#endif
}
