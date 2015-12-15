/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public static class NamedTreeFhirExtensions
    {
        private const string PolymorphicNameSuffix = "[x]";
        public const string NameWildcard = "*";

        public static bool IsMatch<T>(this T tree, string name) where T : INamedTree
        {
            if (tree == null) { throw new ArgumentNullException("tree"); } // nameof(tree)
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException("name"); } // nameof(name)

            return name == NameWildcard | tree.Name == name | IsPolymorphicMatch(tree, name);
        }

        private static bool IsPolymorphicMatch<T>(T tree, string name) where T : INamedTree
        {
            if (name.EndsWith(PolymorphicNameSuffix))
            {
                var prefixLength = name.Length - PolymorphicNameSuffix.Length;
                return String.Compare(tree.Name, 0, name, 0, Math.Max(0, prefixLength)) == 0
                    && IsValidTypeName(tree.Name.Substring(prefixLength + 1));
            }
            return false;
        }

        static bool IsValidTypeName(string name)
        {
            // TODO: validate typename
            // EK: Only way is to use ModelInfo. If you don't want that dependency here, we should move the
            // FHIR extensions to a more "fhiry" place
            return ModelInfo.IsDataType(name);
            //return char.IsUpper(name, 0);
        }
    }
}
