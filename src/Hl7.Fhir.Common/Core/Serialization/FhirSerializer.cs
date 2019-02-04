/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using Newtonsoft.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization
{

    [Obsolete("Obsolete. Instead, create a new FhirXmlSerializer or FhirJsonSerializer instance and call one of the serialization methods.")]
    public static class FhirSerializer
    {
        private static FhirXmlSerializer _xmlSerializer = new FhirXmlSerializer();
        private static FhirJsonSerializer _jsonSerializer = new FhirJsonSerializer();

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
        public static string SerializeResourceToXml(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToString(resource, summary);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
        public static string SerializeToXml(Base data, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToString(data, summary, root);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
        public static byte[] SerializeResourceToXmlBytes(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToBytes(resource, summary);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
        public static byte[] SerializeToXmlBytes(Base instance, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToBytes(instance, summary, root);

        [Obsolete("Create a new FhirXmlSerializer and call Serialize()")]
        public static void SerializeResource(Resource resource, XmlWriter writer, SummaryType summary = SummaryType.False) => 
            _xmlSerializer.Serialize(resource, writer, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
        public static string SerializeResourceToJson(Resource resource, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToString(resource, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
        public static string SerializeToJson(Base instance, SummaryType summary = SummaryType.False) => 
            _jsonSerializer.SerializeToString(instance, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
        public static byte[] SerializeResourceToJsonBytes(Resource resource, SummaryType summary = SummaryType.False) => 
            _jsonSerializer.SerializeToBytes(resource, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
        public static byte[] SerializeToJsonBytes(Base instance, SummaryType summary = SummaryType.False) => 
            _jsonSerializer.SerializeToBytes(instance, summary);

        [Obsolete("Create a new FhirJsonSerializer and call Serialize()")]
        public static void SerializeResource(Resource resource, JsonWriter writer, SummaryType summary = SummaryType.False) =>
            _jsonSerializer.Serialize(resource, writer, summary);
    }  
}
