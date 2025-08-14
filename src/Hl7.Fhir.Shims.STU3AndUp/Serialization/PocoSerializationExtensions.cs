/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Provides extension methods on POCOs to serialize to Xml and Json.
/// </summary>
public static class PocoSerializationExtensions
{
    /// <summary>
    /// Serializes the given POCO into a FHIR Json string.
    /// </summary>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    public static string ToJson(this Base instance, bool pretty = false) =>
        FhirJsonSerializer.Default.SerializeToString(instance, pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Xml string.
    /// </summary>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    public static string ToXml(this Base instance, bool pretty = false) =>
        FhirXmlSerializer.Default.SerializeToString(instance);

    // 20241217
    [Obsolete("We're cleaning up the POCO API surface, please use FhirJsonSerializer.Default.SerializeToBytes() instead.")]
    public static byte[] ToJsonBytes(this Base source, bool pretty = false) =>
          FhirJsonSerializer.Default.SerializeToBytes(source, pretty);

    // 20241217
    [Obsolete("We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static void WriteTo(this Base source, JsonWriter destination) =>
        source.ToTypedElement().WriteTo(destination);

    // 20241217
    [Obsolete("We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static async Tasks.Task WriteToAsync(this Base source, JsonWriter destination) =>
        await source.ToTypedElement().WriteToAsync(destination).ConfigureAwait(false);


    [Obsolete("The new serializers are written against non-async APIs, so this async call is actually sync. Change to a non-async call.")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public static async Tasks.Task<string> ToXmlAsync(this Base source, bool pretty = false) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        FhirXmlSerializer.Default.SerializeToString(source);

    // 20241217
    [Obsolete("We're cleaning up the POCO API surface, please use FhirXmlSerializer.Default.SerializeToBytes() instead.")]
    public static byte[] ToXmlBytes(this Base source, bool pretty = false, SerializationFilter? filter = null) =>
        FhirXmlSerializer.Default.SerializeToBytes(source, pretty, filter is not null ? () => filter : null);

    // 20241217
    [Obsolete("We're cleaning up the POCO API surface, please use FhirXmlSerializer.Default.Serialize() instead.")]
    public static void WriteTo(this Base source, XmlWriter destination, SerializationFilter? filter = null) =>
        FhirXmlSerializer.Default.Serialize(source, destination, filter);

    // 20241217
    [Obsolete("We're cleaning up the POCO API surface, please use and FhirXmlSerializer.Default.SerializeToDocument() instead.")]
    public static XDocument ToXDocument(this Base source) => FhirXmlSerializer.Default.SerializeToDocument(source);
}