using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    // Following extension methods are implemented against concrete NavTreeNode class type

    /// <summary>Extension methods to facilitate tree building.</summary>
    public static class NavTreeBuilderExtensions
    {
        public static NavTreeNode AppendChild(this NavTreeNode node, string name)
        {
            return node.AppendChildNode(new NavTreeNode(name));
        }

        public static NavTreeLeafNode<TValue> AppendChild<TValue>(this NavTreeNode node, string name, TValue value)
        {
            return node.AppendChildNode(new NavTreeLeafNode<TValue>(name, value));
        }

        public static NavTreeNode AppendSibling(this NavTreeNode node, string name)
        {
            return node.AppendSiblingNode(new NavTreeNode(name));
        }

        public static NavTreeLeafNode<TValue> AppendSibling<TValue>(this NavTreeNode node, string name, TValue value)
        {
            return node.AppendSiblingNode(new NavTreeLeafNode<TValue>(name, value));
        }
    }
}
