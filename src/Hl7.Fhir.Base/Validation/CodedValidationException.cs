/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    public const string MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE = "PVAL105";
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
    public const string PROPERTY_TYPE_MISMATCH_CODE = "PVAL127";
    public const string UNKNOWN_ELEMENT_CODE = "PVAL128";
    public const string ELEMENT_CANNOT_BE_EMPTY_CODE = "PVAL129";
    public const string UNKNOWN_RESOURCE_TYPE_CODE = "PVAL130";

    // A list of all issues that would throw an exception if the user used the
    // properties on the POCOs (and specifically the Value prop on datatypes).
    // Otherwise said, if none of these are raised, the user should be able to
    // use the POCOs without us throwing validation exceptions and should not be
    // required to check the <see cref="Base.Overflow"/> for unknown elements.
    internal static readonly HashSet<string> ISSUES_CAUSED_BY_OVERFLOW =
    [
        INVALID_CODED_VALUE_CODE,
        INVALID_STRING_LENGTH_CODE,
        INVALID_BASE64_VALUE_CODE,
        INCORRECT_LITERAL_VALUE_TYPE_CODE,
        LITERAL_INVALID_CODE,
        PROPERTY_TYPE_MISMATCH_CODE,
        UNKNOWN_ELEMENT_CODE,
    ];

    internal static COVE CHOICE_TYPE_NOT_ALLOWED(PocoValidationContext context, string typeName) => Initialize(context, CHOICE_TYPE_NOT_ALLOWED_CODE, $"Value is of type '{typeName}', which is not an allowed choice.", "Invalid datatype used", OO_Sev.Error, OO_Typ.Value);
    internal static COVE INCORRECT_CARDINALITY_MIN(PocoValidationContext context, int count, int Min) => Initialize(context, INCORRECT_CARDINALITY_MIN_CODE, $"Element has {count} elements, but minimum cardinality is {Min}.", "Missing mandatory field", OO_Sev.Error, OO_Typ.Required);
    internal static COVE INCORRECT_CARDINALITY_MAX(PocoValidationContext context, int count, int Max) => Initialize(context, INCORRECT_CARDINALITY_MAX_CODE, $"Element has {count} elements, but maximum cardinality is {Max}.", "Exceeded max values", OO_Sev.Error, OO_Typ.BusinessRule);
    internal static COVE REPEATING_ELEMENT_CANNOT_CONTAIN_NULL(PocoValidationContext context) => Initialize(context, REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, "Repeating elements should not contain a null value.", "Cannot be null", OO_Sev.Error, OO_Typ.BusinessRule);
    internal static COVE MANDATORY_ELEMENT_MUST_BE_PRESENT(PocoValidationContext context, string? memberName, int Min) => Initialize(context, MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, $"Element '{memberName}' with minimum cardinality {Min} must be present.", "Mandatory field cannot be null", OO_Sev.Error, OO_Typ.Required);
    internal static COVE NARRATIVE_XML_IS_MALFORMED(PocoValidationContext context, string? value) => Initialize(context, NARRATIVE_XML_IS_MALFORMED_CODE, $"Value is not well-formatted Xml: {value}", "Malformed narrative", OO_Sev.Error, OO_Typ.Value);
    internal static COVE NARRATIVE_XML_IS_INVALID(PocoValidationContext context, string value) => Initialize(context, NARRATIVE_XML_IS_INVALID_CODE, $"Value is not well-formed Xml adhering to the FHIR schema for Narrative: {value}", "Invalid narrative", OO_Sev.Error, OO_Typ.Value);
    internal static COVE INVALID_CODED_VALUE(PocoValidationContext? context, object? value, string name) => Initialize(context, INVALID_CODED_VALUE_CODE, $"Value '{value}' is not a correct code for valueset '{name}'.", "Invalid code", OO_Sev.Error, OO_Typ.CodeInvalid);
    internal static COVE CONTAINED_RESOURCES_CANNOT_BE_NESTED(PocoValidationContext context) => Initialize(context, CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE, "It is not allowed for a resource to contain resources which themselves contain resources.", "Contained resources cannot be nested", OO_Sev.Error, OO_Typ.Invariant);
    internal static COVE INVALID_STRING_LENGTH(PocoValidationContext context, string name, string value) => Initialize(context, INVALID_STRING_LENGTH_CODE, (value.Length > 0 ? $"String {name} exceeds maximum length of 1MB." : $"String {name} is empty"), "String too long", OO_Sev.Error, OO_Typ.Value);
    internal static COVE INVALID_BASE64_VALUE(PocoValidationContext? context, object? value) => Initialize(context, INVALID_BASE64_VALUE_CODE, $"Value '{value}' is not parseable as Base64 data.", "Invalid Base64 data", OO_Sev.Error, OO_Typ.Value);
    internal static COVE INCORRECT_LITERAL_VALUE_TYPE(PocoValidationContext? context, object? value, string fhirTypeName) =>
        Initialize(context, INCORRECT_LITERAL_VALUE_TYPE_CODE, $"{niceValue(value)} is not the right type of literal for a {fhirTypeName}.", "Invalid literal type", OO_Sev.Error, OO_Typ.Value);
    internal static COVE LITERAL_INVALID(PocoValidationContext? context, object? value, string fhirTypeName) =>
        Initialize(context, LITERAL_INVALID_CODE, $"{niceValue(value)} is not a correct literal for a {fhirTypeName}.", "Invalid literal", OO_Sev.Error, OO_Typ.Value);
    internal static COVE POSITIVE_INT_MUST_BE_POSITIVE(PocoValidationContext? context, int value) =>
        Initialize(context, POSITIVE_INT_MUST_BE_POSITIVE_CODE, $"Value {value} is not positive, which is required for a PositiveInt.", "Number must be positive", OO_Sev.Error, OO_Typ.Value);
    internal static COVE UNSIGNED_INT_MUST_NOT_BE_NEGATIVE(PocoValidationContext? context, int value) =>
        Initialize(context, UNSIGNED_INT_MUST_NOT_BE_NEGATIVE_CODE, $"Value {value} is negative, which is not allowed for an UnsignedInt.", "Cannot be negative", OO_Sev.Error, OO_Typ.Value);
    
    internal static COVE PROPERTY_TYPE_MISMATCH(PocoValidationContext? context, string expected, string actual) =>
        Initialize(context, PROPERTY_TYPE_MISMATCH_CODE, $"Expected property to be a {expected}, but found a {actual}.", "Type mismatch", OO_Sev.Error, OO_Typ.Value);
    
    internal static COVE UNKNOWN_ELEMENT(PocoValidationContext? context, string elementName, string serializedForm = "element") =>
        Initialize(context, UNKNOWN_ELEMENT_CODE, $"Found unknown {serializedForm} '{elementName}'.", "Unknown element", OO_Sev.Error, OO_Typ.Value);
    
    internal static COVE ELEMENT_CANNOT_BE_EMPTY(PocoValidationContext? context) =>
        Initialize(context, ELEMENT_CANNOT_BE_EMPTY_CODE, $"Empty FHIR elements are invalid.", "Element cannot be empty", OO_Sev.Error, OO_Typ.Value);

    internal static COVE UNKNOWN_RESOURCE_TYPE(PocoValidationContext? context, string resourceName) =>
        Initialize(context, UNKNOWN_RESOURCE_TYPE_CODE, $"Encountered unknown resource type '{resourceName}'.", "Unknown resource", OO_Sev.Error, OO_Typ.Value);

    private static string niceValue(object? v)
    {
        return v switch
        {
            null => "null",
            string s => $"string '{s}'",
            int i => $"integer {i}",
            decimal d => $"decimal {PrimitiveTypeConverter.ConvertTo<string>(d)}",
            bool b => $"boolean {b}",
            _ => $"value '{PrimitiveTypeConverter.ConvertTo<string>(v)}' of type '{v.GetType()}'"
        };
    }

    public CodedValidationException(string code, string message)
        : base(code, message, null, null, null, null, OO_Sev.Error, OO_Typ.Unknown)
    {
        // Nothing
    }

    public CodedValidationException(
        string errorCode,
        string baseMessage,
        string display,
        string? instancePath,
        long? lineNumber,
        long? position,
        OperationOutcome.IssueSeverity issueSeverity,
        OperationOutcome.IssueType issueType,
        string? memberName) :
        base(errorCode, baseMessage, display, instancePath, lineNumber, position, issueSeverity, issueType)
    {
        MemberName = memberName;
    }

    internal static COVE Initialize(PocoValidationContext? context, string code, string message, string display, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, string? memberName = null)
    {
        var path = context?.PathProducer.Invoke();

        var codedException = new COVE(
            code, message, display, path,
            context?.LineNumber, context?.LinePosition, 
            issueSeverity, issueType,
            memberName ?? context?.MemberName);

        return codedException;
    }
    
    /// <summary>
    /// Name of member property on which the error was encountered.
    /// </summary>
    public string? MemberName { get; init; }

    internal static COVE FromTypes(Type expected, object? actual, PocoValidationContext? context = null, [CallerMemberName] string memberName = "")
    {
        bool expectedList = typeof(IList).IsAssignableFrom(expected);
        bool actualList = actual is IList;

        string expectedFhirTypeName = expectedList
            ? "collection of " + (fhirTypeNameForRepeatingType(expected))
            : fhirTypeNameForSingleType(expected);

        string actualFhirTypeName = FhirTypeNameForObject(actual);

        // Make contrast between list and non-list a bit bigger
        if (expectedList && !actualList)
            actualFhirTypeName = "single " + actualFhirTypeName;
        if(actualList && !expectedList)
            expectedFhirTypeName = "single " + expectedFhirTypeName;

        return PROPERTY_TYPE_MISMATCH(context, expectedFhirTypeName, actualFhirTypeName);
    }

    internal static string FhirTypeNameForObject(object? actual) =>
        actual switch
        {
            IDynamicType { DynamicTypeName: null } => "unknown type",
            Base b => b.TypeName,
            IList list => "collection of " + fhirTypeNameForRepeatingType(list.GetType()),
            null => "null",
            _ => actual.GetType().Name
        };

    private static string fhirTypeNameForSingleType(Type t)
    {
        return typeof(IDynamicType).IsAssignableFrom(t)
            ? "unknown type"
            : t.GetCustomAttribute<FhirTypeAttribute>()?.Name ?? t.Name;
    }

    private static string fhirTypeNameForRepeatingType(Type t) =>
        fhirTypeNameForSingleType(ReflectionHelper.GetRepeatingElementType(t));
}