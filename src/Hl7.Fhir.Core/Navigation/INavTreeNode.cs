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

        /// <summary>Returns <c>true</c> if the current instance represents a root node (<see cref="Parent"/> equals <c>null</c>).</summary>
        bool IsRoot { get; }

        /// <summary>Returns <c>true</c> if the current node has any children (the node implements <see cref="INavTreeNode{TNode}"/> and <see cref="INavTreeNode{TNode}.FirstChild"/> is not <c>null</c>).</summary>
        bool HasChildren { get; }

        /// <summary>Returns <c>true</c> if the current instance represents a leaf node (the node does not implement the <see cref="INavTreeNode{TNode}"/> interface).</summary>
        bool IsLeaf { get; }

        /// <summary>Returns <c>true</c> if the current instance represents a value leaf node (the node implement the <see cref="IValue{TValue}"/> interface).</summary>
        bool IsValue { get; }

    }

    /// <summary>Generic navigable tree node interface.</summary>
    /// <typeparam name="TNode">The concrete type of the implementing class.</typeparam>
    public interface INavTreeNode<out TNode> : INavTreeLeafNode<TNode> where TNode : INavTreeLeafNode<TNode> // INode
    {
        /// <summary>Reference to first child node.</summary>
        TNode FirstChild { get; }
    }

    #endregion
}
