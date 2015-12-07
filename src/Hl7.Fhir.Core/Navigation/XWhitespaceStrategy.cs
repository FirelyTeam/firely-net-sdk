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

        public void PostProcess(FhirNavigationTree convertedNode)
        {
            return;
        }
    }
}