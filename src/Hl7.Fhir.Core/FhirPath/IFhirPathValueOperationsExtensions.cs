using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    public static class IFhirPathValueOperationsExtensions
    {
        public static bool IsEqualTo(this IFhirPathValue me, IFhirPathValue value)
        {
            if (me.Value != value.Value) return false;

            return me.Children().IsEqualTo(value.Children());
        }

        public static IFhirPathValue Add(this IFhirPathValue me, IFhirPathValue value)
        {
            if (me.Value == null || value.Value == null)
                throw Error.InvalidOperation("Add requires both operands to be values");

            if(me.Value is Int64 && value.Value is Int64)
            {
                return new TypedValue((Int64)me.Value + (Int64)value.Value);
            }
            else if (me.Value is Decimal && value.Value is Decimal)
            {
                return new TypedValue((Decimal)me.Value + (Decimal)value.Value);
            }
            else if (me.Value is String && value.Value is String)
            {
                return new TypedValue((string)me.Value + (string)value.Value);
            }
            else
            {
                throw Error.InvalidOperation("Add cannot add a value of type {0} to a value of type {1}".FormatWith(me.Value.GetType(), value.Value.GetType()));
            }
        }
    }
}
