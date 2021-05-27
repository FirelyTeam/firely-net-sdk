/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementExtensions
    {
        public static ITypedElement ToTypedElement(this Base @base, string rootName = null) =>
            new PocoElementNode(ModelInfo.ModelInspector, @base, rootName: rootName);
    }
}