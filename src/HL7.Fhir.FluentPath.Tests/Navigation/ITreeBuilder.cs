/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#define NEWBUILDER

using System;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common generic interface for constructing a tree of type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The tree type.</typeparam>
    public interface ITreeBuilder<out T> : ITree<T> where T : ITreeBuilder<T>
        // : ILinkedTree<T> where T : ITreeBuilder<T>
    {
        /// <summary>Add a new node with the specified name as the first child.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddFirstChild(string name);

        /// <summary>Add a new node with the specified name as the next sibling.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddNextSibling(string name);
    }

    /// <summary>
    /// Common generic interface for constructing a tree of type <typeparamref name="T"/>
    /// that supports node values of varying types.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    public interface IVariantTreeBuilder<out T> : ITreeBuilder<T> where T : ITreeBuilder<T>
        // : ILinkedTreeBuilder<T> where T : IVariantTreeBuilder<T>
    {
        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the first child.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddFirstChild<V>(string name, V value);

        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the next sibling.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddNextSibling<V>(string name, V value);
    }

    /// <summary>
    /// Common generic interface for constructing a tree of type <typeparamref name="T"/>
    /// that supports uniformly typed node values of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    public interface IValueTreeBuilder<out T, in V> : ITreeBuilder<T> where T : ITreeBuilder<T>
        // : ILinkedTreeBuilder<T> where T : IValueTreeBuilder<T, V>
    {
        /// <summary>Add a new node with the specified name and value as the first child.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddFirstChild(string name, V value);

        /// <summary>Add a new node with the specified name and value as the next sibling.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        T AddNextSibling(string name, V value);
    }

}
