/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.ElementModel;
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
        public bool IgnoreSourceXmlDetails;
        public bool AllowUntypedSource;
        public bool IncludeUntypedElements;
        public IExceptionSink Sink;
    }

    public static class FhirXmlWriterExtensions
    {
        public static void WriteTo(this IElementNavigator source, XmlWriter destination, FhirXmlWriterSettings settings = null) =>
            new FhirXmlWriter(settings).Write(source, destination);

        public static string ToXml(this IElementNavigator source, FhirXmlWriterSettings settings = null)
                => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings));

        public static byte[] ToXmlBytes(this IElementNavigator source, FhirXmlWriterSettings settings = null)
                => SerializationUtil.WriteXmlToBytes(writer => source.WriteTo(writer, settings));
    }

    public class FhirXmlWriter : IExceptionSource, IExceptionSink
    {
        public bool IgnoreSourceXmlDetails;
        public bool AllowUntypedSource;
        public bool IncludeUntypedMembers;

        public FhirXmlWriter(FhirXmlWriterSettings settings = null)
        {
            IgnoreSourceXmlDetails = settings?.IgnoreSourceXmlDetails ?? false;
            AllowUntypedSource = settings?.AllowUntypedSource ?? false;
            IncludeUntypedMembers = settings?.IncludeUntypedElements ?? false;
            Sink = settings?.Sink;
        }

        public void Write(IElementNavigator source, XmlWriter destination)
        {
            var settings = new XmlWriterSettings { NewLineHandling = NewLineHandling.Entitize };
            var xw = XmlWriter.Create(destination, settings);

            var dest = new XDocument();

            if (source is IExceptionSource)
            {
                using (source.Catch((o, a) => Sink.NotifyOrThrow(o, a)))
                {
                    write(source, dest);
                }
            }
            else
                write(source, dest);

            dest.WriteTo(xw);
            xw.Flush();
        }

        public void Write(ISourceNavigator source, XmlWriter destination)
             => Write(source.AsElementNavigator(), destination);

        private void write(IElementNavigator source, XContainer parent)
        {
            var xmlDetails = IgnoreSourceXmlDetails ? null : source.GetXmlSerializationDetails();
            var sourceComments = (source as IAnnotated)?.Annotation<SourceComments>();
            var serializationInfo = source.GetSerializationInfo();

            if (serializationInfo == null && !AllowUntypedSource)
                throw Error.NotSupported("The FhirXmlWriter does not work correctly with an untyped IElementNavigator source. Use the 'AllowUntypedSource' setting to override this behaviour and proceed anyway.");

            var hasTypeInfo = serializationInfo != null && serializationInfo != ElementSerializationInfo.NO_SERIALIZATION_INFO;

            if (!AllowUntypedSource && !hasTypeInfo)
            {
                var message = $"Element '{source.Location}' is missing type information.";
                if (IncludeUntypedMembers)
                {
                    Notify(source, ExceptionNotification.Warning(
                        new MissingTypeInformationException(message)));
                    // fall through, to include the untyped member
                }
                else
                {
                    Notify(source, ExceptionNotification.Error(
                        new MissingTypeInformationException(message)));
                    return;
                }
            }

            var value = source.Value != null ?
                PrimitiveTypeConverter.ConvertTo<string>(source.Value) : null;

            // xhtml children require special treament:
            // - They don't use an xml "value" attribute to represent the value, instead their Value is inserted verbatim into the parent
            // - They cannot have child nodes - the "Value" on the node contains all children as raw xml text
            var isXhtml = xmlDetails?.Namespace?.GetName("div") == XmlNs.XHTMLDIV ||
                (hasTypeInfo && source.Type == "xhtml");

            if (isXhtml && !String.IsNullOrWhiteSpace(value))
            {
                using (var divWriter = parent.CreateWriter())
                using (var nodeReader = SerializationUtil.XmlReaderFromXmlText(value))
                    divWriter.WriteNode(nodeReader, false);
                return;
            }

            var usesAttribute = xmlDetails?.NodeType != null ?
                xmlDetails.NodeType == XmlNodeType.Attribute : serializationInfo?.IsAtomicValue ?? false;
            var ns = xmlDetails?.Namespace.NamespaceName ?? (usesAttribute ? "" : XmlNs.FHIR);
            var localName = serializationInfo?.IsChoiceElement == true ?
                            source.Name + source.Type.Capitalize() : source.Name;

            bool atRoot = parent is XDocument;

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
            var containedResourceType = serializationInfo?.IsContainedResource == true ?
                                            source.Type :
                                            (atRoot == false ? source.GetResourceType() : null);

            XElement containedResource = null;
            if (containedResourceType != null)
                containedResource = new XElement(XName.Get(containedResourceType, ns));

            var childParent = containedResource ?? me;

            // Now, do the same for the children
            // xml requires a certain order, so let's make sure we serialize in the right order
            var orderedChildren = source.Children().OrderBy(c => c.GetSerializationInfo()?.Order ?? 0);            
            foreach (var child in orderedChildren)
                write(child, childParent);

            if (xmlDetails?.NodeText != null)
                childParent.Add(new XText(xmlDetails.NodeText));

            if (sourceComments?.ClosingComments != null)
                writeComments(sourceComments.ClosingComments, me);

            // Only really add this contained resource to me when it has contents
            if (containedResource != null && (containedResource.HasAttributes || containedResource.HasElements))
                me.Add(containedResource);

            // Only add myself to my parent if I have content
            if (me.HasAttributes || me.HasElements)
            {
                if (sourceComments?.CommentsBefore != null)
                    writeComments(sourceComments.CommentsBefore, parent);

                parent.Add(me);
            }

            if (atRoot && sourceComments?.DocumentEndComments != null)
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

        public IExceptionSink Sink { get; set; }

        public void Notify(object source, ExceptionNotification args) => Sink.NotifyOrThrow(source, args);
    }
}
