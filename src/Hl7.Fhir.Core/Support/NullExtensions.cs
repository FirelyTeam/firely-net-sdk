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
            if (list == null) return true;

            return list.Count == 0;
        }

        public static bool IsNullOrEmpty(this Primitive element)
        {
            if (element == null) return true;

            if (element.ObjectValue == null) return true;

            return false;
        }

        // [WMR] Following method operates on any type
        // Not an extension method, to prevent global namespace pollution

        /// <summary>Utility function to simulate C# 6 null propagation operator ?.</summary>
        /// <typeparam name="TInstance">The type of the current instance.</typeparam>
        /// <typeparam name="TValue">The return type of the specified projection.</typeparam>
        /// <param name="instance">An object instance.</param>
        /// <param name="projection">A function from <typeparamref name="TInstance"/> to <typeparamref name="TValue"/>.</param>
        /// <param name="defaultValue">The value to return when the instance equals <c>null</c>.</param>
        /// <returns>A value of type <typeparamref name="TValue"/>.</returns>
        public static TValue IfNotNull<TInstance, TValue>(TInstance instance, Func<TInstance, TValue> projection, TValue defaultValue = default(TValue))
        {
            return instance != null ? projection(instance) : defaultValue;
        }

    }
}
