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
        public void TestAutoDeriveTypeForPolymorphicElement()
        {
            // Explicit types will be passed through on polymorphic elements
            var obs = ElementNode.Root(provider, "Observation");
            var value = obs.Add(provider, "value", true, "boolean");
            Assert.Equal("boolean", value.InstanceType);

            // But if you leave the type out, Add() will try to determine the type
            obs = ElementNode.Root(provider, "Observation");
            value = obs.Add(provider, "value", true);  // without an explicit type
            Assert.Equal("boolean", value.InstanceType);

            // complex types are untouched
            var id = obs.Add(provider, "identifier");
            Assert.Equal("Identifier", id.InstanceType);

            // so are unvalued primitive non-polymorphic elements
            var act = obs.Add(provider, "status");
            Assert.Equal("code", act.InstanceType);

            // and valued non-polymorhpic primitives
            act = obs.Add(provider, "status", "registered");
            Assert.Equal("code", act.InstanceType);

            // actual type from definition will always win
            var data = ElementNode.Root(provider, "SampledData");
            var dims = data.Add(provider, "dimensions", 3);  // though this is a long, the actual type should be more precise
            Assert.Equal("positiveInt", dims.InstanceType);
        }

        [Fact]
        public void TestConstruction()
        {
            var patient = createPatient();

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
            var patient = createPatient();

            var pat = new Patient();
            var containedObs = new Observation { Value = new FhirBoolean(true) };
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
            var patient = createPatient();
            var patientClone = patient.ShallowCopy();
            var result = patientClone.IsEqualTo(patient);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void KnowsPath()
        {
            Assert.Equal("Patient", patient.Location);
            Assert.Equal("Patient.contained[0].value", patient[0][0].Location);
            Assert.Equal("Patient.active", patient[1].Location);
            Assert.Equal("Patient.active.id", patient[1][0].Location);
            Assert.Equal("Patient.identifier[0]", patient[2].Location);
            Assert.Equal("Patient.identifier[1]", patient[3].Location);
            Assert.Equal("Patient.active.extension[0].value", patient[1][1][0].Location);
            Assert.Equal("Patient.active.extension[1].value", patient[1][2][0].Location);
        }

        [TestMethod]
        public void KnowsShortPath()
        {
            var patient = createPatient();

            Assert.Equal("Patient", patient.ShortPath);
            Assert.Equal("Patient.contained[0].value", patient[0][0].ShortPath);
            Assert.Equal("Patient.active", patient[1].ShortPath);
            Assert.Equal("Patient.active.id", patient[1][0].ShortPath);
            Assert.Equal("Patient.identifier[0]", patient[2].ShortPath);
            Assert.Equal("Patient.identifier[1]", patient[3].ShortPath);
            Assert.Equal("Patient.active.extension[0].value", patient[1][1][0].ShortPath);
            Assert.Equal("Patient.active.extension[1].value", patient[1][2][0].ShortPath);
        }

        [Fact]
        public void AccessViaIndexers()
        {
            Assert.Equal("Patient.active.extension[1].value", patient["active"][0]["extension"][1]["value"][0].Location);
            Assert.Equal("Patient.active.extension[1].value", patient["active"]["extension"][1]["value"].Single().Location);
            Assert.Equal("Patient.active.extension[0].value", patient.Children("active").First()
                                .Children("extension").First()
                                .Children("value").First().Location);
            Assert.Equal("Patient.active.extension[0].value", patient.Children("active")
                                .Children("extension").First()
                                .Children("value").Single().Location);
        }

        [TestMethod]
        public void KnowsChildren()
        {
            Assert.False(patient["active"][0]["id"].Children().Any());
            Assert.False(patient["active"]["id"].Children().Any());
        }

        [TestMethod]
        public void CanQueryNodeAxis()
        {
            Assert.Equal(7, patient["active"].Descendants().Count());
            Assert.Equal(8, patient["active"].DescendantsAndSelf().Count());
            Assert.Equal(2, patient["active"]["extension"].Count());
        }

        [TestMethod]
        public void KeepsAnnotations()
        {
            var patient = createPatient();

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
        public void CheckRemove()
        {
            var patient = createPatient();
            Assert.Equal(2, patient.Children("identifier").Count());

            patient.Remove(patient.Children("identifier").First() as ElementNode);
            Assert.Single(patient.Children("identifier"));

            Assert.False(copiedRoot.Children().Any());
            Assert.Equal(root.Name, copiedRoot.Name);
            Assert.Equal(root.Location, copiedRoot.Location);
            Assert.Equal(root.Text, copiedRoot.Text);
            Assert.Equal(root.ResourceType, copiedRoot.ResourceType);
            Assert.Null((root as IAnnotated).Annotation<string>());

            copiedRoot = SourceNode.FromNode(root, recursive: true, annotationsToCopy: annotationTypes);
            Assert.True(copiedRoot.Children().Any());
            Assert.Null((root as IAnnotated).Annotation<string>());

            var copiedChild = copiedRoot.Children().Single();
            Assert.False(copiedChild.Children().Any());
            Assert.Equal(child1.Name, copiedChild.Name);
            Assert.Equal(child1.Location, copiedChild.Location);
            Assert.Equal(child1.Text, copiedChild.Text);
            Assert.Equal("The first annotation", (copiedChild as IAnnotated).Annotation<string>());
        }

        [TestMethod]
        public void ReplaceChildInElement()
        {
            var patient = createPatient();

            var newActive = ElementNode.Root(provider, "boolean");
            newActive.Value = false;
            patient.Replace(provider, patient["active"].Single(), newActive);
            Assert.Single(patient["active"]);
            Assert.Equal(false, patient["active"].Single().Value);

            var newIdentifier = ElementNode.Root(provider, "Identifier");
            newIdentifier.Add(provider, "system", "http://nos.nl");
            newIdentifier.Add(provider, "value", "1234");
            patient["identifier"].Last().ReplaceWith(provider, newIdentifier);
            Assert.Equal(2, patient["identifier"].Count());
            Assert.Equal(newIdentifier, patient["identifier"].Last());
        }

        [Fact]
        public void FromElementClonesCorrectly()
        {
            var patient = createPatient();

            var newElement = ElementNode.FromElement(patient, recursive: true, annotationsToCopy: new[] { typeof(string) });
            Assert.Equal(4, newElement.Children().Count());

            var activeChild = newElement["active"].Single();
            Assert.True((bool)activeChild.Value);
            Assert.True(activeChild.Annotations<string>().Single() == "a string annotation");

            var identifierSystemChild = newElement["identifier"][0]["system"].Single();
            
            // check whether we really have a clone by changing the copy
            identifierSystemChild.Value = "http://dan.nl";
            Assert.Equal("http://nu.nl",patient["identifier"][0]["system"].Single().Value);
        }

        [Fact]
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