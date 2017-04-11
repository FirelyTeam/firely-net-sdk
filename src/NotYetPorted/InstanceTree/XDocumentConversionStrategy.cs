using System.Collections.Generic;
using System.Xml.Linq;
using Hl7.Fhir.Navigation;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    internal class XDocumentConversionStrategy : INodeConversionStrategy<XObject>
    {        
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XDocument);
        }

        public FhirInstanceTree ConstructTreeNode(XObject docNode, FhirInstanceTree parent)
        {
            var result = FhirInstanceTree.Create("(root)");
            var nestedResourceName = ((XDocument)docNode).Root.Name.LocalName;

            result.AddAnnotation(new XmlRenderHints { NestedResourceName = nestedResourceName });

            return result;
        }
        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirInstanceTree treeNode)
        {
            return ((XDocument)docNode).Nodes();
            //var doc = (XDocument)docNode;

            //foreach (var child in doc.Nodes())
            //{
            //    if (child is XElement && child == doc.Root)
            //    {
            //        foreach (var containedNode in doc.Root.Nodes())
            //            yield return containedNode;
            //    }
            //    else
            //        yield return child;
            //}
        }

        public void PostProcess(FhirInstanceTree convertedNode)
        {
            return;
        }
    }
}