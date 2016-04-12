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
        #region Internal helpers

        /// <summary>Perform depth-first recursion on a <see cref="IDoublyLinkedTree{T}"/> structure.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">The start tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        /// <remarks>The implementation is O(n).</remarks>
        private static IEnumerable<T> DepthFirst<T>(T tree) where T : IDoublyLinkedTree<T>
        {
            return TreeIterators.DepthFirst(tree, t => t.FirstChild, t => t.NextSibling, t => t.Parent);
        }

        #endregion

        // Note: The ToEnumerable extension method is universally applicable on works on any type T.
        // However we specify a generic constraint so as not to pollute the global namespace.

        /// <summary>Convert the specified node to an <see cref="IEnumerable{T}"/> sequence of tree nodes.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">An instance of <typeparamref name="T"/>.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T tree) where T : ILinkedTree<T>
        {
            // return Enumerable.Repeat(node, 1);
            yield return tree;
        }

        /// <summary>Returns the last sibling of the tree node, or the node itself if it has no following siblings.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="ILinkedTree{T}"/> node.</returns>
        public static T LastSibling<T>(this T tree) where T : ILinkedTree<T>
        {
            for (T next; (next = tree.NextSibling) != null; tree = next) ;

            Debug.Assert(tree != null);
            Debug.Assert(tree.NextSibling == null);

            return tree;
        }

        /// <summary>Returns the last child of the tree node, or <c>null</c> of the node has no children.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="ILinkedTree{T}"/> node.</returns>
        public static T LastChild<T>(this T tree) where T : ILinkedTree<T>
        {
            var first = tree.FirstChild;
            return first != null ? first.LastSibling() : default(T);
        }

        /// <summary>Returns the distance from the tree node to the root node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A non-negative integer value.</returns>
        public static int Depth<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            return tree.IsRoot ? 0 : 1 + tree.Parent.Depth();
        }

        #region Axes navigation

        /// <summary>Enumerate the direct children of the current tree node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> Children<T>(this T tree) where T : ILinkedTree<T>
        {
            var child = tree.FirstChild;
            while (child != null)
            {
                yield return child;
                child = child.NextSibling;
            }
            yield break;
        }

        public static bool HasChildren<T>(this T tree) where T : ILinkedTree<T>
        {
            return tree.FirstChild != null;
        }

        /// <summary>Enumerate the direct children of the current tree node that comply with the specified predicate.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <param name="predicate">A predicate to select the relevant tree nodes.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> Children<T>(this T tree, Func<T, bool> predicate) where T : ILinkedTree<T>
        {
            return tree.Children().Where(predicate);
        }

        private readonly static StringComparer nameComparer = StringComparer.Ordinal;

        /// <summary>Enumerate the direct children of the current tree node with the specified name.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <param name="name">The name of the target node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> Children<T>(this T tree, string name) where T : ILinkedTree<T>, INamedTree
        {
            return tree.Children().Where(n => nameComparer.Compare(n.Name, name) == 0);
        }

        /// <summary>Returns the first child of the current tree node that complies with the specified predicate, or <c>null</c>.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <param name="predicate">A predicate to select the relevant tree nodes.</param>
        /// <returns>An tree node of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T FirstChild<T>(this T tree, Func<T, bool> predicate) where T : ILinkedTree<T>
        {
            return tree.Children(predicate).FirstOrDefault();
        }

        /// <summary>Returns the first child of the current tree node with the specified name, or <c>null</c>.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <param name="name">The name of the target node.</param>
        /// <returns>An tree node of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T FirstChild<T>(this T tree, string name) where T : ILinkedTree<T>, INamedTree
        {
            return tree.Children(name).FirstOrDefault();
        }

        /// <summary>Enumerate the descendants of the current tree node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> Descendants<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
#if true
            // Skip the root node
            return DepthFirst(tree).Skip(1);
#else
            // [WMR] Nested foreach is inefficient O(n^2)
            // Note: C# should implement "yield foreach" - maybe in C# 7?

            foreach (var child in tree.Children())
            {
                foreach (var node in child.Descendants())
                {
                    yield return node;
                }
                yield return child;
            }
            yield break;
#endif
        }

        /// <summary>Enumerate the current tree node and it's descendants.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> DescendantsAndSelf<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            return DepthFirst(tree);
        }

        /// <summary>Enumerate the ancestors of the current tree node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> Ancestors<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            for (tree = tree.Parent; tree != null; tree = tree.Parent) { yield return tree; }
            yield break;
        }

        /// <summary>Enumerate the current tree node and it's ancestors.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> AncestorsAndSelf<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            for (; tree != null; tree = tree.Parent) { yield return tree; }
            yield break;

        }

        /// <summary>Enumerate the siblings preceding the current tree node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of tree nodes.</returns>
        public static IEnumerable<T> PrecedingSiblings<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            for (tree = tree.PreviousSibling; tree != null; tree = tree.PreviousSibling) { yield return tree; }
            yield break;
        }

        /// <summary>Enumerate the siblings following the current tree node.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree node.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of linked tree nodes.</returns>
        public static IEnumerable<T> FollowingSiblings<T>(this T tree) where T : ILinkedTree<T>
        {
            for (tree = tree.NextSibling; tree != null; tree = tree.NextSibling) { yield return tree; }
            yield break;
        }

        #endregion
  
    }
}
