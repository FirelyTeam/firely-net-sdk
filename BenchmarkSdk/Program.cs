using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Poco;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BenchmarkSdk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //_ = BenchmarkRunner.Run<SerializerBenchmarks>();
            _ = BenchmarkRunner.Run<DeserializerBenchmarks>();
        }
    }

    [MemoryDiagnoser]
    public class DeserializerBenchmarks
    {
        byte[] testInput;
        string testJson;
        JsonDynamicDeserializer dynDes;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var oo = SerializerBenchmarks.SetupOutcome();
            testJson = TypedSerialization.ToTypedElement(oo).ToJson();
            testInput = Encoding.UTF8.GetBytes(testJson);

            dynDes = new JsonDynamicDeserializer(typeof(OperationOutcome).Assembly);
        }

        [Benchmark]
        public Resource DynamicDeserializer()
        {
            var reader = new Utf8JsonReader(testInput);
            return dynDes.DeserializeResource(ref reader);
        }

        [Benchmark(Baseline = true)]
        public Resource GeneratedDeserializer()
        {
            var options = new JsonSerializerOptions();
            var reader = new Utf8JsonReader(testInput);

            return JsonStreamResourceConverter.PolymorphicRead(ref reader, options);
        }

        [Benchmark]
        public Resource TypedElementDeserializer()
        {
            var sourceNode = FhirJsonNode.Parse(testJson);
            return TypedSerialization.ToPoco<Resource>(sourceNode);
        }
    }

    [MemoryDiagnoser]
    public class SerializerBenchmarks
    {
        public static OperationOutcome SetupOutcome()
        {
            OperationOutcome oo = new OperationOutcome()
            {
                Id = "1",
                Meta = new Meta { Profile = new[] { "http://simplifier.net/profiles/x" }, VersionId = "2" }
            };

            var fu = new FhirUri();
            fu.SetStringExtension("http://ha.nl", "hi");
            oo.Meta.ProfileElement.Add(fu);

            oo.Issue.Add(
                new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.BusinessRule,
                    Details = new CodeableConcept("http://nu.nl", "then"),
                    Diagnostics = "This has low level information",
                    Expression = new[] { "Patient.x" },
                    Severity = OperationOutcome.IssueSeverity.Error
                });
            oo.Id = "1";

            return oo;
        }

        OperationOutcome oo;
        string testJson;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            oo = SetupOutcome();
            testJson = TypedSerialization.ToTypedElement(oo).ToJson();
        }

        [Benchmark]
        public void IDictionarySerializer()
        {
            var ms = new MemoryStream();
            var jw = new Utf8JsonWriter(ms);

            JsonSerializationExtensions.SerializeObject(oo, jw);
        }

        [Benchmark]
        public void CallbackSerializer()
        {
            var ms = new MemoryStream();
            var jw = new Utf8JsonWriter(ms);
            var ser = new JsonCallbackSerializer(jw);

            ser.SerializeObject(oo);
        }

        [Benchmark(Baseline = true)]
        public void GeneratedSerializer()
        {
            var options = new JsonSerializerOptions();
            var ms = new MemoryStream();
            var jw = new Utf8JsonWriter(ms);

            oo.SerializeJson(jw, options);
        }

        [Benchmark]
        public byte[] TypedElementSerializer()
        {
            return oo.ToJsonBytes();
        }
    }
}
