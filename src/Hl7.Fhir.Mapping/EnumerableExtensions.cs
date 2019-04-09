/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Mapping
{
    public static class EnumerableExtensions
    {
        public static void Apply(this IEnumerable<ITypedElement> list, Action<ITypedElement> body)
        {
            foreach (var elem in list) body(elem);
        }

        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source) => new LazyEnumerable<T>(source);
    }
}
