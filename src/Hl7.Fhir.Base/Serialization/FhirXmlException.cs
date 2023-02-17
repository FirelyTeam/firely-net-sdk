#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlException : CodedException
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

        internal static readonly FhirXmlException EMPTY_ELEMENT_NAMESPACE = new(EMPTY_ELEMENT_NAMESPACE_CODE, $"The element '{0}' has no namespace, expected the HL7 FHIR namespace ({XmlNs.FHIR})");
        internal static readonly FhirXmlException INCORRECT_ELEMENT_NAMESPACE = new(INCORRECT_ELEMENT_NAMESPACE_CODE, "The element '{0}' uses the namespace '{1}', which is not allowed.");
        internal static readonly FhirXmlException UNKNOWN_RESOURCE_TYPE = new(UNKNOWN_RESOURCE_TYPE_CODE, "Unknown type '{0}' found in root property.");
        internal static readonly FhirXmlException RESOURCE_TYPE_NOT_A_RESOURCE = new(RESOURCE_TYPE_NOT_A_RESOURCE_CODE, "Data type '{0}' in property 'resourceType' is not a type of resource.");
        internal static readonly FhirXmlException UNKNOWN_ELEMENT = new(UNKNOWN_ELEMENT_CODE, "Encountered unrecognized element '{0}'.");
        internal static readonly FhirXmlException CHOICE_ELEMENT_HAS_NO_TYPE = new(CHOICE_ELEMENT_HAS_NO_TYPE_CODE, "Choice element '{0}' is not suffixed with a type.");
        internal static readonly FhirXmlException CHOICE_ELEMENT_HAS_UNKOWN_TYPE = new(CHOICE_ELEMENT_HAS_UNKNOWN_TYPE_CODE, "Choice element '{0}' is suffixed with an unrecognized type '{1}'.");
        internal static readonly FhirXmlException INCORRECT_BASE64_DATA = new(INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.");
        internal static readonly FhirXmlException VALUE_IS_NOT_OF_EXPECTED_TYPE = new(VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, "Literal string '{0}' cannot be parsed as a '{1}'.");
        internal static readonly FhirXmlException INCORRECT_XHTML_NAMESPACE = new(INCORRECT_XHTML_NAMESPACE_CODE, $"Narrative has incorrect namespace. Namespace should be {XmlNs.XHTML}");
        internal static readonly FhirXmlException UNKNOWN_ATTRIBUTE = new(UNKNOWN_ATTRIBUTE_CODE, "Encountered unrecognized attribute '{0}'.");
        internal static readonly FhirXmlException ELEMENT_OUT_OF_ORDER = new(ELEMENT_OUT_OF_ORDER_CODE, "Element '{0}' is not in the correct order ");
        internal static readonly FhirXmlException ELEMENT_NOT_IN_SEQUENCE = new(ELEMENT_NOT_IN_SEQUENCE_CODE, "Element with name '{0}' was found multiple times, but not in sequence.");
        internal static readonly FhirXmlException UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER = new(UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE, "Encountered unallowed content '{0}' in the resource container. Only a single resource is allowed.");
        internal static readonly FhirXmlException NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER = new(NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE, "The element '{0}' has a contained resource and therefore should not have attributes.");
        internal static readonly FhirXmlException UNALLOWED_NODE_TYPE = new(UNALLOWED_NODE_TYPE_CODE, "Xml node of type '{0}' is unexpected at this point");
        internal static readonly FhirXmlException INCORRECT_ATTRIBUTE_NAMESPACE = new(INCORRECT_ATTRIBUTE_NAMESPACE_CODE, "The attribute '{0}' in element '{1}' uses the namespace '{2}', which is not allowed.");
        internal static readonly FhirXmlException ATTRIBUTE_HAS_EMPTY_VALUE = new(ATTRIBUTE_HAS_EMPTY_VALUE_CODE, "Attributes cannot be empty. Either they are absent, or they are present with at least one character of non - whitespace content");
        internal static readonly FhirXmlException SCHEMALOCATION_DISALLOWED = new(SCHEMALOCATION_DISALLOWED_CODE, "The 'schemaLocation' attribute is disallowed.");
        internal static readonly FhirXmlException EXPECTED_OPENING_ELEMENT = new(EXPECTED_OPENING_ELEMENT_CODE, "Expected opening element, but found {0}.");
        internal static readonly FhirXmlException ENCOUNTERED_DTD_REFERENCES = new(ENCOUNTERED_DTP_REFERENCES_CODE, "There SHALL be no DTD references in FHIR resources (because of the XXE security exploit)");
        internal static readonly FhirXmlException ELEMENT_HAS_NO_VALUE_OR_CHILDREN = new(ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, "Element '{0}' must have child elements and / or a value attribute");

        public FhirXmlException(string errorCode, string message) : base(errorCode, message)
        {
        }

        public FhirXmlException(string code, string message, Exception? innerException) : base(code, message, innerException)
        {
        }

        internal FhirXmlException With(XmlReader reader, params object?[] parameters) =>
          With(reader, inner: null, parameters);

        internal FhirXmlException With(XmlReader reader, FhirXmlException? inner, params object?[] parameters)
        {
            var location = reader.GenerateLocationMessage();
            return With(location, inner, parameters);
        }

        internal FhirXmlException With(string locationMessage, FhirXmlException? inner, params object?[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, Message, parameters);
            var message = $"{formattedMessage} {locationMessage}";

            return new FhirXmlException(ErrorCode, message, inner);
        }

    }
}

#nullable restore
