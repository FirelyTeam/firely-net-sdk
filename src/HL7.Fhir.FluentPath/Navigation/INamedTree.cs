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
    /// <summary>Common interface for a tree with named nodes.</summary>
    public interface INamedTree : ITree
    {
        /// <summary>The name of the tree node.</summary>
        string Name { get; }
    }

    /// <summary>
    /// Common generic interface for a tree of type <typeparamref name="T"/> with named nodes.
    /// Provides named indexer properties to access child and descendant tree nodes by name.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : INamedTree&lt;MyTree&gt; { }</code></example>
    public interface INamedTree<out T> : ITree<T>, INamedTree
    {
        /// <summary>Indexer property. Returns a sequence of child nodes matching the specified name.</summary>
        /// <param name="name">An node name.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        IEnumerable<T> this[string name] { get; }

        /// <summary>Indexer property. Returns a sequence of descendant nodes matching the specified node path.</summary>
        /// <param name="names">A sequence of node names describing the path of the target node.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        /// <example>
        /// Retrieve descendants of the specified node with path "child.grandchild.greatgrandchild":
        /// <list type="number">
        ///     <item><description>Find child nodes of the specified node with name "child"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "grandchild"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "greatgrandchild"</description></item>
        /// </list>
        /// <code>var result = node["child", "grandchild", "greatgrandchild"];</code>
        /// </example>
        IEnumerable<T> this[params string[] names] { get; }
    }
}
