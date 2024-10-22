/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Represents an object that can serialize/deserialize FHIR data from the supported
    /// serialization formats.
    /// </summary>
    public interface IFhirSerializationEngine
    {
        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Json.
        /// </summary>
        public string SerializeToJson(Resource instance);
    
        /// <summary>
        /// Deserialize a Json string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Json format.</exception>
        public Resource? DeserializeFromJson(string data);
        
        /// <summary>
        /// Deserialize an XML string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Xml format.</exception>
        /// <returns>Null if the data did not contain a resource, but another FHIR datatype.</returns>
        public Resource? DeserializeFromXml(string data);

        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Xml.
        /// </summary>
        [TemporarilyChanged]
        public string SerializeToXml(Base instance);
    }
    
    /// <summary>
    /// Extension methods for the <see cref="IFhirSerializationEngine"/> interface when the underlying engine is NOT a legacy engine.
    /// </summary>
    public static class SerializationEngineExtensions
    {
        /// <summary>
        /// Deserialize a FHIR Resource from a JSON reader.
        /// </summary>
        /// <returns>The resource, or null if the operation failed</returns>
        /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
        /// <exception cref="DeserializationFailedException">Thrown if a FHIR error was encountered in the data</exception>
        public static Resource? SerializeReaderToJson(this IFhirSerializationEngine engine, ref Utf8JsonReader reader)
        {
            if (engine is not PocoSerializationEngine pse)
            {
                throw new InvalidOperationException("stream reading is not supported by legacy engines");
            }
            
            return pse.DeserializeFromJson(ref reader);
        }

        /// <summary>
        /// Deserialize a FHIR Resource from an XML reader.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
        /// <exception cref="DeserializationFailedException">Thrown if a FHIR error was encountered in the data</exception>
        [TemporarilyChanged]
        public static Base? SerializeReaderToXml(this IFhirSerializationEngine engine, XmlReader reader)
        {
            if (engine is not PocoSerializationEngine pse)
            {
                throw new InvalidOperationException("stream reading is not supported by legacy engines");
            }
            
            return pse.DeserializeFromXml(reader);
        }
        
        /// <summary>
        /// Serialize a FHIR Resource to a JSON writer.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SerializeToJsonWriter(this IFhirSerializationEngine engine, Resource instance, Utf8JsonWriter writer)
        {
            if (engine is not PocoSerializationEngine pse)
            {
                throw new InvalidOperationException("stream writing is not supported by legacy engines");
            }
            
            pse.SerializeToJsonWriter(instance, writer);
        }
        
        /// <summary>
        /// Serialize a FHIR Resource to an XML writer.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
        [TemporarilyChanged]
        public static void SerializeToXmlWriter(this IFhirSerializationEngine engine, Base instance, XmlWriter writer)
        {
            if (engine is not PocoSerializationEngine pse)
            {
                throw new InvalidOperationException("stream writing is not supported by legacy engines");
            }
            
            pse.SerializeToXmlWriter(instance, writer);
        }
    }
}

#nullable restore
