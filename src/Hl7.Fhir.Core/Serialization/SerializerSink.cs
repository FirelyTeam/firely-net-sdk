using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    public class SerializerSinkException : Exception
    {
        public SerializerSinkException(string message) : base(message)
        {}
    }

    internal abstract class SerializerSink
    {
        public SerializerSink(Model.Version version, Rest.SummaryType summary, IEnumerable<string> elements)
        {
            _version = version;
            _summary = summary;
            _elements = elements == null ?
                null :
                new HashSet<string>(elements);
        }

        /// <summary>
        /// Serialize a Meta data type value - requires its own special method to output the SUBSETTED tags when summarizing
        /// </summary>
        /// <param name="meta">The Meta value to serialize</param>
        public void Serialize(Meta meta)
        {
            var isSubsetted = _summary != Rest.SummaryType.False
                || _elements != null;
            var isBundleRoot = _states.Count == 1 
                && _states.Last.Value is DataTypeState dataTypeState 
                && dataTypeState.Type == "Bundle";
            if (!isSubsetted || isBundleRoot)
            {
                meta?.Serialize(this);
            }
            else
            {
                var observationValueSystem = _version == Model.Version.DSTU2 || _version == Model.Version.STU3 ?
                    OldObservationValueSystem :
                    ObservationValueSystem;
                var subsettedMeta = meta ?? new Meta();
                if (!subsettedMeta.Tag.Any(t => t.System == observationValueSystem && t.Code == ObservationValueCodeSubsetted))
                {
                    var subsettedTag = new Coding(observationValueSystem, ObservationValueCodeSubsetted);
                    subsettedMeta.Tag.Add(subsettedTag);
                }
                subsettedMeta.Serialize(this);
            }
        }

        /// <summary>
        /// Describe an element within a resource or data type - it is followed by a call to BeginDataType or BeginResource or Serialize(Primitive)
        /// with the element content
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="elementVersions">FHIR versions the element belongs to</param>
        /// <param name="summaryVersions">FHIR versions for which the element is part of the summary</param>
        /// <param name="isRequired">True if the element is required - ie min. cardinality 1</param>
        /// <param name="isChoice">True if the element is a type choice one - ie supporting values of different type - eg. Observation.value[x].
        /// Note that is such cases the name is just the prefix part - eg value</param>
        public void Element(string name, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false, bool isChoice = false)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                throw new SerializerSinkException("Misssing call to BeginResource(), BeginDataType() or BeginList()");
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

        /// <summary>
        /// Begin of a data type - eg HumanName
        /// </summary>
        /// <param name="type">The data type</param>
        public void BeginDataType(string type)
        {
            BeginDataTypePrimitive(type, false);
        }

        /// <summary>
        /// Begin of a resource - eg Patient
        /// </summary>
        /// <param name="type">The resource type</param>
        public void BeginResource(string type)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                PushState(new DataTypeState(type, name: null));
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
                    PushState(new DataTypeState(type, elementName));
                }
            }
        }

        /// <summary>
        /// Begin of a list - eg Patient.name
        /// </summary>
        /// <param name="name">Name of the list element</param>
        /// <param name="elementVersions">FHIR versions the list element belongs to</param>
        /// <param name="summaryVersions">FHIR versions for which the list element is part of the summary</param>
        /// <param name="isRequired">True if the list element is required - ie min. cardinality 1</param>
        public void BeginList(string name, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (IsSkipping() || ShouldSkip(name, elementVersions, summaryVersions, isRequired))
            {
                PushState(new SkipState());
            }
            else
            {
                PushState(new ListState(name));
            }
        }

        /// <summary>
        /// Special elements with string value - used for Extension.url and Element.id that have special handling in XML
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="value">Value of the element</param>
        /// <param name="elementVersions">FHIR versions the element belongs to</param>
        /// <param name="summaryVersions">FHIR versions for which the element is part of the summary</param>
        /// <param name="isRequired">True if the element is required - ie min. cardinality 1</param>
        public abstract void StringValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false);

        /// <summary>
        /// Special elements with XHTML value - used for Narrative.div that has special handling in XML
        /// </summary>
        /// <param name="name">Name of the element</param>
        /// <param name="value">Value of the element as a valid XHTML string</param>
        /// <param name="elementVersions">FHIR versions the element belongs to</param>
        /// <param name="summaryVersions">FHIR versions for which the element is part of the summary</param>
        /// <param name="isRequired">True if the element is required - ie min. cardinality 1</param>
        public abstract void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false);

        /// <summary>
        /// Primitive data type value - with a value and optional (sub)elements. Handled differently between JSON and XML
        /// </summary>
        /// <param name="primitive">The primitive data type value</param>
        public abstract void Serialize(Primitive primitive);

        /// <summary>
        /// List of primitive data type values - 
        /// handled specially in JSON so it requires it own separate method instead of simply calling Serialize(Primitive) in a loop
        /// </summary>
        /// <param name="primitives">The primitive data type values</param>
        public abstract void Serialize(IEnumerable<Primitive> primitives);

        /// <summary>
        /// End of a resource, data type or list
        /// </summary>
        public void End()
        {
            if (_states.Count == 0)
            {
                throw new SerializerSinkException("Misssing call to BeginResource(), BeginDataType() or BeginList()");
            }

            if (_states.Count == 1 && _notRenderedNode != null)
            {
                // No empty output
                RenderStates();
            }

            var renderedState = _notRenderedNode == null ?
                _states.Last.Value :
                null;
            if (_notRenderedNode == _states.Last)
            {
                _notRenderedNode = null;
            }
            _states.RemoveLast();

            if (renderedState != null)
            {
                RenderEndState(renderedState);
            }
        }

        protected bool BeginDataTypePrimitive(string type, bool isPrimitiveType)
        {
            var currentState = GetCurrentState();
            if (currentState == null)
            {
                if (isPrimitiveType)
                {
                    throw new SerializerSinkException("Primitive data type cannot be the root");
                }
                // Special case: a data type as the root - we use the type as the element name
                PushState(new DataTypeState(type, isPrimitiveType: false));
                return true;
            }

            var elementName = currentState.GetElementName(type, isResource: false);
            if (elementName == null)
            {
                PushState(new SkipState());
                return false;
            }

            PushState(new DataTypeState(elementName, isPrimitiveType));
            return true;
        }

        protected void RenderStates()
        {
            while (_notRenderedNode != null)
            {
                RenderBeginState(_notRenderedNode.Value, _notRenderedNode.Previous?.Value);
                _notRenderedNode = _notRenderedNode.Next;
            }
        }

        protected abstract void RenderBeginState(IState state, IState previousState);

        protected abstract void RenderEndState(IState renderedState);

        protected bool IsSkipping()
        {
            return _states.Last?.Value is SkipState;
        }

        /// <summary>
        /// Check if the element should be skipped (no output) - based on the element FHIR versions and the summarization settings
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="elementVersions">FHIR versions this element applies to</param>
        /// <param name="summaryVersions">FHIR versions in which the element is part of the summary</param>
        /// <param name="isRequired">True if the element is a required one (min. cardinality 1)</param>
        /// <returns>True if the element should be skipped</returns>
        protected bool ShouldSkip(string name, Model.Version elementVersions, Model.Version summaryVersions, bool isRequired)
        {
            if ((elementVersions & _version) == 0)
            {
                return true;
            }
            switch (_summary)
            {
                case Rest.SummaryType.False:
                    return _elements != null
                        && !_elements.Contains(name)
                        && IsResourceElement();
                case Rest.SummaryType.Data:
                    return !isRequired
                        && name == "text"
                        && IsResourceElement();
                case Rest.SummaryType.True:
                    return (summaryVersions & _version) == 0
                        && !isRequired;
                case Rest.SummaryType.Text:
                    return !isRequired
                        && !(name == "id" || name == "meta" || name == "text")
                        && IsResourceElement();
                case Rest.SummaryType.Count:
                    return true;
                default:
                    throw new InvalidOperationException($"Unknown or not supported summary type '{_summary}'");
            }

            bool IsResourceElement() =>
                _states.Last?.Value is DataTypeState dataTypeState
                && dataTypeState.Type != null;
        }

        /// <summary>
        /// Computes the value that should be written to the output - checking for empty values and trimming spaces as needed
        /// </summary>
        /// <param name="value">Value to process</param>
        /// <returns>Value to write - null if nothing should be written</returns>
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
            return _states.Last?.Value;
        }

        private void PushState(IState state)
        {
            _states.AddLast(state);
            if (_notRenderedNode == null)
            {
                _notRenderedNode = _states.Last;
            }
        }

        protected interface IState
        {
            string Name { get; }
            string GetElementName(string type, bool isResource);
            void SetElement(IElement element);
        }

        /// <summary>
        /// We are in an element that is being skipped 
        /// (either because belongs to a diffrent FHIR version or is being removed due to summarization)
        /// </summary>
        protected class SkipState : IState
        {
            public string Name => null;

            public string GetElementName(string type, bool isResource) => null;

            public void SetElement(IElement element)
            {
                // Do nothing - we are already skipping
            }
        }

        /// <summary>
        /// We are in a list - ie element with max cardinality > 1
        /// </summary>
        protected class ListState : IState
        {
            public ListState(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }

            public string Name { get; }

            public string GetElementName(string type, bool isResource) => Name;

            public void SetElement(IElement element)
            {
                throw new SerializerSinkException("Misssing call to BeginResource() or BeginDataType()");
            }
        }

        /// <summary>
        /// We are in a resource or data type
        /// </summary>
        protected class DataTypeState : IState
        {
            /// <summary>
            /// A resource
            /// </summary>
            /// <param name="type">The resource type</param>
            /// <param name="name">The name of the (optional) element containing the resoure - eg set to 'resource' in a Bundle.entry. 
            /// Null if the resource is at the root or in a list (eg DomainResource.contained)</param>
            public DataTypeState(string type, string name)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Name = name;
                IsPrimitiveType = false;
            }

            /// <summary>
            /// A data type
            /// </summary>
            /// <param name="name">The name of the element containng the data type</param>
            /// <param name="isPrimitiveType">True if it is a primitive data type</param>
            public DataTypeState(string name, bool isPrimitiveType)
            {
                Type = null;
                Name = name ?? throw new ArgumentNullException(nameof(name));
                IsPrimitiveType = isPrimitiveType;
            }

            public string Name { get; }
            public string Type { get; }
            public bool IsPrimitiveType { get; }

            public string GetElementName(string type, bool isResource)
            {
                if (_element is SkipElement) return null;

                if (_element is ActualElement actual)
                {
                    if (isResource)
                    {
                        if (actual.IsChoice)
                        {
                            throw new SerializerSinkException("Choice elements cannot be followed by BeginResource()");
                        }
                        return actual.Name;
                    }
                    return actual.GetElementName(type);
                }

                throw new SerializerSinkException($"Unexpected element {_element?.GetType()}");
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

        private const string OldObservationValueSystem = "http://hl7.org/fhir/v3/ObservationValue";
        private const string ObservationValueSystem = "http://terminology.hl7.org/CodeSystem/v3-ObservationValue";
        private const string ObservationValueCodeSubsetted = "SUBSETTED";

        private readonly Model.Version _version;
        private readonly Rest.SummaryType _summary;
        private readonly HashSet<string> _elements;

        private readonly LinkedList<IState> _states = new LinkedList<IState>();
        private LinkedListNode<IState> _notRenderedNode = null;
    }

    /// <summary>
    /// Sink that generates no output, used to determine if any output would be generated
    /// </summary>
    internal class NullSerializerSink : SerializerSink
    {
        public NullSerializerSink(Model.Version version, Rest.SummaryType summary, IEnumerable<string> elements) :
            base(version, summary, elements)
        { }

        public bool IsDirty { get; set; }

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

        protected override void RenderBeginState(IState state, IState previousState)
        {
            // Nothing to do
        }

        protected override void RenderEndState(IState renderedState)
        {
            // Nothing to do
        }
    }

    /// <summary>
    /// Sink generating JSON output - via a JsonWriter
    /// </summary>
    internal class JsonSerializerSink : SerializerSink
    {
        public JsonSerializerSink(JsonWriter writer, Model.Version version, Rest.SummaryType summary = Rest.SummaryType.False, IEnumerable<string> elements = null) :
            base(version, summary, elements)
        {
            _nullSink = new NullSerializerSink(version, summary, elements);
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Serialize(Primitive primitive)
        {
            if (primitive != null)
            {
                var currentState = GetCurrentState();
                if (currentState == null)
                {
                    throw new SerializerSinkException("Misssing call to BeginResource(), BeginDataType() or BeginList()");
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

        public override void Serialize(IEnumerable<Primitive> primitives)
        {
            if (primitives != null && !IsSkipping())
            {
                var anyNonEmpty = false;
                var elements = new List<ElementHandling>();
                foreach (var primitive in primitives)
                {
                    var objectValue = ValueToWrite(primitive.ObjectValue);
                    var noElement = !HasElement(primitive);
                    if (objectValue == null && noElement)
                    {
                        elements.Add(ElementHandling.Skip);
                    }
                    else
                    {
                        RenderStates();
                        if (objectValue == null)
                        {
                            _writer.WriteNull();
                        }
                        else
                        {
                            _writer.WriteValue(objectValue);
                        }
                        if (noElement)
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
                    _writer.WritePropertyName(PropertyName(GetCurrentState().Name, isExtension: true));
                    _writer.WriteStartArray();

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

        protected override void RenderBeginState(IState state, IState previousState)
        {
            if (state is DataTypeState dataTypeState)
            {
                if (previousState != null && !(previousState is ListState) && dataTypeState.Name != null)
                {
                    _writer.WritePropertyName(PropertyName(dataTypeState.Name, dataTypeState.IsPrimitiveType));
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
                throw new SerializerSinkException($"Unexpected state {state.GetType()}");
            }
        }

        protected override void RenderEndState(IState renderedState)
        {
            if (renderedState is ListState)
            {
                _writer.WriteEndArray();
            }
            else
            {
                _writer.WriteEndObject();
            }
        }

        /// <summary>
        /// Checks if the primitive data type has non-empty element id or extensions 
        /// </summary>
        private bool HasElement(Primitive primitive)
        {
            _nullSink.IsDirty = false;
            _nullSink.BeginDataType(primitive.TypeName);
            primitive.SerializeElement(_nullSink);
            _nullSink.End();
            return _nullSink.IsDirty;
        }

        private string PropertyName(string name, bool isExtension)
        {
            return isExtension ?
                "_" + name :
                name;
        }

        private void ValuePrimitive(string propertyName, object value)
        {
            var valueToWrite = ValueToWrite(value);
            if (valueToWrite != null)
            {
                RenderStates();

                _writer.WritePropertyName(propertyName);
                _writer.WriteValue(valueToWrite);
            }
        }

        private enum ElementHandling
        {
            Skip,
            Null,
            Serialize
        }

        private readonly JsonWriter _writer;
        private readonly NullSerializerSink _nullSink;
    }

    /// <summary>
    /// Sink generating XML output - via a XmlWriter
    /// </summary>
    internal class XmlSerializerSink : SerializerSink
    {
        public XmlSerializerSink(XmlWriter writer, Model.Version version, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, IEnumerable<string> elements = null) :
            base(version, summary, elements)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _root = root;
        }

        public override void Serialize(Primitive primitive)
        {
            if (primitive != null)
            {
                if (BeginDataTypePrimitive(primitive.TypeName, true))
                {
                    var valueToWrite = ValueToWrite(primitive.ObjectValue);
                    if (valueToWrite != null)
                    {
                        RenderStates();
                        _writer.WriteStartAttribute("value");
                        _writer.WriteValue(valueToWrite);
                        _writer.WriteEndAttribute();
                    }
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
                RenderStates();
                _writer.WriteAttributeString(name, value.Trim());
            }
        }

        public override void XhtmlValue(string name, string value, Model.Version elementVersions = Model.Version.All, Model.Version summaryVersions = Model.Version.All, bool isRequired = false)
        {
            if (!IsSkipping() && !ShouldSkip(name, elementVersions, summaryVersions, isRequired) && !string.IsNullOrWhiteSpace(value))
            {
                RenderStates();
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
                                    if (xmlReader.LocalName != name)
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

        protected override void RenderBeginState(IState state, IState previousState)
        {
            if (state is DataTypeState dataTypeState)
            {
                var outerElementName = previousState != null && previousState is ListState listState ?
                    listState.Name :
                    dataTypeState.Name;
                if (outerElementName != null)
                {
                    WriteStartElement(outerElementName);
                }
                if (dataTypeState.Type != null)
                {
                    WriteStartElement(dataTypeState.Type);
                }
            }
            else if (!(state is ListState))
            {
                throw new SerializerSinkException($"Unexpected state {state.GetType()}");
            }
        }

        protected override void RenderEndState(IState renderedState)
        {
            if (renderedState is DataTypeState dataTypeState)
            {
                if (dataTypeState.Name != null)
                {
                    _writer.WriteEndElement();
                }
                if (dataTypeState.Type != null)
                {
                    _writer.WriteEndElement();
                }
            }
        }

        private void WriteStartElement(string localName)
        {
            if (_root != null)
            {
                localName = _root;
                _root = null;
            }
            _writer.WriteStartElement(localName, "http://hl7.org/fhir");
        }

        private readonly XmlWriter _writer;
        private string _root;
    }
}
