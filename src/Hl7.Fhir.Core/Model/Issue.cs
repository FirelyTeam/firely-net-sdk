using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Version-independent operation outcome issue 
    /// </summary>
    public class Issue
    {
        public IssueType? Code { get; set; }
        public IssueSeverity? Severity { get; set; }
        public CodeableConcept Details { get; set; }
        public string Diagnostics { get; set; }
    }
}
