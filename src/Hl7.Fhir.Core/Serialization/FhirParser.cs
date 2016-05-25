/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
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

        public static IFhirReader CreateFhirReader(string xml, bool disallowXsiAttributesOnRoot)
        {
            // [WMR 20160421] Explicit disposal
            // return new XmlDomFhirReader(SerializationUtil.XmlReaderFromXmlText(xml));
            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml))
            {
                // [WMR 20160421] Safely dispose reader after executing JsonDomFhirReader ctor
                return new XmlDomFhirReader(reader, disallowXsiAttributesOnRoot);
            }
        }

        public T Parse<T>(string xml) where T : Resource
        {
            return (T)Parse(xml, typeof(T));
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public T Parse<T>(XmlReader reader) where T : Resource
        {
            return (T)Parse(reader, typeof(T));
        }

        public Base Parse(string xml, Type dataType)
        {
            if (dataType == null) throw Error.ArgumentNull("dataType");

            var reader = CreateFhirReader(xml, Settings.DisallowXsiAttributesOnRoot);
            return Parse(reader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(XmlReader reader, Type dataType)
        {
            if (dataType == null) throw Error.ArgumentNull("dataType");

            var xmlReader = new XmlDomFhirReader(reader, Settings.DisallowXsiAttributesOnRoot);
            return Parse(xmlReader, dataType);
        }

    }

    public class FhirJsonParser : BaseFhirParser
    {
        public FhirJsonParser() : base()
        {

        }

        public FhirJsonParser(ParserSettings settings) : base(settings)
        {
        }

        public static IFhirReader CreateFhirReader(string json)
        {
            // [WMR 20160421] Explicit disposal
            // return new JsonDomFhirReader(SerializationUtil.JsonReaderFromJsonText(json));
            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                // [WMR 20160421] Safely dispose reader after executing JsonDomFhirReader ctor
                return new JsonDomFhirReader(reader);
            }
        }

        public T Parse<T>(string json) where T:Resource
        {
            return (T)Parse(json, typeof(T));
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public T Parse<T>(JsonReader reader) where T : Resource
        {
            return (T)Parse(reader, typeof(T));
        }

        public Base Parse(string json, Type dataType)
        {
            var reader = CreateFhirReader(json);
            return Parse(reader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(JsonReader reader, Type dataType)
        {
            var jsonReader = new JsonDomFhirReader(reader);
            return Parse(jsonReader, dataType);
        }
    }


    public class BaseFhirParser
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirParser(ParserSettings settings)
        {
            if (settings == null) throw Error.ArgumentNull("settings");
            Settings = settings;
        }

        public BaseFhirParser()
        {
            Settings = new ParserSettings();
        }

        //public static void Clear()
        //{
        //    _inspector = createDefaultModelInspector();
        //}

        //public static void AddModelAssembly(Assembly assembly)
        //{
        //    Inspector.Import(assembly);
        //}

        //public static void AddModelType(Type type)
        //{
        //    if (type.IsEnum())
        //        Inspector.ImportEnum(type);
        //    else
        //        Inspector.ImportType(type);
        //}


        private static Lazy<ModelInspector> _inspector = createDefaultModelInspector();

        private static Lazy<ModelInspector> createDefaultModelInspector()
        {
            return new Lazy<ModelInspector>(() =>
            {
                var result = new ModelInspector();
#if PORTABLE45
                    result.Import(typeof(Resource).GetTypeInfo().Assembly);
#else
                result.Import(typeof(Resource).Assembly);
#endif
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

        public T Parse<T>(IFhirReader reader) where T : Resource
        {
            return (T)Parse(reader, typeof(T));
        }

        public Base Parse(IFhirReader reader, Type dataType)
        {
            if(dataType.CanBeTreatedAsType(typeof(Resource)))
                return new ResourceReader(reader, Settings).Deserialize();
            else
                return new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }
    }


    public static class FhirParser
    {
        #region Helper methods / stream creation methods

        [Obsolete("Use SerializationUtil.ProbeIsXml() instead")]
        public static bool ProbeIsXml(string data)
        {
            return SerializationUtil.ProbeIsXml(data);
        }

        [Obsolete("Use SerializationUtil.ProbeIsJson() instead")]
        public static bool ProbeIsJson(string data)
        {
            return SerializationUtil.ProbeIsJson(data);
        }

        [Obsolete("Use SerializationUtil.XDocumentFromXmlText() instead")]
        public static XDocument XDocumentFromXml(string xml)
        {
            return SerializationUtil.XDocumentFromXmlText(xml);
        }

        [Obsolete("Use FhirXmlParser.CreateFhirReader() instead")]
        public static IFhirReader FhirReaderFromXml(string xml, bool disallowXsiAttributesOnRoot = false)
        {
            return FhirXmlParser.CreateFhirReader(xml, disallowXsiAttributesOnRoot);
        }

        [Obsolete("Use FhirJsonParser.CreateFhirReader() instead")]
        public static IFhirReader FhirReaderFromJson(string json)
        {
            return FhirJsonParser.CreateFhirReader(json);
        }

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
                return _xmlParser.Parse<Resource>(xml);
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
                return _jsonParser.Parse<Resource>(json);
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
                return _xmlParser.Parse<Resource>(reader);
            else
                return _xmlParser.Parse(reader, dataType);
        }

        [Obsolete("Create an instance of FhirJsonParser and call Parse()")]
        // [WMR 20160421] Caller is responsible for disposing reader
        public static Base Parse(JsonReader reader, Type dataType = null)
        {
            if (dataType == null)
                return _jsonParser.Parse<Resource>(reader);
            else
                return _jsonParser.Parse(reader, dataType);
        }
    }
}
