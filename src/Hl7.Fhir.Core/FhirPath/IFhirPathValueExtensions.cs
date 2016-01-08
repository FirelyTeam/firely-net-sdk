using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FhirPath
{
    public static class IFhirPathValueExtensions
    {
        public static Int64 AsInteger(this IFhirPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (Int64)me.Value;
        }

        public static decimal AsDecimal(this IFhirPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (decimal)me.Value;
        }

        public static bool AsBoolean(this IFhirPathValue me)
        {
            if (me.Value == null) throw Error.ArgumentNull("me");
            return (bool)me.Value;
        }

        public static string AsString(this IFhirPathValue me)
        {
            return (string)me.Value;
        }
        public static PartialDateTime AsDateTime(this IFhirPathValue me)
        {
            return (PartialDateTime)me.Value;
        }

        public static string AsStringRepresentation(this IFhirPathValue me)
        {
            if (me.Value == null) return null;

            if (me.Value is PartialDateTime)
                return me.Value.ToString();
            else
                return PrimitiveTypeConverter.ConvertTo<string>(me.Value);
        }

        public static IFhirPathValue Add(this IFhirPathValue left, IFhirPathValue right)
        {
            return left.math((a,b) => a+b, right);
        }

        public static IFhirPathValue Sub(this IFhirPathValue left, IFhirPathValue right)
        {
            return left.math((a,b) => a-b, right);
        }

        public static IFhirPathValue Mul(this IFhirPathValue left, IFhirPathValue right)
        {
            return left.math((a,b) => a*b, right);
        }

        public static IFhirPathValue Div(this IFhirPathValue left, IFhirPathValue right)
        {
            return left.math((a,b) => a/b, right);
        }

        private static IFhirPathValue math(this IFhirPathValue left, Func<dynamic,dynamic,object> f, IFhirPathValue right)
        {
            if (left.Value == null || right.Value == null)
                throw Error.InvalidOperation("Operands must both be values");
            if (left.Value.GetType() != right.Value.GetType())
                throw Error.InvalidOperation("Operands must be of the same type");

            return new TypedValue(f(left.Value, right.Value));
        }

        public static bool IsEqualTo(this IFhirPathValue left, IFhirPathValue right)
        {
            if (!Object.Equals(left.Value, right.Value)) return false;

            return left.Children().IsEqualTo(right.Children());
        }

        public static bool IsEquivalentTo(this IFhirPathValue left, IFhirPathValue right)
        {
            // Exception: In equality comparisons, the "id" elements do not need to be equal
            if (left is IFhirPathElement && right is IFhirPathElement)
            {
                var lElem = (IFhirPathElement)left;
                var rElem = (IFhirPathElement)right;

                if (lElem.Name == "id" && rElem.Name == "id")
                    return true;
            }

            throw new NotImplementedException();
        }

        public static IFhirPathValue GreaterOrEqual(this IFhirPathValue left, IFhirPathValue right)
        {
            return new TypedValue(left.IsEqualTo(right) || left.compare(InfixOperator.GreaterThan, right));
        }

        public static IFhirPathValue LessOrEqual(this IFhirPathValue left, IFhirPathValue right)
        {
            return new TypedValue(left.IsEqualTo(right) || left.compare(InfixOperator.LessThan, right));
        }

        public static IFhirPathValue LessThan(this IFhirPathValue left, IFhirPathValue right)
        {
            return new TypedValue(left.compare(InfixOperator.LessThan, right));
        }

        public static IFhirPathValue GreaterThan(this IFhirPathValue left, IFhirPathValue right)
        {
            return new TypedValue(left.compare(InfixOperator.GreaterThan, right));
        }

        private static bool compare(this IFhirPathValue left, InfixOperator comp, IFhirPathValue right)
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
