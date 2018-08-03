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
    public class ParseDemoPatientJsonTyped
    {
        public IElementNavigator getJsonNav(string json, FhirJsonNavigatorSettings settings = null) 
            => FhirJsonNavigator.ForResource(json, new PocoStructureDefinitionSummaryProvider(), settings: settings);

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
            ParseDemoPatient.RoundtripJson(jsonText =>
                getJsonNav(jsonText, new FhirJsonNavigatorSettings { AllowJsonComments = true }));
        }

        [TestMethod]
        public void PingpongJson()
        {
            var tp = File.ReadAllText(@"TestData\fp-test-patient.json");
            // will allow whitespace and comments to come through      
            var navJson = FhirJsonNavigator.ForResource(tp, new PocoStructureDefinitionSummaryProvider());
            var xml = navJson.ToXml();

            var navXml = FhirXmlNavigator.ForResource(xml, new PocoStructureDefinitionSummaryProvider());
            var json = navXml.ToJson();

            JsonAssert.AreSame(tp, json);
        }

        [TestMethod]
        public void CatchesComplexPrimitiveMismatch()
        {
            // First, use a simple value where a complex type was expected
            var tp = "{ 'resourceType' : 'Patient', 'maritalStatus' : 'UNK' }";
            var navJson = FhirJsonNavigator.ForResource(tp, new PocoStructureDefinitionSummaryProvider());
            var errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("it cannot have a value"));

            // then, use a simple value where an array (of a complex type) was expected
            tp = "{ 'resourceType' : 'Patient', 'name' : ['Ewout'] }";
            navJson = FhirJsonNavigator.ForResource(tp, new PocoStructureDefinitionSummaryProvider());
            errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("it cannot have a value"));
        }

        [TestMethod]
        public void CatchesSingleValueForArray()
        {
            // Use a single element where an array was expected
            var tp = "{ 'resourceType' : 'Patient', 'identifier' :  { 'value': 'AB60001' }}";
            var navJson = FhirJsonNavigator.ForResource(tp, new PocoStructureDefinitionSummaryProvider());
            var errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("an array must be used here"));

            // Use an array where a single value was expected
            tp = "{ 'resourceType' : 'Patient', 'active' : [true,false] }";
            navJson = FhirJsonNavigator.ForResource(tp, new PocoStructureDefinitionSummaryProvider());
            errors = navJson.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("an array must not be used here"));
        }

        [TestMethod]
        public void CatchesIncorrectNarrativeXhtml()
        {
            // Total crap - passes unless we activate xhtml validation
            var nav = getJsonNav("{ 'resourceType': 'Patient', 'text': {" +
             "'status': 'generated', " +
             "'div': 'crap' } }");
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

            IElementNavigator getValidatingJsonNav(string jsonText) =>
                getJsonNav(jsonText, new FhirJsonNavigatorSettings { ValidateFhirXhtml = true });
        }

        [TestMethod]
        public void DelayedParseErrors()
        {
            var text = "{";
            var patient = getJsonNav(text);

            var errors = patient.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("Invalid Json encountered"));
        }

    }
}