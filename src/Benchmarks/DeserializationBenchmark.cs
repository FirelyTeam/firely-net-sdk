using BenchmarkDotNet.Attributes;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class DeserializationBenchmarks
    {
        internal string JsonData;
        internal string XmlData;
        internal SourceNode JsonSourceNode;
        internal SourceNode XmlSourceNode;
        internal BaseFhirXmlDeserializer XmlDeserializer;
        internal BaseFhirJsonDeserializer JsonDeserializer;

        internal JsonSerializerOptions Options;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var jsonFileName = Path.Combine("TestData", "fp-test-patient.json");
            JsonData = File.ReadAllText(jsonFileName);

            var xmlFileName = Path.Combine("TestData", "fp-test-patient.xml");
            XmlData = File.ReadAllText(xmlFileName);

            XmlDeserializer = new FhirXmlDeserializer();
            JsonDeserializer = new FhirJsonDeserializer();

            JsonSourceNode = SourceNode.FromNode(FhirJsonNode.Parse(JsonData));
            XmlSourceNode = SourceNode.FromNode(FhirXmlNode.Parse(XmlData));

            Options = new JsonSerializerOptions().ForFhir();
        }

        [Benchmark]
        public Resource JsonDictionaryDeserializer()
        {
            try
            {
                return JsonSerializer.Deserialize<Patient>(JsonData, Options);
            }
            catch (DeserializationFailedException e)
            {
                return (Resource)e.PartialResult;
            }

        }

        [Benchmark]
        public Resource XmlDictionaryDeserializer()
        {
            using var xmlReader = XmlReader.Create(new StringReader(XmlData));
            try
            {
                return XmlDeserializer.DeserializeResource(xmlReader);
            }
            catch (DeserializationFailedException e)
            {
                return (Resource)e.PartialResult;
            }
        }


        [Benchmark(Baseline = true)]
        public Patient PocoBuilderViaBridgeJson()
        {
            return JsonSourceNode
                .ToTypedElement(ModelInfo.ModelInspector)
                .ToPoco<Patient>();
        }

        [Benchmark]
        public Patient PocoBuilderDirectJson()
        {
            return JsonSourceNode.ToPoco<Patient>();
        }

        [Benchmark]
        public Patient PocoBuilderViaBridgeXml()
        {
            return XmlSourceNode
                .ToTypedElement(ModelInfo.ModelInspector)
                .ToPoco<Patient>();
        }

        [Benchmark]
        public Patient PocoBuilderDirectXml()
        {
            return XmlSourceNode.ToPoco<Patient>();
        }
    }
}