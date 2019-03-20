/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class ElementNodeTests
    {
        SourceNode patient;

        public ITypedElement getXmlNode(string xml) => 
            FhirXmlNode.Parse(xml).ToTypedElement(new PocoStructureDefinitionSummaryProvider());

        public ElementNodeTests()
        {
            var annotatedNode = SourceNode.Valued("id", "myId1");
            (annotatedNode as IAnnotatable).AddAnnotation("a string annotation");

            patient = SourceNode.Node("Patient", 
                SourceNode.Resource("contained", "Observation", SourceNode.Valued("valueBoolean", "true")),
                SourceNode.Valued("active", "true",
                   annotatedNode,
                   SourceNode.Valued("id", "myId2"),
                   SourceNode.Node("extension",
                       SourceNode.Valued("value", "4")),
                   SourceNode.Node("extension",
                       SourceNode.Valued("value", "world!"))));
        }

        [TestMethod]
        public void TestConstruction()
        {
            var data = patient[0];
            Assert.AreEqual("contained", data.Name);
            Assert.IsNull(data.Text);
            Assert.AreEqual("Observation", data.ResourceType);
            Assert.AreEqual(1, data.Children().Count());

            data = patient[1];
            Assert.AreEqual("active", data.Name);
            Assert.AreEqual("true", data.Text);
            Assert.AreEqual(4, data.Children().Count());
        }


        [TestMethod]
        public void KnowsPath()
        {
            Assert.AreEqual("Patient", patient.Location);
            Assert.AreEqual("Patient.contained[0].valueBoolean[0]", patient[0][0].Location);
            Assert.AreEqual("Patient.active[0]", patient[1].Location);
            Assert.AreEqual("Patient.active[0].id[0]", patient[1][0].Location);
            Assert.AreEqual("Patient.active[0].id[1]", patient[1][1].Location);
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", patient[1][2][0].Location);
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", patient[1][3][0].Location);
        }

        [TestMethod]
        public void AccessViaIndexers()
        {
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", patient["active"][0]["extension"][1]["value"][0].Location);
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", patient["active"]["extension"][1]["value"].Single().Location);
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", patient.Children("active").First()
                                .Children("extension").First()
                                .Children("value").First().Location);
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", patient.Children("active")
                                .Children("extension").First()
                                .Children("value").Single().Location);
        }

        [TestMethod]
        public void KnowsChildren()
        {
            Assert.IsFalse(patient["active"][0]["id"].Children().Any());
            Assert.IsFalse(patient["active"]["id"].Children().Any());
        }

        [TestMethod]
        public void CanQueryNodeAxis()
        {
            Assert.AreEqual(6, patient["active"].Descendants().Count());
            Assert.AreEqual(7, patient["active"].DescendantsAndSelf().Count());
            Assert.AreEqual(2, patient["active"]["extension"].Count());
        }

        [TestMethod]
        public void CanNavigateOverNode()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var nav = patient.ToElementNavigator();
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.AreEqual("Patient", nav.Name);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("active", nav.Name);
           // Assert.AreEqual("boolean", nav.Type);
            Assert.IsFalse(nav.MoveToNext());

            Assert.AreEqual("true", nav.Value);
            Assert.IsTrue(nav.MoveToFirstChild("id"));
            Assert.AreEqual("id", nav.Name);
            Assert.IsFalse(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("id", nav.Name);
            Assert.IsTrue(nav.MoveToNext("extension"));
            Assert.AreEqual("extension", nav.Name);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("value", nav.Name);
        }

        [TestMethod]
        public void KeepsAnnotations()
        {
            ISourceNode firstIdNode = patient[1][0];
            Assert.AreEqual("a string annotation", firstIdNode.Annotation<string>());
            Assert.AreEqual("a string annotation", patient["active"]["id"].First().Annotation<string>());
        }

        // Test clone()

        [TestMethod]
        public void ReadsFromNav()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlNode(tpXml).ToSourceNode();
            var nodes = SourceNode.FromNode(nav);
            Assert.IsTrue(nav.IsEqualTo(nodes).Success);
        }


        //[Fact]
        //public void CanUseBackboneTypeForEntry()
        //{
        //    var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
        //    var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
        //    var bundle = FhirJsonNode.Parse(bundleJson);
        //    var typedBundle = bundle.ToTypedElement(_sdsProvider, "Bundle");

        //    //Type of entry is BackboneElement, but you can't set that, see below.
        //    Assert.AreEqual("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

        //    var entry = SourceNode.Node("entry", SourceNode.Valued("fullUrl", "http://example.org/Patient/1"));

        //    //What DOES work:
        //    var typedEntry = entry.ToTypedElement(_sdsProvider, "Element"); //But you can't use BackboneElement here, see below.
        //    Assert.AreEqual("Element", typedEntry.InstanceType);

        //    //But this leads to a System.ArgumentException: 
        //    //Type BackboneElement is not a mappable Fhir datatype or resource
        //    //Parameter name: type
        //    typedEntry = entry.ToTypedElement(_sdsProvider, "BackboneElement");
        //    Assert.AreEqual("BackboneElement", typedEntry.InstanceType);
        //    // Expected to be able to use BackboneElement as type for the entry SourceNode;
        //}

        [TestMethod]
        public void CannotUseAbstractType()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
            var bundle = FhirJsonNode.Parse(bundleJson);
            var typedBundle = bundle.ToTypedElement(_sdsProvider, "Bundle");

            //Type of entry is BackboneElement, but you can't set that, see below.
            Assert.AreEqual("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

            var entry = SourceNode.Node("entry", SourceNode.Valued("fullUrl", "http://example.org/Patient/1"));

            try
            {
                var typedEntry =
                    entry.ToTypedElement(_sdsProvider, "Element");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Should have thrown on invalid Div format");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}