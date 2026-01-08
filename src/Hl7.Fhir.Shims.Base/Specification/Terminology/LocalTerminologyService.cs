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
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

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

        _expander = new ValueSetExpander(settings);
    }

    /// <summary>
    /// Creates a MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets.
    /// </summary>
    /// <param name="coreResourceResolver">Resource resolves to resolve FHIR core artifacts</param>
    /// <param name="expanderSettings">ValueSet expansion settings</param>
    /// <returns>A MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets</returns>
    public static MultiTerminologyService CreateDefaultForCore(IAsyncResourceResolver coreResourceResolver,
        ValueSetExpanderSettings? expanderSettings = null)
    {
        return TerminologyServiceFactory.CreateDefaultForCore(coreResourceResolver, expanderSettings);
    }

    internal async Task<ValueSet?> FindValueSet(Canonical canonical)
    {
        var valueset = await _resolver.FindValueSetAsync(canonical).ConfigureAwait(false);

#if STU3
            if (valueset == null && _resolver is IConformanceSource source)
#else
        if (valueset == null && _resolver is ICommonConformanceSource source)
#endif
        {
            var cs = source.FindCodeSystemByValueSet(canonical);
            if (cs != null)
            {
                valueset = new ValueSet
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
                    Compose = new ValueSet.ComposeComponent
                    {
                        Include =
                        [
                            new ValueSet.ConceptSetComponent { System = cs.Url }
                        ]
                    }
                };

                return valueset;
            }
        }

        return valueset;
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

        var vs = await FindValueSet(resolvableCanonical).ConfigureAwait(false);

        if (vs is null)
            throw new FhirOperationException(
                $"Operation {operation} failed: valueset '{resolvableCanonical}' is unknown.", HttpStatusCode.NotFound);

        return await getExpandedValueSet(vs, operation).ConfigureAwait(false);
    }

    /// <summary>
    /// Resolve and expand a ValueSet by its canonical URL.
    /// </summary>
    protected override async Task<ValueSet> ResolveValueSet(FhirUri canonical)
    {
        var vs = await FindValueSet(new Canonical(canonical.Value)).ConfigureAwait(false);

        if (vs is null)
            throw new FhirOperationException(
                $"ValueSet '{canonical.Value}' is unknown.", HttpStatusCode.NotFound);

        return await getExpandedValueSet(vs, "validate code").ConfigureAwait(false);
    }

    /// <summary>
    /// Validate a single code against an expanded ValueSet.
    /// </summary>
    protected override async Task<ValidateCodeResult> ValidateCode(ValidateCodeParameters parameters)
    {
        var vs = (parameters.ValueSet as ValueSet)!;
        var code = parameters.Code;
        var system = parameters.System?.Value;
        var display = parameters.Display?.Value;
        var abstractAllowed = parameters.Abstract;
        
        if (code is null)
            return ValidateCodeResult.ForResult(false, "No code supplied.");

        var component = vs.FindInExpansion(code.Value!, system);
        var codeLabel = $"Code '{code.Value}'"
            + (string.IsNullOrEmpty(display) ? string.Empty : $" (display '{display}')") 
            + (string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'");
        
        var success = true;
        var messages = new StringBuilder();

        if (component is null)
        {
            await messageForCodeNotFound(vs, system, codeLabel, messages).ConfigureAwait(false);
            success = false;
        }
        else
        {
            if (component.Abstract == true && abstractAllowed?.Value == false)
            {
                messages.AppendLine($"{codeLabel} is abstract, which is not allowed here");
                success = false;
            }

            if (display != null && component.Display != null && display != component.Display)
            {
                // this is only a warning (so success is still true)
                messages.AppendLine($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'");
            }
        }

        var displayValue = component?.Display ?? display;
        var message = messages.Length > 0 ? messages.ToString().TrimEnd() : null;
        
        return ValidateCodeResult.ForResult(success, message, displayValue);
    }

    ///<inheritdoc />
    protected override async Task<Resource> Expand(ExpandParameters parameters)
    {
        var url = parameters.Url;
        var valueSet = parameters.ValueSet as ValueSet;

        if (valueSet is null && url is null)
            throw new FhirOperationException("Have to supply either a canonical url or a valueset.",
                HttpStatusCode.UnprocessableEntity);

        var version = parameters.GetSingleValue<FhirString>("valueSetVersion");

        return valueSet is null
            ? await getExpandedValueSet(url!, version, "expand").ConfigureAwait(false)
            : await getExpandedValueSet(valueSet, "expand").ConfigureAwait(false);
    }


    private async Tasks.Task messageForCodeNotFound(ValueSet vs, string? system, string codeLabel,
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