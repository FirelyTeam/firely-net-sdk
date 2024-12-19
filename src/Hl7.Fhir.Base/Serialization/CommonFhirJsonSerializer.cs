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
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class CommonFhirJsonSerializer(ModelInspector modelInspector) : BaseFhirSerializer(modelInspector)
{
    public string SerializeToString(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .ToJson(pretty);

    public async Tasks.Task<string> SerializeToStringAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false)=>
        await MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .ToJsonAsync(pretty).ConfigureAwait(false);

    public byte[] SerializeToBytes(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .ToJsonBytes(pretty);

    public async Tasks.Task<byte[]> SerializeToBytesAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null,
        bool includeMandatoryInElementsSummary = false,
        bool pretty = false) =>
        await MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .ToJsonBytesAsync(pretty).ConfigureAwait(false);

    public JObject SerializeToDocument(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .ToJObject();

    public void Serialize(Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .WriteTo(writer);

    public async Tasks.Task SerializeAsync(Base instance, JsonWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false) =>
        await MakeElementStack(instance, summary, elements, includeMandatoryInElementsSummary)
            .WriteToAsync(writer)
            .ConfigureAwait(false);
}