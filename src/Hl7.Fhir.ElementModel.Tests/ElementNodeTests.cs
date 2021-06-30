/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
using Hl7.Fhir.Specification.Source;
using System.Threading.Tasks;
using Hl7.Fhir.Specification.Snapshot;
using Tasks = System.Threading.Tasks;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class ElementNodeTests
    {
        private readonly IStructureDefinitionSummaryProvider _provider = new PocoStructureDefinitionSummaryProvider();

        private ElementNode createPatient()
        {
            var patientRoot = ElementNode.Root(_provider, "Patient");

            var containedObs = ElementNode.Root(_provider, "Observation", "contained");
            containedObs.Add(_provider, "value", true, "boolean");
            patientRoot.Add(_provider, containedObs);

            var activeNode = patientRoot.Add(_provider, "active", true);
            activeNode.Add(_provider, "id", "myId1");
               
            activeNode.AddAnnotation("a string annotation");
            var ext1 = activeNode.Add(_provider, "extension");
            ext1.Add(_provider, "value", 4, "integer");
            ext1.Add(_provider, "url", "urn:1");
            var ext2 = activeNode.Add(_provider, "extension");
            ext2.Add(_provider, "value", "world!", "string");
            ext2.Add(_provider, "url", "urn:2");

            var identifier0 = patientRoot.Add(_provider, "identifier");
            identifier0.Add(_provider, "system", "http://nu.nl");
            identifier0.Add(_provider, "value", "1234567");

            var identifier1 = patientRoot.Add(_provider, "identifier");
            identifier1.Add(_provider, "system", "http://toen.nl");
            identifier1.Add(_provider, "value", "7654321");

            return patientRoot;
        }

        [TestMethod]
        public void TestFpNavigate()
        {
            var patient = ElementNode.Root(_provider, "Patient");
            patient.Add(_provider, "active", true);

            var obs = ElementNode.Root(_provider, "Observation");
            obs.Add(_provider, "id", "test");

            patient.Add(_provider, obs, "contained");

            // Select on the root of the resource, path should match with resource name included
            var active = patient.Select("Patient.active");
            Assert.IsNotNull(active);
            Assert.AreEqual(true, active.FirstOrDefault().Value);

            // Select on the root of the resource, resource type does not match
            var id = obs.Select("Patient.id");
            Assert.IsNull(id.FirstOrDefault());

            // Select on root of the resource, path does not include the resourceType
            active = patient.Select("active");
            Assert.IsNotNull(active);
            Assert.AreEqual(true, active.FirstOrDefault().Value);

            // Select on the root of the resource, path is for a generic Resource / DomainResource element
            id = obs.Select("Resource.id");
            Assert.IsNotNull(id);
            Assert.AreEqual("test", id.FirstOrDefault().Value);

            var contained = patient.Select("DomainResource.contained");
            Assert.IsNotNull(contained);
            Assert.AreEqual("Observation", contained.FirstOrDefault().InstanceType);
        }

        [TestMethod]
        public void TestFpNavigateCustomResource()
        {
            var provider = new StructureDefinitionSummaryProvider(new CustomResourceResolver());
            var customResource = ElementNode.Root(provider, "MyCustomResource");
            customResource.Add(provider, "UpperCaseElement", true);
            customResource.Add(provider, "lowerCaseElement", false);

            var result = customResource.Select("UpperCaseElement");
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(true, result.FirstOrDefault().Value);

            result = customResource.Select("lowerCaseElement");
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(false, result.FirstOrDefault().Value);
        }

        [TestMethod]
        public void TestAutoDeriveTypeForPolymorphicElement()
        {
            // Explicit types will be passed through on polymorphic elements
            var obs = ElementNode.Root(_provider, "Observation");
            var value = obs.Add(_provider, "value", true, "boolean");
            Assert.AreEqual("boolean", value.InstanceType);

            // But if you leave the type out, Add() will try to determine the type
            obs = ElementNode.Root(_provider, "Observation");
#if !NET40
            Assert.ThrowsException<ArgumentException>(() => obs.Add(_provider, "value", true));  // without an explicit type
#endif
            value = obs.Add(_provider, "value", true, "boolean");  // with an explicit type
            Assert.AreEqual("boolean", value.InstanceType);

            // complex types are untouched
            var id = obs.Add(_provider, "identifier");
            Assert.AreEqual("Identifier", id.InstanceType);

            // so are unvalued primitive non-polymorphic elements
            var act = obs.Add(_provider, "status");
            Assert.AreEqual("code", act.InstanceType);

            // and valued non-polymorhpic primitives
            act = obs.Add(_provider, "status", "registered");
            Assert.AreEqual("code", act.InstanceType);

            // actual type from definition will always win
            var data = ElementNode.Root(_provider, "SampledData");
            var dims = data.Add(_provider, "dimensions", 3);  // though this is a long, the actual type should be more precise
            Assert.AreEqual("positiveInt", dims.InstanceType);
        }

        [TestMethod]
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
        public async Tasks.Task SuccessfullyCreated()
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
            XmlAssert.AreSame("in place", await pat.ToXmlAsync(), await patient.ToXmlAsync());
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
            var patient = createPatient();

            Assert.AreEqual("Patient", patient.Location);
            Assert.AreEqual("Patient.contained[0].value[0]", patient[0][0].Location);
            Assert.AreEqual("Patient.active[0]", patient[1].Location);
            Assert.AreEqual("Patient.active[0].id[0]", patient[1][0].Location);
            Assert.AreEqual("Patient.identifier[0]", patient[2].Location);
            Assert.AreEqual("Patient.identifier[1]", patient[3].Location);
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", patient[1][1][0].Location);
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", patient[1][2][0].Location);
        }

        [TestMethod]
        public void KnowsShortPath()
        {
            var patient = createPatient();

            Assert.AreEqual("Patient", patient.ShortPath);
            Assert.AreEqual("Patient.contained[0].value", patient[0][0].ShortPath);
            Assert.AreEqual("Patient.active", patient[1].ShortPath);
            Assert.AreEqual("Patient.active.id", patient[1][0].ShortPath);
            Assert.AreEqual("Patient.identifier[0]", patient[2].ShortPath);
            Assert.AreEqual("Patient.identifier[1]", patient[3].ShortPath);
            Assert.AreEqual("Patient.active.extension[0].value", patient[1][1][0].ShortPath);
            Assert.AreEqual("Patient.active.extension[1].value", patient[1][2][0].ShortPath);
        }

        [TestMethod]
        public void AccessViaIndexers()
        {
            var patient = createPatient();

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
            var patient = createPatient();

            Assert.IsFalse(patient["active"][0]["id"].Children().Any());
            Assert.IsFalse(patient["active"]["id"].Children().Any());
        }

        [TestMethod]
        public void CanQueryNodeAxis()
        {
            var patient = createPatient();

            Assert.AreEqual(7, patient["active"].Descendants().Count());
            Assert.AreEqual(8, patient["active"].DescendantsAndSelf().Count());
            Assert.AreEqual(2, patient["active"]["extension"].Count());
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
            Assert.AreEqual(2, patient.Children("identifier").Count());

            patient.Remove(patient.Children("identifier").First() as ElementNode);
            Assert.AreEqual(1, patient.Children("identifier").Count());

            patient.Remove(patient.Children("identifier").First() as ElementNode);
            Assert.AreEqual(0, patient.Children("identifier").Count());

            var anotherPatient = createPatient();

            Assert.IsFalse(patient.Remove(anotherPatient["identifier"].First()));
        }

        [TestMethod]
        public void ReplaceChildInElement()
        {
            var patient = createPatient();

            var newActive = ElementNode.Root(_provider, "boolean");
            newActive.Value = false;
            patient.Replace(_provider, patient["active"].Single(), newActive);
            Assert.AreEqual(1, patient["active"].Count);
            Assert.AreEqual(false, patient["active"].Single().Value);

            var newIdentifier = ElementNode.Root(_provider, "Identifier");
            newIdentifier.Add(_provider, "system", "http://nos.nl");
            newIdentifier.Add(_provider, "value", "1234");
            patient["identifier"].Last().ReplaceWith(_provider, newIdentifier);
            Assert.AreEqual(2, patient["identifier"].Count());
            Assert.AreEqual(newIdentifier, patient["identifier"].Last());
        }

        [TestMethod]
        public void FromElementClonesCorrectly()
        {
            var patient = createPatient();

            var newElement = ElementNode.FromElement(patient, recursive: true, annotationsToCopy: new[] { typeof(string) });
            Assert.AreEqual(4, newElement.Children().Count());

            var activeChild = newElement["active"].Single();
            Assert.IsTrue((bool)activeChild.Value);
            Assert.IsTrue(activeChild.Annotations<string>().Single() == "a string annotation");

            var identifierSystemChild = newElement["identifier"][0]["system"].Single();
            
            // check whether we really have a clone by changing the copy
            identifierSystemChild.Value = "http://dan.nl";
            Assert.AreEqual("http://nu.nl",patient["identifier"][0]["system"].Single().Value);
        }

        [TestMethod]
        public async Tasks.Task CannotUseAbstractType()
        {
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":[{\"fullUrl\":\"http://example.org/Patient/1\"}]}";
            var bundle = await FhirJsonNode.ParseAsync(bundleJson);
            var typedBundle = bundle.ToTypedElement(_provider, "Bundle");

            //Type of entry is BackboneElement, but you can't set that, see below.
            Assert.AreEqual("BackboneElement", typedBundle.Select("$this.entry[0]").First().InstanceType);

            var entry = SourceNode.Node("entry", SourceNode.Valued("fullUrl", "http://example.org/Patient/1"));

            try
            {
                var typedEntry =
                    entry.ToTypedElement(_provider, "Element");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Should have thrown on invalid Div format");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestImportChild()
        {
            var humanname = ElementNode.Root(_provider, "HumanName", "name");
            humanname.Add(_provider, "given", "given");

            var patient = ElementNode.Root(_provider, "Patient", "Patient");
            var practitioner = ElementNode.Root(_provider, "Practitioner", "Practitioner");

            patient.Add(_provider, humanname);
            practitioner.Add(_provider, humanname); // Parent of humanname needs to be switched from Patient to Practitioner

            var location = humanname.Location;
            Assert.AreEqual("Practitioner.name[0]", location);
        }

        [TestMethod]
        public void TestValueAttrRepresentationLogicalModelDataType()
        {
            var basicWithTel = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><BasicWithTel xmlns=\"http://hl7.org/fhir\"><telecom value =\"(tel)06-12345678\"/></BasicWithTel>";
            var node = FhirXmlNode.Parse(basicWithTel);
            var errors = node.VisitAndCatch();
            Assert.IsFalse(errors.Any());

            var provider = new StructureDefinitionSummaryProvider(new CustomResourceResolver());
            var typedElement = node.ToTypedElement(provider);
            errors = typedElement.VisitAndCatch();
            Assert.IsFalse(errors.Any());
        }

        private class CustomResourceResolver : IAsyncResourceResolver
        {
            private readonly ZipSource _zipSource;
            private readonly CachedResolver _resolver;

            public CustomResourceResolver()
            {
                _zipSource = new ZipSource("specification.zip");
                _resolver = new CachedResolver(new MultiResolver(_zipSource, new DirectorySource("TestData/TestSd")));
            }

            public async Task<Resource> ResolveByCanonicalUriAsync(string uri)
            {
                var sd = await _resolver.FindStructureDefinitionAsync(uri);
                if (!sd.HasSnapshot)
                {
                    var snapShotGenerator = new SnapshotGenerator(_resolver);
                    await snapShotGenerator.UpdateAsync(sd);
                }

                return sd;
            }

            public Task<Resource> ResolveByUriAsync(string uri) => throw new NotImplementedException();
        }

    }
}
