using Hl7.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    [TestClass]
    public class ScopeTrackerTests
    {
        [TestInitialize]
        public void SetupSource()
        {
            source = new CachedResolver(
                new MultiResolver(
                    new TestProfileArtifactSource(),
                    new ZipSource("specification.zip")));

            var ctx = new ValidationSettings() { ResourceResolver = source, GenerateSnapshot = true, Trace = false };

            validator = new Validator(ctx);
        }

        IResourceResolver source;
        Validator validator;

        [TestMethod]
        public void HarvestScopedIdentifiers()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            var cpNav = new PocoNavigator(bundle);

            var references = ReferenceHarvester.Harvest(cpNav);
            Assert.AreEqual(7, references.Count());
            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with no contained resources, an Organization
            var orgNav = references.Single(r => r.Uri == "urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d");
            Assert.AreEqual("Organization", orgNav.Container.TypeName);
            var orgReferences = ReferenceHarvester.Harvest(orgNav.Container);
            Assert.AreEqual(0, orgReferences.Count());
            Assert.AreEqual("Organization", orgNav.Container.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with contained resources, a Patient
            var patNav = references.First(r => r.Uri == "http://example.org/fhir/Patient/e");
            Assert.AreEqual("Patient", patNav.Container.TypeName);
            var patReferences = ReferenceHarvester.Harvest(patNav.Container);
            Assert.AreEqual(2, patReferences.Count());
            Assert.AreEqual("#orgX", patReferences.First().Uri);

            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else
        }

        [TestMethod]
        public void ScopeAndChildCreation()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            var cpNav = new PocoNavigator(bundle);

            var bundleScope = new Scope(cpNav, "http://somebase.org/fhir/Bundle/test");

            Assert.AreEqual(7, bundleScope.Children.Count());
            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with no contained resources, an Organization
            var orgScope = bundleScope.ResolveChild("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d");
            Assert.AreEqual("Organization", orgScope.Container.TypeName);
            Assert.AreEqual(0, orgScope.Children.Count());
            Assert.AreEqual("Organization", orgScope.Container.TypeName);
            Assert.AreEqual("Bundle", bundleScope.Container.TypeName);

            // Get one of the entries with contained resources, a Patient
            var patScope = bundleScope.ResolveChild("http://example.org/fhir/Patient/e");
            Assert.AreEqual("Patient", patScope.Container.TypeName);
            Assert.AreEqual(2, patScope.Children.Count());
            Assert.AreEqual("#orgX", patScope.Children.First().Uri);

            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else
        }

        [TestMethod]
        public void BuildScopeList()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            IElementNavigator cpNav = new PocoNavigator(bundle);

            var tracker = new ScopeTracker();
            tracker.Enter(cpNav);

            var entries = cpNav.GetChildrenByName("entry").GetChildrenByName("resource");

            int index = 0;
            foreach (var entry in entries)
            {
                tracker.Enter(entry);

                if (index == 6)
                {
                    var orgX = entry.GetChildrenByName("contained").First();

                    tracker.Enter(orgX);
                    Assert.AreEqual("http://example.org/fhir/Patient/e", tracker.ContextFullUrl(orgX));
                    Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", tracker.ResourceContext(orgX).Path);
                    tracker.Leave(orgX);

                    var careProvRef = entry.GetChildrenByName("managingOrganization").GetChildrenByName("reference").Single();
                    Assert.AreEqual("Bundle.entry[6].resource[0]", tracker.ResourceContext(careProvRef).Path);

                    tracker.Enter(careProvRef);
                    Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", tracker.Resolve(careProvRef, "#orgY").Path);
                    Assert.AreEqual("Bundle.entry[2].resource[0]", tracker.Resolve(careProvRef, "http://example.org/fhir/Patient/a").Path);
                    tracker.Leave(careProvRef);
                }

                tracker.Leave(entry);

                index++;
            }

            tracker.Leave(cpNav);
        }


        [TestMethod]
        public void TestResolution()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            IElementNavigator cpNav = new PocoNavigator(bundle);

            var tracker = new ScopeTracker();
            tracker.Enter(cpNav);

            var entries = cpNav.GetChildrenByName("entry");

            var index = 0;
            foreach(var entry in entries)
            {
                tracker.Enter(entry);

                var resource = entry.GetChildrenByName("resource").First();
                tracker.Enter(resource);

                if(index == 2 || index == 3 || index == 4 || index == 6)
                {
                    var refr = resource.GetChildrenByName("managingOrganization").GetChildrenByName("reference").First();
                    var res = tracker.Resolve(refr, refr.Value as string);
                    Assert.IsNotNull(res);
                }
                else if (index == 5)
                {
                    var refr = resource.GetChildrenByName("managingOrganization").GetChildrenByName("reference").First();
                    var res = tracker.Resolve(refr, refr.Value as string);
                    Assert.IsNull(res);
                }

                tracker.Leave(resource);


                tracker.Leave(entry);

                index += 1;
            }
        }
    }
}

