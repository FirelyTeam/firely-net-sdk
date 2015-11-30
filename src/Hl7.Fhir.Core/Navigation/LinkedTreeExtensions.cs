/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public static class LinkedTreeExtensions
    {
        /// <summary>
        /// Returns the last sibling of the specified tree item.
        /// Returns the given item if it has no siblings.
        /// </summary>
        /// <param name="tree">A <see cref="ILinkedTree{T}"/> item.</param>
        /// <typeparam name="T">The link type.</typeparam>
        /// <returns>A <see cref="ILinkedTree{T}"/> item.</returns>
        public static T LastSibling<T>(this T tree) where T : ILinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            for (T next; (next = tree.NextSibling) != null; tree = next) ;

            Debug.Assert(tree != null);
            Debug.Assert(tree.NextSibling == null);

            return tree;
        }

        // [WMR] Null propagation operator requires additional generic class constraint
        // public static T LastChild<T>(this T node) where T : class, INavTreeNode<T> => node?.FirstChild?.LastSibling();

        public static T LastChild<T>(this T node) where T : ILinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            var first = node.FirstChild;
            return first != null ? first.LastSibling() : default(T);
        }


        /// <summary>Perform depth-first recursion on a <see cref="DoublyLinkedTree"/> structure.</summary>
        /// <typeparam name="T">The node type; should implement <see cref="IDoublyLinkedTree{T}"/>.</typeparam>
        /// <param name="root">The recursion start node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> that returns all tree nodes in depth-first order.</returns>
        /// <remarks>The implementation is O(n).</remarks>
        private static IEnumerable<T> DepthFirst<T>(T root) where T : IDoublyLinkedTree<T>
        {
            return LinkedTreeHelpers.DepthFirst(root, node => node.FirstChild, node => node.NextSibling, node => node.Parent);
        }

        #region Axes navigation

        /// <summary>Enumerate the direct children of the current tree node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Children<T>(this T node) where T : ILinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)

            var child = node.FirstChild;
            while (child != null)
            {
                yield return child;
                child = child.NextSibling;
            }
            yield break;
        }

        /// <summary>Enumerate the descendants of the current tree node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Descendants<T>(this T node) where T : IDoublyLinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
#if true
            // Skip the root node
            return DepthFirst(node).Skip(1);
#else
            // [WMR] Nested foreach is inefficient O(n^2)
            // Note: C# should implement "yield foreach" - maybe in C# 7?

            foreach (var child in node.Children())
            {
                foreach (var item in node.Descendants())
                {
                    yield return item;
                }
                yield return child;
            }
            yield break;
#endif
        }

        /// <summary>Enumerate the current tree node and it's descendants.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> DescendantsAndSelf<T>(this T node) where T : IDoublyLinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            return DepthFirst(node);
        }

        /// <summary>Enumerate the ancestors of the current tree node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Ancestors<T>(this T node) where T : IDoublyLinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.Parent; node != null; node = node.Parent) { yield return node; }
            yield break;
        }

        /// <summary>Enumerate the current tree node and it's ancestors.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> AncestorsAndSelf<T>(this T node) where T : IDoublyLinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (; node != null; node = node.Parent) { yield return node; }
            yield break;

        }

        /// <summary>Enumerate the siblings preceding the current tree node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> PrecedingSiblings<T>(this T node) where T : IDoublyLinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.PreviousSibling; node != null; node = node.PreviousSibling) { yield return node; }
            yield break;
        }

        /// <summary>Enumerate the siblings following the current tree node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for tree nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> FollowingSiblings<T>(this T node) where T : ILinkedTree<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.NextSibling; node != null; node = node.NextSibling) { yield return node; }
            yield break;
        }

        #endregion

        // TODO: Move to different (extension) class

        #region FHIR Name matching

        private const string PolymorphicNodeNameSuffix = "[x]";
        public const string NodeNameWildcard = "*";

        public static bool IsMatch<T>(this T node, string nodeName) where T : ITree
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            if (string.IsNullOrEmpty(nodeName)) { throw new ArgumentNullException("nodeName"); } // nameof(nodeName)

            return nodeName == NodeNameWildcard | node.Name == nodeName | IsPolymorphicMatch(node, nodeName);
        }

        private static bool IsPolymorphicMatch<T>(T node, string nodeName) where T : ITree
        {
            if (nodeName.EndsWith(PolymorphicNodeNameSuffix))
            {
                var prefixLength = nodeName.Length - PolymorphicNodeNameSuffix.Length;
                return String.Compare(node.Name, 0, nodeName, 0, Math.Max(0, prefixLength)) == 0
                    && IsValidTypeName(node.Name.Substring(prefixLength + 1));
            }
            return false;
        }

        static bool IsValidTypeName(string name)
        {
            // TODO: validate typename
            return char.IsUpper(name, 0);
        }

        #endregion
    }
}
