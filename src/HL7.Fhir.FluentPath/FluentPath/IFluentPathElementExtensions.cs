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

namespace Hl7.Fhir.FluentPath
{
    public static class IFluentPathElementExtensions
    {
        public static IEnumerable<IFluentPathElement> Children(this IFluentPathElement element, string name)
        {
            var result = element.Children().Where(c => c.IsMatch(name)).Select(c => c.Child);

            // If we are at a resource, we should match a path that is possibly not rooted in the resource
            // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")        
            if (!result.Any()) 
            {
                var resourceRoot = element.Children().SingleOrDefault(node => Char.IsUpper(node.Name[0]));
                if(resourceRoot != null)
                    result = resourceRoot.Child.Children(name);
            }

            return result;
        }

        public static IEnumerable<IFluentPathElement> Descendants(this IFluentPathElement element)
        {
            //TODO: Don't think this is performant with these nested yields
            foreach (var child in element.Children())
            {
                yield return child.Child;
                foreach (var grandchild in child.Child.Descendants()) yield return grandchild;
            }
        }

    }
}
