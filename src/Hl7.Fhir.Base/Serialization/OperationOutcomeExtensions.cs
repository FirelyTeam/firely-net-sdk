using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Serialization
{
    public static class OperationOutcomeExtensions
    {
        /// <summary>
        /// Convert to an OperationOutcome
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static OperationOutcome ToOperationOutcome(this DeserializationFailedException ex)
        {
            // Need to convert the list of general exceptions into an OperationOutcome.
            OperationOutcome oc = new OperationOutcome();
            foreach (var e in ex.Exceptions)
            {
                var issue =
                    new OperationOutcome.IssueComponent()
                    {
                        Severity = OperationOutcome.IssueSeverity.Error,
                        Code = OperationOutcome.IssueType.Invalid
                    };

                if (e is ExtendedCodedException ecl)
                {
                    issue = ecl.ToIssue();
                }
                issue.Diagnostics = e.Message;
                oc.Issue.Add(issue);
            }

            return oc;
        }

        /// <summary>
        /// CodeSystem to be used in generating error messages in the OperationOutcome
        /// </summary>
        public static string ValidationErrorMessageCodeSystem = "http://firely.com/CodeSystem/ErrorMessages";

        /// <summary>
        /// Convert to an OperationOutcome.Issue
        /// </summary>
        /// <returns></returns>
        public static Model.OperationOutcome.IssueComponent ToIssue(this ExtendedCodedException me)
        {
            var result = new OperationOutcome.IssueComponent()
            {
                Severity = me.IssueSeverity,
                Code = me.IssueType,
                Details = new CodeableConcept(ValidationErrorMessageCodeSystem, me.ErrorCode, me.Display, me.BaseErrorMessage)
            };

            if (me.LineNumber.HasValue && me.Position.HasValue)
                result.Location = [$"line {me.LineNumber}, position {me.Position}"];
            if (!string.IsNullOrEmpty(me.InstancePath))
                result.Expression = [me.InstancePath];

            return result;
        }
    }
}