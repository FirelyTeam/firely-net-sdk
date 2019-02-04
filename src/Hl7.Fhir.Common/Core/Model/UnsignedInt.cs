using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.Model
{
    public partial class UnsignedInt : INullableIntegerValue
    {
        public static bool IsValidValue(string value)
        {
            try
            {
                var dummy = XmlConvert.ToInt32(value);
                if (dummy < 0) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
