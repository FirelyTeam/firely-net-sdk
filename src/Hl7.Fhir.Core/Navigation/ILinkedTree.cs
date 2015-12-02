/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common interface for a tree item.</summary>
    public interface ITree
    {
        /// <summary>The name of the tree item.</summary>
        string Name { get; }

        // <summary>Returns <c>true</c> if the tree item supports the <see cref="IValue"/> interface.</summary>
        // bool IsValue { get; }
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

        /// <summary>Indexer property. Enumerates the child items with the specified name.</summary>
        /// <param name="name">An item name.</param>
        /// <returns>An tree item enumerator.</returns>
        IEnumerable<T> this[string name] { get; }

        /// <summary>Indexer property. Enumerates the descendant items with the specified path, i.e. a sequence of nested node names.</summary>
        /// <param name="path">An array of nested item names.</param>
        /// <returns>An tree item enumerator.</returns>
        /// <example>
        /// Retrieve descendants of the specified node with path "child.grandchild.greatgrandchild":
        /// <list type="number">
        ///     <item><description>Find child nodes of the specified node with name "child"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "grandchild"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "greatgrandchild"</description></item>
        /// </list>
        /// <code>var result = node["child", "grandchild", "greatgrandchild"];</code>
        /// </example>
        IEnumerable<T> this[params string[] path] { get; }

        /// <summary>Returns <c>true</c> if the instance is an internal tree node, i.e. if the item has at least one child.</summary>
        bool IsInternal { get; }

        /// <summary>Returns <c>true</c> if the instance is a tree leaf item, i.e. if the item has no children.</summary>
        bool IsLeaf { get; }

    }

    /// <summary>Common generic interface for constructing a linked tree.</summary>
    /// <typeparam name="T">The item type.</typeparam>
    public interface ILinkedTreeBuilder<out T> : ILinkedTree<T> where T : ILinkedTree<T>
    {
        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        T AddLastSibling(string name);

        /// <summary>Add a new leaf item with the specified name and value as the last sibling.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new sibling leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new sibling leaf item of type <typeparamref name="T"/>.</returns>
        T AddLastSibling<V>(string name, V value);

        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        T AddLastChild(string name);

        /// <summary>Add a new node with the specified name and value as the last child.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new child leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new child leaf item of type <typeparamref name="T"/>.</returns>
        T AddLastChild<V>(string name, V value);
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
