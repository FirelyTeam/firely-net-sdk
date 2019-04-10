using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientJsonTyped
    {
        public ITypedElement getJsonNode(string json, FhirJsonParsingSettings settings = null) 
            => JsonParsingHelpers.ParseToTypedElement(json, new PocoStructureDefinitionSummaryProvider(), settings: settings);

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughTypedNavigator()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = getJsonNode(tp);
            ParseDemoPatient.CanReadThroughNavigator(nav, typed: true);
        }
        
        [TestMethod]
        public void ElementNavPerformanceTypedJson()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = getJsonNode(tp);
            ParseDemoPatient.ElementNavPerformance(nav.ToSourceNode());
        }

        [TestMethod]
        public void ProducesCorrectTypedLocations()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var patient = getJsonNode(tp);
            ParseDemoPatient.ProducedCorrectTypedLocations(patient);
        }

        [TestMethod]
        public void ForwardsLowLevelDetails()
        {
            var nav = getJsonNode("{ 'resourceType': 'Patient', 'active' : true }");

            var active = nav.Children().Single();
            Assert.AreEqual("active", active.Name);        // none-xmlns attributes will come through
            Assert.IsNotNull(active.GetJsonSerializationDetails());
        }


        [TestMethod]
        public void HasLineNumbersTypedJson()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = getJsonNode(tp);
            ParseDemoPatient.HasLineNumbers<JsonSerializationDetails>(nav.ToSourceNode());
        }

        [TestMethod]
        public void CheckBundleEntryNavigation()
        {
            var bundle = File.ReadAllText(Path.Combine("TestData", "BundleWithOneEntry.json"));
            var nav = getJsonNode(bundle);
            ParseDemoPatient.CheckBundleEntryNavigation(nav);
        }

        [TestMethod]
        public void RoundtripJson()
        {
            ParseDemoPatient.RoundtripJson(jsonText =>
                getJsonNode(jsonText, new FhirJsonParsingSettings { AllowJsonComments = true }));
        }

        [TestMethod]
        public void PingpongJson()
        {
            var tp = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            // will allow whitespace and comments to come through      
            var navJson = JsonParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider());
            var xml = navJson.ToXml();

            var navXml = XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider());
            var json = navXml.ToJson();

            List<string> errors = new List<string>();
            JsonAssert.AreSame(@"TestData\fp-test-patient.json", tp, json, errors);
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public void IgnoreElements()
        {
            var patient = SourceNode.Resource("Patient", "Patient", SourceNode.Valued("id", "pat1"));
            var jsonBare = patient.ToTypedElement(new PocoStructureDefinitionSummaryProvider()).ToJson(new FhirJsonSerializationSettings { IgnoreUnknownElements = false });
            Assert.IsTrue(jsonBare.Contains("pat1"));

            patient.Add(SourceNode.Valued("unknownElement", "someValue"));
            var jsonUnknown = patient.ToTypedElement(new PocoStructureDefinitionSummaryProvider(), settings: new TypedElementSettings { ErrorMode = TypedElementSettings.TypeErrorMode.Ignore }).ToJson(new FhirJsonSerializationSettings { IgnoreUnknownElements = true });
            Assert.IsFalse(jsonUnknown.Contains("unknownElement"));
        }

        [TestMethod]
        public void CatchesComplexPrimitiveMismatch()
        {
            // First, use a simple value where a complex type was expected
            var tp = "{ 'resourceType' : 'Patient', 'maritalStatus' : 'UNK' }";
            var navJson = JsonParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider());
            var errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("it cannot have a value"));

            // then, use a simple value where an array (of a complex type) was expected
            tp = "{ 'resourceType' : 'Patient', 'name' : ['Ewout'] }";
            navJson = JsonParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider());
            errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("it cannot have a value"));
        }

        [TestMethod]
        public void CatchesSingleValueForArray()
        {
            // Use a single element where an array was expected
            var tp = "{ 'resourceType' : 'Patient', 'identifier' :  { 'value': 'AB60001' }}";
            var navJson = JsonParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider(), null, new FhirJsonParsingSettings() { PermissiveParsing = false });
            var errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("an array must be used here"));

            // Use an array where a single value was expected
            tp = "{ 'resourceType' : 'Patient', 'active' : [true,false] }";
            navJson = JsonParsingHelpers.ParseToTypedElement(tp, new PocoStructureDefinitionSummaryProvider(), null, new FhirJsonParsingSettings() { PermissiveParsing = false });
            errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("an array must not be used here"));
        }

        [TestMethod]
        public void CatchesIncorrectNarrativeXhtml()
        {
                // Total crap - passes unless we activate xhtml validation
                var nav = getJsonNode("{ 'resourceType': 'Patient', 'text': {" +
                 "'status': 'generated', " +
                 "'div': 'crap' } }", new FhirJsonParsingSettings { PermissiveParsing = true });
                var errors = nav.VisitAndCatch();
                Assert.AreEqual(0,errors.Count);

                // Total crap - now with validation
                nav = getValidatingJsonNav("{ 'resourceType': 'Patient', 'text': {" +
                 "'status': 'generated', " +
                 "'div': 'crap' } }");
                errors = nav.VisitAndCatch();
                Assert.IsTrue(errors.Single().Message.Contains("Invalid Xml encountered"));

                // No xhtml namespace
                nav = getValidatingJsonNav("{ 'resourceType': 'Patient', 'text': {" +
                 "'status': 'generated', " +
                 "'div': '<div><p>Donald</p></div>' } }");
                errors = nav.VisitAndCatch();
                Assert.IsTrue(errors.Single().Message.Contains("is not a <div> from the XHTML namespace"));

                // Active content
                nav = getValidatingJsonNav("{ 'resourceType': 'Patient', 'text': {" +
                 "'status': 'generated', " +
                 "'div': '<div xmlns=\"http://www.w3.org/1999/xhtml\"><p onclick=\"myFunction();\">Donald</p></div>' } }");
                errors = nav.VisitAndCatch();
                Assert.IsTrue(errors.Single().Message.Contains("The 'onclick' attribute is not declared"));

                ITypedElement getValidatingJsonNav(string jsonText) =>
                    getJsonNode(jsonText, new FhirJsonParsingSettings { ValidateFhirXhtml = true, PermissiveParsing = false });
        }

        [TestMethod]
        public void CatchParseErrors()
        {
            var text = "{";

            try
            {
                var patient = getJsonNode(text);
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Json encountered"));
            }
        }

    }
}