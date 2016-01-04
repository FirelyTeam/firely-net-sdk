using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FhirPath
{
    public class UntypedValue
    {
        public string Representation { get; private set; }
        public UntypedValue(string representation)
        {
            Representation = representation;
        }

        public object ToTypedValue()
        {
            var rep = Representation;

            if (rep.ToLower() == "true") return true;

            if (rep.ToLower() == "false") return false;

            if (rep.Contains("-") || rep.Contains(":"))
            {
                PartialDateTime dt;
                if (PartialDateTime.TryParse(rep, out dt)) return dt;
            }

            if (rep.Contains("."))
            {
                try
                {
                    return XmlConvert.ToDecimal(rep);
                }
                catch
                {
                    ;  // Fall through to next case
                }
            }

            try
            {
                return XmlConvert.ToInt32(rep);
            }
            catch
            {
                ; // Fall through to next case
            }

            return rep;     // If all else fails, it's probably just a string
        }
    }
}
