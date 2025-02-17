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

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// An error found during deserialization of Json data. These errors do not present issues
    /// with the Json itself, but issues in the data with regards to the rules for FHIR Json format described
    /// in http://hl7.org/fhir/json.html.
    /// </summary>
    public class FhirJsonException : ExtendedCodedException
    {
        public const string EXPECTED_START_OF_OBJECT_CODE = "JSON101";
        public const string RESOURCETYPE_SHOULD_BE_STRING_CODE = "JSON102";
        public const string NO_RESOURCETYPE_PROPERTY_CODE = "JSON103";
        public const string EXPECTED_PRIMITIVE_NOT_OBJECT_CODE = "JSON104";
        public const string EXPECTED_PRIMITIVE_NOT_ARRAY_CODE = "JSON105";
        public const string EXPECTED_PRIMITIVE_NOT_NULL_CODE = "JSON109";
        public const string EXPECTED_START_OF_ARRAY_CODE = "JSON111";
        public const string USE_OF_UNDERSCORE_ILLEGAL_CODE = "JSON113";
        public const string CHOICE_ELEMENT_HAS_NO_TYPE_CODE = "JSON114";
        public const string CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE = "JSON115";
        public const string UNKNOWN_RESOURCE_TYPE_CODE = "JSON116";
        public const string RESOURCE_TYPE_NOT_A_RESOURCE_CODE = "JSON117";
        public const string UNKNOWN_PROPERTY_FOUND_CODE = "JSON118";
        public const string OBJECTS_CANNOT_BE_EMPTY_CODE = "JSON120";
        public const string ARRAYS_CANNOT_BE_EMPTY_CODE = "JSON121";
        public const string PRIMITIVE_ARRAYS_ONLY_NULL_CODE = "JSON125";
        public const string PROPERTY_MAY_NOT_BE_EMPTY_CODE = "JSON127";
        public const string DUPLICATE_ARRAY_CODE = "JSON128";
        public const string DUPLICATE_PROPERTY_CODE = "JSON129";

        // ==========================================
        // Unrecoverable Errors
        // ==========================================
        internal static FhirJsonException EXPECTED_START_OF_OBJECT(ref Utf8JsonReader reader, string instancePath, JsonTokenType value) => Initialize(ref reader, instancePath, EXPECTED_START_OF_OBJECT_CODE, $"Expected start of object, but found {value}.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException RESOURCETYPE_SHOULD_BE_STRING(ref Utf8JsonReader reader, string instancePath, JsonTokenType value) => Initialize(ref reader, instancePath, RESOURCETYPE_SHOULD_BE_STRING_CODE, $"Property 'resourceType' should be a string, but found {value}.", OO_Sev.Fatal, OO_Typ.Value);
        internal static FhirJsonException NO_RESOURCETYPE_PROPERTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, NO_RESOURCETYPE_PROPERTY_CODE, "Resource has no 'resourceType' property.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException EXPECTED_PRIMITIVE_NOT_OBJECT(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_PRIMITIVE_NOT_OBJECT_CODE, "Expected a primitive value, not a json object.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException EXPECTED_PRIMITIVE_NOT_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_PRIMITIVE_NOT_ARRAY_CODE, "Expected a primitive value, not the start of an array.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException CHOICE_ELEMENT_HAS_NO_TYPE(ref Utf8JsonReader reader, string instancePath, string propName) => Initialize(ref reader, instancePath, CHOICE_ELEMENT_HAS_NO_TYPE_CODE, $"Choice element '{propName}' is not suffixed with a type.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException CHOICE_ELEMENT_HAS_UNKOWN_TYPE(ref Utf8JsonReader reader, string instancePath, string value, string typeValue) => Initialize(ref reader, instancePath, CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE, $"Choice element '{value}' is suffixed with an unrecognized type '{typeValue}'.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException UNKNOWN_RESOURCE_TYPE(ref Utf8JsonReader reader, string instancePath, string resourceType) => Initialize(ref reader, instancePath, UNKNOWN_RESOURCE_TYPE_CODE, $"Unknown type '{resourceType}' found in 'resourceType' property.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException RESOURCE_TYPE_NOT_A_RESOURCE(ref Utf8JsonReader reader, string instancePath, string name) => Initialize(ref reader, instancePath, RESOURCE_TYPE_NOT_A_RESOURCE_CODE, $"Data type '{name}' in property 'resourceType' is not a type of resource.", OO_Sev.Fatal, OO_Typ.Structure);
        internal static FhirJsonException UNKNOWN_PROPERTY_FOUND(ref Utf8JsonReader reader, string instancePath, string propName) => Initialize(ref reader, instancePath, UNKNOWN_PROPERTY_FOUND_CODE, $"Encountered unrecognized element '{propName}'.", OO_Sev.Error, OO_Typ.Structure); // this could be ignored, so isn't fatal?

        internal static FhirJsonException DUPLICATE_PROPERTY(ref Utf8JsonReader reader, string instancePath, string propName) => Initialize(ref reader, instancePath, DUPLICATE_PROPERTY_CODE, $"Encountered duplicate property '{propName}'.", OO_Sev.Fatal, OO_Typ.Structure);

        // ==========================================
        // Recoverable Errors
        // ==========================================

        // The serialization contained a json null where it is not allowed, but a null does not contain data anyway.
        internal static FhirJsonException EXPECTED_PRIMITIVE_NOT_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_PRIMITIVE_NOT_NULL_CODE, "Expected a primitive value, not a json null.", OO_Sev.Warning, OO_Typ.Value);

        // Encountered an empty property, which is illegal, but the empty text is maintained.
        internal static FhirJsonException PROPERTY_MAY_NOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, PROPERTY_MAY_NOT_BE_EMPTY_CODE, "Properties cannot be empty strings. Either they are absent, or they are present with at least one character of non-whitespace content.", OO_Sev.Warning, OO_Typ.Value);

        internal static FhirJsonException DUPLICATE_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, DUPLICATE_ARRAY_CODE, $"Duplicate array detected.", OO_Sev.Warning, OO_Typ.Value);

        // The parser will turn a non-array value into an array with a single element, so no data is lost.
        internal static FhirJsonException EXPECTED_START_OF_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_START_OF_ARRAY_CODE, "Expected start of array.", OO_Sev.Error, OO_Typ.Structure);

        // We will just ignore the underscore and keep on parsing
        internal static FhirJsonException USE_OF_UNDERSCORE_ILLEGAL(ref Utf8JsonReader reader, string instancePath, string propertyNameMapped, string propertyName) => Initialize(ref reader, instancePath, USE_OF_UNDERSCORE_ILLEGAL_CODE, $"Element '{propertyNameMapped}' is not a FHIR primitive, so it should not use an underscore in the '{propertyName}' property.", OO_Sev.Warning, OO_Typ.Structure);

        // Empty objects and arrays can be ignored without discarding data
        internal static FhirJsonException OBJECTS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, OBJECTS_CANNOT_BE_EMPTY_CODE, "An object needs to have at least one property.", OO_Sev.Warning, OO_Typ.Structure);
        internal static FhirJsonException ARRAYS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, ARRAYS_CANNOT_BE_EMPTY_CODE, "An array needs to have at least one element.", OO_Sev.Warning, OO_Typ.Structure);

        // This leaves the incorrect nulls in place, no change in data.
        internal static FhirJsonException PRIMITIVE_ARRAYS_ONLY_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, PRIMITIVE_ARRAYS_ONLY_NULL_CODE, "Arrays need to have at least one non-null element.", OO_Sev.Warning, OO_Typ.Structure);

        /// <summary>
        /// List of issues which do NOT lead to data loss. Recoverable issues mean that all data present in the parsed data could be retrieved and
        /// captured in the POCO model, even if the syntax or the data was not fully FHIR compliant.
        /// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
        internal static string[] RecoverableIssues =
        [
            EXPECTED_PRIMITIVE_NOT_NULL_CODE,
            EXPECTED_START_OF_ARRAY_CODE,
            USE_OF_UNDERSCORE_ILLEGAL_CODE,
            OBJECTS_CANNOT_BE_EMPTY_CODE,
            ARRAYS_CANNOT_BE_EMPTY_CODE,
            PRIMITIVE_ARRAYS_ONLY_NULL_CODE,
            PROPERTY_MAY_NOT_BE_EMPTY_CODE,
            DUPLICATE_ARRAY_CODE,
        ];
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer 
        /// FHIR release. This means allowing unknown elements, codes and types in a choice element. Note that the POCO model cannot capture
        /// these newer elements and data, so this means data loss may occur.
        /// </summary>
        internal static string[] BackwardsCompatibilityAllowedIssues =
        [
            CodedValidationException.INVALID_CODED_VALUE_CODE,
            CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE,
            UNKNOWN_PROPERTY_FOUND_CODE
        ];

        public FhirJsonException(string code, string message)
            : base(code, message, null, null, null, OO_Sev.Error, OO_Typ.Unknown)
        {
            // Nothing
        }

        public FhirJsonException(string code, string message, Exception? innerException)
            : base(code, message, null, null, null, OO_Sev.Error, OO_Typ.Unknown, innerException)
        {
            // Nothing
        }

        public FhirJsonException(
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


        internal static FhirJsonException Initialize(ref Utf8JsonReader reader, string instancePath, string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, Exception? innerException = null)
        {
            var (lineNumber, position) = reader.GetLocation();

            return new FhirJsonException(
                code,
                message,
                instancePath,
                lineNumber,
                position,
                issueSeverity,
                issueType,
                innerException);
        }

        public FhirJsonException? CloneWith(string baseMessage, OO_Sev issueSeverity, OO_Typ issueType) =>
            new(ErrorCode, baseMessage, InstancePath, LineNumber, Position,
                    issueSeverity, issueType);
    }
}

#nullable restore