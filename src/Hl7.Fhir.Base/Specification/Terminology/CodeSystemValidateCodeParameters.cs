/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Specification.Terminology;

public class CodeSystemValidateCodeParameters : Parameters
{
    public const string URL_ATTRIBUTE = "url";
    public const string VALUE_SET_ATTRIBUTE = "codeSystem";
    public const string CODE_ATTRIBUTE = "code";
    public const string VERSION_ATTRIBUTE = "version";
    public const string DISPLAY_ATTRIBUTE = "display";
    public const string CODING_ATTRIBUTE = "coding";
    public const string CODEABLE_CONCEPT_ATTRIBUTE = "codeableConcept";
    public const string DATE_ATTRIBUTE = "date";
    public const string ABSTRACT_ATTRIBUTE = "abstract";
    public const string DISPLAY_LANGUAGE_ATTRIBUTE = "displayLanguage";

    public CodeSystemValidateCodeParameters()
    {
        // Nothing
    }

    public CodeSystemValidateCodeParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// A canonical reference to a value set.
    /// </summary>
    public FhirUri? Url
    {
        get => this.GetSingleValue<FhirUri>(URL_ATTRIBUTE);
        set => this.SetSingleValue(URL_ATTRIBUTE, value);
    }

    /// <summary>
    /// The value set is provided directly as part of the request.
    /// </summary>
    public Resource? CodeSystem
    {
        get => this.GetSingleResource(VALUE_SET_ATTRIBUTE);
        set => this.SetSingleResource(VALUE_SET_ATTRIBUTE, value);
    }

    /// <summary>
    /// The identifier that is used to identify a specific version of the value set to be used when validating the code.
    /// </summary>
    public FhirString? CodeSystemVersion
    {
        get => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// The code that is to be validated.
    /// </summary>
    /// <remarks>If a code is provided, a system or a context must be provided.</remarks>
    public Code? Code
    {
        get => this.GetSingleValue<Code>(CODE_ATTRIBUTE);
        set => this.SetSingleValue(CODE_ATTRIBUTE, value);
    }
    
    /// <summary>
    /// The display associated with the code.
    /// </summary>
    /// <remarks>If a display is provided a code must be provided.</remarks>
    public FhirString? Display
    {
        get => this.GetSingleValue<FhirString>(DISPLAY_ATTRIBUTE);
        set => this.SetSingleValue(DISPLAY_ATTRIBUTE, value);
    }

    /// <summary>
    /// A coding to validate.
    /// </summary>
    public Coding? Coding
    {
        get => this.GetSingleValue<Coding>(CODING_ATTRIBUTE);
        set => this.SetSingleValue(CODING_ATTRIBUTE, value);
    }

    /// <summary>
    /// A full codeableConcept to validate.
    /// </summary>
    /// <remarks>The server returns true if one of the coding values is in the value set, and may also validate that the codings are not in conflict with each other if more than one is present.</remarks>
    public CodeableConcept? CodeableConcept
    {
        get => this.GetSingleValue<CodeableConcept>(CODEABLE_CONCEPT_ATTRIBUTE);
        set => this.SetSingleValue(CODEABLE_CONCEPT_ATTRIBUTE, value);
    }

    /// <summary>
    /// The date for which the validation should be checked.
    /// </summary>
    public FhirDateTime? Date
    {
        get => this.GetSingleValue<FhirDateTime>(DATE_ATTRIBUTE);
        set => this.SetSingleValue(DATE_ATTRIBUTE, value);
    }

    /// <summary>
    /// If this parameter has a value of true, the client is stating that the validation is being performed in a context where a concept designated as 'abstract' is appropriate/allowed to be used, and the server should regard abstract codes as valid.
    /// If this parameter is false, abstract codes are not considered to be valid.
    /// </summary>
    public FhirBoolean? Abstract
    {
        get => this.GetSingleValue<FhirBoolean>(ABSTRACT_ATTRIBUTE);
        set => this.SetSingleValue(ABSTRACT_ATTRIBUTE, value);
    }

    /// <summary>
    /// Specifies the language to be used for description when validating the display property.
    /// </summary>
    public Code? DisplayLanguage
    {
        get => this.GetSingleValue<Code>(DISPLAY_LANGUAGE_ATTRIBUTE);
        set => this.SetSingleValue(DISPLAY_LANGUAGE_ATTRIBUTE, value);
    }

    #region Builder methods
    public CodeSystemValidateCodeParameters WithCodeSystem(string? url, Resource? codeSystem = null, string? version = null)
    {
        if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
        if (codeSystem is not null) CodeSystem = codeSystem;
        if (!string.IsNullOrWhiteSpace(version)) CodeSystemVersion = new FhirString(version);
        
        return this;
    }

    /// <summary>
    /// Takes a canonical and splits it into the correct "url", and "valueSetVersion" parameters.
    /// </summary>
    /// <param name="canonical">Canonical to be split up</param>
    /// <returns></returns>
    public CodeSystemValidateCodeParameters WithCodeSystem(Canonical canonical)
    {
        var (uri, version, fragment) = canonical;
        Url = new FhirUri(new Canonical(uri, null, fragment));
        if (!string.IsNullOrWhiteSpace(version)) CodeSystemVersion = new FhirString(version);
        return this;
    }

    public CodeSystemValidateCodeParameters WithCode(string? code = null, string? display = null, string? displayLanguage = null)
    {
        if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
        if (!string.IsNullOrWhiteSpace(display)) Display = new FhirString(display);
        if (!string.IsNullOrWhiteSpace(displayLanguage)) DisplayLanguage = new Code(displayLanguage);
        return this;
    }

    public CodeSystemValidateCodeParameters WithCoding(Coding? coding)
    {
        Coding = coding;
        return this;
    }

    public CodeSystemValidateCodeParameters WithCodeableConcept(CodeableConcept? codeableConcept)
    {
        CodeableConcept = codeableConcept;
        return this;
    }

    public CodeSystemValidateCodeParameters WithDate(FhirDateTime? date)
    {
        Date = date;
        return this;
    }

    public CodeSystemValidateCodeParameters WithAbstract(bool? @abstract)
    {
        Abstract = @abstract.HasValue ? new FhirBoolean(@abstract) : null;
        return this;
    }
    #endregion

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => this.DeepCopy();
}
