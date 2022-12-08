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
        internal TestPatient Patient;
        JsonSerializerOptions Options;
        FhirXmlPocoSerializer XmlSerializer;


        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var filename = Path.Combine("TestData", "fp-test-patient.json");
            var data = File.ReadAllText(filename);
            // For now, deserialize with the existing deserializer, until we have completed
            // the dynamicserializer too.
            Patient = FhirJsonNode.Parse(data).ToPoco<TestPatient>(ModelInspector.ForType<TestPatient>());
            Options = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);
            XmlSerializer = new FhirXmlPocoSerializer(Hl7.Fhir.Specification.FhirRelease.STU3);
        }

        [Benchmark]
        public string JsonDictionarySerializer()
        {
            return JsonSerializer.Serialize(Patient, Options);
        }

        [Benchmark]
        public string XmlDictionarySerializer()
        {
            return SerializationUtil.WriteXmlToString(Patient, (o, w) => XmlSerializer.Serialize(o, w));
        }

        [Benchmark]
        public string TypedElementSerializerJson()
        {
            return Patient.ToTypedElement(ModelInspector.ForType<TestPatient>()).ToJson();
        }

        [Benchmark]
        public string TypedElementSerializerXml()
        {
            return Patient.ToTypedElement(ModelInspector.ForType<TestPatient>()).ToXml();
        }
    }
}
