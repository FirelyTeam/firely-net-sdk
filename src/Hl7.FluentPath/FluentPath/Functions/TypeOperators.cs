/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;
using Hl7.ElementModel;
using Furore.Support;

namespace Hl7.FluentPath.Functions
{
    internal static class TypeOperators
    {
        public static bool Is(this IElementNavigator focus, string typeName)
        {
            if (focus.IsTypeProvider())
            {
                return focus.TypeName == typeName;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on data which does not support ITypeNameProvider");
        }

        public static bool Is(this IEnumerable<IElementNavigator> f, string typeName)
        {
            var focus = f.First();

            if (focus.IsTypeProvider())
            {
                return focus.TypeName == typeName;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on data which does not support ITypeNameProvider");
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
