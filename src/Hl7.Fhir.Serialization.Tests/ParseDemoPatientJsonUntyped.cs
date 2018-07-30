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

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientJsonUntyped
    {
        public ISourceNavigator getJsonNavU(string json, FhirJsonNavigatorSettings settings=null) => FhirJsonNavigator.Untyped(json, settings:settings);

        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNavU(tp);
            ParseDemoPatient.CanReadThroughNavigator(nav.ToElementNavigator(), typed: false);
        }

        [TestMethod]
        public void CloningWorks()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNavU(tp);
            ParseDemoPatient.CloningWorks(nav);
        }

        [TestMethod]
        public void ElementNavPerformanceUntypedJson()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNavU(tp);
            ParseDemoPatient.ElementNavPerformance(nav.ToElementNavigator());
        }

        [TestMethod]
        public void ProducesCorrectUntypedLocations()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var patient = getJsonNavU(tp);

            ParseDemoPatient.ProducesCorrectUntypedLocations(patient);
        }

        [TestMethod]
        public void HasLineNumbers()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNavU(tp);

            ParseDemoPatient.HasLineNumbers<JsonSerializationDetails>(nav.ToElementNavigator());
        }

        [TestMethod]
        public void RoundtripJson()
        {
            ParseDemoPatient.RoundtripJson(jsonText => 
                getJsonNavU(jsonText, new FhirJsonNavigatorSettings { AllowJsonComments = true }));
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
            var nav = getJsonNavU(bundle).ToElementNavigator();
            var entryNav = nav.Select("entry.resource").First();
            var id = entryNav.Scalar("id");
            Assert.IsNotNull(id);
        }


        [TestMethod]
        public void CanReadEdgeCases()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");
            var patient = getJsonNavU(tpJson, new FhirJsonNavigatorSettings { AllowJsonComments = true });

            Assert.AreEqual("Patient", patient.Name);
            Assert.AreEqual("Patient", patient.GetResourceType());
            Assert.IsFalse(patient.MoveToNext());

            // Move into child Patient.identifier
            Assert.IsTrue(patient.MoveToFirstChild());
            var identifier = patient.Clone();
            Assert.IsNull(identifier.Text);

            // Move to child Patient.identifier.period
            Assert.IsTrue(identifier.MoveToFirstChild());
            var period = identifier.Clone();

            // Move to child Patient.identifier.period.start
            Assert.IsTrue(period.MoveToFirstChild("start"));
            Assert.AreEqual("start", period.Name);
            Assert.AreEqual("2001-05-06", period.Text);

            Assert.IsTrue(identifier.MoveToNext());    // assigner
            Assert.IsTrue(identifier.MoveToNext());    // use
            Assert.AreEqual("usual", identifier.Text);
            Assert.IsTrue(identifier.MoveToNext());    // system
            Assert.IsTrue(identifier.MoveToNext());    // value

            // Check the value + extensions on Patient.identifier.value
            Assert.AreEqual("12345", identifier.Text);
            var value = identifier.Clone();
            Assert.IsFalse(value.MoveToFirstChild());
            Assert.IsFalse(identifier.MoveToNext());

            // Move to sibling Patient.managingOrganization
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("managingOrganization", patient.Name);

            // Move to sibling Patient.active
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("active", patient.Name);
            Assert.IsNull(patient.Text);

            // Move to sibling Patient.name (2x)
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("name", patient.Name);
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("name", patient.Name);

            Assert.IsTrue(patient.MoveToNext("deceasedBoolean"));
            Assert.AreEqual("deceasedBoolean", patient.Name);
            Assert.AreEqual("true", patient.Text);

            var details = (patient as IAnnotated).Annotation<JsonSerializationDetails>();
            Assert.AreEqual(true, details.OriginalValue);

            Assert.IsTrue(patient.MoveToNext()); // address
            Assert.IsTrue(patient.MoveToNext()); // maritalStatus
            Assert.IsTrue(patient.MoveToNext()); // multipleBirthInteger
            Assert.AreEqual("3", patient.Text);

            Assert.IsTrue(patient.MoveToNext()); // text
            Assert.IsTrue(patient.MoveToNext()); // contained

            // Check Patient.contained[0], a Binary
            Assert.AreEqual("contained", patient.Name);
            Assert.AreEqual("Binary", patient.GetResourceType());

            Assert.IsTrue(patient.MoveToNext()); // contained

            // Check Patient.contained[1], an Organization
            Assert.AreEqual("contained", patient.Name);
            Assert.AreEqual("Organization", patient.GetResourceType());

            Assert.IsTrue(patient.MoveToNext()); // contact
            validateContact(patient);

            Assert.IsTrue(patient.MoveToNext()); // careProvider
            Assert.IsTrue(patient.MoveToNext()); // telecom (2x)
            Assert.IsTrue(patient.MoveToNext());
            Assert.IsFalse(patient.MoveToNext());
        }

        private void validateContact(ISourceNavigator patient)
        {
            var contact = patient.Clone();

            Assert.IsTrue(contact.MoveToFirstChild()); // contact.name

            Assert.IsTrue(contact.MoveToFirstChild()); // contact.name.family[0]            
            Assert.IsNull(contact.Text);

            Assert.IsTrue(contact.MoveToNext()); // family[1]
            Assert.AreEqual("du", contact.Text);

            Assert.IsTrue(contact.MoveToNext()); // family[2]
            Assert.IsTrue(contact.MoveToNext()); // family[3]

            Assert.AreEqual("Marché", contact.Text);
            Assert.IsFalse(contact.MoveToFirstChild());

            Assert.IsTrue(contact.MoveToNext()); // family[4]
            Assert.IsNull(contact.Text);
            Assert.IsTrue(contact.MoveToFirstChild()); // family[4].extension
            Assert.AreEqual("extension", contact.Name);
        }

        [TestMethod]
        public void CatchesArrayMismatch()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4], '_a' : [2,4] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': 2, '_a' : [2] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4], '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ '_a': [4,5,6] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': [2,3,4] }", "test");
            Assert.IsTrue(nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null,2], '_a' : [{'active':true},null] }", "test");
            Assert.IsTrue(nav.MoveToFirstChild());
        }

        [TestMethod]
        public void CatchesUnsupportedFeatures()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': '   ' }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': {}, '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': {'active':true}, '_a': {'dummy':4} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': 3, '_a' : 4 }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ '_a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());
        }

        [TestMethod]
        public void CatchNullErrors()
        {
            var nav = FhirJsonNavigator.Untyped("{ 'a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ '_a': null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': null, '_a' : null }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = FhirJsonNavigator.Untyped("{ 'a': [null], '_a': [null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());
        }
    }
}