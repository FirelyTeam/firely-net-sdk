using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class XDocumentConversionStrategy : XElementNestedResourceConversionStrategy
    {        
        public override bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XDocument);
        }

        public override FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            return FhirNavigationTree.Create("(root)");           
        }
    }
}