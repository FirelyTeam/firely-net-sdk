﻿using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientXmlTyped
    {
        public ITypedElement getXmlNode(string xml, FhirXmlParsingSettings settings = null, TypedElementSettings tnSettings=null)
        {
            settings = settings ?? FhirXmlParsingSettings.CreateDefault();
            settings.PermissiveParsing = false;

            return XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), settings, tnSettings);
        }
            

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughTypedNavigator()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlNode(tpXml);
            ParseDemoPatient.CanReadThroughTypedElement(nav, typed: true);
        }

        [TestMethod]
        public void ElementNavPerformanceTypedXml()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlNode(tpXml);
            ParseDemoPatient.ElementNavPerformance(nav.ToSourceNode());
        }

        [TestMethod]
        public void ProducesCorrectTypedLocations()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var patient = getXmlNode(tpXml);
            ParseDemoPatient.ProducedCorrectTypedLocations(patient);
        }

        [TestMethod]
        public void ForwardsLowLevelDetails()
        {
            var nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'><active value='true'/></Patient>");

            var active = nav.Children().Single();
            Assert.AreEqual("active", active.Name);        // none-xmlns attributes will come through
            Assert.IsNotNull(active.GetXmlSerializationDetails());
        }


        [TestMethod]
        public async Task CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var tpJson = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            // If on a Unix platform replace \\r\\n in json strings to \\n.
            if(Environment.NewLine == "\n")
                tpJson = tpJson.Replace(@"\r\n", @"\n");
            var navXml = getXmlNode(tpXml);
            var navJson = await JsonParsingHelpers.ParseToTypedElementAsync(tpJson, new PocoStructureDefinitionSummaryProvider());

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
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlNode(tpXml);
            ParseDemoPatient.HasLineNumbers<XmlSerializationDetails>(nav.ToSourceNode());
        }

        [TestMethod]
        public void CheckBundleEntryNavigation()
        {
            var bundle = File.ReadAllText(Path.Combine("TestData", "BundleWithOneEntry.xml"));
            var node = getXmlNode(bundle);
            ParseDemoPatient.CheckBundleEntryNavigation(node);
        }


        [TestMethod]
        public async Task RoundtripXml()
        {
            await ParseDemoPatient.RoundtripXml(reader => XmlParsingHelpers.ParseToTypedElement(reader, new PocoStructureDefinitionSummaryProvider()));
        }

        [TestMethod]
        public async Task PingpongXml()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            // will allow whitespace and comments to come through      
            var navXml = XmlParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider());
            var json = await navXml.ToJsonAsync();

            var navJson = await JsonParsingHelpers.ParseToTypedElementAsync(json, new PocoStructureDefinitionSummaryProvider());
            var xml = await navJson.ToXmlAsync();

            XmlAssert.AreSame("fp-test-patient.xml", tp, xml, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void CatchesBasicTypeErrorsWithUnknownRoot()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "with-errors.xml"));
            var patient = getXmlNode(tpXml, tnSettings: new TypedElementSettings { ErrorMode = TypedElementSettings.TypeErrorMode.Passthrough });
            var result = patient.VisitAndCatch();
            Assert.AreEqual(11, result.Count);  // 11 syntax errors, unknown root is passed through without errors
        }

        [TestMethod]
        public void CatchesMissingNamespace()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "no-namespace.xml"));
            var bundle = FhirXmlNode.Parse(tpXml, new FhirXmlParsingSettings { PermissiveParsing = true });
            var result = bundle.ToTypedElement(new PocoStructureDefinitionSummaryProvider()).VisitAndCatch();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CatchesBasicTypeErrors()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "typeErrors.xml"));
            var patient = getXmlNode(tpXml);
            var result = patient.VisitAndCatch();
            Assert.AreEqual(10, result.Count);
        }

        [TestMethod]
        public void CatchesAttributeElementMismatch()
        {
            // First, use a simple value where a complex type was expected
            var nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'><contact gender='male' /></Patient>");
            var errors = nav.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("should be an XML element."));

            // Use xhtml when required
            nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'><text><status value= 'generated' />" +
                "<div>hi!</div></text></Patient>");
            errors = nav.VisitAndCatch();
            Assert.IsTrue(errors.First().Message.Contains("should be an XHTML element."));
            Assert.AreEqual(2, errors.Count);

            // Use an element where an attribute was expected
            nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'>" +
                "<extension>" +
                "<url value='http://example.org/fhir/StructureDefinition/recordStatus' />" +
                "<valueCode value='archived' /></extension></Patient>");
            errors = nav.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("should be an XML attribute."));
        }

        [TestMethod]
        public void CatchesElementOutOfOrder()
        {
            var nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'><gender value='male'/><active value='true' /></Patient>");
            var errors = nav.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("not in the correct order"));
        }

        [TestMethod]
        public void CatchesIncorrectNarrativeXhtml()
        {
            // passes unless we activate xhtml validation
            var nav = getXmlNode("<Patient xmlns='http://hl7.org/fhir'><text>" +
             "<status value='generated' />" +
             "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p onclick=\"myFunction();\">Donald</p></div></text></Patient>");
            var errors = nav.VisitAndCatch();
            Assert.AreEqual(0, errors.Count);

            // No xhtml namespace
            nav = getValidatingXmlNav("<Patient xmlns='http://hl7.org/fhir'><text>" +
             "<status value='generated' />" +
             "<div><p>Donald</p></div></text></Patient>");
            errors = nav.VisitAndCatch();
            Assert.AreEqual(2, errors.Count);
            Assert.IsTrue(errors.Any(e => e.Message.Contains("should be an XHTML element")));

            // Active content
            nav = getValidatingXmlNav("<Patient xmlns='http://hl7.org/fhir'><text>" +
             "<status value='generated' />" +
             "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p onclick=\"myFunction();\">Donald</p></div></text></Patient>");
            errors = nav.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("The 'onclick' attribute is not declared"));

            ITypedElement getValidatingXmlNav(string jsonText) =>
                getXmlNode(jsonText, new FhirXmlParsingSettings { ValidateFhirXhtml = true });
        }

        [TestMethod]
        public void CatchParseErrors()
        {
            var tpXml = "<Patient>";

            try
            {
                var patient = getXmlNode(tpXml);
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Xml encountered"));
            }
        }



    }
}
