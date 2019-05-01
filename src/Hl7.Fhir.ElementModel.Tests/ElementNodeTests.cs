/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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
using Hl7.Fhir.Model;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class ElementNodeTests
    {
        readonly IStructureDefinitionSummaryProvider provider = new PocoStructureDefinitionSummaryProvider();

        readonly ElementNode patient;

        public ElementNodeTests()
        {
            patient = createPatient();
        }


        private ElementNode createPatient()
        {
            var patientRoot = ElementNode.Root(provider, "Patient");

            var containedObs = ElementNode.Root(provider, "Observation", "contained");
            containedObs.Add(provider, "value", true, "boolean");
            patientRoot.Add(provider, containedObs);

            var activeNode = patientRoot.Add(provider, "active", true);
            activeNode.Add(provider, "id", "myId1");
               
            activeNode.AddAnnotation("a string annotation");
            var ext1 = activeNode.Add(provider, "extension");
            ext1.Add(provider, "value", 4, "integer");
            ext1.Add(provider, "url", "urn:1");
            var ext2 = activeNode.Add(provider, "extension");
            ext2.Add(provider, "value", "world!", "string");
            ext2.Add(provider, "url", "urn:2");

            var identifier0 = patientRoot.Add(provider, "identifier");
            identifier0.Add(provider, "system", "http://nu.nl");
            identifier0.Add(provider, "value", "1234567");

            var identifier1 = patientRoot.Add(provider, "identifier");
            identifier1.Add(provider, "system", "http://toen.nl");
            identifier1.Add(provider, "value", "7654321");

            return patientRoot;
        }

        [TestMethod]
        public void TestConstruction()
        {
            var data = patient[0];
            Assert.AreEqual("contained", data.Name);
            Assert.IsNull(data.Value);
            Assert.AreEqual("Observation", data.InstanceType);

            data = patient[1];
            Assert.AreEqual("active", data.Name);
            Assert.AreEqual(true, data.Value);
            Assert.AreEqual("boolean", data.InstanceType);
        }

        [TestMethod]
        public void SuccessfullyCreated()
        {
            var pat = new Patient();
            var containedObs = new Observation();
            containedObs.Value = new FhirBoolean(true);
            pat.Contained.Add(containedObs);
            pat.ActiveElement = new FhirBoolean(true) { ElementId = "myId1" };
            pat.ActiveElement.AddAnnotation("a string annotation");
            pat.ActiveElement.SetIntegerExtension("urn:1", 4);
            pat.ActiveElement.SetStringExtension("urn:2", "world!");
            pat.Identifier.Add(new Identifier("http://nu.nl", "1234567"));
            pat.Identifier.Add(new Identifier("http://toen.nl", "7654321"));
            XmlAssert.AreSame("in place", pat.ToXml(), patient.ToXml());
        }

        [TestMethod]
        public void ClonesOk()
        {
            var patientClone = patient.Clone();
            var result = patientClone.IsEqualTo(patient);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void KnowsPath()
        {
            Assert.AreEqual("Patient", patient.Location);
            Assert.AreEqual("Patient.contained[0].value", patient[0][0].Location);
            Assert.AreEqual("Patient.active", patient[1].Location);
            Assert.AreEqual("Patient.active.id", patient[1][0].Location);
            Assert.AreEqual("Patient.identifier[0]", patient[2].Location);
            Assert.AreEqual("Patient.identifier[1]", patient[3].Location);
            Assert.AreEqual("Patient.active.extension[0].value", patient[1][1][0].Location);
            Assert.AreEqual("Patient.active.extension[1].value", patient[1][2][0].Location);
        }

        [TestMethod]
        public void AccessViaIndexers()
        {
            Assert.AreEqual("Patient.active.extension[1].value", patient["active"][0]["extension"][1]["value"][0].Location);
            Assert.AreEqual("Patient.active.extension[1].value", patient["active"]["extension"][1]["value"].Single().Location);
            Assert.AreEqual("Patient.active.extension[0].value", patient.Children("active").First()
                                .Children("extension").First()
                                .Children("value").First().Location);
            Assert.AreEqual("Patient.active.extension[0].value", patient.Children("active")
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
            Assert.AreEqual(7, patient["active"].Descendants().Count());
            Assert.AreEqual(8, patient["active"].DescendantsAndSelf().Count());
            Assert.AreEqual(2, patient["active"]["extension"].Count());
        }

        [TestMethod]
        public void KeepsAnnotations()
        {
            ITypedElement identifier = patient["active"][0];
            Assert.AreEqual("a string annotation", identifier.Annotation<string>());
        }

        [TestMethod]
        public void CanBuildFromITypedElement()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patientElem = (new FhirXmlParser()).Parse(tpXml).ToTypedElement();
            var nodes = ElementNode.FromElement(patientElem);
            Assert.IsTrue(patientElem.IsEqualTo(nodes).Success);
        }

        [TestMethod]
        public void FromNodeClonesCorrectly()
        {
            var child1 = SourceNode.Valued("child1", "a value");
            child1.AddAnnotation("The first annotation");

            var root = SourceNode.Node("TestRoot", child1);
            root.ResourceType = "TestR";
            var annotationTypes = new[] { typeof(string) };
            var copiedRoot = SourceNode.FromNode(root, recursive: false, annotationsToCopy: annotationTypes);

            Assert.IsFalse(copiedRoot.Children().Any());
            Assert.AreEqual(root.Name, copiedRoot.Name);
            Assert.AreEqual(root.Location, copiedRoot.Location);
            Assert.AreEqual(root.Text, copiedRoot.Text);
            Assert.AreEqual(root.ResourceType, copiedRoot.ResourceType);
            Assert.IsNull((root as IAnnotated).Annotation<string>());

            copiedRoot = SourceNode.FromNode(root, recursive: true, annotationsToCopy: annotationTypes);
            Assert.IsTrue(copiedRoot.Children().Any());
            Assert.IsNull((root as IAnnotated).Annotation<string>());

            var copiedChild = copiedRoot.Children().Single();
            Assert.IsFalse(copiedChild.Children().Any());
            Assert.AreEqual(child1.Name, copiedChild.Name);
            Assert.AreEqual(child1.Location, copiedChild.Location);
            Assert.AreEqual(child1.Text, copiedChild.Text);
            Assert.AreEqual("The first annotation", (copiedChild as IAnnotated).Annotation<string>());
        }

        [TestMethod]
        public void CannotUseAbstractType()
        {
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
            var bundle = FhirJsonNode.Parse(bundleJson);
            var typedBundle = bundle.ToTypedElement(provider, "Bundle");

            //Type of entry is BackboneElement, but you can't set that, see below.
            Assert.AreEqual("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

            var entry = SourceNode.Node("entry", SourceNode.Valued("fullUrl", "http://example.org/Patient/1"));

            try
            {
                var typedEntry =
                    entry.ToTypedElement(provider, "Element");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Should have thrown on invalid Div format");
            }
            catch (ArgumentException)
            {
            }
        }

    }
}