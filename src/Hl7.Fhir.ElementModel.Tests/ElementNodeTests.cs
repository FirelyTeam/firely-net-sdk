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

        [Fact]
        public void TestConstruction()
        {
            var data = patient[0];
            Assert.Equal("contained", data.Name);
            Assert.Null(data.Text);
            Assert.Equal("Observation", data.ResourceType);
            Assert.Single(data.Children());

            data = patient[1];
            Assert.Equal("active", data.Name);
            Assert.Equal("true", data.Text);
            Assert.Equal(4, data.Children().Count());
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
        public void CanNavigateOverNode()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var nav = patient.ToElementNavigator();
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.Equal("Patient", nav.Name);
            Assert.True(nav.MoveToFirstChild());
            Assert.True(nav.MoveToNext());
            Assert.Equal("active", nav.Name);
           // Assert.Equal("boolean", nav.Type);
            Assert.False(nav.MoveToNext());

            Assert.Equal("true", nav.Value);
            Assert.True(nav.MoveToFirstChild("id"));
            Assert.Equal("id", nav.Name);
            Assert.False(nav.MoveToFirstChild());
            Assert.True(nav.MoveToNext());
            Assert.Equal("id", nav.Name);
            Assert.True(nav.MoveToNext("extension"));
            Assert.Equal("extension", nav.Name);
            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("value", nav.Name);
        }

        [Fact]
        public void KeepsAnnotations()
        {
            ISourceNode firstIdNode = patient[1][0];
            Assert.Equal("a string annotation", firstIdNode.Annotation<string>());
            Assert.Equal("a string annotation", patient["active"]["id"].First().Annotation<string>());
        }

        // Test clone()

        [Fact]
        public void ReadsFromNav()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlNode(tpXml).ToSourceNode();
            var nodes = SourceNode.FromNode(nav);
            Assert.True(nav.IsEqualTo(nodes).Success);
        }


        //[Fact]
        //public void CanUseBackboneTypeForEntry()
        //{
        //    var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
        //    var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
        //    var bundle = FhirJsonNode.Parse(bundleJson);
        //    var typedBundle = bundle.ToTypedElement(_sdsProvider, "Bundle");

        //    //Type of entry is BackboneElement, but you can't set that, see below.
        //    Assert.Equal("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);
            
        //    var entry = SourceNode.Node("entry", SourceNode.Valued("fullUrl", "http://example.org/Patient/1"));

        //    //What DOES work:
        //    var typedEntry = entry.ToTypedElement(_sdsProvider, "Element"); //But you can't use BackboneElement here, see below.
        //    Assert.Equal("Element", typedEntry.InstanceType);

        //    //But this leads to a System.ArgumentException: 
        //    //Type BackboneElement is not a mappable Fhir datatype or resource
        //    //Parameter name: type
        //    typedEntry = entry.ToTypedElement(_sdsProvider, "BackboneElement");
        //    Assert.Equal("BackboneElement", typedEntry.InstanceType);
        //    // Expected to be able to use BackboneElement as type for the entry SourceNode;
        //}

        [Fact]
        public void CannotUseAbstractType()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
            var bundle = FhirJsonNode.Parse(bundleJson);
            var typedBundle = bundle.ToTypedElement(_sdsProvider, "Bundle");

            //Type of entry is BackboneElement, but you can't set that, see below.
            Assert.Equal("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

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