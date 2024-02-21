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
using System.ComponentModel.DataAnnotations;
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
        private readonly FhirJsonPocoDeserializerSettings _jsonSettings;
        private readonly FhirXmlPocoDeserializerSettings _xmlSettings;

        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        /// <param name="ignoreFilter">Predicate specifying which exceptions to ignore</param>
        /// <param name="jsonSettings">Settings for json deserializing</param>
        /// <param name="xmlSettings">Settings for xml deserializing</param>
        public PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException>? ignoreFilter=null, FhirJsonPocoDeserializerSettings? jsonSettings=null, FhirXmlPocoDeserializerSettings? xmlSettings=null)
        {
            _inspector = inspector;
            _ignoreFilter = ignoreFilter ?? (_ => false);
            _jsonSettings = jsonSettings ?? new FhirJsonPocoDeserializerSettings();
            _xmlSettings = xmlSettings ?? new FhirXmlPocoDeserializerSettings();
        }

        /// <inheritdoc />
        public Resource DeserializeFromXml(string data)
        {
            var deserializer = new BaseFhirXmlPocoDeserializer(_inspector, _xmlSettings);
            return deserializeAndIgnoreErrors(deserializer.TryDeserializeResource, data);
        }

        /// <inheritdoc />
        public Resource DeserializeFromJson(string data)
        {
            var deserializer = new BaseFhirJsonPocoDeserializer(_inspector, _jsonSettings);
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