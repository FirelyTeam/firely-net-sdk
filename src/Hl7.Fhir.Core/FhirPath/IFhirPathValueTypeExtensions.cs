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

    //Unknown,
    //Boolean,
    //String, 
    //Integer,
    //Decimal,
    //DateTime


    public static class IFhirPathValueTypeExtensions
    {
        internal static object AsObject(this IFhirPathValue value)
        {
            if (value == null || value.Value == null) throw Error.ArgumentNull("value");

            object val = null;

            if (value.Value is UntypedValue)
                val = ((UntypedValue)value.Value).ToTypedValue();
            else
                val = value.Value;

            //TODO: Add support for FHIR types representing these values
            if (val is Boolean)
                return val;
            if (val is String)
                return val;
            if (val is Int32 || val is Int16 || val is UInt16 || val is UInt32)
                return (Int64)val;
            if (val is float || val is double || val is Decimal)
                return (Decimal)val;
            if (val is PartialDateTime)
                return val;

            throw Error.NotSupported("Cannot process values of type {0} (with value {1}) in the FhirPath engine".FormatWith(val.GetType(), val.ToString()));
        }

        public static ValueType GetFhirType(this IFhirPathValue value)
        {
            if (value == null || value.Value == null) throw Error.ArgumentNull("value");

            var val = value.AsObject();

            if (val is Boolean)
                return ValueType.Boolean;
            if (val is String)
                return ValueType.String;
            if (val is Int64)
                return ValueType.Integer;
            if (val is Decimal)
                return ValueType.Decimal;
            if (val is PartialDateTime)
                return ValueType.DateTime;

            throw new InvalidOperationException("Internal error: AsObject() casted a value to an unsupported type");
        }
    }
}
