/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
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
        public async Task<Parameters?> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id!), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters?> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id!), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        private static Uri constructUri<T>(string id) where T : Resource =>
            ResourceIdentity.Build(ModelInspector.ForType<T>().GetFhirTypeNameForType(typeof(T)), id);

        ///<inheritdoc />
        public Task<Resource?> Expand(Parameters parameters, string? id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, parameters, useGet);
            else
                return Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id!), RestOperation.EXPAND_VALUESET, parameters, useGet);
        }

        ///<inheritdoc />
        public async Task<Parameters?> Lookup(Parameters parameters, bool useGet = false)
        {
            return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters?> Translate(Parameters parameters, string? id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync(RestOperation.TRANSLATE, FhirTypeNames.CONCEPTMAP_NAME, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(
                    ResourceIdentity.Build(FhirTypeNames.CONCEPTMAP_NAME, id!),
                    RestOperation.TRANSLATE, parameters, useGet)
                    .ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters?> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id!), RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        /// <inheritdoc />
        public Task<Resource?> Closure(Parameters parameters, bool useGet = false)
        {
            return Endpoint.WholeSystemOperationAsync(RestOperation.CLOSURE, parameters, useGet);
        }
    }
}

#nullable restore