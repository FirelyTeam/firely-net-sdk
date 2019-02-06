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
using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Specification;

namespace Hl7.FhirPath.Tests
{
#if false
    // Not yet ready - removed for 1.2
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

            patientRoot
                .Add(provider, "active", true)
                .Add(provider, "contact")
                .Add(provider, "identifier")
                .Add(provider, "identifier");

            patientRoot["identifier"][0]
                .Add(provider, "system", "http://nu.nl")
                .Add(provider, "value", "1234567");

            patientRoot["identifier"][1]
                .Add(provider, "system", "http://toen.nl")
                .Add(provider, "value", "7654321")
                .AddAnnotation(42);

            patientRoot["contact"][0]
                .Add(provider, "gender", "male")
                .AddAnnotation("String annotation on contact");

            var containedOrganization = ElementNode.Root(provider, "Organization", "contained")
                    .Add(provider, "name", "Firely");

            patientRoot.Add(provider, containedOrganization);

            return patientRoot;
        }

        [Fact]
        public void SuccessfullyCreated()
        {
            var xml = patient.ToXml();
        }

        [Fact]
        public void ClonesOk()
        {
            var patientClone = patient.Clone();

            throw new NotImplementedException();
            // Implement Equals() and compare
        }

        [Fact]
        public void KnowsPath()
        {
            Assert.Equal("Patient", patient.Location);
            Assert.Equal("Patient.contained[0].valueBoolean[0]", patient[0][0].Location);
            Assert.Equal("Patient.active[0]", patient[1].Location);
            Assert.Equal("Patient.active[0].id[0]", patient[1][0].Location);
            Assert.Equal("Patient.active[0].id[1]", patient[1][1].Location);
            Assert.Equal("Patient.active[0].extension[0].value[0]", patient[1][2][0].Location);
            Assert.Equal("Patient.active[0].extension[1].value[0]", patient[1][3][0].Location);
        }

        [Fact]
        public void AccessViaIndexers()
        {
            Assert.Equal("Patient.active[0].extension[1].value[0]", patient["active"][0]["extension"][1]["value"][0].Location);
            Assert.Equal("Patient.active[0].extension[1].value[0]", patient["active"]["extension"][1]["value"].Single().Location);
            Assert.Equal("Patient.active[0].extension[0].value[0]", patient.Children("active").First()
                                .Children("extension").First()
                                .Children("value").First().Location);
            Assert.Equal("Patient.active[0].extension[0].value[0]", patient.Children("active")
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
            Assert.Equal(6, patient["active"].Descendants().Count());
            Assert.Equal(7, patient["active"].DescendantsAndSelf().Count());
            Assert.Equal(2, patient["active"]["extension"].Count());
        }

        [Fact]
        public void KeepsAnnotations()
        {
            ITypedElement identifier = patient["identifier"][1];
            Assert.Equal(42, identifier.Annotation<int>());

            ITypedElement contact = patient["contact"][0];
            Assert.Equal("String annotation on contact", contact.Annotation<string>());
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
            var copiedRoot = SourceNode.FromNode(root, recursive: false, annotationsToCopy:annotationTypes);

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
            Assert.Equal("The first annotation",(copiedChild as IAnnotated).Annotation<string>());
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
#endif
}