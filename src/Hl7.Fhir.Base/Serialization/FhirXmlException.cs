#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Xml;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlException : ExtendedCodedException
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
        [Obsolete("This name contains a spelling mistake, use ENCOUNTERED_DTD_REFERENCES_CODE instead.")]
        public const string ENCOUNTERED_DTP_REFERENCES_CODE = "XML119";
        public const string ENCOUNTERED_DTD_REFERENCES_CODE = "XML119";
        public const string ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE = "XML120";
        public const string INVALID_DUPLICATE_PROPERTY_CODE = "XML121";

        public const string INCORRECT_BASE64_DATA_CODE = "XML202";
        public const string VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE = "XML203";

        // ==========================================
        // Unrecoverable Errors - when adding a new error, also add it to the appropriate error collections below.
        // ==========================================
        internal static FhirXmlException UNKNOWN_RESOURCE_TYPE(XmlReader reader, string instancePath, string typeName) => Initialize(reader, instancePath, UNKNOWN_RESOURCE_TYPE_CODE, $"Unknown type '{typeName}' found in root property.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException RESOURCE_TYPE_NOT_A_RESOURCE(XmlReader reader, string instancePath, string resourceType) => Initialize(reader, instancePath, RESOURCE_TYPE_NOT_A_RESOURCE_CODE, $"Data type '{resourceType}' in property 'resourceType' is not a type of resource.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException UNKNOWN_ELEMENT(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, UNKNOWN_ELEMENT_CODE, $"Encountered unrecognized element '{elementName}'.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException CHOICE_ELEMENT_HAS_NO_TYPE(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, CHOICE_ELEMENT_HAS_NO_TYPE_CODE, $"Choice element '{elementName}' is not suffixed with a type.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException CHOICE_ELEMENT_HAS_UNKOWN_TYPE(XmlReader reader, string instancePath, string elementName, string typeSuffix) => Initialize(reader, instancePath, CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE, $"Choice element '{elementName}' is suffixed with an unrecognized type '{typeSuffix}'.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException UNKNOWN_ATTRIBUTE(XmlReader reader, string instancePath, string s0) => Initialize(reader, instancePath, UNKNOWN_ATTRIBUTE_CODE, $"Encountered unrecognized attribute '{s0}'.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER(XmlReader reader, string instancePath, string s0) => Initialize(reader, instancePath, UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE, $"Encountered unallowed content '{s0}' in the resource container. Only a single resource is allowed.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE, $"Element '{elementName}' has a contained resource and therefore should not have attributes.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException UNALLOWED_NODE_TYPE(XmlReader reader, string instancePath, string s0) => Initialize(reader, instancePath, UNALLOWED_NODE_TYPE_CODE, $"Xml node of type '{s0}' is unexpected at this point", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException EXPECTED_OPENING_ELEMENT(XmlReader reader, string instancePath, string openElementName) => Initialize(reader, instancePath, EXPECTED_OPENING_ELEMENT_CODE, $"Expected opening element, but found {openElementName}.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirXmlException INVALID_DUPLICATE_PROPERTY(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, INVALID_DUPLICATE_PROPERTY_CODE, $"Element '{elementName}' is not permitted to repeat", OO_Sev.Error, OO_Typ.Structure);

        // ==========================================
        // Recoverable Errors - when adding a new error, also add it to the appropriate error collections below.
        // ==========================================
        // Although the namespace is not correct, we continue as if it was.
        internal static FhirXmlException EMPTY_ELEMENT_NAMESPACE(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, EMPTY_ELEMENT_NAMESPACE_CODE, $"Element '{elementName}' has no namespace, expected the HL7 FHIR namespace ({XmlNs.FHIR})", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_ELEMENT_NAMESPACE(XmlReader reader, string instancePath, string elementName, string s1) => Initialize(reader, instancePath, INCORRECT_ELEMENT_NAMESPACE_CODE, $"Element '{elementName}' uses the namespace '{s1}', which is not allowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_XHTML_NAMESPACE(XmlReader reader, string instancePath) => Initialize(reader, instancePath, INCORRECT_XHTML_NAMESPACE_CODE, $"Narrative has incorrect namespace. Namespace should be {XmlNs.XHTML}", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException INCORRECT_ATTRIBUTE_NAMESPACE(XmlReader reader, string instancePath, string localName, string elementName, string namespaceURI) => Initialize(reader, instancePath, INCORRECT_ATTRIBUTE_NAMESPACE_CODE, $"The attribute '{localName}' in element '{elementName}' uses the namespace '{namespaceURI}', which is not allowed.", OO_Sev.Error, OO_Typ.Structure);

        // These errors signal parsing errors, but the original raw data is retained in the POCO so no data is lost.
        internal static FhirXmlException INCORRECT_BASE64_DATA(XmlReader reader, string instancePath) => Initialize(reader, instancePath, INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.", OO_Sev.Error, OO_Typ.Value);
        internal static FhirXmlException VALUE_IS_NOT_OF_EXPECTED_TYPE(XmlReader reader, string instancePath, string trimmedValue, string typeName) => Initialize(reader, instancePath, VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, $"Literal string '{trimmedValue}' cannot be parsed as a '{typeName}'.", OO_Sev.Error, OO_Typ.Structure);

        // An incorrect order does not mean we cannot parse the data safely
        internal static FhirXmlException ELEMENT_OUT_OF_ORDER(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, ELEMENT_OUT_OF_ORDER_CODE, $"Element '{elementName}' is not in the correct order ", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_NOT_IN_SEQUENCE(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, ELEMENT_NOT_IN_SEQUENCE_CODE, $"Element '{elementName}' was found multiple times, but not in sequence.", OO_Sev.Error, OO_Typ.Structure);

        // Empty values will result in nulls, but no data is lost.
        internal static FhirXmlException ATTRIBUTE_HAS_EMPTY_VALUE(XmlReader reader, string instancePath) => Initialize(reader, instancePath, ATTRIBUTE_HAS_EMPTY_VALUE_CODE, "Attributes cannot be empty. Either they are absent, or they are present with at least one character of non - whitespace content", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, $"Element '{elementName}' must have child elements and / or a value attribute", OO_Sev.Error, OO_Typ.Structure);
        internal static FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN(string instancePath, int lineNumber, int position, string? locationMessage, string? localName)
        {
            var message = $"Element '{localName}' must have child elements and / or a value attribute";

            return new FhirXmlException(
                ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE,
                message, instancePath, lineNumber, position, OO_Sev.Error, OO_Typ.Structure);
        }

        // Xml paraphernalia that do not contain data so they can be safely skipped.
        internal static FhirXmlException SCHEMALOCATION_DISALLOWED(XmlReader reader, string instancePath) => Initialize(reader, instancePath, SCHEMALOCATION_DISALLOWED_CODE, "The 'schemaLocation' attribute is disallowed.", OO_Sev.Warning, OO_Typ.Structure);
        internal static FhirXmlException ENCOUNTERED_DTD_REFERENCES(XmlReader reader, string instancePath) => Initialize(reader, instancePath, ENCOUNTERED_DTD_REFERENCES_CODE, "There SHALL be no DTD references in FHIR resources (because of the XXE security exploit)", OO_Sev.Warning, OO_Typ.Structure);

        /// <summary>
        /// Whether this issue leads to dataloss or not. Recoverable issues mean that all data present in the parsed data could be retrieved and
        /// captured in the POCO model, even if the syntax or the data was not fully FHIR compliant.
        /// </summary>
        internal static bool IsRecoverableIssue(FhirXmlException e) =>
            e.ErrorCode is EMPTY_ELEMENT_NAMESPACE_CODE or
            INCORRECT_ELEMENT_NAMESPACE_CODE or
            INCORRECT_XHTML_NAMESPACE_CODE or
            INCORRECT_ATTRIBUTE_NAMESPACE_CODE or
            INCORRECT_BASE64_DATA_CODE or
            VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE or
            ELEMENT_OUT_OF_ORDER_CODE or
            ELEMENT_NOT_IN_SEQUENCE_CODE or
            ATTRIBUTE_HAS_EMPTY_VALUE_CODE or
            ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE or
            SCHEMALOCATION_DISALLOWED_CODE or
            ENCOUNTERED_DTD_REFERENCES_CODE;

        /// <summary>
        /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer 
        /// FHIR release. This means allowing unknown elements, attributes, codes and types in a choice element. Note that the POCO model cannot capture
        /// these newer elements and data, so this means data loss may occur.
        /// </summary>
        internal static bool AllowedForBackwardsCompatibility(CodedException e) =>
            e.ErrorCode is CodedValidationException.INVALID_CODED_VALUE_CODE or
            UNKNOWN_ELEMENT_CODE or
            CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE or
            UNKNOWN_ATTRIBUTE_CODE;

        public FhirXmlException(string code, string message)
        : base(code, message, null, null, null, OO_Sev.Error, OO_Typ.Unknown)
        {
            // Nothing
        }

        public FhirXmlException(string code, string message, Exception? innerException)
            : base(code, message, null, null, null, OO_Sev.Error, OO_Typ.Unknown, innerException)
        {
            // Nothing
        }

        public FhirXmlException(
            string errorCode,
            string baseMessage,
            string? instancePath,
            long? lineNumber,
            long? position,
            OperationOutcome.IssueSeverity issueSeverity,
            OperationOutcome.IssueType issueType,
            Exception? innerException = null) :
                base(errorCode, baseMessage, instancePath, lineNumber, position, issueSeverity, issueType, innerException)
        {
            // Nothing
        }

        internal static FhirXmlException Initialize(XmlReader reader, string instancePath, string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, FhirXmlException? innerException = null)
        {
            var (lineNumber, position) = reader.GenerateLineInfo();

            return new FhirXmlException(
                code,
                message,
                instancePath,
                lineNumber,
                position,
                issueSeverity,
                issueType,
                innerException);
        }

        public FhirXmlException? CloneWith(string baseMessage, OO_Sev issueSeverity, OO_Typ issueType) =>
           new FhirXmlException(ErrorCode, baseMessage, InstancePath, LineNumber, Position,
                   issueSeverity, issueType);
    }
}

#nullable restore
