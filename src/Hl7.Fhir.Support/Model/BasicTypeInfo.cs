using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Model
{
    public class BasicTypeInfo
    {
        public string Type;
        public bool? IsResource;
        public bool? MayRepeat;
        public bool? IsChoice;
    }
}
