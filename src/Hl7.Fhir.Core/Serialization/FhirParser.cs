/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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
        public FhirXmlParser() : base()
        {

        }

        public FhirXmlParser(ParserSettings settings) : base(settings)
        {
        }

        [Obsolete("Create a new navigating parser (XmlDomFhirNavigator.Create()), and then use one of the Parse() overloads taking IElementNavigator")]
        public static IFhirReader CreateFhirReader(string xml, bool disallowXsiAttributesOnRoot)
            => new ElementNavFhirReader(XmlDomFhirNavigator.Create(xml), disallowXsiAttributesOnRoot);


        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));
        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

#pragma warning disable 612, 618
        public Base Parse(string xml, Type dataType)
        {
            IFhirReader xmlReader = new ElementNavFhirReader(XmlDomFhirNavigator.Create(xml), Settings.DisallowXsiAttributesOnRoot);
            return Parse(xmlReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(XmlReader reader, Type dataType)
        {
            IFhirReader xmlReader = new ElementNavFhirReader(XmlDomFhirNavigator.Create(reader), Settings.DisallowXsiAttributesOnRoot);
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

        [Obsolete("Create a new navigating parser (JsonDomFhirNavigator.Create()), and then use one of the Parse() overloads taking IElementNavigator")]
        public static IFhirReader CreateFhirReader(string json) =>  new ElementNavFhirReader(JsonDomFhirNavigator.Create(json));

        public T Parse<T>(string json) where T:Base => (T)Parse(json, typeof(T));

        // [WMR 20160421] Caller is responsible for disposing reader
        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));

#pragma warning disable 612,618
        public Base Parse(string json, Type dataType)
        {
            IFhirReader jsonReader = new ElementNavFhirReader(JsonDomFhirNavigator.Create(json, ModelInfo.GetFhirTypeNameForType(dataType)));
            return Parse(jsonReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(JsonReader reader, Type dataType)
        {
            IFhirReader jsonReader = new ElementNavFhirReader(JsonDomFhirNavigator.Create(reader, ModelInfo.GetFhirTypeNameForType(dataType)));
            return Parse(jsonReader, dataType);
        }
#pragma warning restore 612, 618
    }


    public class BaseFhirParser
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirParser(ParserSettings settings)
        {
            if (settings == null) throw Error.ArgumentNull(nameof(settings));
            Settings = settings;
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

        [Obsolete("Create a new navigating parser (Xml/JsonDomFhirNavigator.Create()), and then use one of the Parse() overloads taking IElementNavigator")]
        public T Parse<T>(IFhirReader reader) where T : Base
        {
            return (T)Parse(reader, typeof(T));
        }

        [Obsolete("Create a new navigating parser (Xml/JsonDomFhirNavigator.Create()), and then use one of the Parse() overloads taking IElementNavigator")]
        public Base Parse(IFhirReader reader, Type dataType)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (dataType == null) throw Error.ArgumentNull(nameof(dataType));

            if(dataType.CanBeTreatedAsType(typeof(Resource)))
                return new ResourceReader(reader, Settings).Deserialize();
            else
                return new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }

        public T Parse<T>(IElementNavigator nav) where T : Base => (T)Parse(nav, typeof(T));

        public Base Parse(IElementNavigator nav, Type dataType)
        {
            if (nav == null) throw Error.ArgumentNull(nameof(nav));
            if (dataType == null) throw Error.ArgumentNull(nameof(dataType));

            var reader = new ElementNavFhirReader(nav, Settings.DisallowXsiAttributesOnRoot);

            if (dataType.CanBeTreatedAsType(typeof(Resource)))
                return new ResourceReader(reader, Settings).Deserialize();
            else
                return new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }
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

        [Obsolete("Use FhirXmlParser.CreateFhirReader() instead")]
        public static IFhirReader FhirReaderFromXml(string xml, bool disallowXsiAttributesOnRoot = false) => FhirXmlParser.CreateFhirReader(xml, disallowXsiAttributesOnRoot);

        [Obsolete("Use FhirJsonParser.CreateFhirReader() instead")]
        public static IFhirReader FhirReaderFromJson(string json) => FhirJsonParser.CreateFhirReader(json);

        #endregion

        private static FhirXmlParser _xmlParser = new FhirXmlParser();
        private static FhirJsonParser _jsonParser = new FhirJsonParser();

        [Obsolete("Create an instance of FhirXmlParser and call Parse<Resource>()")]
        public static Resource ParseResourceFromXml(string xml)
        {
            return _xmlParser.Parse<Resource>(xml);
        }

        [Obsolete("Create an instance of FhirXmlParser and call Parse()")]
        public static Base ParseFromXml(string xml, Type dataType = null)
        {
            if (dataType == null)
                return _xmlParser.Parse<Base>(xml);
            else
                return _xmlParser.Parse(xml, dataType);
        }

        [Obsolete("Create an instance of FhirJsonParser and call Parse<Resource>()")]
        public static Resource ParseResourceFromJson(string json)
        {
            return _jsonParser.Parse<Resource>(json);
        }

        [Obsolete("Create an instance of FhirJsonParser and call Parse()")]
        public static Base ParseFromJson(string json, Type dataType = null)
        {
            if (dataType == null)
                return _jsonParser.Parse<Base>(json);
            else
                return _jsonParser.Parse(json, dataType);
        }

        [Obsolete("Create an instance of FhirXmlParser and call Parse<Resource>()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(XmlReader reader)
        {
            return _xmlParser.Parse<Resource>(reader);
        }

        [Obsolete("Create an instance of FhirJsonParser and call Parse<Resource>()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Resource ParseResource(JsonReader reader)
        {
            return _jsonParser.Parse<Resource>(reader);
        }

        [Obsolete("Create an instance of FhirXmlParser and call Parse()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(XmlReader reader, Type dataType = null)
        {
            if (dataType == null)
                return _xmlParser.Parse<Base>(reader);
            else
                return _xmlParser.Parse(reader, dataType);
        }

        [Obsolete("Create an instance of FhirJsonParser and call Parse()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(JsonReader reader, Type dataType = null)
        {
            if (dataType == null)
                return _jsonParser.Parse<Base>(reader);
            else
                return _jsonParser.Parse(reader, dataType);
        }
    }
}
