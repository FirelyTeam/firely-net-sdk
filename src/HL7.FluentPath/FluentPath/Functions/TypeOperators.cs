/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.FluentPath.Support;
using Hl7.ElementModel;

namespace Hl7.FluentPath.Functions
{
    internal static class TypeOperators
    {
        public static bool Is(this IValueProvider focus, string typeName)
        {
            if (focus is ITypeNameProvider)
            {
                return ((ITypeNameProvider)focus).TypeName == typeName;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on data which does not support ITypeNameProvider");
        }

        public static bool Is(this IEnumerable<IValueProvider> f, string typeName)
        {
            var focus = f.First();

            if (focus is ITypeNameProvider)
            {
                return ((ITypeNameProvider)focus).TypeName == typeName;     // I have no information about classes/subclasses
            }
            else
                throw Error.InvalidOperation("Is operator is called on data which does not support ITypeNameProvider");
        }


        public static IEnumerable<IValueProvider> FilterType(this IEnumerable<IValueProvider> focus, string typeName)
        {
            return focus.Where(item => item.Is(typeName));
        }

        public static IValueProvider CastAs(this IValueProvider focus, string typeName)
        {
            if (focus.Is(typeName))
                return focus;
            else
                return null;
        }
    }
}
