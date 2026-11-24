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

/// <summary>
/// A sort() key with its direction. <paramref name="EmptyLast"/> says that items with an empty key go after the others
/// (the FHIRPath spec treats an empty key as lower than all values, so that is the case for <c>desc</c>).
/// </summary>
internal record OrderedNode(PrimitiveType Primitive, PocoNode Parent, int? Index, string Name = null, bool Descending = false, bool EmptyLast = false) : PrimitiveNode(Primitive, Parent, Index, Name)
{
    /// <summary>
    /// The direction used for a sort() key prefixed with '-' (e.g. <c>sort(-family)</c>): descending, but with
    /// empty keys first, which is what negating the key and sorting ascending would give.
    /// </summary>
    internal const string MINUS_DIRECTION = "minus";

    /// <summary>
    /// Wraps a sort key so the sort() comparer knows in which direction to order it. Complex types that have a System
    /// type equivalent that can be ordered (Quantity) are converted to that System type first. Other keys cannot be
    /// ordered and are returned unchanged; the sort() comparer reports them as an error.
    /// </summary>
    internal static PocoNode FromPrimitiveNode(PocoNode node, bool descending = false, bool emptyLast = false) => node.Poco switch
    {
        PrimitiveType primitive => new OrderedNode(primitive, node.Parent, node.Index, node.Name, descending, emptyLast),
        P.IToSystemPrimitive tsp when tsp.TryConvertToSystemType(out var value) && value is P.Quantity quantity =>
            new OrderedNode(PrimitiveNode.InferFromValue(quantity), node.Parent, node.Index, node.Name, descending, emptyLast),
        _ => node
    };
}

