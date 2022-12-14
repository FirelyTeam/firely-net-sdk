/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// An error found during deserialization of Json data. These errors do not present issues
    /// with the Json itself, but issues in the data with regards to the rules for FHIR Json format described
    /// in http://hl7.org/fhir/json.html.
    /// </summary>
    public class FhirJsonException : CodedException
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
        public const string RESOURCETYPE_UNEXPECTED_CODE = "JSON119";
        public const string OBJECTS_CANNOT_BE_EMPTY_CODE = "JSON120";
        public const string ARRAYS_CANNOT_BE_EMPTY_CODE = "JSON121";

        [Obsolete("According to the latest updates of the Json format, primitive arrays of different sizes are no longer considered an error.")]
        public const string PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE = "JSON122";

        public const string PRIMITIVE_ARRAYS_ONLY_NULL_CODE = "JSON125";
        public const string INCOMPATIBLE_SIMPLE_VALUE_CODE = "JSON126";

        // ==========================================
        // Unrecoverable Errors
        // ==========================================
        internal static readonly FhirJsonException EXPECTED_START_OF_OBJECT = new(EXPECTED_START_OF_OBJECT_CODE, "Expected start of object, but found {0}.");
        internal static readonly FhirJsonException RESOURCETYPE_SHOULD_BE_STRING = new(RESOURCETYPE_SHOULD_BE_STRING_CODE, "Property 'resourceType' should be a string, but found {0}.");
        internal static readonly FhirJsonException NO_RESOURCETYPE_PROPERTY = new(NO_RESOURCETYPE_PROPERTY_CODE, "Resource has no 'resourceType' property.");
        internal static readonly FhirJsonException EXPECTED_PRIMITIVE_NOT_OBJECT = new(EXPECTED_PRIMITIVE_NOT_OBJECT_CODE, "Expected a primitive value, not a json object.");
        internal static readonly FhirJsonException EXPECTED_PRIMITIVE_NOT_ARRAY = new(EXPECTED_PRIMITIVE_NOT_ARRAY_CODE, "Expected a primitive value, not the start of an array.");
        internal static readonly FhirJsonException CHOICE_ELEMENT_HAS_NO_TYPE = new(CHOICE_ELEMENT_HAS_NO_TYPE_CODE, "Choice element '{0}' is not suffixed with a type.");
        internal static readonly FhirJsonException CHOICE_ELEMENT_HAS_UNKOWN_TYPE = new(CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE, "Choice element '{0}' is suffixed with an unrecognized type '{1}'.");
        internal static readonly FhirJsonException UNKNOWN_RESOURCE_TYPE = new(UNKNOWN_RESOURCE_TYPE_CODE, "Unknown type '{0}' found in 'resourceType' property.");
        internal static readonly FhirJsonException RESOURCE_TYPE_NOT_A_RESOURCE = new(RESOURCE_TYPE_NOT_A_RESOURCE_CODE, "Data type '{0}' in property 'resourceType' is not a type of resource.");
        internal static readonly FhirJsonException UNKNOWN_PROPERTY_FOUND = new(UNKNOWN_PROPERTY_FOUND_CODE, "Encountered unrecognized property '{0}'.");
        internal static readonly FhirJsonException INCOMPATIBLE_SIMPLE_VALUE = new(INCOMPATIBLE_SIMPLE_VALUE_CODE, "Json primitive value does not match the expected type of the primitive property. Details: ({0})");

        // ==========================================
        // Recoverable Errors
        // ==========================================

        // The serialization contained a json null where it is not allowed, but a null does not contain data anyway.
        internal static readonly FhirJsonException EXPECTED_PRIMITIVE_NOT_NULL = new(EXPECTED_PRIMITIVE_NOT_NULL_CODE, "Expected a primitive value, not a json null.");

        // These errors signal parsing errors, but the original raw data is retained in the POCO so no data is lost.
        internal static readonly FhirJsonException INCORRECT_BASE64_DATA = new(INCORRECT_BASE64_DATA_CODE, "Encountered incorrectly encoded base64 data.");
        internal static readonly FhirJsonException STRING_ISNOTAN_INSTANT = new(STRING_ISNOTAN_INSTANT_CODE, "Literal string '{0}' cannot be parsed as an instant.");
        internal static readonly FhirJsonException NUMBER_CANNOT_BE_PARSED = new(NUMBER_CANNOT_BE_PARSED_CODE, "Json number '{0}' cannot be parsed as a {1}.");
        internal static readonly FhirJsonException UNEXPECTED_JSON_TOKEN = new(UNEXPECTED_JSON_TOKEN_CODE, "Expecting a {0}, but found a json {1} with value '{2}'.");

        // The parser will turn a non-array value into an array with a single element, so no data is lost.
        internal static readonly FhirJsonException EXPECTED_START_OF_ARRAY = new(EXPECTED_START_OF_ARRAY_CODE, "Expected start of array.");

        // We will just ignore the underscore and keep on parsing
        internal static readonly FhirJsonException USE_OF_UNDERSCORE_ILLEGAL = new(USE_OF_UNDERSCORE_ILLEGAL_CODE, "Element '{0}' is not a FHIR primitive, so it should not use an underscore in the '{1}' property.");

        // The serialization contained a superfluous 'resourceType' property, but we have read all data anyway.
        internal static readonly FhirJsonException RESOURCETYPE_UNEXPECTED = new(RESOURCETYPE_UNEXPECTED_CODE, "The 'resourceType' property should only be used in resources.");

        // Empty objects and arrays can be ignored without discarding data
        internal static readonly FhirJsonException OBJECTS_CANNOT_BE_EMPTY = new(OBJECTS_CANNOT_BE_EMPTY_CODE, "An object needs to have at least one property.");
        internal static readonly FhirJsonException ARRAYS_CANNOT_BE_EMPTY = new(ARRAYS_CANNOT_BE_EMPTY_CODE, "An array needs to have at least one element.");

        // Shortest array will be filled out with nulls
        // [EK 20221027] The new R5 spec clarifies that this is actually correct behaviour, so this error is not used anymore.
        //internal static readonly FhirJsonException PRIMITIVE_ARRAYS_INCOMPAT_SIZE = new(PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE, "Primitive arrays split in two properties should have the same size.");

        // This leaves the incorrect nulls in place, no change in data.
        internal static readonly FhirJsonException PRIMITIVE_ARRAYS_ONLY_NULL = new(PRIMITIVE_ARRAYS_ONLY_NULL_CODE, "Arrays need to have at least one non-null element.");

        public FhirJsonException(string code, string message) : base(code, message)
        {
        }

        public FhirJsonException(string code, string message, Exception? innerException) : base(code, message, innerException)
        {
        }

        internal FhirJsonException With(ref Utf8JsonReader reader, params object?[] parameters) =>
            With(ref reader, inner: null, parameters);

        /// <summary>
        /// Creates a new instance of a <see cref="FhirJsonException"/> based on this one. This exception
        /// serves as the prototype for which the location and message can be altered for the copy.
        /// </summary>
        internal FhirJsonException With(ref Utf8JsonReader reader, FhirJsonException? inner, params object?[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, Message, parameters);
            var location = reader.GenerateLocationMessage();
            var message = $"{formattedMessage} {location}";

            return new FhirJsonException(ErrorCode, message, inner);
        }
    }
}

#nullable restore
#endif