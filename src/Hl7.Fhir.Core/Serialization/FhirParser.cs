/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
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
            IFhirReader xmlReader = new ElementNavFhirReader(FhirXmlNavigator.Untyped(xml,
                new FhirXmlNavigatorSettings
                {
                    DisallowSchemaLocation = this.Settings.DisallowXsiAttributesOnRoot,
                }));
            return Parse(xmlReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(XmlReader reader, Type dataType)
        {
            IFhirReader xmlReader = new ElementNavFhirReader(FhirXmlNavigator.Untyped(reader,
                new FhirXmlNavigatorSettings
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
            IFhirReader jsonReader = new ElementNavFhirReader(
                FhirJsonNavigator.Untyped(json, ModelInfo.GetFhirTypeNameForType(dataType),
                new FhirJsonNavigatorSettings
                {
                    AllowJsonComments = true       // DSTU2, should be false in STU3
                }));
            return Parse(jsonReader, dataType);
        }

        // [WMR 20160421] Caller is responsible for disposing reader
        public Base Parse(JsonReader reader, Type dataType)
        {
            IFhirReader jsonReader = new ElementNavFhirReader(
                FhirJsonNavigator.Untyped(reader, ModelInfo.GetFhirTypeNameForType(dataType),
                new FhirJsonNavigatorSettings
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

            if (dataType.CanBeTreatedAsType(typeof(Resource)))
                return new ResourceReader(reader, Settings).Deserialize();
            else
                return new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }

        public T Parse<T>(IElementNavigator nav) where T : Base =>
            (T)Parse(new SourceNavigatorOnElementNavigator(nav), typeof(T));

        public Base Parse(IElementNavigator nav, Type dataType) =>
            Parse(new SourceNavigatorOnElementNavigator(nav), dataType);


        public T Parse<T>(ISourceNavigator nav) where T : Base => (T)Parse(nav, typeof(T));

        public Base Parse(ISourceNavigator nav, Type dataType)
        {
            if (nav == null) throw Error.ArgumentNull(nameof(nav));
            if (dataType == null) throw Error.ArgumentNull(nameof(dataType));

            var reader = new ElementNavFhirReader(nav);

            if (dataType.CanBeTreatedAsType(typeof(Resource)))
                return new ResourceReader(reader, Settings).Deserialize();
            else
                return new ComplexTypeReader(reader, Settings).Deserialize(dataType);
        }
    }

    /// <summary>
    /// This class sole reason for existence is that the BaseFhirParser accepts untyped navigators (ISourceNavigator)
    /// only. This class below simulates a ISourceNavigator on top of an IElementNavigator, basically by throwing
    /// away the type information. The parser then will re-associate this information while it parses the source
    /// into the POCO.  Not the most efficient way of doing things, but until I simplify the poco parser to work
    /// on typed navigators, this stop-gap needs to stay in place.
    /// </summary>
    internal class SourceNavigatorOnElementNavigator : ISourceNavigator, IAnnotated, IExceptionSource
    {
        private IElementNavigator _sourceNav;

        public SourceNavigatorOnElementNavigator(IElementNavigator sourceNav)
        {
            this._sourceNav = sourceNav;

            if (sourceNav is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name
        {
            get
            {
                var typeInfo = _sourceNav.GetElementDefinitionSummary();

                return typeInfo?.IsChoiceElement == true ?
                    _sourceNav.Name + _sourceNav.Type.Capitalize() : _sourceNav.Name;
            }
        }

        public string Text => _sourceNav.Value == null ? null :
            PrimitiveTypeConverter.ConvertTo<string>(_sourceNav.Value);

        public string Location => _sourceNav.Location;

        public ISourceNavigator Clone() =>
            new SourceNavigatorOnElementNavigator(_sourceNav.Clone())
            {
                ExceptionHandler = this.ExceptionHandler
            };

        public bool MoveToFirstChild(string nameFilter = null) =>
            nameFilter == null ? _sourceNav.MoveToFirstChild() :
            throw Error.NotImplemented($"This {nameof(SourceNavigatorOnElementNavigator)} shim should not be called with a name filter.");

        public bool MoveToNext(string nameFilter = null) =>
            nameFilter == null ? _sourceNav.MoveToNext() :
            throw Error.NotImplemented($"This {nameof(SourceNavigatorOnElementNavigator)} shim should not be called with a name filter.");


        IEnumerable<object> IAnnotated.Annotations(Type type)
        {
            if (type == typeof(SourceNavigatorOnElementNavigator))
                return new[] { this };
            else
                return _sourceNav.Annotations(type);
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
