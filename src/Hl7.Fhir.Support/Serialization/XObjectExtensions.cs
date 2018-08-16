using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

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
            if (node is XContainer container)
            {
                // Alright, containers may have children, but if this is an Element, 
                // we need to iterate through the attributes first
                if (container is XElement element && element.FirstAttribute != null)
                    return element.FirstAttribute;
                else
                    return container.FirstNode;
            }

            return null;
        }

        public static XObject NextSibling(this XObject node)
        {
            if (node is XNode n)
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

        public static XName Name(this XObject node)
        {
            if (node is XElement xe) return xe.Name;
            if (node is XAttribute xa) return xa.Name;

            return null;
        }

        public static string Value(this XObject node)
        {
            if (node is XElement xe) return xe.Value;
            if (node is XAttribute xa) return xa.Value;
            if (node is XText xt) return xt.Value;

            return null;
        }

        public static string Text(this XObject node)
        {
            return (node is XContainer container) ?
                extractString(container.Nodes().OfType<XText>().Select(t => t.Value)) : null;

            string extractString(IEnumerable<string> source)
            {
                var concatenated = string.Concat(source).Trim();
                return string.IsNullOrEmpty(concatenated) ? null : concatenated;
            }
        }

        public static IEnumerable<XNode> PreviousNodes(this XNode node)
        {
            var scan = node.PreviousNode;

            while(scan != null)
            {
                yield return scan;
                scan = scan.PreviousNode;
            }
        }

        public static bool AtXhtmlDiv(this XObject node) => (node as XElement)?.Name == XmlNs.XHTMLDIV;

        public static XDocument Rename(this XDocument doc, string newRootName)
        {
            if (newRootName != null)
                doc.Root.Name = XName.Get(newRootName, doc.Root.Name.Namespace.NamespaceName);
            return doc;
        }
    }
}
