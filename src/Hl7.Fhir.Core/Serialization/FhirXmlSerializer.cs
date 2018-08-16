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
            MakeNav(instance, summary).ToXml(settings: buildFhirXmlWriterSettings(), rootName: root);


        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null) => 
            MakeNav(instance, summary).ToXmlBytes(settings: buildFhirXmlWriterSettings(), rootName: root);

        // [WMR 20180409] NEW
        // https://github.com/ewoutkramer/fhir-net-api/issues/545
        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            var nav = MakeNav(instance, summary);

            return SerializationUtil.WriteXmlToDocument(w =>
            {
                var fhirWriter = new FhirXmlWriter(buildFhirXmlWriterSettings());
                fhirWriter.Write(nav, w, root);
            });
        }

        public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False, string root = null) =>
            MakeNav(instance, summary).WriteTo(writer, settings: buildFhirXmlWriterSettings(), rootName:root);
    }
}
