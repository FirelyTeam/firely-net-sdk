/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Buffers;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A converter factory to construct FhirJsonConverters for subclasses of <see cref="Base"/>.
    /// </summary>
    public class FhirJsonConverterFactory : JsonConverterFactory
    {
        private readonly Assembly _assembly;
        private readonly FhirJsonPocoSerializerSettings _serializerSettings;
        private readonly FhirJsonPocoDeserializerSettings _deserializerSettings;

        public FhirJsonConverterFactory(
            Assembly assembly,
            FhirJsonPocoSerializerSettings serializerSettings,
            FhirJsonPocoDeserializerSettings deserializerSettings)
        {
            _assembly = assembly;
            _serializerSettings = serializerSettings;
            _deserializerSettings = deserializerSettings;
        }

        public override bool CanConvert(Type typeToConvert) => typeof(Base).IsAssignableFrom(typeToConvert);

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(FhirJsonConverter<>).MakeGenericType(typeToConvert),
                new object[] { _assembly, _serializerSettings, _deserializerSettings })!;
        }
    }


    /// <summary>
    /// FHIR Resource and datatype converter for FHIR deserialization.
    /// </summary>
    public class FhirJsonConverter<F> : JsonConverter<F> where F : Base
    {
        /// <summary>
        /// Constructs a <see cref="JsonConverter{T}"/> that (de)serializes FHIR json for the 
        /// POCOs in a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing classes to be used for deserialization.</param>
        /// <param name="serializerSettings">The optional features used during serialization.</param>
        /// <param name="deserializerSettings">The optional features used during deserialization.</param>
        public FhirJsonConverter(
            Assembly assembly,
            FhirJsonPocoSerializerSettings serializerSettings,
            FhirJsonPocoDeserializerSettings deserializerSettings)
        {
            ModelInspector inspector = ModelInspector.ForAssembly(assembly);

            _deserializer = new FhirJsonPocoDeserializer(assembly, deserializerSettings);
            _serializer = new FhirJsonPocoSerializer(inspector.FhirRelease, serializerSettings);
        }

        /// <summary>
        /// Constructs a <see cref="JsonConverter{T}"/> that (de)serializes FHIR json for the 
        /// POCOs in a given assembly.
        /// </summary>
        /// <param name="deserializer">A custom deserializer to be used by the json converter.</param>
        /// <param name="serializer">A customer serializer to be used by the json converter.</param>
        /// <remarks>Since the standard serializer/deserializer will allow you to override its behaviour to produce
        /// custom behaviour, this constructor will allow the developer to use such custom serializers/deserializers instead
        /// of the defaults.</remarks>
        public FhirJsonConverter(FhirJsonPocoDeserializer deserializer, FhirJsonPocoSerializer serializer)
        {
            _deserializer = deserializer;
            _serializer = serializer;
        }

        /// <summary>
        /// Determines whether the specified type can be converted.
        /// </summary>
        public override bool CanConvert(Type objectType) => typeof(F) == objectType;

        private readonly FhirJsonPocoDeserializer _deserializer;
        private readonly FhirJsonPocoSerializer _serializer;

        /// <summary>
        /// The filter used to serialize a summary of the resource.
        /// </summary>
        public SerializationFilter? SerializationFilter { get; }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, F poco, JsonSerializerOptions options)
        {
            _serializer.Serialize(poco, writer);
        }

        /// <summary>
        /// Reads and converts the JSON to a typed object.
        /// </summary>
        public override F Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return typeof(Resource).IsAssignableFrom(typeToConvert)
                ? (F)(Base)_deserializer.DeserializeResource(ref reader)
                : (F)_deserializer.DeserializeObject(typeToConvert, ref reader);
        }
    }
}

#endif
#nullable restore