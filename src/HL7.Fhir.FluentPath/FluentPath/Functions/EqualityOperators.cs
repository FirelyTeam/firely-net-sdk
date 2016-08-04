/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;
using Furore.MetaModel;

namespace Hl7.Fhir.FluentPath.Functions
{
    internal static class EqualityOperators
    {
        public static bool IsEqualTo(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            if (left.Count() != right.Count()) return false;

            return left.Zip(right, (l, r) => l.IsEqualTo(r)).All(x => x);
        }

        public static bool IsEqualTo(this IValueProvider left, IValueProvider right)
        {
            // If the values have names, compare them
            if (left is INameProvider && right is INameProvider)
            {
                var lNP = (INameProvider)left;
                var rNP = (INameProvider)right;

                if (lNP.Name != rNP.Name) return false;
            }

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
                else if (l.GetType() == typeof(Time) && r.GetType() == typeof(Time))
                    return (Time)l == (Time)r;
                else if (l.GetType() == typeof(PartialDateTime) && r.GetType() == typeof(PartialDateTime))
                    return (PartialDateTime)l == (PartialDateTime)r;
                else
                    return false;
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
                var childrenL = left.childrenOrEmpty();
                var childrenR = right.childrenOrEmpty();

                return childrenL.IsEqualTo(childrenR);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }
        }


        public static bool IsEquivalentTo(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            if (left.Count() != right.Count()) return false;

            return left.All(l => right.Any(r => l.IsEquivalentTo(r)));
        }


        public static bool IsEquivalentTo(this IValueProvider left, IValueProvider right)
        {
            // If the values have names, compare them
            if (left is INameProvider && right is INameProvider)
            {
                var lNP = (INameProvider)left;
                var rNP = (INameProvider)right;

                if (lNP.Name != rNP.Name) return false;

                if (lNP.Name == "id") return true;      // don't compare 'id' elements
            }

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
                else if (l.GetType() == typeof(Time) && r.GetType() == typeof(Time))
                    return ((Time)l).IsEquivalentTo((Time)r);
                else if (l.GetType() == typeof(PartialDateTime) && r.GetType() == typeof(PartialDateTime))
                    return ((PartialDateTime)l).IsEquivalentTo((PartialDateTime)r);
                else
                    return false;
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above

                var childrenL = left.childrenOrEmpty();
                var childrenR = right.childrenOrEmpty();

                return childrenL.IsEquivalentTo(childrenR);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }
        }



        private static IEnumerable<IValueProvider> childrenOrEmpty(this IValueProvider focus)
        {
            if (focus is IElementNavigator)
            {
                return ((IElementNavigator)focus).Children();
            }

            return FhirValueList.Empty;
        }

        public static bool IsEquivalentTo(this string a, string b)
        {
            if (b == null) return false;

#if NETSTANDARD
            // todo: check equivalence
            return String.Compare(a, b) == 0;
#else
            return String.Compare(a, b, CultureInfo.InvariantCulture, 
                CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
#endif
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

        internal class ValueProviderEqualityComparer : IEqualityComparer<IValueProvider>
        {
            public bool Equals(IValueProvider x, IValueProvider y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return x.IsEqualTo(y);
            }

            public int GetHashCode(IValueProvider value)
            {
                var result = value.Value != null ? value.Value.GetHashCode() : 0;

                if (value is IElementNavigator)
                {
                    var childnames = String.Concat(((IElementNavigator)value).GetChildNames());
                    if (!String.IsNullOrEmpty(childnames))
                        result ^=  childnames.GetHashCode();
                }

                return result;
            }
        }
    }
}
