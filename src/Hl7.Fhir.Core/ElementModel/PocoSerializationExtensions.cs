/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using System.Xml;

namespace Hl7.Fhir.ElementModel
{
    public static class PocoSerializationExtensions
    {
        public static string ToJson(this Base source, FhirJsonWriterSettings settings = null) =>
            source.ToTypedElement().ToJson(settings);
        public static byte[] ToJsonBytes(this Base source, FhirJsonWriterSettings settings = null) =>
            source.ToTypedElement().ToJsonBytes(settings);
        public static void WriteTo(this Base source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);

        public static string ToXml(this Base source, FhirXmlWriterSettings settings = null, string rootName = null) =>
            source.ToTypedElement().ToXml(settings, rootName);
        public static byte[] ToXmlBytes(this Base source, FhirXmlWriterSettings settings = null, string rootName = null) =>
            source.ToTypedElement().ToXmlBytes(settings, rootName);
        public static void WriteTo(this Base source, XmlWriter destination, FhirXmlWriterSettings settings = null, string rootName = null) =>
            source.ToTypedElement().WriteTo(destination, settings, rootName);


    }
}
