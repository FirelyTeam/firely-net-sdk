/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlSerializer : BaseFhirSerializer
    {
        public FhirXmlSerializer(SerializerSettings settings=null) : base(settings)
        {
        }

        private FhirXmlSerializationSettings buildFhirXmlWriterSettings() =>
            new FhirXmlSerializationSettings { Pretty = Settings.Pretty };

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) => 
            MakeElementStack(instance, summary, elements)
            .Rename(root)
            .ToXml(settings: buildFhirXmlWriterSettings());

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) => 
            MakeElementStack(instance, summary, elements)
            .Rename(root)
            .ToXmlBytes(settings: buildFhirXmlWriterSettings());

        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
           MakeElementStack(instance, summary, elements)
            .Rename(root)
            .ToXDocument(buildFhirXmlWriterSettings()).Rename(root);

        public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            MakeElementStack(instance, summary, elements)
            .Rename(root)
            .WriteTo(writer, settings: buildFhirXmlWriterSettings());
    }
}
