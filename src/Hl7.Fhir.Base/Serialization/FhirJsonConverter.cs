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
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// A converter factory to construct FhirJsonConverters for subclasses of <see cref="Base"/>.
/// </summary>
public class FhirJsonConverterFactory(ModelInspector inspector, FhirJsonConverterOptions converterOptions) : JsonConverterFactory
{
    private BaseFhirJsonDeserializer _deserializer = new(inspector, converterOptions);
    private readonly BaseFhirJsonSerializer _serializer = new(inspector);
    private Func<SerializationFilter>? _serializationFilterFactory = converterOptions.SummaryFilterFactory;

    internal FhirJsonConverterOptions CurrentOptions = converterOptions;

    public void Reconfigure(FhirJsonConverterOptions newOptions)
    {
        _deserializer = new BaseFhirJsonDeserializer(inspector, newOptions);
        _serializationFilterFactory = newOptions.SummaryFilterFactory;
        CurrentOptions = newOptions;
    }
    public override bool CanConvert(Type typeToConvert) => typeof(Base).IsAssignableFrom(typeToConvert);

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter?)Activator.CreateInstance(
            typeof(FhirJsonConverter<>).MakeGenericType(typeToConvert), BindingFlags.Public | BindingFlags.Instance, null,
            [_deserializer, _serializer, _serializationFilterFactory], null, null);
    }
}

/// <summary>
/// FHIR Resource and datatype converter for FHIR deserialization.
/// </summary>
internal class FhirJsonConverter<TF>(BaseFhirJsonDeserializer deserializer, BaseFhirJsonSerializer serializer, Func<SerializationFilter>? summaryFilterFactory = null) : JsonConverter<TF>
    where TF : Base
{
    /// <summary>
    /// Determines whether the specified type can be converted.
    /// </summary>
    public override bool CanConvert(Type objectType) => typeof(TF) == objectType;

    /// <summary>
    /// Writes a specified value as JSON.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, TF poco, JsonSerializerOptions options)
    {
        serializer.Serialize(poco, writer, summaryFilterFactory);
    }

    /// <summary>
    /// Reads and converts the JSON to a typed object.
    /// </summary>
    public override TF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        typeof(Resource).IsAssignableFrom(typeToConvert)
            ? (TF)(Base)deserializer.DeserializeResource(ref reader)
            : (TF)deserializer.DeserializeObject(typeToConvert, ref reader);
}