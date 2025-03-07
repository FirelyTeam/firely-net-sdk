/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class JsonParsingExtensions
{
    /// <inheritdoc cref="Parse(BaseFhirJsonParser,string,System.Type?)" />
    public static T Parse<T>(this BaseFhirJsonParser parser, string json) where T : Base
        => (T)parser. Parse(json, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirJsonParser,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirJsonParser parser, string json) where T : Base
        => (T)await parser.ParseAsync(json, typeof(T)).ConfigureAwait(false);

    /// <inheritdoc cref="Parse(BaseFhirJsonParser,JsonReader,System.Type?)" />
    public static T Parse<T>(this BaseFhirJsonParser parser, JsonReader reader) where T : Base
        => (T)parser.Parse(reader, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirJsonParser,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirJsonParser parser, JsonReader reader) where T : Base
        => (T)await parser.ParseAsync(reader, typeof(T)).ConfigureAwait(false);

    /// <summary>
    /// Deserializes the given Json string into a FHIR resource or datatype.
    /// </summary>
    /// <param name="parser">The parser for which this extension method can be called.</param>
    /// <param name="json">A string of FHIR Json.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirJsonParser parser, string json, Type? dataType = null)
    {
        using var jsonReader = SerializationUtil.JsonReaderFromJsonText(json);
        return parse(parser, jsonReader, dataType);
    }

    /// <inheritdoc cref="Parse(BaseFhirJsonParser,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirJsonParser parser, string json, Type? dataType = null) =>
        Task.FromResult(parser.Parse(json, dataType));

    /// <summary>
    /// Deserializes the Json passed in the JsonReader into a FHIR resource or datatype.
    /// </summary>
    /// <param name="parser">The parser for which this extension method can be called.</param>
    /// <param name="reader">An JsonReader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirJsonParser parser, JsonReader reader, Type? dataType = null) =>
        parse(parser, reader, dataType);

    /// <inheritdoc cref="Parse(BaseFhirJsonParser,JsonReader,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirJsonParser parser, JsonReader reader, Type? dataType = null) =>
        Task.FromResult(parser.Parse(reader, dataType));

    private static Base parse(BaseFhirJsonParser parser, JsonReader json, Type? dataType = null) =>
        parse(parser, json.ToString()!, dataType);

    private static Base parse(BaseFhirJsonParser parser, string json, Type? dataType = null)
    {
        if (dataType is null || typeof(Resource).IsAssignableFrom(dataType))
            return parser.DeserializeResource(json);

        return parser.DeserializeObject(dataType, json);
    }
}