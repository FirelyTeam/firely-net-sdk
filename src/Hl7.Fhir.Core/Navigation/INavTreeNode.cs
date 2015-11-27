/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using System;

namespace Hl7.Fhir.Navigation
{
    // [WMR] Navigation tree node interfaces
    // Separate interfaces for internal nodes (with children) and leaf nodes (with value, without children)
    // Use generic interfaces so implementing classes can operate on concrete (not interface) node references

    // TODO:
    // - Clone (sub)tree ?

    /// <summary>Common tree node interface.</summary>
    public interface INode
    {
        /// <summary>Gets the name of the node.</summary>
        string Name { get; }
    }

    /// <summary>Generic interface for a strongly-typed value.</summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface IValue<out TValue>
    {
        /// <summary>Gets a value of type <typeparamref name="TValue"/>.</summary>
        TValue Value { get; }
    }

    #region Navigation Tree Node interfaces

    // [WMR] Note: generic constraints try to enforce the concrete class type of the implementing class
    // e.g. class MyTreeNode : INavTreeNode<MyTreeNode>
    // The linked node properties return the concrete type of the implementing class (instead of an interface)

    /// <summary>Generic navigable tree node interface for empty leaf nodes.</summary>
    /// <typeparam name="TNode">The concrete type of the implementing class.</typeparam>
    public interface INavTreeLeafNode<out TNode> : INode where TNode : INavTreeLeafNode<TNode> // INode
    {
        /// <summary>Reference to the parent node.</summary>
        TNode Parent { get; }
        /// <summary>Reference to preceding sibling node.</summary>
        TNode PreviousSibling { get; }
        /// <summary>Reference to following sibling node.</summary>
        TNode NextSibling { get; }
    }

    /// <summary>Generic navigable tree node interface.</summary>
    /// <typeparam name="TNode">The concrete type of the implementing class.</typeparam>
    public interface INavTreeNode<out TNode> : INavTreeLeafNode<TNode> where TNode : INavTreeLeafNode<TNode> // INode
    {
        /// <summary>Reference to first child node.</summary>
        TNode FirstChild { get; }
    }

    /// <summary>Generic navigable tree node interface for leaf nodes with a typed value.</summary>
    /// <typeparam name="TNode">The concrete type of the implementing class.</typeparam>
    /// <typeparam name="TValue">The type of the node value.</typeparam>
    public interface INavTreeValueLeafNode<out TNode, out TValue> : INavTreeLeafNode<TNode>, IValue<TValue>
        where TNode : INavTreeLeafNode<TNode> // INavTreeNode<TNode>
    {
        // 
    }

    #endregion

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

}
