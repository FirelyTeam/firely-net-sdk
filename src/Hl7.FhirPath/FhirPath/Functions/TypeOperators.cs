/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class TypeOperators
    {
        public static bool Is(this IElementNavigator focus, string type)
        {
            if (focus.Type != null)
            {
                return focus.Type == type;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on untyped data");
        }

        public static bool Is(this IEnumerable<IElementNavigator> f, string type)
        {
            var focus = f.First();

            if (focus.Type == null)
            {
                return focus.Type == type;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on untyped data");
        }


        public static IEnumerable<IElementNavigator> FilterType(this IEnumerable<IElementNavigator> focus, string typeName)
        {
            return focus.Where(item => item.Is(typeName));
        }

        public static IElementNavigator CastAs(this IElementNavigator focus, string typeName)
        {
            if (focus.Is(typeName))
                return focus;
            else
                return null;
        }
    }
}
