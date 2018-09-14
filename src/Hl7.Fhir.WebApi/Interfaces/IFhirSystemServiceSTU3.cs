/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.WebApi
{
    /// <summary>
    /// Implementations of this interface cover the system scope of the FHIR Server
    /// </summary>
    public interface IFhirSystemServiceSTU3
    {
        /// <summary>
        /// Retreive the CapabilityStatement resource applicable for this server
        /// (was Conformance in DSTU2)
        /// </summary>
        /// <returns></returns>
        CapabilityStatement GetConformance(ModelBaseInputs request, Rest.SummaryType summary);

        /// <summary>
        /// Retrieve a ResourceService processor for the provided resource type
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        IFhirResourceServiceSTU3 GetResourceService(ModelBaseInputs request, string resourceName);

        /// <summary>
        /// Process the bundle passed in (could be a batch or a transaction)
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns>the matching bundle with the results of the request</returns>
        Bundle ProcessBatch(ModelBaseInputs request, Bundle bundle);

        /// <summary>
        /// Retreive the system history for the request
        /// </summary>
        /// <param name="since"></param>
        /// <param name="Till"></param>
        /// <param name="Count"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        Bundle SystemHistory(ModelBaseInputs request, DateTimeOffset? since, DateTimeOffset? Till, int? Count, Rest.SummaryType summary);

        /// <summary>
        /// Perform the FHIR operation on the whole system (not a resource type/instance specific operation)
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="operationParameters"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        Resource PerformOperation(ModelBaseInputs request, string operation, Parameters operationParameters, Rest.SummaryType summary);

        /// <summary>
        /// Search the entire server (not a resource type/instance specific operation)
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="Count"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        Bundle Search(ModelBaseInputs request, IEnumerable<KeyValuePair<string, string>> parameters, int? Count, Rest.SummaryType summary);
    }
}
