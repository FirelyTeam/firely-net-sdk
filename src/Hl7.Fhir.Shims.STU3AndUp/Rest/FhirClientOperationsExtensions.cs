/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class FhirClientOperationsExtensions
    {
        #region Meta
        //[base]/Resource/$meta
        public static async Task<Meta> MetaAsync(this BaseFhirClient client, ResourceType type, CancellationToken? ct = null)
        {
            return FhirClientOperations.ExtractMeta(FhirClientOperations.OperationResult<Parameters>(
                await client.TypeOperationAsync(RestOperation.META, type.GetLiteral(), useGet: true, ct:ct).ConfigureAwait(false)));
        }

        [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
        public static Meta Meta(this BaseFhirClient client, ResourceType type)
        {
            return MetaAsync(client, type).WaitResult();
        }
        #endregion

        #region Conformance

        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public static Task<CapabilityStatement> CapabilityStatementAsync(this BaseFhirClient client, SummaryType? summary = null, CancellationToken? ct = null)
        {
            var tx = new TransactionBuilder(client.Endpoint).CapabilityStatement(summary).ToBundle();
            return client.executeAsync<CapabilityStatement>(tx, HttpStatusCode.OK, ct);
        }

        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
        public static CapabilityStatement CapabilityStatement(this BaseFhirClient client, SummaryType? summary = null)
        {
            return client.CapabilityStatementAsync(summary).WaitResult();
        }
        #endregion
    }
}
