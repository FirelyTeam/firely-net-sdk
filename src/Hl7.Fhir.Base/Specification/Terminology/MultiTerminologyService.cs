#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Aggregation of multiple terminology services. 
    /// </summary>
    public class MultiTerminologyService : ITerminologyService
    {
        private readonly List<ITerminologyService> _services = new();

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Terminology services to be used. Note that the first service in the list will be used first, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(IEnumerable<ITerminologyService> services)
        {
            if (services == null) throw Error.ArgumentNull(nameof(services));
            _services.AddRange(services);
        }

        public MultiTerminologyService(params ITerminologyService[] services) : this((IEnumerable<ITerminologyService>)services) { }

        ///<inheritdoc/>
        public Task<Resource> Closure(Parameters parameters, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public Task<Parameters> Lookup(Parameters parameters, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false) => throw new NotImplementedException();

        ///<inheritdoc/>
        public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
        {
            List<FhirOperationException> exceptions = new();

            foreach (var service in _services)
            {
                try
                {
                    return await service.ValueSetValidateCode(parameters, id, useGet).ConfigureAwait(false);
                }
                catch (FhirOperationException ex)
                {
                    exceptions.Add(ex);
                }
            }

            //Exceptions were thrown by all of the terminology services.
            if (exceptions.Count > 1)
            {
                var aggregate = new AggregateException(exceptions);
                //We pick to display the first exception, because we have to choose something. If the used terminology service is implemented correctly, this shouldn't matter.
                throw new FhirOperationException(exceptions.First().Message, exceptions.First().Status, aggregate);
            }
            else if (exceptions.Count == 1)
            {
                throw exceptions.Single();
            }

            throw new InvalidOperationException("We should never have come here");
        }
    }
}

#nullable restore
