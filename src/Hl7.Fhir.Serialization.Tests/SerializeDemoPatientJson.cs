using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializeDemoPatientJson
    {
        public ITypedElement getJsonElement(string json, FhirJsonParsingSettings s = null) => 
            JsonParsingHelpers.ParseToTypedElement(json, new PocoStructureDefinitionSummaryProvider(Version.DSTU2), settings: s);

        [TestMethod]
        public void CanSerializeThroughNavigatorAndCompare()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));

            var nav = getJsonElement(json);
            var output = nav.ToJson();
            JsonAssert.AreSame(json, output);
        }

        [TestMethod]
        public void TestPruneEmptyNodes()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "test-empty-nodes.json"));

            // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
            var nav = getJsonElement(tp, new FhirJsonParsingSettings { PermissiveParsing = true });

            var output = nav.ToJson();
            var doc = JObject.Parse(output);
            Assert.AreEqual(17, doc.DescendantsAndSelf().Count());
        }

       
        [TestMethod]
        public void CanSerializeFromPoco()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var pser = new FhirJsonParser(new ParserSettings(Version.DSTU2) { DisallowXsiAttributesOnRoot = false } );
            var pat = pser.Parse<Model.DSTU2.Patient>(tp);

            var output = pat.ToJson(Version.DSTU2);
            JsonAssert.AreSame(tp, output);
        }

        [TestMethod]
        public void CanStreamingSerializeFromPoco()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var pser = new FhirJsonParser(new ParserSettings(Version.DSTU2) { DisallowXsiAttributesOnRoot = false });
            var pat = pser.Parse<Model.DSTU2.Patient>(tp);

            var output = new FhirJsonStreamingSerializer(Version.DSTU2).SerializeToString(pat);
            JsonAssert.AreSame(tp, output);
        }

        [TestMethod]
        public void DoesPretty()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));

            var nav = getJsonElement(json);
            var output = nav.ToJson();
            Assert.IsFalse(output.Substring(0, 20).Contains('\n'));
            var pretty = nav.ToJson(new FhirJsonSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 20).Contains('\n'));

            var p = (new FhirJsonParser(Version.DSTU2)).Parse<Model.DSTU2.Patient>(json);
            output = (new FhirJsonSerializer(new SerializerSettings(Version.DSTU2) { Pretty = false })).SerializeToString(p);
            Assert.IsFalse(output.Substring(0, 20).Contains('\n'));
            pretty = (new FhirJsonSerializer(new SerializerSettings(Version.DSTU2) { Pretty = true })).SerializeToString(p);
            Assert.IsTrue(pretty.Substring(0, 20).Contains('\n'));
        }

        [TestMethod]
        public void DoesPrettyStreaming()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));

            var nav = getJsonElement(json);
            var output = nav.ToJson();
            Assert.IsFalse(output.Substring(0, 20).Contains('\n'));
            var pretty = nav.ToJson(new FhirJsonSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 20).Contains('\n'));

            var p = (new FhirJsonParser(Version.DSTU2)).Parse<Model.DSTU2.Patient>(json);
            output = (new FhirJsonStreamingSerializer(new SerializerSettings(Version.DSTU2) { Pretty = false })).SerializeToString(p);
            Assert.IsFalse(output.Substring(0, 20).Contains('\n'));
            pretty = (new FhirJsonStreamingSerializer(new SerializerSettings(Version.DSTU2) { Pretty = true })).SerializeToString(p);
            Assert.IsTrue(pretty.Substring(0, 20).Contains('\n'));
        }
    }
}