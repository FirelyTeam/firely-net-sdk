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
using System;
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class CommonFhirXmlSerializerExtensions
{
    /// <summary>
    /// Serializes the given POCO into a FHIR Xml string.
    /// </summary>
    public static string SerializeToString(this CommonFhirXmlSerializer ser, Base instance, bool pretty = false,
        SerializationFilter? filter = null, string? rootName = null) =>
        SerializationUtil.WriteXmlToString(w => ser.Serialize(instance, w, filter, rootName), pretty);

    public static string SerializeToString(this CommonFhirXmlSerializer ser, Base instance,
            SummaryType summary, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            string? rootName = null,
            bool pretty = false) =>
        ser.SerializeToString(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<string> SerializeToStringAsync(this CommonFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, rootName, pretty));


    /// <summary>
    /// Serializes the given POCO into a FHIR Xml byte array.
    /// </summary>
    public static byte[] SerializeToBytes(this CommonFhirXmlSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null, string? rootName = null) =>
        SerializationUtil.WriteXmlToBytes(w => ser.Serialize(element, w, filter, rootName), pretty);

    public static byte[] SerializeToBytes(this CommonFhirXmlSerializer ser, Base instance,
        SummaryType summary, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        ser.SerializeToBytes(
            instance,
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<byte[]> SerializeToBytesAsync(this CommonFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, rootName, pretty));

    public static XDocument SerializeToDocument(this CommonFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null)
    {
        var result = new XDocument();
        using var writer = result.CreateWriter();
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary, rootName);
        writer.Flush();

        return result;
    }

    public static void Serialize(this CommonFhirXmlSerializer ser, Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null) =>
        ser.Serialize(
            instance,
            writer,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task SerializeAsync(this CommonFhirXmlSerializer ser, Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        string? rootName = null)
    {
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary, rootName);
        return Tasks.Task.CompletedTask;
    }
}