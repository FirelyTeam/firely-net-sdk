using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializeDemoPatientJson
    {
        private static async Tasks.Task<ITypedElement> getJsonElement(string json, FhirJsonParsingSettings s = null) =>
            await JsonParsingHelpers.ParseToTypedElementAsync(json, new PocoStructureDefinitionSummaryProvider(), settings: s);

        [TestMethod]
        public async Tasks.Task CanSerializeThroughNavigatorAndCompare()
        {
            var json = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));

            var nav = await getJsonElement(json);
            var output = nav.ToJson();

            List<string> errors = [];
            JsonAssert.AreSame(@"TestData\fp-test-patient.json", json, output, errors);
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.IsEmpty(errors, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public async Tasks.Task TestPruneEmptyNodes()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "test-empty-nodes.json"));

            // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
            var nav = await getJsonElement(tp, new FhirJsonParsingSettings { PermissiveParsing = true });

            var output = nav.ToJson();
            var doc = JObject.Parse(output);
            Assert.AreEqual(17, doc.DescendantsAndSelf().Count());
        }

       
        [TestMethod]
        public async Tasks.Task CanSerializeFromPoco()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            var pat = FhirJsonDeserializer.OSTRICH.Deserialize<Patient>(tp);

            var output = pat.ToJson();

            var errors = new List<string>();
            JsonAssert.AreSame(@"TestData\fp-test-patient.json", tp, output, errors);
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.IsEmpty(errors, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public async Tasks.Task DoesPretty()
        {
            var json = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));

            var nav = await getJsonElement(json);
            var output = nav.ToJson();
            Assert.DoesNotContain('\n', output[..20]);
            var pretty = nav.ToJson(pretty: true);
            Assert.Contains('\n', pretty[..20]);

            var p = FhirJsonDeserializer.OSTRICH.Deserialize<Patient>(json);
            output = new FhirJsonSerializer().SerializeToString(p, pretty: false);
            Assert.DoesNotContain('\n', output[..20]);
            pretty = new FhirJsonSerializer().SerializeToString(p, pretty: true);
            Assert.Contains('\n', pretty[..20]);
        }
    }
}