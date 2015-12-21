/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Common interface for a tree node.
    /// Derives from <see cref="IEnumerable"/> to provide access to a sequence of all descendant tree nodes.
    /// </summary>
    public interface ITree
    {
        /// <summary>Indicates if the tree node is a root node.</summary>
        /// <value><c>true</c> if the node has a parent node, or <c>false</c> otherwise.</value>
        bool IsRoot { get; }

        /// <summary>Indicates if the tree node is an internal node.</summary>
        /// <value><c>true</c> if the node has at least one child, or <c>false</c> otherwise.</value>
        bool IsInternal { get; }

        /// <summary>Indicates if the tree node is a leaf node.</summary>
        /// <value><c>true</c> if the node has no children, or <c>false</c> otherwise.</value>
        bool IsLeaf { get; }

        /// <summary>Indicates if the tree node is a first sibling node.</summary>
        /// <value><c>true</c> if the node has no previous sibling, or <c>false</c> otherwise.</value>
        bool IsFirstSibling { get; }

        /// <summary>Indicates if the instance is a last sibling node.</summary>
        /// <value><c>true</c> if the node has no following sibling, or <c>false</c> otherwise.</value>
        bool IsLastSibling { get; }
    }

    /// <summary>
    /// Common generic interface for a tree of type <typeparamref name="T"/>.
    /// Derives from <see cref="IEnumerable{T}"/> to provide access to a sequence of all descendant tree nodes.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : ITree&lt;MyTree&gt; { }</code></example>
    public interface ITree<out T> : ITree
    {
    }
}
