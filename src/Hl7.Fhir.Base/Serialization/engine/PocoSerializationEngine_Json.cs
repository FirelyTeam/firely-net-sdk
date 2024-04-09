#nullable enable

using Hl7.Fhir.Model;
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
    public string SerializeToJson(Resource instance) => getJsonSerializer().SerializeToString(instance);
    
    /// <summary>
    /// Deserializes a resource from a JSON reader
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <returns>The parsed resource</returns>
    public Resource DeserializeFromJson(ref Utf8JsonReader reader) => deserializeAndFilterErrors(getJsonDeserializer(), ref reader);
    
    /// <summary>
    /// Deserializes an object from a JSON reader
    /// </summary>
    /// <param name="targetType">The target type of the object</param>
    /// <param name="reader">The JSON reader</param>
    /// <returns>The parsed object</returns>
    public Base DeserializeObjectFromJson(Type targetType, ref Utf8JsonReader reader) => 
        deserializeObjectAndFilterErrors(targetType, getJsonDeserializer(), ref reader);

    /// <summary>
    /// Serializes an instance of any child of base to the supplied writer
    /// </summary>
    /// <param name="instance">An instance of Base or any of its children</param>
    /// <param name="writer">The JSON writer</param>
    public void SerializeToJsonWriter(Base instance, Utf8JsonWriter writer) => getJsonSerializer().Serialize(instance, writer);
    
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
}

#nullable restore