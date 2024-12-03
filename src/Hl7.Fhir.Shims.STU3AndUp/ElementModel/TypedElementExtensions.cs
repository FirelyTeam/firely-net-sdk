/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Model;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementExtensions
    {
        [TemporarilyChanged] // This should be restored to use the original ITE stack.
        public static ITypedElement ToTypedElement(this Base @base, string? rootName = null)
            => ToElementNode(@base, rootName);

        private static SinglePocoElementNode ToElementNode(this Base @base, string? rootName = null)
            => @base is PrimitiveType primitive 
                ? new SinglePrimitiveElementNode(primitive, rootName) 
                : new SinglePocoElementNode(@base, null, null, rootName);
    }
}
#nullable restore