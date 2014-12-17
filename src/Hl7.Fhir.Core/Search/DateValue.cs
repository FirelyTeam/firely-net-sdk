/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class DateValue : ValueExpression
    {
        public string Value { get; private set; }

        public DateValue(DateTimeOffset value)
        {
            // The DateValue datatype is not interested in any time related
            // components, so we must strip those off before converting to the string
            // value
            Value = value.Date.ToString("yyyy-MM-dd");
        }

        public DateValue(string date)
        {
            if (!Date.IsValidValue(date))
            {
                if (!FhirDateTime.IsValidValue(date))
                    throw Error.Argument("date", "The string [" + date + "] is not a valid FHIR date string and isn't a FHIR datetime either");
                
                // This was a time, so we can just use the date portion of this
                date = (new FhirDateTime(date)).ToDateTimeOffset().Date.ToString("yyyy-MM-dd");
            }
            Value = date;
        }

        public override string ToString()
        {
            return Value;
        }

        public static DateValue Parse(string text)
        {
            return new DateValue(text);
        }
    }
}