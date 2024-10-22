#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization;

internal partial class PocoSerializationEngine
{
    private readonly FhirXmlPocoDeserializerSettings _xmlSettings;
    
    private BaseFhirXmlPocoDeserializer? _xmlDeserializer;
    private BaseFhirXmlPocoSerializer? _xmlSerializer;

    private BaseFhirXmlPocoDeserializer getXmlDeserializer() =>
        _xmlDeserializer ??= new BaseFhirXmlPocoDeserializer(_inspector, _xmlSettings);

    private BaseFhirXmlPocoSerializer getXmlSerializer() =>
        _xmlSerializer ??= new BaseFhirXmlPocoSerializer(_inspector.FhirRelease);
    
    /// <inheritdoc />
    public Resource DeserializeFromXml(string data)
    {
        return (Resource)deserializeAndFilterErrors(() =>
        { 
            _ = getXmlDeserializer().TryDeserializeResource(data, out var instance, out var issues);
            return (instance, issues);
        });
    }

    internal Base DeserializeBaseFromXml(string data)
    {
        return deserializeAndFilterErrors(() =>
        { 
            _ = getXmlDeserializer().TryDeserializeResource(data, out var instance, out var issues);
            return (instance, issues);
        });
    }
    
    /// <inheritdoc />
    [TemporarilyChanged]
    public string SerializeToXml(Base instance) => getXmlSerializer().SerializeToString(instance);

    /// <summary>
    /// Deserializes a resource from an XML reader
    /// </summary>
    /// <param name="reader">The XML reader</param>
    /// <returns>The parsed resource</returns>
    public Resource DeserializeFromXml(XmlReader reader)
    {
        return (Resource)deserializeAndFilterErrors(() =>
        { 
            _ = getXmlDeserializer().TryDeserializeResource(reader, out var instance, out var issues);
            return (instance, issues);
        });
    }

    /// <summary>
    /// Deserializes an element from an XML reader
    /// </summary>
    /// <param name="targetType">The target type of the object</param>
    /// <param name="reader">The XML reader</param>
    /// <returns>A POCO representation of the input read by the reader</returns>
    public Base DeserializeElementFromXml(Type targetType, XmlReader reader)
    {
        return deserializeAndFilterErrors(() =>
        {
            _ = getXmlDeserializer().TryDeserializeElement(targetType, reader, out var instance, out var issues);
            return (instance, issues);
        });
    }
    
    /// <summary>
    /// Serializes an element to the supplied writer
    /// </summary>
    /// <param name="instance">An instance of Base or any of its children</param>
    /// <param name="writer">The XML writer</param>
    [TemporarilyChanged]
    public void SerializeToXmlWriter(Base instance, XmlWriter writer) => getXmlSerializer().Serialize(instance, writer);
}