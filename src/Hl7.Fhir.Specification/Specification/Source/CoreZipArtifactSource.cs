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
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from validation.zip
    /// </summary>
    public class CoreZipArtifactSource : IArtifactSource
    {
        private readonly string CACHE_KEY = "FhirArtifactCache-" + typeof(CoreZipArtifactSource).Assembly.GetName().Version.ToString();

        private readonly string _contentDirectory;
        
        private bool _prepared = false;

        public CoreZipArtifactSource(string contentDirectory = null)
        {
            _contentDirectory = contentDirectory;

            if(_contentDirectory == null)
            {
                var modelDir = FileArtifactSource.SpecificationDirectory;

                // Add the current directory to the list of directories with artifact content, unless there's
                // a special subdirectory available
                if (Directory.Exists(modelDir))
                    _contentDirectory = modelDir;
                else
                    _contentDirectory = Directory.GetCurrentDirectory();
            }
        }


        private FileArtifactSource _resourceSource;
        private FileArtifactSource _filesSource;

        /// <summary>
        /// Unpacks zip-files containing the artifact files, and enumerates all (zipped/nonzipped) files.
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        private void prepare()
        {
            if (_prepared) return;

            var zipPath = Path.Combine(_contentDirectory, "validation.zip");
            if (!File.Exists(zipPath)) throw new FileNotFoundException("CoreZipArtifactSource cannot locate file validation.zip");
           
            var zc = new ZipCacher(zipPath, CACHE_KEY);

            _resourceSource = new FileArtifactSource(zc.GetContentDirectory(), includeSubdirectories: false);
            _resourceSource.Mask = "*.xml";

            _filesSource = new FileArtifactSource(zc.GetContentDirectory(), includeSubdirectories: false);

            _prepared = true;
        }

        public IEnumerable<string> ListArtifactNames()
        {
            prepare();
            return _filesSource.ListArtifactNames();
        }

        public Stream ReadContentArtifact(string name)
        {
            if (name == null) throw Error.ArgumentNull("name");
            prepare();

            return _filesSource.ReadContentArtifact(name);
        }


        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            prepare();
            return _resourceSource.ListConformanceResources();
        }


        /// <summary>
        /// Locates the file belonging to the given artifactId on a filesystem (within the store directory given in the constructor)
        /// and reads an artifact with the given id from it.
        /// </summary>
        /// <param name="identifier">identifying uri of the conformance resource to find</param>
        /// <returns>An artifact (Profile, ValueSet, etc) or null if an artifact with the given uri could not be located</returns>
        public Resource ReadConformanceResource(string identifier)
        {
            if (identifier == null) throw Error.ArgumentNull("identifier");
            prepare();

            return _resourceSource.ReadConformanceResource(identifier);
        }                
    }

#endif
}
