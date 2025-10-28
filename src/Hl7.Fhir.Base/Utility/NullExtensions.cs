/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections;
using System.Linq;

namespace Hl7.Fhir.Utility
{
    public static class NullExtensions
    {
        /// <summary>Determines if the list is <c>null</c> or empty.</summary>
        public static bool IsNullOrEmpty(this IList list) => list == null || list.Count == 0;

        /// <summary>
        /// Determines if the element is <c>null</c> or empty.
        /// For primitive values, verifies that the value equals <c>null</c>.
        /// For primitive string values, verifies that the string value is <c>null</c> or empty.
        /// Recursively verifies that all child elements are <c>null</c> or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this Base element)
        {
            if (element == null) { return true; }

            var isEmpty = element is IValue<string> ss ? string.IsNullOrEmpty(ss.Value)
                : element is not PrimitiveType pp || pp.JsonValue == null;

            // Note: Children collection includes extensions
#pragma warning disable CS0618 // Type or member is obsolete
            return isEmpty && element.Children().All(c => c.IsNullOrEmpty());
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}