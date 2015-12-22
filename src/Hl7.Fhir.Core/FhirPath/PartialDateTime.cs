using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FhirPath
{
    public struct PartialDateTime
    {
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

            var precCount = value.Count(c => c == '-') + (value.Contains("T") ? 1 : 0);
            var prec = (PartialDateTime.Precision)precCount;

            return new PartialDateTime { Value = dtValue, Prec = prec };
        }
    }
}
