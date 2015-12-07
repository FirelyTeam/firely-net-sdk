using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class XAttributeConversionStrategy : INodeConversionStrategy<XObject>
    {
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XAttribute) && ((XAttribute)docNode).Name.NamespaceName == "";
        }

        public FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            var attr = (XAttribute)docNode;

            var newNodeName = attr.Name.LocalName;
            var result = parent.AddLastChild(newNodeName, attr.Value);
            result.AddAnnotation(new XmlRenderHints() { IsXmlAttribute = true });

            return result;
        }

        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirNavigationTree treeNode)
        {
            return null;
        }

        public void PostProcess(FhirNavigationTree convertedNode)
        {
            throw Error.NotImplemented("Don't know how to process children of an xml attribute.");
        }
    }
}