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
    /// A <see cref="ITerminologyService"/> together with the corresponding options, used for ordening.
    /// </summary>
    /// <param name="Settings">Settings needed for ordening.</param>
    /// <param name="Service">A <see cref="ITerminologyService"/>.</param>
    public record OrderableTerminologyService(ITerminologyService Service, TerminologyServiceSettings Settings)
    {

    }

    /// <summary>
    /// Aggregation of multiple terminology services. 
    /// </summary>
    public class MultiTerminologyService : ITerminologyService
    {
        private readonly List<OrderableTerminologyService> _termServices = new();

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Terminology services to be used. Note that the first service in the list will be used first, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(IEnumerable<ITerminologyService> services) : this(services.Select(s => new OrderableTerminologyService(s, TerminologyServiceSettings.CreateDefault())))
        {

        }

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Orderable terminology services to be used. You can set the order of the services to be used using the settings, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(IEnumerable<OrderableTerminologyService> services)
        {
            if (services == null) throw Error.ArgumentNull(nameof(services));
            _termServices = services.OrderBy(s => s.Settings.Order).ToList();
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

            // check if any preferedServices
            var preferred = preferredService(parameters);
            var orderedTermServices = preferred.Any() ?
                                        reorderList(_termServices.Select(t => t.Service), preferred)
                                        : _termServices.Select(t => t.Service);

            // then check other services
            foreach (var termService in orderedTermServices)
            {
                try
                {
                    return await termService.ValueSetValidateCode(parameters, id, useGet).ConfigureAwait(false);
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

        private IEnumerable<ITerminologyService> preferredService(Parameters parameters)
        {
            var valueSetUrl = new ValidateCodeParameters(parameters).Url.Value;
            return _termServices.Where(t => matchSystem(t.Settings.PreferredValueSets, valueSetUrl)).Select(t => t.Service);

            static bool matchSystem(IEnumerable<string> preferredSystems, string valueSetUrl) =>
                !preferredSystems.Any() || preferredSystems.Any(valueSetUrl.StartsWith);
        }

        private static IEnumerable<T> reorderList<T>(IEnumerable<T> originalList, IEnumerable<T> itemsToMove)
        {
            return itemsToMove.Concat(originalList.Except(itemsToMove));
        }
    }
}

#nullable restore
