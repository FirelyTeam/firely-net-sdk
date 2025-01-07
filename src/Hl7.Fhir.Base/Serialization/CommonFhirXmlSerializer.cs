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
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class CommonFhirXmlSerializer(ModelInspector modelInspector)
{
    public string SerializeToString(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXml(pretty);

    public async Tasks.Task<string> SerializeToStringAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        await instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXmlAsync(pretty).ConfigureAwait(false);

    public byte[] SerializeToBytes(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXmlBytes(pretty);

    public async Tasks.Task<byte[]> SerializeToBytesAsync(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null,
        bool pretty = false) =>
        await instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXmlBytesAsync(pretty).ConfigureAwait(false);

    public XDocument SerializeToDocument(Base instance,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .ToXDocument().Rename(root);

    public void Serialize(Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null) =>
        instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .WriteTo(writer);

    public async Tasks.Task SerializeAsync(Base instance, XmlWriter writer,
        SummaryType summary = SummaryType.False, string[]? elements = null, bool includeMandatoryInElementsSummary = false,
        string? root = null) =>
        await instance.MakeElementStack(modelInspector, summary, elements, includeMandatoryInElementsSummary)
            .Rename(root)
            .WriteToAsync(writer).ConfigureAwait(false);
}