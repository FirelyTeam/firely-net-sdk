/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Utility
{
    public static class PredicateExtensions
    {
        public static Predicate<T> And<T>(this Predicate<T> first, Predicate<T> second) => v => first(v) && second(v);
        public static Predicate<T> Or<T>(this Predicate<T> first, Predicate<T> second) => v => first(v) || second(v);
        public static Predicate<T> Not<T>(this Predicate<T> first) => v => !first(v);
    }
}
