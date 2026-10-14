/*
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Hl7.Fhir.Model.OperationOutcome;
using T = System.Threading.Tasks;
using TX = Hl7.Fhir.Specification.Terminology.TerminologyServiceOperationExceptionExtensions;

namespace Hl7.Fhir.Specification.Terminology;

public class LocalTerminologyService : BaseTerminologyService
{
    private static readonly SemaphoreSlim SEMAPHORE = new(1, 1);

    private readonly IAsyncResourceResolver _resolver;
    private readonly ValueSetExpander _expander;

    public LocalTerminologyService(IAsyncResourceResolver resolver, ValueSetExpanderSettings? expanderSettings = null)
    {
        _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

        var settings = expanderSettings ?? ValueSetExpanderSettings.CreateDefault();
        settings.ValueSetSource ??= resolver;

        _expander = new(settings);
    }

    /// <summary>
    /// Creates a MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets.
    /// </summary>
    /// <param name="coreResourceResolver">Resource resolves to resolve FHIR core artifacts</param>
    /// <param name="expanderSettings">ValueSet expansion settings</param>
    /// <returns>A MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets</returns>
    public static MultiTerminologyService CreateDefaultForCore(IAsyncResourceResolver coreResourceResolver, ValueSetExpanderSettings? expanderSettings = null)
    {
        return TerminologyServiceFactory.CreateDefaultForCore(coreResourceResolver, expanderSettings);
    }

    protected internal override async Task<ValueSet?> ResolveValueSet(Canonical canonical)
    {
        var canonicalString = canonical.ToString();
        var valueset = await _resolver.FindValueSetAsync(canonicalString).ConfigureAwait(false);

        if (valueset is not null)
            return valueset;

#if STU3
            if (_resolver is IConformanceSource source)
#else
        if (_resolver is ICommonConformanceSource source)
#endif
        {
            var cs = source.FindCodeSystemByValueSet(canonicalString);
            if (cs != null)
            {
                valueset = new()
                {
                    Url = canonical,
                    Status = cs.Status, // mandatory field

#if !STU3
                    ApprovalDate = cs.ApprovalDate,
                    Author = cs.Author,
                    CopyrightLabel = cs.CopyrightLabel,
                    Editor = cs.Editor,
                    EffectivePeriod = cs.EffectivePeriod,
                    Endorser = cs.Endorser,
                    LastReviewDate = cs.LastReviewDate,
                    RelatedArtifact = cs.RelatedArtifact,
                    Reviewer = cs.Reviewer,
                    Topic = cs.Topic,
                    VersionAlgorithm = cs.VersionAlgorithm,
#endif
                    Contact = cs.Contact,
                    Copyright = cs.Copyright,
                    Date = cs.Date,
                    Description = cs.Description,
                    Experimental = cs.Experimental,
                    Id = cs.Id,
                    Jurisdiction = cs.Jurisdiction,
                    Language = cs.Language,
                    Name = cs.Name,
                    Publisher = cs.Publisher,
                    Purpose = cs.Purpose,
                    Title = cs.Title,
                    UseContext = cs.UseContext,
                    Version = cs.Version,
                    Compose = new()
                    {
                        Include =
                        [
                            new() { System = cs.Url }
                        ]
                    }
                };

                return valueset;
            }
        }

        return null;
    }

    private async Task<ValueSet> getExpandedValueSet(ValueSet vs, string operation)
    {
        try
        {
            await SEMAPHORE.WaitAsync().ConfigureAwait(false);

            try
            {
                // We might have a cached or pre-expanded version brought to us by the _source
                if (!vs.HasExpansion)
                {
                    // This will expand te vs - since we do not deepcopy() it, it will change the instance
                    // as it was passed to us from the source
                    await _expander.ExpandAsync(vs).ConfigureAwait(false);
                }
            }
            finally
            {
                SEMAPHORE.Release();
            }
        }
        catch (TerminologyServiceException e)
        {
            // Unprocessable entity
            throw new FhirOperationException(
                $"Operation {operation} failed: creating the required expansion failed with message \"{e.Message}\".",
                HttpStatusCode.UnprocessableEntity);
        }

        return vs;
    }

    private async Task<ValueSet> getExpandedValueSet(FhirUri url, FhirString? version, string operation)
    {
        // Handling the url + version is a bit tricky, since some callers (i.e. Firely tools) will call this
        // operation with a version in the url, but others will supply this as url+version (which is the correct way).
        var (uri, canonicalVersion, fragment) = new Canonical(url.Value);
        var versionToUse = version?.Value ?? canonicalVersion;
        var resolvableCanonical = new Canonical(uri, versionToUse, fragment);

        var vs = await ResolveValueSet(resolvableCanonical).ConfigureAwait(false);

        if (vs is null)
            throw new FhirOperationException(
                $"Operation {operation} failed: valueset '{resolvableCanonical}' is unknown.", HttpStatusCode.NotFound);

        return await getExpandedValueSet(vs, operation).ConfigureAwait(false);
    }

    protected override async Task<ValidateCodeResult> ValueSetValidateCode(ValidateCodeParameters parameters)
    {
        var valueSet = parameters.ValueSet as ValueSet;
        valueSet = valueSet is null
            ? await getExpandedValueSet(parameters.Url!, parameters.ValueSetVersion, "validate code").ConfigureAwait(false)
            : await getExpandedValueSet(valueSet, "validate code").ConfigureAwait(false);
        
        if (parameters.CodeableConcept is not null)
            return await validateCodeVs(valueSet, parameters.CodeableConcept, parameters.Abstract?.Value).ConfigureAwait(false);
        if (parameters.Coding is not null)
            return await validateCodeVs(valueSet, parameters.Coding, parameters.Abstract?.Value).ConfigureAwait(false);
        
        return await validateCodeVs(valueSet, parameters.Code?.Value, parameters.System?.Value, parameters.Display?.Value, parameters.Abstract?.Value).ConfigureAwait(false);
    }

    protected override async Task<Resource> Expand(ExpandParameters parameters)
    {
        if(parameters.ValueSet is ValueSet vs)
            return await getExpandedValueSet(vs, "expand").ConfigureAwait(false);
        
        return await getExpandedValueSet(parameters.Url!, parameters.ValueSetVersion, "expand").ConfigureAwait(false);
    }


    private async Task<ValidateCodeResult> validateCodeVs(ValueSet vs, CodeableConcept cc, bool? abstractAllowed)
    {
        // Look for one succesful match in any of the codes in the CodeableConcept.
        // These are run sequentially (rather than in parallel) since the caller-supplied resolver
        // used to resolve the codings is not guaranteed to be safe for concurrent use.
        var callResults = new List<ValidateCodeResult>();
        for (var i = 0; i < cc.Coding.Count; i++)
        {
            var coding = cc.Coding[i];
            callResults.Add(await validateCodeVs(vs, coding.Code, coding.System, coding.Display, abstractAllowed, $"CodeableConcept.coding[{i}]").ConfigureAwait(false));
        }

        var result = callResults.FirstOrDefault(p => p.Result?.Value == true);

        if (result is null && callResults.Count == 1)
        {
            // A single invalid coding: its own result already says everything there is to say.
            result = callResults[0];
        }
        else if (result is null)
        {
            // None of the codings were valid: report the issues of all codings, but not the code/system/display
            // of any one of them, since none of them was the one that was validated.
            result = ValidateCodeResult.ForResult(false);
            result.AddIssue(IssueSeverity.Error, IssueType.CodeInvalid, TX.NOT_IN_VS,
                $"None of the Codings in the CodeableConcept were valid for the value set '{vs.Title ?? vs.Name}' ({vs.Url})");

            if (result.Issues is OperationOutcome outcome)
            {
                foreach (var callResult in callResults)
                {
                    if (callResult.Issues is OperationOutcome callOutcome)
                        outcome.Issue.AddRange(callOutcome.Issue);
                }
            }

#if !STU3
            // Only report an unknown system when it is the reason that none of the codings could be validated.
            var unknownSystems = callResults.Select(r => r.UnknownSystem?.Value).Distinct().ToList();
            if (unknownSystems is [{ } unknownSystem])
                result.UnknownSystem = new Canonical(unknownSystem);
#endif

            setMessage(result);
        }

        result.CodeableConcept = (CodeableConcept)cc.DeepCopy();
        return result;
    }

    private async Task<ValidateCodeResult> validateCodeVs(ValueSet vs, Coding coding, bool? abstractAllowed)
    {
        return await validateCodeVs(vs, coding.Code, coding.System, coding.Display, abstractAllowed, "Coding")
            .ConfigureAwait(false);
    }

    // Validates a code against an expanded valueset. The path is the path of the coding that is validated, used in
    // the expressions of the issues: null when validating the code/system/display parameters, "Coding" when
    // validating a Coding, and "CodeableConcept.coding[n]" when validating a coding of a CodeableConcept.
    private async Task<ValidateCodeResult> validateCodeVs(ValueSet vs, string? code, string? system, string? display, bool? abstractAllowed, string? path = null)
    {
        var codePath = elementPath(path, "code");

        if (code is null)
        {
            var noCode = ValidateCodeResult.ForResult(false, system: system);
            noCode.AddIssue(IssueSeverity.Error, IssueType.Invalid, TX.INVALID_DATA, "No code supplied.", codePath);
            setMessage(noCode);
            return noCode;
        }

        var component = vs.FindInExpansion(code, system);
        var codeLabel = $"Code '{code}'"
                        + (string.IsNullOrEmpty(display) ? string.Empty : $" (display '{display}')")
                        + (string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'");

        if (component is null)
        {
            var notFound = ValidateCodeResult.ForResult(false, code: code, system: system);
            await addIssuesForCodeNotFound(notFound, vs, code, system, codeLabel, path).ConfigureAwait(false);
            setMessage(notFound);
            return notFound;
        }

        // The expansion may lack the display and version (e.g. for concepts enumerated in the ValueSet),
        // so complete them from the code system, which we also need for the status of the concept.
        var resolvedSystem = component.System ?? system;
        var codeSystem = resolvedSystem is null ? null : await findCodeSystem(resolvedSystem, component.Version).ConfigureAwait(false);
        var concept = codeSystem?.Concept.FindCode(code);
        var conceptDisplay = component.Display ?? concept?.Display;

        // abstractAllowed == null means abstract codes are not checked.
        var abstractRejected = component.Abstract == true && abstractAllowed == false;

        var result = ValidateCodeResult.ForResult(!abstractRejected,
            display: conceptDisplay ?? display,
            code: code,
            system: resolvedSystem,
            version: component.Version ?? codeSystem?.Version);

        if (abstractRejected)
        {
            result.AddIssue(IssueSeverity.Error, IssueType.BusinessRule, TX.CODE_RULE,
                $"{codeLabel} is abstract, which is not allowed here", codePath);
        }

        if (display != null && conceptDisplay != null && display != conceptDisplay)
        {
            // this is only a warning (so success is still true)
            result.AddIssue(IssueSeverity.Warning, IssueType.Invalid, TX.INVALID_DISPLAY,
                $"{codeLabel} has incorrect display '{display}', should be '{conceptDisplay}'", elementPath(path, "display"));
        }

        addConceptStatus(result, component, codeSystem, concept, code, path ?? "code");

        setMessage(result);
        return result;
    }

    private static string elementPath(string? path, string element) => path is null ? element : $"{path}.{element}";

    /// <summary>
    /// Sets the message of the result to the texts of its error and warning issues, or removes it when there are none.
    /// </summary>
    private static void setMessage(ValidateCodeResult result)
    {
        var texts = (result.Issues as OperationOutcome)?.Issue
            .Where(i => i.Severity is IssueSeverity.Fatal or IssueSeverity.Error or IssueSeverity.Warning)
            .Select(i => i.Details?.Text)
            .Where(t => !string.IsNullOrEmpty(t))
            .ToList();

        result.SetSingleValue(ValidateCodeResult.MESSAGE_ATTRIBUTE,
            texts is { Count: > 0 } ? new FhirString(string.Join("; ", texts)) : null);
    }

    private async T.Task addIssuesForCodeNotFound(ValidateCodeResult result, ValueSet vs, string code, string? system, string codeLabel, string? path)
    {
        var codePath = elementPath(path, "code");
        var systemPath = elementPath(path, "system");

        if (system is not null && await isValueSet(system).ConfigureAwait(false))
        {
            result.AddIssue(IssueSeverity.Error, IssueType.Invalid, TX.INVALID_DATA,
                $"The Coding references a value set, not a code system ('{system}')", systemPath);
            return;
        }

        result.AddIssue(IssueSeverity.Error, IssueType.CodeInvalid, TX.NOT_IN_VS,
            $"{codeLabel} does not exist in the value set '{vs.Title ?? vs.Name}' ({vs.Url})", codePath);

        if (system is null)
            return;

        // Find out why the code is not in the valueset: is it unknown in its code system,
        // or is the code system itself unknown?
        var codeSystem = await findCodeSystem(system, null).ConfigureAwait(false);
        var concept = codeSystem?.Concept.FindCode(code);
        if (codeSystem is not null)
        {
            // Report what we know about the code, even though it is not in the valueset.
            if (!string.IsNullOrEmpty(codeSystem.Version))
                result.SetSingleValue(ValidateCodeResult.VERSION_ATTRIBUTE, new FhirString(codeSystem.Version));
            if (!string.IsNullOrEmpty(concept?.Display))
                result.SetSingleValue(ValidateCodeResult.DISPLAY_ATTRIBUTE, new FhirString(concept!.Display));
        }

        if (codeSystem is null)
        {
            result.AddIssue(IssueSeverity.Error, IssueType.NotFound, TX.NOT_FOUND,
                $"A definition for CodeSystem '{system}' could not be found, so the code cannot be validated", systemPath);
#if !STU3
            result.UnknownSystem = new Canonical(system);
#endif
        }
#if STU3
        else if (codeSystem.Content == CodeSystem.CodeSystemContentMode.Complete && concept is null)
#else
        else if (codeSystem.Content == CodeSystemContentMode.Complete && concept is null)
#endif
        {
            var version = string.IsNullOrEmpty(codeSystem.Version) ? string.Empty : $" version '{codeSystem.Version}'";
            result.AddIssue(IssueSeverity.Error, IssueType.CodeInvalid, TX.INVALID_CODE,
                $"Code '{code}' does not exist in the code system '{system}'{version}", codePath);
        }

        async Task<bool> isValueSet(string sys)
        {
            // First, conduct a quick initial check, and if that fails, proceed with a more comprehensive approach.
            return (sys.Contains(@"/ValueSet/") ||
                    await _resolver.FindValueSetAsync(sys).ConfigureAwait(false) is not null);
        }
    }

    // Finds the code system, in the given version if there is one. Returns null rather than another version.
    private async Task<CodeSystem?> findCodeSystem(string system, string? version)
    {
        // Take the same lock as the expansion, since the caller-supplied resolver
        // is not guaranteed to be safe for concurrent use.
        await SEMAPHORE.WaitAsync().ConfigureAwait(false);
        try
        {
            if (string.IsNullOrEmpty(version))
                return await _resolver.FindCodeSystemAsync(system).ConfigureAwait(false);

            var codeSystem = await _resolver.FindCodeSystemAsync($"{system}|{version}").ConfigureAwait(false)
                             ?? await _resolver.FindCodeSystemAsync(system).ConfigureAwait(false);
            return codeSystem?.Version == version ? codeSystem : null;
        }
        finally
        {
            SEMAPHORE.Release();
        }
    }

    /// <summary>
    /// Adds the inactive and status parameters and a warning when the concept is not active in its code system.
    /// </summary>
    private static void addConceptStatus(ValidateCodeResult result, ValueSet.ContainsComponent component,
        CodeSystem? codeSystem, CodeSystem.ConceptDefinitionComponent? concept, string code, string expression)
    {
        var inactive = component.Inactive == true;
        string? status = null;

        if (codeSystem is not null && concept is not null)
        {
            status = conceptProperty(codeSystem, concept, "status", CodeSystem.CONCEPTPROPERTY_STATUS) switch
            {
                Code c => c.Value,
                FhirString s => s.Value,
                _ => null
            };

            if (conceptProperty(codeSystem, concept, "inactive", CodeSystem.CONCEPTPROPERTY_INACTIVE) is FhirBoolean { Value: true })
                inactive = true;
        }

        if (status is "inactive" or "retired")
            inactive = true;

        if (inactive)
            result.Inactive = new FhirBoolean(true);

        if (status is not null && status != "active")
            result.Status = new Code(status);

        if (inactive)
        {
            result.AddIssue(IssueSeverity.Warning, IssueType.BusinessRule, TX.CODE_COMMENT,
                $"The concept '{code}' has a status of {status ?? "inactive"} and its use should be reviewed", expression);
        }
        else if (status is not null && status != "active")
        {
            result.AddIssue(IssueSeverity.Warning, IssueType.BusinessRule, TX.CODE_COMMENT,
                $"The concept '{code}' has a status of {status} and its use should be reviewed", expression);
        }
    }

    /// <summary>
    /// Finds the value of a concept property, either by its code or by the uri of the property definition.
    /// </summary>
    private static DataType? conceptProperty(CodeSystem codeSystem, CodeSystem.ConceptDefinitionComponent concept, string code, string uri)
    {
        var propertyCodes = codeSystem.Property.Where(p => p.Uri == uri).Select(p => p.Code).Append(code).ToList();
        return concept.Property.FirstOrDefault(p => propertyCodes.Contains(p.Code))?.Value;
    }
}
