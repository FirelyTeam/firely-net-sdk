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
    public static class IFluentPathValueExtensions
    {
        internal static T GetValue<T>(this IFluentPathValue val, string name)
        {
            if (val == null) throw Error.ArgumentNull(name);
            if (val.Value == null) throw Error.ArgumentNull(name + ".Value");
            if (!(val is T)) throw Error.Argument(name + " must be of type " + typeof(T).Name);

            return (T)val.Value;
        }


        // FluentPath toInteger() function
        public static IFluentPathValue ToInteger(this IFluentPathValue focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is long)
                return new ConstantValue(val);
            else if (val is string)
            {
                long result;
                if (Int64.TryParse((string)val, out result))
                    return new ConstantValue(result);
            }
            else if(val is bool)
            {
                return new ConstantValue((bool)val ? 1 : 0);
            }

            return null;
        }


        // FluentPath toDecimal() function
        public static IFluentPathValue ToDecimal(this IFluentPathValue focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is decimal)
                return new ConstantValue(val);
            else if (val is string)
            {
                decimal result;
                if (Decimal.TryParse((string)val, out result))
                    return new ConstantValue(result);
            }
            else if (val is bool)
            {
                return new ConstantValue((bool)val ? 1m : 0m);
            }

            return null;
        }


        // FluentPath toString() function
        public static IFluentPathValue ToString(this IFluentPathValue focus)
        {
            var val = focus.GetValue<object>("focus");

            if (val is string)
                return new ConstantValue(val);
            else if (val is long)
                return new ConstantValue(XmlConvert.ToString((long)val));
            else if (val is decimal)
                return new ConstantValue(XmlConvert.ToString((decimal)val));
            else if (val is bool)
                return new ConstantValue((bool)val ? "true" : "false");

            return null;
        }


        //TODO: Implement latest STU3 decisions around empty strings, start > length etc
        public static IFluentPathValue Substring(this IFluentPathValue focus, long start, long? length)
        {
            var str = focus.GetValue<string>("focus");

            if (length.HasValue)
                return new ConstantValue(str.Substring((int)start, (int)length.Value));
            else
                return new ConstantValue(str.Substring((int)start));
        }


        public static IFluentPathValue StartsWith(this IFluentPathValue focus, string prefix)
        {
            var str = focus.GetValue<string>("focus");

            return new ConstantValue(str.StartsWith(prefix));
        }


        public static IFluentPathValue Or(this IFluentPathValue left, IFluentPathValue right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal || rVal);
        }

        public static IFluentPathValue And(this IFluentPathValue left, IFluentPathValue right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal && rVal);

        }


        public static IFluentPathValue Xor(this IFluentPathValue left, IFluentPathValue right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(lVal ^ rVal);
        }


        public static IFluentPathValue Implies(this IFluentPathValue left, IFluentPathValue right)
        {
            var lVal = left.GetValue<bool>("left");
            var rVal = right.GetValue<bool>("right");

            return new ConstantValue(!lVal || rVal);
        }



        public static IFluentPathValue Add(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.math((a,b) => a+b, right);
        }

        public static IFluentPathValue Sub(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.math((a,b) => a-b, right);
        }

        public static IFluentPathValue Mul(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.math((a,b) => a*b, right);
        }

        public static IFluentPathValue Div(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.math((a,b) => a/b, right);
        }

        private static IFluentPathValue math(this IFluentPathValue left, Func<dynamic,dynamic,object> f, IFluentPathValue right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("Operands must both be values");
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands must be of the same type");

            return new ConstantValue(f(left.Value, right.Value));
        }

        public static IFluentPathValue IsEqualTo(this IFluentPathValue left, IFluentPathValue right)
        {
            if (!Object.Equals(left.Value, right.Value)) return new ConstantValue(false);

            return left.Children().IsEqualTo(right.Children());
        }

        public static bool IsEquivalentTo(this IFluentPathValue left, IFluentPathValue right)
        {
            // Exception: In equality comparisons, the "id" elements do not need to be equal
            throw new NotImplementedException();
        }

        public static IFluentPathValue GreaterOrEqual(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.IsEqualTo(right).Or(left.compare(Operator.GreaterThan, right));
        }

        public static IFluentPathValue LessOrEqual(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.IsEqualTo(right).Or(left.compare(Operator.LessThan, right));
        }

        public static IFluentPathValue LessThan(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.compare(Operator.LessThan, right);
        }

        public static IFluentPathValue GreaterThan(this IFluentPathValue left, IFluentPathValue right)
        {
            return left.compare(Operator.GreaterThan, right);
        }

        private static IFluentPathValue compare(this IFluentPathValue left, Operator comp, IFluentPathValue right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("'{0)' requires both operands to be values".FormatWith(comp));
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands to '{0}' must be of the same type".FormatWith(comp));

            if (left.Value is string)
            {
                var result = String.Compare((string)left.Value, (string)right.Value);
                if (comp == Operator.LessThan) return new ConstantValue(result == -1);
                if (comp == Operator.GreaterThan) return new ConstantValue(result == 1);
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
