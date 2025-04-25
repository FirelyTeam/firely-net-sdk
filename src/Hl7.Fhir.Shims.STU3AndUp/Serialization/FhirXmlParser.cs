/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Xml;
using System.Threading.Tasks;
using Task = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

/// <inheritdoc />
public class FhirXmlDeserializer(DeserializerSettings? settings = null)
    : BaseFhirXmlDeserializer(ModelInfo.ModelInspector, settings)
{
    /// <summary>
    /// A parser with default settings: strict validation, only XML is not validated.
    /// </summary>
    public static readonly FhirXmlDeserializer DEFAULT = new();

    /// <summary>
    /// A parser with the most strict settings, will detect all issues we know about.
    /// </summary>
    public static readonly FhirXmlDeserializer STRICT = new(new DeserializerSettings().UsingMode(DeserializationMode.Strict));

    /// <summary>
    /// A parsers that only checks the FHIR XML syntax rules, but no content rules.
    /// </summary>
    public static readonly FhirXmlDeserializer SYNTAXONLY = new(new DeserializerSettings().UsingMode(DeserializationMode.SyntaxOnly));

    /// <summary>
    /// A parser that allows all errors that will not lead to dataloss when roundtripping.
    /// </summary>
    public static readonly FhirXmlDeserializer RECOVERABLE = new(new DeserializerSettings().UsingMode(DeserializationMode.Recoverable));

    /// <summary>
    /// A parser that allows all errors that result from reading data from other FHIR versions: it allows
    /// unknown elements and coded values. This will be roundtrippable.
    /// </summary>
    public static readonly FhirXmlDeserializer BACKWARDSCOMPATIBLE = new(new DeserializerSettings().UsingMode(DeserializationMode.BackwardsCompatible));

    /// <summary>
    /// A parser that continues to parse, ignoring all errors. May result in data loss.
    /// </summary>
    public static readonly FhirXmlDeserializer OSTRICH = new(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));
}

[Obsolete("FhirXmlParser is obsolete, use FhirXmlDeserializer instead.")]
public class FhirXmlParser(ParserSettings? settings = null) : FhirXmlDeserializer(settings)
{
    [Obsolete("Use Deserialize<T>() instead.")]
    public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

    /// <inheritdoc cref="Parse(XmlReader,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Deserialize<T> instead.")]
    public async Task<T> ParseAsync<T>(XmlReader reader) where T : Base
        => (T)(await ParseAsync(reader, typeof(T)).ConfigureAwait(false));

    /// <inheritdoc cref="Parse(string,System.Type?)" />
    [Obsolete("Use Deserialize<T>() instead.")]
    public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));

    /// <inheritdoc cref="Parse(string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public async Task<T> ParseAsync<T>(string xml) where T : Base
        => (T)(await ParseAsync(xml, typeof(T)).ConfigureAwait(false));

    /// <summary>
    /// Deserializes the given XML string into a FHIR resource or datatype.
    /// </summary>
    /// <param name="xml">A string of FHIR XML.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    [Obsolete("Use Deserialize<Resource>() instead (with no dataType parameter), otherwise DeserializeElement().")]
    public Base Parse(string xml, Type? dataType = null)
    {
        using var xmlReader = SerializationUtil.XmlReaderFromXmlText(xml);
        return deserialize(xmlReader, dataType);
    }

    /// <inheritdoc cref="Parse(string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public Task<Base> ParseAsync(string xml, Type? dataType = null) =>
        System.Threading.Tasks.Task.FromResult(Parse(xml, dataType));

    /// <summary>
    /// Deserializes the XML passed in the XmlReader into a FHIR resource or datatype.
    /// </summary>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    [Obsolete("Use Deserialize<Resource>() instead (with no dataType parameter), otherwise DeserializeElement().")]
    public Base Parse(XmlReader reader, Type? dataType = null) =>
        deserialize(reader, dataType);

    /// <inheritdoc cref="ParseAsync(XmlReader, Type)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public Task<Base> ParseAsync(XmlReader reader, Type? dataType = null) =>
        System.Threading.Tasks.Task.FromResult(Parse(reader, dataType));

    private Base deserialize(XmlReader reader, Type? dataType = null)
    {
        if (dataType is null || typeof(Resource).IsAssignableFrom(dataType))
            return this.DeserializeResource(reader);

        return this.DeserializeElement(dataType, reader);
    }
}