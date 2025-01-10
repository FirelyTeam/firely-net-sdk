/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using PocoNode = Hl7.Fhir.ElementModel.PocoNode;

namespace Hl7.Fhir.Serialization;

public static class FhirXmlBuilderExtensions
{
    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into FHIR Xml.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
    public static void WriteTo(this ISourceNode source, XmlWriter writer) =>
        new FhirXmlBuilder().Build(source).writeTo(writer);

    /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ISourceNode,System.Xml.XmlWriter)"/>
    public static async Task WriteToAsync(this ISourceNode source, XmlWriter destination) =>
        await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance to FHIR Xml.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    public static void WriteTo(this ITypedElement source, XmlWriter writer) =>
        new FhirXmlBuilder().Build(source).writeTo(writer);

    /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ITypedElement,System.Xml.XmlWriter)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task WriteToAsync(this ITypedElement source, XmlWriter destination) =>
        await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into a <see cref="XDocument"/>.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
    public static XDocument ToXDocument(this ISourceNode source) =>
        new FhirXmlBuilder().Build(source);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a <see cref="XDocument"/>
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    public static XDocument ToXDocument(this ITypedElement source) =>
        new FhirXmlBuilder().Build(source);

    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into a FHIR Xml string.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
    public static string ToXml(this ISourceNode source, bool pretty = false)
        => SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

    /// <inheritdoc cref="ToXml(Hl7.Fhir.ElementModel.ISourceNode,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToXmlAsync(this ISourceNode source, bool pretty = false)
        => await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Xml string.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    public static string ToXml(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode node)
            return SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

        return serializePocoNode(node, pretty);
    }

    /// <inheritdoc cref="ToXml(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToXmlAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode node)
            return await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

        return serializePocoNode(node, pretty);
    }

    private static string serializePocoNode(PocoNode pn, bool pretty)
    {
        var serializer = new BaseFhirXmlSerializer(pn.FindInspector() ?? ModelInspector.ForType(pn.Poco.GetType()));

        // If we are serializing a subtree of a resource, then if the current node is a datatype or a nested resource,
        // we need to pick a name for this root element.
        var pickElementName = pn.Poco is not Resource || pn.Parent is not null;
        var rootName = pickElementName ? pn.Name : null;

        return serializer.SerializeToString(pn.Poco, pretty, rootName: rootName);
    }

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Xml byte array.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    public static byte[] ToXmlBytes(this ITypedElement source, bool pretty = false)
        => SerializationUtil.WriteXmlToBytes(source.WriteTo, pretty);

    /// <inheritdoc cref="ToXmlBytes(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<byte[]> ToXmlBytesAsync(this ITypedElement source, bool pretty = false)
        => await SerializationUtil.WriteXmlToBytesAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

    private static void writeTo(this XDocument doc, XmlWriter destination)
    {
        if (doc.Root != null)
            doc.WriteTo(destination);

        destination.Flush();
    }

    private static async Task writeToAsync(this XDocument doc, XmlWriter destination)
    {
        if (doc.Root != null)
            await doc.WriteToAsync(destination, CancellationToken.None);

        await destination.FlushAsync().ConfigureAwait(false);
    }
}