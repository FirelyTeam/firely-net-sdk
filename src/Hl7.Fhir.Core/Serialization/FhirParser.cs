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
        #region Helper methods / stream creation methods

        public static bool ProbeIsXml(string data)
        {
            Regex xml = new Regex("^<[^>]+>");

            return xml.IsMatch(data.TrimStart());
        }

        public static bool ProbeIsJson(string data)
        {
            return data.TrimStart().StartsWith("{");
        }

        public static XDocument XDocumentFromXml(string xml)
        {
            return XDocument.Parse(SerializationUtil.SanitizeXml(xml));
        }

        public static IFhirReader FhirReaderFromXml(string xml)
        {
            // [WMR 20160421] Explicit disposal
            // return new XmlDomFhirReader(SerializationUtil.XmlReaderFromXmlText(xml));
            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml))
            {
                // [WMR 20160421] Safely dispose reader after executing JsonDomFhirReader ctor
                return new XmlDomFhirReader(reader);
            }
        }

        public static IFhirReader FhirReaderFromJson(string json)
        {
            // [WMR 20160421] Explicit disposal
            // return new JsonDomFhirReader(SerializationUtil.JsonReaderFromJsonText(json));
            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                // [WMR 20160421] Safely dispose reader after executing JsonDomFhirReader ctor
                return new JsonDomFhirReader(reader);
            }
        }

        #endregion

        internal static Base Parse(IFhirReader reader, Type dataType = null)
        {
            if (dataType == null)
                return new ResourceReader(reader).Deserialize();
            else
                return new ComplexTypeReader(reader).Deserialize(dataType);
        }

        public static Resource ParseResourceFromXml(string xml)
        {
            return (Resource)ParseFromXml(xml);
        }

        public static Base ParseFromXml(string xml, Type dataType = null)
        {
            var reader = FhirReaderFromXml(xml);
            return Parse(reader, dataType);
        }

        public static Resource ParseResourceFromJson(string json)
        {
            return (Resource)ParseFromJson(json);
        }

        public static Base ParseFromJson(string json, Type dataType = null)
        {
            var reader = FhirReaderFromJson(json);
            return Parse(reader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(XmlReader reader)
        {
            return (Resource)Parse(reader);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(JsonReader reader)
        {
            return (Resource)Parse(reader);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(XmlReader reader, Type dataType = null)
        {
            var xmlReader = new XmlDomFhirReader(reader);
            return Parse(xmlReader);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(JsonReader reader, Type dataType = null)
        {
            var jsonReader = new JsonDomFhirReader(reader);
            return Parse(jsonReader);
        }
    }
}
