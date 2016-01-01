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

namespace Hl7.Fhir.FhirPath
{
    internal class ConstantFhirPathValue : IFhirPathValue
    {
        public ConstantFhirPathValue(object value)
        {
            Value = value;
        }

        public ValueType Type
        {
            get
            {
                if (Value is bool)
                    return ValueType.Boolean;
                else if (Value is string)
                    return ValueType.String;
                else if (Value is int)
                    return ValueType.Integer;
                else if (Value is decimal)
                    return ValueType.Decimal;
                else if (Value is PartialDateTime)
                    return ValueType.DateTime;
                else
                    return ValueType.Unknown;
            }
        }

        public object Value
        {
            get; private set;
        }
    }
}
