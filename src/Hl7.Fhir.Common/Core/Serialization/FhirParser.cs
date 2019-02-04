/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;


namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Static access to parsing.
    /// </summary>
    /// <remarks>Only present for backwards-compatibility reasons</remarks>
    public static class FhirParser
    {
        #region Helper methods / stream creation methods

        [Obsolete("Use SerializationUtil.ProbeIsXml() instead")]
        public static bool ProbeIsXml(string data) => SerializationUtil.ProbeIsXml(data);

        [Obsolete("Use SerializationUtil.ProbeIsJson() instead")]
        public static bool ProbeIsJson(string data) => SerializationUtil.ProbeIsJson(data);

        [Obsolete("Use SerializationUtil.XDocumentFromXmlText() instead")]
        public static XDocument XDocumentFromXml(string xml) => SerializationUtil.XDocumentFromXmlText(xml);

        #endregion

        private static FhirXmlParser _xmlParser = new FhirXmlParser();
        private static FhirJsonParser _jsonParser = new FhirJsonParser();

        [Obsolete("Create an instance of FhirXmlParser and call Parse<Resource>()")]
        public static Resource ParseResourceFromXml(string xml) => _xmlParser.Parse<Resource>(xml);

        [Obsolete("Create an instance of FhirXmlParser and call Parse()")]
        public static Base ParseFromXml(string xml, Type dataType = null) =>
            dataType == null ? _xmlParser.Parse<Base>(xml) : _xmlParser.Parse(xml, dataType);

        [Obsolete("Create an instance of FhirJsonParser and call Parse<Resource>()")]
        public static Resource ParseResourceFromJson(string json) => _jsonParser.Parse<Resource>(json);

        [Obsolete("Create an instance of FhirJsonParser and call Parse()")]
        public static Base ParseFromJson(string json, Type dataType = null) => dataType == null ? _jsonParser.Parse<Base>(json) : _jsonParser.Parse(json, dataType);

        [Obsolete("Create an instance of FhirXmlParser and call Parse<Resource>()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(XmlReader reader) => _xmlParser.Parse<Resource>(reader);

        [Obsolete("Create an instance of FhirJsonParser and call Parse<Resource>()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(JsonReader reader) => _jsonParser.Parse<Resource>(reader);

        [Obsolete("Create an instance of FhirXmlParser and call Parse()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(XmlReader reader, Type dataType = null) => dataType == null ? _xmlParser.Parse<Base>(reader) : _xmlParser.Parse(reader, dataType);

        [Obsolete("Create an instance of FhirJsonParser and call Parse()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(JsonReader reader, Type dataType = null) => dataType == null ? _jsonParser.Parse<Base>(reader) : _jsonParser.Parse(reader, dataType);
    }

}
