/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    // Just need to keep this in mind (for json): if value == null && no children -> render "null" as primitive value of node?
    public class NavigatorXmlWriter
    {
        public NavigatorXmlWriter()
        {
        }

        public void Write(IElementNavigator source, XmlWriter destination)
        {
            var settings = new XmlWriterSettings { NewLineHandling = NewLineHandling.Entitize };

            var xw = XmlWriter.Create(destination, settings);
            write(source, xw, atRoot:true);
        }

        private void write(IElementNavigator source, XmlWriter destination, bool atRoot=false)
        {
            var xmlDetails = (source as IAnnotated)?.Annotation<XmlSerializationDetails>();
            var sourceComments = (source as IAnnotated)?.Annotation<SourceComments>();

            var isXhtml = xmlDetails?.Name == XmlNs.XHTMLDIV;
            var value = source.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(source.Value) : null;

            if(sourceComments?.CommentsBefore != null)
                writeComments(sourceComments.CommentsBefore, destination);

            // xhtml children require special treament:
            // - They don't use an xml "value" attribute to represent the value, instead their Value is inserted verbatim into the parent
            // - They cannot have child nodes - the "Value" on the node contains all children as raw xml text
            if (isXhtml)
            {
                using(var nodeReader = SerializationUtil.XmlReaderFromXmlText(value))
                    destination.WriteNode(nodeReader, false);
                return;
            }

            var ns = xmlDetails?.Name.NamespaceName ?? XmlNs.FHIR;
            var prefix = ns != null ? destination.LookupPrefix(ns) : null;
            var localName = xmlDetails?.Name.LocalName ?? source.Name;
            var usesAttribute = xmlDetails?.NodeType == XmlNodeType.Attribute;

            // If the node is represented by an attribute (e.g. an "id" child), write
            // an attribute with the child's name + the child's Value into the parent
            if(usesAttribute && destination.WriteState == WriteState.Element)
            {
                destination.WriteAttributeString(prefix, localName, ns, value);
                return;
            }

            // Not being xhtml or an attribute node, we can now create a new element
            // that might represent the nodes Value (if any) and/or its children
            destination.WriteStartElement(prefix, localName, ns);

            // If the node has a value, at the standard FHIR value attribute
            if (value != null)
                destination.WriteAttributeString("value", value);

            // Now, do the same for the children
            if (source.HasChildren())
            {
                foreach (var child in source.Children())
                    write(child, destination);
            }

            if(sourceComments?.ClosingComments != null)
                writeComments(sourceComments.ClosingComments, destination);

            if (xmlDetails?.NodeText != null)
                destination.WriteValue(xmlDetails.NodeText);

            // Close the tag with the element name
            destination.WriteEndElement();

            if (atRoot && sourceComments?.DocumentEndComments != null)
                writeComments(sourceComments.DocumentEndComments, destination);
        }


        private void writeComments(string[] comments, XmlWriter destination)
        {
            if (comments?.Any() == true)
            {
                foreach (var comment in comments)
                    destination.WriteComment(comment);
            }
        }
    }
}
