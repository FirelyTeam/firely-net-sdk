using Hl7.Fhir.Navigation;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    internal class XElementDivConversionStrategy : INodeConversionStrategy<XObject>
    {
        public static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";

        public bool HandlesDocNode(XObject docNode)
        {
            return docNode is XElement && ((XElement)docNode).Name == XHTMLDIV;
        }

        public FhirInstanceTree ConstructTreeNode(XObject docNode, FhirInstanceTree parent)
        {
            var element = (XElement)docNode;

            var value = getDivValue(element);
            var result = parent.AddLastChild(element.Name.LocalName, (IValueProvider)new ConstantValue(value));

            result.AddAnnotation(new XmlRenderHints() { IsXhtmlDiv = true });
            result.AddAnnotation(docNode);
            return result;
        }



        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirInstanceTree treeNode)
        {
            return null;    // No children
        }

        public void PostProcess(FhirInstanceTree convertedNode)
        {
            return;
        }

        private static string getDivValue(XElement docNode)
        {
            return docNode.ToString(SaveOptions.DisableFormatting);
        }
    }
}