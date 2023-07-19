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
        private readonly SortedList<int, TerminologyServiceRoutingSettings> _termServices = new();

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Terminology services to be used. Note that the first service in the list will be used first, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(IEnumerable<ITerminologyService> services) : this(services.Select(s => TerminologyServiceRoutingSettings.CreateDefault(s))) { }

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="settings">Routing settings to be used. You can add settings to prefer certain terminology services, the others will still be used based as fallback based on their order.</param>
        public MultiTerminologyService(IEnumerable<TerminologyServiceRoutingSettings> settings)
        {
            if (settings == null || !settings.Any()) throw Error.ArgumentNull(nameof(settings));

            int count = 0;
            foreach (var setting in settings)
            {
                _termServices.Add(count, setting);
                count++;
            }
        }

        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Terminology services to be used. Note that the first service in the list will be used first, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(params ITerminologyService[] services) : this(services.Select(s => new TerminologyServiceRoutingSettings(s))) { }


        /// <summary>
        /// A collection of multiple terminology services to allow for one or multiple fallback services when validating codes for example.
        /// </summary>
        /// <param name="services">Orderable terminology services to be used. You can set the order of the services to be used using the settings, the others will be used for fallback based on their order.</param>
        public MultiTerminologyService(params TerminologyServiceRoutingSettings[] services) : this((IEnumerable<TerminologyServiceRoutingSettings>)services) { }


        /// <summary>
        /// Add an <see cref="ITerminologyService"/> at the back of the order of terminologyServices to be used.
        /// </summary>
        /// <param name="service"><see cref="ITerminologyService"/> to be added.</param>
        public void Add(ITerminologyService service)
        {
            var settings = new TerminologyServiceRoutingSettings(service);
            Add(settings);
        }

        /// <summary>
        /// Add an <see cref="ITerminologyService"/> including their routing settings at the back of the order of terminologyServices to be used.
        /// </summary>
        /// <param name="settings"><see cref="TerminologyServiceRoutingSettings"/> to be added.</param>
        public void Add(TerminologyServiceRoutingSettings settings)
        {
            var highest = _termServices.Keys?.Last();
            var position = highest is not null ? highest.Value + 1 : 0;
            _termServices.Add(position, settings);
        }

        /// <summary>
        /// Add an <see cref="ITerminologyService"/> at the front of the order of terminologyServices to be used.
        /// </summary>
        /// <param name="service"><see cref="ITerminologyService"/> to be added.</param>
        public void AddFirst(ITerminologyService service)
        {
            var settings = new TerminologyServiceRoutingSettings(service);
            AddFirst(settings);
        }


        /// <summary>
        /// Add an <see cref="ITerminologyService"/> including their routing settings at the front of the order of terminologyServices to be used.
        /// </summary>
        /// <param name="settings"><see cref="TerminologyServiceRoutingSettings"/> to be added.</param>
        public void AddFirst(TerminologyServiceRoutingSettings settings)
        {
            var lowest = _termServices.Keys?.First();
            var position = lowest is not null ? lowest.Value - 1 : 0;
            _termServices.Add(position, settings);
        }


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
            var services = _termServices.Values.Select(s => s.Service);
            var orderedTermServices = preferred.Any() ?
                                      reorderList(services, preferred)
                                      : services;

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
            var valueSetUrl = new ValidateCodeParameters(parameters).Url?.Value;
            return valueSetUrl != null
                ? _termServices.Values.Where(t => matchSystem(t.PreferredValueSets, valueSetUrl)).Select(t => t.Service)
                : Enumerable.Empty<ITerminologyService>();

            static bool matchSystem(IEnumerable<string>? preferredSystems, string valueSetUrl) =>
                preferredSystems?.Any() == true && preferredSystems.Any(valueSetUrl.StartsWith);
        }

        private static IEnumerable<T> reorderList<T>(IEnumerable<T> originalList, IEnumerable<T> itemsToMoveToFront)
        {
            return itemsToMoveToFront.Concat(originalList.Except(itemsToMoveToFront));
        }
    }
}

#nullable restore
