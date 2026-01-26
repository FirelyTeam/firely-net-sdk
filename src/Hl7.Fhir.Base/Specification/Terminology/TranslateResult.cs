/* 
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the ConceptMap/$translate operation.
/// </summary>
public class TranslateResult : Parameters
{
    public const string RESULT_ATTRIBUTE = "result";
    public const string MESSAGE_ATTRIBUTE = "message";
    public const string MATCH_ATTRIBUTE = "match";

    public TranslateResult()
    {
        // Nothing
    }

    public TranslateResult(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// True if the concept could be translated successfully.
    /// </summary>
    public FhirBoolean? Result => this.GetSingleValue<FhirBoolean>(RESULT_ATTRIBUTE);

    /// <summary>
    /// Error details, for server errors or when result = false. If this is provided when result = true, the message carries hints and warnings.
    /// </summary>
    public FhirString? Message => this.GetSingleValue<FhirString>(MESSAGE_ATTRIBUTE);

    /// <summary>
    /// A list of matches for the translation.
    /// </summary>
    public IEnumerable<ParameterComponent> Match => this.Get(MATCH_ATTRIBUTE);

    /// <summary>
    /// Creates a TranslateResult with the given result and optional message.
    /// </summary>
    public static TranslateResult ForResult(bool result, string? message = null)
    {
        var resultParams = new TranslateResult();
        resultParams.Add(RESULT_ATTRIBUTE, new FhirBoolean(result));

        if (!string.IsNullOrWhiteSpace(message))
            resultParams.Add(MESSAGE_ATTRIBUTE, new FhirString(message));

        return resultParams;
    }
}

