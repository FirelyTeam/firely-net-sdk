
/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
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
