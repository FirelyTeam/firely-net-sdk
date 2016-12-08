/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.IO;

namespace Hl7.Fhir.Specification.Source
{
#if !PORTABLE45

    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from validation.zip/validation-min.zip
    /// </summary>
    public class ZipSource : IConformanceSource, IArtifactSource
    {
        public static ZipSource CreateValidationSource()
        {
            var path = Path.Combine(DirectorySource.SpecificationDirectory, "specification.zip");
            if(File.Exists(path)) return new ZipSource(path);

            //path = Path.Combine(DirectorySource.SpecificationDirectory, "validation-min.xml.zip");
            //if (File.Exists(path)) return new ZipSource(path);

            throw new FileNotFoundException("Cannot create a ZipArtifactSource for the core specification: specification.zip was not found");
        }

        private readonly string CACHE_KEY = "FhirArtifactCache-STU3-" + typeof(ZipSource).Assembly.GetName().Version.ToString();
       
        private bool _prepared = false;
        private string _mask;

        public string ZipPath { get; private set; }

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the ZIP archive.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// </summary>
        public string Mask
        {
            get { return _mask; }
            set {
                _mask = value;
                if (_filesSource != null)
                {
                    _filesSource.Mask = Mask;
                }
                _prepared = false;
            }
        }

        public ZipSource(string zipPath)
        {
            ZipPath = zipPath;
        }


        private DirectorySource _filesSource;

        /// <summary>
        /// Unpacks the zip-file and constructs a new FileArtifactSource on the unzipped directory
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        private void prepare()
        {
            if (_prepared) return;

            if (!File.Exists(ZipPath)) throw new FileNotFoundException(String.Format("Cannot prepare ZipArtifactSource: file '{0}' was not found", ZipPath ));
           
            var zc = new ZipCacher(ZipPath, CACHE_KEY);
            _filesSource = new DirectorySource(zc.GetContentDirectory(), includeSubdirectories: false);
            if (!string.IsNullOrEmpty(Mask))
            {
                _filesSource.Mask = Mask;
            }
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


        public IEnumerable<string> ListResourceUris(ResourceType? filter = default(ResourceType?))
        {
            prepare();
            return _filesSource.ListResourceUris(filter);
        }

        public ValueSet FindValueSetBySystem(string system)
        {
            prepare();
            return _filesSource.FindValueSetBySystem(system);
        }

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            prepare();
            return _filesSource.FindConceptMaps(sourceUri, targetUri);
        }

        public NamingSystem FindNamingSystem(string uniqueid)
        {
            prepare();
            return _filesSource.FindNamingSystem(uniqueid);
        }

        public Resource ResolveByUri(string uri)
        {
            prepare();
            return _filesSource.ResolveByUri(uri);
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            prepare();
            return _filesSource.ResolveByCanonicalUri(uri);
        }
    }

#endif
}
