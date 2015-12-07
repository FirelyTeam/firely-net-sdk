using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace Hl7.Fhir.Navigation
{
    internal class XDocumentConversionStrategy : INodeConversionStrategy<XObject>
    {        
        public bool HandlesDocNode(XObject docNode)
        {
            return (docNode is XDocument);
        }

        public FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            var result = FhirNavigationTree.Create("(root)");
            var nestedResourceName = ((XDocument)docNode).Root.Name.LocalName;

            result.AddAnnotation(new XmlRenderHints { NestedResourceName = nestedResourceName });

            return result;
        }
        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirNavigationTree treeNode)
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

        public void PostProcess(FhirNavigationTree convertedNode)
        {
            return;
        }
    }
}