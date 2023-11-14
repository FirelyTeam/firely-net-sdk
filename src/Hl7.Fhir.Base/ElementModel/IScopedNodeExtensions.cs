/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class IScopedNodeExtensions
    {
        /// <summary>
        /// Converts a <see cref="IScopedNode"/> to a <see cref="ITypedElement"/>.
        /// </summary>
        /// <param name="node">An <see cref="IScopedNode"/> node</param>
        /// <returns>An implementation of <see cref="ITypedElement"/></returns>
        /// <remarks>Be careful when using this method, the returned <see cref="ITypedElement"/> does not implement
        /// the methods <see cref="ITypedElement.Location"/> and <see cref="ITypedElement.Definition"/>.    
        /// </remarks>
        public static ITypedElement AsTypedElement(this IScopedNode node) =>
            node is ITypedElement ite ? ite : new ScopedNodeToTypedElementAdapter(node);

        /// <summary>
        /// Returns the parent resource of this node, or null if this node is not part of a resource.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<IScopedNode> Children(this IEnumerable<IScopedNode> nodes, string? name = null) =>
           nodes.SelectMany(n => n.Children(name));

    }
}

#nullable restore