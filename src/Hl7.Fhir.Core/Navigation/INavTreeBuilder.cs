using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    #region Tree building interfaces

    // - separate interfaces for tree building methods to allow covariance on INavTreeNode interfaces
    // - separate interfaces per tree building method so leaf nodes do not have to implement FirstChild

    /// <summary>Generic interface for adding siblings to <see cref="INavTreeLeafNode{TNode}"/> tree nodes.</summary>
    /// <typeparam name="TNode">The type of the receiving node, i.e. of the implementing class.</typeparam>
    public interface INavTreeLeafBuilder<TNode> where TNode : INavTreeLeafNode<TNode>
    {
        /// <summary>Append the specified node as a new sibling node of the current node.</summary>
        /// <param name="node">The sibling node to append.</param>
        /// <typeparam name="T">The type of the specified sibling node.</typeparam>
        /// <returns>The specified node.</returns>
        T AppendSiblingNode<T>(T node) where T : TNode;
    }

    /// <summary>Generic interface for adding children to <see cref="INavTreeNode{TNode}"/> tree nodes.</summary>
    /// <typeparam name="TNode">The type of the receiving node, i.e. of the implementing class.</typeparam>
    public interface INavTreeBuilder<TNode> : INavTreeLeafBuilder<TNode> where TNode : INavTreeLeafNode<TNode>
    {
        /// <summary>Append the specified node as a new child node of the current node.</summary>
        /// <param name="node">The child node to append.</param>
        /// <typeparam name="T">The type of the specified child node.</typeparam>
        /// <returns>The specified node.</returns>
        T AppendChildNode<T>(T node) where T : TNode;
    }

    #endregion

    // Following extension methods return concrete NavTreeNode & NavTreeLeafNode instances

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
