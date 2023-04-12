using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonFastSerializer : BaseFhirSerializer
    {
        public FhirJsonFastSerializer(Model.Version version) : base(version)
        {
        }

        public FhirJsonFastSerializer(SerializerSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null) =>
            Utility.SerializationUtil.WriteJsonToString(jsonWriter => Serialize(instance, jsonWriter, summary, elements), Settings.Pretty);

        public byte[] SerializeToBytes(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null) =>
            Utility.SerializationUtil.WriteJsonToBytes(jsonWriter => Serialize(instance, jsonWriter, summary, elements));

        public void Serialize(Base instance, JsonWriter writer, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            var serializerSink = new JsonSerializerSink(writer, Settings.Version, summary, elements);
            instance.Serialize(serializerSink);
        }
    }
}
