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
//using System.Reflection;

namespace Hl7.Fhir.Specification.Source
{
#if !PORTABLE45

    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from validation.zip/validation-min.zip
    /// </summary>
    public class ZipArtifactSource : IArtifactSource
    {
        public static ZipArtifactSource CreateValidationSource()
        {
            var path = Path.Combine(FileDirectoryArtifactSource.SpecificationDirectory, "validation.xml.zip");
            if(File.Exists(path)) return new ZipArtifactSource(path);

            path = Path.Combine(FileDirectoryArtifactSource.SpecificationDirectory, "validation-min.xml.zip");
            if (File.Exists(path)) return new ZipArtifactSource(path);

            throw new FileNotFoundException("Cannot create a ZipArtifactSource for validation.xml.zip: neither validation.xml.zip nor validation-min.xml.zip was found");
        }

        private readonly string CACHE_KEY = "FhirArtifactCache-" + typeof(ZipArtifactSource).Assembly.GetName().Version.ToString();
       
        private bool _prepared = false;

        private string _zipPath = null;

        public ZipArtifactSource(string zipPath)
        {
            _zipPath = zipPath;
        }


        private FileDirectoryArtifactSource _filesSource;

        /// <summary>
        /// Unpacks the zip-file and constructs a new FileArtifactSource on the unzipped directory
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        private void prepare()
        {
            if (_prepared) return;

            if (!File.Exists(_zipPath)) throw new FileNotFoundException(String.Format("Cannot prepare ZipArtifactSource: file '{0}' was not found", _zipPath ));
           
            var zc = new ZipCacher(_zipPath, CACHE_KEY);
            _filesSource = new FileDirectoryArtifactSource(zc.GetContentDirectory(), includeSubdirectories: false);

            _prepared = true;
        }

        public IEnumerable<string> ListArtifactNames()
        {
            prepare();
            return _filesSource.ListArtifactNames();
        }

        public Stream LoadArtifactByName(string name)
        {
            prepare();
            return _filesSource.LoadArtifactByName(name);
        }


        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            prepare();
            return _filesSource.ListConformanceResources();
        }


        /// <summary>
        /// Locates the file belonging to the given artifactId on a filesystem (within the store directory given in the constructor)
        /// and reads an artifact with the given id from it.
        /// </summary>
        /// <param name="url">identifying uri of the conformance resource to find</param>
        /// <returns>An artifact (Profile, ValueSet, etc) or null if an artifact with the given uri could not be located</returns>
        public Resource LoadConformanceResourceByUrl(string url)
        {
            prepare();
            return _filesSource.LoadConformanceResourceByUrl(url);
        }                
    }

#endif
}
