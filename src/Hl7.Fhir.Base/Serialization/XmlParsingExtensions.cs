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
    /// <inheritdoc cref="Parse(BaseFhirXmlParser,XmlReader,System.Type?)" />
    public static T Parse<T>(this BaseFhirXmlParser parser, XmlReader reader) where T : Base =>
        (T)parser.Parse(reader, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirXmlParser, XmlReader,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirXmlParser parser, XmlReader reader) where T : Base
        => (T)(await parser.ParseAsync(reader, typeof(T)).ConfigureAwait(false));

    /// <inheritdoc cref="Parse(BaseFhirXmlParser, string,System.Type?)" />
    public static T Parse<T>(this BaseFhirXmlParser parser, string xml) where T : Base => (T)parser.Parse(xml, typeof(T));

    /// <inheritdoc cref="Parse(BaseFhirXmlParser,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static async Task<T> ParseAsync<T>(this BaseFhirXmlParser parser, string xml) where T : Base
        => (T)(await parser.ParseAsync(xml, typeof(T)).ConfigureAwait(false));

    /// <summary>
    /// Deserializes the given XML string into a FHIR resource or datatype.
    /// </summary>
    /// <param name="parser">The parser for which this extension method can be called.</param>
    /// <param name="xml">A string of FHIR XML.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirXmlParser parser, string xml, Type? dataType = null)
    {
        using var xmlReader = SerializationUtil.XmlReaderFromXmlText(xml);
        return parse(parser, xmlReader, dataType);
    }

    /// <inheritdoc cref="Parse(BaseFhirXmlParser,string,System.Type?)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirXmlParser parser, string xml, Type? dataType = null) =>
        Task.FromResult(parser.Parse(xml, dataType));

    /// <summary>
    /// Deserializes the XML passed in the XmlReader into a FHIR resource or datatype.
    /// </summary>
    /// <param name="parser">The parser for which this extension method can be called.</param>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="dataType">Optional. Can be used when deserializing datatypes and
    /// will be ignored when parsing resources. </param>
    /// <remarks>Note that there is no official serialization for FHIR datatypes, just for FHIR resources, so
    /// deserializing non-resource types might not always work.</remarks>
    public static Base Parse(this BaseFhirXmlParser parser, XmlReader reader, Type? dataType = null) =>
        parse(parser, reader, dataType);

    /// <inheritdoc cref="ParseAsync(BaseFhirXmlParser,XmlReader, Type)" />
    [Obsolete("The current parsers do not support async parsing, so this method is synchronous and " +
              "you should explicitly call Parse instead.")]
    public static Task<Base> ParseAsync(this BaseFhirXmlParser parser, XmlReader reader, Type? dataType = null) =>
        Task.FromResult(parser.Parse(reader, dataType));

    private static Base parse(BaseFhirXmlParser parser, XmlReader reader, Type? dataType = null)
    {
        if (dataType is null || typeof(Resource).IsAssignableFrom(dataType))
            return parser.DeserializeResource(reader);

        return parser.DeserializeElement(dataType, reader);
    }
}