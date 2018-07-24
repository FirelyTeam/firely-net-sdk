using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Xunit.Abstractions;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Hl7.FhirPath.Tests.JsonNavTests
{
    [TestClass]
    public class ParseDemoPatientJson
    {
        [TestMethod]
        public void CanReadThroughNavigator()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            Assert.AreEqual("Patient", patient.Name);
            Assert.AreEqual("Patient", patient.Type);
            Assert.IsFalse(patient.MoveToNext());

            // Move into child Patient.identifier
            Assert.IsTrue(patient.MoveToFirstChild());
            var identifier = patient.Clone();
            Assert.IsNull(identifier.Type);
            Assert.IsNull(identifier.Value);

            // Move to child Patient.identifier.period
            Assert.IsTrue(identifier.MoveToFirstChild());
            var period = identifier.Clone();

            // Move to child Patient.identifier.period.start
            Assert.IsTrue(period.MoveToFirstChild("start"));
            Assert.AreEqual("start", period.Name);
            Assert.AreEqual("2001-05-06", period.Value);

            Assert.IsTrue(identifier.MoveToNext());    // assigner
            Assert.IsTrue(identifier.MoveToNext());    // use
            Assert.AreEqual("usual", identifier.Value);
            Assert.IsTrue(identifier.MoveToNext());    // system
            Assert.IsTrue(identifier.MoveToNext());    // value

            // Check the value + extensions on Patient.identifier.value
            Assert.AreEqual("12345", identifier.Value);
            var value = identifier.Clone();
            Assert.IsTrue(value.MoveToFirstChild());
            Assert.AreEqual("fhir_comments", value.Name);
            Assert.AreEqual("     seems like a likely choice     ", value.Value);
            Assert.IsFalse(value.MoveToNext());

            Assert.IsFalse(identifier.MoveToNext());

            // Move to sibling Patient.managingOrganization
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("managingOrganization", patient.Name);

            // Move to sibling Patient.active
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("active", patient.Name);
            Assert.IsNull(patient.Value);

            // Move to sibling Patient.name (2x)
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("name", patient.Name);
            Assert.IsTrue(patient.MoveToNext());
            Assert.AreEqual("name", patient.Name);

            Assert.IsTrue(patient.MoveToNext("deceasedBoolean"));
            Assert.AreEqual("deceasedBoolean", patient.Name);
            Assert.AreEqual("true", patient.Value);

            var details = (patient as IAnnotated).Annotation<JsonSerializationDetails>();
            Assert.AreEqual(true, details.RawValue);

            Assert.IsTrue(patient.MoveToNext()); // address
            Assert.IsTrue(patient.MoveToNext()); // maritalStatus
            Assert.IsTrue(patient.MoveToNext()); // multipleBirthInteger
            Assert.AreEqual("3", patient.Value);

            Assert.IsTrue(patient.MoveToNext()); // text
            Assert.IsTrue(patient.MoveToNext()); // contained

            // Check Patient.contained[0], a Binary
            Assert.AreEqual("contained", patient.Name);
            Assert.AreEqual("Binary", patient.Type);

            Assert.IsTrue(patient.MoveToNext()); // contained

            // Check Patient.contained[1], an Organization
            Assert.AreEqual("contained", patient.Name);
            Assert.AreEqual("Organization", patient.Type);

            Assert.IsTrue(patient.MoveToNext()); // contact
            validateContact(patient);

            Assert.IsTrue(patient.MoveToNext()); // careProvider
            Assert.IsTrue(patient.MoveToNext()); // telecom (2x)
            Assert.IsTrue(patient.MoveToNext());
            Assert.IsFalse(patient.MoveToNext());
        }

        private void validateContact(IElementNavigator patient)
        {
            var contact = patient.Clone();

            Assert.IsTrue(contact.MoveToFirstChild()); // contact.name

            Assert.IsTrue(contact.MoveToFirstChild()); // contact.name.family[0]            
            Assert.IsNull(contact.Value);

            Assert.IsTrue(contact.MoveToNext()); // family[1]
            Assert.AreEqual("du", contact.Value);

            Assert.IsTrue(contact.MoveToNext()); // family[2]
            Assert.IsTrue(contact.MoveToNext()); // family[3]

            Assert.AreEqual("Marché", contact.Value);
            Assert.IsFalse(contact.MoveToFirstChild());

            Assert.IsTrue(contact.MoveToNext()); // family[4]
            Assert.IsNull(contact.Value);
            Assert.IsTrue(contact.MoveToFirstChild()); // family[4].extension
            Assert.AreEqual("extension", contact.Name);
        }

        [TestMethod]
        public void CatchesArrayMisMatch()
        {
            var nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4], '_a' : [{},null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 2, '_a' : [{},null] }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4], '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4] }", "test");
            Assert.IsTrue(nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ '_a': [{},{},{}] }", "test");
            Assert.IsTrue(nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [null,2], '_a' : [{},null] }", "test");
            Assert.IsTrue(nav.MoveToFirstChild());
        }

        [TestMethod]
        public void CatchesUnsupportedFeatures()
        {
            var nav = JsonDomFhirNavigator.Create("{ 'a': {}, '_a' : {} }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 3, '_a' : 4 }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 3, '_a' : new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': new DateTime() }", "test");
            Assert.ThrowsException<FormatException>(() => nav.MoveToFirstChild());
        }


        [TestMethod]
        public void ProducesCorrectLocations()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            var cont = patient.Clone();

            Assert.AreEqual("Patient.identifier[0]", patient.Location);
            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.identifier[0].period[0]", patient.Location);

            cont.MoveToNext(); // managingOrganization
            cont.MoveToNext();
            Assert.AreEqual("Patient.active[0]", cont.Location);

            cont.MoveToNext();
            Assert.AreEqual("Patient.name[0]", cont.Location);

            cont.MoveToNext();
            Assert.AreEqual("Patient.name[1]", cont.Location);

            cont.MoveToFirstChild();
            Assert.AreEqual("Patient.name[1].given[0]", cont.Location);
        }

        [TestMethod]
        public void CompareJsonXmlParseOutcomes()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");
            var tpXml = File.ReadAllText(@"TestData\json-edge-cases.xml");
            
            var navJson = JsonDomFhirNavigator.Create(tpJson);
            var navXml = XmlDomFhirNavigator.Create(tpXml);

            var compare = navJson.IsEqualTo(navXml);

            if (compare.Success == false)
            {
                Debug.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.IsTrue(compare.Success);
            }
            Assert.IsTrue(compare.Success);
        }

        [TestMethod]
        public void FindFirstChild()
        {
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            patient.MoveToFirstChild("gender");
            Assert.AreEqual("male", patient.Value.ToString());
            Assert.AreEqual("Patient.gender[0]", patient.Location);
        }

        [TestMethod]
        public void HasLineNumbersJson()
        {
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = JsonDomFhirNavigator.Create(tpJson);

            Assert.IsTrue(nav.MoveToFirstChild());

            var jsonDetails = (nav as IAnnotated)?.Annotation<JsonSerializationDetails>();
            Assert.IsNotNull(jsonDetails);
            Assert.AreNotEqual(-1, jsonDetails.LineNumber);
            Assert.AreNotEqual(-1, jsonDetails.LinePosition);
        }
    }
}