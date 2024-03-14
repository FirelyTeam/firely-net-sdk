using Hl7.Fhir.Model;
using System;
using System.Text.Json;
using System.Xml;

#nullable enable

namespace Hl7.Fhir.Serialization;

internal interface IFhirExtendedSerializationEngine : IFhirSerializationEngine
{
    /// <summary>
    /// Deserializes a resource from a JSON reader
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <returns>The parsed resource</returns>
    public Resource? DeserializeFromJson(ref Utf8JsonReader reader);

    /// <summary>
    /// Deserializes an object from a JSON reader
    /// </summary>
    /// <param name="targetType">The target type of the object</param>
    /// <param name="reader">The JSON reader</param>
    /// <returns>The parsed object</returns>
    public Base? DeserializeObjectFromJson(Type targetType, ref Utf8JsonReader reader);
    
    /// <summary>
    /// Serializes an instance of any child of base to the supplied writer
    /// </summary>
    /// <param name="instance">An instance of Base or any of its children</param>
    /// <param name="writer">The JSON writer</param>
    public void SerializeToJsonWriter(Base instance, Utf8JsonWriter writer);
    
    /// <summary>
    /// Deserializes a resource from an XML reader
    /// </summary>
    /// <param name="reader">The XML reader</param>
    /// <returns>The parsed resource</returns>
    public Resource? DeserializeFromXml(XmlReader reader);
    
    /// <summary>
    /// Deserializes an element from an XML reader
    /// </summary>
    /// <param name="targetType">The target type of the object</param>
    /// <param name="reader">The XML reader</param>
    /// <returns>A POCO representation of the input read by the reader</returns>
    public Base? DeserializeElementFromXml(Type targetType, XmlReader reader);
}

#nullable restore