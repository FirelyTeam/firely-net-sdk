#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Serialization;

internal partial class PocoSerializationEngine
{
    private readonly FhirJsonPocoDeserializerSettings? _jsonDeserializerSettings;
    private readonly FhirJsonPocoSerializerSettings? _jsonSerializerSettings;
    
    private BaseFhirJsonPocoDeserializer? _jsonDeserializer;
    private BaseFhirJsonPocoSerializer? _jsonSerializer;
    
    private BaseFhirJsonPocoDeserializer getJsonDeserializer() => 
        _jsonDeserializer ??= new BaseFhirJsonPocoDeserializer(_inspector, _jsonDeserializerSettings!);

    private BaseFhirJsonPocoSerializer getJsonSerializer() =>
        _jsonSerializer ??= new BaseFhirJsonPocoSerializer(_inspector.FhirRelease, _jsonSerializerSettings!);
    
    /// <inheritdoc />
    public Resource DeserializeFromJson(string data)
    {
        return (Resource)deserializeAndFilterErrors(() =>
        { 
            _ = getJsonDeserializer().TryDeserializeResource(data, out var instance, out var issues);
            return (instance, issues);
        });
    }
    
    /// <inheritdoc />
    public Resource DeserializeFromJson(ref Utf8JsonReader reader) => deserializeAndFilterErrors(getJsonDeserializer(), ref reader);
    
    /// <inheritdoc />
    public Base DeserializeObjectFromJson(Type targetType, ref Utf8JsonReader reader) => 
        deserializeObjectAndFilterErrors(targetType, getJsonDeserializer(), ref reader);
    
    // overload necessary since ref structs cannot be captured in the lambda
    private Resource deserializeAndFilterErrors(BaseFhirJsonPocoDeserializer deserializer, ref Utf8JsonReader reader)
    {
        _ = deserializer.TryDeserializeResource(ref reader, out var instance, out var issues);
        var relevantIssues = issues.Where(i => !IgnoreFilter(i)).ToList();

        return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
    }
    
    // overload necessary since ref structs cannot be captured in the lambda
    private Base deserializeObjectAndFilterErrors(Type targetType, BaseFhirJsonPocoDeserializer deserializer, ref Utf8JsonReader reader)
    {
        _ = deserializer.TryDeserializeObject(targetType, ref reader, out var instance, out var issues);
        var relevantIssues = issues.Where(i => !IgnoreFilter(i)).ToList();

        return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
    }
    
    /// <inheritdoc />
    public string SerializeToJson(Resource instance) => getJsonSerializer().SerializeToString(instance);

    /// <inheritdoc />
    public void SerializeToJsonWriter(Base instance, Utf8JsonWriter writer) => getJsonSerializer().Serialize(instance, writer);
}

#nullable restore