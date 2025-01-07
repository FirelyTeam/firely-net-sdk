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
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Runtime.CompilerServices;

namespace Hl7.FhirPath.Functions
{ 
    internal static class TypeOperators
    {
        public static bool Is(this PocoNode focus, string type)
        {
            var selfAndBaseClasses = getBaseClasses(focus.Poco.GetType()).Select(t => t.Name).Append(((ITypedElement)focus).InstanceType);
            return selfAndBaseClasses.Any(typeString => Is(typeString, type));
            
            static IEnumerable<Type> getBaseClasses(Type t)
            {
                return t.BaseType == null ? [] : getBaseClasses(t.BaseType).Append(t);
            }
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

        public static IEnumerable<PocoNode> FilterType(this IEnumerable<PocoNode> focus, string typeName)
            => focus.Where(item => item.Is(typeName));

        public static PocoNode CastAs(this PocoNode focus, string typeName)
            => focus.Is(typeName) ? focus : null;
    }
}
