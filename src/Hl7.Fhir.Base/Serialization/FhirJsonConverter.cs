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
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A converter factory to construct FhirJsonConverters for subclasses of <see cref="Base"/>.
    /// </summary>
    public class FhirJsonConverterFactory : JsonConverterFactory
    {
        private readonly ModelInspector _inspector;
        internal Predicate<CodedException> IgnoreFilter;
        private readonly FhirJsonPocoSerializerSettings? _serializerSettings;
        private readonly FhirJsonPocoDeserializerSettings? _deserializerSettings;

        public FhirJsonConverterFactory(
            Assembly assembly,
            FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null,
            Predicate<CodedException>? ignoreFilter = null) : this(ModelInspector.ForAssembly(assembly), serializerSettings, deserializerSettings, ignoreFilter)
        {
            // Nothing
        }

        public FhirJsonConverterFactory(
            ModelInspector inspector,
            FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null,
            Predicate<CodedException>? ignoreFilter = null)
        {
            _inspector = inspector;
            _serializerSettings = serializerSettings;
            _deserializerSettings = deserializerSettings;
            IgnoreFilter = ignoreFilter ?? (_ => false);
        }

        public override bool CanConvert(Type typeToConvert) => typeof(Base).IsAssignableFrom(typeToConvert);

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter?)Activator.CreateInstance(
                typeof(FhirJsonConverter<>).MakeGenericType(typeToConvert),
                [ _inspector, _serializerSettings, _deserializerSettings, IgnoreFilter ]);
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
        /// <param name="serializerSettings">The settings to be used during serialization.</param>
        /// <param name="deserializerSettings">The settings to be used during deserialization.</param>
        /// <param name="ignoreFilter">A predicate specifying which errors to ignore when parsing</param>
        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(
            Assembly assembly,
            FhirJsonPocoSerializerSettings? serializerSettings,
            FhirJsonPocoDeserializerSettings? deserializerSettings,
            Predicate<CodedException>? ignoreFilter = null)
        {
            var inspector = ModelInspector.ForAssembly(assembly);
            _engine = FhirSerializationEngineFactory.Custom(inspector, ignoreFilter ?? (_ => false), deserializerSettings, serializerSettings);
        }

        /// <summary>
        /// Constructs a <see cref="JsonConverter{T}"/> that (de)serializes FHIR json for the 
        /// POCOs in a given assembly.
        /// </summary>
        /// <param name="inspector">The <see cref="ModelInspector" /> containing classes to be used for deserialization.</param>
        /// <param name="serializerSettings">The optional features used during serialization.</param>
        /// <param name="deserializerSettings">The optional features used during deserialization.</param>
        /// <param name="ignoreFilter">A predicate specifying which errors to ignore when parsing</param>
        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(
            ModelInspector inspector,
            FhirJsonPocoSerializerSettings? serializerSettings,
            FhirJsonPocoDeserializerSettings? deserializerSettings,
            Predicate<CodedException>? ignoreFilter = null)
        {
            _engine = FhirSerializationEngineFactory.Custom(inspector, ignoreFilter ?? (_ => false), deserializerSettings,
                serializerSettings);
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
        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(BaseFhirJsonPocoDeserializer deserializer, BaseFhirJsonPocoSerializer serializer)
        {
            _engine = FhirSerializationEngineFactory.WithCustomJsonSerializers(deserializer, serializer);
        }

        /// <summary>
        /// Determines whether the specified type can be converted.
        /// </summary>
        public override bool CanConvert(Type objectType) => typeof(F) == objectType;
        
        private readonly IFhirExtendedSerializationEngine _engine;

        /// <summary>
        /// The filter used to serialize a summary of the resource.
        /// </summary>
        public SerializationFilter? SerializationFilter { get; }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, F poco, JsonSerializerOptions options)
        {
            _engine.SerializeToJsonWriter(poco, writer);
        }

        /// <summary>
        /// Reads and converts the JSON to a typed object.
        /// </summary>
        public override F Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return typeof(Resource).IsAssignableFrom(typeToConvert)
                ? (F)(Base)_engine.DeserializeFromJson(ref reader)
                : (F)_engine.DeserializeObjectFromJson(typeToConvert, ref reader);
        }
    }
}

#endif
#nullable restore