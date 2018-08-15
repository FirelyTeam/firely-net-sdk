/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;


namespace Hl7.Fhir.Serialization
{
    public class FhirXmlParser : BaseFhirParser
    {
        public FhirXmlParser() : base()
        {

        }

        public FhirXmlParser(ParserSettings settings) : base(settings)
        {
        }

        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));
        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

#pragma warning disable 612, 618
        public Base Parse(string xml, Type dataType)
        {
            IFhirReader xmlReader = new SourceNodeToFhirReaderAdapter(FhirXmlNode.Parse(xml,
                new FhirXmlNodeSettings
                {
                    DisallowSchemaLocation = this.Settings.DisallowXsiAttributesOnRoot,
                }));
            return Parse(xmlReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(XmlReader reader, Type dataType)
        {
            IFhirReader xmlReader = new SourceNodeToFhirReaderAdapter(FhirXmlNode.Read(reader,
                new FhirXmlNodeSettings
                {
                    DisallowSchemaLocation = this.Settings.DisallowXsiAttributesOnRoot
                }));
            return Parse(xmlReader, dataType);
        }
#pragma warning restore 612, 618
    }

    public class FhirJsonParser : BaseFhirParser
    {
        public FhirJsonParser() : base()
        {

        }

        public FhirJsonParser(ParserSettings settings) : base(settings)
        {
        }

        public T Parse<T>(string json) where T : Base => (T)Parse(json, typeof(T));

        // [WMR 20160421] Caller is responsible for disposing reader
        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));


#pragma warning disable 612,618
        public Base Parse(string json, Type dataType)
        {
            IFhirReader jsonReader = new SourceNodeToFhirReaderAdapter(
                FhirJsonNode.Parse(json, ModelInfo.GetFhirTypeNameForType(dataType),
                new FhirJsonNodeSettings
                {
                    AllowJsonComments = true       // DSTU2, should be false in STU3
                }));
            return Parse(jsonReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(JsonReader reader, Type dataType)
        {
            IFhirReader jsonReader = new SourceNodeToFhirReaderAdapter(
                FhirJsonNode.Read(reader, ModelInfo.GetFhirTypeNameForType(dataType),
                new FhirJsonNodeSettings
                {
                    AllowJsonComments = true       // DSTU2, should be false in STU3
                })); return Parse(jsonReader, dataType);
        }
#pragma warning restore 612, 618
    }


    public class BaseFhirParser
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirParser(ParserSettings settings)
        {
            Settings = settings ?? throw Error.ArgumentNull(nameof(settings));
        }

        public BaseFhirParser()
        {
            Settings = new ParserSettings();
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

        [Obsolete("Create a new navigating parser using FhirXmlNavigator/FhirJsonNavigator.ForRoot()")]
        internal Base Parse(IFhirReader reader, Type dataType)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (dataType == null) throw Error.ArgumentNull(nameof(dataType));

            return dataType.CanBeTreatedAsType(typeof(Resource))
                ? new ResourceReader(reader, Settings).Deserialize()
                : new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }

        public Base Parse(ITypedElement nav, Type dataType) =>
            Parse(new ElementNodeToFhirReaderAdapter(nav), dataType);

        public T Parse<T>(ITypedElement nav) where T : Base => (T)Parse(nav, typeof(T));


        public Base Parse(IElementNavigator nav, Type dataType) =>
            Parse(new ElementNavToFhirReaderAdapter(nav), dataType);

        public T Parse<T>(IElementNavigator nav) where T : Base => (T)Parse(nav, typeof(T));


        public Base Parse(ISourceNode nav, Type dataType) =>
            Parse(new SourceNodeToFhirReaderAdapter(nav), dataType);

        public T Parse<T>(ISourceNode nav) where T : Base => (T)Parse(nav, typeof(T));
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
