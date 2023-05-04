/*
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
    /// new Poco-based parser and serializer, initialized with the default settings.
    /// </summary>
    internal class PocoSerializationEngine : IFhirSerializationEngine
    {
        private delegate bool TryDeserializer(string data, out Resource? instance, out IEnumerable<CodedException> issues);

        private readonly ModelInspector _inspector;
        private readonly Predicate<CodedException> _ignoreFilter;
      
        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        /// <param name="ignoreFilter">A predicate that returns true for issues that should not be reported.</param>
        public PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException> ignoreFilter)
        {
            _inspector = inspector;
            _ignoreFilter = ignoreFilter;
        }

        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        public PocoSerializationEngine(ModelInspector inspector)
        {
            _inspector = inspector;
            _ignoreFilter = _ => false;
        }

        /// <inheritdoc />
        public Resource DeserializeFromXml(string data)
        {
            var deserializer = new BaseFhirXmlPocoDeserializer(_inspector);
            return deserializeAndIgnoreErrors(deserializer.TryDeserializeResource, data);
        }

        /// <inheritdoc />
        public Resource DeserializeFromJson(string data)
        {
            var deserializer = new BaseFhirJsonPocoDeserializer(_inspector);
            return deserializeAndIgnoreErrors(deserializer.TryDeserializeResource, data);
        }

        private Resource deserializeAndIgnoreErrors(TryDeserializer deserializer, string data)
        {
            bool success = deserializer(data, out var instance, out var issues);
            var relevantIssues = issues.Where(i => !_ignoreFilter(i)).ToList();

            return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
        }

        public string SerializeToXml(Resource instance) => new BaseFhirXmlPocoSerializer(_inspector.FhirRelease).SerializeToString(instance);

        public string SerializeToJson(Resource instance) => new BaseFhirJsonPocoSerializer(_inspector.FhirRelease).SerializeToString(instance);
    }
}

#nullable restore