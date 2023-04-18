/*
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
    /// new Poco-based parser and serializer. It is used as a switchable serialization
    /// in the <c>BaseFhirClient</c>.
    /// </summary>
    internal class PocoSerializationEngine : IFhirSerializationEngine
    {
        private readonly JsonSerializerOptions _options;
        private readonly ModelInspector _inspector;
        private readonly Predicate<CodedException> _ignoreFilter;

        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        /// <param name="ignoreFilter">A predicate that returns true for issues that should not be reported.</param>
        public PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException> ignoreFilter)
        {
            _options = new JsonSerializerOptions().ForFhir(inspector);
            _inspector = inspector;
            _ignoreFilter = ignoreFilter;
        }

        /// <summary>
        /// Creates an implementation of <see cref="IFhirSerializationEngine"/> that uses the newer POCO (de)serializers.
        /// </summary>
        /// <param name="inspector">Reflection data of the POCO model to use.</param>
        public PocoSerializationEngine(ModelInspector inspector)
        {
            _options = new JsonSerializerOptions().ForFhir(inspector);
            _inspector = inspector;
            _ignoreFilter = _ => false;
        }


        public bool TryDeserializeFromXml(string data, out Resource? instance, IEnumerable<CodedException> issues) => 
            new BaseFhirXmlPocoDeserializer(_inspector).TryDeserializeResource(data, out instance, out issues);

        public Resource DeserializeFromJson(string data) => JsonSerializer.Deserialize<Resource>(data, _options)!;

        public string SerializeToXml(Resource instance) => new BaseFhirXmlPocoSerializer(_inspector.FhirRelease).SerializeToString(instance);

        public string SerializeToJson(Resource instance) => JsonSerializer.Serialize(instance, _options);
    }

    /// <summary>
    /// Extension methods that provide additional utility methods for deserialization on top of
    /// the TryDeserialize() functions of the json and xml deserializers.
    /// </summary>
    public static class PocoDeserializationExtensions
    {
        /// <summary>
        /// Deserialize the FHIR xml from the reader and create a new POCO resource containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static Resource DeserializeResource(this BaseFhirXmlPocoDeserializer deserializer, XmlReader reader) =>
                deserializer.TryDeserializeResource(reader, out var instance, out var issues) ?
                instance! : throw new DeserializationFailedException(instance, issues);

        /// <summary>
        /// Deserialize the FHIR xml from a string and create a new POCO resource containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="data">A string containing the XML from which to deserialize the resource.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static Resource DeserializeResource(this BaseFhirXmlPocoDeserializer deserializer, string data)
        {
            var xmlReader = SerializationUtil.XmlReaderFromXmlText(data);
            return deserializer.DeserializeResource(xmlReader);
        }

        /// <summary>
        /// Deserialize the FHIR xml from a string and create a new POCO resource containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="data">A string containing the XML from which to deserialize the resource.</param>
        /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
        /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static bool TryDeserializeResource(
            this BaseFhirXmlPocoDeserializer deserializer, 
            string data, 
            out Resource? instance, 
            out IEnumerable<CodedException> issues)
        {
            var xmlReader = SerializationUtil.XmlReaderFromXmlText(data);
            return deserializer.TryDeserializeResource(xmlReader, out instance, out issues);
        }

        /// <summary>
        /// Reads a (subtree) of serialized FHIR Xml data into a POCO object.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="targetType">The type of POCO to construct and deserialize</param>
        /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static Base DeserializeElement(this BaseFhirXmlPocoDeserializer deserializer, Type targetType, XmlReader reader) =>
            deserializer.TryDeserializeElement(targetType, reader, out var instance, out var issues) ?
            instance! : throw new DeserializationFailedException(instance, issues);

        /// <summary>
        /// Reads a (subtree) of serialized FHIR Xml data into a POCO object.
        /// </summary>
        /// <typeparam name="T">The type of POCO to construct and deserialize</typeparam>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static T DeserializeElement<T>(this BaseFhirXmlPocoDeserializer deserializer, XmlReader reader) where T : Base => 
            (T)deserializer.DeserializeElement(typeof(T), reader);

        /// <summary>
        /// Deserialize the FHIR Json from the reader and create a new POCO object containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static Resource DeserializeResource(this BaseFhirJsonPocoDeserializer deserializer, ref Utf8JsonReader reader) =>
            deserializer.TryDeserializeResource(ref reader, out var instance, out var issues)
                ? instance! : throw new DeserializationFailedException(instance, issues);

        /// <summary>
        /// Deserialize the FHIR Json from the reader and create a new POCO object containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="json">A string of json.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static Resource DeserializeResource(this BaseFhirJsonPocoDeserializer deserializer, string json)
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json), new() { CommentHandling = JsonCommentHandling.Skip });
            return deserializer.DeserializeResource(ref reader);
        }

        /// <summary>
        /// Deserialize the FHIR Json from the reader and create a new POCO object containing the data from the reader.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="json">A string of json.</param>
        /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
        /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
        /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
        public static bool TryDeserializeResource(
            this BaseFhirJsonPocoDeserializer deserializer, 
            string json, 
            out Resource? instance, 
            out IEnumerable<CodedException> issues)
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json), new() { CommentHandling = JsonCommentHandling.Skip });
            return deserializer.TryDeserializeResource(ref reader, out instance, out issues);
        }

        /// <summary>
        /// Reads a (subtree) of serialized FHIR Json data into a POCO object.
        /// </summary>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="targetType">The type of POCO to construct and deserialize</param>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns> 
        public static Base DeserializeObject(this BaseFhirJsonPocoDeserializer deserializer, Type targetType, ref Utf8JsonReader reader) =>
            deserializer.TryDeserializeObject(targetType, ref reader, out var instance, out var issues) ?
                instance! : throw new DeserializationFailedException(instance, issues);

        /// <summary>
        /// Reads a (subtree) of serialzed FHIR Json data into a POCO object.
        /// </summary>
        /// <typeparam name="T">The type of POCO to construct and deserialize</typeparam>
        /// <param name="deserializer">The deserializer to use.</param>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public static T DeserializeObject<T>(this BaseFhirJsonPocoDeserializer deserializer, ref Utf8JsonReader reader) where T : Base => 
            (T)deserializer.DeserializeObject(typeof(T), ref reader);

        internal static void Throw(this IEnumerable<CodedException> issues, Base instance)
        {
            throw new DeserializationFailedException(instance, issues);
        }
    }
}

#nullable restore