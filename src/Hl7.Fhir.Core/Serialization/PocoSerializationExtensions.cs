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
using System.Xml;
using System.Xml.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public static class PocoSerializationExtensions
    {
        /// <inheritdoc cref="ToJsonAsync(Base, FhirJsonSerializationSettings)" />
        public static string ToJson(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJson(settings);

        public static async T.Task<string> ToJsonAsync(this Base source, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().ToJsonAsync(settings).ConfigureAwait(false);

        /// <inheritdoc cref="ToJsonBytesAsync(Base, FhirJsonSerializationSettings)" />
        public static byte[] ToJsonBytes(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJsonBytes(settings);

        public static async T.Task<byte[]> ToJsonBytesAsync(this Base source, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().ToJsonBytesAsync(settings).ConfigureAwait(false);

        /// <inheritdoc cref="WriteToAsync(Base, JsonWriter, FhirJsonSerializationSettings)" />
        public static void WriteTo(this Base source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);
        
        public static async T.Task WriteToAsync(this Base source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            await source.ToTypedElement().WriteToAsync(destination, settings).ConfigureAwait(false);

        public static JObject ToJObject(this Base source, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement().ToJObject(settings);

        /// <inheritdoc cref="ToXmlAsync(Base, FhirXmlSerializationSettings)" />
        public static string ToXml(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXml(settings);

        public static async T.Task<string> ToXmlAsync(this Base source, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().ToXmlAsync(settings).ConfigureAwait(false);

        /// <inheritdoc cref="ToXmlBytesAsync(Base, FhirXmlSerializationSettings)" />
        public static byte[] ToXmlBytes(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXmlBytes(settings);

        public static async T.Task<byte[]> ToXmlBytesAsync(this Base source, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().ToXmlBytesAsync(settings).ConfigureAwait(false);

        /// <inheritdoc cref="WriteToAsync(Base, XmlWriter, FhirXmlSerializationSettings)" />
        public static void WriteTo(this Base source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);

        public static async T.Task WriteToAsync(this Base source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            await source.ToTypedElement().WriteToAsync(destination, settings).ConfigureAwait(false);

        public static XDocument ToXDocument(this Base source, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement().ToXDocument(settings);
    }
}
