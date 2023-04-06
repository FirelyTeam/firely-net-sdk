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
using System.Text.Json;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
    /// new Poco-based parser and serializer. It is used as a switchable serialization
    /// in the <c>BaseFhirClient</c>.
    /// </summary>
    internal class PocoSerializationEngine : IFhirSerializationEngine
    {
        private readonly JsonSerializerOptions _options;
        private readonly ModelInspector _inspector;

        public PocoSerializationEngine(ModelInspector inspector)
        {
            _options = new JsonSerializerOptions().ForFhir(inspector);
            _inspector = inspector;
        }

        public Resource DeserializeFromXml(string data) => new BaseFhirXmlPocoDeserializer(_inspector).DeserializeResource(data);

        public Resource DeserializeFromJson(string data) => JsonSerializer.Deserialize<Resource>(data, _options)!;

        public string SerializeToXml(Resource instance) => new BaseFhirXmlPocoSerializer(_inspector.FhirRelease).SerializeToString(instance);

        public string SerializeToJson(Resource instance) => JsonSerializer.Serialize(instance, _options);
    }
}

#nullable restore