using Hl7.Fhir.FluentPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Functions
{
    public static class EqualityOperators
    {
        public static bool? IsEqualTo(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            if (left.Count() != right.Count()) return false;

            return left.Zip(right, (l, r) => l.IsEqualTo(r)).All(x => x);
        }

        public static bool IsEqualTo(this IValueProvider left, IValueProvider right)
        {
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

            // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
            var childrenL = left.Children();
            var childrenR = right.Children();

            if (childrenL.Any() && childrenR.Any())
                return childrenL.IsEqualTo(childrenR).Value;    // NOTE: Assumes null will never be returned when any() children exist

            // Else, we're comparing a complex to a primitive which (probably) should return false
            return false;
        }

        public static bool IsEqualTo(this string left, string right)
        {
            return left == right;
        }

        public static bool IsEqualTo(this bool left, bool right)
        {
            return left == right;
        }

        public static bool IsEqualTo(this long left, long right)
        {
            return left == right;
        }

        public static bool IsEqualTo(this decimal left, decimal right)
        {
            // return left.ToString() == right.ToString();  -- this is what the spec says, but that's incorrect
            return left == right;
        }


        //public static IEnumerable<IValueProvider> IsEquivalentTo(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        //{
        //    if (!left.Any() && !right.Any()) return FhirValueList.Create(true);
        //    if (left.Count() != right.Count()) return FhirValueList.Create(false);

        //    return FhirValueList.Create(left.All((IValueProvider l) => right.Any(r => l.IsEquivalentTo(r))));
        //}


    }
}
