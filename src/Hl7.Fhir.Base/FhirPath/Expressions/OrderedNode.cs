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

namespace Hl7.FhirPath.Expressions;

internal record OrderedNode(PrimitiveType Primitive, PocoNode Parent, int? Index, string Name = null, bool Descending = false) : PrimitiveNode(Primitive, Parent, Index, Name)
{
    internal static OrderedNode FromPrimitiveNode(PocoNode primitiveNode, bool descending = false) =>
        new((PrimitiveType)primitiveNode.Poco, primitiveNode.Parent, primitiveNode.Index, primitiveNode.Name, descending);
}

