/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class FhirXmlBuilder : IExceptionSource
    {
        internal FhirXmlBuilder(FhirXmlSerializationSettings settings = null)
        {
            _settings = settings?.Clone() ?? new FhirXmlSerializationSettings();
        }

        private FhirXmlSerializationSettings _settings;
        private bool _roundtripMode;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public XDocument Build(ITypedElement source) =>
            buildInternal(source);

        public XDocument Build(ISourceNode source)
        {
            bool hasXmlSource = source.Annotation<FhirXmlNode>() != null;

            // We can only work with an untyped source if we're doing a roundtrip,
            // so we have all serialization details available.
            if (hasXmlSource)
            {
                _roundtripMode = true;
#pragma warning disable CS0618 // Type or member is obsolete
                return buildInternal(source.ToTypedElement());
#pragma warning restore CS0618 // Type or member is obsolete
            }
            else
            {
                throw Error.NotSupported($"The {nameof(FhirXmlBuilder)} will only work correctly on an untyped " +
                    $"source if the source is a {nameof(FhirXmlNode)}.");
            }
        }

        public XDocument buildInternal(ITypedElement source)
        {
            var dest = new XDocument();

            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => ExceptionHandler.NotifyOrThrow(o, a)))
                {
                    build(source, dest);
                }
            }
            else
                build(source, dest);

            return dest;
        }

        internal bool MustSerializeMember(ITypedElement source, out IElementDefinitionSummary info)
        {
            info = source.Definition;

            if (info == null && !_roundtripMode)
            {
                var message = $"Element '{source.Location}' is missing type information.";

                if (_settings.SkipUnknownElements)
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Warning(
                        new MissingTypeInformationException(message)));
                }
                else
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Error(
                        new MissingTypeInformationException(message)));
                }

                return false;
            }

            return true;
        }

        private void build(ITypedElement source, XContainer parent)
        {
            var xmlDetails = source.GetXmlSerializationDetails();
            var sourceComments = (source as IAnnotated)?.Annotation<SourceComments>();

            if (!MustSerializeMember(source, out var serializationInfo)) return;
            bool hasTypeInfo = serializationInfo != null;

            var value = source.Value != null ?
                PrimitiveTypeConverter.ConvertTo<string>(source.Value) : null;

            // xhtml children require special treament:
            // - They don't use an xml "value" attribute to represent the value, instead their Value is inserted verbatim into the parent
            // - They cannot have child nodes - the "Value" on the node contains all children as raw xml text
            var isXhtml = source.InstanceType == "xhtml" ||
                serializationInfo?.Representation == XmlRepresentation.XHtml ||
                xmlDetails?.Namespace?.GetName("div") == XmlNs.XHTMLDIV;

            if (isXhtml && !String.IsNullOrWhiteSpace(value))
            {
                // The value *should* be valid xhtml - however if people just provide a plain
                // string, lets add a <div> around it.
                if (!value.TrimStart().StartsWith("<div"))
                    value = $"<div xmlns='http://www.w3.org/1999/xhtml'>{value}</div>";

                var sanitized = SerializationUtil.SanitizeXml(value);
                XElement xe = XElement.Parse(sanitized);

                // The div should be in the XHTMLNS namespace, correct if it 
                // is not the case.
                xe.Name = XmlNs.XHTMLNS + xe.Name.LocalName;
                parent.Add(xe);

                //using (var divWriter = parent.CreateWriter())
                //using (var nodeReader = SerializationUtil.XmlReaderFromXmlText(value))
                //    divWriter.WriteNode(nodeReader, false);
                return;
            }

            var usesAttribute = serializationInfo?.Representation == XmlRepresentation.XmlAttr ||
                                (xmlDetails?.NodeType == XmlNodeType.Attribute);
            var ns = serializationInfo?.NonDefaultNamespace ??
                            xmlDetails?.Namespace.NamespaceName ??
                            (usesAttribute ? "" : XmlNs.FHIR);
            bool atRoot = parent is XDocument;
            var localName = serializationInfo?.IsChoiceElement == true ?
                            source.Name + source.InstanceType.Capitalize() : source.Name;

            // If the node is represented by an attribute (e.g. an "id" child), write
            // an attribute with the child's name + the child's Value into the parent
            if (usesAttribute && !String.IsNullOrWhiteSpace(value) && !atRoot)
            {
                parent.Add(new XAttribute(XName.Get(localName, ns), value));
                return;
            }
            // else: fall through - value will be serialized as an element

            var me = new XElement(XName.Get(localName, ns));

            if (xmlDetails?.SchemaLocation != null)
                me.Add(new XAttribute(XmlNs.XSCHEMALOCATION, xmlDetails.SchemaLocation));

            // If the node has a value, add the standard FHIR value attribute
            if (value != null)
                me.Add(new XAttribute("value", value));

            // If this needs to be serialized as a contained resource, do so
            var containedResourceType = atRoot ? null :
                            (serializationInfo?.IsResource == true ?
                                            source.InstanceType :
                                            source.Annotation<IResourceTypeSupplier>()?.ResourceType);

            XElement containedResource = null;
            if (containedResourceType != null)
                containedResource = new XElement(XName.Get(containedResourceType, ns));

            var childParent = containedResource ?? me;

            // Now, do the same for the children
            // xml requires a certain order, so let's make sure we serialize in the right order
            var orderedChildren = source.Children().OrderBy(c => c.Definition?.Order ?? 0);

            foreach (var child in orderedChildren)
                build(child, childParent);

            if (serializationInfo?.Representation == XmlRepresentation.XmlText || xmlDetails?.NodeText != null)
            {
                childParent.Add(new XText(value ?? xmlDetails.NodeText));
            }

            if (sourceComments?.ClosingComments != null)
                writeComments(sourceComments.ClosingComments, me);

            // Only really add this contained resource to me when it has contents
            if (containedResource != null && (containedResource.HasAttributes || containedResource.HasElements))
                me.Add(containedResource);

            // Only add myself to my parent if I have content, or I am the root
            if (value != null || me.HasElements || atRoot)
            {
                if (sourceComments?.CommentsBefore != null)
                    writeComments(sourceComments.CommentsBefore, parent);

                parent.Add(me);
            }

            if (atRoot && parent.Elements().Any() && sourceComments?.DocumentEndComments != null)
                writeComments(sourceComments.DocumentEndComments, parent);
        }

        private void writeComments(string[] comments, XContainer parent)
        {
            if (comments?.Any() == true)
            {
                foreach (var comment in comments)
                    parent.Add(new XComment(comment));
            }
        }
    }
}
