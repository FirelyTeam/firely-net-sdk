/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Support
{
    public static class NullExtensions
    {
        public static bool IsNullOrEmpty(this IList list)
        {
            if (list == null) { return true; }

            return list.Count == 0;
        }

        public static bool IsNullOrEmpty(this Primitive element)
        {
            if (element == null) { return true; }

            if (element.ObjectValue != null) { return false; }

            // Recursively check all child elements
            return element.Children.All(e => e.IsNullOrEmpty());
        }

        /// <summary>Determines if the element is <c>null</c> or empty.</summary>
        /// <param name="element">A <see cref="Base"/> instance.</param>
        /// <returns>
        /// Returns <c>true</c> if the element is <c>null</c>, or if all child elements are <c>null</c> or empty.
        /// Returns <c>false</c> if the element has one or more non-empty child elements.
        /// </returns>
        public static bool IsNullOrEmpty(this Base element)
        {
            // Actually, we shoud really check all members...
            // return element == null;

            // [WMR 20161019] New
            if (element == null) { return true; }

            // If the element is a primitive, then check ObjectValue
            var p = element as Primitive;
            if (p != null) { return p.IsNullOrEmpty(); }

            // Recursively check all child elements
            return element.Children.All(e => e.IsNullOrEmpty());
        }

    }
}
