/*
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
/// Settings that control the behaviour of the <see cref="BaseFhirXmlDeserializer"/>, FhirXmlDeserializer,
/// <see cref="BaseFhirJsonDeserializer"/> and FhirJsonDeserializer.
/// </summary>
public record DeserializerSettings
{
    /// <summary>
    /// If set, this validator is invoked before the value is set in the object under construction to validate
    /// and possibly alter the value. Setting this property to <c>null</c> will disable validation completely.
    /// </summary>
    public IPocoValidator? Validator { get; init; } = FhirAttributeValidator.Default;

    /// <summary>
    /// Specifies a filter that can be used to filter out exceptions that are not considered fatal. The filter
    /// returns <c>true</c> for exceptions that should be ignored, and <c>false</c> otherwise.
    /// </summary>
    /// <remarks>Setting <see cref="AllowUnrecognizedEnums"/>,  <see cref="AcceptUnknownMembers"/> or
    /// <see cref="ParserSettings.PermissiveParsing" /> will modify this filter to reflect these settings.</remarks>
    public virtual Predicate<CodedException>? ExceptionFilter
    {
        get => augmentFilter(_exceptionFilter);
        init => _exceptionFilter = value;
    }

    // By default (i.e. when no mode or explicit filter is chosen), case-sensitivity issues are
    // ignored, so that the default behaviour of the deserializers is the same as in previous
    // versions of the SDK, which did not detect these issues at all.
    private readonly Predicate<CodedException>? _exceptionFilter
        = CodedExceptionFilters.FilterCaseSensitivityIssues;

    /// <summary>
    /// Determines whether the deserializers should treat names that differ from a defined element or
    /// resource type name only by casing ("wrong-cased names") as unknown, rather than binding the data
    /// to the defined element like previous versions of the SDK did.
    /// </summary>
    /// <returns><c>true</c> when wrong-cased element names must be treated as unknown elements, so that
    /// the data is preserved under its original name in the overflow dictionary and the validator reports
    /// <see cref="CodedValidationException.WRONG_CASED_ELEMENT_CODE"/>. <c>false</c> when wrong-cased
    /// names are bound to the element they nearly match, so the data remains available in the typed
    /// POCO properties (the behaviour of SDK 6.2 and earlier).</returns>
    /// <remarks>
    /// <para>This decision is <b>intentionally derived from the <see cref="ExceptionFilter"/></b> in effect,
    /// rather than from a separate setting. The reasoning: if the configured filter <em>reports</em>
    /// wrong-case errors (as <see cref="DeserializationMode.Strict"/> does, since it reports everything),
    /// deserialization will fail because of them - and a failing parse should faithfully preserve the
    /// input rather than silently correct the name, so we bind strictly. If the filter <em>ignores</em>
    /// these errors (the default, and all other modes), the caller has chosen to tolerate them, and we
    /// keep the lenient legacy binding so the data ends up in the typed properties where it is most
    /// useful. This way the reported errors always accurately describe what happened to the data, and
    /// case strictness follows the chosen mode - or can be controlled explicitly with
    /// <see cref="Enforcing"/> / <see cref="Ignoring"/> and the wrong-case error codes - without a
    /// dedicated setting.</para>
    /// <para>When <see cref="Validator"/> is <c>null</c> (<see cref="DeserializationMode.SyntaxOnly"/>
    /// and <see cref="DeserializationMode.Ostrich"/>), all model validation is off. Wrong-case detection
    /// is a model-level check, not a syntax check, so no wrong-case errors will be raised at all and
    /// lenient binding is used.</para>
    /// <para>The check works by asking the filter how it would treat a representative
    /// <see cref="CodedValidationException.WRONG_CASED_ELEMENT_CODE"/> error. Custom filters therefore
    /// participate automatically: a filter that reports this code gets strict binding, a filter that
    /// ignores it gets lenient binding.</para>
    /// </remarks>
    internal bool UsesStrictCaseBinding() =>
        Validator is not null &&
        (ExceptionFilter is not { } filter || !filter(WRONG_CASE_PROBE));

    // A representative wrong-case error, used only to ask the ExceptionFilter whether it would
    // report or ignore errors with this code. Never surfaces to users.
    private static readonly CodedValidationException WRONG_CASE_PROBE =
        new(CodedValidationException.WRONG_CASED_ELEMENT_CODE,
            "This is a probe instance, used to determine how the exception filter treats wrong-case errors. It should never be reported.");

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
    /// Enable annotating line information of the parsed resources and properties.
    /// </summary>
    /// <remarks>
    /// This has a big impact on memory usage, as every element has to be aware where it was in the source data.
    /// It is recommended to be kept disabled.
    /// </remarks>
    public bool AnnotateLineInfo { get; init; } = false;

    /// <summary>
    /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
    /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
    /// default. Set this property to any other value than <see cref="None{T}"/>
    /// to perform validation.
    /// </summary>
    public NarrativeValidationKind NarrativeValidation { get; init; } = NarrativeValidationKind.None;

    private Predicate<CodedException>? augmentFilter(Predicate<CodedException>? baseFilter = null)
    {
        if(AllowUnrecognizedEnums) baseFilter = baseFilter.Ignore([CodedValidationException.INVALID_CODED_VALUE_CODE]);
        if (AcceptUnknownMembers) baseFilter = baseFilter.Ignore(
            [CodedValidationException.UNKNOWN_ELEMENT_CODE]);

        return baseFilter;
    }

    /// <summary>
    /// Raise an error when an xsi:schemaLocation is encountered.
    /// </summary>
    public bool DisallowXsiAttributesOnRoot { get; init; }

    /// <summary>
    /// Do not throw when encountering values not parseable as a member of an enumeration in a Poco.
    /// </summary>
    /// <remarks>
    /// This is the same as calling <see cref="Ignoring"/> with
    /// <c>CodedValidationException.INVALID_CODED_VALUE_CODE</c> as the argument on these settings.
    /// </remarks>
    public bool AllowUnrecognizedEnums { get; init; }

    /// <summary>
    /// Do not throw when the data has an element that does not map to a property in the Poco.
    /// </summary>
    /// <remarks>
    /// This is the same as calling <see cref="Ignoring"/> with
    /// <c>FhirXmlException.UNKNOWN_ELEMENT_CODE</c> and <c>FhirXmlException.UNKNOWN_ATTRIBUTE_CODE</c>
    /// as the arguments on these settings.
    /// </remarks>
    public bool AcceptUnknownMembers { get; init; }

    /// <summary>
    /// Enables all validation rules that are available.
    /// </summary>
    /// <param name="mode">The selected mode to use, see <see cref="DeserializationMode"/>.</param>
    /// <param name="nvk">How strict to validate the XHtml in FHIR Narrative. Only relevant in mode <see cref="DeserializationMode.Strict"/></param>
    public DeserializerSettings UsingMode(DeserializationMode mode,
        NarrativeValidationKind? nvk = null) =>
        mode switch
        {
            DeserializationMode.Strict => this with
            {
                ExceptionFilter = null, // No exceptions are ignored
                NarrativeValidation = nvk ?? NarrativeValidationKind.FhirXhtml
            },
            DeserializationMode.BackwardsCompatible => this with
            {
                ExceptionFilter = CodedExceptionFilters.FilterBackwardsCompatibilityIssues,
                NarrativeValidation = nvk ?? NarrativeValidationKind.None
            },
            DeserializationMode.Recoverable => this with
            {
                ExceptionFilter = CodedExceptionFilters.FilterRecoverableIssues,
                NarrativeValidation = nvk ?? NarrativeValidationKind.None
            },
            DeserializationMode.SyntaxOnly => this with
            {
                Validator = null,   // Disable all model validations, we don't care.
                ExceptionFilter = null,  // All exceptions coming from the parser are reported, on filtering.
                NarrativeValidation = NarrativeValidationKind.None // Irrelevant as the Validator = null.
            },
            DeserializationMode.NoOverflow => this with
            {
                ExceptionFilter = CodedExceptionFilters.FilterNoOverflowIssues,
                NarrativeValidation = nvk ?? NarrativeValidationKind.None
            },
            DeserializationMode.Ostrich => this with
            {
                Validator = null,   // Disable all model validations, we don't care.
                ExceptionFilter = _ => true,   // If there are still errors, ignore.
                NarrativeValidation = NarrativeValidationKind.None  // Irrelevant as the Validator = null.
            },
            _ => throw Error.NotSupported("Unknown deserialization mode.")
        };

    /// <summary>
    /// Alters the options to enforce specific parsing exceptions.
    /// </summary>
    public DeserializerSettings Enforcing(IEnumerable<string> toEnforce) =>
        this with { ExceptionFilter = this.ExceptionFilter.Enforce(toEnforce) };

    /// <summary>
    /// Alters the options to ignore specific parsing exceptions.
    /// </summary>
    public DeserializerSettings Ignoring(IEnumerable<string> toIgnore) =>
        this with { ExceptionFilter = this.ExceptionFilter.Ignore(toIgnore) };
}

[Obsolete("All parsing settings are unified under DeserializerSettings. Use that class instead.")]
public record FhirXmlPocoDeserializerSettings : DeserializerSettings;

[Obsolete("All parsing settings are unified under DeserializerSettings. Use that class instead.")]
public record ParserSettings : DeserializerSettings
{
    /// <inheritdoc />
    public override Predicate<CodedException>? ExceptionFilter
    {
#pragma warning disable CS0618 // Type or member is obsolete
    //    if (PermissiveParsing) augmentedFilter = augmentedFilter.Or(CodedExceptionFilters.IsRecoverableIssue);
    get => PermissiveParsing ? base.ExceptionFilter.Or(CodedExceptionFilters.FilterRecoverableIssues) : base.ExceptionFilter;
#pragma warning restore CS0618 // Type or member is obsolete


        init => base.ExceptionFilter = value;
    }

    /// <summary>
    /// Do not raise exceptions for recoverable errors.
    /// </summary>
    /// <remarks>This is the same as adding <see cref="CodedExceptionFilters.FilterRecoverableIssues"/> to the exception filter.</remarks>
    [Obsolete("Use WithMode(DeserializationMode.Recoverable) instead.")]
    public bool PermissiveParsing { get; init; }
}