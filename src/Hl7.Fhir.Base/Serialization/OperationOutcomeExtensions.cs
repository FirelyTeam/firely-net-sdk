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
                case CodedValidationException.CODE_LITERAL_INVALID_CODE: shortDisplay = "Invalid code"; break;
                case CodedValidationException.DATE_LITERAL_INVALID_CODE: shortDisplay = "Invalid date"; break;
                case CodedValidationException.DATETIME_LITERAL_INVALID_CODE: shortDisplay = "Invalid datetime"; break;
                case CodedValidationException.ID_LITERAL_INVALID_CODE: shortDisplay = "Invalid id"; break;
                case CodedValidationException.OID_LITERAL_INVALID_CODE: shortDisplay = "Invalid oid"; break;
                case CodedValidationException.TIME_LITERAL_INVALID_CODE: shortDisplay = "Invalid time"; break;
                case CodedValidationException.URI_LITERAL_INVALID_CODE: shortDisplay = "Invalid uri"; break;
                case CodedValidationException.UUID_LITERAL_INVALID_CODE: shortDisplay = "Invalid uuid"; break;
                case CodedValidationException.NARRATIVE_XML_IS_MALFORMED_CODE: shortDisplay = "Malformed narrative"; break;
                case CodedValidationException.NARRATIVE_XML_IS_INVALID_CODE: shortDisplay = "Invalid narrative"; break;
                case CodedValidationException.INVALID_CODED_VALUE_CODE: shortDisplay = "Invalid code"; break;
                case CodedValidationException.CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE: shortDisplay = "Contained resources cannot be nested"; break;

                case FhirJsonException.EXPECTED_START_OF_OBJECT_CODE: shortDisplay = "Expected Object"; break;
                // case FhirJsonException.RESOURCETYPE_SHOULD_BE_STRING_CODE: shortDisplay = "JSON102"; break;
                case FhirJsonException.NO_RESOURCETYPE_PROPERTY_CODE: shortDisplay = "Missing ResourceType"; break;
                case FhirJsonException.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE: shortDisplay = "Expected Object"; break;
                case FhirJsonException.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE: shortDisplay = "Expected Primitive"; break;
                case FhirJsonException.INCORRECT_BASE64_DATA_CODE: shortDisplay = "Invalid Base64 data"; break;
                // case FhirJsonException.STRING_ISNOTAN_INSTANT_CODE: shortDisplay = "JSON107"; break;
                case FhirJsonException.NUMBER_CANNOT_BE_PARSED_CODE: shortDisplay = "Invalid number"; break;
                // case FhirJsonException.EXPECTED_PRIMITIVE_NOT_NULL_CODE: shortDisplay = "JSON109"; break;
                case FhirJsonException.UNEXPECTED_JSON_TOKEN_CODE: shortDisplay = "Invalid datatype"; break;
                // case FhirJsonException.EXPECTED_START_OF_ARRAY_CODE: shortDisplay = "JSON111"; break;
                // case FhirJsonException.USE_OF_UNDERSCORE_ILLEGAL_CODE: shortDisplay = "JSON113"; break;
                // case FhirJsonException.CHOICE_ELEMENT_HAS_NO_TYPE_CODE: shortDisplay = "JSON114"; break;
                // case FhirJsonException.CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE: shortDisplay = "JSON115"; break;
                case FhirJsonException.UNKNOWN_RESOURCE_TYPE_CODE: shortDisplay = "Invalid ResourceType"; break;
                // case FhirJsonException.RESOURCE_TYPE_NOT_A_RESOURCE_CODE: shortDisplay = "JSON117"; break;
                case FhirJsonException.UNKNOWN_PROPERTY_FOUND_CODE: shortDisplay = "Unknown element"; break;
                // case FhirJsonException.RESOURCETYPE_UNEXPECTED_CODE: shortDisplay = "JSON119"; break;
                // case FhirJsonException.OBJECTS_CANNOT_BE_EMPTY_CODE: shortDisplay = "JSON120"; break;
                // case FhirJsonException.ARRAYS_CANNOT_BE_EMPTY_CODE: shortDisplay = "JSON121"; break;
                // case FhirJsonException.LONG_CANNOT_BE_PARSED_CODE: shortDisplay = "JSON122"; break;
                // case FhirJsonException.LONG_INCORRECT_FORMAT_CODE: shortDisplay = "JSON123"; break;

                // case FhirJsonException.PRIMITIVE_ARRAYS_ONLY_NULL_CODE: shortDisplay = "JSON125"; break;
                // case FhirJsonException.INCOMPATIBLE_SIMPLE_VALUE_CODE: shortDisplay = "JSON126"; break;


                case FhirXmlException.EMPTY_ELEMENT_NAMESPACE_CODE: shortDisplay = "Empty Element Namespace"; break;
                case FhirXmlException.UNKNOWN_RESOURCE_TYPE_CODE: shortDisplay = "Invalid ResourceType"; break;
                // case FhirXmlException.RESOURCE_TYPE_NOT_A_RESOURCE_CODE: shortDisplay = "XML103"; break;
                case FhirXmlException.UNKNOWN_ELEMENT_CODE: shortDisplay = "Unknown element"; break;
                // case FhirXmlException.CHOICE_ELEMENT_HAS_NO_TYPE_CODE: shortDisplay = "XML105"; break;
                // case FhirXmlException.CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE: shortDisplay = "XML106"; break;
                // case FhirXmlException.INCORRECT_XHTML_NAMESPACE_CODE: shortDisplay = "XML107"; break;
                // case FhirXmlException.UNKNOWN_ATTRIBUTE_CODE: shortDisplay = "XML108"; break;
                case FhirXmlException.ELEMENT_OUT_OF_ORDER_CODE: shortDisplay = "XML element out of order"; break;
                // case FhirXmlException.UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE: shortDisplay = "XML110"; break;
                // case FhirXmlException.NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE: shortDisplay = "XML111"; break;
                case FhirXmlException.INCORRECT_ELEMENT_NAMESPACE_CODE: shortDisplay = "Invalid element namespace"; break;
                // case FhirXmlException.UNALLOWED_NODE_TYPE_CODE: shortDisplay = "XML113"; break;
                case FhirXmlException.INCORRECT_ATTRIBUTE_NAMESPACE_CODE: shortDisplay = "Invalid attribute namespace"; break;
                // case FhirXmlException.ATTRIBUTE_HAS_EMPTY_VALUE_CODE: shortDisplay = "XML115"; break;
                // case FhirXmlException.ELEMENT_NOT_IN_SEQUENCE_CODE: shortDisplay = "XML116"; break;
                // case FhirXmlException.SCHEMALOCATION_DISALLOWED_CODE: shortDisplay = "XML117"; break;
                // case FhirXmlException.EXPECTED_OPENING_ELEMENT_CODE: shortDisplay = "XML118"; break;
                // case FhirXmlException.ENCOUNTERED_DTP_REFERENCES_CODE: shortDisplay = "XML119"; break;
                // case FhirXmlException.ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE: shortDisplay = "XML120"; break;

                case FhirXmlException.INCORRECT_BASE64_DATA_CODE: shortDisplay = "Invalid Base64 data"; break;
                case FhirXmlException.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE: shortDisplay = "Invalid datatype"; break;
            }

            var result = new Model.OperationOutcome.IssueComponent()
            {
                Severity = me.IssueSeverity,
                Code = me.IssueType,
                Details = new Model.CodeableConcept(ValidationErrorMessageCodeSystem, me.ErrorCode, shortDisplay, me.BaseErrorMessage)
            };

            if (me.LineNumber.HasValue && me.Position.HasValue)
                result.Location = new[] { $"line {me.LineNumber}, position {me.Position}" };
            if (!string.IsNullOrEmpty(me.InstancePath))
                result.Expression = new[] { me.InstancePath };

            return result;
        }
    }
}
