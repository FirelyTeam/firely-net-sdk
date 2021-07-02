using BenchmarkDotNet.Attributes;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Serialization.Poco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.Json;

namespace BenchmarkSdk
{
#if !DEBUG
    class Program
    {
        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<SerializerBenchmarks>();
        }

    }
#endif

    [MemoryDiagnoser]
    [TestClass]
    public class SerializerBenchmarks
    {

        private OperationOutcome setupOutcome()
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

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            oo = setupOutcome();
            _ = TypedSerialization.ToTypedElement(oo).ToJson();
        }

        [Benchmark]
        public void IDictionarySerializer()
        {
            var ms = new MemoryStream();
            var jw = new Utf8JsonWriter(ms);

            JsonSerializationExtensions.SerializeObject(oo, jw);
        }

        [Benchmark]
        [TestMethod]
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
