/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.FhirPath
{
    public static class IFhirPathNodeExtensions
    {
        private const string POLYMORPHICNAMESUFFIX = "[x]";

        public static bool IsMatch(this IFhirPathNode node, string name)
        {
            if (node == null) { throw Error.ArgumentNull("node"); } // nameof(tree)
            if (string.IsNullOrEmpty(name)) { throw Error.ArgumentNull("name"); } // nameof(name)

            return node.Name == name | isPolymorphicMatch(node, name);
        }

        private static bool isPolymorphicMatch(IFhirPathNode node, string name)
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
