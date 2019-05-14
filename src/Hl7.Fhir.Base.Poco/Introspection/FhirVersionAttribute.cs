using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class FhirVersionAttribute : Attribute
    {
        public string StartingVersion { get; set; }
        public string FinalVersion { get; set; }
        public string[] ExcludedVersions { get; set; }
    }
}
