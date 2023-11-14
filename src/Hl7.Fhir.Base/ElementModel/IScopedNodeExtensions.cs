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
        public static IEnumerable<IScopedNode> Children(this IEnumerable<IScopedNode> nodes, string? name = null) =>
           nodes.SelectMany(n => n.Children(name));

        public static ITypedElement AsTypedElement(this IScopedNode node) =>
            node is ITypedElement ite ? ite : new ScopedNodeToTypedElementAdapter(node);

        public static IScopedNode GetRootResource(this IScopedNode node) // TODO
        {
            return null!;
        }
    }
}

#nullable restore