using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientJsonTyped
    {
        public IElementNavigator getJsonNav(string json) => FhirJsonNavigator.Typed(json, new PocoSerializationInfoProvider());

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughTypedNavigator()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNav(tp);
            ParseDemoPatient.CanReadThroughNavigator(nav, typed: true);
        }

        [TestMethod]
        public void CloningWorks()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNav(tp);
            ParseDemoPatient.CloningWorks(nav);
        }

        [TestMethod]
        public void ElementNavPerformanceTypedJson()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNav(tp);
            ParseDemoPatient.ElementNavPerformance(nav);
        }

        [TestMethod]
        public void ProducesCorrectTypedLocations()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var patient = getJsonNav(tp);
            ParseDemoPatient.ProducedCorrectTypedLocations(patient);
        }

        [TestMethod]
        public void ForwardsLowLevelDetails()
        {
            var nav = getJsonNav("{ 'resourceType': 'Patient', 'active' : true }");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("active", nav.Name);        // none-xmlns attributes will come through
            Assert.IsNotNull((nav as IAnnotated).Annotation<JsonSerializationDetails>());
        }


        [TestMethod]
        public void HasLineNumbersTypedJson()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            var nav = getJsonNav(tp);
            ParseDemoPatient.HasLineNumbers<JsonSerializationDetails>(nav);
        }

        [TestMethod]
        public void RoundtripJson()
        {
            ParseDemoPatient.RoundtripJson(reader => FhirXmlNavigator.Typed(reader, new PocoSerializationInfoProvider()));
        }
    }
}