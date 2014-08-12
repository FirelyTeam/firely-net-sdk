using Hl7.Fhir.Introspection.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Hl7.Fhir.Model;
using Ionic.Zip;
using Hl7.Fhir.Support;
using Hl7.Fhir.Serialization;
using System.Xml.Linq;

namespace Fhir.Profiling.IO
{
    public class EmbeddedResource
    {
        static Stream zipstream;

        public static Stream GetStream<T>(string name)
        {
            Assembly assembly = typeof(T).Assembly;
            return assembly.GetManifestResourceStream(name);
        }

        public static IEnumerable<string> ZipXmlContentStrings<T>(string name)
        {
            Stream stream = GetStream<T>(name);
            ZipFile zip = Ionic.Zip.ZipFile.Read(stream);
            foreach (ZipEntry entry in zip)
            {
                if (Path.GetExtension(entry.FileName) == ".xml")
                {
                    Stream output = entry.OpenReader();
                    StreamReader reader = new StreamReader(output);
                    string text = reader.ReadToEnd();
                    yield return text;
                }
            }
        }

        public static ArtifactSource CreateArtifactSource<T>(string name)
        {
            IEnumerable<string> texts = ZipXmlContentStrings<T>(name);
            ResourceCollection col = new ResourceCollection(texts);
            IEnumerable<ResourceEntry> entries = col.ResourceEntries();
            ArtifactSource source = new ArtifactSource(entries);
            return source;
        }


        public static string GetText<T>(string name)
        {
            string s;
            using (Stream stream = GetStream<T>(name))
            using (StreamReader reader = new StreamReader(stream))
            {
                s = reader.ReadToEnd();
            }
            return s;
        }

        public static XPathNavigator GetXPathNavigator<T>(string name)
        {
            using (Stream stream = GetStream<T>(name))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                return doc.CreateNavigator();
            }
        }

        public static ZipFile ZipFile<T>(string name)
        {
            Stream stream = GetStream<T>(name);
            ZipFile zip = Ionic.Zip.ZipFile.Read(stream);
            return zip;
        }
    }

    public class ResourceCollection
    {
        List<string> texts;

        public ResourceCollection(IEnumerable<string> texts)
        {
            this.texts = texts.ToList();
        }

        public IEnumerable<ResourceEntry> ResourceEntries()
        {
            foreach (string text in texts)
            {
                Bundle bundle = FhirParser.ParseBundleFromXml(text);
                foreach (ResourceEntry entry in bundle.Entries.OfType<ResourceEntry>())
                {
                    yield return entry;
                }
            }
        }
        
        public static IEnumerable<XElement> FeedEntries(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                       && reader.LocalName == "entry"
                       && reader.NamespaceURI == BundleXmlParser.ATOMPUB_NS)
                {
                    XElement element = (XElement)XElement.ReadFrom(reader);
                    yield return element;
                }
            }
        }

        public static readonly XName ENTRY_CONTENT = BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_CONTENT;

        public static string Content(XElement element)
        {
            var contentElement = element.Element(ENTRY_CONTENT);
            var entryContentXml = contentElement.Elements().FirstOrDefault();
            return entryContentXml == null ? null : entryContentXml.ToString();
        }
    }

    public class ArtifactSource : IArtifactSource
    {
        
        List<ResourceEntry> entries;

        public ArtifactSource(IEnumerable<ResourceEntry> entries)
        {
            this.entries = entries.ToList();
        }
        
        private void load()
        {
           
        }

        bool _isPrepared = false;
        public void Prepare()
        {
            if (!_isPrepared)
            {
                load();
                _isPrepared = true;
            }
        }
    
        public Stream ReadContentArtifact(string name)
        {
            Prepare();
            throw new NotImplementedException();
        }

        public Resource ReadResourceArtifact(Uri artifactId)
        {
            
            Prepare();
            foreach (ResourceEntry entry in entries)
            {
                if (entry.Id.ToString().ToLower() == artifactId.ToString().ToLower())
                    return entry.Resource;
            }
            return null;
        }
    }
}
