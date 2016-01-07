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
    public static class IFhirPathValueOperationsExtensions
    {
        public static bool IsEqualTo(this IFhirPathValue me, IFhirPathValue value)
        {
            if (!Object.Equals(me.Value, value.Value)) return false;

            return me.Children().IsEqualTo(value.Children());
        }

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

        public static bool AsBool(this IFhirPathValue me)
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

            return PrimitiveTypeConverter.ConvertTo<string>(me.Value);
        }

        internal static IFhirPathValue Operator(this IFhirPathValue me, InfixOperator op, IFhirPathValue value)
        {
            if (me.Value == null || value.Value == null)
                throw Error.InvalidOperation("'{0)' requires both operands to be values".FormatWith(op));
            if (me.Value.GetType() != value.Value.GetType())
                throw Error.InvalidOperation("Operands to '{0}' must be of the same type".FormatWith(op));

            switch(op)
            {
                case InfixOperator.Add: return new TypedValue(((dynamic)me.Value) + ((dynamic)value.Value));
                case InfixOperator.Sub: return new TypedValue(((dynamic)me.Value) - ((dynamic)value.Value));
                case InfixOperator.Mul: return new TypedValue(((dynamic)me.Value) * ((dynamic)value.Value));
                case InfixOperator.Div: return new TypedValue(((dynamic)me.Value) / ((dynamic)value.Value));
                default:
                    throw Error.InvalidOperation("Unsupported operator '{0}'".FormatWith(op));
            }
        }
    }
}
