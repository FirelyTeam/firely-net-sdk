#nullable enable

/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Specification.Terminology;

public class TranslateParameters : Parameters
{
    public const string URL_ATTRIBUTE = "url";
    public const string CONCEPT_MAP_ATTRIBUTE = "conceptMap";
    public const string CONCEPT_MAP_VERSION_ATTRIBUTE = "conceptMapVersion";
    public const string CODE_ATTRIBUTE = "code";
    public const string SYSTEM_ATTRIBUTE = "system";
    public const string VERSION_ATTRIBUTE = "version";
    public const string SOURCE_ATTRIBUTE = "source";
    public const string CODING_ATTRIBUTE = "coding";
    public const string CODEABLE_CONCEPT_ATTRIBUTE = "codeableConcept";
    public const string TARGET_ATTRIBUTE = "target";
    public const string TARGET_SYSTEM_ATTRIBUTE = "targetSystem";
    public const string REVERSE_ATTRIBUTE = "reverse";

    public TranslateParameters()
    {
        // Nothing
    }

    public TranslateParameters(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// A canonical URL for a concept map.
    /// </summary>
    public FhirUri? Url
    {
        get => this.GetSingleValue<FhirUri>(URL_ATTRIBUTE);
        set => this.SetSingleValue(URL_ATTRIBUTE, value);
    }

    /// <summary>
    /// The concept map is provided directly as part of the request.
    /// </summary>
#if STU3
    public ConceptMap? ConceptMap
#else
    public Resource? ConceptMap
#endif
    {
        get => this.GetSingleResource(CONCEPT_MAP_ATTRIBUTE);
        set => this.SetSingleResource(CONCEPT_MAP_ATTRIBUTE, value);
    }

    /// <summary>
    /// The identifier that is used to identify a specific version of the concept map to be used for the translation.
    /// </summary>
    public FhirString? ConceptMapVersion
    {
        get => this.GetSingleValue<FhirString>(CONCEPT_MAP_VERSION_ATTRIBUTE);
        set => this.SetSingleValue(CONCEPT_MAP_VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// The code that is to be translated. If a code is provided, a system must be provided.
    /// </summary>
    public Code? Code
    {
        get => this.GetSingleValue<Code>(CODE_ATTRIBUTE);
        set => this.SetSingleValue(CODE_ATTRIBUTE, value);
    }

    /// <summary>
    /// The system for the code that is to be translated
    /// </summary>
    public FhirUri? System
    {
        get => this.GetSingleValue<FhirUri>(SYSTEM_ATTRIBUTE);
        set => this.SetSingleValue(SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// The version of the system, if one was provided in the source data.
    /// </summary>
    public FhirString? Version
    {
        get => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);
        set => this.SetSingleValue(VERSION_ATTRIBUTE, value);
    }

    /// <summary>
    /// Identifies the value set used when the concept (system/code pair) was chosen. May be a logical id, or an absolute or relative location.
    /// </summary>
    public FhirUri? Source
    {
        get => this.GetSingleValue<FhirUri>(SOURCE_ATTRIBUTE);
        set => this.SetSingleValue(SOURCE_ATTRIBUTE, value);
    }

    /// <summary>
    /// A coding to translate
    /// </summary>
    public Coding? Coding
    {
        get => this.GetSingleValue<Coding>(CODING_ATTRIBUTE);
        set => this.SetSingleValue(CODING_ATTRIBUTE, value);
    }

    /// <summary>
    /// A full codeableConcept to validate.
    /// </summary>
    public CodeableConcept? CodeableConcept
    {
        get => this.GetSingleValue<CodeableConcept>(CODEABLE_CONCEPT_ATTRIBUTE);
        set => this.SetSingleValue(CODEABLE_CONCEPT_ATTRIBUTE, value);
    }

    /// <summary>
    /// Identifies the value set in which a translation is sought.
    /// </summary>
    public FhirUri? Target
    {
        get => this.GetSingleValue<FhirUri>(TARGET_ATTRIBUTE);
        set => this.SetSingleValue(TARGET_ATTRIBUTE, value);
    }

    /// <summary>
    /// identifies a target code system in which a mapping is sought. This parameter is an alternative to the target parameter - only one is required.
    /// </summary>
    public FhirUri? TargetSystem
    {
        get => this.GetSingleValue<FhirUri>(TARGET_SYSTEM_ATTRIBUTE);
        set => this.SetSingleValue(TARGET_SYSTEM_ATTRIBUTE, value);
    }

    /// <summary>
    /// If this is true, then the operation should return all the codes that might be mapped to this code. This parameter reverses the meaning of the source and target parameters
    /// </summary>
    public FhirBoolean? Reverse
    {
        get => this.GetSingleValue<FhirBoolean>(REVERSE_ATTRIBUTE);
        set => this.SetSingleValue(REVERSE_ATTRIBUTE, value);
    }

    #region Builder methods

#if STU3
        public TranslateParameters WithConceptMap(string? url = null, ConceptMap? conceptMap = null, string? conceptMapVersion = null, string? source = null)
#else
    public TranslateParameters WithConceptMap(string? url = null, Resource? conceptMap = null, string? conceptMapVersion = null, string? source = null)
#endif
    {
        if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
        ConceptMap = conceptMap;
        if (!string.IsNullOrWhiteSpace(conceptMapVersion)) ConceptMapVersion = new FhirString(conceptMapVersion);
        if (!string.IsNullOrWhiteSpace(source)) Source = new FhirUri(source);
        return this;
    }

    public TranslateParameters WithCode(string? code, string? system = null, string? version = null)
    {
        if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
        if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
        if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);
        return this;
    }

    public TranslateParameters WithCoding(Coding? coding)
    {
        Coding = coding;
        return this;
    }

    public TranslateParameters WithCodeableConcept(CodeableConcept? codeableConcept)
    {
        CodeableConcept = codeableConcept;
        return this;
    }

    public TranslateParameters WithTarget(string? target, string? targetSystem = null)
    {
        if (!string.IsNullOrWhiteSpace(target)) Target = new FhirUri(target);
        if (!string.IsNullOrWhiteSpace(targetSystem)) TargetSystem = new FhirUri(targetSystem);
        return this;
    }

    public TranslateParameters WithReverse(bool? reverse)
    {
        if (reverse.HasValue) Reverse = new FhirBoolean(reverse);
        return this;
    }

    #endregion

    [Obsolete("This is just a DeepCopy of the current instance, use the instance or DeepCopy() instead", false)]
    public Parameters Build() => this.DeepCopy();
}

#nullable restore

