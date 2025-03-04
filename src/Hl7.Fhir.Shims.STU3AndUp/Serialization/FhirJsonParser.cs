/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class FhirJsonParser(ParserSettings? settings = null) : BaseFhirParser
{
    public ParserSettings Settings { get; set; } = settings ?? new ParserSettings();

    /// <inheritdoc cref="ParseAsync{T}(string)" />
    public T Parse<T>(string json) where T : Base => (T)Parse(json, typeof(T));

    public async Tasks.Task<T> ParseAsync<T>(string json) where T : Base
        => (T)await ParseAsync(json, typeof(T)).ConfigureAwait(false);

    /// <inheritdoc cref="ParseAsync{T}(JsonReader)" />
    public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));

    public async Tasks.Task<T> ParseAsync<T>(JsonReader reader) where T : Base
        => (T)await ParseAsync(reader, typeof(T)).ConfigureAwait(false);

    /// <inheritdoc cref="ParseAsync(string, Type)" />
    public Base Parse(string json, Type? dataType = null)
    {
        var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
        var jsonReader = FhirJsonNode.Parse(json, rootName, BuildJsonParserSettings(Settings));
        return parse(jsonReader, dataType);
    }

    public async Tasks.Task<Base> ParseAsync(string json, Type? dataType = null)
    {
        var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
        var jsonReader = await FhirJsonNode.ParseAsync(json, rootName, BuildJsonParserSettings(Settings)).ConfigureAwait(false);
        return parse(jsonReader, dataType);
    }

    /// <inheritdoc cref="ParseAsync(JsonReader, Type)" />
    public Base Parse(JsonReader reader, Type? dataType = null)
    {
        var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
        var jsonReader = FhirJsonNode.Read(reader, rootName, BuildJsonParserSettings(Settings));
        return parse(jsonReader, dataType);
    }

    public async Tasks.Task<Base> ParseAsync(JsonReader reader, Type? dataType = null)
    {
        var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
        var jsonReader = await FhirJsonNode.ReadAsync(reader, rootName, BuildJsonParserSettings(Settings)).ConfigureAwait(false);
        return parse(jsonReader, dataType);
    }

    private Base parse(ISourceNode node, Type? type = null) =>
        node.ToPoco(ModelInfo.ModelInspector, type, BuildPocoBuilderSettings(Settings));
}