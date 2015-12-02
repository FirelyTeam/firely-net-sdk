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

        public static T LastChild<T>(this T tree) where T : ILinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            var first = tree.FirstChild;
            return first != null ? first.LastSibling() : default(T);
        }

        /// <summary>Convert the specified item to an <see cref="IEnumerable{T}"/> sequence.</summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="item">An instance of <typeparamref name="T"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> instance.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T item) where T : ILinkedTree<T>
        {
            // return Enumerable.Repeat(item, 1);
            yield return item;
        }

        /// <summary>Perform depth-first recursion on a <see cref="DoublyLinkedTree"/> structure.</summary>
        /// <typeparam name="T">The tree type; should implement <see cref="IDoublyLinkedTree{T}"/>.</typeparam>
        /// <param name="tree">The start item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        /// <remarks>The implementation is O(n).</remarks>
        private static IEnumerable<T> DepthFirst<T>(T tree) where T : IDoublyLinkedTree<T>
        {
            return LinkedTreeHelpers.DepthFirst(tree, t => t.FirstChild, t => t.NextSibling, t => t.Parent);
        }

        #region Axes navigation

        /// <summary>Enumerate the direct children of the current tree item.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> Children<T>(this T tree) where T : ILinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)

            var child = tree.FirstChild;
            while (child != null)
            {
                yield return child;
                child = child.NextSibling;
            }
            yield break;
        }

        /// <summary>Enumerate the direct children of the current tree item that comply with the specified predicate.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <param name="predicate">A predicate to select the relevant tree items.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> Children<T>(this T tree, Func<T, bool> predicate) where T : ILinkedTree<T>
        {
            return tree.Children().Where(predicate);
        }

        private readonly static StringComparer nameComparer = StringComparer.Ordinal;

        /// <summary>Enumerate the direct children of the current tree item with the specified name.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <param name="name">The name of the target item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> Children<T>(this T tree, string name) where T : ILinkedTree<T>
        {
            return tree.Children().Where(n => nameComparer.Compare(n.Name, name) == 0);
        }

        /// <summary>Returns the first child of the current tree item that complies with the specified predicate, or <c>null</c>.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <param name="predicate">A predicate to select the relevant tree items.</param>
        /// <returns>An instance of <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T FirstChild<T>(this T tree, Func<T, bool> predicate) where T : ILinkedTree<T> { return tree.Children(predicate).FirstOrDefault(); }

        /// <summary>Returns the first child of the current tree item with the specified name, or <c>null</c>.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <param name="name">The name of the target item.</param>
        /// <returns>An instance of <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T FirstChild<T>(this T tree, string name) where T : ILinkedTree<T> { return tree.Children(name).FirstOrDefault(); }

        /// <summary>Enumerate the descendants of the current tree item.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> Descendants<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
#if true
            // Skip the root item
            return DepthFirst(tree).Skip(1);
#else
            // [WMR] Nested foreach is inefficient O(n^2)
            // Note: C# should implement "yield foreach" - maybe in C# 7?

            foreach (var child in tree.Children())
            {
                foreach (var item in child.Descendants())
                {
                    yield return item;
                }
                yield return child;
            }
            yield break;
#endif
        }

        /// <summary>Enumerate the current tree item and it's descendants.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> DescendantsAndSelf<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            return DepthFirst(tree);
        }

        /// <summary>Enumerate the ancestors of the current tree item.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> Ancestors<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            for (tree = tree.Parent; tree != null; tree = tree.Parent) { yield return tree; }
            yield break;
        }

        /// <summary>Enumerate the current tree item and it's ancestors.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> AncestorsAndSelf<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            for (; tree != null; tree = tree.Parent) { yield return tree; }
            yield break;

        }

        /// <summary>Enumerate the siblings preceding the current tree item.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> PrecedingSiblings<T>(this T tree) where T : IDoublyLinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            for (tree = tree.PreviousSibling; tree != null; tree = tree.PreviousSibling) { yield return tree; }
            yield break;
        }

        /// <summary>Enumerate the siblings following the current tree item.</summary>
        /// <typeparam name="T">The tree type.</typeparam>
        /// <param name="tree">A tree item.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence.</returns>
        public static IEnumerable<T> FollowingSiblings<T>(this T tree) where T : ILinkedTree<T>
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            for (tree = tree.NextSibling; tree != null; tree = tree.NextSibling) { yield return tree; }
            yield break;
        }

        #endregion

        // TODO: Move to different (extension) class

        #region FHIR Name matching

        private const string PolymorphicNameSuffix = "[x]";
        public const string NameWildcard = "*";

        public static bool IsMatch<T>(this T tree, string name) where T : ITree
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException("name"); } // nameof(name)

            return name == NameWildcard | tree.Name == name | IsPolymorphicMatch(tree, name);
        }

        private static bool IsPolymorphicMatch<T>(T tree, string name) where T : ITree
        {
            if (name.EndsWith(PolymorphicNameSuffix))
            {
                var prefixLength = name.Length - PolymorphicNameSuffix.Length;
                return String.Compare(tree.Name, 0, name, 0, Math.Max(0, prefixLength)) == 0
                    && IsValidTypeName(tree.Name.Substring(prefixLength + 1));
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
