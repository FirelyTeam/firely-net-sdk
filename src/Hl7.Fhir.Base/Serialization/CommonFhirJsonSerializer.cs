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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class CommonFhirJsonSerializer(ModelInspector modelInspector)
{
    private readonly BaseFhirJsonPocoSerializer _serializer = new(modelInspector);

    private static Base markSubsettedIfNecessary(Base instance, SummaryType summaryType) =>
        summaryType == SummaryType.False ? instance : instance.MakeSubsettedClone();

    public string SerializeToString(Base instance,
            SummaryType summary = SummaryType.False, string[]? elements = null,
            bool includeMandatoryInElementsSummary = false,
            bool pretty = false) =>
        _serializer.SerializeToString(
            markSubsettedIfNecessary(instance, summary),
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public Tasks.Task<string> SerializeToStringAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
       TaskExtensions.FromResult(SerializeToString(instance, summary, elements, includeMandatoryInElementsSummary, pretty));

    public byte[] SerializeToBytes(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        _serializer.SerializeToBytes(
            markSubsettedIfNecessary(instance, summary),
            pretty,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("The new serializers do not support async serialization, use the synchronous version instead.")]
    public Tasks.Task<byte[]> SerializeToBytesAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        TaskExtensions.FromResult(SerializeToBytes(instance, summary, elements, includeMandatoryInElementsSummary, pretty));

    [Obsolete("This method uses the older ITypedElement-based serializers and should not be used anymore.")]
    public JObject SerializeToDocument(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .ToJObject();

    [Obsolete("This method uses the older ITypedElement-based serializers and should not be used anymore.")]
    public void Serialize(Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .WriteTo(writer);

    public void Serialize(Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        _serializer.Serialize(
            markSubsettedIfNecessary(instance, summary),
            writer,
            summary.GetSerializationFilter(elements, includeMandatoryInElementsSummary));

    [Obsolete("This method uses the older ITypedElement-based serializers and should not be used anymore.")]
    public async Tasks.Task SerializeAsync(Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        await instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .WriteToAsync(writer)
            .ConfigureAwait(false);

    public Tasks.Task SerializeAsync(Base instance, Utf8JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false)
    {
        Serialize(instance, writer, summary, elements, includeMandatoryInElementsSummary);
        return Tasks.Task.CompletedTask;
    }
}