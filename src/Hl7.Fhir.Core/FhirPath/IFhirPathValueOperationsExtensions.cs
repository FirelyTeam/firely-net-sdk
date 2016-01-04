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
            if (me.Value == null && value.Value == null)
                return me.Children().IsEqualTo(value.Children());

            else if (me.Value != null && value.Value != null)
            {
                // GetInternalValue will normalize the IFhirPath's value to a standard type so we can accurately compare them 
                if (me.AsObject() == value.AsObject())
                {
                    return me.Children().IsEqualTo(value.Children());
                }
                else
                    return false;
            }

            else 
                return false;
        }

        public static IFhirPathValue Add(this IFhirPathValue me, IFhirPathValue value)
        {
            if (me.Value == null || value.Value == null)
                throw Error.InvalidOperation("Add requires both operands to be values");

            if(me.GetFhirType() == ValueType.Integer && value.GetFhirType() == ValueType.Integer)
            {
                return new ConstantFhirPathValue((Int64)me.AsObject() + (Int64)value.AsObject());
            }
            else if (me.GetFhirType() == ValueType.Decimal && value.GetFhirType() == ValueType.Decimal)
            {
                return new ConstantFhirPathValue((Decimal)me.AsObject() + (Decimal)value.AsObject());
            }
            else if (me.GetFhirType() == ValueType.String && value.GetFhirType() == ValueType.String)
            {
                return new ConstantFhirPathValue((string)me.AsObject() + (string)value.AsObject());
            }
            else
            {
                throw Error.InvalidOperation("Add cannot add a value of type {0} to a value of type {1}".FormatWith(me.GetFhirType(), value.GetFhirType()));
            }
        }
    }
}
