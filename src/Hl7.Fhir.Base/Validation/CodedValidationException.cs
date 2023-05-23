/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.ComponentModel.DataAnnotations;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// An error found during validation of POCO's using the <see cref="ValidationAttribute"/> validators.
    /// </summary>
    public class CodedValidationException : ExtendedCodedException
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
        public const string CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE = "PVAL117"; // This was removed in R4 and is no longer validated
        public const string CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE = "PVAL118";

        internal static COVE CHOICE_TYPE_NOT_ALLOWED(ValidationContext context, string TypeName) => Initialize(context, CHOICE_TYPE_NOT_ALLOWED_CODE, $"Value is of type '{TypeName}', which is not an allowed choice.", OO_Sev.Error, OO_Typ.Structure);
        internal static COVE INCORRECT_CARDINALITY_MIN(ValidationContext context, int count, int Min) => Initialize(context, INCORRECT_CARDINALITY_MIN_CODE, $"Element has {count} elements, but minium cardinality is {Min}.", OO_Sev.Error, OO_Typ.Required);
        internal static COVE INCORRECT_CARDINALITY_MAX(ValidationContext context, int count, int Max) => Initialize(context, INCORRECT_CARDINALITY_MAX_CODE, $"Element has {count} elements, but maximum cardinality is {Max}.", OO_Sev.Error, OO_Typ.BusinessRule);
        internal static COVE REPEATING_ELEMENT_CANNOT_CONTAIN_NULL(ValidationContext context) => Initialize(context, REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, "Repeating elements should not contain a null value.", OO_Sev.Error, OO_Typ.Structure);
        internal static COVE MANDATORY_ELEMENT_CANNOT_BE_NULL(ValidationContext context, string? MemberName, int Min) => Initialize(context, MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE, $"Element '{MemberName}' with minimum cardinality {Min} cannot be null.", OO_Sev.Error, OO_Typ.Required);
        internal static COVE CODE_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, CODE_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for a code.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE DATE_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, DATE_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for a date.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE DATETIME_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, DATETIME_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for a dateTime.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE ID_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, ID_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for an id.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE NARRATIVE_XML_IS_MALFORMED(ValidationContext context, string? value) => Initialize(context, NARRATIVE_XML_IS_MALFORMED_CODE, $"Value is not well-formatted Xml: {value}", OO_Sev.Error, OO_Typ.Structure);
        internal static COVE NARRATIVE_XML_IS_INVALID(ValidationContext context, string value) => Initialize(context, NARRATIVE_XML_IS_INVALID_CODE, $"Value is not well-formed Xml adhering to the FHIR schema for Narrative: {value}", OO_Sev.Error, OO_Typ.Structure);
        internal static COVE OID_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, OID_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for an oid.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE TIME_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, TIME_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for a time.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE URI_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, URI_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for an uri.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE UUID_LITERAL_INVALID(ValidationContext context, string s) => Initialize(context, UUID_LITERAL_INVALID_CODE, $"'{s}' is not a correct literal for a uuid.", OO_Sev.Error, OO_Typ.Value);
        internal static COVE INVALID_CODED_VALUE(ValidationContext context, object? value, string name) => Initialize(context, INVALID_CODED_VALUE_CODE, $"Value '{value}' is not a correct code for valueset '{name}'.", OO_Sev.Error, OO_Typ.CodeInvalid);
        // internal static COVE CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE(ValidationContext context) => Initialize(context, CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE, "Resource has contained resources with narrative, which is not allowed.", OO_Sev.Error, OO_Typ.Structure);
        internal static COVE CONTAINED_RESOURCES_CANNOT_BE_NESTED(ValidationContext context) => Initialize(context, CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE, "It is not allowed for a resource to contain resources which themselves contain resources.", OO_Sev.Error, OO_Typ.Structure);

        public CodedValidationException(string code, string message)
            : base(code, message, null, null, null, OO_Sev.Error, OO_Typ.Unknown)
        {
            // Nothing
        }

        public CodedValidationException(
          string errorCode,
          string baseMessage,
          string? instancePath,
          long? lineNumber,
          long? position,
          OperationOutcome.IssueSeverity issueSeverity,
          OperationOutcome.IssueType issueType) :
              base(errorCode, baseMessage, instancePath, lineNumber, position, issueSeverity, issueType)
        {
            // Nothing
        }

        internal static CodedValidationException Initialize(ValidationContext context, string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType)
        {
            var path = context.GetLocation() as string;

            if (path is not null)
            {
                // Bit of a hack. The location returned by GetLocation() will be different depending on
                // whether this validation is run within the deserializer or the DataAnnotations.Validator.
                // In the latter case, the MemberName will be set, and GetLocation()
                // will return the parent, so we need to add the MemberName.
                if (context.MemberName is not null)
                {
                    path = $"{path}.{context.MemberName}";
                }
            }

            var pi = context.GetPositionInfo() as IPositionInfo;

            var codedException = new CodedValidationException(
                code,
                message,
                path,
                pi?.LineNumber,
                pi?.LinePosition, issueSeverity, issueType);

            return codedException;
        }

        internal CodedValidationResult AsResult(ValidationContext context)
        {
            return context.MemberName is string mn
                ? new(this, memberNames: new[] { mn })
                : new(this);
        }
    }
}

#nullable restore
