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


    public static class IFhirPathValueExtensions
    {
        public static ValueType GetFhirType(this IFhirPathValue value)
        {
            //TODO: Add support for FHIR types representing these values
            if (value.Value is UntypedValue)
                return guessFhirTypeFromUntypedValue((UntypedValue)value.Value);
            if (value.Value is Boolean)
                return ValueType.Boolean;
            if (value.Value is String)
                return ValueType.String;
            if (value.Value is Int32 || value.Value is Int16 || value.Value is Int64 || value.Value is UInt16 || value.Value is UInt32 || value.Value is UInt64)
                return ValueType.Integer;
            if (value.Value is float || value.Value is double || value.Value is Decimal)
                return ValueType.Decimal;
            if (value.Value is PartialDateTime)
                return ValueType.DateTime;

            throw Error.NotSupported("Cannot process values of type {0} (with value {1}) in the FhirPath engine".FormatWith(value.Value.GetType(), value.Value.ToString()));
        }

        private static ValueType guessFhirTypeFromUntypedValue(UntypedValue value)
        {
            var rep = value.Representation;

            if (rep.ToLower() == "true" || rep.ToLower() == "false")
                return ValueType.Boolean;

            if(rep.Contains("-") || rep.Contains(":"))
            {
                if (PartialDateTime.CanParse(rep)) return ValueType.DateTime;
            }

            if(rep.Contains("."))
            {
                try
                {
                    XmlConvert.ToDecimal(rep);
                    return ValueType.Decimal;
                }
                catch
                {
                    ;  // Fall through to next case
                }
            }

            try
            {
                XmlConvert.ToInt64(rep);
                return ValueType.Integer;
            }
            catch
            {
                ; // Fall through to next case
            }

            return ValueType.String;
        }
    }
}
