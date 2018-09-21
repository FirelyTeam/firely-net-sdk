/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public static class FhirXmlBuilderExtensions
    {
        private static void writeTo(this XDocument doc, XmlWriter destination)
        {
            if (doc.Root != null)
                doc.WriteTo(destination);

            destination.Flush();
        }

        public static void WriteTo(this ISourceNode source, XmlWriter destination, FhirXmlBuilderSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source).writeTo(destination);

        public static void WriteTo(this ITypedElement source, XmlWriter destination, FhirXmlBuilderSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source).writeTo(destination);

        public static XDocument ToXDocument(this ISourceNode source, FhirXmlBuilderSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source);

        public static XDocument ToXDocument(this ITypedElement source, FhirXmlBuilderSettings settings = null) =>
            new FhirXmlBuilder(settings).Build(source);

#pragma warning disable 612, 618
        public static void WriteTo(this IElementNavigator source, XmlWriter destination, FhirXmlBuilderSettings settings = null) =>
             source.ToTypedElement().WriteTo(destination, settings);
#pragma warning restore 612, 618

        public static string ToXml(this ISourceNode source, FhirXmlBuilderSettings settings = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);

        public static string ToXml(this ITypedElement source, FhirXmlBuilderSettings settings = null)
                => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);

#pragma warning disable 612, 618
        public static string ToXml(this IElementNavigator source, FhirXmlBuilderSettings settings = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);
#pragma warning restore 612, 618

        public static byte[] ToXmlBytes(this ITypedElement source, FhirXmlBuilderSettings settings = null)
                => SerializationUtil.WriteXmlToBytes(writer => source.WriteTo(writer, settings));
    }
}
