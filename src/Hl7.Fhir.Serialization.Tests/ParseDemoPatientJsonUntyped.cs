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
        public ISourceNode getJsonNodeU(string json, FhirJsonNavigatorSettings settings=null) => 
            FhirJsonNavigator.Untyped(json, settings:settings);

        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNodeU(tp);
            ParseDemoPatient.CanReadThroughNavigator(nav.ToElementNode(), typed: false);
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
                getJsonNodeU(jsonText, new FhirJsonNavigatorSettings { AllowJsonComments = true }));
        }

        [TestMethod]
        public void TryInvalidUntypedSource()
        {
            var xmlNav = FhirXmlNavigator.Untyped("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            try
            {
                var jsonWriter = new FhirJsonWriter();

                var output = SerializationUtil.WriteJsonToString(writer => jsonWriter.Write(xmlNav, writer));
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
            var nav = getJsonNodeU(bundle).ToElementNavigator();
            ParseDemoPatient.CheckBundleEntryNavigation(nav);
        }


        [TestMethod]
        public void CanReadEdgeCases()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");
            var patient = getJsonNodeU(tpJson, new FhirJsonNavigatorSettings { AllowJsonComments = true });

            Assert.AreEqual("Patient", patient.Name);
            Assert.AreEqual("Patient", patient.GetResourceType());

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
            Assert.AreEqual("Binary", contained[0].GetResourceType());

            // Check Patient.contained[1], an Organization
            Assert.AreEqual("contained", contained[1].Name);
            Assert.AreEqual("Organization", contained[1].GetResourceType());
        }

        [TestMethod]
        public void CatchesArrayMismatch()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4], '_a' : [2,4] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': 2, '_a' : [2] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4], '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ '_a': [4,5,6] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4] }", "test");
            Assert.IsTrue(nav.Children().Any());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null,2], '_a' : [{'active':true},null] }", "test");
            Assert.IsTrue(nav.Children().Any());
        }

        [TestMethod]
        public void CatchesUnsupportedFeatures()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': '   ' }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': {}, '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': {'active':true}, '_a': {'dummy':4} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': 3, '_a' : 4 }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ '_a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        public void CatchNullErrors()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ '_a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': null, '_a' : null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null], '_a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.VisitAll());
        }

        [TestMethod]
        public void PreservesParsingExceptionDetails()
        {
            try
            {
                var nav = FhirJsonNavigator.Untyped("<bla", "test");
                var dummy = nav.Text;
                Assert.Fail();
            }
            catch(FormatException fe)
            {
                Assert.IsInstanceOfType(fe.InnerException, typeof(JsonException));
            }                        
        }

        [TestMethod]
        public void DelayedParseErrors()
        {
            var text = "{";
            var patient = getJsonNodeU(text);

            var errors = patient.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("Invalid Json encountered"));
        }
    }
}