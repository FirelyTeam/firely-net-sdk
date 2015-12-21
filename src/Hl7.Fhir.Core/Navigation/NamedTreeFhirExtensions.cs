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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public static class NamedTreeFhirExtensions
    {
        private const string POLYMORPHICNAMESUFFIX = "[x]";

        public static bool IsMatch<T>(this T tree, string name) where T : INamedTree
        {
            if (tree == null) { throw Error.ArgumentNull("tree"); } // nameof(tree)
            if (string.IsNullOrEmpty(name)) { throw Error.ArgumentNull("name"); } // nameof(name)

            return tree.Name == name | isPolymorphicMatch(tree, name);
        }

        private static bool isPolymorphicMatch<T>(T tree, string name) where T : INamedTree
        {
            if (name.EndsWith(POLYMORPHICNAMESUFFIX))
            {
                var prefixLength = name.Length - POLYMORPHICNAMESUFFIX.Length;
                return String.Compare(tree.Name, 0, name, 0, Math.Max(0, prefixLength)) == 0
                    && IsValidTypeName(tree.Name.Substring(prefixLength));
            }
            return false;
        }

        static bool IsValidTypeName(string name)
        {
            // EK: Only way is to use ModelInfo. If you don't want that dependency here, we should move the
            // FHIR extensions to a more "fhiry" place
            return ModelInfo.IsDataType(name) || ModelInfo.IsPrimitive(name.ToLower());
        }
    }
}
