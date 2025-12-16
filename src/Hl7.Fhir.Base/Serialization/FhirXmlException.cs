/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Xml;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

namespace Hl7.Fhir.Serialization;

public class FhirXmlException(
    string errorCode,
    string baseMessage,
    string? display,
    string? instancePath,
    long? lineNumber,
    long? position,
    OO_Sev issueSeverity,
    OO_Typ issueType = OO_Typ.Structure,
    Exception? innerException = null)
    : ExtendedCodedException(errorCode, baseMessage, display, instancePath, lineNumber, position, issueSeverity, issueType,
        innerException)
{
    public const string EMPTY_ELEMENT_NAMESPACE_CODE = "XML101";
    public const string CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE = "XML105";
    public const string INCORRECT_XHTML_NAMESPACE_CODE = "XML107";
    public const string ELEMENT_OUT_OF_ORDER_CODE = "XML109";
    public const string MULTIPLE_ELEMENTS_IN_RESOURCE_CONTAINER_CODE = "XML110";
    public const string NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE = "XML111";
    public const string INCORRECT_ELEMENT_NAMESPACE_CODE = "XML112";
    public const string DISALLOWED_NODE_TYPE_CODE = "XML113";
    public const string INCORRECT_ATTRIBUTE_NAMESPACE_CODE = "XML114";
    public const string ELEMENT_NOT_IN_SEQUENCE_CODE = "XML116";
    public const string SCHEMALOCATION_DISALLOWED_CODE = "XML117";
    public const string EMPTY_RESOURCE_CONTAINER_CODE = "XML122";
    public const string ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE_CODE = "XML124";
    public const string ATTRIBUTE_SHOULD_HAVE_BEEN_AN_ELEMENT_CODE = "XML125";
    public const string STRING_SHOULD_NOT_HAVE_LEADING_OR_TRAILING_WHITESPACE = "XML126";

    // Fatal errors - there is dataloss so processing should not continue.
    internal static FhirXmlException MULTIPLE_ELEMENTS_IN_RESOURCE_CONTAINER(XmlReader reader, string instancePath) => Initialize(reader, instancePath, MULTIPLE_ELEMENTS_IN_RESOURCE_CONTAINER_CODE, $"Encountered multiple elements in a resource container. Only a single resource is allowed.", "Multiple resources in contained", OO_Sev.Fatal);
    internal static FhirXmlException NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(XmlReader reader, string instancePath, string attributeName) => Initialize(reader, instancePath, NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER_CODE, $"Encountered unexpected attribute '{attributeName}' in a resource container. Only a single resource is allowed.", "Attributes on contained", OO_Sev.Fatal);
    internal static FhirXmlException DISALLOWED_NODE_TYPE(XmlReader reader, string instancePath, string nodeType) => Initialize(reader, instancePath, DISALLOWED_NODE_TYPE_CODE, $"Xml node of type '{nodeType}' is unexpected at this point", "Invalid XML node type", OO_Sev.Fatal);

    // Non Fatal errors - All data present in the parsed data could be retrieved and
    // captured in the POCO model (maybe using overflow), even if the syntax or the data was not fully FHIR compliant.

    // Although the namespace is not correct, we continue as if it was.
    internal static FhirXmlException EMPTY_ELEMENT_NAMESPACE(XmlReader reader, string instancePath) => Initialize(reader, instancePath, EMPTY_ELEMENT_NAMESPACE_CODE, $"Element has no namespace, expected the HL7 FHIR namespace ({XmlNs.FHIR})", "Empty Element Namespace", OO_Sev.Error);
    internal static FhirXmlException INCORRECT_ELEMENT_NAMESPACE(XmlReader reader, string instancePath, string @namespace) => Initialize(reader, instancePath, INCORRECT_ELEMENT_NAMESPACE_CODE, $"Element uses the namespace '{@namespace}', which is not allowed.", "Invalid element namespace", OO_Sev.Error);
    internal static FhirXmlException INCORRECT_XHTML_NAMESPACE(XmlReader reader, string instancePath) => Initialize(reader, instancePath, INCORRECT_XHTML_NAMESPACE_CODE, $"Narrative has incorrect namespace. Namespace should be {XmlNs.XHTML}", "Narrative must use xhtml namespace", OO_Sev.Error);
    internal static FhirXmlException INCORRECT_ATTRIBUTE_NAMESPACE(XmlReader reader, string instancePath, string namespaceUri) => Initialize(reader, instancePath, INCORRECT_ATTRIBUTE_NAMESPACE_CODE, $"Attribute uses namespace '{namespaceUri}', which is not allowed.", "Invalid attribute namespace", OO_Sev.Error);

    // An incorrect order does not mean we cannot parse the data safely
    internal static FhirXmlException ELEMENT_OUT_OF_ORDER(XmlReader reader, string instancePath, string elementName, string after) => Initialize(reader, instancePath, ELEMENT_OUT_OF_ORDER_CODE, $"Element '{elementName}' is not in the correct order, should go before element '{after}'.", "XML Element out of order", OO_Sev.Error);
    internal static FhirXmlException ELEMENT_NOT_IN_SEQUENCE(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, ELEMENT_NOT_IN_SEQUENCE_CODE, $"Element '{elementName}' was found multiple times, but not in sequence.", "Invalid repeating element", OO_Sev.Error);

    // Xml paraphernalia that do not contain data so they can be safely skipped.
    internal static FhirXmlException SCHEMALOCATION_DISALLOWED(XmlReader reader, string instancePath) => Initialize(reader, instancePath, SCHEMALOCATION_DISALLOWED_CODE, "The 'schemaLocation' attribute is disallowed.", "No schemalocation allowed", OO_Sev.Warning);

    // Empty resource containers are not allowed in FHIR, but there is no data loss.
    internal static FhirXmlException EMPTY_RESOURCE_CONTAINER(XmlReader reader, string instancePath) => Initialize(reader, instancePath, EMPTY_RESOURCE_CONTAINER_CODE, $"Encountered an empty resource container.", "Empty contained resource", OO_Sev.Error);

    // This will use a DynamicXXX, so no data loss.
    internal static FhirXmlException CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE, "Choice element names should be suffixed by a type.", "Expected type suffix", OO_Sev.Error);

    // We'll be capturing its content, even if it was incorrectly an attribute or element
    internal static FhirXmlException ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE(XmlReader reader, string instancePath, string elementName) => Initialize(reader, instancePath, ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE_CODE, $"Element '{elementName}' should have been encoded as an attribute.", "Expected attribute", OO_Sev.Error);
    internal static FhirXmlException ATTRIBUTE_SHOULD_HAVE_BEEN_AN_ELEMENT(XmlReader reader, string instancePath, string attributeName) => Initialize(reader, instancePath, ATTRIBUTE_SHOULD_HAVE_BEEN_AN_ELEMENT_CODE, $"Attribute '{attributeName}' should have been an element with a `value` property, not an attribute.", "Expected element", OO_Sev.Error);
    
    // XML strings do not accept leading or trailing whitespaces and will become trimmed during parsing
    internal static FhirXmlException STRING_SHOULD_NOT_HAVE_LEADING_TRAILING_WHITESPACE(XmlReader reader, string instancePath, string attributeName) => Initialize(reader, instancePath, STRING_SHOULD_NOT_HAVE_LEADING_OR_TRAILING_WHITESPACE, $"Attribute '{attributeName}' should not contain  leading or trailing whitespace.", "Invalid whitespace", OO_Sev.Warning);

    /// <summary>
    /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer
    /// FHIR release. This means allowing unknown elements, attributes, codes and types in a choice element. Note that the POCO model cannot capture
    /// these newer elements and data, so this means data loss may occur.
    /// </summary>
    internal static readonly string[] BACKWARDS_COMPATIBILITY_ALLOWED_ISSUES =
    [
        CodedValidationException.INVALID_CODED_VALUE_CODE,
        CodedValidationException.UNKNOWN_ELEMENT_CODE,
        CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE,
        CodedValidationException.UNKNOWN_RESOURCE_TYPE_CODE
    ];


    internal static FhirXmlException Initialize(XmlReader reader, string instancePath, string code,
        string message, string display, OO_Sev issueSeverity, OO_Typ issueType = OO_Typ.Structure, FhirXmlException? innerException = null)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        return new FhirXmlException(
            code,
            message,
            display,
            instancePath,
            lineNumber,
            position,
            issueSeverity,
            issueType,
            innerException);
    }

    public FhirXmlException CloneWith(string baseMessage, string? display, OO_Sev issueSeverity, OO_Typ issueType) =>
        new(ErrorCode, baseMessage, display, InstancePath, LineNumber, Position,
            issueSeverity, issueType);
}