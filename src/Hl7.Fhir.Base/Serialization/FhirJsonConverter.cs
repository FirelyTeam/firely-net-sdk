/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
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
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A converter factory to construct FhirJsonConverters for subclasses of <see cref="Base"/>.
    /// </summary>
    public class FhirJsonConverterFactory(ModelInspector inspector, FhirJsonPocoSerializerSettings serializerSettings, FhirJsonPocoDeserializerSettings deserializerSettings) : JsonConverterFactory
    {
        internal PocoSerializationEngine? Engine { get; set; }

        private PocoSerializationEngine createDefaultEngine()
        {
            return (PocoSerializationEngine)FhirSerializationEngineFactory.Strict(inspector, serializerSettings, deserializerSettings);
        }

        public FhirJsonConverterFactory(
            Assembly assembly, FhirJsonPocoSerializerSettings serializerSettings, FhirJsonPocoDeserializerSettings deserializerSettings) : this(ModelInspector.ForAssembly(assembly), serializerSettings, deserializerSettings)
        {
            // Nothing
        }

        internal void SetEnforcedErrors(IEnumerable<string> toEnforce)
        {
            Engine ??= createDefaultEngine();
            Engine.IgnoreFilter = Engine.IgnoreFilter.And(toEnforce.ToPredicate().Negate());
        }

        internal void SetIgnoredErrors(IEnumerable<string> toIgnore)
        {
            Engine ??= createDefaultEngine();
            Engine.IgnoreFilter = Engine.IgnoreFilter.Or(toIgnore.ToPredicate());
        }

        internal void SetMode(DeserializerModes mode)
        {
            Engine = mode switch
            {
                DeserializerModes.Recoverable => (PocoSerializationEngine)FhirSerializationEngineFactory.Recoverable(inspector, serializerSettings, deserializerSettings),
                DeserializerModes.BackwardsCompatible => (PocoSerializationEngine)FhirSerializationEngineFactory.BackwardsCompatible(inspector, serializerSettings, deserializerSettings),
                DeserializerModes.Ostrich => (PocoSerializationEngine)FhirSerializationEngineFactory.Ostrich(inspector, serializerSettings, deserializerSettings),
                _ => createDefaultEngine()
            };
        }

        public override bool CanConvert(Type typeToConvert) => typeof(Base).IsAssignableFrom(typeToConvert);

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Engine ??= createDefaultEngine();
            return (JsonConverter?)Activator.CreateInstance(
                typeof(FhirJsonConverter<>).MakeGenericType(typeToConvert), BindingFlags.NonPublic | BindingFlags.Instance, null,
                [Engine], null, null);
        }
    }


    /// <summary>
    /// FHIR Resource and datatype converter for FHIR deserialization.
    /// </summary>
    public class FhirJsonConverter<F> : JsonConverter<F>
        where F : Base
    {
        private readonly PocoSerializationEngine _engine;

        private FhirJsonConverter(IFhirSerializationEngine engine)
        {
            this._engine = (PocoSerializationEngine)engine;
        }

        /// <summary>
        /// Constructs a <see cref="JsonConverter{T}"/> that (de)serializes FHIR json for the 
        /// POCOs in a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing classes to be used for deserialization.</param>
        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(
            Assembly assembly) : this(ModelInspector.ForAssembly(assembly))
        {
            // nothing
        }

        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(
            Assembly assembly, FhirJsonPocoSerializerSettings? serializerSettings = null, FhirJsonPocoDeserializerSettings? deserializerSettings = null,
            Predicate<CodedException>? ignoreFilter = null) :
            this(FhirSerializationEngineFactory.Custom(ModelInspector.ForAssembly(assembly), ignoreFilter ?? (_ => false), deserializerSettings, serializerSettings))
        { }

        /// <summary>
        /// Constructs a <see cref="JsonConverter{T}"/> that (de)serializes FHIR json for the 
        /// POCOs in a given assembly.
        /// </summary>
        /// <param name="inspector">The <see cref="ModelInspector" /> containing classes to be used for deserialization.</param>
        [Obsolete("Using this directly is not recommended. Instead, try creating a converter using the .ForFhir static method of the JsonSerializerOptions class")]
        public FhirJsonConverter(
            ModelInspector inspector) : this(FhirSerializationEngineFactory.Strict(inspector))
        {
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
        public FhirJsonConverter(BaseFhirJsonPocoDeserializer deserializer, BaseFhirJsonPocoSerializer serializer) : this(FhirSerializationEngineFactory.WithCustomJsonSerializers(deserializer, serializer))
        {
        }

        /// <summary>
        /// Determines whether the specified type can be converted.
        /// </summary>
        public override bool CanConvert(Type objectType) => typeof(F) == objectType;

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

#nullable restore