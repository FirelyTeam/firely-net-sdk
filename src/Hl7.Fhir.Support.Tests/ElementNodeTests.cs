/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;

namespace Hl7.FhirPath.Tests
{
    public class ElementNodeTests
    {
        ElementNode patient;

        public ElementNodeTests()
        {
            var annotatedNode = ElementNode.Valued("id", "myId1");
            (annotatedNode as IAnnotatable).AddAnnotation("a string annotation");

            patient = ElementNode.Node("Patient", "Patient",                
                ElementNode.Valued("active", true, "boolean",
                   annotatedNode,
                   ElementNode.Valued("id", "myId2"),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", 4, "integer")),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", "world!", "string"))));
        }

        [Fact]
        public void TestConstruction()
        {
            var data = patient[0];
            Assert.Equal("active", data.Name);
            Assert.Equal(true, data.Value);
            Assert.Equal("boolean", data.Type);
            Assert.Equal(4, data.Children.Count());
        }


        [Fact]
        public void KnowsPath()
        {
            Assert.Equal("Patient", patient.Location);
            Assert.Equal("Patient.active[0]", patient[0].Location);
            Assert.Equal("Patient.active[0].id[0]", patient[0][0].Location);
            Assert.Equal("Patient.active[0].id[1]", patient[0][1].Location);
            Assert.Equal("Patient.active[0].extension[0].value[0]", patient[0][2][0].Location);
            Assert.Equal("Patient.active[0].extension[1].value[0]", patient[0][3][0].Location);
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
            Assert.False(patient["active"][0]["id"].HasChildren());
            Assert.False(patient["active"]["id"].HasChildren());
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
            var nav = patient.ToNavigator();

            Assert.Equal("Patient", nav.Name);
            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("active", nav.Name);
            Assert.Equal("boolean", nav.Type);
            Assert.False(nav.MoveToNext());

            Assert.Equal(true, nav.Value);
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
            var firstIdNode = patient[0][0];
            Assert.Equal("a string annotation", (firstIdNode as IAnnotated).Annotation<string>());

            var nav = patient.ToNavigator();
            nav.MoveToFirstChild(); // active
            nav.MoveToFirstChild(); // id
            Assert.Equal("a string annotation", (nav as IAnnotated).Annotation<string>());
        }

        // Test clone()

        [Fact]
        public void ReadsFromNav()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);
            var nodes = ElementNode.FromNavigator(nav);
            var nav2 = nodes.ToNavigator();

            Assert.True(nav.IsEqualTo(nav2).Success);
        }

    }
}