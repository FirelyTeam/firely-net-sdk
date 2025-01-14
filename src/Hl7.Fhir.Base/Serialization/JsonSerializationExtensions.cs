/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class JsonSerializationExtensions
{
    /// <summary>
    /// Serializes the given POCO into a FHIR Json string.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    /// <param name="filter">An optional <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    public static string SerializeToString(this BaseFhirJsonSerializer ser, Base instance, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToString(w => ser.Serialize(instance, w, filter), pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Json string.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    public static string SerializeToString(this BaseFhirJsonSerializer ser, Base instance,
            SummaryType summary, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            bool pretty = false) =>
        ser.SerializeToString(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<string> SerializeToStringAsync(this BaseFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
       TaskExtensions.FromResult(ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, pretty));


    /// <summary>
    /// Serializes the given POCO into a FHIR Json byte array.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    /// <param name="filter">An optional <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    public static byte[] SerializeToBytes(this BaseFhirJsonSerializer ser, Base instance, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToBytes(w => ser.Serialize(instance, w, filter), pretty);

    /// <summary>
    /// Serializes the given POCO into a  FHIR Json byte array.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    public static byte[] SerializeToBytes(this BaseFhirJsonSerializer ser, Base instance,
        SummaryType summary, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        ser.SerializeToBytes(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<byte[]> SerializeToBytesAsync(this BaseFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, pretty));

    [Obsolete(
        "We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static JObject SerializeToDocument(this BaseFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        return JObject.Parse(jsonText);
    }

    [Obsolete(
        "We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static void Serialize(this BaseFhirJsonSerializer ser, Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        writer.WriteRaw(jsonText);
    }

    [Obsolete("We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static async Tasks.Task SerializeAsync(this BaseFhirJsonSerializer ser, Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        await writer.WriteRawAsync(jsonText).ConfigureAwait(false);
    }

    /// <summary>
    /// Serializes the given POCO into Json.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write the serialized data to.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    public static void Serialize(this BaseFhirJsonSerializer ser, Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        ser.Serialize(
            instance,
            writer,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task SerializeAsync(this BaseFhirJsonSerializer ser, Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary);
        return Tasks.Task.CompletedTask;
    }
}