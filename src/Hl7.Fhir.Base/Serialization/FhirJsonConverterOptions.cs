/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Specify the optional features for Json deserialization.
/// </summary>
public record FhirJsonConverterOptions
{
    /// <summary>
    /// Specifies the filter to use for summary serialization.
    /// </summary>
    public SerializationFilter? SummaryFilter { get; init; } = null;

    /// <summary>
    /// If set, this validator is invoked before the value is set in the object under construction to validate
    /// and possibly alter the value. Setting this property to <c>null</c> will disable validation completely.
    /// </summary>
    public IDeserializationValidator? Validator { get; init; } = DataAnnotationDeserialzationValidator.Default;

    /// <summary>
    /// Specifies a filter that can be used to filter out exceptions that are not considered fatal. The filter
    /// returns <c>true</c> for exceptions that should be ignored, and <c>false</c> otherwise.
    /// </summary>
    public Predicate<CodedException>? ExceptionFilter { get; init; } = null;

    /// <summary>
    /// Perform the parse time validation on the deserialized object even if parsing issues occurred.
    /// </summary>
    /// <remarks>
    /// This is useful for "strict mode" single-pass validators and may result in spurious error messages
    /// from validating incomplete content.
    /// </remarks>
    public bool ValidateOnFailedParse { get; init; } = false;

    /// <summary>
    /// During parsing any contained resources (such as those in a bundle) that encounter some form of parse/validation exception
    /// will have a <c>List&lt;CodedException&gt;</c> of these exceptions added as an annotation to the child resource.
    /// </summary>
    /// <remarks>
    /// This is primarily added to ease the processing of bundles during a batch submission.
    /// (without requiring processing fhirpath expressions in the issues in the parsing operation outcome to determine if a
    /// resource was clean and possibly ok to process).
    /// </remarks>
    public bool AnnotateResourceParseExceptions { get; init; } = false;

    /// <summary>
    /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
    /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
    /// default. Set this property to any other value than <see cref="None{T}"/>
    /// to perform validation.
    /// </summary>
    public NarrativeValidationKind NarrativeValidation { get; init; } = NarrativeValidationKind.None;

    /// <summary>
    /// Enables all validation rules that are available.
    /// </summary>
    /// <param name="mode">The selected mode to use, see <see cref="DeserializationMode"/>.</param>
    /// <param name="nvk">How strict to validate the XHtml in FHIR Narrative. Only relevant in mode <see cref="DeserializationMode.Strict"/></param>
    public FhirJsonConverterOptions WithMode(DeserializationMode mode,
        NarrativeValidationKind nvk = NarrativeValidationKind.FhirXhtml) =>
        mode switch
        {
            DeserializationMode.Strict => this with
            {
                ExceptionFilter = null, // No exceptions are ignored
                NarrativeValidation = nvk
            },
            DeserializationMode.BackwardsCompatible => this with
            {
                ExceptionFilter = CodedExceptionFilters.IsBackwardsCompatibilityIssue,
                NarrativeValidation = NarrativeValidationKind.None
            },
            DeserializationMode.Recoverable => this with
            {
                ExceptionFilter = CodedExceptionFilters.IsRecoverableIssue,
                NarrativeValidation = NarrativeValidationKind.None
            },
            DeserializationMode.Ostrich => this with
            {
                Validator = null,   // Disable all validations, we don't care.
                ExceptionFilter = _ => true,   // If there are still errors, ignore.
                NarrativeValidation = NarrativeValidationKind.None   // We don't care about the narrative.
            },
            _ => throw Error.NotSupported("Unknown deserialization mode.")
        };

    /// <summary>
    /// Alters the options to enforce specific parsing exceptions.
    /// </summary>
    public FhirJsonConverterOptions Enforcing(IEnumerable<string> toEnforce) =>
        this with { ExceptionFilter = this.ExceptionFilter.Enforce(toEnforce) };

    /// <summary>
    /// Alters the options to ignore specific parsing exceptions.
    /// </summary>
    public FhirJsonConverterOptions Ignoring(IEnumerable<string> toIgnore) =>
        this with { ExceptionFilter = this.ExceptionFilter.Ignore(toIgnore) };
}