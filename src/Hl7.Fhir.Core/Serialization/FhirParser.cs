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
    public class FhirXmlParser : BaseFhirParser
    {
        public FhirXmlParser(ParserSettings settings=null) : base(settings)
        {
            //
        }

        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));

        private FhirXmlNodeSettings buildNodeSettings(ParserSettings settings) =>
                new FhirXmlNodeSettings
                {
                    DisallowSchemaLocation = Settings.DisallowXsiAttributesOnRoot,
                };

        public Base Parse(string xml, Type dataType)
        {
            var xmlReader = FhirXmlNode.Parse(xml, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }

        public Base Parse(XmlReader reader, Type dataType)
        {
            var xmlReader = FhirXmlNode.Read(reader, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }
    }

    public class FhirJsonParser : BaseFhirParser
    {
        public FhirJsonParser(ParserSettings settings=null) : base(settings)
        {
            //
        }

        public T Parse<T>(string json) where T : Base => (T)Parse(json, typeof(T));

        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));

        // TODO: True for DSTU2, should be false in STU3
        private readonly FhirJsonNodeSettings jsonNodeSettings = new FhirJsonNodeSettings { AllowJsonComments = true };

        public Base Parse(string json, Type dataType)
        {
            var jsonReader =
                FhirJsonNode.Parse(json, ModelInfo.GetFhirTypeNameForType(dataType), jsonNodeSettings);
            return Parse(jsonReader, dataType);
        }

        public Base Parse(JsonReader reader, Type dataType)
        {
            var jsonReader =
                FhirJsonNode.Read(reader, ModelInfo.GetFhirTypeNameForType(dataType), jsonNodeSettings);
            return Parse(jsonReader, dataType);
        }
    }


    public class BaseFhirParser
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirParser(ParserSettings settings = null)
        {
            Settings = settings ?? new ParserSettings();
        }

        private static Lazy<ModelInspector> _inspector = createDefaultModelInspector();

        private static Lazy<ModelInspector> createDefaultModelInspector()
        {
            return new Lazy<ModelInspector>(() =>
            {
                var result = new ModelInspector();

                result.Import(typeof(Resource).GetTypeInfo().Assembly);
                return result;
            });

        }

        internal static ModelInspector Inspector
        {
            get
            {
                return _inspector.Value;
            }
        }

        private PocoBuilderSettings buildBuilderSettings(ParserSettings ps) =>
            new PocoBuilderSettings
            {
                AllowUnrecognizedEnums = ps.AllowUnrecognizedEnums,
                IgnoreUnknownMembers = ps.AcceptUnknownMembers
            };

        public Base Parse(ITypedElement element) => element.ToPoco(buildBuilderSettings(Settings));

        public T Parse<T>(ITypedElement element) where T : Base => element.ToPoco<T>(buildBuilderSettings(Settings));

        public Base Parse(ISourceNode node, Type type=null) => node.ToPoco(type, buildBuilderSettings(Settings));

        public T Parse<T>(ISourceNode node) where T : Base => node.ToPoco<T>(buildBuilderSettings(Settings));

#pragma warning disable 612, 618
        public Base Parse(IElementNavigator nav, Type type = null) => nav.ToPoco(type, buildBuilderSettings(Settings));

        public T Parse<T>(IElementNavigator nav) where T : Base => (T)nav.ToPoco<T>(buildBuilderSettings(Settings));
#pragma warning restore 612, 618


    }


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
