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
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.Serialization;

public static class XmlParsingExtensions
{
    /// <inheritdoc cref="Parse(BaseFhirXmlDeserializer,XmlReader,System.Type?)" />
    public static T Parse<T>(this BaseFhirXmlDeserializer deserializer, XmlReader reader) where T : Base =>
        (T)deserializer.Parse(reader, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirXmlDeserializer, XmlReader,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirXmlDeserializer deserializer, XmlReader reader) where T : Base
        => (T)(await deserializer.ParseAsync(reader, typeof(T)).ConfigureAwait(false));

    /// <inheritdoc cref="Parse(BaseFhirXmlDeserializer, string,System.Type?)" />
    public static T Parse<T>(this BaseFhirXmlDeserializer deserializer, string xml) where T : Base => (T)deserializer.Parse(xml, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirXmlDeserializer,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirXmlDeserializer deserializer, string xml) where T : Base
        => (T)(await deserializer.ParseAsync(xml, typeof(T)).ConfigureAwait(false));

    /// <summary>
    /// Deserializes the given XML string into a FHIR resource or datatype.
    /// </summary>
    /// <param name="deserializer">The parser for which this extension method can be called.</param>
    /// <param name="xml">A string of FHIR XML.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirXmlDeserializer deserializer, string xml, Type? dataType = null)
    {
        using var xmlReader = SerializationUtil.XmlReaderFromXmlText(xml);
        return parse(deserializer, xmlReader, dataType);
    }

    /// <inheritdoc cref="Parse(BaseFhirXmlDeserializer,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirXmlDeserializer deserializer, string xml, Type? dataType = null) =>
        Task.FromResult(deserializer.Parse(xml, dataType));

    /// <summary>
    /// Deserializes the XML passed in the XmlReader into a FHIR resource or datatype.
    /// </summary>
    /// <param name="deserializer">The parser for which this extension method can be called.</param>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirXmlDeserializer deserializer, XmlReader reader, Type? dataType = null) =>
        parse(deserializer, reader, dataType);

    /// <inheritdoc cref="ParseAsync(BaseFhirXmlDeserializer,XmlReader, Type)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirXmlDeserializer deserializer, XmlReader reader, Type? dataType = null) =>
        Task.FromResult(deserializer.Parse(reader, dataType));

    private static Base parse(BaseFhirXmlDeserializer deserializer, XmlReader reader, Type? dataType = null)
    {
        if (dataType is null || typeof(Resource).IsAssignableFrom(dataType))
            return deserializer.DeserializeResource(reader);

        return deserializer.DeserializeElement(dataType, reader);
    }
}