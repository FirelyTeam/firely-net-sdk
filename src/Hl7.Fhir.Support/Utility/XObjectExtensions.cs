using System.Xml.Linq;
using System.Xml;

namespace Hl7.Fhir.Utility
{
    public static class XObjectExtensions
    {
        public static bool TryGetAttribute(this XElement docNode, XName name, out string value)
        {
            var attr = docNode.Attribute(name);

            if (attr != null)
            {
                value = attr.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }


        /***
         *  Make sure we handle all possible node types:
         *  System.Xml.Linq.XObject
         *    System.Xml.Linq.XAttribute
         *    System.Xml.Linq.XNode
         *       System.Xml.Linq.XComment
         *       System.Xml.Linq.XContainer
         *           System.Xml.Linq.XDocument
         *           System.Xml.Linq.XElement
         *       System.Xml.Linq.XDocumentType
         *       System.Xml.Linq.XProcessingInstruction
         *       System.Xml.Linq.XText   
         *
         * Note: XDocumentType, XProcessingInstruction appear only on XDocument 
         */
        public static XObject FirstChild(this XObject node)
        {
            var container = node as XContainer;

            if (container != null)
            {
                // Alright, containers may have children, but if this is an Element, 
                // we need to iterate through the attributes first
                var element = container as XElement;

                if(element?.FirstAttribute != null)
                    return element.FirstAttribute;
                else
                    return container.FirstNode;
            }

            return null;
        }

        public static XObject NextChild(this XObject node)
        {
            var n = node as XNode;

            if (n != null)
                return n.NextNode;
            else
            {
                // If we are not an XNode, we are an attribute (in Linq to Xml hierarchy)
                var attr = (XAttribute)node;
                if (attr.NextAttribute != null) return attr.NextAttribute;

                // out of attributes, continue with our parents first "real" node
                return attr.Parent.FirstNode;
            }
        }

        public static string Value(this XObject node)
        {
            if (node.NodeType == XmlNodeType.Attribute)
            {
                return ((XAttribute)node).Value;
            }
            else if (node.NodeType == XmlNodeType.Text)
            {
                return ((XText)node).Value;
            }
            else
                return null;
        }
    }
}
