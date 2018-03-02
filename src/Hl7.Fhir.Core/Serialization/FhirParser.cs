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
        public FhirXmlParser(Model.Version version) : base(version)
        { }

        public FhirXmlParser(ParserSettings settings) : base(settings)
        { }

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
        public FhirJsonParser(Model.Version version) : base(version)
        { }

        public FhirJsonParser(ParserSettings settings) : base(settings)
        { }

        [Obsolete("Create a new navigating parser (JsonDomFhirNavigator.Create()), and then use one of the Parse() overloads taking IElementNavigator")]
        public static IFhirReader CreateFhirReader(string json) =>  new ElementNavFhirReader(JsonDomFhirNavigator.Create(json));

        public T Parse<T>(string json) where T:Base => (T)Parse(json, typeof(T));

        // [WMR 20160421] Caller is responsible for disposing reader
        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));

#pragma warning disable 612,618
        public Base Parse(string json, Type dataType)
        {
            IFhirReader jsonReader = new ElementNavFhirReader(JsonDomFhirNavigator.Create(json, AllVersionsModelInfo.GetFhirTypeNameForType(dataType)));
            return Parse(jsonReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(JsonReader reader, Type dataType)
        {
            IFhirReader jsonReader = new ElementNavFhirReader(JsonDomFhirNavigator.Create(reader, AllVersionsModelInfo.GetFhirTypeNameForType(dataType)));
            return Parse(jsonReader, dataType);
        }
#pragma warning restore 612, 618
    }


    public class BaseFhirParser
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirParser(Model.Version version) : this(new ParserSettings(version))
        { }

        public BaseFhirParser(ParserSettings settings)
        {
            Settings = settings ?? throw Error.ArgumentNull(nameof(settings));
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
}
