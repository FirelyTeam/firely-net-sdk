/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hl7.FhirPath;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Functions
{
    internal static class EqualityOperators
    {
        public static bool IsEqualTo(this IEnumerable<ITypedElement> left, IEnumerable<ITypedElement> right, bool compareNames = false)
        {
            var r = right.GetEnumerator();

            foreach (var l in left)
            {
                if (!r.MoveNext()) return false;        // number of children not the same            
                if (!l.IsEqualTo(r.Current, compareNames)) return false;
            }

            if (r.MoveNext())
                return false;   // number of children not the same
            else
                return true;
        }

        public static bool IsEqualTo(this ITypedElement left, ITypedElement right, bool compareNames = false)
        {
            if (compareNames && (left.Name != right.Name)) return false;

            var l = left.Value;
            var r = right.Value;

            // Compare primitives (or extended primitives)
            if (l != null && r != null)
            {
                if (l.GetType() == typeof(string) && r.GetType() == typeof(string))
                    return (string)l == (string)r;
                else if (l.GetType() == typeof(bool) && r.GetType() == typeof(bool))
                    return (bool)l == (bool)r;
                else if (l.GetType() == typeof(long) && r.GetType() == typeof(long))
                    return (long)l == (long)r;
                else if (l.GetType() == typeof(decimal) && r.GetType() == typeof(decimal))
                    return (decimal)l == (decimal)r;
                else if (l.GetType() == typeof(long) && r.GetType() == typeof(decimal))
                    return (decimal)(long)l == (decimal)r;
                else if (l.GetType() == typeof(decimal) && r.GetType() == typeof(long))
                    return (decimal)l == (decimal)(long)r;
                else if (l.GetType() == typeof(PartialTime) && r.GetType() == typeof(PartialTime))
                    return (PartialTime)l == (PartialTime)r;
                else if (l.GetType() == typeof(PartialDateTime) && r.GetType() == typeof(PartialDateTime))
                    return (PartialDateTime)l == (PartialDateTime)r;
                else
                    return false;
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
                var childrenL = left.Children();
                var childrenR = right.Children();

                return childrenL.IsEqualTo(childrenR, compareNames:true);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }
        }


    

        public static bool IsEquivalentTo(this IEnumerable<ITypedElement> left, IEnumerable<ITypedElement> right, bool compareNames = false)
        {
            var r = right.ToList();
            int count = 0;

            foreach (var l in left)
            {
                count += 1;
                if (!r.Any(ri => l.IsEquivalentTo(ri, compareNames))) return false;
            }

            if (count != r.Count)
                return false;
            else
                return true;
        }


        public static bool IsEquivalentTo(this ITypedElement left, ITypedElement right, bool compareNames = false)
        {
            if (compareNames && !namesAreEquivalent(left, right)) return false;

            var l = left.Value;
            var r = right.Value;

            // Compare primitives (or extended primitives)
            if (l != null && r != null)
            {
                if (l.GetType() == typeof(string) && r.GetType() == typeof(string))
                    return ((string)l).IsEquivalentTo((string)r);
                else if (l.GetType() == typeof(bool) && r.GetType() == typeof(bool))
                    return (bool)l == (bool)r;
                else if (l.GetType() == typeof(long) && r.GetType() == typeof(long))
                    return (long)l == (long)r;
                else if (l.GetType() == typeof(decimal) && r.GetType() == typeof(decimal))
                    return ((decimal)l).IsEquivalentTo((decimal)r);
                else if (l.GetType() == typeof(long) && r.GetType() == typeof(decimal))
                    return ((decimal)(long)l).IsEquivalentTo((decimal)r);
                else if (l.GetType() == typeof(decimal) && r.GetType() == typeof(long))
                    return ((decimal)l).IsEquivalentTo((decimal)(long)r);
                else if (l.GetType() == typeof(PartialTime) && r.GetType() == typeof(PartialTime))
                    return ((PartialTime)l).IsEquivalentTo((PartialTime)r);
                else if (l.GetType() == typeof(PartialDateTime) && r.GetType() == typeof(PartialDateTime))
                    return ((PartialDateTime)l).IsEquivalentTo((PartialDateTime)r);
                else
                    return false;
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
                var childrenL = left.Children();
                var childrenR = right.Children();

                return childrenL.IsEquivalentTo(childrenR, compareNames: true);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }

            bool namesAreEquivalent(ITypedElement le, ITypedElement ri)
            {
                if (le.Name == "id" && ri.Name == "id") return true;      // don't compare 'id' elements for equivalence
                if (le.Name != ri.Name) return false;

                return true;
            }
        }



        private static IEnumerable<ITypedElement> childrenOrEmpty(this ITypedElement focus)
        {
            return (focus is ITypedElement ien) ? ien.Children() : FhirValueList.Empty;
        }

        public static bool IsEquivalentTo(this string a, string b)
        {
            if (b == null) return false;

            a = a.Trim().ToLowerInvariant();
            b = b.Trim().ToLowerInvariant();

            return a == b;
            //    return String.Compare(a, b, CultureInfo.InvariantCulture,
            //CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
        }

        public static bool IsEquivalentTo(this decimal a, decimal b)
        {
            var prec = Math.Min(a.precision(), b.precision());
            var aR = Math.Round(a, prec);
            var bR = Math.Round(b, prec);

            return aR == bR;
        }

        private static int precision(this decimal a)
        {
            var repr = a.ToString(CultureInfo.InvariantCulture);
            return repr.Length - repr.IndexOf('.') - 1;
        }

        internal class ValueProviderEqualityComparer : IEqualityComparer<ITypedElement>
        {
            public bool Equals(ITypedElement x, ITypedElement y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return x.IsEqualTo(y);
            }

            public int GetHashCode(ITypedElement element)
            {
                var result = element.Value != null ? element.Value.GetHashCode() : 0;

                if (element is ITypedElement)
                {
                    var childnames = String.Concat(((ITypedElement)element).Children().Select(c => c.Name));
                    if (!String.IsNullOrEmpty(childnames))
                        result ^= childnames.GetHashCode();
                }

                return result;
            }
        }
    }
}
