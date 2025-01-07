/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class CommonFhirXmlSerializer(ModelInspector modelInspector)
{
    private readonly BaseFhirXmlPocoSerializer _serializer = new(modelInspector);

    private static Base markSubsettedIfNecessary(Base instance, SummaryType summaryType) =>
        summaryType == SummaryType.False ? instance : instance.MakeSubsettedClone();

    public string SerializeToString(Base instance,
            SummaryType summary = SummaryType.False, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            string? root = null,
            bool pretty = false) =>
        _serializer.SerializeToString(
            markSubsettedIfNecessary(instance, summary),
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            root);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public Tasks.Task<string> SerializeToStringAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, root, pretty));

    public byte[] SerializeToBytes(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        _serializer.SerializeToBytes(
            markSubsettedIfNecessary(instance, summary),
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            root);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public Tasks.Task<byte[]> SerializeToBytesAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        TaskExtensions.FromResult(SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, root, pretty));

    [Obsolete("This method uses the older ITypedElement-based serializers and should not be used anymore.")]
    public XDocument SerializeToDocument(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXDocument().Rename(root);

    public void Serialize(Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null) =>
        _serializer.Serialize(
            markSubsettedIfNecessary(instance, summary),
            writer,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary),
            root);

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public Tasks.Task SerializeAsync(Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        string? root = null)
    {
        Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary, root);
        return Tasks.Task.CompletedTask;
    }
}