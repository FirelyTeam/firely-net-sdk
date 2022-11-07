/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class FhirClientOperationsExtensions
    {
        #region Meta
        //[base]/Resource/$meta
        public static async Task<Meta> MetaAsync(this BaseFhirClient client, ResourceType type)
        {
            return FhirClientOperations.ExtractMeta(FhirClientOperations.OperationResult<Parameters>(await client.TypeOperationAsync(RestOperation.META, type.GetLiteral(), useGet: true).ConfigureAwait(false)));
        }
        public static Meta Meta(this BaseFhirClient client, ResourceType type)
        {
            return MetaAsync(client, type).WaitResult();
        }
        #endregion
    }
}
