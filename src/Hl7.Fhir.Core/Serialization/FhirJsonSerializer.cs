/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonSerializer : BaseFhirSerializer
    {
        public FhirJsonSerializer(SerializerSettings settings = null) : base(settings)
        {
        }

        private FhirJsonSerializationSettings buildFhirJsonWriterSettings() =>
            new FhirJsonSerializationSettings { Pretty = Settings.Pretty, AppendNewLine = Settings.AppendNewLine };

        /// <inheritdoc cref="SerializeToStringAsync(Base, SummaryType, string[])" />
        [Obsolete("Use SerializeToStringAsync(Base, SummaryType, string[]) instead.")]
        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).ToJson(buildFhirJsonWriterSettings());

        public async Tasks.Task<string> SerializeToStringAsync(Base instance, SummaryType summary = SummaryType.False, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).ToJsonAsync(buildFhirJsonWriterSettings());

        /// <inheritdoc cref="SerializeToBytesAsync(Base, SummaryType, string[])" />
        [Obsolete("Use SerializeToBytesAsync(Base, SummaryType, string[]) instead.")]
        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).ToJsonBytes(buildFhirJsonWriterSettings());

        public async Tasks.Task<byte[]> SerializeToBytesAsync(Base instance, SummaryType summary = SummaryType.False, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).ToJsonBytesAsync(buildFhirJsonWriterSettings());

        public JObject SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).ToJObject(buildFhirJsonWriterSettings());

        /// <inheritdoc cref="SerializeAsync(Base, JsonWriter, SummaryType, string[])" />
        [Obsolete("Use SerializeAsync(Base, JsonWriter, SummaryType, string[]) instead.")]
        public void Serialize(Base instance, JsonWriter writer, SummaryType summary = SummaryType.False, string[] elements = null) =>
            MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).WriteTo(writer, buildFhirJsonWriterSettings());

        public async Tasks.Task SerializeAsync(Base instance, JsonWriter writer, SummaryType summary = SummaryType.False, string[] elements = null) =>
            await MakeElementStack(instance, summary, elements, Settings?.IncludeMandatoryInElementsSummary ?? false).WriteToAsync(writer, buildFhirJsonWriterSettings());
    }
}
