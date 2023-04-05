#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlException : CodedWithLocationException
    {
        public const string EMPTY_ELEMENT_NAMESPACE_CODE = "XML101";
        public const string UNKNOWN_RESOURCE_TYPE_CODE = "XML102";
        public const string RESOURCE_TYPE_NOT_A_RESOURCE_CODE = "XML103";
        public const string UNKNOWN_ELEMENT_CODE = "XML104";
        public const string CHOICE_ELEMENT_HAS_NO_TYPE_CODE = "XML105";
        public const string CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE = "XML106";
        public const string INCORRECT_XHTML_NAMESPACE_CODE = "XML107";
        public const string UNKNOWN_ATTRIBUTE_CODE = "XML108";
        public const string ELEMENT_OUT_OF_ORDER_CODE = "XML109";
        public const string UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE = "XML110";
        public const string NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE = "XML111";
        public const string INCORRECT_ELEMENT_NAMESPACE_CODE = "XML112";
        public const string UNALLOWED_NODE_TYPE_CODE = "XML113";
        public const string INCORRECT_ATTRIBUTE_NAMESPACE_CODE = "XML114";
        public const string ATTRIBUTE_HAS_EMPTY_VALUE_CODE = "XML115";
        public const string ELEMENT_NOT_IN_SEQUENCE_CODE = "XML116";
        public const string SCHEMALOCATION_DISALLOWED_CODE = "XML117";
        public const string EXPECTED_OPENING_ELEMENT_CODE = "XML118";
        public const string ENCOUNTERED_DTP_REFERENCES_CODE = "XML119";
        public const string ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE = "XML120";

        public const string INCORRECT_BASE64_DATA_CODE = "XML202";
        public const string VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE = "XML203";

        internal static readonly FhirXmlException EMPTY_ELEMENT_NAMESPACE = new(EMPTY_ELEMENT_NAMESPACE_CODE, $"The element '{0}' has no namespace, expected the HL7 FHIR namespace ({XmlNs.FHIR})") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException INCORRECT_ELEMENT_NAMESPACE = new(INCORRECT_ELEMENT_NAMESPACE_CODE, "The element '{0}' uses the namespace '{1}', which is not allowed.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException UNKNOWN_RESOURCE_TYPE = new(UNKNOWN_RESOURCE_TYPE_CODE, "Unknown type '{0}' found in root property.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Value };
        internal static readonly FhirXmlException RESOURCE_TYPE_NOT_A_RESOURCE = new(RESOURCE_TYPE_NOT_A_RESOURCE_CODE, "Data type '{0}' in property 'resourceType' is not a type of resource.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException UNKNOWN_ELEMENT = new(UNKNOWN_ELEMENT_CODE, "Encountered unrecognized element '{0}'.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException CHOICE_ELEMENT_HAS_NO_TYPE = new(CHOICE_ELEMENT_HAS_NO_TYPE_CODE, "Choice element '{0}' is not suffixed with a type.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException CHOICE_ELEMENT_HAS_UNKOWN_TYPE = new(CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE, "Choice element '{0}' is suffixed with an unrecognized type '{1}'.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException INCORRECT_BASE64_DATA = new(INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException VALUE_IS_NOT_OF_EXPECTED_TYPE = new(VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, "Literal string '{0}' cannot be parsed as a '{1}'.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException INCORRECT_XHTML_NAMESPACE = new(INCORRECT_XHTML_NAMESPACE_CODE, $"Narrative has incorrect namespace. Namespace should be {XmlNs.XHTML}") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException UNKNOWN_ATTRIBUTE = new(UNKNOWN_ATTRIBUTE_CODE, "Encountered unrecognized attribute '{0}'." ) { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException ELEMENT_OUT_OF_ORDER = new(ELEMENT_OUT_OF_ORDER_CODE, "Element '{0}' is not in the correct order ") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException ELEMENT_NOT_IN_SEQUENCE = new(ELEMENT_NOT_IN_SEQUENCE_CODE, "Element with name '{0}' was found multiple times, but not in sequence.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER = new(UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE, "Encountered unallowed content '{0}' in the resource container. Only a single resource is allowed.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER = new(NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE, "The element '{0}' has a contained resource and therefore should not have attributes.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException UNALLOWED_NODE_TYPE = new(UNALLOWED_NODE_TYPE_CODE, "Xml node of type '{0}' is unexpected at this point") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException INCORRECT_ATTRIBUTE_NAMESPACE = new(INCORRECT_ATTRIBUTE_NAMESPACE_CODE, "The attribute '{0}' in element '{1}' uses the namespace '{2}', which is not allowed.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException ATTRIBUTE_HAS_EMPTY_VALUE = new(ATTRIBUTE_HAS_EMPTY_VALUE_CODE, "Attributes cannot be empty. Either they are absent, or they are present with at least one character of non - whitespace content") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException SCHEMALOCATION_DISALLOWED = new(SCHEMALOCATION_DISALLOWED_CODE, "The 'schemaLocation' attribute is disallowed.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException EXPECTED_OPENING_ELEMENT = new(EXPECTED_OPENING_ELEMENT_CODE, "Expected opening element, but found {0}.") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException ENCOUNTERED_DTD_REFERENCES = new(ENCOUNTERED_DTP_REFERENCES_CODE, "There SHALL be no DTD references in FHIR resources (because of the XXE security exploit)") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };
        internal static readonly FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN = new(ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, "Element '{0}' must have child elements and / or a value attribute") { IssueSeverity = Model.OperationOutcome.IssueSeverity.Error, IssueType = Model.OperationOutcome.IssueType.Structure };

        public override OperationOutcome.IssueComponent ToIssue()
        { 
            var result = base.ToIssue();
            // Set the Display values based on the code
            switch (ErrorCode)
            {
                case EMPTY_ELEMENT_NAMESPACE_CODE: result.Details.Coding[0].Display ="XML101"; break;
                case UNKNOWN_RESOURCE_TYPE_CODE: result.Details.Coding[0].Display ="XML102"; break;
                case RESOURCE_TYPE_NOT_A_RESOURCE_CODE: result.Details.Coding[0].Display ="XML103"; break;
                case UNKNOWN_ELEMENT_CODE: result.Details.Coding[0].Display ="XML104"; break;
                case CHOICE_ELEMENT_HAS_NO_TYPE_CODE: result.Details.Coding[0].Display ="XML105"; break;
                case CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE: result.Details.Coding[0].Display ="XML106"; break;
                case INCORRECT_XHTML_NAMESPACE_CODE: result.Details.Coding[0].Display ="XML107"; break;
                case UNKNOWN_ATTRIBUTE_CODE: result.Details.Coding[0].Display ="XML108"; break;
                case ELEMENT_OUT_OF_ORDER_CODE: result.Details.Coding[0].Display ="XML109"; break;
                case UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE: result.Details.Coding[0].Display ="XML110"; break;
                case NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE: result.Details.Coding[0].Display ="XML111"; break;
                case INCORRECT_ELEMENT_NAMESPACE_CODE: result.Details.Coding[0].Display ="XML112"; break;
                case UNALLOWED_NODE_TYPE_CODE: result.Details.Coding[0].Display ="XML113"; break;
                case INCORRECT_ATTRIBUTE_NAMESPACE_CODE: result.Details.Coding[0].Display ="XML114"; break;
                case ATTRIBUTE_HAS_EMPTY_VALUE_CODE: result.Details.Coding[0].Display ="XML115"; break;
                case ELEMENT_NOT_IN_SEQUENCE_CODE: result.Details.Coding[0].Display ="XML116"; break;
                case SCHEMALOCATION_DISALLOWED_CODE: result.Details.Coding[0].Display ="XML117"; break;
                case EXPECTED_OPENING_ELEMENT_CODE: result.Details.Coding[0].Display ="XML118"; break;
                case ENCOUNTERED_DTP_REFERENCES_CODE: result.Details.Coding[0].Display ="XML119"; break;
                case ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE: result.Details.Coding[0].Display ="XML120"; break;

                case INCORRECT_BASE64_DATA_CODE: result.Details.Coding[0].Display ="XML202"; break;
                case VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE: result.Details.Coding[0].Display ="XML203"; break;
            }

            return result;
        }

        public FhirXmlException(string errorCode, string message) : base(errorCode, message)
        {
        }

        public FhirXmlException(string code, string message, Exception? innerException) : base(code, message, innerException)
        {
        }

        internal FhirXmlException With(XmlReader reader, string locationPath, params object?[] parameters)
        {
            var location = reader.GenerateLocationMessage(out long lineNumber, out long position);
            return With(locationPath, lineNumber, position, location, null, parameters);
        }

        internal FhirXmlException With(string locationPath, long lineNumber, long position, string locationMessage, FhirXmlException? inner, params object?[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, Message, parameters);
            var message = $"{formattedMessage} {locationMessage}";

            return new FhirXmlException(ErrorCode, message, inner)
            {
                FormattedMessage = formattedMessage,
                LineNumber = lineNumber,
                Position = position,
                Location = locationPath,
                IssueSeverity = IssueSeverity,
                IssueType = IssueType,
            };
        }
    }
}

#nullable restore
