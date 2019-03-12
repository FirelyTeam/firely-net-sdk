/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public static class PocoSerializationExtensions
    {
        public static string ToJson(this Base source, Model.Version version, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToJson(settings);
        public static byte[] ToJsonBytes(this Base source, Model.Version version, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToJsonBytes(settings);
        public static void WriteTo(this Base source, Model.Version version, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement(version).WriteTo(destination, settings);
        public static JObject ToJObject(this Base source, Model.Version version, FhirJsonSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToJObject(settings);

        public static string ToXml(this Base source, Model.Version version, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToXml(settings);
        public static byte[] ToXmlBytes(this Base source, Model.Version version, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToXmlBytes(settings);
        public static void WriteTo(this Base source, Model.Version version, XmlWriter destination, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement(version).WriteTo(destination, settings);
        public static XDocument ToXDocument(this Base source, Model.Version version, FhirXmlSerializationSettings settings = null) =>
            source.ToTypedElement(version).ToXDocument(settings);
    }
}
