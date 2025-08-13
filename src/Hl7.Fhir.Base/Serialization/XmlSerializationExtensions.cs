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

public static class XmlSerializationExtensions
{
    /// <summary>
    /// Serializes the given POCO with FHIR data into Xml.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    /// <param name="filter">An optional <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    [Obsolete("Use the overload that takes Func<SerializationFilter> instead to ensure thread-safety when reusing" +
              " serializer instances in concurrent scenarios. This method will be removed in a future version.")]
    public static void Serialize(
        this BaseFhirXmlSerializer ser,
        Base instance,
        XmlWriter writer,
        SerializationFilter? filter = null,
        string? rootName = null)
    {
        ser.Serialize(instance, writer, () => filter, rootName);
    }

    /// <summary>
    /// Serializes the given POCO into a FHIR Xml string.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    /// <param name="filterFactory">An optional factory that creates a fresh <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static string SerializeToString(this BaseFhirXmlSerializer ser, Base instance, bool pretty = false,
        Func<SerializationFilter?>? filterFactory = null, string? rootName = null) =>
        SerializationUtil.WriteXmlToString(w => ser.Serialize(instance, w, filterFactory, rootName), pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Xml string.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static string SerializeToString(this BaseFhirXmlSerializer ser, Base instance,
            SummaryType summary, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            string? rootName = null,
            bool pretty = false) =>
        ser.SerializeToString(
            instance,
            pretty,
            () => summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<string> SerializeToStringAsync(this BaseFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, rootName, pretty));


    /// <summary>
    /// Serializes the given POCO into a FHIR Xml byte array.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    /// <param name="filterFactory">An optional factory that creates a fresh <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static byte[] SerializeToBytes(this BaseFhirXmlSerializer ser, Base instance, bool pretty = false,
        Func<SerializationFilter?>? filterFactory = null, string? rootName = null) =>
        SerializationUtil.WriteXmlToBytes(w => ser.Serialize(instance, w, filterFactory, rootName), pretty);

    /// <summary>
    /// Serializes the given POCO into a  FHIR Xml byte array.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static byte[] SerializeToBytes(this BaseFhirXmlSerializer ser, Base instance,
        SummaryType summary, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        ser.SerializeToBytes(
            instance,
            pretty,
            () => summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task<byte[]> SerializeToBytesAsync(this BaseFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(ser.SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, rootName, pretty));

    /// <summary>
    /// Serializes the given POCO into a FHIR <see cref="XDocument"/>.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static XDocument SerializeToDocument(this BaseFhirXmlSerializer ser, Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null)
    {
        var result = new XDocument();
        using var writer = result.CreateWriter();
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary, rootName);
        writer.Flush();

        return result;
    }

    /// <summary>
    /// Serializes the given POCO into Xml.
    /// </summary>
    /// <param name="ser">The serializer to use.</param>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    /// <param name="summary">The kind of summary to use for serialization. Optional .</param>
    /// <param name="elements">If <paramref name="summary"/> is <see cref="SummaryType.False"/>, specifies which
    /// top-level elements of the resource to serialize. Optional.</param>
    /// <param name="includeMandatoryInElementsSummary">If <paramref name="summary"/> is <see cref="SummaryType.False"/>,
    /// indicates whether to include mandatory elements in the summary, in addition to those in <paramref name="elements"/>. Optional.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public static void Serialize(this BaseFhirXmlSerializer ser, Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? rootName = null) =>
        ser.Serialize(
            instance,
            writer,
            () => summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            rootName);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public static Tasks.Task SerializeAsync(this BaseFhirXmlSerializer ser, Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        string? rootName = null)
    {
        ser.Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary, rootName);
        return Tasks.Task.CompletedTask;
    }
}