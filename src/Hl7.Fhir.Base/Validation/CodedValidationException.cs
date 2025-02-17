/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.ComponentModel.DataAnnotations;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using OO_Sev = Hl7.Fhir.Model.OperationOutcome.IssueSeverity;
using OO_Typ = Hl7.Fhir.Model.OperationOutcome.IssueType;

#nullable enable

namespace Hl7.Fhir.Validation;

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
    public const string NARRATIVE_XML_IS_MALFORMED_CODE = "PVAL114";
    public const string NARRATIVE_XML_IS_INVALID_CODE = "PVAL115";
    public const string INVALID_CODED_VALUE_CODE = "PVAL116";
    public const string CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE = "PVAL118";
    public const string INVALID_STRING_LENGTH_CODE = "PVAL119";
    public const string INVALID_BASE64_VALUE_CODE = "PVAL120";
    public const string INCORRECT_LITERAL_VALUE_TYPE_CODE = "PVAL123";
    public const string LITERAL_INVALID_CODE = "PVAL124";
    public const string POSITIVE_INT_MUST_BE_POSITIVE_CODE = "PVAL125";
    public const string UNSIGNED_INT_MUST_NOT_BE_NEGATIVE_CODE = "PVAL126";

    internal static COVE CHOICE_TYPE_NOT_ALLOWED(ValidationContext context, string typeName) => Initialize(context, CHOICE_TYPE_NOT_ALLOWED_CODE, $"Value is of type '{typeName}', which is not an allowed choice.", OO_Sev.Error, OO_Typ.Structure);
    internal static COVE INCORRECT_CARDINALITY_MIN(ValidationContext context, int count, int Min) => Initialize(context, INCORRECT_CARDINALITY_MIN_CODE, $"Element has {count} elements, but minimum cardinality is {Min}.", OO_Sev.Error, OO_Typ.Required);
    internal static COVE INCORRECT_CARDINALITY_MAX(ValidationContext context, int count, int Max) => Initialize(context, INCORRECT_CARDINALITY_MAX_CODE, $"Element has {count} elements, but maximum cardinality is {Max}.", OO_Sev.Error, OO_Typ.BusinessRule);
    internal static COVE REPEATING_ELEMENT_CANNOT_CONTAIN_NULL(ValidationContext context) => Initialize(context, REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, "Repeating elements should not contain a null value.", OO_Sev.Error, OO_Typ.Structure);
    internal static COVE MANDATORY_ELEMENT_CANNOT_BE_NULL(ValidationContext context, string? memberName, int Min) => Initialize(context, MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE, $"Element '{memberName}' with minimum cardinality {Min} cannot be null.", OO_Sev.Error, OO_Typ.Required);
    internal static COVE NARRATIVE_XML_IS_MALFORMED(ValidationContext context, string? value) => Initialize(context, NARRATIVE_XML_IS_MALFORMED_CODE, $"Value is not well-formatted Xml: {value}", OO_Sev.Error, OO_Typ.Structure);
    internal static COVE NARRATIVE_XML_IS_INVALID(ValidationContext context, string value) => Initialize(context, NARRATIVE_XML_IS_INVALID_CODE, $"Value is not well-formed Xml adhering to the FHIR schema for Narrative: {value}", OO_Sev.Error, OO_Typ.Structure);
    internal static COVE INVALID_CODED_VALUE(ValidationContext? context, object? value, string name) => Initialize(context, INVALID_CODED_VALUE_CODE, $"Value '{value}' is not a correct code for valueset '{name}'.", OO_Sev.Error, OO_Typ.CodeInvalid);
    internal static COVE CONTAINED_RESOURCES_CANNOT_BE_NESTED(ValidationContext context) => Initialize(context, CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE, "It is not allowed for a resource to contain resources which themselves contain resources.", OO_Sev.Error, OO_Typ.Structure);
    internal static COVE INVALID_STRING_LENGTH(ValidationContext context, string name, string value) => Initialize(context, INVALID_STRING_LENGTH_CODE, (value.Length > 0 ? $"String {name} exceeds maximum length of 1MB." : $"String {name} is empty"), OO_Sev.Error, OO_Typ.Value);
    internal static COVE INVALID_BASE64_VALUE(ValidationContext? context, object? value) => Initialize(context, INVALID_BASE64_VALUE_CODE, $"Value '{value}' is not parseable as Base64 data.", OO_Sev.Error, OO_Typ.Value);
    internal static COVE INCORRECT_LITERAL_VALUE_TYPE(ValidationContext? context, object? value, string fhirTypeName) =>
        Initialize(context, INCORRECT_LITERAL_VALUE_TYPE_CODE, $"{niceValue(value)} is not the right type of literal for a {fhirTypeName}.", OO_Sev.Error, OO_Typ.Value);
    internal static COVE LITERAL_INVALID(ValidationContext? context, object? value, string fhirTypeName) =>
        Initialize(context, LITERAL_INVALID_CODE, $"{niceValue(value)} is not a correct literal for a {fhirTypeName}.", OO_Sev.Error, OO_Typ.Value);
    internal static COVE POSITIVE_INT_MUST_BE_POSITIVE(ValidationContext? context, int value) =>
        Initialize(context, POSITIVE_INT_MUST_BE_POSITIVE_CODE, $"Value {value} is not positive, which is required for a PositiveInt.", OO_Sev.Error, OO_Typ.Value);
    internal static COVE UNSIGNED_INT_MUST_NOT_BE_NEGATIVE(ValidationContext? context, int value) =>
        Initialize(context, UNSIGNED_INT_MUST_NOT_BE_NEGATIVE_CODE, $"Value {value} is negative, which is not allowed for an UnsignedInt.", OO_Sev.Error, OO_Typ.Value);


    private static string niceValue(object? v)
    {
        return v switch
        {
            null => "null",
            string s => $"string '{s}'",
            int i => $"integer {i}",
            decimal d => $"decimal {d}",
            bool b => $"boolean {b}",
            _ => $"value '{PrimitiveTypeConverter.ConvertTo<string>(v)}' of type '{v.GetType()}'"
        };
    }

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

    internal static CodedValidationException Initialize(ValidationContext? context, string code, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType)
    {
        var path = context?.GetLocation();

        if (path is not null)
        {
            // Bit of a hack. The location returned by GetLocation() will be different depending on
            // whether this validation is run within the deserializer or the DataAnnotations.Validator.
            // In the latter case, the MemberName will be set, and GetLocation()
            // will return the parent, so we need to add the MemberName.
            if (context?.MemberName is not null)
            {
                path = $"{path}.{context.MemberName}";
            }
        }

        var pi = context?.GetPositionInfo();

        var codedException = new CodedValidationException(
            code,
            message,
            path,
            pi?.LineNumber,
            pi?.LinePosition, issueSeverity, issueType);

        return codedException;
    }

    internal CodedValidationResult AsResult(ValidationContext? context) =>
        context?.MemberName is { } mn
            ? new CodedValidationResult(this, memberNames: [mn])
            : new CodedValidationResult(this);
}