/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using System;
using System.Linq;
using System.Net;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

public partial class BaseTerminologyService
{
    /// <summary>
    /// Class to wrap the implemented ResolveValueSet into IAsyncResourceResolver
    /// </summary>
    /// <param name="term"></param>
    private class ValueSetExpanderProvider(BaseTerminologyService term) : IAsyncResourceResolver
    {
        // ValueSetExpander only uses TryResolveByCanonicalUriAsync so other things can be skipped
        public T.Task<Resource?> ResolveByUriAsync(string uri) => throw new NotImplementedException();
        public T.Task<Resource?> ResolveByCanonicalUriAsync(string uri) => throw new NotImplementedException();
        
        public async T.Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri)
        {
            return await term.ResolveValueSet(new(uri))
                   ?? new ResolverResult(ResolverException.NotFound());
        }
    }


    protected virtual ValueSetExpanderSettings CreateExpanderSettings(ExpandParameters parameters)
    {
        return new()
        {
            IncludeDesignations = parameters.IncludeDesignations?.Value ?? false, ValueSetSource = new ValueSetExpanderProvider(this)
        };
    }
    
    async T.Task<Resource> IExpandingTerminologyService.Expand(Parameters parameters, string? id, bool useGet)
    {
        try
        {
            var validParams = new ExpandParameters(parameters.NoDuplicates(ExpandParameters.KnownNoDuplicates));
            TerminologyValidationHelpers.ValidateExpandParameters(validParams.Url, validParams.ValueSet, validParams.Context, validParams.ContextDirection, validParams.Offset, validParams.Count);
            return await Expand(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// The request's parameters that are in <see cref="ValueSetExpander.SupportedParameters"/>. The others are ignored, so
    /// the terminology services stay lenient about parameters they do not implement.
    /// </summary>
    private protected static ExpandParameters SupportedExpandParameters(ExpandParameters parameters)
    {
        var supported = new ExpandParameters();
        supported.Parameter.AddRange(parameters.Parameter.Where(p => ValueSetExpander.SupportedParameters.Contains(p.Name)));
        return supported;
    }

    /// <summary>
    /// Whether the request asks for an expansion that differs from the default one (flat, or without not-for-UI codes).
    /// </summary>
    private protected static bool AsksForDifferentShape(ExpandParameters parameters) =>
        parameters.ExcludeNested?.Value == true || parameters.ExcludeNotForUI?.Value == true;

    /// <summary>Whether the value set has an expansion with nested codes.</summary>
    private protected static bool IsNested(ValueSet valueSet) =>
        valueSet.Expansion?.Contains.Any(c => c.Contains.Any()) == true;

    /// <summary>
    /// Expands a copy of the value set in the shape the request asks for, so that shape is not stored in the resolved
    /// value set and reused for later requests.
    /// </summary>
    /// <remarks>
    /// A value set with a <c>compose</c> is always expanded again, so the answer does not depend on an expansion stored by
    /// an earlier request (which, for example, does not mark enumerated codes as abstract). Only when that fails, or when
    /// there is no <c>compose</c>, is the requested shape applied to a copy of the stored expansion.
    /// </remarks>
    private protected static async T.Task<ValueSet> ExpandCopy(ValueSetExpander expander, ValueSet valueSet, ExpandParameters supported)
    {
        var copy = (ValueSet)valueSet.DeepCopy();

        if (copy.Compose is not null)
        {
            try
            {
                await expander.ExpandAsync(copy, supported).ConfigureAwait(false);
                return copy;
            }
            catch (TerminologyServiceException) when (valueSet.HasExpansion)
            {
                // The stored expansion was made elsewhere (e.g. it came with the value set): use that one.
                copy = (ValueSet)valueSet.DeepCopy();
            }
        }

        if (copy.HasExpansion)
            expander.ApplyToStoredExpansion(copy, supported);
        else
            await expander.ExpandAsync(copy, supported).ConfigureAwait(false);

        return copy;
    }

    protected virtual async T.Task<Resource> Expand(ExpandParameters parameters)
    {
        // Context parameter is not supported
        if (parameters.Context != null)
            throw FhirOperationException.NotSupported("The 'context' parameter is not supported.");

        var expander = new ValueSetExpander(CreateExpanderSettings(parameters));

        var canonical = $"{parameters.Url!}|{parameters.ValueSetVersion?.Value}";
        var vs = parameters.ValueSet as ValueSet 
                 ?? await ResolveValueSet(new(canonical)).ConfigureAwait(false);

        if (vs is null)
            throw FhirOperationException.Unresolvable($"Unable to resolve ValueSet '{canonical}'.");

        // Unsupported parameters are ignored, as before. A flat or not-for-UI expansion is made on a copy.
        var supported = SupportedExpandParameters(parameters);
        if (AsksForDifferentShape(supported) || (expander.Settings.AlwaysExpandFlat && IsNested(vs)))
            return await ExpandCopy(expander, vs, supported).ConfigureAwait(false);

        // do not regenerate expansion if already present
        if (!vs.HasExpansion)
        {
            await expander.ExpandAsync(vs).ConfigureAwait(false);
        }
        
        return vs;
    }
}