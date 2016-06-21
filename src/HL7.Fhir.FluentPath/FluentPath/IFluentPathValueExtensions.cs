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
        public static Int64 AsInteger(this IValueProvider me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (Int64)me.Value;
        }

        public static decimal AsDecimal(this IValueProvider me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (decimal)me.Value;
        }

        public static bool AsBoolean(this IValueProvider me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (bool)me.Value;
        }

        /// <summary>
        /// Cast this value to a string (not ToString, consider AsStringRepresentation if you want that)
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string AsString(this IValueProvider me)
        {
            return (string)me.Value;
        }

        public static PartialDateTime AsDateTime(this IValueProvider me)
        {
            return (PartialDateTime)me.Value;
        }


        public static Time AsTime(this IValueProvider me)
        {
            return (Time)me.Value;
        }

        /// <summary>
        /// A String representation of the entity that will convert whatever type it is into a string
        /// (unlike the AsString, which just cases to a string)
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string AsStringRepresentation(this IValueProvider me)
        {
            if (me.Value == null) return null;

            if (me.Value is PartialDateTime || me.Value is Time)
                return me.Value.ToString();
            else
                return XmlConvert.ToString((dynamic)me.Value);
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
            if (!Object.Equals(left.Value, right.Value)) return false;

            return left.Children().IsEqualTo(right.Children()).AsBoolean();
        }

        public static bool IsEquivalentTo(this IValueProvider left, IValueProvider right)
        {
            // Exception: In equality comparisons, the "id" elements do not need to be equal
            if (left is IElementNavigator && right is IElementNavigator)
            {
                var lElem = (IElementNavigator)left;
                var rElem = (IElementNavigator)right;

              //  if (lElem.Name == "id" && rElem.Name == "id")
                    return true;
            }

            throw new NotImplementedException();
        }

        public static IValueProvider GreaterOrEqual(this IValueProvider left, IValueProvider right)
        {
            return new ConstantValue(left.IsEqualTo(right) || left.compare(Operator.GreaterThan, right));
        }

        public static IValueProvider LessOrEqual(this IValueProvider left, IValueProvider right)
        {
            return new ConstantValue(left.IsEqualTo(right) || left.compare(Operator.LessThan, right));
        }

        public static IValueProvider LessThan(this IValueProvider left, IValueProvider right)
        {
            return new ConstantValue(left.compare(Operator.LessThan, right));
        }

        public static IValueProvider GreaterThan(this IValueProvider left, IValueProvider right)
        {
            return new ConstantValue(left.compare(Operator.GreaterThan, right));
        }

        private static bool compare(this IValueProvider left, Operator comp, IValueProvider right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("'{0)' requires both operands to be values".FormatWith(comp));
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands to '{0}' must be of the same type".FormatWith(comp));

            if (left.Value is string)
            {
                var result = String.Compare(left.AsString(), right.AsString());
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
