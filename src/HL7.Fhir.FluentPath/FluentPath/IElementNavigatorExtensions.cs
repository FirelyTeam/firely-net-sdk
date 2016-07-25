/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{
    public static class IFluentPathElementExtensions
    {
        public static IEnumerable<IElementNavigator> EnumerateChildrenByName(this IElementNavigator element, string name)
        {
            var result = element.GetChildrenByName(name);

            // If we are at a resource, we should match a path that is possibly not rooted in the resource
            // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")        
            if (!result.Any() && char.IsUpper(name[0])) 
            {
                // Probably a resource name ;-)
                // If we match the name of the resource, return all its children
                if(element is ITypeNameProvider)
                {
                    if (((ITypeNameProvider)element).TypeName == name)
                    {
                        return new List<IElementNavigator>() { element };
                    }
                }
                else
                {
                    throw Error.InvalidOperation("Cannot verify whether the root object is of type '{0}'. ".FormatWith(name) +
                        "You could try leaving out the resource name of the expression.");
                }
            }

            return result;
        }

        public static IEnumerable<IElementNavigator> EnumerateChildrenByName(this IEnumerable<IElementNavigator> elements, string name)
        {
            return elements.SelectMany(e => e.EnumerateChildrenByName(name));
        }
        
    }
}
