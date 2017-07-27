using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Validation
{
    [TestClass]
    public class ScopedNavigatorTests
    {
        IElementNavigator _bundleNav;

        [TestInitialize]
        public void SetupSource()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            _bundleNav = new PocoNavigator(bundle);
        }

        private ScopedNavigator NewNav() => (ScopedNavigator)new ScopedNavigator(_bundleNav);

        [TestMethod]
        public void KeepScopes()
        {
            var nav = NewNav();

            Assert.IsNull(nav.Parent);
            Assert.IsTrue(nav.AtBundle);

            Assert.IsTrue(nav.MoveToFirstChild("entry"));
            Assert.AreEqual("Bundle.entry[0]", nav.Location);
            Assert.AreEqual("Bundle", nav.Parent.Location);
            Assert.IsFalse(nav.AtBundle);
            Assert.IsFalse(nav.AtResource);

            Assert.IsTrue(nav.MoveToFirstChild("resource"));
            Assert.AreEqual("Bundle.entry[0].resource[0]", nav.Location);
            Assert.AreEqual("Bundle", nav.Parent.Location);
            Assert.IsFalse(nav.AtBundle);
            Assert.IsTrue(nav.AtResource);

            Assert.IsTrue(nav.MoveToFirstChild("active"));
            Assert.AreEqual("Bundle.entry[0].resource[0].active[0]", nav.Location);
            Assert.AreEqual("Bundle.entry[0].resource[0]", nav.Parent.Location);
            Assert.AreEqual("Bundle", nav.Parent.Parent.Location);
            Assert.IsFalse(nav.AtBundle);
            Assert.IsFalse(nav.AtResource);
        }

        [TestMethod]
        public void KeepScopesContained()
        {
            var nav = NewNav();
            var entry = (ScopedNavigator)nav.Children("entry").Skip(6).First();
            Assert.AreEqual("Bundle.entry[6]", entry.Location);
            Assert.AreEqual("Bundle", entry.Parent.Location);

            Assert.IsTrue(entry.MoveToFirstChild("resource"));
            Assert.IsTrue(entry.MoveToFirstChild("contained"));
            Assert.IsFalse(entry.AtBundle);
            Assert.IsTrue(entry.AtResource);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", entry.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", entry.Parent.Location);
            Assert.AreEqual("Bundle", entry.Parent.Parent.Location);

            Assert.IsTrue(entry.MoveToFirstChild("id"));
            Assert.IsFalse(entry.AtBundle);
            Assert.IsFalse(entry.AtResource);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0].id[0]", entry.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", entry.Parent.Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", entry.Parent.Parent.Location);
            Assert.AreEqual("Bundle", entry.Parent.Parent.Parent.Location);
            Assert.AreEqual(3, entry.Parents().Count());

        }

        [TestMethod]
        public void GetChildResources()
        {
            var nav = NewNav();
            Assert.AreEqual(0, nav.ContainedResources().Count());

            var entries = nav.BundledResources().ToList();
            Assert.AreEqual(7, entries.Count);

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d", entries[1].FullUrl);
            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            Assert.IsFalse(entries[1].Resource.ContainedResources().Any());
            Assert.IsTrue(entries[1].Resource.MoveToFirstChild("active"));

            Assert.AreEqual("#a", entries[2].Resource.Id());

            var entry6 = entries[6].Resource;
            Assert.AreEqual(2, entry6.ContainedResources().Count());
            Assert.IsFalse(entry6.BundledResources().Any());
            Assert.AreEqual("#orgY", entry6.ContainedResources().Skip(1).First().Id());
        }

        [TestMethod]
        public void GetFullUrl()
        {
            var nav = NewNav();
            var entries = nav.BundledResources().ToList();

            Assert.AreEqual("http://example.org/fhir/Patient/b", entries[3].FullUrl);

            var entry3 = entries[3].Resource;
            Assert.IsTrue(entry3.MoveToFirstChild("managingOrganization"));
            Assert.IsTrue(entry3.MoveToFirstChild("reference"));
            Assert.AreEqual(entries[3].FullUrl, entry3.FullUrl());
            Assert.AreEqual(entry3.Parent.FullUrl(), entry3.FullUrl());

            var entry6 = entries[6].Resource;
            Assert.IsTrue(entry6.MoveToFirstChild("contained"));
            Assert.IsTrue(entry6.MoveToNext("contained"));
            Assert.AreEqual("#orgY", entry6.Id());
            Assert.AreEqual(entries[6].FullUrl, entry6.FullUrl());
            Assert.AreEqual(entry6.Parent.FullUrl(), entry6.FullUrl());
        }

        [TestMethod]
        public void TestMakeAbsolute()
        {
            var nav = NewNav();
            var inner0 = (ScopedNavigator)nav.Children("entry").First().Children("resource").Children("active").SingleOrDefault();
            Assert.IsNotNull(inner0);

            Assert.AreEqual("http://example.org/fhir/Patient/3", inner0.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner0.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner0.MakeAbsolute("http://example.org/fhir/Organization/5"));

            var inner1 = (ScopedNavigator)nav.Children("entry").Skip(1).First().Children("resource").Children("active").SingleOrDefault();

            Assert.AreEqual("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d/3", inner1.MakeAbsolute("Patient/3"));
            Assert.AreEqual("http://nu.nl/myPat/3x", inner1.MakeAbsolute("http://nu.nl/myPat/3x"));
            Assert.AreEqual("http://example.org/fhir/Organization/5", inner1.MakeAbsolute("http://example.org/fhir/Organization/5"));
        }

        [TestMethod]
        public void TestResolve()
        {
            var nav = NewNav();

            var inner7 = (ScopedNavigator)nav.Children("entry").Skip(6).First().Children("resource").Children("managingOrganization").SingleOrDefault();

            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7.Resolve("http://example.org/fhir/Patient/e").Location);
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Resolve("#orgY").Location);
            Assert.AreEqual("Bundle.entry[6].resource[0]", inner7.Resolve("#e").Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7.Resolve("http://example.org/fhir/Patient/d").Location);
            Assert.AreEqual("Bundle.entry[5].resource[0]", inner7.Resolve("Patient/d").Location);
            Assert.AreEqual("Bundle.entry[1].resource[0]", inner7.Resolve("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d").Location);
            Assert.IsNull(inner7.Resolve("#d"));
            Assert.IsNull(inner7.Resolve("http://nu.nl/3"));

            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Resolve().Location);
            Assert.IsTrue(inner7.MoveToFirstChild("reference"));
            Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", inner7.Resolve().Location);

            string lastUrlResolved = "";

            Assert.IsNull(inner7.Resolve("#d", externalResolve));
            Assert.AreEqual("#d", lastUrlResolved);
            Assert.IsNull(inner7.Resolve("http://nu.nl/3", externalResolve));
            Assert.AreEqual("http://nu.nl/3", lastUrlResolved);

            IElementNavigator externalResolve(string url)
            {
                lastUrlResolved = url;
                return null;
            }
        }
    }
}
