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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T = System.Threading.Tasks;

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
        // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
        if (cc.Coding.Count == 1)
            return await validateCodeVs(vs, cc.Coding.Single(), abstractAllowed).ConfigureAwait(false);

        // Else, look for one succesful match in any of the codes in the CodeableConcept
        var callResults = await T.Task.WhenAll(cc.Coding.Select(coding => validateCodeVs(vs, coding, abstractAllowed))).ConfigureAwait(false);
        var successResult = callResults.FirstOrDefault(p => p.GetSingleValue<FhirBoolean>("result")?.Value == true);

        if (successResult is not null)
            return successResult;

        var messages = new StringBuilder();
        messages.AppendLine("None of the Codings in the CodeableConcept were valid for the binding. Details follow.");

        // gathering the messages of all calls
        foreach (var msg in callResults.Select(cr => cr.GetSingleValue<FhirString>("message")?.Value)
                     .Where(m => m is not null))
        {
            messages.AppendLine(msg);
        }

        return ValidateCodeResult.ForResult(false, messages.ToString());
    }

    private async Task<ValidateCodeResult> validateCodeVs(ValueSet vs, Coding coding, bool? abstractAllowed)
    {
        return await validateCodeVs(vs, coding.Code, coding.System, coding.Display, abstractAllowed)
            .ConfigureAwait(false);
    }

    private async Task<ValidateCodeResult> validateCodeVs(ValueSet vs, string? code, string? system, string? display, bool? abstractAllowed)
    {
        if (code is null)
            return ValidateCodeResult.ForResult(false, "No code supplied.");

        var component = vs.FindInExpansion(code, system);
        var codeLabel = $"Code '{code}'"
                        + (string.IsNullOrEmpty(display) ? string.Empty : $" (display '{display}')")
                        + (string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'");
        
        var messages = new StringBuilder();
        if (component is null)
        {
            await messageForCodeNotFound(vs, system, codeLabel, messages).ConfigureAwait(false);
            return ValidateCodeResult.ForResult(false, messages.ToString().TrimEnd());
        }

        if (component.Abstract == true && abstractAllowed == false)// will be ignored if abstractAllowed == null
            return ValidateCodeResult.ForResult(false, $"{codeLabel} is abstract, which is not allowed here");

        if (display != null && component.Display != null && display != component.Display)
        {
            // this is only a warning (so success is still true)
            messages.AppendLine($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'");
        }

        return ValidateCodeResult.ForResult(true, messages.Length > 0 ? messages.ToString().TrimEnd() : null, display: component.Display ?? display);
    }

    private async T.Task messageForCodeNotFound(ValueSet vs, string? system, string codeLabel,
        StringBuilder messages)
    {
        if (system is not null && await isValueSet(system).ConfigureAwait(false))
        {
            messages.AppendLine($"The Coding references a value set, not a code system ('{system}')");
        }
        else
        {
            messages.AppendLine($"{codeLabel} does not exist in the value set '{vs.Title ?? vs.Name}' ({vs.Url})");
        }

        async Task<bool> isValueSet(string sys)
        {
            // First, conduct a quick initial check, and if that fails, proceed with a more comprehensive approach.
            return (sys.Contains(@"/ValueSet/") ||
                    await _resolver.FindValueSetAsync(sys).ConfigureAwait(false) is not null);
        }
    }
}