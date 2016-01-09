/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    public static class IFhirPathElementExtensions
    {
        public static IEnumerable<IFhirPathElement> Children(this IFhirPathElement element, string name)
        {
            var result = element.Children().Where(c => c.IsMatch(name));

            // If we are at a resource, we should match a path that is possibly not rooted in the resource
            // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")        
            if (!result.Any()) 
            {
                var resourceRoot = element.Children().SingleOrDefault(node => Char.IsUpper(node.Name[0]));
                if(resourceRoot != null)
                    result = resourceRoot.Children(name);
            }

            return result;
        }

        public static IEnumerable<IFhirPathElement> Descendants(this IFhirPathElement element)
        {
            //TODO: Don't think this is performant with these nested yields
            foreach (var child in element.Children())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

        private const string POLYMORPHICNAMESUFFIX = "[x]";

        public static bool IsMatch(this IFhirPathElement node, string name)
        {
            if (node == null) { throw Error.ArgumentNull("node"); } // nameof(tree)
            if (string.IsNullOrEmpty(name)) { throw Error.ArgumentNull("name"); } // nameof(name)

            return node.Name == name | isPolymorphicMatch(node, name);
        }

        private static bool isPolymorphicMatch(IFhirPathElement node, string name)
        {
            if (name.EndsWith(POLYMORPHICNAMESUFFIX))
            {
                var prefixLength = name.Length - POLYMORPHICNAMESUFFIX.Length;
                return String.Compare(node.Name, 0, name, 0, Math.Max(0, prefixLength)) == 0
                    && isValidTypeName(node.Name.Substring(prefixLength));
            }
            return false;
        }

        private static bool isValidTypeName(string name)
        {
            // EK: Only way is to use ModelInfo. If you don't want that dependency here, we should move the
            // FHIR extensions to a more "fhiry" place
            return ModelInfo.IsDataType(name) || ModelInfo.IsPrimitive(name.ToLower());
        }
    }
}
