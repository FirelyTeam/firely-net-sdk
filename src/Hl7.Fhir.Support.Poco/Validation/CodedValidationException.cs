/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// An error found during validation of POCO's using the <see cref="ValidationAttribute"/> validators.
    /// </summary>
    public class CodedValidationException : CodedException
    {
        public const string CHOICE_TYPE_NOT_ALLOWED_CODE = "PVAL101";
        public const string INCORRECT_CARDINALITY_MIN_CODE = "PVAL102";
        public const string INCORRECT_CARDINALITY_MAX_CODE = "PVAL103";
        public const string REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE = "PVAL104";
        public const string MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE = "PVAL105";
        public const string CODE_LITERAL_INVALID_CODE = "PVAL106";
        public const string DATE_LITERAL_INVALID_CODE = "PVAL107";
        public const string DATETIME_LITERAL_INVALID_CODE = "PVAL108";
        public const string ID_LITERAL_INVALID_CODE = "PVAL109";
        public const string OID_LITERAL_INVALID_CODE = "PVAL110";
        public const string TIME_LITERAL_INVALID_CODE = "PVAL111";
        public const string URI_LITERAL_INVALID_CODE = "PVAL112";
        public const string UUID_LITERAL_INVALID_CODE = "PVAL113";
        public const string NARRATIVE_XML_IS_MALFORMED_CODE = "PVAL114";
        public const string NARRATIVE_XML_IS_INVALID_CODE = "PVAL115";
        public const string INVALID_CODED_VALUE_CODE = "PVAL116";
        public const string CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE = "PVAL117";
        public const string CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE = "PVAL118";

        internal static readonly COVE CHOICE_TYPE_NOT_ALLOWED = new(CHOICE_TYPE_NOT_ALLOWED_CODE, "Value is of type '{0}', which is not an allowed choice.");
        internal static readonly COVE INCORRECT_CARDINALITY_MIN = new(INCORRECT_CARDINALITY_MIN_CODE, "Element has {0} elements, but minium cardinality is {1}.");
        internal static readonly COVE INCORRECT_CARDINALITY_MAX = new(INCORRECT_CARDINALITY_MAX_CODE, "Element has {0} elements, but maximum cardinality is {1}.");
        internal static readonly COVE REPEATING_ELEMENT_CANNOT_CONTAIN_NULL = new(REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, "Repeating elements should not contain a null value.");
        internal static readonly COVE MANDATORY_ELEMENT_CANNOT_BE_NULL = new(MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE, "Element with minimum cardinality {0} cannot be null.");
        internal static readonly COVE CODE_LITERAL_INVALID = new(CODE_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for a code.");
        internal static readonly COVE DATE_LITERAL_INVALID = new(DATE_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for a date.");
        internal static readonly COVE DATETIME_LITERAL_INVALID = new(DATETIME_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for a dateTime.");
        internal static readonly COVE ID_LITERAL_INVALID = new(ID_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for an id.");
        internal static readonly COVE NARRATIVE_XML_IS_MALFORMED = new(NARRATIVE_XML_IS_MALFORMED_CODE, "Value is not well-formatted Xml: {0}");
        internal static readonly COVE NARRATIVE_XML_IS_INVALID = new(NARRATIVE_XML_IS_INVALID_CODE, "Value is not well-formed Xml adhering to the FHIR schema for Narrative: {0}");
        internal static readonly COVE OID_LITERAL_INVALID = new(OID_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for an oid.");
        internal static readonly COVE TIME_LITERAL_INVALID = new(TIME_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for a time.");
        internal static readonly COVE URI_LITERAL_INVALID = new(URI_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for an uri.");
        internal static readonly COVE UUID_LITERAL_INVALID = new(UUID_LITERAL_INVALID_CODE, "'{0}' is not a correct literal for a uuid.");
        internal static readonly COVE INVALID_CODED_VALUE = new(INVALID_CODED_VALUE_CODE, "Value '{0}' is not a correct code for valueset '{1}'.");
        internal static readonly COVE CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE = new(CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE, "Resource has contained resources with narrative, which is not allowed.");
        internal static readonly COVE CONTAINED_RESOURCES_CANNOT_BE_NESTED = new(CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE, "It is not allowed for a resource to contain resources which themselves contain resources.");

        public CodedValidationException(string code, string message) : base(code, message)
        {
            // nothing
        }

        internal CodedValidationResult AsResult(ValidationContext context, params object?[] parameters)
        {
            string? location = null;

            if (context.GetPositionInfo() is IPositionInfo pi)
                location = $"line {pi.LineNumber}, position {pi.LinePosition}";

            if (context.GetLocation() is string loc)
            {
                // Bit of a hack. The location returned by GetLocation() will be different depending on
                // whether this validation is run within the deserializer or the DataAnnotations.Validator.
                // In the latter case, the MemberName will be set, and the location will be the GetLocation()
                // will return the parent, so we need to add the MemberName.
                if (context.MemberName is not null) loc = $"{loc}.{context.MemberName}";
                location = location is null ? loc : $"{loc}, {location}";
            }

            var formattedMessage = string.Format(CultureInfo.InvariantCulture, Message, parameters);

            var messageWithLocation = $"{formattedMessage} At {location}.";

            var codedException = new CodedValidationException(ErrorCode, messageWithLocation);

            return context.MemberName is string mn
                ? new(codedException, memberNames: new[] { mn })
                : new(codedException);
        }
    }
}

#nullable restore
