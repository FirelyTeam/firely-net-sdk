using Hl7.Fhir.Navigation;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hl7.Fhir.FluentPath.InstanceTree
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

            var result = parent.AddLastChild(COMMENT_ELEMENT_NAME, (IValueProvider)new ConstantValue(comment.Value));
            result.AddAnnotation(new StructuralHints() { IsComment = true });
            result.AddAnnotation(docNode);
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