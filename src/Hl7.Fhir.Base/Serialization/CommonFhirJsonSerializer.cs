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

public static class CommonFhirJsonSerializerExtensions
{
    /// <summary>
    /// Serializes the given POCO into a FHIR Json string.
    /// </summary>
    public static string SerializeToString(this CommonFhirJsonSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToString(w => ser.Serialize(element, w, filter), pretty);

    public static string SerializeToString(this CommonFhirJsonSerializer ser, Base instance,
            SummaryType summary, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            bool pretty = false) =>
        ser.SerializeToString(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<string> SerializeToStringAsync(this CommonFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
       TaskExtensions.FromResult(ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, pretty));


    /// <summary>
    /// Serializes the given POCO into a FHIR Json byte array.
    /// </summary>
    public static byte[] SerializeToBytes(this CommonFhirJsonSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToBytes(w => ser.Serialize(element, w, filter), pretty);

    public static byte[] SerializeToBytes(this CommonFhirJsonSerializer ser, Base instance,
        SummaryType summary, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        ser.SerializeToBytes(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<byte[]> SerializeToBytesAsync(this CommonFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, pretty));

    [Obsolete(
        "We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static JObject SerializeToDocument(this CommonFhirJsonSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        return JObject.Parse(jsonText);
    }

    [Obsolete(
        "We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static void Serialize(this CommonFhirJsonSerializer ser, Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        writer.WriteRaw(jsonText);
    }

    [Obsolete("We're phasing out Newtonsoft in favor of System.Text.Json, please use FhirJsonSerializer.Default.Serialize() instead.")]
    public static async Tasks.Task SerializeAsync(this CommonFhirJsonSerializer ser, Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false)
    {
        var jsonText = ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary);
        await writer.WriteRawAsync(jsonText).ConfigureAwait(false);
    }

    public static void Serialize(this CommonFhirJsonSerializer ser, Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        ser.Serialize(
            instance,
            writer,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    public static Tasks.Task SerializeAsync(this CommonFhirJsonSerializer ser, Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary);
        return Tasks.Task.CompletedTask;
    }
}