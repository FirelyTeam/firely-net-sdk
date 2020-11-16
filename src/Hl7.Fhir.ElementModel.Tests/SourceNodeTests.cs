/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class SourceNodeTests
    {
        readonly SourceNode patient;

        public SourceNodeTests()
        {
            var annotatedNode = SourceNode.Valued("id", "myId1");
            annotatedNode.AddAnnotation("a string annotation");

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
        public void ClonesOk()
        {
            var patientClone = patient.Clone();
            var result = patientClone.IsEqualTo(patient);
            Assert.IsTrue(result.Success);
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
        public void KeepsAnnotations()
        {
            ISourceNode firstIdNode = patient[1][0];
            Assert.AreEqual("a string annotation", firstIdNode.Annotation<string>());
            Assert.AreEqual("a string annotation", (patient["active"]["id"].First() as IAnnotated).Annotation<string>());
        }

        [TestMethod]
        public void ReadsFromNav()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var xmlnode = FhirXmlNode.Parse(tpXml);
            var nodes = SourceNode.FromNode(xmlnode);
            Assert.IsTrue(xmlnode.IsEqualTo(nodes).Success);
        }

        [TestMethod]
        public void FromNodeClonesCorrectly()
        {
            var child1 = SourceNode.Valued("child1", "a value");
            child1.AddAnnotation("The first annotation");

            var root = SourceNode.Node("TestRoot", child1);
            root.ResourceType = "TestR";
            var annotationTypes = new[] { typeof(string) };
            var copiedRoot = SourceNode.FromNode(root, recursive: false, annotationsToCopy:annotationTypes);

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
            Assert.AreEqual("The first annotation",(copiedChild as IAnnotated).Annotation<string>());
        }
    }
}