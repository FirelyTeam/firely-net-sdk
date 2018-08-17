/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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

        private FhirXmlWriterSettings buildFhirXmlWriterSettings() =>
            new FhirXmlWriterSettings { Pretty = Settings.Pretty };

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False, string root = null) => 
            MakeElementStack(instance, summary)
            .Rename(root)
            .ToXml(settings: buildFhirXmlWriterSettings());

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null) => 
            MakeElementStack(instance, summary)
            .Rename(root)
            .ToXmlBytes(settings: buildFhirXmlWriterSettings());

        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null) =>
           MakeElementStack(instance, summary)
            .Rename(root)
            .ToXDocument(buildFhirXmlWriterSettings()).Rename(root);

        public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False, string root = null) =>
            MakeElementStack(instance, summary)
            .Rename(root)
            .WriteTo(writer, settings: buildFhirXmlWriterSettings());
    }
}
