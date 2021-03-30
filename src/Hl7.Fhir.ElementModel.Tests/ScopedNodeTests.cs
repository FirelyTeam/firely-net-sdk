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
using T = System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class ScopedNodeTests
    {
        ScopedNode? _bundleNode;

        [TestInitialize]
        public void SetupSource()
        {
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            _bundleNode = new ScopedNode(bundle.ToTypedElement());
        }

        [TestMethod]
        public void KeepScopes()
        {
            Assert.IsNull(_bundleNode!.ParentResource);
            Assert.AreEqual("Bundle", _bundleNode.InstanceType);
            Assert.AreEqual("Bundle", _bundleNode.NearestResourceType);

            var entry = (ScopedNode)_bundleNode.Children("entry").First();
            Assert.IsNotNull(entry);
            Assert.AreEqual("Bundle.entry[0]", entry.Location);
            Assert.AreEqual("Bundle.entry[0]", entry.LocalLocation);
            Assert.AreEqual("Bundle", entry.ParentResource!.Location);
            Assert.AreEqual("Bundle", entry.ParentResource.LocalLocation);
            Assert.AreNotEqual("Bundle", entry.InstanceType);
            Assert.IsFalse(entry.AtResource);

            var resource = (ScopedNode)entry.Children("resource").First();
            Assert.IsNotNull(resource);
            Assert.AreEqual("Bundle.entry[0].resource[0]", resource.Location);
            Assert.AreEqual("Bundle.entry[0].resource[0]", resource.LocalLocation);
            Assert.AreEqual("Bundle", resource.ParentResource!.Location);
            Assert.AreNotEqual("Bundle", resource.InstanceType);
            Assert.IsTrue(resource.AtResource);

            var active = (ScopedNode)resource.Children("active").First();
            Assert.IsNotNull(active);
            Assert.AreEqual("Bundle.entry[0].resource[0].active[0]", active.Location);
            Assert.AreEqual("Organization", active.NearestResourceType);
            Assert.AreEqual("Organization.active[0]", active.LocalLocation);
            Assert.AreEqual("Bundle.entry[0].resource[0]", active.ParentResource!.Location);
            Assert.AreEqual("Bundle", active.ParentResource.ParentResource!.Location);
            Assert.AreNotEqual("Bundle", active.InstanceType);
            Assert.IsFalse(active.AtResource);
        }

        [TestMethod]
        public void KeepScopesContained()
        {
            var entry = (ScopedNode)_bundleNode!.Children("entry").Skip(6).First();
            Assert.AreEqual("Bundle.entry[6]", entry.Location);
            Assert.AreEqual("Bundle", entry.ParentResource!.Location);

            entry = entry.Children("resource").FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry);
            entry = entry!.Children("contained").FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry);
            Assert.AreNotEqual("Bundle", entry!.InstanceType);

            Assert.IsTrue(entry.AtResource);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", entry.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", entry.ParentResource!.Location);
            Assert.AreEqual("Bundle", entry.ParentResource.ParentResource!.Location);

            entry = entry.Children("id").FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry);
            Assert.AreNotEqual("Bundle", entry!.InstanceType);

            Assert.IsFalse(entry.AtResource);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0].id[0]", entry.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", entry.ParentResource!.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", entry.ParentResource.ParentResource!.Location);
            Assert.AreEqual("Bundle", entry.ParentResource.ParentResource.ParentResource!.Location);
            Assert.AreEqual(3, entry.ParentResources().Count());

        }

        [TestMethod]
        public void GetChildResources()
        {
            Assert.AreEqual(0, _bundleNode!.ContainedResources().Count());

            var entries = _bundleNode.BundledResources().ToList();
            Assert.AreEqual(7, entries.Count);

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d", entries[1].FullUrl);
            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            Assert.IsFalse(entries[1].Resource!.ContainedResources().Any());
            Assert.IsNotNull(entries[1].Resource!.Children("active").First());

            Assert.AreEqual("#a", entries[2].Resource!.Id());

            var entry6 = entries[6].Resource;
            Assert.AreEqual(2, entry6!.ContainedResources().Count());
            Assert.IsFalse(entry6.BundledResources().Any());
            Assert.AreEqual("#orgY", entry6.ContainedResources().Skip(1).First().Id());
        }

        [TestMethod]
        public void GetFullUrl()
        {
            var entries = _bundleNode!.BundledResources().ToList();

            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            var entry3 = entries[3].Resource;
            entry3 = entry3!.Children("managingOrganization").FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry3);
            entry3 = entry3!.Children("reference").FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry3);
            Assert.AreEqual(entries[3].FullUrl, entry3!.FullUrl());
            Assert.AreEqual(entry3!.ParentResource!.FullUrl(), entry3.FullUrl());

            var entry6 = entries[6].Resource;
            entry6 = entry6!.Children("contained").Skip(1).FirstOrDefault() as ScopedNode;
            Assert.IsNotNull(entry6);
            Assert.AreEqual("#orgY", entry6!.Id());
            Assert.AreEqual(entries[6].FullUrl, entry6.FullUrl());
            Assert.AreEqual(entry6.ParentResource!.FullUrl(), entry6.FullUrl());
        }

        [TestMethod]
        public void TestMakeAbsolute()
        {
            var inner0 = _bundleNode!.Children("entry").First().Children("resource").Children("active").SingleOrDefault() as ScopedNode;
            Assert.IsNotNull(inner0);

            Assert.AreEqual("http://example.org/fhir/Patient/3", inner0.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner0.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner0.MakeAbsolute("http://example.org/fhir/Organization/5"));

            var inner1 = _bundleNode.Children("entry").Skip(1).First().Children("resource").Children("active").SingleOrDefault() as ScopedNode;

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d/3", inner1.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner1.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner1.MakeAbsolute("http://example.org/fhir/Organization/5"));
        }

        [TestMethod]
        public void TestResolve()
        {
            var inner7 = _bundleNode!.Children("entry").Skip(6).First().Children("resource").Children("managingOrganization").SingleOrDefault() as ScopedNode;

            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7!.Resolve("http://example.org/fhir/Patient/e").Location);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7!.Resolve("#orgY").Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7!.Resolve("#e").Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7!.Resolve("http://example.org/fhir/Patient/d").Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7!.Resolve("Patient/d").Location);
            Assert.AreEqual("Bundle.entry[1].resource[0]", inner7!.Resolve("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d").Location);
            Assert.IsNull(inner7.Resolve("#d"));
            Assert.IsNull(inner7.Resolve("http://nu.nl/3"));

            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7!.Resolve().Location);
            Assert.IsTrue(inner7!.Children("reference").Any());
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Children("reference").First().Resolve().Location);

            string lastUrlResolved = "";

            Assert.IsNull(inner7.Resolve("#d", externalResolve));
            Assert.AreEqual("#d", lastUrlResolved);
            Assert.IsNull(inner7.Resolve("http://nu.nl/3", externalResolve));
            Assert.AreEqual("http://nu.nl/3", lastUrlResolved);

            ITypedElement? externalResolve(string url)
            {
                lastUrlResolved = url;
                return null;
            }
        }

        [TestMethod]
        public void AtResourceWithoutDefinition()
        {
            var provider = new NoTypeProvider();
            var elementNode = ElementNode.Root(provider, "Patient");
            elementNode.Add(provider, "active", true, "boolean");

            var node = new ScopedNode(new TypedElementWithoutDefinition(elementNode));

            Assert.IsTrue(node.AtResource);
            var inner = (ScopedNode)node.Children().First();
            Assert.IsFalse(inner.AtResource);
        }

        [TestMethod]
        public void CcdaWithXhtmlTag()
        {
            bool CCDATypeNameMapper(string typeName, out string canonical)
            {
                if (ModelInfo.IsPrimitive(typeName))
                    canonical = "http://hl7.org/fhir/StructureDefinition/" + typeName;
                else
                    canonical = "http://hl7.org/fhir/cda/StructureDefinition/" + typeName;

                return true;
            }

            var ccdaInstanceXml = File.ReadAllText(Path.Combine("TestData", "CCDA_With_Xhtml_Tag.xml"));
            var ccdaNode = FhirXmlNode.Parse(ccdaInstanceXml);

            var summaryProvider = new StructureDefinitionSummaryProvider(new CCDAResourceResolver(), CCDATypeNameMapper);

            var typedElement = ccdaNode.ToTypedElement(summaryProvider);
            Assert.IsNotNull(typedElement);

            var errors = typedElement.VisitAndCatch();

            Assert.IsTrue(!errors.Any());

            var assertXHtml = typedElement.Children("text");

            Assert.IsNotNull(assertXHtml);
            Assert.AreEqual(1, assertXHtml.Count());
            Assert.AreEqual("text", assertXHtml.First().Name);
            Assert.AreEqual("xhtml", assertXHtml.First().InstanceType);
            Assert.AreEqual("text", assertXHtml.First().Location);
            Assert.IsNotNull(assertXHtml.First().Value);


        }

        private class CCDAResourceResolver : IAsyncResourceResolver
        {
            private readonly Dictionary<string, StructureDefinition> _cache;
            private readonly ZipSource _zipSource;
            private readonly CachedResolver _coreResolver;

            public CCDAResourceResolver()
            {
                _cache = new Dictionary<string, StructureDefinition>();
                _zipSource = new ZipSource("specification.zip");
                _coreResolver = new CachedResolver(new MultiResolver(_zipSource, new DirectorySource("TestData/TestSd")));
            }

            public async T.Task<Resource> ResolveByCanonicalUriAsync(string uri)
            {
                if (_cache.TryGetValue(uri, out StructureDefinition? sd))
                    return sd;

                sd = await _coreResolver.FindStructureDefinitionAsync(uri);
                if (!sd.HasSnapshot)
                {
                    var snapShotGenerator = new SnapshotGenerator(_coreResolver);
                    await snapShotGenerator.UpdateAsync(sd);
                    _cache.Add(sd.Url, sd);
                }

                return sd;
            }

            public T.Task<Resource> ResolveByUriAsync(string uri) => throw new NotImplementedException();
        }

        private class TypedElementWithoutDefinition : ITypedElement, IResourceTypeSupplier
        {
            private readonly ITypedElement _wrapped;

            public TypedElementWithoutDefinition(ITypedElement wrapped) => _wrapped = wrapped;

            public string Name => _wrapped.Name;

            public string InstanceType => _wrapped.InstanceType;

            public object Value => _wrapped.Value;

            public string Location => _wrapped.Location;

            public IElementDefinitionSummary? Definition => null;

            public string? ResourceType => !string.IsNullOrEmpty(InstanceType) && char.IsUpper(InstanceType, 0) ? InstanceType : null;

            public IEnumerable<ITypedElement> Children(string? name = null) =>
                _wrapped.Children(name).Select(c => new TypedElementWithoutDefinition(c));
        }

        private class NoTypeProvider : IStructureDefinitionSummaryProvider
        {
            public IStructureDefinitionSummary? Provide(string canonical) => null;
        }
    }
}