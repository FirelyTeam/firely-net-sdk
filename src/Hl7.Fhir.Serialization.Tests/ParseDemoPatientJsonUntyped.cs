using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.Introspection;
using Hl7.FhirPath;
using Newtonsoft.Json;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientJsonUntyped
    {
        public ISourceNode getJsonNodeU(string json, FhirJsonNodeSettings settings=null) => 
            FhirJsonNode.Parse(json, settings:settings);

        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNodeU(tp);
#pragma warning disable 612, 618
            ParseDemoPatient.CanReadThroughNavigator(nav.ToTypedElement(), typed: false);
#pragma warning restore 612, 618
        }

        [TestMethod]
        public void ElementNavPerformanceUntypedJson()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNodeU(tp);
            ParseDemoPatient.ElementNavPerformance(nav);
        }

        [TestMethod]
        public void ProducesCorrectUntypedLocations()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var patient = getJsonNodeU(tp);

            ParseDemoPatient.ProducesCorrectUntypedLocations(patient);
        }

        [TestMethod]
        public void HasLineNumbers()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNodeU(tp);

            ParseDemoPatient.HasLineNumbers<JsonSerializationDetails>(nav);
        }

        [TestMethod]
        public void RoundtripJsonUntyped()
        {
            ParseDemoPatient.RoundtripJson(jsonText => 
                getJsonNodeU(jsonText, new FhirJsonNodeSettings { AllowJsonComments = true }));
        }

        [TestMethod]
        public void TryInvalidUntypedSource()
        {
            var xmlNav = FhirXmlNode.Parse("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            try
            {
                var output = xmlNav.ToJson();
                Assert.Fail();
            }
            catch (NotSupportedException)
            {
            }
        }

        [TestMethod]
        public void CheckBundleEntryNavigation()
        {
            var bundle = File.ReadAllText(@"TestData\BundleWithOneEntry.json");
            var nav = getJsonNodeU(bundle);
#pragma warning disable 612,618
            ParseDemoPatient.CheckBundleEntryNavigation(nav.ToTypedElement());
#pragma warning restore 612, 618
        }


        [TestMethod]
        public void CanReadEdgeCases()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");
            var patient = getJsonNodeU(tpJson, new FhirJsonNodeSettings { AllowJsonComments = true });

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
        public void CatchesArrayMismatch()
        {
            var nav = FhirJsonNode.Parse("{ 'a': [2,3,4], '_a' : [2,4] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': 2, '_a' : [2] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': [2,3,4], '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ '_a': [4,5,6] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': [2,3,4] }", "test");
            Assert.IsTrue(nav.Children().Any());

            nav = FhirJsonNode.Parse("{ 'a': [null,2], '_a' : [{'active':true},null] }", "test");
            Assert.IsTrue(nav.Children().Any());
        }

        [TestMethod]
        public void CatchesUnsupportedFeatures()
        {
            var nav = FhirJsonNode.Parse("{ 'a': '   ' }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': {}, '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': {'active':true}, '_a': {'dummy':4} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': 3, '_a' : 4 }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ '_a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        public void CatchNullErrors()
        {
            var nav = FhirJsonNode.Parse("{ 'a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ '_a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': null, '_a' : null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNode.Parse("{ 'a': [null], '_a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        public void PreservesParsingExceptionDetails()
        {
            try
            {
                var nav = FhirJsonNode.Parse("<bla", "test");
                var dummy = nav.Text;
                Assert.Fail();
            }
            catch(FormatException fe)
            {
                Assert.IsInstanceOfType(fe.InnerException, typeof(JsonException));
            }                        
        }

        [TestMethod]
        public void CatchParseErrors()
        {
            var text = "{";

            try
            {
                var patient = getJsonNodeU(text);
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Json encountered"));
            }
        }
    }
}