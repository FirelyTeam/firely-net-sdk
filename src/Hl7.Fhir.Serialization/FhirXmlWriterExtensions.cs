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

namespace Hl7.Fhir.Serialization
{
    public static class FhirXmlWriterExtensions
    {
        public static void WriteTo(this ISourceNode source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
            new FhirXmlWriter(settings).Write(source, destination, rootName);

        public static void WriteTo(this ITypedElement source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
            new FhirXmlWriter(settings).Write(source, destination, rootName);

#pragma warning disable 612, 618
        public static void WriteTo(this IElementNavigator source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
             source.ToTypedElement().WriteTo(destination, settings,rootName);
#pragma warning restore 612, 618

        public static string ToXml(this ISourceNode source, FhirXmlWriterSettings settings = null, string rootName = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName), settings?.Pretty ?? false);

        public static string ToXml(this ITypedElement source, FhirXmlWriterSettings settings = null, string rootName = null)
                => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName), settings?.Pretty ?? false);

#pragma warning disable 612, 618
        public static string ToXml(this IElementNavigator source, FhirXmlWriterSettings settings = null, string rootName = null)
        => SerializationUtil.WriteXmlToString(writer => source.WriteTo(writer, settings, rootName), settings?.Pretty ?? false);
#pragma warning restore 612, 618

        public static byte[] ToXmlBytes(this ITypedElement source, FhirXmlWriterSettings settings = null, string rootName = null)
                => SerializationUtil.WriteXmlToBytes(writer => source.WriteTo(writer, settings, rootName));
    }
}
