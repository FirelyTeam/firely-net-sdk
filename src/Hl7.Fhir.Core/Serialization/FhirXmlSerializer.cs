/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System.Xml;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlSerializer : BaseFhirSerializer
    {
        public FhirXmlSerializer(SerializerSettings settings = null) : base(settings)
        {
        }

        private FhirXmlSerializationSettings buildFhirXmlWriterSettings() =>
            new FhirXmlSerializationSettings { Pretty = Settings.Pretty, AppendNewLine = Settings.AppendNewLine, TrimWhitespaces = Settings.TrimWhiteSpacesInXml };

        /// <inheritdoc cref="SerializeToStringAsync(Base, SummaryType, string, string[])" />
        [Obsolete("Use SerializeToStringAsync(Base, SummaryType, string, string[]) instead.")]
        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
            .Rename(root)
            .ToXml(settings: buildFhirXmlWriterSettings());
        
        public async Tasks.Task<string> SerializeToStringAsync(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
                .Rename(root)
                .ToXmlAsync(settings: buildFhirXmlWriterSettings());

        /// <inheritdoc cref="SerializeToBytesAsync(Base, SummaryType, string, string[])" />
        [Obsolete("Use SerializeToBytesAsync(Base, SummaryType, string, string[]) instead.")]
        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
            .Rename(root)
            .ToXmlBytes(settings: buildFhirXmlWriterSettings());
        
        public async Tasks.Task<byte[]> SerializeToBytesAsync(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
                .Rename(root)
                .ToXmlBytesAsync(settings: buildFhirXmlWriterSettings());

        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
           MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
            .Rename(root)
            .ToXDocument(buildFhirXmlWriterSettings()).Rename(root);

        /// <inheritdoc cref="SerializeAsync(Base, XmlWriter, SummaryType, string, string[])" />
        [Obsolete("Use SerializeAsync(Base, SummaryType, string, string[]) instead.")]
        public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
            .Rename(root)
            .WriteTo(writer, settings: buildFhirXmlWriterSettings());
        
        public async Tasks.Task SerializeAsync(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False, string root = null, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false)
                .Rename(root)
                .WriteToAsync(writer, settings: buildFhirXmlWriterSettings());
    }
}
