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
using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Model;
using Hl7.Fhir.Tests;

namespace Hl7.FhirPath.Tests
{
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

        [Fact]
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
            var data = patient[0];
            Assert.Equal("contained", data.Name);
            Assert.Null(data.Value);
            Assert.Equal("Observation", data.InstanceType);

            data = patient[1];
            Assert.Equal("active", data.Name);
            Assert.Equal(true, data.Value);
            Assert.Equal("boolean", data.InstanceType);
        }

        [Fact]
        public void SuccessfullyCreated()
        {
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

        [Fact]
        public void ClonesOk()
        {
            var patientClone = patient.Clone();
            var result = patientClone.IsEqualTo(patient);
            Assert.True(result.Success);
        }

        [Fact]
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

        [Fact]
        public void KnowsChildren()
        {
            Assert.False(patient["active"][0]["id"].Children().Any());
            Assert.False(patient["active"]["id"].Children().Any());
        }

        [Fact]
        public void CanQueryNodeAxis()
        {
            Assert.Equal(7, patient["active"].Descendants().Count());
            Assert.Equal(8, patient["active"].DescendantsAndSelf().Count());
            Assert.Equal(2, patient["active"]["extension"].Count());
        }


        [Fact]
        public void KeepsAnnotations()
        {
            ITypedElement identifier = patient["active"][0];
            Assert.Equal("a string annotation", identifier.Annotation<string>());
        }

        [Fact]
        public void CanBuildFromITypedElement()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patientElem = (new FhirXmlParser()).Parse(tpXml).ToTypedElement();
            var nodes = ElementNode.FromElement(patientElem);
            Assert.True(patientElem.IsEqualTo(nodes).Success);
        }

        [Fact]
        public void FromNodeClonesCorrectly()
        {
            var child1 = SourceNode.Valued("child1", "a value");
            child1.AddAnnotation("The first annotation");

            var root = SourceNode.Node("TestRoot", child1);
            root.ResourceType = "TestR";
            var annotationTypes = new[] { typeof(string) };
            var copiedRoot = SourceNode.FromNode(root, recursive: false, annotationsToCopy: annotationTypes);

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

        [Fact]
        public void CannotUseAbstractType()
        {
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
            var bundle = FhirJsonNode.Parse(bundleJson);
            var typedBundle = bundle.ToTypedElement(provider, "Bundle");

            //Type of entry is BackboneElement, but you can't set that, see below.
            Assert.Equal("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

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