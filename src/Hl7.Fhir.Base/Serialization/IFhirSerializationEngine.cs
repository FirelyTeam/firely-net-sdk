using Hl7.Fhir.Model;
using System.Collections.ObjectModel;
using System.IO;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Represents an object that can serialize/deserialize FHIR data from the supported
    /// serialization formats.
    /// </summary>
    public interface IFhirSerializationEngine
    {
        /// <summary>
        /// Deserialize an XML string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Xml format.</exception>
        public Resource DeserializeFromXml(string data);

        /// <summary>
        /// Deserialize a Json string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Json format.</exception>
        public Resource DeserializeFromJson(string data);
        
        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Xml.
        /// </summary>
        public string SerializeToXml(Resource instance);

        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Json.
        /// </summary>
        public string SerializeToJson(Resource instance);
    }
}