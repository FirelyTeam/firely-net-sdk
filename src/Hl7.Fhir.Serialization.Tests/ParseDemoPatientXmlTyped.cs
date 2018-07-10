using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    [TestClass]
    public class ParseDemoPatientXmlTyped
    {
        public IElementNavigator getXmlNav(string xml, FhirXmlNavigatorSettings settings = null) =>
            FhirXmlNavigator.Typed(xml, new PocoModelMetadataProvider(), settings);

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughTypedNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);
            ParseDemoPatient.CanReadThroughTypedNavigator(nav, typed: true);
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
            ParseDemoPatient.ElementNavPerformanceXml(nav);
        }

        [TestMethod]
        public void ProducesCorrectTypedLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = getXmlNav(tpXml);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id", patient.Location);
            var patNav = patient.Clone();

            patient.MoveToNext();   // text
            patient.MoveToNext("identifier");
            Assert.AreEqual("Patient.identifier[0]", patient.Location);
            var idNav = patient.Clone();

            Assert.IsTrue(patient.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[0].use", patient.Location);

            Assert.IsTrue(patNav.MoveToNext("deceased"));
            Assert.AreEqual("Patient.deceased", patNav.Location);

            idNav.MoveToNext(); // identifier
            Assert.AreEqual("Patient.identifier[1]", idNav.Location);

            Assert.IsTrue(idNav.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[1].use", idNav.Location);

        }

        [TestMethod]
        public void ForwardsLowLevelDetails()
        {
            var nav = getXmlNav("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("active", nav.Name);        // none-xmlns attributes will come through
            Assert.IsNotNull((nav as IAnnotated).Annotation<XmlSerializationDetails>());
        }


        // resurface when read through a validating navigator
        [TestMethod, Ignore]
        public void CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var navXml = getXmlNav(tpXml);
            var navJson = JsonDomFhirNavigator.Create(tpJson);

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
            ParseDemoPatient.RoundtripXml(reader => FhirXmlNavigator.Typed(reader, new PocoModelMetadataProvider()));
        }

        [TestMethod,Ignore]
        public void CatchesBasicTypeErrors()
        {
            var tpXml = File.ReadAllText(@"TestData\with-errors.xml");
            var patient = getXmlNav(tpXml);

            List<ExceptionRaisedEventArgs> runTest(IElementNavigator nav)
            {
                var errors = new List<ExceptionRaisedEventArgs>();

                using (patient.Catch((o, arg) => { errors.Add(arg); return true; }))
                {
                    var x = patient.DescendantsAndSelf().ToList();
                }

                return errors;
            }

            var result = runTest(patient);
            var originalCount = result.Count;
            Assert.AreEqual(11, result.Count);
            Assert.IsTrue(!result.Any(r => r.Message.Contains("schemaLocation")));
        }
    }
}