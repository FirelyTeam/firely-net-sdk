using ConsoleAppTestAnalyzer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAnalyzer1
{
    public class TestClass
    {
        [FhirVersion(StartingVersion = "Hl7.Fhir.DSTU2.Core", EndingVersion = "Hl7.Fhir.R4.Core", ExcludedVersions = "Hl7.Fhir.STU3.Core")]
        public string TestMember { get; set; }
    }
}
