/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace Hl7.Fhir.Serialization
{
    public partial class FhirParser
    {
        public static bool ProbeIsXml(string data)
        {
            Regex xml = new Regex("^<[^>]+>");

            return xml.IsMatch(data.TrimStart());
        }

        public static bool ProbeIsJson(string data)
        {
            return data.TrimStart().StartsWith("{");
        }

        internal static object Parse(IFhirReader reader)
        {
            return new ResourceReader(reader).Deserialize();
        }

        internal static T Parse<T>(IFhirReader reader) where T : class
        {
            var result = Parse(reader);

            if (result is T)
                return (T)result;
            else
                throw Error.Format("Parsed data is not of given type {0}", reader, typeof(T).Name);
        }


        internal static object Parse(XmlReader reader)
        {
            return Parse(new XmlDomFhirReader(reader));
        }

        internal static T Parse<T>(XmlReader reader) where T : class
        {
            return Parse<T>(new XmlDomFhirReader(reader));
        }


        internal static object Parse(JsonReader reader)
        {
            return Parse(new JsonDomFhirReader(reader));
        }

        internal static T Parse<T>(JsonReader reader) where T : class
        {
            return Parse<T>(new JsonDomFhirReader(reader));
        }

        internal static object ParseFromXml(string xml)
        {
            return Parse(XmlReaderFromString(xml));
        }

        internal static T ParseFromXml<T>(string xml) where T : class
        {
            return Parse<T>(XmlReaderFromString(xml));
        }

        internal static object ParseFromJson(string json)
        {
             return Parse(JsonReaderFromString(json));
        }

        internal static T ParseFromJson<T>(string json) where T : class
        {
             return Parse<T>(JsonReaderFromString(json));
        }

        public static Resource ParseResourceFromXml(string xml)
        {
            return ParseFromXml<Resource>(xml);
        }

        public static Resource ParseResourceFromJson(string json)
        {
            return ParseFromJson<Resource>(json);
        }

        public static TagList ParseTagListFromXml(string xml)
        {
            return ParseFromXml<TagList>(xml);
        }

        public static TagList ParseTagListFromJson(string json)
        {
            return ParseFromJson<TagList>(json);
        }

        public static Resource ParseResource(XmlReader reader)
        {
            return Parse<Resource>(reader);
        }

        public static Resource ParseResource(JsonReader reader)
        {
            return Parse<Resource>(reader);
        }

        public static TagList ParseTagList(XmlReader reader)
        {
            return Parse<TagList>(reader);
        }

        public static TagList ParseTagList(JsonReader reader)
        {
            return Parse<TagList>(reader);
        }

        public static BundleEntry ParseBundleEntry(JsonReader reader)
        {
            return BundleJsonParser.LoadEntry(reader);
        }

        public static BundleEntry ParseBundleEntry(XmlReader reader)
        {
            return BundleXmlParser.LoadEntry(reader);
        }

        public static BundleEntry ParseBundleEntryFromJson(string json)
        {
            return ParseBundleEntry(JsonReaderFromString(json));
        }

        public static BundleEntry ParseBundleEntryFromXml(string xml)
        {
            return ParseBundleEntry(XmlReaderFromString(xml));
        }

        public static Bundle ParseBundle(JsonReader reader)
        {
            return BundleJsonParser.Load(reader);
        }

        public static Bundle ParseBundleFromJson(string json)
        {
            return ParseBundle(JsonReaderFromString(json));
        }

        public static Bundle ParseBundle(XmlReader reader)
        {
            return BundleXmlParser.Load(reader);
        }

        public static Bundle ParseBundleFromXml(string xml)
        {
            return ParseBundle(XmlReaderFromString(xml));
        }

        public static Query ParseQueryFromUriParameters(string resource, IEnumerable<Tuple<String, String>> parameters)
        {
            return QueryParser.Load(resource, parameters);
        }
      
        internal static XmlReader XmlReaderFromString(string xml)
        {
            return XmlReader.Create(new StringReader(xml));
        }

        internal static JsonTextReader JsonReaderFromString(string json)
        {
            return new JsonTextReader(new StringReader(json));
        }
    }
}
