/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
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
                doc.WriteTo(destination);

            await destination.FlushAsync().ConfigureAwait(false);
        }

        /// <inheritdoc cref="WriteToAsync(ISourceNode, XmlWriter, FhirXmlSerializationSettings)" />
        public static void WriteTo(this ISourceNode source, XmlWriter destination, FhirXmlSerializationSettings settings = null) => 
            new FhirXmlBuilder(settings).Build(source).writeTo(destination);

        public static async Task WriteToAsync(this ISourceNode source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            await new FhirXmlBuilder(settings).Build(source).writeToAsync(destination).ConfigureAwait(false);

        /// <inheritdoc cref="WriteToAsync(ITypedElement, XmlWriter, FhirXmlSerializationSettings)" />
        public static void WriteTo(this ITypedElement source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source).writeTo(destination);

        public static async Task WriteToAsync(this ITypedElement source, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            await new FhirXmlBuilder(settings).Build(source).writeToAsync(destination).ConfigureAwait(false);

        public static XDocument ToXDocument(this ISourceNode source, FhirXmlSerializationSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source);

        public static XDocument ToXDocument(this ITypedElement source, FhirXmlSerializationSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source);

        /// <inheritdoc cref="ToXmlAsync(ISourceNode, FhirXmlSerializationSettings)" />
        public static string ToXml(this ISourceNode source, FhirXmlSerializationSettings settings = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        public static async Task<string> ToXmlAsync(this ISourceNode source, FhirXmlSerializationSettings settings = null)
            => await SerializationUtil.WriteXmlToStringAsync(async writer => await source.WriteToAsync(writer, settings).ConfigureAwait(false), settings?.Pretty ?? false, settings?.AppendNewLine ?? false).ConfigureAwait(false);

        /// <inheritdoc cref="ToXmlAsync(ITypedElement, FhirXmlSerializationSettings)" />
        public static string ToXml(this ITypedElement source, FhirXmlSerializationSettings settings = null)
                => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        public static async Task<string> ToXmlAsync(this ITypedElement source, FhirXmlSerializationSettings settings = null)
            => await SerializationUtil.WriteXmlToStringAsync(async writer => await source.WriteToAsync(writer, settings).ConfigureAwait(false), settings?.Pretty ?? false, settings?.AppendNewLine ?? false).ConfigureAwait(false);

        /// <inheritdoc cref="ToXmlBytesAsync(ITypedElement, FhirXmlSerializationSettings)" />
        public static byte[] ToXmlBytes(this ITypedElement source, FhirXmlSerializationSettings settings = null)
                => SerializationUtil.WriteXmlToBytes(writer => source.WriteTo(writer, settings));

        public static async Task<byte[]> ToXmlBytesAsync(this ITypedElement source, FhirXmlSerializationSettings settings = null)
            => await SerializationUtil.WriteXmlToBytesAsync(async writer => await source.WriteToAsync(writer, settings).ConfigureAwait(false)).ConfigureAwait(false);
    }
}
