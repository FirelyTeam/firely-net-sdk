using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class XElementDivConversionStrategy : INodeConversionStrategy<XObject>
    {
        public static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";

        public bool HandlesDocNode(XObject docNode)
        {
            return docNode is XElement && ((XElement)docNode).Name == XHTMLDIV;
        }

        public FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            var element = (XElement)docNode;

            var value = getDivValue(element);
            var result = parent.AddLastChild(element.Name.LocalName, value);

            result.AddAnnotation(new XmlRenderHints() { IsXhtmlDiv = true });

            return result;
        }



        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirNavigationTree treeNode)
        {
            return null;    // No children
        }

        public IEnumerable<XObject> PostProcessChildren(IEnumerable<XObject> children, FhirNavigationTree treeNode)
        {
            throw Error.NotImplemented("Don't know how to process children of an XHtml <div>.");
        }

        private static string getDivValue(XElement docNode)
        {
            return docNode.ToString(SaveOptions.DisableFormatting);
        }
    }
}