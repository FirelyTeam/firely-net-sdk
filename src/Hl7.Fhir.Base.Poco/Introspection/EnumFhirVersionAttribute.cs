using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Introspection
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class EnumFhirVersionAttribute : Attribute
    {
        public EnumFhirVersion FhirVersion { get; set; }
    }
}
