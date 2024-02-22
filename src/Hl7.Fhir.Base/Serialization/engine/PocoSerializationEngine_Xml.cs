#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
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

    /// <inheritdoc />
    public Resource DeserializeFromXml(XmlReader reader)
    {
        return (Resource)deserializeAndFilterErrors(() =>
        { 
            _ = getXmlDeserializer().TryDeserializeResource(reader, out var instance, out var issues);
            return (instance, issues);
        });
    }

    /// <inheritdoc />
    public Base DeserializeElementFromXml(Type targetType, XmlReader reader)
    {
        return deserializeAndFilterErrors(() =>
        {
            _ = getXmlDeserializer().TryDeserializeElement(targetType, reader, out var instance, out var issues);
            return (instance, issues);
        });
    }
    
    
    /// <inheritdoc />
    public string SerializeToXml(Resource instance) => getXmlSerializer().SerializeToString(instance);
}