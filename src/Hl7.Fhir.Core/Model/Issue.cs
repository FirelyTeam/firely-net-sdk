namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Version-independent operation outcome issue 
    /// </summary>
    public class OperationOutcomeIssue
    {
        public IssueType? Code { get; set; }
        public IssueSeverity? Severity { get; set; }
        public CodeableConcept Details { get; set; }
        public string Diagnostics { get; set; }
    }
}
