using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public class TreeIterators
    {
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
#if true
                    while (true)
                    {
                        // [WMR] Handle "root" node with siblings (tree fragment)
                        if (Object.ReferenceEquals(node, root))
                        {
                            yield break;
                        }
                        if (getNextSibling(node) != null) { break; }
                        node = getParent(node);     // Walk up ...

                    };
#else
                    while (getNextSibling(node) == null)
                    {
                        if (Object.ReferenceEquals(node, root))
                        {
                            yield break;
                        }
                        node = getParent(node);     // Walk up ...
                    }
#endif
                    node = getNextSibling(node);    // ... and right
                }

            }
        }
    }
}
