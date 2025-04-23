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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class ScopedNodeOnBaseTests
    {
        private PocoNode _bundleNode;

        [TestInitialize]
        public void SetupSource()
        {
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

            var bundle = (new FhirXmlDeserializer()).Deserialize<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            _bundleNode = bundle.ToPocoNode();
        }

        [TestMethod]
        public void GetContainedAndBundledResources()
        {
            Assert.AreEqual(0, _bundleNode!.ContainedResources().Count());
            
            var entries = _bundleNode.Child<PocoListNode>("entry")?.Pocos.OfType<Bundle.EntryComponent>().ToList();
            Assert.AreEqual(7, entries.Count);

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d", entries[1].FullUrl);
            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            Assert.IsFalse(entries[1].Resource!.ToPocoNode().ContainedResources().Any());
            Assert.IsNotNull(entries[1].Resource!.ToPocoNode().Children().First());

            Assert.AreEqual("a", entries[2].Resource!.Id);

            var entry6 = entries[6].Resource;
            Assert.AreEqual(2, entry6!.ToPocoNode().ContainedResources().Count());
            Assert.IsFalse(entry6.ToPocoNode().BundledResources().Any());
            Assert.AreEqual("orgY", (entry6.ToPocoNode().ContainedResources().Skip(1).First().Child<PrimitiveNode>("id")?.Value));
        }

        [TestMethod]
        public void GetFullUrl()
        {
            var entries = _bundleNode!.BundledResources().ToList();

            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].Child("resource")?.FindFullUrl());

            var entry3 = entries[3].Child("resource");
            entry3 = entry3?.FlatChildren("managingOrganization").FirstOrDefault();
            Assert.IsNotNull(entry3);
            entry3 = entry3.FlatChildren("reference").FirstOrDefault();
            Assert.IsNotNull(entry3);
            Assert.AreEqual(entries[3].Child("resource")?.FindFullUrl(), entry3.FindFullUrl());
            Assert.AreEqual(entry3.GetParentResource()!.FindFullUrl(), entry3.FindFullUrl());

            var entry6 = entries[6].FlatChildren("resource").FirstOrDefault();
            entry6 = entry6?.ContainedResources().Skip(1).FirstOrDefault();
            Assert.IsNotNull(entry6);
            Assert.AreEqual("orgY", entry6.FlatChildren("id").FirstOrDefault()?.GetValue());
            Assert.AreEqual(entries[6].Child("resource")?.FindFullUrl(), entry6.FindFullUrl());
            Assert.AreEqual(entry6.GetParentResource()!.FindFullUrl(), entry6.FindFullUrl());
        }

        [TestMethod]
        public void TestMakeAbsolute()
        {
            var inner0 = _bundleNode!.BundledResources().First().Child("resource")?.FlatChildren("active").SingleOrDefault();
            Assert.IsNotNull(inner0);

            Assert.AreEqual("http://example.org/fhir/Patient/3", inner0.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner0.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner0.MakeAbsolute("http://example.org/fhir/Organization/5"));

            var inner1 = _bundleNode.BundledResources().Skip(1).First().Child("resource")?.FlatChildren("active").SingleOrDefault();

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d/3", inner1!.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner1!.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner1!.MakeAbsolute("http://example.org/fhir/Organization/5"));
        }

        [TestMethod]
        public void TestContainedCanResolveToContainer()
        {
            Assert.IsNull(PocoNodeExtensions.Resolve(_bundleNode!, "#"));

            var patient = _bundleNode!.BundledResources().Skip(6).First().Child("resource")?.First();
            Assert.IsNull(PocoNodeExtensions.Resolve(patient, "#"));

            var containedOrg = patient?.ContainedResources().First();
            Assert.AreEqual("Patient", PocoNodeExtensions.Resolve(containedOrg, "#")!.Poco.TypeName);

            var containedId = containedOrg?.Child("id")?.First();
            Assert.AreEqual("Patient", PocoNodeExtensions.Resolve(containedId, "#")!.Poco.TypeName);
        }

        [TestMethod]
        public void TestResolve()
        {
            PocoNode inner7 = _bundleNode!.NavigateTo("entry[6].resource.managingOrganization").Single();

            Assert.AreEqual("Bundle.entry[6].resource[0]", PocoNodeExtensions.Resolve(inner7, "http://example.org/fhir/Patient/e")!.GetLocation());
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", PocoNodeExtensions.Resolve(inner7, "#orgY")!.GetLocation());
            Assert.AreEqual("Bundle.entry[6].resource[0]", PocoNodeExtensions.Resolve(inner7, "#e")!.GetLocation());
            Assert.AreEqual("Bundle.entry[5].resource[0]", PocoNodeExtensions.Resolve(inner7, "http://example.org/fhir/Patient/d")!.GetLocation());
            Assert.AreEqual("Bundle.entry[5].resource[0]", PocoNodeExtensions.Resolve(inner7, "Patient/d")!.GetLocation());
            Assert.AreEqual("Bundle.entry[1].resource[0]", PocoNodeExtensions.Resolve(inner7, "urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d")!.GetLocation());
            Assert.IsNull(PocoNodeExtensions.Resolve(inner7, "#d"));
            Assert.IsNull(PocoNodeExtensions.Resolve(inner7, "http://nu.nl/3"));

            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", PocoNodeExtensions.Resolve(inner7)!.GetLocation());
            Assert.IsTrue(inner7!.Child("reference") is not null);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", PocoNodeExtensions.Resolve(inner7.Child("reference")!.First())!.GetLocation());

            string lastUrlResolved = "";

            Assert.IsNull(PocoNodeExtensions.Resolve(inner7, "#d", externalResolve));
            Assert.AreEqual("#d", lastUrlResolved);
            Assert.IsNull(PocoNodeExtensions.Resolve(inner7, "http://nu.nl/3", externalResolve));
            Assert.AreEqual("http://nu.nl/3", lastUrlResolved);

            PocoNode externalResolve(string url)
            {
                lastUrlResolved = url;
                return null;
            }
        }
        
        [TestMethod]
        public void TestVersionedReferenceResolution()
        {
            var b = new Bundle()
            {
                Entry = new List<Bundle.EntryComponent>
                {
                    new() { FullUrl = "Patient/lol", Resource = new Patient{Id = "a", Meta = new Meta(){VersionId = "1"}}},
                    new() { FullUrl = "Patient/lol", Resource = new Patient{Id = "b", Meta = new Meta(){VersionId = "2"}}},
                    new() { FullUrl = "exampleReferencingVersionedResource", Resource = new Patient
                    {
                        Link = [
                            new ()
                            {
                                Other = new ResourceReference("Patient/lol/_history/2")
                            }
                        ]
                    }},
                    new() { FullUrl = "exampleReferencingUnversionedResource", Resource = new Patient
                    {
                        Link = [
                            new ()
                            {
                                Other = new ResourceReference("Patient/lol")
                            }
                        ]
                    }}
                }
            };

            var node = b.ToPocoNode();
            var bundled = node.BundledResources();
            Assert.AreEqual(4, bundled.Count());
            
            Assert.AreEqual("Bundle.entry[1].resource[0]", node.NavigateTo("entry[2].resource.link.other").First().Resolve()!.GetLocation());
            Assert.AreEqual("Bundle.entry[0].resource[0]", node.NavigateTo("entry[3].resource.link.other").First().Resolve()!.GetLocation());
        }

        
        [TestMethod]
        public void Bundle_WithEntryWithoutFullUrl_ShouldNotThrow()
        {
            var bundle = new Bundle() { Type = Bundle.BundleType.Batch, Entry = [new Bundle.EntryComponent() { Resource = new Patient() }]}.ToPocoNode();

            var enumerate = () => bundle.BundledResources();
            enumerate.Should().NotThrow().Subject.Should().ContainSingle(c => c.Child("fullUrl") == null);
        }
    }
}