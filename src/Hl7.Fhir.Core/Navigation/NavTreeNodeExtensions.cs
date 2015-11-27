/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public static class NavTreeNodeExtensions
    {
        #region Low level navigation helpers

        public static T LastSibling<T>(this T node) where T : INavTreeLeafNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (T next; (next = node.NextSibling) != null; node = next) ;
            return node;
        }

        public static T FirstSibling<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)

            // Optimization: try node.Parent.FirstChild
            var parent = node.Parent;
            if (parent != null)
            {
                return parent.FirstChild;
            }

            // Walk predecessors
            for (T prev; (prev = node.PreviousSibling) != null; node = prev) ;
            return node;
        }

        // [WMR] Null propagation operator requires additional generic class constraint
        // public static T LastChild<T>(this T node) where T : class, INavTreeNode<T> => node?.FirstChild?.LastSibling();

        public static T LastChild<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            var first = node.FirstChild;
            return first != null ? first.LastSibling() : default(T);
        }

        // For trees that also store parent node references, the following algorithm outperforms a nested foreach loop
        // The classical trade-off: performance vs. memory
        // nested foreach     => O(n^2)  requires Children = FirstChild + NextSibling
        // DFS on linked tree => O(n)    requires Parent + FirstChild + NextSibling

        /// <summary>Perform depth-first recursion on a linked tree structure.</summary>
        /// <typeparam name="T">The node type.</typeparam>
        /// <param name="root">The recursion root node.</param>
        /// <param name="getFirstChild">Function that returns the first child of a given tree node.</param>
        /// <param name="getNextSibling">Function that returns the next sibling of a given tree node.</param>
        /// <param name="getParent">Function that returns the parent of a given tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> that returns all tree nodes in depth-first order.</returns>
        /// <remarks>The implementation is O(n).</remarks>
        public static IEnumerable<T> DepthFirst<T>(T root, Func<T, T> getFirstChild, Func<T, T> getNextSibling, Func<T, T> getParent)
        {
            if (root == null) { throw new ArgumentNullException("root"); } // nameof(root)
            if (getFirstChild == null) { throw new ArgumentNullException("getFirstChild"); } // nameof(getFirstChild)
            if (getNextSibling == null) { throw new ArgumentNullException("getNextSibling"); } // nameof(getNextSibling)
            if (getParent == null) { throw new ArgumentNullException("getParent"); } // nameof(getParent)

            var node = root;
            while (true)
            {
                yield return node;
                var child = getFirstChild(node);
                if (child != null)
                {
                    node = child; // Walk down
                }
                else
                {
                    while (getNextSibling(node) == null)
                    {
                        if (Object.ReferenceEquals(node, root))
                        {
                            yield break;
                        }
                        node = getParent(node);     // Walk up ...
                        // [WMR] Handle "root" node with siblings (tree fragment)
                        if (Object.ReferenceEquals(node, root))
                        {
                            yield break;
                        }
                    }
                    node = getNextSibling(node);    // ... and right
                }

            }
        }

        public static IEnumerable<T> DepthFirst<T>(T root) where T : INavTreeNode<T>
            => DepthFirst(root, node => node.FirstChild, node => node.NextSibling, node => node.Parent);

        #endregion

        #region Axes navigation

        /// <summary>Enumerate the direct children of the current node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Children<T>(this T node) where T : INavTreeNode<T>
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

        /// <summary>Enumerate the descendants of the current node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Descendants<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException(nameof(node)); }
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

        /// <summary>Enumerate the current node and it's descendants.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> DescendantsAndSelf<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            return DepthFirst(node);
        }

        /// <summary>Enumerate the ancestors of the current node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Ancestors<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.Parent; node != null; node = node.Parent) { yield return node; }
            yield break;
        }

        /// <summary>Enumerate the current node and it's ancestors.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> AncestorsAndSelf<T>(this T node) where T : INavTreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (; node != null; node = node.Parent) { yield return node; }
            yield break;

        }

        /// <summary>Enumerate the siblings preceding the current node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> PrecedingSiblings<T>(this T node) where T : INavTreeLeafNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.PreviousSibling; node != null; node = node.PreviousSibling) { yield return node; }
            yield break;
        }

        /// <summary>Enumerate the siblings following the current node.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="node">A tree node instance.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> FollowingSiblings<T>(this T node) where T : INavTreeLeafNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (node = node.NextSibling; node != null; node = node.NextSibling) { yield return node; }
            yield break;
        }

#if false
        public static IEnumerable<T> Siblings<T>(this T node) where T : INavTreeLeafNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            for (var sibling = node.FirstSibling(); sibling != null; sibling = sibling.NextSibling)
            {
                if (!object.ReferenceEquals(node, sibling)) { yield return sibling; }
            }
            yield break;
        }
#endif

        #endregion

#if false
        // TODO: Move to different (extension) class
        #region FHIR Name matching

        private const string PolymorphicNodeNameSuffix = "[x]";
        public const string NodeNameWildcard = "*";

        public static bool IsMatch<T>(this T node, string nodeName) where T : ITreeNode<T>
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            if (string.IsNullOrEmpty(nodeName)) { throw new ArgumentNullException(nameof(nodeName)); }

            return nodeName == NodeNameWildcard | node.Name == nodeName | IsPolymorphicMatch(node, nodeName);
        }

        private static bool IsPolymorphicMatch<T>(T node, string nodeName) where T : ITreeNode<T>
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
#endif
    }
}
