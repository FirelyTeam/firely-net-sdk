/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Utility
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Transforms an <see cref="IEnumerable{T}"/> with nullable contents <c>T?</c> into a (possibly smaller)
        /// enumerable of the non-nullable type <c>T</c>.
        /// </summary>
        public static IEnumerable<T> WithValues<T>(this IEnumerable<T?> nullables) where T : struct =>
            nullables.Where(n => n.HasValue).Select(n => n!.Value);
    }
}

#nullable restore