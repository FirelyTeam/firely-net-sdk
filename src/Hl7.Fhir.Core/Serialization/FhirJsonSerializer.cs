/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonSerializer : BaseFhirSerializer
    {
        public FhirJsonSerializer(SerializerSettings settings = null) : base(settings)
        {
        }

        private FhirJsonWriterSettings buildFhirJsonWriterSettings() =>
            new FhirJsonWriterSettings { Pretty = Settings.Pretty };

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False) => 
            MakeElementStack(instance, summary).ToJson(buildFhirJsonWriterSettings());

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False) => 
            MakeElementStack(instance, summary).ToJsonBytes(buildFhirJsonWriterSettings());

        public JObject SerializeToDocument(Base instance, SummaryType summary = SummaryType.False) => 
            MakeElementStack(instance, summary).ToJObject(buildFhirJsonWriterSettings());

        public void Serialize(Base instance, JsonWriter writer, SummaryType summary = SummaryType.False) =>
            MakeElementStack(instance, summary).WriteTo(writer, buildFhirJsonWriterSettings());
    }
}
