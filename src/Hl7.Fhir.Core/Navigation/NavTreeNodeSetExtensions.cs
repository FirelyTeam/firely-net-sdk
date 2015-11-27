using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Extension methods on <see cref="IEnumerable{T}"/> of <see cref="INavTreeNode{T}"/>.
    /// Implemented by lifting existing extension methods on <see cref="INavTreeNode{T}"/>.
    /// </summary>
    public static class NavTreeNodeSetExtensions
    {
        /// <summary>Enumerate the direct children of the specified nodes.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Children<T>(this IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Children());
        }

        /// <summary>Enumerate the descendants of the specified nodes.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Descendants<T>(this IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Descendants());
        }

        /// <summary>Enumerate the specified nodes and their descendants.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> DescendantsAndSelf<T>(this IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.DescendantsAndSelf());
        }

        /// <summary>Enumerate the ancestors of the specified nodes.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> Ancestors<T>(this IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.Ancestors());
        }

        /// <summary>Enumerate the specified nodes and their ancestors.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> AncestorsAndSelf<T>(this IEnumerable<T> nodeSet) where T : INavTreeNode<T>
        {
            return nodeSet.SelectMany(node => node.AncestorsAndSelf());
        }

        /// <summary>Enumerate the siblings preceding the specified nodes.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> PrecedingSiblings<T>(this IEnumerable<T> nodeSet) where T : INavTreeLeafNode<T>
        {
            return nodeSet.SelectMany(node => node.PrecedingSiblings());
        }

        /// <summary>Enumerate the siblings following the specified nodes.</summary>
        /// <typeparam name="T">The type of a tree node.</typeparam>
        /// <param name="nodeSet">A set of tree nodes.</param>
        /// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> FollowingSiblings<T>(this IEnumerable<T> nodeSet) where T : INavTreeLeafNode<T>
        {
            return nodeSet.SelectMany(node => node.PrecedingSiblings());
        }
    }
}
