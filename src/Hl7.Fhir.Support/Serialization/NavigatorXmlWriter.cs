using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    public class NavigatorXmlWriter
    {
        public NavigatorXmlWriter()
        {
        }


        public void Write(IElementNavigator source, XmlWriter destination)
        {
            var settings = new XmlWriterSettings();
            settings.NewLineHandling = NewLineHandling.Entitize;

            // To this from the original XmlFhirWriter, have to double check what it does ;-)
            var xw = XmlWriter.Create(destination, settings);
            write(source, xw, atRoot:true);
        }

        private void write(IElementNavigator source, XmlWriter destination, bool atRoot=false)
        {
            var xmlDetails = (source as IAnnotated)?.Annotation<XmlSerializationDetails>();
            var isXhtml = source.Type == "xhtml" || xmlDetails?.Name == XmlNs.XHTMLDIV;
            var value = source.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(source.Value) : null;

            // xhtml children require special treament:
            // - They don't use an xml "value" attribute to represent the value, instead their Value is inserted verbatim into the parent
            // - They cannot have child nodes - the "Value" on the node contains all children as raw xml text
            if (isXhtml)
            {
                //var sanitized = SerializationUtil.SanitizeXml(valueAsString);
                //XElement xe = XElement.Parse(sanitized);
                //xe.Name = XmlNs.XHTMLNS + xe.Name.LocalName;

                //// Write xhtml directly into the output stream,
                //// the xhtml <div> becomes part of the elements
                //// of the type, just like the other FHIR elements
                //var ready = xe.ToString();
                //xw.WriteRaw(ready);

                // To this from the original XmlFhirWriter, have to double check what it does ;-)
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
            // Note that the named root of an IElementNav tree is not represented, so
            // we need to check for that
            if (!atRoot)
                destination.WriteStartElement(prefix, localName, ns);

            // If the node has a value, at the standard FHIR value attribute
            if (value != null)
                destination.WriteAttributeString("value", value);

            // If this element is a resource, the "type" of the element is inserted
            // as an xml element with the capitalized name of the Resource type
            // (both true for the root as for nested resources (e.g. <contained>)
            var isResourceType = char.IsUpper(source.Type?[0] ?? 'q');
            if (isResourceType)
                destination.WriteStartElement(prefix, source.Type, ns);

            // Now, do the same for the children
            if (source.HasChildren())
            {
                foreach (var child in source.Children())
                    write(child, destination);
            }

            // Close the tag with the resource type name, if any
            if (isResourceType)
                destination.WriteEndElement();

            // Close the tag with the element name
            if (!atRoot)
                destination.WriteEndElement();
        }
    }
}
