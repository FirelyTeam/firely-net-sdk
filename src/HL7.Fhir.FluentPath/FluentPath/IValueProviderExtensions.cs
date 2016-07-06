/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FluentPath
{
    public static class IValueProviderExtensions
    {
        internal static T GetValue<T>(this IValueProvider val, string name)
        {
            if (val == null) throw Error.ArgumentNull(name);
            if (val.Value == null) throw Error.ArgumentNull(name + ".Value");
            if (!(val is T)) throw Error.Argument(name + " must be of type " + typeof(T).Name);

            return (T)val.Value;
        }

        // FluentPath toInteger() function
        public static long? ToInteger(this IValueProvider focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is long)
                return (long)val;
            else if (val is string)
            {
                try
                {
                    return XmlConvert.ToInt64((string)val);
                }
                catch
                {
                    return null;
                }
            }
            else if(val is bool)
            {
                return (bool)val ? 1L : 0L;
            }

            return null;
        }

        // FluentPath toDecimal() function
        public static decimal? ToDecimal(this IValueProvider focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is decimal)
                return (decimal)val;
            else if (val is string)
            {
                try
                {
                    return XmlConvert.ToDecimal((string)val);
                }
                catch
                {
                    return null;
                }
            }
            else if (val is bool)
            {
                return (bool)val ? 1m : 0m;
            }

            return null;
        }


        // FluentPath toString() function
        public static string ToStringRepresentation(this IValueProvider focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is string)
                return (string)val;
            else if (val is long)
                return XmlConvert.ToString((long)val);
            else if (val is decimal)
                return XmlConvert.ToString((decimal)val);
            else if (val is bool)
                return (bool)val ? "true" : "false";

            return null;
        }

        //TODO: Implement latest STU3 decisions around empty strings, start > length etc
        public static IValueProvider Substring(this IValueProvider focus, long start, long? length)
        {
            var str = focus.GetValue<string>("focus");

            if (length.HasValue)
                return new ConstantValue(str.Substring((int)start, (int)length.Value));
            else
                return new ConstantValue(str.Substring((int)start));
        }


        public static IValueProvider StartsWith(this IValueProvider focus, string prefix)
        {
            var str = focus.GetValue<string>("focus");

            return new ConstantValue(str.StartsWith(prefix));
        }

        public static IValueProvider Or(this IValueProvider left, IValueProvider right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal || rVal);
        }

        public static IValueProvider And(this IValueProvider left, IValueProvider right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal && rVal);

        }


        public static IValueProvider Xor(this IValueProvider left, IValueProvider right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal ^ rVal);
        }

        public static IValueProvider Implies(this IValueProvider left, IValueProvider right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(!lVal || rVal);
        }



        public static IValueProvider Add(this IValueProvider left, IValueProvider right)
        {
            return left.math((a,b) => a+b, right);
        }

        public static IValueProvider Sub(this IValueProvider left, IValueProvider right)
        {
            return left.math((a,b) => a-b, right);
        }

        public static IValueProvider Mul(this IValueProvider left, IValueProvider right)
        {
            return left.math((a,b) => a*b, right);
        }

        public static IValueProvider Div(this IValueProvider left, IValueProvider right)
        {
            return left.math((a,b) => a/b, right);
        }

        private static IValueProvider math(this IValueProvider left, Func<dynamic,dynamic,object> f, IValueProvider right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("Operands must both be values");
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands must be of the same type");

            return new ConstantValue(f(left.Value, right.Value));
        }

        public static bool IsEqualTo(this IValueProvider left, IValueProvider right)
        {
            // Compare primitives
            if (left.Value != null && right.Value != null)
                return Object.Equals(left.Value, right.Value);

            // Compare complex types
            var childrenL = left.Children();
            var childrenR = right.Children();

            if (childrenL.Any() && childrenR.Any())
                return childrenL.IsEqualTo(childrenR).Value;    // NOTE: Assumes null will never be returned when any() children exist

            // Else, we're comparing a complex to a primitive which (probably) should return false
            return false;
        }

        public static bool IsEquivalentTo(this IValueProvider left, IValueProvider right)
        {
            // Exception: In equality comparisons, the "id" elements do not need to be equal
            throw new NotImplementedException();
        }

        public static bool GreaterOrEqual(this IValueProvider left, IValueProvider right)
        {
            return left.IsEqualTo(right) || left.compare(Operator.GreaterThan, right);
        }

        public static bool LessOrEqual(this IValueProvider left, IValueProvider right)
        {
            return left.IsEqualTo(right) || left.compare(Operator.LessThan, right);
        }

        public static bool LessThan(this IValueProvider left, IValueProvider right)
        {
            return left.compare(Operator.LessThan, right);
        }

        public static bool GreaterThan(this IValueProvider left, IValueProvider right)
        {
            return left.compare(Operator.GreaterThan, right);
        }

        private static bool compare(this IValueProvider left, Operator comp, IValueProvider right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("'{0)' requires both operands to be primitives".FormatWith(comp));
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands to '{0}' must be of the same type".FormatWith(comp));

            if (left.Value is string)
            {
                var result = String.Compare((string)left.Value, (string)right.Value);
                if (comp == Operator.LessThan) return result == -1;
                if (comp == Operator.GreaterThan) return result == 1;
            }
            else
            {
                if (comp == Operator.LessThan) return (dynamic)left.Value < (dynamic)right.Value;
                if (comp == Operator.GreaterThan) return (dynamic)left.Value > (dynamic)right.Value;
            }

            throw Error.InvalidOperation("Comparison failed on operator '{0}'".FormatWith(comp));
        }
    }
}
