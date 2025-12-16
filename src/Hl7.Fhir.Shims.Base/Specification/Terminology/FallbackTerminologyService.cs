/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// An implementation of <see cref="ITerminologyService"/> that first tries an operation on a given <see cref="LocalTerminologyService"/>,
/// and if that fails, calls out to a fallback service.
/// </summary>
/// <remarks>The <see cref="MultiTerminologyService"/> now offers a more flexible way to achieve this result, with more fallback options.</remarks>
public class FallbackTerminologyService : ITerminologyService
{
    private readonly LocalTerminologyService _localService;
    private readonly ITerminologyService _fallbackService;

    /// <summary>
    /// Construct an instance, given a local terminology service, and a fallback service.
    /// </summary>
    /// <param name="local"></param>
    /// <param name="fallback"></param>
    public FallbackTerminologyService(LocalTerminologyService local, ITerminologyService fallback)
    {
        _localService = local;
        _fallbackService = fallback;
    }

    private async Task<T> tryFallback<T>(Func<ITerminologyService, Parameters, Task<T>> operation, Parameters parameters)
    {
        try
        {
            // First, try the local service
            return await operation(_localService, parameters).ConfigureAwait(false);
        }
        catch (FhirOperationException)
        {
            // If that fails, call the fallback
            try
            {
                return await operation(_fallbackService, parameters).ConfigureAwait(false);
            }
            catch (FhirOperationException vse) when (vse.Status == System.Net.HttpStatusCode.NotFound)
            {
                // The fall back service does not know the valueset. If our local service
                // does, try get the VS from there, and retry by sending the vs inline
                var url = parameters.GetSingleValue<FhirUri>("url")?.Value;
                if (url is null) throw;

                var version = parameters.GetSingleValue<FhirString>("valueSetVersion")?.Value;
                var canonical = new Canonical(url, version);
                var valueSet = await _localService.FindValueSet(canonical).ConfigureAwait(false);
                if (valueSet == null) throw;

                var paramsWithVs = parameters.DeepCopy();
                paramsWithVs.Remove("valueSet");
                paramsWithVs.Remove("url");
                paramsWithVs.Add("valueSet", valueSet);

                return await operation(_fallbackService, paramsWithVs).ConfigureAwait(false);
            }
        }
    }

    /// <inheritdoc/>
    public Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false) =>
        tryFallback((s, p) => s.ValueSetValidateCode(p, id, useGet), parameters);

    /// <inheritdoc/>
    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false) =>
        tryFallback((s, p) => s.CodeSystemValidateCode(p, id, useGet), parameters);

    /// <inheritdoc/>
    public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false) =>
        tryFallback((s, p) => s.Expand(p, id, useGet), parameters);

    /// <inheritdoc/>
    public Task<Parameters> Lookup(Parameters parameters, bool useGet = false) =>
        tryFallback((s, p) => s.Lookup(p, useGet), parameters);

    /// <inheritdoc/>
    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false) =>
        tryFallback((s, p) => s.Translate(p, id, useGet), parameters);

    /// <inheritdoc/>
    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false) =>
        tryFallback((s, p) => s.Subsumes(p, id, useGet), parameters);

    /// <inheritdoc/>
    public Task<Resource> Closure(Parameters parameters, bool useGet = false) =>
        tryFallback((s, p) => s.Closure(p, useGet), parameters);
}