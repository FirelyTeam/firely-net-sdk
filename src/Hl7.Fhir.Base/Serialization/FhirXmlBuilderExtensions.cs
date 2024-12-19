/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using PocoNode = Hl7.Fhir.ElementModel.PocoNode;

namespace Hl7.Fhir.Serialization;

public static class FhirXmlBuilderExtensions
{
    /// <inheritdoc cref="writeToAsync(XDocument, XmlWriter)" />
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

    public static void WriteTo(this ISourceNode source, XmlWriter destination) =>
        new FhirXmlBuilder().Build(source).writeTo(destination);

    public static async Task WriteToAsync(this ISourceNode source, XmlWriter destination) =>
        await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    public static void WriteTo(this ITypedElement source, XmlWriter destination) =>
        new FhirXmlBuilder().Build(source).writeTo(destination);

    public static async Task WriteToAsync(this ITypedElement source, XmlWriter destination) =>
        await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    public static XDocument ToXDocument(this ISourceNode source) =>
        new FhirXmlBuilder().Build(source);

    public static XDocument ToXDocument(this ITypedElement source) =>
        new FhirXmlBuilder().Build(source);

    public static string ToXml(this ISourceNode source, bool pretty = false)
        => SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

    public static async Task<string> ToXmlAsync(this ISourceNode source, bool pretty = false)
        => await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

    [TemporarilyChanged] // This works. Remove this attribute after writing new extensions on the pocos
    public static string ToXml(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: { } b } node)
            return SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

        var serializer = new BaseFhirXmlPocoSerializer(node.FindInspector() ?? ModelInspector.ForType(b.GetType()));
        return serializer.SerializeToString(b, pretty);
    }

    [TemporarilyChanged] // This works. Remove this attribute after writing new extensions on the pocos
    public static async Task<string> ToXmlAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode {Poco: {} b} node)
            return await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

        var serializer = new BaseFhirXmlPocoSerializer(node.FindInspector() ?? ModelInspector.ForType(b.GetType()));
        return serializer.SerializeToString(b, pretty);
    }

    public static byte[] ToXmlBytes(this ITypedElement source, bool pretty = false)
        => SerializationUtil.WriteXmlToBytes(source.WriteTo, pretty);

    public static async Task<byte[]> ToXmlBytesAsync(this ITypedElement source, bool pretty = false)
        => await SerializationUtil.WriteXmlToBytesAsync(source.WriteToAsync, pretty).ConfigureAwait(false);
}