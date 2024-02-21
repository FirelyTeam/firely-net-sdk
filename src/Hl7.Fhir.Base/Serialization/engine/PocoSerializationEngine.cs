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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
    /// new Poco-based parser and serializer, initialized with the default settings.
    /// </summary>
    internal class PocoSerializationEngine : IFhirSerializationEngine, IFhirStreamingSerializationEngine
    {
        private delegate (Resource?, IEnumerable<CodedException>) TryDeserializer();

        private readonly ModelInspector _inspector;
        private readonly Predicate<CodedException> _ignoreFilter;
        private readonly FhirJsonPocoDeserializerSettings _jsonDeserialzerSettings;
        private readonly FhirJsonPocoSerializerSettings _jsonSerializerSettings;
        private readonly FhirXmlPocoDeserializerSettings _xmlSettings;
        
        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        /// <param name="ignoreFilter">A predicate that returns true for issues that should not be reported.</param>
        public PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException> ignoreFilter) : this(inspector, ignoreFilter, null)
        {
            //TODO maybe we can remove this?
        }

        public PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException>? ignoreFilter = null,
            FhirJsonPocoDeserializerSettings? jsonDeserializerSettings = null,
            FhirJsonPocoSerializerSettings? jsonSerializerSettings = null,
            FhirXmlPocoDeserializerSettings? xmlSettings = null)
        {
            _inspector = inspector;
            _ignoreFilter = ignoreFilter ?? (_ => false);
            _jsonDeserialzerSettings = jsonDeserializerSettings ?? new();
            _jsonSerializerSettings = jsonSerializerSettings ?? new();
            _xmlSettings = xmlSettings ?? new();
        }


        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        public PocoSerializationEngine(ModelInspector inspector) : this(inspector, null, null)
        {
            // TODO maybe we can remove this?
        }

        /// <inheritdoc />
        public Resource DeserializeFromXml(string data)
        {
            var deserializer = new BaseFhirXmlPocoDeserializer(_inspector, _xmlSettings);
            return deserializeAndIgnoreErrors(() =>
            { 
                _ = deserializer.TryDeserializeResource(data, out var instance, out var issues);
                return (instance, issues);
            });
        }

        /// <inheritdoc />
        public Resource DeserializeFromXml(XmlReader reader)
        {
            var deserializer = new BaseFhirXmlPocoDeserializer(_inspector, _xmlSettings);
            return deserializeAndIgnoreErrors(() =>
            { 
                _ = deserializer.TryDeserializeResource(reader, out var instance, out var issues);
                return (instance, issues);
            });
        }

        /// <inheritdoc />
        public Resource DeserializeFromJson(string data)
        {
            var deserializer = new BaseFhirJsonPocoDeserializer(_inspector, _jsonDeserialzerSettings);
            return deserializeAndIgnoreErrors(() =>
            { 
                _ = deserializer.TryDeserializeResource(data, out var instance, out var issues);
                return (instance, issues);
            });
        }
        
        /// <inheritdoc />
        public Resource DeserializeFromJson(Utf8JsonReader reader)
        {
            var deserializer = new BaseFhirJsonPocoDeserializer(_inspector, _jsonDeserialzerSettings);
            return deserializeAndIgnoreErrors(deserializer, reader);
        }

        private Resource deserializeAndIgnoreErrors(TryDeserializer deserializer)
        {
            var (instance, issues) = deserializer();
            var relevantIssues = issues.Where(i => !_ignoreFilter(i)).ToList();

            return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
        }
        
        // overload necessary since ref structs cannot be captured in the delegate
        private Resource deserializeAndIgnoreErrors(BaseFhirJsonPocoDeserializer deserializer, Utf8JsonReader reader)
        {
            _ = deserializer.TryDeserializeResource(reader, out var instance, out var issues);
            var relevantIssues = issues.Where(i => !_ignoreFilter(i)).ToList();

            return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
        }

        public string SerializeToXml(Resource instance) => new BaseFhirXmlPocoSerializer(_inspector.FhirRelease).SerializeToString(instance);

        public string SerializeToJson(Resource instance) => new BaseFhirJsonPocoSerializer(_inspector.FhirRelease, _jsonSerializerSettings).SerializeToString(instance);
    }
}

#nullable restore