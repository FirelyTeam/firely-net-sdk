/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Text.Json;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

#nullable enable

namespace Hl7.Fhir.Serialization;

/// <summary>
/// An error found during deserialization of Json data. These errors do not present issues
/// with the Json itself, but issues in the data with regards to the rules for FHIR Json format described
/// in http://hl7.org/fhir/json.html.
/// </summary>
public class FhirJsonException(
    string errorCode,
    string baseMessage,
    string? display,
    string? instancePath,
    long? lineNumber,
    long? position,
    OperationOutcome.IssueSeverity issueSeverity,
    OperationOutcome.IssueType issueType,
    Exception? innerException = null)
    : ExtendedCodedException(errorCode, baseMessage, display, instancePath, lineNumber, position, issueSeverity, issueType,
        innerException)
{
    public const string RESOURCETYPE_SHOULD_BE_STRING_CODE = "JSON102";
    public const string NO_RESOURCETYPE_PROPERTY_CODE = "JSON103";
    public const string EXPECTED_PRIMITIVE_NOT_NULL_CODE = "JSON109";
    public const string CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE = "JSON114";
    public const string OBJECTS_CANNOT_BE_EMPTY_CODE = "JSON120";
    public const string ARRAYS_CANNOT_BE_EMPTY_CODE = "JSON121";
    public const string PRIMITIVE_ARRAYS_ONLY_NULL_CODE = "JSON125";
    public const string DUPLICATE_PROPERTY_CODE = "JSON129";
    public const string NESTED_ARRAY_CODE = "JSON130";
    public const string UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE_CODE = "JSON131";
    public const string UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE_CODE = "JSON132";
    public const string USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE_CODE = "JSON133";
    public const string UNDERSCORE_SHOULD_BE_OBJECT_CODE = "JSON134";

    // Fatal errors - there is dataloss so processing should not continue.
    internal static FhirJsonException DUPLICATE_PROPERTY(ref Utf8JsonReader reader, string instancePath, string propName) => Initialize(ref reader, instancePath, DUPLICATE_PROPERTY_CODE, $"Encountered duplicate property '{propName}'.", "Duplicate property", OO_Sev.Fatal);

    // Non Fatal errors - All data present in the parsed data could be retrieved and
    // captured in the POCO model (maybe using overflow), even if the syntax or the data was not fully FHIR compliant.

    // The serialization contained a json null where it is not allowed, but a null does not contain data anyway.
    internal static FhirJsonException EXPECTED_PRIMITIVE_NOT_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_PRIMITIVE_NOT_NULL_CODE, "Expected a primitive value, not a json null.", "Null not allowed", OO_Sev.Error);

    // Nested arrays are flattened, so no data loss occurs.
    internal static FhirJsonException NESTED_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, NESTED_ARRAY_CODE, "Nested array detected.", "Invalid nested array", OO_Sev.Error);

    // We will just ignore the underscore and keep on parsing
    internal static FhirJsonException USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE(ref Utf8JsonReader reader, string instancePath, string elementName, string propertyName) => Initialize(ref reader, instancePath, USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE_CODE, $"Element '{elementName}' is not a FHIR primitive, so it should not use an underscore in the '{propertyName}' property.", "Invalid _ in name", OO_Sev.Error);

    // We will add the primitive value as a "value" property to the POCO, no data loss.
    internal static FhirJsonException UNDERSCORE_SHOULD_BE_OBJECT(ref Utf8JsonReader reader, string instancePath, string propertyName) => Initialize(ref reader, instancePath, UNDERSCORE_SHOULD_BE_OBJECT_CODE, $"Property '{propertyName}' has an underscore, which should be a (an array of) Json object or null.", "Property should be an object", OO_Sev.Error);

    internal static FhirJsonException UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE(ref Utf8JsonReader reader, string instancePath, string elementName) => Initialize(ref reader, instancePath, UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE_CODE, $"Encountered a json primitive while expecting a json object for non-primitive element '{elementName}'.", "Expected object", OO_Sev.Error);

    internal static FhirJsonException UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE(ref Utf8JsonReader reader, string instancePath, string elementName) => Initialize(ref reader, instancePath, UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE_CODE, $"Encountered an unexpected json object while reading the value for primitive element '{elementName}'.", "Expected primitive", OO_Sev.Error);

    // Empty objects and arrays can be ignored without discarding data
    internal static FhirJsonException OBJECTS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, OBJECTS_CANNOT_BE_EMPTY_CODE, "An object needs to have at least one property.", "Objects cannot be empty", OO_Sev.Error);

    // We define this as: "empty lists are allowed in the model, but not in json",
    // but otherwise, empty lists do not cause data loss.
    internal static FhirJsonException ARRAYS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, ARRAYS_CANNOT_BE_EMPTY_CODE, "An array needs to have at least one element.", "Array cannot be empty", OO_Sev.Error);

    // This leaves the incorrect nulls in place, no change in data.
    internal static FhirJsonException PRIMITIVE_ARRAYS_ONLY_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, PRIMITIVE_ARRAYS_ONLY_NULL_CODE, "Arrays need to have at least one non-null element.", "Only nulls in array", OO_Sev.Error);

    // This will use a DynamicXXX, so no data loss.
    internal static FhirJsonException CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(ref Utf8JsonReader reader, string instancePath, string elementName) => Initialize(ref reader, instancePath, CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE, $"Choice element '{elementName}' should be suffixed by a type.", "Expected type suffix", OO_Sev.Error);

    // Will store the data as a DynamicResource
    internal static FhirJsonException RESOURCETYPE_SHOULD_BE_STRING(ref Utf8JsonReader reader, string instancePath, JsonTokenType valueToken, string value) => Initialize(ref reader, instancePath, RESOURCETYPE_SHOULD_BE_STRING_CODE, $"Property 'resourceType' should be a string, but found token {valueToken} with value {value}.", "Invalid resource type", OO_Sev.Error);
    internal static FhirJsonException NO_RESOURCETYPE_PROPERTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, NO_RESOURCETYPE_PROPERTY_CODE, "Resource has no 'resourceType' property.", "Missing ResourceType", OO_Sev.Error);

    /// <summary>
    /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer
    /// FHIR release. This means allowing unknown elements, codes and types in a choice element. Note that the POCO model cannot capture
    /// these newer elements and data, so this means data loss may occur.
    /// </summary>
    internal static readonly string[] BACKWARDS_COMPATIBILITY_ALLOWED_ISSUES =
    {
        CodedValidationException.INVALID_CODED_VALUE_CODE,
        CodedValidationException.UNKNOWN_ELEMENT_CODE,
        CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE,
        CodedValidationException.UNKNOWN_RESOURCE_TYPE_CODE
    };

    internal static FhirJsonException Initialize(ref Utf8JsonReader reader, string instancePath, string code, string message, string display, OO_Sev issueSeverity, OO_Typ issueType = OO_Typ.Structure, Exception? innerException = null)
    {
        var (lineNumber, position) = reader.GetLocation();

        // If the reader is on a primitive token, we need to adjust the position
        // because the position is the start of the token, not the end.
        if (BaseFhirJsonDeserializer.IsOnJsonPrimitiveToken(ref reader))
        {
            var length = reader.GetRawText().Length;
            position -= length;
        }

        return new FhirJsonException(
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

    public FhirJsonException? CloneWith(string baseMessage, string? display, OO_Sev issueSeverity, OO_Typ issueType) =>
        new(ErrorCode, baseMessage, display, InstancePath, LineNumber, Position,
            issueSeverity, issueType);
}