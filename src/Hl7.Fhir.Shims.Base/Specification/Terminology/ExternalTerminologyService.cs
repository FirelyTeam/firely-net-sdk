/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// An implementation of <see cref="ITerminologyService"/> that contacts an external terminology service using the <see cref="BaseFhirClient"/>.
/// </summary>
public class ExternalTerminologyService : ITerminologyService
{
    /// <summary>
    /// Construct an instance that uses the given client.
    /// </summary>
    /// <param name="client"></param>
    public ExternalTerminologyService(BaseFhirClient client)
    {
        Endpoint = client;
    }

    /// <summary>
    /// The external service to reach out to.
    /// </summary>
    public BaseFhirClient Endpoint { get; set; }

    ///<inheritdoc />
    public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return string.IsNullOrEmpty(id)
            ? assertIs<Parameters>(await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false))
            : assertIs<Parameters>(await Endpoint.InstanceOperationAsync(constructUri(FhirTypeNames.VALUESET_NAME,id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false));
    }

    private static T assertIs<T>(object? result)
    {
        if (result is T t)
            return t;

        throw new InvalidOperationException($"Expected result of type {typeof(T).Name}, but got {result?.GetType().Name}");
    }

    ///<inheritdoc />
    public async Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return string.IsNullOrEmpty(id)
            ? assertIs<Parameters>(await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false))
            : assertIs<Parameters>(await Endpoint.InstanceOperationAsync(constructUri(FhirTypeNames.CODESYSTEM_NAME, id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false));
    }

    private static Uri constructUri(string resourceName, string id) =>
        ResourceIdentity.Build(resourceName, id);

    ///<inheritdoc />
    public async Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
        return string.IsNullOrEmpty(id)
            ? assertIs<Resource>(
                await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, parameters, useGet)
                    .ConfigureAwait(false))
            : assertIs<Resource>(await Endpoint.InstanceOperationAsync(constructUri(FhirTypeNames.VALUESET_NAME, id),
                RestOperation.EXPAND_VALUESET, parameters, useGet).ConfigureAwait(false));
    }

    ///<inheritdoc />
    public async Task<Parameters> Lookup(Parameters parameters, bool useGet = false) =>
        assertIs<Parameters>(await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet).ConfigureAwait(false));

    ///<inheritdoc />
    public async Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        return string.IsNullOrEmpty(id)
            ? assertIs<Parameters>(await Endpoint
                .TypeOperationAsync(RestOperation.TRANSLATE, FhirTypeNames.CONCEPTMAP_NAME, parameters, useGet)
                .ConfigureAwait(false))
            : assertIs<Parameters>(await Endpoint.InstanceOperationAsync(
                    ResourceIdentity.Build(FhirTypeNames.CONCEPTMAP_NAME, id),
                    RestOperation.TRANSLATE, parameters, useGet)
                .ConfigureAwait(false));
    }

    ///<inheritdoc />
    public async Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        return string.IsNullOrEmpty(id)
            ? assertIs<Parameters>(await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false))
            : assertIs<Parameters>(await Endpoint.InstanceOperationAsync(constructUri(FhirTypeNames.CODESYSTEM_NAME,id), RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false));
    }

    /// <inheritdoc />
    public async Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        return assertIs<Resource>(await Endpoint.WholeSystemOperationAsync(RestOperation.CLOSURE, parameters, useGet).ConfigureAwait(false));
    }
}