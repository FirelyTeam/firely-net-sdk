using BenchmarkDotNet.Attributes;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class DeserializationBenchmarks
    {
        internal string JsonData;
        internal byte[] JsonDataBytes;
        internal string XmlData;
        internal BaseFhirXmlDeserializer XmlDeserializer;
        internal BaseFhirJsonDeserializer JsonDeserializer;
        internal XmlReader xmlreader;
        internal JsonSerializerOptions options;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var jsonFileName = Path.Combine("TestData", "fp-test-patient.json");
            JsonData = File.ReadAllText(jsonFileName);
            JsonDataBytes = Encoding.UTF8.GetBytes(JsonData);

            var xmlFileName = Path.Combine("TestData", "fp-test-patient.xml");
            XmlData = File.ReadAllText(xmlFileName);

            XmlDeserializer = new FhirXmlDeserializer(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));
            JsonDeserializer = new FhirJsonDeserializer(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));

            options = new JsonSerializerOptions().ForFhir();
        }

        [Benchmark]
        public Resource JsonDictionaryDeserializer()
        {
            var reader = new Utf8JsonReader(JsonDataBytes);
            try
            {
                return JsonDeserializer.DeserializeResource(ref reader);
            }
            catch (DeserializationFailedException e)
            {
                return (Resource)e.PartialResult;
            }

        }

        [Benchmark]
        public Resource XmlDictionaryDeserializer()
        {
            xmlreader = XmlReader.Create(new StringReader(XmlData));
            try
            {
                return XmlDeserializer.DeserializeResource(xmlreader);
            }
            catch (DeserializationFailedException e)
            {
                return (Resource)e.PartialResult;
            }
        }


        [Benchmark]
        public Patient TypedElementDeserializerJson()
        {
            return FhirJsonNode.Parse(JsonData).ToPoco<Patient>();
        }

        [Benchmark]
        public Resource TypedElementDeserializerXml()
        {
            return FhirXmlNode.Parse(XmlData).ToPoco<Patient>();
        }
    }
}