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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlWriterSettings
    {
        public bool AllowUntypedElements;
        public bool IncludeUntypedElements;
    }

    public static class FhirXmlWriterExtensions
    {
        public static void WriteTo(this ISourceNode source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
            new FhirXmlWriter(settings).Write(source, destination, rootName);

        public static void WriteTo(this IElementNode source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
            new FhirXmlWriter(settings).Write(source, destination, rootName);

        public static void WriteTo(this IElementNavigator source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
             source.ToElementNode().WriteTo(destination, settings,rootName);

        public static string ToXml(this ISourceNode source, FhirXmlWriterSettings settings = null, string rootName = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName));

        public static string ToXml(this IElementNode source, FhirXmlWriterSettings settings = null, string rootName = null)
                => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName));

        public static string ToXml(this IElementNavigator source, FhirXmlWriterSettings settings = null, string rootName = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName));

        public static byte[] ToXmlBytes(this IElementNode source, FhirXmlWriterSettings settings = null, string rootName = null)
                => SerializationUtil.WriteXmlToBytes(writer => source.WriteTo(writer, settings, rootName));
    }

    public class FhirXmlWriter : IExceptionSource
    {
        public FhirXmlWriter(FhirXmlWriterSettings settings = null)
        {
            AllowUntypedElements = settings?.AllowUntypedElements ?? false;
            IncludeUntypedElements = settings?.IncludeUntypedElements ?? false;
        }

        public bool AllowUntypedElements;
        public bool IncludeUntypedElements;
        
        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public void Write(IElementNode source, XmlWriter destination, string rootName = null)
        {
            //Re-enable when the PocoNavigator is also fed through the TypedNavigator
            //if(!source.InPipeline(typeof(TypedNavigator)))
            //    throw Error.NotSupported($"The {nameof(FhirXmlWriter)} requires a {nameof(TypedNavigator)} to be present in the pipeline.");
            writeInternal(source, destination, rootName);
        }

        private void writeInternal(IElementNode source, XmlWriter destination, string rootName = null)
        {
            var dest = new XDocument();

            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => ExceptionHandler.NotifyOrThrow(o, a)))
                {
                    write(source, dest, rootName);
                }
            }
            else
                write(source, dest, rootName);

            if (dest.Root != null)
            {
                // The name of the root node can be overidden - if so, change the name of the root element
                if (rootName != null)
                    dest.Root.Name = XName.Get(rootName, dest.Root.Name.Namespace.NamespaceName);

                dest.WriteTo(destination);
            }

            destination.Flush();
        }

        public void Write(ISourceNode source, XmlWriter destination, string rootName = null)
        {
            bool hasXmlSource = source.InPipeline(typeof(FhirXmlNode));

            // We can only work with an untyped source if we're doing a roundtrip,
            // so we have all serialization details available.
            if (hasXmlSource)
                writeInternal(source.ToElementNode(), destination, rootName);
            else
                throw Error.NotSupported($"The {nameof(FhirXmlWriter)} will only work correctly on an untyped " +
                    $"source if the source is a {nameof(FhirXmlNavigator)}.");
        }

        internal bool MustSerializeMember(IElementNode source, out ElementDefinitionSummary info)
        {
            info = source.GetElementDefinitionSummary();

            if (info == null && !AllowUntypedElements)
            {
                var message = $"Element '{source.Location}' is missing type information.";
                if (IncludeUntypedElements)
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Warning(
                        new MissingTypeInformationException(message)));
                    return true;
                }
                else
                {
                    ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Error(
                        new MissingTypeInformationException(message)));
                    return false;
                }
            }

            return true;
        }

        private void write(IElementNode source, XContainer parent, string rootName = null)
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
            var isXhtml = source.Type == "xhtml" ||
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
                            source.Name + source.Type.Capitalize() : source.Name;

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
                                            source.Type : source.GetResourceType());

            XElement containedResource = null;
            if (containedResourceType != null)
                containedResource = new XElement(XName.Get(containedResourceType, ns));

            var childParent = containedResource ?? me;

            // Now, do the same for the children
            // xml requires a certain order, so let's make sure we serialize in the right order
            var orderedChildren = source.Children().OrderBy(c => c.GetElementDefinitionSummary()?.Order ?? 0);

            foreach (var child in orderedChildren)
                write(child, childParent);

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
