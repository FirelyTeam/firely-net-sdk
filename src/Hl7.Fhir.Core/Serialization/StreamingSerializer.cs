using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class NullSerializer : StreamingSerializer
    {
        public NullSerializer(Model.Version version, Rest.SummaryType summary, IEnumerable<string> elements) :
            base(version, summary, elements)
        {}

        public bool IsDirty { get; set; }

        public override void End()
        {
            PopState();
        }

        public override void Serialize(Primitive primitive)
        {
            if (primitive != null)
            {
                if (BeginDataTypePrimitive(primitive.TypeName, true) && ValueToWrite(primitive.ObjectValue) != null)
                {
                    IsDirty = true;
                }
                primitive.SerializeElement(this);
                End();
            }
        }

        public override void Serialize(IEnumerable<Primitive> primitives)
        {
            if (primitives != null)
            {
                foreach (var primitive in primitives)
                {
                    Serialize(primitive);
                }
            }
        }

        public override void StringValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (!IsSkipping() && !ShouldSkip(name, elementVersions, summaryVersions, isRequired) && !string.IsNullOrWhiteSpace(value))
            {
                IsDirty = true;
            }
        }

        public override void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            StringValue(name, value, elementVersions, summaryVersions, isRequired);
        }
    }

    public class JsonStreamingSerializer : StreamingSerializer
    {
        public JsonStreamingSerializer(JsonWriter writer, Model.Version version, Rest.SummaryType summary = Rest.SummaryType.False, IEnumerable<string> elements = null) :
            base(version, summary, elements)
        {
            _isDirtySerializer = new NullSerializer(version, summary, elements);
            _isDirtySerializer.BeginResource("Dummy");
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Serialize(Model.Primitive primitive)
        {
            if (primitive != null)
            {
                var currentState = GetCurrentState();
                if (currentState == null)
                {
                    throw new InvalidOperationException();
                }
                var elementName = currentState.GetElementName(primitive.TypeName, isResource: false);
                if (elementName != null)
                {
                    ValuePrimitive(elementName, primitive.ObjectValue);
                    BeginDataTypePrimitive(primitive.TypeName, true);
                    primitive.SerializeElement(this);
                    End();
                }
            }
        }

        public override void Serialize(IEnumerable<Model.Primitive> primitives)
        {
            if (primitives != null && !IsSkipping())
            {
                var anyNonEmpty = false;
                var elements = new List<ElementHandling>();
                foreach (var primitive in primitives)
                {
                    var objectValue = ValueToWrite(primitive.ObjectValue);

                    _isDirtySerializer.Element("e", isRequired: true);
                    _isDirtySerializer.IsDirty = false;
                    _isDirtySerializer.BeginDataType(primitive.TypeName);
                    primitive.SerializeElement(_isDirtySerializer);
                    _isDirtySerializer.End();
                    var isEmptyElement = !_isDirtySerializer.IsDirty;
                    if (objectValue == null && isEmptyElement)
                    {
                        elements.Add(ElementHandling.Skip);
                    }
                    else
                    {
                        Y();
                        if (objectValue == null)
                        {
                            _writer.WriteNull();
                        }
                        else
                        {
                            _writer.WriteValue(objectValue);
                        }
                        if (isEmptyElement)
                        {
                            elements.Add(ElementHandling.Null);
                        }
                        else
                        {
                            anyNonEmpty = true;
                            elements.Add(ElementHandling.Serialize);
                        }
                    }
                }
                if (anyNonEmpty)
                {
                    _writer.WriteEndArray();
                    _writer.WritePropertyName("_" + ((ListState)GetCurrentState()).Name);
                    _writer.WriteStartArray();
                    Y();

                    var index = 0;
                    foreach (var primitive in primitives)
                    {
                        var element = elements[index++];
                        switch (element)
                        {
                            case ElementHandling.Null:
                                _writer.WriteNull();
                                break;
                            case ElementHandling.Skip:
                                break;
                            case ElementHandling.Serialize:
                                BeginDataTypePrimitive(primitive.TypeName, true);
                                primitive.SerializeElement(this);
                                End();
                                break;
                            default:
                                throw new InvalidOperationException($"Unknown or not supported ElementHandling '{element}'");
                        }
                    }
                }
            }
        }

        enum ElementHandling
        {
            Skip,
            Null,
            Serialize
        }

        public override void StringValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (!IsSkipping() && !ShouldSkip(name, elementVersions, summaryVersions, isRequired))
            {
                ValuePrimitive(name, value);
            }
        }

        public override void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            StringValue(name, value, elementVersions, summaryVersions, isRequired);
        }

        public override void End()
        {
            var renderedState = PopState();
            if (renderedState != null)
            {
                if (renderedState.IsArray)
                {
                    _writer.WriteEndArray();
                }
                else
                {
                    _writer.WriteEndObject();
                }
            }
        }

        private void ValuePrimitive(string elementName, object value)
        {
            var valueToWrite = ValueToWrite(value);
            if (valueToWrite != null)
            {
                Y();

                _writer.WritePropertyName(elementName);
                _writer.WriteValue(valueToWrite);
            }
        }

        private void Y()
        {
            while (TryGetNotRenderedState(out var state, out var previousState))
            {
                if (state is DataTypeState dataTypeState)
                {
                    if ((previousState == null || !previousState.IsArray) && dataTypeState.ElementName != null)
                    {
                        var propertyName = dataTypeState.HasValue ?
                            "_" + dataTypeState.ElementName :
                            dataTypeState.ElementName;
                        _writer.WritePropertyName(propertyName);
                    }
                    _writer.WriteStartObject();
                    if (dataTypeState.Type != null)
                    {
                        _writer.WritePropertyName("resourceType");
                        _writer.WriteValue(dataTypeState.Type);
                    }
                }
                else if (state is ListState listState)
                {
                    _writer.WritePropertyName(listState.Name);
                    _writer.WriteStartArray();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private readonly JsonWriter _writer;
        private readonly NullSerializer _isDirtySerializer;
    }

    public class XmlStreamingSerializer : StreamingSerializer
    {
        public XmlStreamingSerializer(XmlWriter writer, Model.Version version, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, IEnumerable<string> elements = null) :
            base(version, summary, elements)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _root = root;
        }

        public override void Serialize(Model.Primitive primitive)
        {
            if (primitive != null)
            {
                if (BeginDataTypePrimitive(primitive.TypeName, true))
                {
                    EmitValue(primitive.ObjectValue);
                }
                primitive.SerializeElement(this);
                End();
            }
        }

        public override void Serialize(IEnumerable<Model.Primitive> primitives)
        {
            if (primitives != null)
            {
                foreach (var primitive in primitives)
                {
                    Serialize(primitive);
                }
            }
        }

        public override void StringValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (!IsSkipping() && !ShouldSkip(name, elementVersions, summaryVersions, isRequired) && !string.IsNullOrWhiteSpace(value))
            {
                Y();
                _writer.WriteAttributeString(name, value);
            }
        }

        public override void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (!IsSkipping() && !ShouldSkip(name, elementVersions, summaryVersions, isRequired) && !string.IsNullOrWhiteSpace(value))
            {
                Y();
                value = value.Trim();
                if (!value.StartsWith("<"))
                {
                    value = $"<{name}>{value}</{name}>";
                }
                var addedRootElement = false;
                var firstElement = true;
                using (var xmlReader = Utility.SerializationUtil.XmlReaderFromXmlText(value))
                {
                    while (xmlReader.Read())
                    {
                        // Remove comments, processing instructions, non-significative whitespaces
                        // Put all elements in the XHTML namespace
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (firstElement)
                                {
                                    if (xmlReader.LocalName != name )
                                    {
                                        _writer.WriteStartElement(name, Utility.XmlNs.XHTML);
                                        addedRootElement = true;
                                    }
                                    firstElement = false;
                                }
                                _writer.WriteStartElement(xmlReader.LocalName, Utility.XmlNs.XHTML);
                                _writer.WriteAttributes(xmlReader, defattr: false);
                                if (xmlReader.IsEmptyElement)
                                {
                                    _writer.WriteEndElement();
                                }
                                break;
                            case XmlNodeType.Text:
                                _writer.WriteString(xmlReader.Value);
                                break;
                            case XmlNodeType.SignificantWhitespace:
                                _writer.WriteWhitespace(xmlReader.Value);
                                break;
                            case XmlNodeType.CDATA:
                                _writer.WriteCData(xmlReader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                _writer.WriteEndElement();
                                break;
                        }
                    }
                    if (addedRootElement)
                    {
                        _writer.WriteEndElement();
                    }
                }
            }
        }

        private void EmitValue(object value)
        {
            var valueToWrite = ValueToWrite(value);
            if (valueToWrite != null)
            {
                Y();
                _writer.WriteStartAttribute("value");
                _writer.WriteValue(valueToWrite);
                _writer.WriteEndAttribute();
            }
        }

        public override void End()
        {
            var renderedState = PopState();
            if (renderedState != null)
            {
                if (renderedState is DataTypeState dataTypeState)
                {
                    if (dataTypeState.ElementName != null)
                    {
                        _writer.WriteEndElement();
                    }
                    if (dataTypeState.Type != null)
                    {
                        _writer.WriteEndElement();
                    }
                }
            }
        }

        private void Y()
        {
            while (TryGetNotRenderedState(out var state, out var previousState))
            {
                if (state is DataTypeState dataTypeState)
                {
                    var outerElementName = previousState != null && previousState is ListState listState ?
                        listState.Name :
                        dataTypeState.ElementName;
                    if (outerElementName != null)
                    {
                        _writer.WriteStartElement(outerElementName, "http://hl7.org/fhir");
                    }
                    if (dataTypeState.Type != null)
                    {
                        _writer.WriteStartElement(dataTypeState.Type, "http://hl7.org/fhir");
                    }
                }
                else if (!(state is ListState))
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private readonly XmlWriter _writer;
        private readonly string _root;
    }

    public abstract class StreamingSerializer
    {
        public StreamingSerializer(Model.Version version, Hl7.Fhir.Rest.SummaryType summary, IEnumerable<string> elements)
        {
            _version = version;
            _summary = summary;
            _elements = elements == null ?
                null :
                new HashSet<string>(elements);
        }

        public void Serialize(Model.Meta meta)
        {
            var isSubsetted = _summary != Rest.SummaryType.False
                || _elements != null;
            var isBundleRoot = _states.Count == 1 && _states.Last.Value is DataTypeState dataTypeState && dataTypeState.Type == "Bundle";
            if (!isSubsetted || isBundleRoot)
            {
                meta?.Serialize(this);
            }
            else
            {
                var subsettedMeta = meta ?? new Model.Meta();
                if (!subsettedMeta.Tag.Any(t => t.System == ObservationValueSystem && t.Code == ObservationValueCodeSubsetted))
                {
                    var subsettedTag = new Model.Coding(ObservationValueSystem, ObservationValueCodeSubsetted);
                    subsettedMeta.Tag.Add(subsettedTag);
                }
                subsettedMeta.Serialize(this);
            }
        }

        public abstract void Serialize(Model.Primitive primitive);

        public abstract void Serialize(IEnumerable<Model.Primitive> primitives);

        private const string ObservationValueSystem = "http://hl7.org/fhir/v3/ObservationValue";
        private const string ObservationValueCodeSubsetted = "SUBSETTED";

        public abstract void StringValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false);

        public abstract void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false);

        public void Element(string name, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false, bool isChoice = false)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                throw new InvalidOperationException();
            }
            if (ShouldSkip(name, elementVersions, summaryVersions, isRequired))
            {
                currentState.SetElement( new SkipElement() );
            }
            else
            {
                currentState.SetElement( new ActualElement { Name = name, IsChoice = isChoice } );
            }
        }

        public void BeginList(string name, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (IsSkipping() || ShouldSkip(name, elementVersions, summaryVersions, isRequired))
            {
                PushState(new SkipState());
            }
            else
            {
                PushState(new ListState { Name = name });
            }
        }

        public void BeginDataType(string type)
        {
            BeginDataTypePrimitive(type, false);
        }

        public void BeginResource(string type)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                PushState(new DataTypeState { Type = type });
            }
            else
            {
                var elementName = currentState.GetElementName(type, isResource: true);
                if (elementName == null)
                {
                    PushState(new SkipState());
                }
                else
                {
                    PushState(new DataTypeState { ElementName = elementName, Type = type });
                }
            }
        }

        protected bool BeginDataTypePrimitive(string type, bool hasValue)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                throw new InvalidOperationException();
            }

            var elementName = currentState.GetElementName(type, isResource: false);
            if (elementName == null)
            {
                PushState(new SkipState());
                return false;
            }

            PushState(new DataTypeState { ElementName = elementName, HasValue = hasValue });
            return true;
        }

        public abstract void End();

        protected bool IsSkipping()
        {
            return _states.Count > 0
                && _states.Last.Value is SkipState;
        }

        protected bool ShouldSkip(string name, Model.Version elementVersions, Model.Version summaryVersions, bool isRequired)
        {
            if ((elementVersions & _version) == 0)
            {
                return true;
            }
            switch (_summary)
            {
                case Rest.SummaryType.False:
                    return !isRequired
                        && _elements != null
                        && _states.Count <= 1
                        && _elements.Contains(name);
                case Rest.SummaryType.Data:
                    return !isRequired
                        && name == "text";
                case Rest.SummaryType.True:
                    return (summaryVersions & _version) == 0
                        && !isRequired;
                case Rest.SummaryType.Text:
                    return !isRequired
                        && name != "text";
                case Rest.SummaryType.Count:
                    return true;
                default:
                    throw new InvalidOperationException($"Unknown or not supported summary type '{_summary}'");
            }
        }

        protected static object ValueToWrite(object value)
        {
            if (value is string stringValueToTrim)
            {
                if (string.IsNullOrWhiteSpace(stringValueToTrim))
                {
                    return null;
                }
                return stringValueToTrim.Trim();
            }

            return value;
        }

        protected IState GetCurrentState()
        {
            if (_states.Count == 0)
            {
                return null;
            }
            return _states.Last.Value;
        }

        private void PushState(IState state)
        {
            _states.AddLast(state);
            if (_notRenderedNode == null)
            {
                _notRenderedNode = _states.Last;
            }
        }

        protected IState PopState()
        {
            if (_states.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var result = _notRenderedNode == null ?
                _states.Last.Value :
                null;
            if (_notRenderedNode == _states.Last)
            {
                _notRenderedNode = null;
            }
            _states.RemoveLast();
            return result;
        }

        protected bool TryGetNotRenderedState(out IState state, out IState previousState)
        {
            if (_notRenderedNode == null)
            {
                state = null;
                previousState = null;
                return false;
            }
            state = _notRenderedNode.Value;
            previousState = _notRenderedNode.Previous?.Value;
            _notRenderedNode = _notRenderedNode.Next;
            return true;
        }

        protected interface IState
        {
            bool IsArray { get; }
            string GetElementName(string type, bool isResource);
            void SetElement(IElement element);
        }

        protected class SkipState : IState
        {
            public bool IsArray => false;

            public string GetElementName(string type, bool isResource) => null;

            public void SetElement(IElement element)
            {
                // Do nothing - we are already skipping
            }
        }

        protected class ListState : IState
        {
            public bool IsArray => true;

            public string Name { get; set; }

            public string GetElementName(string type, bool isResource) => Name;

            public void SetElement(IElement element)
            {
                throw new InvalidOperationException();
            }
        }

        protected class DataTypeState : IState
        {
            public string ElementName { get; set; }
            public string Type { get; set; }
            public bool HasValue { get; set; }

            public bool IsArray => false;

            public string GetElementName(string type, bool isResource)
            {
                if (_element is SkipElement) return null;

                if (_element is ActualElement actual)
                {
                    if (isResource)
                    {
                        if (actual.IsChoice)
                        {
                            throw new InvalidOperationException();
                        }
                        return actual.Name;
                    }
                    return actual.GetElementName(type);
                }

                throw new InvalidOperationException();
            }

            public void SetElement(IElement element)
            {
                _element = element ?? throw new ArgumentNullException(nameof(element));
            }

            private IElement _element;
        }

        protected interface IElement
        {
        }

        protected class SkipElement : IElement
        {
        }

        protected class ActualElement : IElement
        {
            public string Name { get; set; }
            public bool IsChoice { get; set; }

            public string GetElementName(string type)
            {
                if (IsChoice) return Name + type.Substring(0, 1).ToUpperInvariant() + type.Substring(1);
                return Name;
            }
        }

        private readonly Model.Version _version;
        private readonly Hl7.Fhir.Rest.SummaryType _summary;
        private readonly HashSet<string> _elements;
        private readonly LinkedList<IState> _states = new LinkedList<IState>();
        private LinkedListNode<IState> _notRenderedNode = null;
    }

    public class FhirJsonStreamingSerializer : BaseFhirSerializer
    {
        public FhirJsonStreamingSerializer(Model.Version version) : base(version)
        {
        }

        public FhirJsonStreamingSerializer(SerializerSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null) =>
            Utility.SerializationUtil.WriteJsonToString(jsonWriter => Serialize(instance, jsonWriter, summary, elements), Settings.Pretty);

        public byte[] SerializeToBytes(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null) =>
            Utility.SerializationUtil.WriteJsonToBytes(jsonWriter => Serialize(instance, jsonWriter, summary, elements));

        public void Serialize(Base instance, JsonWriter writer, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            var serializer = new JsonStreamingSerializer(writer, Settings.Version, summary, elements);
            instance.Serialize(serializer);
        }
    }

    public class FhirXmlStreamingSerializer : BaseFhirSerializer
    {
        public FhirXmlStreamingSerializer(Model.Version version) : base(version)
        {
        }

        public FhirXmlStreamingSerializer(SerializerSettings settings) : base(settings)
        {
        }


        public string SerializeToString(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToString(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements), Settings.Pretty);

        public byte[] SerializeToBytes(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToBytes(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements));

        public XDocument SerializeToDocument(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToDocument(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements));

        public void Serialize(Base instance, XmlWriter writer, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            var serializer = new XmlStreamingSerializer(writer, Settings.Version, summary, root, elements);
            instance.Serialize(serializer);
        }
    }
}
