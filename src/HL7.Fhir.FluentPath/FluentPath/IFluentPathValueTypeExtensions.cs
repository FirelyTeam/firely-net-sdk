using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.FluentPath
{
    public enum ValueType
    {
        Boolean,
        String,
        Integer,
        Decimal,
        DateTime
    }


    public static class IFluentPathValueTypeExtensions
    {    
        public static ValueType GetFhirType(this IFluentPathValue value)
        {
            if (value == null || value.Value == null) throw Error.ArgumentNull("value");

            var val = value.Value;

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

            throw new InvalidOperationException("IFhirPathValue.Value returned an unsupported type {0}".FormatWith(val.GetType().Name));
        }
    }
}
