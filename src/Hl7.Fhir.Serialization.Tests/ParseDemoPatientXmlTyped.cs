using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientXmlTyped
    {
        public IElementNavigator getXmlNav(string xml, FhirXmlNavigatorSettings settings = null) =>
            FhirXmlNavigator.ForResource(xml, new PocoSerializationInfoProvider(), settings);

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughTypedNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);
            ParseDemoPatient.CanReadThroughNavigator(nav, typed: true);
        }

        [TestMethod]
        public void CloningWorks()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);
            ParseDemoPatient.CloningWorks(nav);
        }

        [TestMethod]
        public void ElementNavPerformanceTypedXml()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);
            ParseDemoPatient.ElementNavPerformance(nav);
        }

        [TestMethod]
        public void ProducesCorrectTypedLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = getXmlNav(tpXml);
            ParseDemoPatient.ProducedCorrectTypedLocations(patient);
        }

        [TestMethod]
        public void ForwardsLowLevelDetails()
        {
            var nav = getXmlNav("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("active", nav.Name);        // none-xmlns attributes will come through
            Assert.IsNotNull((nav as IAnnotated).Annotation<XmlSerializationDetails>());
        }


        [TestMethod]
        public void CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var navXml = getXmlNav(tpXml);
            var navJson =  FhirJsonNavigator.ForResource(tpJson, new PocoSerializationInfoProvider());

            var compare = navXml.IsEqualTo(navJson);

            if (compare.Success == false)
            {
                Debug.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.IsTrue(compare.Success);
            }
            Assert.IsTrue(compare.Success);
        }

        [TestMethod]
        public void HasLineNumbersTypedXml()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);
            ParseDemoPatient.HasLineNumbers<XmlSerializationDetails>(nav);
        }

        [TestMethod]
        public void RoundtripXml()
        {
            ParseDemoPatient.RoundtripXml(reader => FhirXmlNavigator.ForResource(reader, new PocoSerializationInfoProvider()));
        }

        [TestMethod]
        public void PingpongXml()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.xml");
            // will allow whitespace and comments to come through      
            var navXml = FhirXmlNavigator.ForResource(tp, new PocoSerializationInfoProvider());
            var json = navXml.ToJson();

            var navJson = FhirJsonNavigator.ForResource(json, new PocoSerializationInfoProvider());
            var xml = navJson.ToXml();

            XmlAssert.AreSame("fp-test-patient.xml", tp, xml, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void CatchesBasicTypeErrorsWithUnknownRoot()
        {
            var tpXml = File.ReadAllText(@"TestData\with-errors.xml");
            var patient = getXmlNav(tpXml);
            var result = ParseDemoPatient.VisitAndCatch(patient);
            Assert.AreEqual(12, result.Count);  // 11 syntax errors + 1 error reporting the root type is unknown
        }

        [TestMethod]
        public void CatchesBasicTypeErrors()
        {
            var tpXml = File.ReadAllText(@"TestData\typeErrors.xml");
            var patient = getXmlNav(tpXml);
            var result = ParseDemoPatient.VisitAndCatch(patient);
            Assert.AreEqual(10, result.Count);  
        }


    }
}