using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class XCommentStrategy : INodeConversionStrategy<XObject>
    {
        public const string COMMENT_ELEMENT_NAME = "(comment)";

        public bool HandlesDocNode(XObject docNode)
        {
            return docNode is XComment;
        }

        public FhirNavigationTree ConstructTreeNode(XObject docNode, FhirNavigationTree parent)
        {
            var comment = (XComment)docNode;

            var result = parent.AddLastChild(COMMENT_ELEMENT_NAME, comment.Value);
            result.AddAnnotation(new StructuralHints() { IsComment = true });

            return result;
        }

        public IEnumerable<XObject> SelectChildren(XObject docNode, FhirNavigationTree treeNode)
        {
            return null;
        }

        public void PostProcess(FhirNavigationTree convertedNode);
        {
            return;
        }
    }
}