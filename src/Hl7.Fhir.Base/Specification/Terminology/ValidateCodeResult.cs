/* 
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static Hl7.Fhir.Model.OperationOutcome;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the ValueSet/$validate-code and CodeSystem/$validate-code operations.
/// </summary>
public class ValidateCodeResult : Parameters
{
    public const string RESULT_ATTRIBUTE = "result";
    public const string MESSAGE_ATTRIBUTE = "message";
    public const string DISPLAY_ATTRIBUTE = "display";
    public const string CODE_ATTRIBUTE = "code";
    public const string SYSTEM_ATTRIBUTE = "system";
    public const string VERSION_ATTRIBUTE = "version";
    public const string ISSUES_ATTRIBUTE = "issues";
    public const string CODEABLECONCEPT_ATTRIBUTE = "codeableConcept";
    public const string NORMALIZED_CODE_ATTRIBUTE = "normalized-code";
    public const string CAUSED_BY_UNKNOWN_SYSTEM_ATTRIBUTE = "x-caused-by-unknown-system";
    public const string UNKNOWN_SYSTEM_ATTRIBUTE = "x-unknown-system";
    public const string INACTIVE_ATTRIBUTE = "inactive";
    public const string STATUS_ATTRIBUTE = "status";

    public ValidateCodeResult()
    {
        // Nothing
    }

    public ValidateCodeResult(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// True if the concept details supplied are valid.
    /// </summary>
    public FhirBoolean? Result => this.GetSingleValue<FhirBoolean>(RESULT_ATTRIBUTE);

    /// <summary>
    /// Error details, if result = false. If this is provided when result = true, the message carries hints and warnings.
    /// </summary>
    public FhirString? Message => this.GetSingleValue<FhirString>(MESSAGE_ATTRIBUTE);

    /// <summary>
    /// A valid display for the concept if the system wishes to display this to a user.
    /// </summary>
    public FhirString? Display => this.GetSingleValue<FhirString>(DISPLAY_ATTRIBUTE);

    /// <summary>
    /// List of itemized issues of various severity (e.g. validation warnings).
    /// </summary>
    public Resource? Issues => this.GetSingleResource(ISSUES_ATTRIBUTE);

    /// <summary>
    /// Code for which validation was performed.
    /// </summary>
    public Code? Code => this.GetSingleValue<Code>(CODE_ATTRIBUTE);

    /// <summary>
    /// System in which validated code belongs.
    /// </summary>
    public FhirUri? System => this.GetSingleValue<FhirUri>(SYSTEM_ATTRIBUTE);

    /// <summary>
    /// Version of the system.
    /// </summary>
    public FhirString? Version => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);

    /// <summary>
    /// The CodeableConcept that was validated, echoed back when the input was a CodeableConcept.
    /// Setting this to <c>null</c> removes the parameter.
    /// </summary>
    public CodeableConcept? CodeableConcept
    {
        get => this.GetSingleValue<CodeableConcept>(CODEABLECONCEPT_ATTRIBUTE);
        set => this.SetSingleValue(CODEABLECONCEPT_ATTRIBUTE, value);
    }

    /// <summary>
    /// The normalized form of the code, for code systems where codes can have more than one form
    /// (e.g. case-insensitive code systems). Setting this to <c>null</c> removes the parameter.
    /// </summary>
    public Code? NormalizedCode
    {
        get => this.GetSingleValue<Code>(NORMALIZED_CODE_ATTRIBUTE);
        set => this.SetSingleValue(NORMALIZED_CODE_ATTRIBUTE, value);
    }

    /// <summary>
    /// The canonical urls of code systems that could not be found while processing the value set,
    /// which caused the validation to be incomplete (the <c>x-caused-by-unknown-system</c> parameter).
    /// Returns <c>null</c> when the parameter is absent. Setting this to <c>null</c> or an empty list removes the parameter.
    /// </summary>
    /// <remarks><c>canonical</c> is not a datatype in FHIR STU3, so this parameter should not be used with STU3.</remarks>
    public IReadOnlyList<Canonical>? CausedByUnknownSystem
    {
        get
        {
            var values = this.GetMultipleValues<Canonical>(CAUSED_BY_UNKNOWN_SYSTEM_ATTRIBUTE).ToList();
            return values.Count > 0 ? values : null;
        }
        set => this.SetMultipleValues(CAUSED_BY_UNKNOWN_SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// The canonical url of the code system of the code being validated, when that code system could not be found
    /// (the <c>x-unknown-system</c> parameter). Setting this to <c>null</c> removes the parameter.
    /// </summary>
    /// <remarks><c>canonical</c> is not a datatype in FHIR STU3, so this parameter should not be used with STU3.</remarks>
    public Canonical? UnknownSystem
    {
        get => this.GetSingleValue<Canonical>(UNKNOWN_SYSTEM_ATTRIBUTE);
        set => this.SetSingleValue(UNKNOWN_SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// True if the concept is inactive in its code system. Setting this to <c>null</c> removes the parameter.
    /// </summary>
    public FhirBoolean? Inactive
    {
        get => this.GetSingleValue<FhirBoolean>(INACTIVE_ATTRIBUTE);
        set => this.SetSingleValue(INACTIVE_ATTRIBUTE, value);
    }

    /// <summary>
    /// The status of the concept in its code system, when it is not simply active (e.g. inactive, retired or deprecated).
    /// Setting this to <c>null</c> removes the parameter.
    /// </summary>
    public Code? Status
    {
        get => this.GetSingleValue<Code>(STATUS_ATTRIBUTE);
        set => this.SetSingleValue(STATUS_ATTRIBUTE, value);
    }

    /// <summary>
    /// Adds an issue to the <c>issues</c> OperationOutcome of this result, creating the OperationOutcome on first use.
    /// </summary>
    /// <remarks>Use the named methods in <see cref="TerminologyServiceOperationExceptionExtensions"/>
    /// (e.g. <c>AddCodeNotInValueSet</c>) to add an issue for a specific cause.</remarks>
    internal void AddIssue(IssueComponent issue)
    {
        if (Issues is not OperationOutcome outcome)
        {
            outcome = new OperationOutcome();
            this.SetSingleResource(ISSUES_ATTRIBUTE, outcome);
        }

        outcome.Issue.Add(issue);
    }

    /// <summary>
    /// Creates a ValidateCodeResult with the given result and optional message.
    /// </summary>
    public static ValidateCodeResult ForResult(bool result, string? message = null, string? display = null, string? code = null, string? system = null, string? version = null, OperationOutcome? issues = null)
    {
        var resultParams = new ValidateCodeResult();
        resultParams.Add(RESULT_ATTRIBUTE, new FhirBoolean(result));

        if (!string.IsNullOrWhiteSpace(message))
            resultParams.Add(MESSAGE_ATTRIBUTE, new FhirString(message));

        if (!string.IsNullOrWhiteSpace(display))
            resultParams.Add(DISPLAY_ATTRIBUTE, new FhirString(display));

        if (!string.IsNullOrWhiteSpace(code))
            resultParams.Add(CODE_ATTRIBUTE, new Code(code));

        if (!string.IsNullOrWhiteSpace(system))
            resultParams.Add(SYSTEM_ATTRIBUTE, new FhirUri(system));

        if (!string.IsNullOrWhiteSpace(version))
            resultParams.Add(VERSION_ATTRIBUTE, new FhirString(version));

        if (issues is not null)
            resultParams.SetSingleResource(ISSUES_ATTRIBUTE, issues);

        return resultParams;
    }

    /// <summary>
    /// Converts this ValidateCodeResult to a Parameters instance.
    /// </summary>
    [Obsolete("ValidateCodeResult already inherits from Parameters, use the instance directly", false)]
    public Parameters ToParameters() => this;
}

