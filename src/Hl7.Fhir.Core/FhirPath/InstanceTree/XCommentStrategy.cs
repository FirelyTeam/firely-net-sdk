using Hl7.Fhir.Navigation;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.FhirPath.InstanceTree
{
    internal class XCommentStrategy : INodeConversionStrategy<XObject>
    {
        public const string COMMENT_ELEMENT_NAME = "(comment)";

        public bool HandlesDocNode(XObject docNode)
        {
            return docNode is XComment;
        }

        public FhirInstanceTree ConstructTreeNode(XObject docNode, FhirInstanceTree parent)
        {
            var comment = (XComment)docNode;

            var result = parent.AddLastChild(COMMENT_ELEMENT_NAME, (IFhirPathValue)new TypedValue(comment.Value));
            result.AddAnnotation(new StructuralHints() { IsComment = true });

            return result;
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