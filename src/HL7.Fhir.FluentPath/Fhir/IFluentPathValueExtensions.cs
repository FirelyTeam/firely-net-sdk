using Hl7.Fhir.Serialization;
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
        public static Int64 AsInteger(this IFluentPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (Int64)me.Value;
        }

        public static decimal AsDecimal(this IFluentPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (decimal)me.Value;
        }

        public static bool AsBoolean(this IFluentPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (bool)me.Value;
        }

        /// <summary>
        /// Cast this value to a string (not ToString, consider AsStringRepresentation if you want that)
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string AsString(this IFluentPathValue me)
        {
            return (string)me.Value;
        }
        public static PartialDateTime AsDateTime(this IFluentPathValue me)
        {
            return (PartialDateTime)me.Value;
        }

        /// <summary>
        /// A String representation of the entity that will convert whatever type it is into a string
        /// (unlike the AsString, which just cases to a string)
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string AsStringRepresentation(this IFluentPathValue me)
        {
            if (me.Value == null) return null;

            if (me.Value is PartialDateTime)
                return me.Value.ToString();
            else
                return PrimitiveTypeConverter.ConvertTo<string>(me.Value);
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

            return new TypedValue(f(left.Value, right.Value));
        }

        public static bool IsEqualTo(this IFluentPathValue left, IFluentPathValue right)
        {
            if (!Object.Equals(left.Value, right.Value)) return false;

            return left.Children().IsEqualTo(right.Children()).AsBoolean();
        }

        public static bool IsEquivalentTo(this IFluentPathValue left, IFluentPathValue right)
        {
            // Exception: In equality comparisons, the "id" elements do not need to be equal
            if (left is IFluentPathElement && right is IFluentPathElement)
            {
                var lElem = (IFluentPathElement)left;
                var rElem = (IFluentPathElement)right;

              //  if (lElem.Name == "id" && rElem.Name == "id")
                    return true;
            }

            throw new NotImplementedException();
        }

        public static IFluentPathValue GreaterOrEqual(this IFluentPathValue left, IFluentPathValue right)
        {
            return new TypedValue(left.IsEqualTo(right) || left.compare(InfixOperator.GreaterThan, right));
        }

        public static IFluentPathValue LessOrEqual(this IFluentPathValue left, IFluentPathValue right)
        {
            return new TypedValue(left.IsEqualTo(right) || left.compare(InfixOperator.LessThan, right));
        }

        public static IFluentPathValue LessThan(this IFluentPathValue left, IFluentPathValue right)
        {
            return new TypedValue(left.compare(InfixOperator.LessThan, right));
        }

        public static IFluentPathValue GreaterThan(this IFluentPathValue left, IFluentPathValue right)
        {
            return new TypedValue(left.compare(InfixOperator.GreaterThan, right));
        }

        private static bool compare(this IFluentPathValue left, InfixOperator comp, IFluentPathValue right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("'{0)' requires both operands to be values".FormatWith(comp));
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands to '{0}' must be of the same type".FormatWith(comp));

            if (left.Value is string)
            {
                var result = String.Compare(left.AsString(), right.AsString());
                if (comp == InfixOperator.LessThan) return result == -1;
                if (comp == InfixOperator.GreaterThan) return result == 1;
            }
            else
            {
                if (comp == InfixOperator.LessThan) return (dynamic)left.Value < (dynamic)right.Value;
                if (comp == InfixOperator.GreaterThan) return (dynamic)left.Value > (dynamic)right.Value;
            }

            throw Error.InvalidOperation("Comparison failed on operator '{0}'".FormatWith(comp));
        }
    }
}
