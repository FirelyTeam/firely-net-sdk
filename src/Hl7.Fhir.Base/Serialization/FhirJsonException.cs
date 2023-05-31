/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
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
        // TODO: Document each of these errors, based on the text for the error.
        public const string EXPECTED_START_OF_OBJECT_CODE = "JSON101";
        public const string RESOURCETYPE_SHOULD_BE_STRING_CODE = "JSON102";
        public const string NO_RESOURCETYPE_PROPERTY_CODE = "JSON103";
        public const string EXPECTED_PRIMITIVE_NOT_OBJECT_CODE = "JSON104";
        public const string EXPECTED_PRIMITIVE_NOT_ARRAY_CODE = "JSON105";
        public const string INCORRECT_BASE64_DATA_CODE = "JSON106";
        public const string STRING_ISNOTAN_INSTANT_CODE = "JSON107";
        public const string NUMBER_CANNOT_BE_PARSED_CODE = "JSON108";
        public const string EXPECTED_PRIMITIVE_NOT_NULL_CODE = "JSON109";
        public const string UNEXPECTED_JSON_TOKEN_CODE = "JSON110";
        public const string EXPECTED_START_OF_ARRAY_CODE = "JSON111";
        public const string USE_OF_UNDERSCORE_ILLEGAL_CODE = "JSON113";
        public const string CHOICE_ELEMENT_HAS_NO_TYPE_CODE = "JSON114";
        public const string CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE = "JSON115";
        public const string UNKNOWN_RESOURCE_TYPE_CODE = "JSON116";
        public const string RESOURCE_TYPE_NOT_A_RESOURCE_CODE = "JSON117";
        public const string UNKNOWN_PROPERTY_FOUND_CODE = "JSON118";

        [Obsolete("This issue is no longer raised as it is now allowed to use `resourceType` as the name of an element.")]
        public const string RESOURCETYPE_UNEXPECTED_CODE = "JSON119";
        public const string OBJECTS_CANNOT_BE_EMPTY_CODE = "JSON120";
        public const string ARRAYS_CANNOT_BE_EMPTY_CODE = "JSON121";
        public const string LONG_CANNOT_BE_PARSED_CODE = "JSON122";
        public const string LONG_INCORRECT_FORMAT_CODE = "JSON123";

        [Obsolete("According to the latest updates of the Json format, primitive arrays of different sizes are no longer considered an error.")]
        public const string PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE = "JSON122";

        public const string PRIMITIVE_ARRAYS_ONLY_NULL_CODE = "JSON125";
        public const string INCOMPATIBLE_SIMPLE_VALUE_CODE = "JSON126";
        public const string PROPERTY_MAY_NOT_BE_EMPTY_CODE = "JSON127";

        public const string DUPLICATE_ARRAY_CODE = "JSON128";

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
        internal static FhirJsonException INCOMPATIBLE_SIMPLE_VALUE(ref Utf8JsonReader reader, string instancePath, string value, FhirJsonException? err) => Initialize(ref reader, instancePath, INCOMPATIBLE_SIMPLE_VALUE_CODE, $"Json primitive value does not match the expected type of the primitive property. Details: ({value})", OO_Sev.Fatal, OO_Typ.Value, err);

        // ==========================================
        // Recoverable Errors
        // ==========================================

        // The serialization contained a json null where it is not allowed, but a null does not contain data anyway.
        internal static FhirJsonException EXPECTED_PRIMITIVE_NOT_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_PRIMITIVE_NOT_NULL_CODE, "Expected a primitive value, not a json null.", OO_Sev.Error, OO_Typ.Value);

        // Encountered an empty property, which is illegal, but the empty text is maintained.
        internal static FhirJsonException PROPERTY_MAY_NOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, PROPERTY_MAY_NOT_BE_EMPTY_CODE, "Properties cannot be empty strings. Either they are absent, or they are present with at least one character of non-whitespace content.", OO_Sev.Error, OO_Typ.Value);

        // These errors signal parsing errors, but the original raw data is retained in the POCO so no data is lost.
        internal static FhirJsonException INCORRECT_BASE64_DATA(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.", OO_Sev.Error, OO_Typ.Value);
        internal static FhirJsonException STRING_ISNOTAN_INSTANT(ref Utf8JsonReader reader, string instancePath, string value) => Initialize(ref reader, instancePath, STRING_ISNOTAN_INSTANT_CODE, $"Literal string '{value}' cannot be parsed as an instant.", OO_Sev.Error, OO_Typ.Value);
        internal static FhirJsonException NUMBER_CANNOT_BE_PARSED(ref Utf8JsonReader reader, string instancePath, string? value, string typeName) => Initialize(ref reader, instancePath, NUMBER_CANNOT_BE_PARSED_CODE, $"Json number '{value}' cannot be parsed as a {typeName}.", OO_Sev.Error, OO_Typ.Value);
        internal static FhirJsonException UNEXPECTED_JSON_TOKEN(ref Utf8JsonReader reader, string instancePath, string expected, string actual, string? value) => Initialize(ref reader, instancePath, UNEXPECTED_JSON_TOKEN_CODE, $"Expecting a {expected}, but found a json {actual} with value '{value}'.", OO_Sev.Warning, OO_Typ.Value);
        internal static FhirJsonException DUPLICATE_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, DUPLICATE_ARRAY_CODE, $"Duplicate array detected.", OO_Sev.Warning, OO_Typ.Value);

        // In R5 Integer64 (long) are serialized as string. So we would expect a string during parsing.
        internal static FhirJsonException LONG_CANNOT_BE_PARSED(ref Utf8JsonReader reader, string instancePath, string? s0, string typeName) => Initialize(ref reader, instancePath, LONG_CANNOT_BE_PARSED_CODE, $"Json string '{s0}' cannot be parsed as a {typeName}.", OO_Sev.Warning, OO_Typ.Value);
        internal static FhirJsonException LONG_INCORRECT_FORMAT(ref Utf8JsonReader reader, string instancePath, string s0, string s1, string s2, string expectedType) => Initialize(ref reader, instancePath, LONG_INCORRECT_FORMAT_CODE, $"{s0} '{s1}' cannot be parsed as a {s2}, because it should be a {expectedType}.", OO_Sev.Warning, OO_Typ.Value);

        // The parser will turn a non-array value into an array with a single element, so no data is lost.
        internal static FhirJsonException EXPECTED_START_OF_ARRAY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, EXPECTED_START_OF_ARRAY_CODE, "Expected start of array.", OO_Sev.Warning, OO_Typ.Structure);

        // We will just ignore the underscore and keep on parsing
        internal static FhirJsonException USE_OF_UNDERSCORE_ILLEGAL(ref Utf8JsonReader reader, string instancePath, string propertyNameMapped, string propertyName) => Initialize(ref reader, instancePath, USE_OF_UNDERSCORE_ILLEGAL_CODE, $"Element '{propertyNameMapped}' is not a FHIR primitive, so it should not use an underscore in the '{propertyName}' property.", OO_Sev.Warning, OO_Typ.Structure);

        // The serialization contained a superfluous 'resourceType' property, but we have read all data anyway.
        // Note, this is no longer considered an error, since there are Resources using an element named "resourceType" (Subscription.filterBy for example).
        [Obsolete("This issue is no longer raised as it is now allowed to use `resourceType` as the name of an element.")]
        internal static FhirJsonException RESOURCETYPE_UNEXPECTED(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, RESOURCETYPE_UNEXPECTED_CODE, "The 'resourceType' property should only be used in resources.", OO_Sev.Warning, OO_Typ.Structure);

        // Empty objects and arrays can be ignored without discarding data
        internal static FhirJsonException OBJECTS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, OBJECTS_CANNOT_BE_EMPTY_CODE, "An object needs to have at least one property.", OO_Sev.Warning, OO_Typ.Structure);
        internal static FhirJsonException ARRAYS_CANNOT_BE_EMPTY(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, ARRAYS_CANNOT_BE_EMPTY_CODE, "An array needs to have at least one element.", OO_Sev.Warning, OO_Typ.Structure);

        // Shortest array will be filled out with nulls
        // [EK 20221027] The new R5 spec clarifies that this is actually correct behaviour, so this error is not used anymore.
        //internal static FhirJsonException PRIMITIVE_ARRAYS_INCOMPAT_SIZE => Initialize(PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE, "Primitive arrays split in two properties should have the same size.");

        // This leaves the incorrect nulls in place, no change in data.
        internal static FhirJsonException PRIMITIVE_ARRAYS_ONLY_NULL(ref Utf8JsonReader reader, string instancePath) => Initialize(ref reader, instancePath, PRIMITIVE_ARRAYS_ONLY_NULL_CODE, "Arrays need to have at least one non-null element.", OO_Sev.Warning, OO_Typ.Structure);

        /// <summary>
        /// Whether this issue leads to dataloss or not. Recoverable issues mean that all data present in the parsed data could be retrieved and
        /// captured in the POCO model, even if the syntax or the data was not fully FHIR compliant.
        /// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
        internal static bool IsRecoverableIssue(FhirJsonException e) =>
            e.ErrorCode is EXPECTED_PRIMITIVE_NOT_NULL_CODE or
            INCORRECT_BASE64_DATA_CODE or
            STRING_ISNOTAN_INSTANT_CODE or
            NUMBER_CANNOT_BE_PARSED_CODE or
            UNEXPECTED_JSON_TOKEN_CODE or
            LONG_CANNOT_BE_PARSED_CODE or
            LONG_INCORRECT_FORMAT_CODE or
            EXPECTED_START_OF_ARRAY_CODE or
            USE_OF_UNDERSCORE_ILLEGAL_CODE or
            RESOURCETYPE_UNEXPECTED_CODE or
            OBJECTS_CANNOT_BE_EMPTY_CODE or
            ARRAYS_CANNOT_BE_EMPTY_CODE or
            PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE or
            PRIMITIVE_ARRAYS_ONLY_NULL_CODE or
            PROPERTY_MAY_NOT_BE_EMPTY_CODE or
            DUPLICATE_ARRAY_CODE;
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer 
        /// FHIR release. This means allowing unknown elements, codes and types in a choice element. Note that the POCO model cannot capture
        /// these newer elements and data, so this means data loss may occur.
        /// </summary>
        internal static bool AllowedForBackwardsCompatibility(CodedException e) =>
            e.ErrorCode is CodedValidationException.INVALID_CODED_VALUE_CODE or
            //EXPECTED_PRIMITIVE_NOT_OBJECT_CODE or  
            //EXPECTED_PRIMITIVE_NOT_ARRAY_CODE or
            CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE or
            UNKNOWN_PROPERTY_FOUND_CODE;

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
#endif