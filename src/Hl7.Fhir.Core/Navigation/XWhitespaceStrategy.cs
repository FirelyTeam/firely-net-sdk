using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class XWhitespaceStrategy : INodeConversionStrategy<XObject>
    {
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XText) && ((XText)docNode).Value.Trim() == "";
        }

        public FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            return null;
        }

        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirNavigationTree treeNode)
        {
            return null;
        }

        public IEnumerable<XObject> PostProcessChildren(IEnumerable<XObject> children, FhirNavigationTree treeNode)
        {
            throw Error.NotImplemented("Don't know how to process children of an empty XText node.");
        }
    }
}