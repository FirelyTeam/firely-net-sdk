/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Support;
using System;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.FhirPath
{

    //TODO: Merge with FhirDateTime from Model namespace?s
    public class PartialDateTime
    {
        private PartialDateTime()
        {

        }
        public enum Precision
        {
            Year = 0,
            Month = 1,
            Day = 2,
            Time = 3,
        }

        public DateTimeOffset Value;
        public Precision Prec;

        public static PartialDateTime Parse(string value)
        {
            var dtValue = XmlConvert.ToDateTimeOffset(value);

            if (value.Contains(":") && !value.Contains("T"))
                throw Error.NotSupported("Partial date times cannot contain just a time");

            var precCount = value.Count(c => c == '-') + (value.Contains("T") ? 1 : 0);
            var prec = (PartialDateTime.Precision)precCount;

            return new PartialDateTime { Value = dtValue, Prec = prec };
        }

        public static bool TryParse(string representation, out PartialDateTime value)
        {
            try
            {
                value = PartialDateTime.Parse(representation);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public override string ToString()
        {
            return XmlConvert.ToString(Value);
        }

        public static PartialDateTime Now()
        {
            return new PartialDateTime { Value = DateTimeOffset.Now, Prec = Precision.Time };
        }
    }
}
