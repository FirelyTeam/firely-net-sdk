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
using Ionic.Zip;
using Hl7.Fhir.Serialization;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;

namespace Hl7.Fhir.Api.Introspection
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from (zipped) Bundles and individual files
    /// </summary>
    public class FileArtifactSource : IArtifactSource
    {
        public const string CORE_SPEC_URI_PREFIX = "http://hl7.org/fhir/";
        public const string CORE_SPEC_PROFILE_URI_PREFIX = "http://hl7.org/fhir/profile/";
        public const string CORE_SPEC_CONFORMANCE_URI_PREFIX = "http://hl7.org/fhir/conformance/";
        public const string CORE_SPEC_VS_URI_PREFIX = "http://hl7.org/fhir/vs/";
        public const string CORE_SPEC_V2_VS_URI_PREFIX = "http://hl7.org/fhir/v2/vs/";
        public const string CORE_SPEC_DICOMVS_URI_PREFIX = "http://nema.org/dicom/vs/";
        public const string CORE_SPEC_V3_VS_URI_PREFIX = "http://hl7.org/fhir/v3/vs/";
        public const string CORE_SPEC_NAMESPACE_URI_PREFIX = "http://hl7.org/fhir/ns/";      //TODO: check prefix
        public const string CORE_SPEC_CONCEPTMAP_URI_PREFIX = "http://hl7.org/fhir/conceptmap/";     //TODO: check prefix

        private const string CACHE_KEY = "FhirArtifactCache";

        private readonly string _contentDirectory;
        private bool _isPrepared = false;

        public FileArtifactSource(string contentDirectory)
        {
            _contentDirectory = contentDirectory;
        }

        public FileArtifactSource()
        {
            var modelDir = Path.Combine(Directory.GetCurrentDirectory(), "Model");

            // Add the current directory to the list of directories with artifact content, unless there's
            // a special "Model" subdirectory available
            if (Directory.Exists(modelDir))
                _contentDirectory = modelDir;
            else
                _contentDirectory = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Unpacks zip-files containing the artifact files, and enumerates all (zipped/nonzipped) files.
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        public void Prepare()
        {
            _artifactFiles = new List<string>();

            // First add files present in the content directory (this includes the zips themselves!)
            _artifactFiles.AddRange(listContentFilesInDirectory(_contentDirectory));

            var zips = Directory.GetFiles(_contentDirectory, "*.zip");

            // Get the files in each *.zip files present in the content directory
            // The ZipCacher will avoid re-extracting these files.
            foreach (string zipPath in zips)
            {
                ZipCacher zc = new ZipCacher(zipPath, CACHE_KEY);
                _artifactFiles.AddRange(zc.GetContents());
            }

            _isPrepared = true;
        }


        private static IEnumerable<string> listContentFilesInDirectory(string fullPath)
        {
            var allFiles = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories);

            return allFiles.Where(name => Path.GetExtension(name) != "exe" && Path.GetExtension(name) != "dll");
        }


        private List<string> _artifactFiles;

        public IEnumerable<string> ArtifactFiles 
        {
            get
            {
                ensurePrepared();
                return _artifactFiles;
            }
        }

        public Stream ReadContentArtifact(string name)
        {
            if (name == null) throw Error.ArgumentNull("name");

            ensurePrepared();

            var searchString = (Path.DirectorySeparatorChar + name).ToLower();
            var fullFileName = ArtifactFiles.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
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
            if (IsCoreArtifact(artifactId))
                artifactXml = loadCoreArtifactXml(artifactId);
            else
                artifactXml = loadUserArtifactXml(artifactId);

            // We're assuming the validation.zip contains xml files
            if (artifactXml != null)
                return FhirParser.ParseResourceFromXml(artifactXml);
            else
                return null;
        }


        /// <summary>
        /// Given the Url for an artifact (e.g. http://hl7.org/fhir/profile/adversereaction), determines whether this is
        /// a core artifact that is pre-packaged in core files from the validation.zip
        /// </summary>
        /// <param name="artifactId">The location on the hl7.org repository of the core artifact</param>
        /// <returns></returns>
        public static bool IsCoreArtifact(Uri artifactId)
        {
            if(artifactId == null) throw Error.ArgumentNull("artifactId");

            var normalized = artifactId.ToString().ToLower();

            return normalized.StartsWith(CORE_SPEC_URI_PREFIX);
        }


        private string loadCoreArtifactXml(Uri artifactId)
        {
            var normalized = artifactId.ToString().ToLower();

            // Depending on the actual artifact uri, determine in which supplied bundled
            // file these artifacts should be found
            if (normalized.StartsWith(CORE_SPEC_PROFILE_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-profiles-datatypes.xml", "core-profiles-resources.xml");
            else if (normalized.StartsWith(CORE_SPEC_CONFORMANCE_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-conformances-base.xml");
            else if (normalized.StartsWith(CORE_SPEC_CONCEPTMAP_URI_PREFIX))
                throw Error.NotImplemented("Don't know where to locate core ConceptMaps, so this feature has not yet been implemented");
            else if (normalized.StartsWith(CORE_SPEC_NAMESPACE_URI_PREFIX))
                throw Error.NotImplemented("Namespaces are a DSTU2 feature, so this feature has not yet been implemented");
            else if (normalized.StartsWith(CORE_SPEC_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-valuesets-fhir.xml");
            else if (normalized.StartsWith(CORE_SPEC_V2_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-valuesets-v2.xml");
            else if (normalized.StartsWith(CORE_SPEC_V3_VS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-valuesets-v3.xml");
            else if (normalized.StartsWith(CORE_SPEC_DICOMVS_URI_PREFIX))
                return readXmlEntryFromCoreBundle(artifactId, "core-valuesets-dicom.xml");
            else
                throw Error.NotImplemented("Url {0} was recognized as a core artifact, but I don't know where to locate it within validation.zip", normalized);
        }

        public static readonly XName ENTRY_CONTENT = BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_CONTENT;

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


        private string loadUserArtifactXml(Uri artifactId)
        {
            // Locate a file that has the same name as the 'logical' id from the uri
            var logicalId = new ResourceIdentity(artifactId).Id;

            if (logicalId == null) throw Error.Argument("The artifactId {0} is not parseable as a normal http based REST endpoint with a logical id", artifactId.ToString());

            // Return the contents of the file, since there's no logical id inside the data of a simple resource file
            using (var content = ReadContentArtifact(logicalId + ".xml"))
            {
                if (content == null) return null;

                return XmlReader.Create(content).ReadOuterXml();
            }
        }

        
        private void ensurePrepared()
        {
            if (!_isPrepared) Prepare();
        }     
    }


    //public enum ArtifactType
    //{
    //    Profile,
    //    ValueSet,
    //    Conformance,
    //    Namespace,
    //    ConceptMap
    //}
}
