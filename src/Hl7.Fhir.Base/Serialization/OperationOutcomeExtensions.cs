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
            string shortDisplay = null;

            // Set the Display values based on the code
            switch (me.ErrorCode)
            {
                case CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE: shortDisplay = "Invalid datatype used"; break;
                case CodedValidationException.INCORRECT_CARDINALITY_MIN_CODE: shortDisplay = "Missing mandatory field"; break;
                case CodedValidationException.INCORRECT_CARDINALITY_MAX_CODE: shortDisplay = "Exceeded max values"; break;
                case CodedValidationException.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE: shortDisplay = "Cannot be null"; break;
                case CodedValidationException.MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE: shortDisplay = "Mandatory field cannot be null"; break;
                case CodedValidationException.LITERAL_INVALID_CODE: shortDisplay = "Invalid literal"; break;
                case CodedValidationException.NARRATIVE_XML_IS_MALFORMED_CODE: shortDisplay = "Malformed narrative"; break;
                case CodedValidationException.NARRATIVE_XML_IS_INVALID_CODE: shortDisplay = "Invalid narrative"; break;
                case CodedValidationException.INVALID_CODED_VALUE_CODE: shortDisplay = "Invalid code"; break;
                case CodedValidationException.CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE: shortDisplay = "Contained resources cannot be nested"; break;
                case CodedValidationException.INVALID_BASE64_VALUE_CODE: shortDisplay = "Invalid Base64 data"; break;

                case FhirJsonException.EXPECTED_START_OF_OBJECT_CODE: shortDisplay = "Expected Object"; break;
                case FhirJsonException.NO_RESOURCETYPE_PROPERTY_CODE: shortDisplay = "Missing ResourceType"; break;
                case FhirJsonException.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE: shortDisplay = "Expected Object"; break;
                case FhirJsonException.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE: shortDisplay = "Expected Primitive"; break;
                case FhirJsonException.UNKNOWN_RESOURCE_TYPE_CODE: shortDisplay = "Invalid ResourceType"; break;
                case FhirJsonException.UNKNOWN_PROPERTY_FOUND_CODE: shortDisplay = "Unknown element"; break;
                case FhirXmlException.EMPTY_ELEMENT_NAMESPACE_CODE: shortDisplay = "Empty Element Namespace"; break;
                case FhirXmlException.UNKNOWN_RESOURCE_TYPE_CODE: shortDisplay = "Invalid ResourceType"; break;
                case FhirXmlException.UNKNOWN_ELEMENT_CODE: shortDisplay = "Unknown element"; break;
                case FhirXmlException.ELEMENT_OUT_OF_ORDER_CODE: shortDisplay = "XML element out of order"; break;
                case FhirXmlException.INCORRECT_ELEMENT_NAMESPACE_CODE: shortDisplay = "Invalid element namespace"; break;
                case FhirXmlException.INCORRECT_ATTRIBUTE_NAMESPACE_CODE: shortDisplay = "Invalid attribute namespace"; break;
            }

            var result = new OperationOutcome.IssueComponent()
            {
                Severity = me.IssueSeverity,
                Code = me.IssueType,
                Details = new CodeableConcept(ValidationErrorMessageCodeSystem, me.ErrorCode, shortDisplay, me.BaseErrorMessage)
            };

            if (me.LineNumber.HasValue && me.Position.HasValue)
                result.Location = [$"line {me.LineNumber}, position {me.Position}"];
            if (!string.IsNullOrEmpty(me.InstancePath))
                result.Expression = [me.InstancePath];

            return result;
        }
    }
}