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
using Newtonsoft.Json;

namespace Hl7.Fhir.Introspection.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from individual files
    /// </summary>
    public class FileArtifactSource : IArtifactSource
    {
        private readonly string _contentDirectory;
        private readonly bool _includeSubs;

        private List<string> _artifactFiles;

        private bool _isPrepared = false;

        private string _mask;

        public string Mask
        { 
            get { return _mask; }
            set { _mask = value; _isPrepared = false; }
        }

        public FileArtifactSource(string contentDirectory, bool includeSubdirectories = false)
        {
            if (contentDirectory == null) throw Error.ArgumentNull("contentDirectory");

            _contentDirectory = contentDirectory;
            _includeSubs = includeSubdirectories;
        }

        public static string  SpecificationDirectory
        {
            get
            {
                return Path.GetDirectoryName(typeof(FileArtifactSource).Assembly.Location);
                //return Path.Combine(location, "Introspection", "Source");
            }
        }

        public FileArtifactSource(bool includeSubdirectories = false)
        {
            // Add the current directory to the list of directories with artifact content, unless there's
            // a special specification subdirectory available (next to the current DLL)
            if (Directory.Exists(SpecificationDirectory))
                _contentDirectory = SpecificationDirectory;
            else
                _contentDirectory = Directory.GetCurrentDirectory();

            _includeSubs = includeSubdirectories;
        }

        /// <summary>
        /// Prepares the source by reading all files present in the directory (matching the mask, if given)
        /// </summary>
        public void Prepare()
        {
            _artifactFiles = new List<string>();

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

            var files = allFiles.Where(name => Path.GetExtension(name) != "exe" && Path.GetExtension(name) != "dll");
            
            _artifactFiles.AddRange(files);
            _isPrepared = true;
        }

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
            if (name == null) throw Error.ArgumentNull("name");

            ensurePrepared();

            var searchString = (Path.DirectorySeparatorChar + name).ToLower();
            var fullFileName = _artifactFiles.SingleOrDefault(fn => fn.ToLower().EndsWith(searchString));

            return fullFileName == null ? null : File.OpenRead(fullFileName);
        }

        /// <summary>
        /// Locates the file belonging to the given artifactId on a filesystem (within the store directory given in the constructor)
        /// and reads an artifact with the given id from it.
        /// </summary>
        /// <param name="artifactId"></param>
        /// <returns>An artifact (Profile, ValueSet, etc) or null if an artifact with the given uri could not be located. If both an
        /// xml and a json version is available, the xml version is returned</returns>
        public Resource ReadResourceArtifact(Uri artifactId)
        {
            if (artifactId == null) throw Error.ArgumentNull("artifactId");

            ensurePrepared();

            // Locate a file that has the same name as the 'logical' id from the uri
            var logicalId = new ResourceIdentity(artifactId).Id;

            if (logicalId == null) throw Error.Argument("The artifactId {0} is not parseable as a normal http based REST endpoint with a logical id", artifactId.ToString());

            // Return the contents of the file, since there's no logical id inside the data of a simple resource file
            var xmlFilename = logicalId.EndsWith(".xml") ? logicalId : logicalId + ".xml";
            using (var contentXml = ReadContentArtifact(xmlFilename))
            {
                if (contentXml != null)
                    return FhirParser.ParseResource(XmlReader.Create(contentXml));
            }
            
            var jsonFilename = logicalId.EndsWith(".json") ? logicalId : logicalId + ".json";
            using (var contentJson = ReadContentArtifact(jsonFilename))
            {
                if (contentJson != null)
                    return FhirParser.ParseResource(new JsonTextReader(new StreamReader(contentJson)));
            }

            return null;
            
        }
        
        private void ensurePrepared()
        {
            if (!_isPrepared) Prepare();
        }     
    }
}
