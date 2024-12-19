using BenchmarkDotNet.Attributes;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.IO;
using System.Text.Json;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class SerializationBenchmarks
    {
        internal Patient Patient;
        JsonSerializerOptions Options;
        BaseFhirXmlPocoSerializer XmlSerializer;


        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var filename = Path.Combine("TestData", "fp-test-patient.json");
            var data = File.ReadAllText(filename);
            // For now, deserialize with the existing deserializer, until we have completed
            // the dynamicserializer too.
            Patient = FhirJsonNode.Parse(data).ToPoco<Patient>();
            Options = new JsonSerializerOptions().ForFhir();
            XmlSerializer = new FhirXmlPocoSerializer();
        }

        [Benchmark]
        public string JsonDictionarySerializer()
        {
            return JsonSerializer.Serialize(Patient, Options);
        }

        [Benchmark]
        public string XmlDictionarySerializer()
        {
            return SerializationUtil.WriteXmlToString(w => XmlSerializer.Serialize(Patient, w));
        }

        [Benchmark]
        public string TypedElementSerializerJson()
        {
#pragma warning disable SDK0001
            return Patient.ToTypedElement().ToJson();
#pragma warning restore SDK0001
        }

        [Benchmark]
        public string TypedElementSerializerXml()
        {
#pragma warning disable SDK0001
            return Patient.ToTypedElement().ToXml();
#pragma warning restore SDK0001
        }
    }
}