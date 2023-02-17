/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class TypeOperators
    {
        public static bool Is(this ITypedElement focus, string type)
        {
            if (focus.InstanceType != null)
            {
                return Is(focus.InstanceType, type);     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on untyped data");
        }

        public static bool Is(string instanceType, string declaredType)
        {
            // Bit of a hack, this hardwires the FhirPath implementation to FHIR
            if (!instanceType.Contains(".")) instanceType = "FHIR." + instanceType;
            if (declaredType.Contains("."))
                return instanceType == declaredType;
            else
            {
                return instanceType == "System." + declaredType ||
                        instanceType == "FHIR." + declaredType;
            }
        }

        public static IEnumerable<ITypedElement> FilterType(this IEnumerable<ITypedElement> focus, string typeName)
            => focus.Where(item => item.Is(typeName));

        public static ITypedElement CastAs(this ITypedElement focus, string typeName)
            => focus.Is(typeName) ? focus : null;
    }
}
