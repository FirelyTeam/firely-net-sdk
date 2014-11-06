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
using Hl7.Fhir.Rest;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from (zipped) Bundles and individual files
    /// </summary>
    public class CoreZipArtifactSource : IArtifactSource
    {
        public const string CORE_SPEC_PROFILE_URI_PREFIX = "http://hl7.org/fhir/Profile/";
        public const string CORE_SPEC_CONFORMANCE_URI_PREFIX = "http://hl7.org/fhir/conformance/";
        public const string CORE_SPEC_VS_URI_PREFIX = "http://hl7.org/fhir/vs/";
        public const string CORE_SPEC_V2_VS_URI_PREFIX = "http://hl7.org/fhir/v2/vs/";
        public const string CORE_SPEC_V3_VS_URI_PREFIX = "http://hl7.org/fhir/v3/vs/";
        public const string CORE_SPEC_NAMESPACE_URI_PREFIX = "http://hl7.org/fhir/ns/";      //TODO: check prefix
        public const string CORE_SPEC_CONCEPTMAP_URI_PREFIX = "http://hl7.org/fhir/conceptmap/";     //TODO: check prefix

        private const string CACHE_KEY = "FhirArtifactCache";

        private readonly string _contentDirectory;
        private bool _isPrepared = false;

        public CoreZipArtifactSource(string contentDirectory)
        {
            _contentDirectory = contentDirectory;
        }

        public CoreZipArtifactSource()
        {
#if !PORTABLE45
            var modelDir = FileArtifactSource.SpecificationDirectory;

            // Add the current directory to the list of directories with artifact content, unless there's
            // a special subdirectory available
            if (Directory.Exists(modelDir))
                _contentDirectory = modelDir;
            else
                _contentDirectory = Directory.GetCurrentDirectory();
#else
            throw Error.NotImplemented("File based Core artifact source is not supported on the portable runtime");
#endif
        }

        /// <summary>
        /// Unpacks zip-files containing the artifact files, and enumerates all (zipped/nonzipped) files.
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        public void Prepare()
        {
#if !PORTABLE45
            _artifactFiles = new List<string>();

            //var zips = Directory.GetFiles(_contentDirectory, "*.zip");

            //// Get the files in each *.zip files present in the content directory
            //// The ZipCacher will avoid re-extracting these files.
            //foreach (string zipPath in zips)
            //{
            //    ZipCacher zc = new ZipCacher(zipPath, CACHE_KEY);
            //    _artifactFiles.AddRange(zc.GetContents());
            //}

            var zipPath = Path.Combine(_contentDirectory, "validation.zip");
            if (File.Exists(zipPath))
            {
                var zc = new ZipCacher(zipPath, CACHE_KEY);
                _artifactFiles.AddRange(zc.GetContents());
                _isPrepared = true;
            }

#else
            throw Error.NotImplemented("File based Core artifact source is not supported on the portable runtime");
#endif

        }


        private List<string> _artifactFiles;

        public IEnumerable<string> ArtifactFiles 
        {
            get
            {
                ensurePrepared();
                return _artifactFiles.Select(path => Path.GetFileName(path));
            }
        }

        public Stream ReadContentArtifact(string name)
        {
#if !PORTABLE45
            if (name == null) throw Error.ArgumentNull("name");

            ensurePrepared();

            var searchString = (Path.DirectorySeparatorChar + name).ToLower();

            // NB: uses _artifactFiles (full paths), not ArtifactFiles (which only has public list of names, not full path)
            var fullFileName = _artifactFiles.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
#else
            throw Error.NotImplemented("File based Core artifact source is not supported on the portable runtime");
#endif

        }


        /// <summary>
        /// Locates the file belonging to the given artifactId on a filesystem (within the store directory given in the constructor)
        /// and reads an artifact with the given id from it.
        /// </summary>
        /// <param name="artifactId"></param>
        /// <returns>An artifact (Profile, ValueSet, etc) or null if an artifact with the given uri could not be located</returns>
        public Resource ReadResourceArtifact(Uri artifactId)
        {
            if (artifactId == null) throw Error.ArgumentNull("artifactId");

            ensurePrepared();

            string artifactXml;

            // Core artifacts come from specific bundles files in the validation.zip
            // We're assuming the validation.zip contains xml files
            artifactXml = loadCoreArtifactXml(artifactId);

            if (artifactXml != null)
                return FhirParser.ParseResourceFromXml(artifactXml);
            else
                return null;
        }
        

        ///// <summary>
        ///// Given the Url for an artifact (e.g. http://hl7.org/fhir/Profile/AdverseReaction), determines whether this is
        ///// a core artifact that is pre-packaged in core files from the validation.zip
        ///// </summary>
        ///// <param name="artifactId">The location on the hl7.org repository of the core artifact</param>
        ///// <returns></returns>
        //public static bool IsCoreArtifact(Uri artifactId)
        //{
        //    if(artifactId == null) throw Error.ArgumentNull("artifactId");

        //    //var normalized = artifactId.ToString().ToLower();

        //    //return normalized.StartsWith(CORE_SPEC_URI_PREFIX);
        //    return artifactId.ToString().StartsWith(CORE_SPEC_URI_PREFIX);
        //}


        private string loadCoreArtifactXml(Uri artifactId)
        {
           // var normalized = artifactId.ToString().ToLower();
            var normalized = artifactId.ToString();     // do case-sensitive search

            // Depending on the actual artifact uri, determine in which supplied bundled
            // file these artifacts should be found
            if (normalized.StartsWith(CORE_SPEC_PROFILE_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "profiles-types.xml", "profiles-resources.xml");
            else if (normalized.StartsWith(CORE_SPEC_CONFORMANCE_URI_PREFIX) || normalized.StartsWith(CORE_SPEC_CONCEPTMAP_URI_PREFIX) ||
                            normalized.StartsWith(CORE_SPEC_NAMESPACE_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "profiles-resources.xml");
            else if (normalized.StartsWith(CORE_SPEC_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "valuesets.xml");
            else if (normalized.StartsWith(CORE_SPEC_V2_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "v2-tables.xml");
            else if (normalized.StartsWith(CORE_SPEC_V3_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "v3-codesystems.xml");
            else
                return readXmlEntryFromCoreBundle(artifactId, "valuesets.xml"); 
        }

        public static readonly XName ENTRY_CONTENT = XmlNs.XATOM + "content";

        private string readXmlEntryFromCoreBundle(Uri artifactId, params string[] fileNames)
        {
            foreach (var filename in fileNames)
            {
                // Note: no exception handling. If the expected bundled file cannot be
                // read, throw the original exception.
                using (var content = ReadContentArtifact(filename))
                {
                    if (content == null) throw new FileNotFoundException("Cannot find bundled core file " + filename);

                    var scanner = new XmlFeedScanner(content);
                    var entry = scanner.FindEntryById(artifactId);

                    if (entry != null)
                    {
                        // Matching entry found
                        try
                        {
                            var contentElement = entry.Element(ENTRY_CONTENT);
                            var entryContentXml = contentElement.Elements().FirstOrDefault();
                            return entryContentXml == null ? null : entryContentXml.ToString();
                        }
                        catch
                        {
                            throw Error.Format("Entry {0} was found in core bundle {1}, but cannot parse its contents", null, artifactId, filename);
                        }
                    }
                }
            }

            return null;
        }
            
        private void ensurePrepared()
        {
            if (!_isPrepared) Prepare();
        }     
    }
}
