/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Serialization;

/// <inheritdoc />
public class FhirJsonDeserializer(DeserializerSettings? settings = null)
    : BaseFhirJsonDeserializer(ModelInfo.ModelInspector, settings)
{
    /// <inheritdoc cref="FhirXmlDeserializer.DEFAULT" />
    public static readonly FhirJsonDeserializer DEFAULT = new();

    /// <inheritdoc cref="FhirXmlDeserializer.STRICT" />
    public static readonly FhirJsonDeserializer STRICT = new(new DeserializerSettings().UsingMode(DeserializationMode.Strict));

    /// <summary>
    /// A parsers that only checks the FHIR Json syntax rules, but no content rules.
    /// </summary>
    public static readonly FhirJsonDeserializer SYNTAXONLY = new(new DeserializerSettings().UsingMode(DeserializationMode.SyntaxOnly));

    /// <inheritdoc cref="FhirXmlDeserializer.RECOVERABLE" />
    public static readonly FhirJsonDeserializer RECOVERABLE = new(new DeserializerSettings().UsingMode(DeserializationMode.Recoverable));


    /// <inheritdoc cref="FhirXmlDeserializer.BACKWARDSCOMPATIBLE" />
    public static readonly FhirJsonDeserializer BACKWARDSCOMPATIBLE = new(new DeserializerSettings().UsingMode(DeserializationMode.BackwardsCompatible));

    /// <inheritdoc cref="FhirXmlDeserializer.OSTRICH" />
    public static readonly FhirJsonDeserializer OSTRICH = new(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));
}

[Obsolete("FhirJsonParser is obsolete, use FhirJsonDeserializer instead.")]
public class FhirJsonParser(ParserSettings? settings = null) : FhirJsonDeserializer(settings)
{
    [Obsolete("Use Deserialize<T>() instead.")]
    public T Parse<T>(string json) where T : Base
        => (T)Parse(json, typeof(T));

    /// <inheritdoc cref="Parse(string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Deserialize<T> instead.")]
    public async Task<T> ParseAsync<T>(string json) where T : Base
        => (T)await ParseAsync(json, typeof(T)).ConfigureAwait(false);

    // /// <inheritdoc cref="Parse(BaseFhirJsonParser,JsonReader,System.Type?)" />
    // public static T Parse<T>(this BaseFhirJsonParser parser, JsonReader reader) where T : Base
    //     => (T)parser.Parse(reader, typeof(T));

    // /// <inheritdoc cref="Parse(BaseFhirJsonParser,string,System.Type?)" />
    // [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
    //           "you should explicitly call Parse instead.")]
    // public static async Task<T> ParseAsync<T>(this BaseFhirJsonParser parser, JsonReader reader) where T : Base
    //     => (T)await parser.ParseAsync(reader, typeof(T)).ConfigureAwait(false);

    /// <summary>
    /// Deserializes the given Json string into a FHIR resource or datatype.
    /// </summary>
    /// <param name="json">A string of FHIR Json.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    [Obsolete("Use Deserialize<Resource>() instead (with no dataType parameter), otherwise DeserializeObject().")]
    public Base Parse(string json, Type? dataType = null) =>
        deserialize(json, dataType);

    /// <inheritdoc cref="Parse(string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public Task<Base> ParseAsync(string json, Type? dataType = null) =>
        Task.FromResult(Parse(json, dataType));

    // /// <summary>
    // /// Deserializes the Json passed in the JsonReader into a FHIR resource or datatype.
    // /// </summary>
    // /// <param name="parser">The parser for which this extension method can be called.</param>
    // /// <param name="reader">An JsonReader positioned on the first element, or the beginning of the stream.</param>
    // /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    // /// will be ignored when parsing resources. </param>
    // /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    // /// deserializing non-resource types might not always work.</remarks>
    // public static Base Parse(this BaseFhirJsonParser parser, JsonReader reader, Type? dataType = null) =>
    //     parse(parser, reader, dataType);

    // /// <inheritdoc cref="Parse(BaseFhirJsonParser,JsonReader,System.Type?)" />
    // [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
    //           "you should explicitly call Parse instead.")]
    // public static Task<Base> ParseAsync(this BaseFhirJsonParser parser, JsonReader reader, Type? dataType = null) =>
    //     Task.FromResult(parser.Parse(reader, dataType));

    // private static Base parse(BaseFhirJsonParser parser, JsonReader json, Type? dataType = null) =>
    //     parse(parser, json.ToString()!, dataType);

    private Base deserialize(string json, Type? dataType = null)
    {
        if (dataType is null || typeof(Resource).IsAssignableFrom(dataType))
            return this.DeserializeResource(json);

        return this.DeserializeObject(dataType, json);
    }
}