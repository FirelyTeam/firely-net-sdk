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
    public class FhirParser
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

        public static IFhirReader FhirReaderFromXml(string xml)
        {
            return new XmlDomFhirReader(XmlReader.Create(new StringReader(xml)));
        }

        public static IFhirReader FhirReaderFromJson(string json)
        {
            return new JsonDomFhirReader(new JsonTextReader(new StringReader(json)));
        }

        public Resource ParseResourceFromXml(string xml)
        {
            var reader = FhirReaderFromXml(xml);
            return new ResourceReader(reader).Deserialize();
        }

        public Resource ParseResourceFromJson(string json)
        {
            var reader = FhirReaderFromJson(json);
            return new ResourceReader(reader).Deserialize();
        }

        public Resource ParseResource(XmlReader reader)
        {
            var xmlReader = new XmlDomFhirReader(reader);
            return new ResourceReader(xmlReader).Deserialize();
        }

        public Resource ParseResource(JsonReader reader)
        {
            var jsonReader = new JsonDomFhirReader(reader);
            return new ResourceReader(jsonReader).Deserialize();
        }

        public Resource.ResourceMetaComponent ParseMetaFromXml(string xml)
        {
            throw Error.NotImplemented("Parsing <meta> is not yet implemented");

            //return ParseFromXml<TagList>(xml);
        }

        public Resource.ResourceMetaComponent ParseMetaFromJson(string json)
        {
            throw Error.NotImplemented("Parsing resourceType:meta is not yet implemented");
            //return ParseFromJson<TagList>(json);
        }

        public Resource.ResourceMetaComponent ParseMeta(XmlReader reader)
        {
            throw Error.NotImplemented("Parsing <meta> is not yet implemented");

            //return ParseFromXml<TagList>(xml);
        }

        public Resource.ResourceMetaComponent ParseMeta(JsonReader reader)
        {
            throw Error.NotImplemented("Parsing <meta> is not yet implemented");

            //return ParseFromXml<TagList>(xml);
        }

        public Parameters ParseQueryFromUriParameters(string resource, IEnumerable<Tuple<String, String>> parameters)
        {
            return ParametersParser.Load(resource, parameters);
        }
    }
}
