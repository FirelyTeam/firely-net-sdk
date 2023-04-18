#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

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
        public const string INVALID_DUPLICATE_PROPERTY_CODE = "XML121";

        public const string INCORRECT_BASE64_DATA_CODE = "XML202";
        public const string VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE = "XML203";

        // ==========================================
        // Unrecoverable Errors (or data loss)
        // ==========================================
        internal static FhirXmlException UNKNOWN_RESOURCE_TYPE(XmlReader reader, string locationPath, string typeName) => Initialize(reader, locationPath, UNKNOWN_RESOURCE_TYPE_CODE, $"Unknown type '{typeName}' found in root property.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException RESOURCE_TYPE_NOT_A_RESOURCE(XmlReader reader, string locationPath, string resourceType) => Initialize(reader, locationPath, RESOURCE_TYPE_NOT_A_RESOURCE_CODE, $"Data type '{resourceType}' in property 'resourceType' is not a type of resource.", OO_Sev.Fatal, OO_Typ.Structure);

        // ==========================================
        // Recoverable Errors
        // ==========================================
        internal static FhirXmlException EMPTY_ELEMENT_NAMESPACE(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, EMPTY_ELEMENT_NAMESPACE_CODE, $"Element '{elementName}' has no namespace, expected the HL7 FHIR namespace ({XmlNs.FHIR})", OO_Sev.Error, OO_Typ.Structure); // Element namespace isn't fatal, but resource level is
        internal static FhirXmlException INCORRECT_ELEMENT_NAMESPACE(XmlReader reader, string locationPath, string elementName, string s1) => Initialize(reader, locationPath, INCORRECT_ELEMENT_NAMESPACE_CODE, $"Element '{elementName}' uses the namespace '{s1}', which is not allowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException UNKNOWN_ELEMENT(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, UNKNOWN_ELEMENT_CODE, $"Encountered unrecognized element '{elementName}'.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException CHOICE_ELEMENT_HAS_NO_TYPE(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, CHOICE_ELEMENT_HAS_NO_TYPE_CODE, $"Choice element '{elementName}' is not suffixed with a type.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException CHOICE_ELEMENT_HAS_UNKOWN_TYPE(XmlReader reader, string locationPath, string elementName, string typeSuffix) => Initialize(reader, locationPath, CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE, $"Choice element '{elementName}' is suffixed with an unrecognized type '{typeSuffix}'.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_BASE64_DATA(XmlReader reader, string locationPath) => Initialize(reader, locationPath, INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.", OO_Sev.Error, OO_Typ.Value);
        internal static FhirXmlException VALUE_IS_NOT_OF_EXPECTED_TYPE(XmlReader reader, string locationPath, string trimmedValue, string typeName) => Initialize(reader, locationPath, VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, $"Literal string '{trimmedValue}' cannot be parsed as a '{typeName}'.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_XHTML_NAMESPACE(XmlReader reader, string locationPath) => Initialize(reader, locationPath, INCORRECT_XHTML_NAMESPACE_CODE, $"Narrative has incorrect namespace. Namespace should be {XmlNs.XHTML}", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException UNKNOWN_ATTRIBUTE(XmlReader reader, string locationPath, string s0) => Initialize(reader, locationPath, UNKNOWN_ATTRIBUTE_CODE, $"Encountered unrecognized attribute '{s0}'.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_OUT_OF_ORDER(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, ELEMENT_OUT_OF_ORDER_CODE, $"Element '{elementName}' is not in the correct order ", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_NOT_IN_SEQUENCE(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, ELEMENT_NOT_IN_SEQUENCE_CODE, $"Element '{elementName}' was found multiple times, but not in sequence.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER(XmlReader reader, string locationPath, string s0) => Initialize(reader, locationPath, UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE, $"Encountered unallowed content '{s0}' in the resource container. Only a single resource is allowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE, $"Element '{elementName}' has a contained resource and therefore should not have attributes.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException UNALLOWED_NODE_TYPE(XmlReader reader, string locationPath, string s0) => Initialize(reader, locationPath, UNALLOWED_NODE_TYPE_CODE, $"Xml node of type '{s0}' is unexpected at this point", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_ATTRIBUTE_NAMESPACE(XmlReader reader, string locationPath, string localName, string elementName, string namespaceURI) => Initialize(reader, locationPath, INCORRECT_ATTRIBUTE_NAMESPACE_CODE, $"The attribute '{localName}' in element '{elementName}' uses the namespace '{namespaceURI}', which is not allowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ATTRIBUTE_HAS_EMPTY_VALUE(XmlReader reader, string locationPath) => Initialize(reader, locationPath, ATTRIBUTE_HAS_EMPTY_VALUE_CODE, "Attributes cannot be empty. Either they are absent, or they are present with at least one character of non - whitespace content", OO_Sev.Warning, OO_Typ.Structure);
        internal static FhirXmlException SCHEMALOCATION_DISALLOWED(XmlReader reader, string locationPath) => Initialize(reader, locationPath, SCHEMALOCATION_DISALLOWED_CODE, "The 'schemaLocation' attribute is disallowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException EXPECTED_OPENING_ELEMENT(XmlReader reader, string locationPath, string openElementName) => Initialize(reader, locationPath, EXPECTED_OPENING_ELEMENT_CODE, $"Expected opening element, but found {openElementName}.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ENCOUNTERED_DTD_REFERENCES(XmlReader reader, string locationPath) => Initialize(reader, locationPath, ENCOUNTERED_DTP_REFERENCES_CODE, "There SHALL be no DTD references in FHIR resources (because of the XXE security exploit)", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, $"Element '{elementName}' must have child elements and / or a value attribute", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INVALID_DUPLICATE_PROPERTY(XmlReader reader, string locationPath, string elementName) => Initialize(reader, locationPath, INVALID_DUPLICATE_PROPERTY_CODE, $"Element '{elementName}' is not permitted to repeat", OO_Sev.Error, OO_Typ.Structure);

        internal static FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN(string locationPath, int lineNumber, int position, string? locationMessage, string? localName)
        {
            var message = $"Element '{localName}' must have child elements and / or a value attribute";
            var messageWithLocation = $"{message} {locationMessage}";

            return new FhirXmlException(ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, messageWithLocation, OO_Sev.Error, OO_Typ.Structure)
            {
                FormattedMessage = message,
                LineNumber = lineNumber,
                Position = position,
                Location = locationPath,
            };
        }

        public FhirXmlException(string errorCode, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType) : base(errorCode, message, issueSeverity, issueType)
        {
        }

        public FhirXmlException(string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, Exception? innerException) : base(code, message, issueSeverity, issueType, innerException)
        {
        }

        internal static FhirXmlException Initialize(XmlReader reader, string locationPath, string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, FhirXmlException? innerException = null)
        {
            var location = reader.GenerateLocationMessage(out long lineNumber, out long position);
            var messageWithLocation = $"{message} {location}";

            return new FhirXmlException(code, message, issueSeverity, issueType, innerException)
            {
                FormattedMessage = message,
                LineNumber = lineNumber,
                Position = position,
                Location = locationPath,
            };
        }
    }
}

#nullable restore
