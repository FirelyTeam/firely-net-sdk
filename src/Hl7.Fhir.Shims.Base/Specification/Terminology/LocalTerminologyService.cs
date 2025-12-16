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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

public class LocalTerminologyService : ITerminologyService
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

    ///<inheritdoc />
    public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        parameters.CheckForValidityOfValidateCodeParams();

        var validateCodeParams = new ValidateCodeParameters(parameters);
        var valueSet = validateCodeParams.ValueSet as ValueSet;
        if (valueSet is null && validateCodeParams.Url is null)
            throw new FhirOperationException("Have to supply either a canonical url or a valueset.",
                (HttpStatusCode)422); // Unprocessable entity

        try
        {
            valueSet = valueSet is null
                ? await getExpandedValueSet(validateCodeParams.Url!, validateCodeParams.ValueSetVersion,
                    "validate code").ConfigureAwait(false)
                : await getExpandedValueSet(valueSet, "validate code").ConfigureAwait(false);

            if (validateCodeParams.CodeableConcept is not null)
                return await validateCodeVs(valueSet, validateCodeParams.CodeableConcept,
                    validateCodeParams.Abstract?.Value).ConfigureAwait(false);
            else if (validateCodeParams.Coding is { })
                return await validateCodeVs(valueSet, validateCodeParams.Coding, validateCodeParams.Abstract?.Value)
                    .ConfigureAwait(false);
            else
                return await validateCodeVs(valueSet, validateCodeParams.Code?.Value, validateCodeParams.System?.Value,
                    validateCodeParams.Display?.Value, validateCodeParams.Abstract?.Value).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            //500 internal server error
            throw new FhirOperationException(e.Message, (HttpStatusCode)500);
        }
    }

    ///<inheritdoc />
    public async Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
     	parameters.NoDuplicates();
     	
        var url = parameters.GetSingleValue<FhirUri>("url")?.Value ??
                  parameters.GetSingleValue<FhirString>("url")?.Value;
        var valueSet = parameters.GetSingle("valueSet")?.Resource as ValueSet;

        if (valueSet is null && url is null)
            throw new FhirOperationException("Have to supply either a canonical url or a valueset.",
                (HttpStatusCode)422); // Unprocessable entity

        var version = parameters.GetSingleValue<FhirString>("valueSetVersion");

        try
        {
            return valueSet is null
                ? await getExpandedValueSet(new FhirUri(url!), version, "expand").ConfigureAwait(false)
                : await getExpandedValueSet(valueSet, "expand").ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            //500 internal server error
            throw new FhirOperationException(e.Message, (HttpStatusCode)500);
        }
    }

    #region Not implemented methods

    ///<inheritdoc />
    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        // make this method async, when implementing
        throw new NotImplementedException();
    }

    ///<inheritdoc />
    public Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
    {
        // make this method async, when implementing
        throw new NotImplementedException();
    }

    ///<inheritdoc />
    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        // make this method async, when implementing
        throw new NotImplementedException();
    }

    ///<inheritdoc />
    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        // make this method async, when implementing
        throw new NotImplementedException();
    }

    ///<inheritdoc />
    public Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        // make this method async, when implementing
        throw new NotImplementedException();
    }

    #endregion

    private async Task<Parameters> validateCodeVs(ValueSet vs, CodeableConcept cc, bool? abstractAllowed)
    {
        var result = new Parameters();

        // Maybe just a text, but if there are no codings, that's a positive result
        if (!cc.Coding.Any())
        {
            result.Add("result", new FhirBoolean(true));
            return result;
        }

        // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
        if (cc.Coding.Count == 1)
            return await validateCodeVs(vs, cc.Coding.Single(), abstractAllowed).ConfigureAwait(false);

        // Else, look for one succesful match in any of the codes in the CodeableConcept
        var callResults = await Tasks.Task
            .WhenAll(cc.Coding.Select(coding => validateCodeVs(vs, coding, abstractAllowed))).ConfigureAwait(false);
        var anySuccesful = callResults.Any(p => p.GetSingleValue<FhirBoolean>("result")?.Value == true);

        if (anySuccesful == false)
        {
            var messages = new StringBuilder();
            messages.AppendLine(
                "None of the Codings in the CodeableConcept were valid for the binding. Details follow.");

            // gathering the messages of all calls
            foreach (var msg in callResults.Select(cr => cr.GetSingleValue<FhirString>("message")?.Value)
                         .Where(m => m is { }))
            {
                messages.AppendLine(msg);
            }

            result.Add("message", new FhirString(messages.ToString()));
            result.Add("result", new FhirBoolean(false));
        }
        else
        {
            result.Add("result", new FhirBoolean(true));
        }

        return result;
    }

    private async Task<Parameters> validateCodeVs(ValueSet vs, Coding coding, bool? abstractAllowed)
    {
        return await validateCodeVs(vs, coding.Code, coding.System, coding.Display, abstractAllowed)
            .ConfigureAwait(false);
    }

    private async Task<Parameters> validateCodeVs(ValueSet vs, string? code, string? system, string? display,
        bool? abstractAllowed)
    {
        if (code is null)
        {
            var resultParam = new Parameters();
            resultParam.Add("message", new FhirString("No code supplied."));
            resultParam.Add("result", new FhirBoolean(false));
            return resultParam;
        }

        var component = vs.FindInExpansion(code, system);
        var codeLabel = $"Code '{code}'"
            + (string.IsNullOrEmpty(display) ? string.Empty : $" (display '{display}')") 
            + (string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'");
        var result = new Parameters();
        var success = true;
        var messages = new StringBuilder();

        if (component is null)
        {
            await messageForCodeNotFound(vs, system, codeLabel, messages).ConfigureAwait(false);
            success = false;
        }
        else
        {
            if (component.Abstract == true && abstractAllowed == false) // will be ignored if abstractAllowed == null
            {
                messages.AppendLine($"{codeLabel} is abstract, which is not allowed here");
                success = false;
            }

            if (display != null && component.Display != null && display != component.Display)
            {
                // this is only a warning (so success is still true)
                messages.AppendLine($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'");
            }

            var displ = component.Display ?? display;
            if (displ is { })
                result.Add("display", new FhirString(displ));
        }

        result.Add("result", new FhirBoolean(success));
        if (messages.Length > 0)
            result.Add("message", new FhirString(messages.ToString().TrimEnd()));
        return result;
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