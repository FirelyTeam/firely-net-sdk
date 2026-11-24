/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Expressions;

internal record OrderedNode(PrimitiveType Primitive, PocoNode Parent, int? Index, string Name = null, bool Descending = false) : PrimitiveNode(Primitive, Parent, Index, Name)
{
    /// <summary>
    /// Wraps a sort key so the sort() comparer knows in which direction to order it. Complex types that have a System
    /// type equivalent that can be ordered (Quantity) are converted to that System type first. Other keys cannot be
    /// ordered and are returned unchanged.
    /// </summary>
    internal static PocoNode FromPrimitiveNode(PocoNode node, bool descending = false) => node.Poco switch
    {
        PrimitiveType primitive => new OrderedNode(primitive, node.Parent, node.Index, node.Name, descending),
        P.IToSystemPrimitive tsp when tsp.TryConvertToSystemType(out var value) && value is P.Quantity quantity =>
            new OrderedNode(PrimitiveNode.InferFromValue(quantity), node.Parent, node.Index, node.Name, descending),
        _ => node
    };
}

