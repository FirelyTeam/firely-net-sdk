/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public static class LinkedTreeBuilderExtensions
    {
        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        public static T AddLastChild<T>(this T node, string name) where T : ILinkedTree<T>, ITreeBuilder<T>
        {
            return node.IsLeaf ? node.AddFirstChild(name) : node.LastChild().AddNextSibling(name);
        }

        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        public static T AddLastSibling<T>(this T node, string name) where T : ILinkedTree<T>, ITreeBuilder<T>
        {
            return node.LastSibling().AddNextSibling(name);
        }

        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        public static T AddLastChild<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ITreeBuilder<T>, IValueProvider<V>
        {
            var valueBuilder = node as IValueTreeBuilder<T, V>;
            if (valueBuilder != null)
            {
                if (node.IsLeaf)
                {
                    return valueBuilder.AddFirstChild(name, value);
                }
                valueBuilder = node.LastChild() as IValueTreeBuilder<T, V>;
                return valueBuilder.AddNextSibling(name, value);
            }
            var variantBuilder = node as IVariantTreeBuilder<T>;
            if (variantBuilder != null)
            {
                if (node.IsLeaf)
                {
                    return variantBuilder.AddFirstChild<V>(name, value);
                }
                variantBuilder = node.LastChild() as IVariantTreeBuilder<T>;
                return variantBuilder.AddNextSibling<V>(name, value);
            }
            throw new InvalidOperationException();
        }

        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        public static T AddLastSibling<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ITreeBuilder<T>, IValueProvider<V>
        {
            var valueBuilder = node as IValueTreeBuilder<T, V>;
            if (valueBuilder != null)
            {
                valueBuilder = node.LastSibling() as IValueTreeBuilder<T, V>;
                return valueBuilder.AddNextSibling(name, value);
            }
            var variantBuilder = node as IVariantTreeBuilder<T>;
            if (variantBuilder != null)
            {
                variantBuilder = node.LastSibling() as IVariantTreeBuilder<T>;
                return variantBuilder.AddNextSibling<V>(name, value);
            }
            throw new InvalidOperationException();
        }
    }

#if false
    
    // Ambiguous - C# type call binding cannot distinguish on different generic constraints

    public static class LinkedValueTreeBuilderExtensions
    {
        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        public static T AddLastChild<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ILinkedValueTreeBuilder<T, V>
        {
            return node.IsLeaf ? node.AddFirstChild(name, value) : node.LastChild().AddNextSibling(name, value);
        }

        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        public static T AddLastSibling<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ILinkedValueTreeBuilder<T, V>
        {
            return node.LastSibling().AddNextSibling(name, value);
        }
    }

    public static class LinkedVariantTreeBuilderExtensions
    {
        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        public static T AddLastChild<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ILinkedVariantTreeBuilder<T>
        {
            return node.IsLeaf ? node.AddFirstChild(name, value) : node.LastChild().AddNextSibling(name, value);
        }

        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="node">A tree node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        public static T AddLastSibling<T, V>(this T node, string name, V value) where T : ILinkedTree<T>, ILinkedVariantTreeBuilder<T>
        {
            return node.LastSibling().AddNextSibling(name, value);
        }
    }
#endif

}
