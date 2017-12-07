namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Version-independent operation outcome issue 
    /// </summary>
    public class OperationOutcomeIssue
    {
        public OperationOutcomeIssue()
        { }

        public OperationOutcomeIssue( DSTU2.OperationOutcome.IssueComponent issue )
        {
            Code = issue?.Code;
            Severity = issue?.Severity;
            Details = (CodeableConcept)issue?.Details.DeepCopy();
            Diagnostics = issue.Diagnostics;
        }

        public OperationOutcomeIssue(STU3.OperationOutcome.IssueComponent issue)
        {
            Code = issue?.Code;
            Severity = issue?.Severity;
            Details = (CodeableConcept)issue?.Details.DeepCopy();
            Diagnostics = issue.Diagnostics;
        }

        public IssueType? Code { get; set; }
        public IssueSeverity? Severity { get; set; }
        public CodeableConcept Details { get; set; }
        public string Diagnostics { get; set; }

        public DSTU2.OperationOutcome.IssueComponent ToDstu2()
        {
            return new DSTU2.OperationOutcome.IssueComponent
            {
                Code = Code,
                Severity = Severity,
                Details = (CodeableConcept)Details.DeepCopy(),
                Diagnostics = Diagnostics
            };
        }

        public STU3.OperationOutcome.IssueComponent ToStu3()
        {
            return new STU3.OperationOutcome.IssueComponent
            {
                Code = Code,
                Severity = Severity,
                Details = (CodeableConcept)Details.DeepCopy(),
                Diagnostics = Diagnostics
            };
        }
    }
}
