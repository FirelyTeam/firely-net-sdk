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
            var validParams = new ExpandParameters(parameters);
            TerminologyValidationHelpers.ValidateExpandParameters(validParams.Url, validParams.ValueSet, validParams.Context, validParams.ContextDirection, validParams.Offset, validParams.Count);
            return await Expand(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    protected virtual async T.Task<Resource> Expand(ExpandParameters parameters)
    {
        // Context parameter is not supported
        if (parameters.Context != null)
            throw FhirOperationException.NotSupported("The 'context' parameter is not supported.");

        var expander = new ValueSetExpander(CreateExpanderSettings(parameters));
        
        var vs = parameters.ValueSet as ValueSet 
                 ?? await ResolveValueSet(new($"{parameters.Url!}|{parameters.ValueSetVersion?.Value}")).ConfigureAwait(false);

        if (vs is null)
            throw FhirOperationException.Unresolvable("Unable to resolve ValueSet.");

        // do not regenerate expansion if already present
        if (!vs.HasExpansion)
        {
            await expander.ExpandAsync(vs).ConfigureAwait(false);
        }
        
        return vs;
    }
}