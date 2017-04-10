using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    internal class XWhitespaceStrategy : INodeConversionStrategy<XObject>
    {
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XText) && ((XText)docNode).Value.Trim() == "";
        }

        public FhirInstanceTree ConstructTreeNode(XObject docNode, FhirInstanceTree parent)
        {
            return null;
        }

        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirInstanceTree treeNode)
        {
            return null;
        }

        public void PostProcess(FhirInstanceTree convertedNode)
        {
            return;
        }
    }
}