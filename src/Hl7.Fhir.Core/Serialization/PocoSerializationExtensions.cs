/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public static class PocoSerializationExtensions
    {
        /// <inheritdoc cref="ToJsonAsync(Base, FhirJsonSerializationSettings)" />
        [Obsolete("Use ToJsonAsync(Base, FhirJsonSerializationSettings) instead.")]
        public static string ToJson(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJson(settings);

        public static async Tasks.Task<string> ToJsonAsync(this Base source, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().ToJsonAsync(settings);

        /// <inheritdoc cref="ToJsonBytesAsync(Base, FhirJsonSerializationSettings)" />
        [Obsolete("Use ToJsonBytesAsync(Base, FhirJsonSerializationSettings) instead.")]
        public static byte[] ToJsonBytes(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJsonBytes(settings);

        public static async Tasks.Task<byte[]> ToJsonBytesAsync(this Base source, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().ToJsonBytesAsync(settings);

        /// <inheritdoc cref="WriteToAsync(Base, JsonWriter, FhirJsonSerializationSettings)" />
        [Obsolete("Use WriteToAsync(Base, JsonWriter, FhirJsonSerializationSettings) instead.")]
        public static void WriteTo(this Base source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);
        
        public static async Tasks.Task WriteToAsync(this Base source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().WriteToAsync(destination, settings);

        public static JObject ToJObject(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJObject(settings);

        /// <inheritdoc cref="ToXmlAsync(Base, FhirXmlSerializationSettings)" />
        [Obsolete("Use ToXmlAsync(Base, FhirXmlSerializationSettings) instead.")]
        public static string ToXml(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXml(settings);

        public static async Tasks.Task<string> ToXmlAsync(this Base source, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().ToXmlAsync(settings);

        /// <inheritdoc cref="ToXmlBytesAsync(Base, FhirXmlSerializationSettings)" />
        [Obsolete("Use ToXmlBytesAsync(Base, FhirXmlSerializationSettings) instead.")]
        public static byte[] ToXmlBytes(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXmlBytes(settings);

        public static async Tasks.Task<byte[]> ToXmlBytesAsync(this Base source, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().ToXmlBytesAsync(settings);

        /// <inheritdoc cref="WriteToAsync(Base, XmlWriter, FhirXmlSerializationSettings)" />
        [Obsolete("Use WriteToAsync(Base, XmlWriter, FhirXmlSerializationSettings) instead.")]
        public static void WriteTo(this Base source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);

        public static async Tasks.Task WriteToAsync(this Base source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().WriteToAsync(destination, settings);

        public static XDocument ToXDocument(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXDocument(settings);
    }
}
