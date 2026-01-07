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

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the ValueSet/$validate-code and CodeSystem/$validate-code operations.
/// </summary>
public class ValidateCodeResult : Parameters
{
    public const string RESULT_ATTRIBUTE = "result";
    public const string MESSAGE_ATTRIBUTE = "message";
    public const string DISPLAY_ATTRIBUTE = "display";
    public const string ISSUES_ATTRIBUTE = "issues";

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
    /// Creates a ValidateCodeResult with the given result and optional message.
    /// </summary>
    public static ValidateCodeResult ForResult(bool result, string? message = null, string? display = null)
    {
        var resultParams = new ValidateCodeResult();
        resultParams.Add(RESULT_ATTRIBUTE, new FhirBoolean(result));

        if (!string.IsNullOrWhiteSpace(message))
            resultParams.Add(MESSAGE_ATTRIBUTE, new FhirString(message));

        if (!string.IsNullOrWhiteSpace(display))
            resultParams.Add(DISPLAY_ATTRIBUTE, new FhirString(display));

        return resultParams;
    }

    /// <summary>
    /// Converts this ValidateCodeResult to a Parameters instance.
    /// </summary>
    [Obsolete("ValidateCodeResult already inherits from Parameters, use the instance directly", false)]
    public Parameters ToParameters() => this;
}

