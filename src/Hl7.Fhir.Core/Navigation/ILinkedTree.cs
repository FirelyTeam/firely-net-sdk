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
    /// <summary>Common interface for a tree item.</summary>
    public interface ITree
    {
        /// <summary>The name of the tree item.</summary>
        string Name { get; }

        /// <summary>Returns <c>true</c> if the tree item supports the <see cref="IValue"/> interface.</summary>
        bool IsValue { get; }
    }

    // [WMR] Notes:
    // - Generic interface allows derived interfaces to return a more specific type
    //   i.e. IDoublyLinkedTree.FirstChild can return a IDoublyLinkedTree reference instead of a ILinkedList reference
    // - out attribute on type parameter T allows for covariance
    //   i.e. a value of type interface<out Tderived> may be assigned to variable of type interface<out Tbase>
    //   Valid because interface members may return a Tderived instance as a Tbase reference

    /// <summary>Common generic interface for a linked tree item.</summary>
    /// <typeparam name="T">The link type.</typeparam>
    public interface ILinkedTree<out T> : ITree where T : ILinkedTree<T>
    {
        /// <summary>Returns a reference to the next sibling tree item.</summary>
        T NextSibling { get; }

        /// <summary>Returns a reference to the first child tree item.</summary>
        T FirstChild { get; }

        /// <summary>Returns <c>true</c> if the instance is an internal tree node, i.e. if the item has at least one child.</summary>
        bool IsInternal { get; }

        /// <summary>Returns <c>true</c> if the instance is a tree leaf item, i.e. if the item has no children.</summary>
        bool IsLeaf { get; }

    }

    /// <summary>Common generic interface for constructing a linked tree.</summary>
    /// <typeparam name="T">The item type.</typeparam>
    public interface ILinkedTreeBuilder<out T> : ILinkedTree<T> where T : ILinkedTree<T>
    {
        /// <summary>Add a new sibling node with the specified name.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        T AddSibling(string name);

        /// <summary>Add a new sibling leaf item with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new sibling leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new sibling leaf item of type <typeparamref name="T"/>.</returns>
        T AddSibling<V>(string name, V value);

        /// <summary>Add a new child node with the specified name.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        T AddChild(string name);

        /// <summary>Add a new child node with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new child leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new child leaf item of type <typeparamref name="T"/>.</returns>
        T AddChild<V>(string name, V value);
    }

    /// <summary>Common generic interface for a doubly linked tree item.</summary>
    /// <typeparam name="T">The link type.</typeparam>
    public interface IDoublyLinkedTree<out T> : ILinkedTree<T> where T : IDoublyLinkedTree<T>
    {
        /// <summary>Returns a reference to the parent tree item.</summary>
        T Parent { get; }

        /// <summary>Returns a reference to the previous sibling tree item.</summary>
        T PreviousSibling { get; }

        /// <summary>Returns <c>true</c> if the item represents a root node, i.e. if the item <see cref="Parent"/> reference equals <c>null</c>.</summary>
        bool IsRoot { get; }
    }
}
