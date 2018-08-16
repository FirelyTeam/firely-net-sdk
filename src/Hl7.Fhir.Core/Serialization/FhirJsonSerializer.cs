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
            MakeNav(instance, summary).ToJson(buildFhirJsonWriterSettings());

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False) => 
            MakeNav(instance, summary).ToJsonBytes(buildFhirJsonWriterSettings());

        public void Serialize(Base instance, JsonWriter writer, SummaryType summary = SummaryType.False, string root = null) =>
            MakeNav(instance, summary).WriteTo(writer, buildFhirJsonWriterSettings());
    }
}
