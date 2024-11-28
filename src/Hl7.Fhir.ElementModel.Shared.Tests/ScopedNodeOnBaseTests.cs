using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class ScopedNodeOnBaseTests
    {
        private SinglePocoElementNode _bundleNode;

        [TestInitialize]
        public void SetupSource()
        {
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            _bundleNode = bundle.ToElementNode();
        }

        [TestMethod]
        public void GetContainedAndBundledResources()
        {
            Assert.AreEqual(0, _bundleNode!.ContainedResources().Count());
            
            var entries = _bundleNode.Child<RepeatingPocoElementNode>("entry")?.Pocos.OfType<Bundle.EntryComponent>().ToList();
            Assert.AreEqual(7, entries.Count);

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d", entries[1].FullUrl);
            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            Assert.IsFalse(entries[1].Resource!.ToElementNode().ContainedResources().Any());
            Assert.IsNotNull(entries[1].Resource!.ToElementNode().Children().First());

            Assert.AreEqual("a", entries[2].Resource!.Id);

            var entry6 = entries[6].Resource;
            // Assert.AreEqual(2, entry6!.ContainedResources().Count());
            // Assert.IsFalse(entry6.BundledResources().Any());
            // Assert.AreEqual("orgY", (entry6.ContainedResources().Skip(1).First() as Resource)!.Id);
        }

        [TestMethod]
        public void GetFullUrl()
        {
            var entries = _bundleNode!.BundledResources().ToList();

            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FindFullUrl());

            IScopedNode entry3 = entries[3].Children("resource").FirstOrDefault();
            entry3 = entry3?.Children("managingOrganization").FirstOrDefault();
            Assert.IsNotNull(entry3);
            entry3 = entry3.Children("reference").FirstOrDefault();
            Assert.IsNotNull(entry3);
            Assert.AreEqual(entries[3].FindFullUrl(), entry3.FindFullUrl());
            Assert.AreEqual(entry3.GetParentResource()!.FindFullUrl(), entry3.FindFullUrl());

            var entry6 = entries[6].Children("resource").FirstOrDefault();
            entry6 = entry6?.ContainedResources().Skip(1).FirstOrDefault();
            Assert.IsNotNull(entry6);
            Assert.AreEqual("orgY", entry6.Children("id").FirstOrDefault()?.Value);
            Assert.AreEqual(entries[6].FindFullUrl(), entry6.FindFullUrl());
            Assert.AreEqual(entry6.GetParentResource()!.FindFullUrl(), entry6.FindFullUrl());
        }

        [TestMethod]
        public void TestMakeAbsolute()
        {
            var inner0 = _bundleNode!.BundledResources().First().Children("resource").Children("active").SingleOrDefault() as IScopedNode;
            Assert.IsNotNull(inner0);

            Assert.AreEqual("http://example.org/fhir/Patient/3", inner0.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner0.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner0.MakeAbsolute("http://example.org/fhir/Organization/5"));

            var inner1 = _bundleNode.BundledResources().Skip(1).First().Children("resource").Children("active").SingleOrDefault() as IScopedNode;

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d/3", inner1!.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner1!.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner1!.MakeAbsolute("http://example.org/fhir/Organization/5"));
        }

        [TestMethod]
        public void TestContainedCanResolveToContainer()
        {
            Assert.IsNull(_bundleNode!.Resolve("#"));

            var patient = _bundleNode!.BundledResources().Skip(6).First().Children("resource").First();
            Assert.IsNull(patient.Resolve("#"));

            var containedOrg = patient.ContainedResources().First();
            Assert.AreEqual("Patient", containedOrg.Resolve("#")!.InstanceType);

            var containedId = containedOrg.Children("id").First();
            Assert.AreEqual("Patient", containedId.Resolve("#")!.InstanceType);
        }

        [TestMethod]
        public void TestResolve()
        {
            IScopedNode inner7 = (_bundleNode!.BundledResources().Skip(6).First().Children("resource").Children("managingOrganization").SingleOrDefault() as IScopedNode)!;

            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7.Resolve("http://example.org/fhir/Patient/e")!.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Resolve("#orgY")!.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7.Resolve("#e")!.Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7.Resolve("http://example.org/fhir/Patient/d")!.Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7.Resolve("Patient/d")!.Location);
            Assert.AreEqual("Bundle.entry[1].resource[0]", inner7.Resolve("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d")!.Location);
            Assert.IsNull(inner7.Resolve("#d"));
            Assert.IsNull(inner7.Resolve("http://nu.nl/3"));

            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Resolve()!.Location);
            Assert.IsTrue(inner7.Children("reference").Any());
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Children("reference").First().Resolve()!.Location);

            string lastUrlResolved = "";

            Assert.IsNull(inner7.Resolve("#d", externalResolve));
            Assert.AreEqual("#d", lastUrlResolved);
            Assert.IsNull(inner7.Resolve("http://nu.nl/3", externalResolve));
            Assert.AreEqual("http://nu.nl/3", lastUrlResolved);

            IScopedNode? externalResolve(string url)
            {
                lastUrlResolved = url;
                return null;
            }
        }

        [TestMethod]
        [TemporarilyChanged] // this test is strange. We cannot support it on pocos (yet)
        [Ignore]
        public void AtResourceWithoutDefinition()
        {
            // var provider = new NoTypeProvider();
            // var elementNode = ElementNode.Root(provider, "Patient");
            // elementNode.Add(provider, "active", true, "boolean");
            //
            // var node = elementNode.ToPoco().ToScopedNode();
            //
            // Assert.IsTrue(node.Type.HasFlag(NodeType.Resource));
            // var inner = node.Children().First();
            // Assert.IsFalse(inner.Type.HasFlag(NodeType.Resource));
        }
        
        [TestMethod]
        public void Bundle_WithEntryWithoutFullUrl_ShouldNotThrow()
        {
            var bundle = new Bundle() { Type = Bundle.BundleType.Batch, Entry = [new Bundle.EntryComponent() { Resource = new Patient() }]}.ToTypedElement().ToScopedNode();

            var enumerate = () => bundle.BundledResources();
            enumerate.Should().NotThrow().Subject.Should().ContainSingle(c => !c.Children("fullUrl").Any());
        }
    }
}