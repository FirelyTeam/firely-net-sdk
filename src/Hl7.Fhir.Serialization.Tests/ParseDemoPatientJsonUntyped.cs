using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientJsonUntyped
    {
        public async Task<ISourceNode> getJsonNodeU(string json, FhirJsonParsingSettings settings = null) =>
            await FhirJsonNode.ParseAsync(json, settings: settings);

        async Task<ISourceNode> FhirJsonNodeParse(string json, string rootName) =>
               await FhirJsonNode.ParseAsync(json, rootName, new FhirJsonParsingSettings() { PermissiveParsing = false });

        [TestMethod]
        public async Task CanReadThroughUntypedNavigator()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = await getJsonNodeU(tp);
#pragma warning disable 612, 618
            ParseDemoPatient.CanReadThroughTypedElement(nav.ToTypedElement(), typed: false);
#pragma warning restore 612, 618
        }

        [TestMethod]
        public async Task ElementNavPerformanceUntypedJson()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = await getJsonNodeU(tp);
            ParseDemoPatient.ElementNavPerformance(nav);
        }

        [TestMethod]
        public async Task ProducesCorrectUntypedLocations()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            var patient = await getJsonNodeU(tp);

            ParseDemoPatient.ProducesCorrectUntypedLocations(patient);
        }

        [TestMethod]
        public async Task HasLineNumbers()
        {
            var tp = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = await getJsonNodeU(tp);

            ParseDemoPatient.HasLineNumbers<JsonSerializationDetails>(nav);
        }

        [TestMethod]
        public async Task RoundtripJsonUntyped()
        {
            await ParseDemoPatient.RoundtripJson(async jsonText =>
                await getJsonNodeU(jsonText, new FhirJsonParsingSettings { AllowJsonComments = true }));
        }

        [TestMethod]
        public async Task TryInvalidUntypedSource()
        {
            var xmlNav = await FhirXmlNode.ParseAsync("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            try
            {
                var output = await xmlNav.ToJsonAsync();
                Assert.Fail();
            }
            catch (NotSupportedException)
            {
            }
        }

        [TestMethod]
        public async Task CheckBundleEntryNavigation()
        {
            var bundle = await File.ReadAllTextAsync(Path.Combine("TestData", "BundleWithOneEntry.json"));
            var nav = await getJsonNodeU(bundle);
#pragma warning disable 612,618
            ParseDemoPatient.CheckBundleEntryNavigation(nav.ToTypedElement());
#pragma warning restore 612, 618
        }

        [TestMethod]
        public async Task CanReadEdgeCases()
        {
            var tpJson = await File.ReadAllTextAsync(Path.Combine("TestData", "json-edge-cases.json"));
            var patient = await getJsonNodeU(tpJson, new FhirJsonParsingSettings { AllowJsonComments = true });

            Assert.AreEqual("Patient", patient.Name);
            Assert.AreEqual("Patient", patient.GetResourceTypeIndicator());

            // Move into child Patient.identifier
            var patientC = patient.Children().ToList();
            var identifier = patientC[0];
            Assert.IsNull(identifier.Text);

            // Move to child Patient.identifier.period

            // Move to child Patient.identifier.period.start
            var start = patient.Children("identifier").Children("period").Children("start").First();
            Assert.AreEqual("start", start.Name);
            Assert.AreEqual("2001-05-06", start.Text);

            // Check the value + extensions on Patient.identifier.value
            Assert.AreEqual("12345", identifier.Children("value").Single().Text);
            Assert.AreEqual(1, patient.Children("identifier").Count());

            // Move to sibling Patient.managingOrganization
            Assert.AreEqual("managingOrganization", patient.Children("managingOrganization").First().Name);

            // Move to sibling Patient.name (2x)
            Assert.AreEqual(2, patient.Children("name").Count());

            var db = patient.Children("deceasedBoolean").Single();
            Assert.AreEqual("deceasedBoolean", db.Name);
            Assert.AreEqual("true", db.Text);
            var details = (db as IAnnotated).Annotation<JsonSerializationDetails>();
            Assert.AreEqual(true, details.OriginalValue);

            Assert.AreEqual("3", patient.Children("multipleBirthInteger").Single().Text);

            // Check Patient.contained[0], a Binary
            var contained = patient.Children("contained").ToList();

            Assert.AreEqual("contained", contained[0].Name);
            Assert.AreEqual("Binary", contained[0].GetResourceTypeIndicator());

            // Check Patient.contained[1], an Organization
            Assert.AreEqual("contained", contained[1].Name);
            Assert.AreEqual("Organization", contained[1].GetResourceTypeIndicator());
        }

        [TestMethod]
        public async Task CatchesArrayMismatch()
        {
            var nav = await FhirJsonNodeParse("{ 'a': [2,3,4], '_a' : [2,4] }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': 2, '_a' : [2] }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': [2,3,4], '_a' : {} }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ '_a': [4,5,6] }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': [2,3,4] }", "test");
            Assert.IsTrue(nav.Children().Any());

            nav = await FhirJsonNodeParse("{ 'a': [null,2], '_a' : [{'active':true},null] }", "test");
            Assert.IsTrue(nav.Children().Any());
        }

        [TestMethod]
        public async Task CatchesUnsupportedFeatures()
        {
            var nav = await FhirJsonNodeParse("{ 'a': '   ' }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': {}, '_a' : {} }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': {'active':true}, '_a': {'dummy':4} }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ '_a' : {} }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': 3, '_a' : 4 }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': new DateTime() }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ '_a': new DateTime() }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Expected an InvalidOperationException about resourceType is missing.")]
        public async Task CatchResourceTypeMissing()
        {
            var json = "{  \"resourceType\": \"\",  \"id\": \"rt1\",  \"meta\": {\"lastUpdated\": \"2020-04-23T13:45:32Z\"  } }";
            _ = await FhirJsonNodeParse(json, null);
        }

        [TestMethod]
        public async Task CatchNullErrors()
        {
            var nav = await FhirJsonNodeParse("{ 'a': null }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ '_a': null }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': null, '_a' : null }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': [null] }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());

            nav = await FhirJsonNodeParse("{ 'a': [null], '_a': [null] }", "test");
            ExceptionAssert.Throws<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        public async Task PreservesParsingExceptionDetails()
        {
            try
            {
                var nav = await FhirJsonNode.ParseAsync("<bla", "test");
                var dummy = nav.Text;
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsInstanceOfType(fe.InnerException, typeof(JsonException));
            }
        }

        [TestMethod]
        public async Task CatchParseErrors()
        {
            var text = "{";

            try
            {
                var patient = await getJsonNodeU(text);
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Json encountered"));
            }
        }
    }
}
