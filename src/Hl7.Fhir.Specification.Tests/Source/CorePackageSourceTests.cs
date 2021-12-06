using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Firely.Fhir.Packages.Tests
{
    [TestClass]
    public class CorePackageSourceTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestResolveByCanonicalUri()
        {
            var resolver = new CorePackageSource();

            //check StructureDefinitions
            var pat = await resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/StructureDefinition/Patient");
            pat.Should().NotBeNull();

            //check expansions
            var adm_gender = await resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/administrative-gender") as ValueSet;
            adm_gender.Should().NotBeNull();
            adm_gender.Expansion.Contains.Should().Contain(c => c.System == "http://hl7.org/fhir/administrative-gender" && c.Code == "other");
        }

        [TestMethod]
        public void TestListFileNames()
        {
            var resolver = new CorePackageSource();

            //check StructureDefinitions
            var names = resolver.ListArtifactNames();
            names.Should().Contain("StructureDefinition-Patient.json");
        }

        [TestMethod]
        public void TestLoadArtifactByName()
        {
            var resolver = new CorePackageSource();

            //check StructureDefinitions
            var stream = resolver.LoadArtifactByName("StructureDefinition-Patient.json");

            using var reader = new StreamReader(stream);
            var artifact = reader.ReadToEnd();

            artifact.Should().StartWith("{\"resourceType\":\"StructureDefinition\",\"id\":\"Patient\"");
        }

        [TestMethod]
        public void TestListResourceUris()
        {
            var resolver = new CorePackageSource();

            //check StructureDefinitions
            var names = resolver.ListResourceUris();
            names.Should().Contain("http://hl7.org/fhir/StructureDefinition/Patient");
            names.Should().Contain("http://hl7.org/fhir/administrative-gender");
        }

        [TestMethod]
        public void TestGetCodeSystemByValueSet()
        {
            var resolver = new CorePackageSource();
            var cs = resolver.FindCodeSystemByValueSet("http://hl7.org/fhir/ValueSet/address-type");
            cs.Should().NotBeNull();
            cs.Url.Should().Be("http://hl7.org/fhir/address-type");
        }

        [TestMethod]
        public void TestGetConceptMap()
        {
            var resolver = new CorePackageSource();
            var cms = resolver.FindConceptMaps(sourceUri: "http://hl7.org/fhir/ValueSet/data-absent-reason", targetUri: "http://hl7.org/fhir/ValueSet/v3-NullFlavor");
            cms.Should().NotBeEmpty();
            cms.Should().Contain(c => c.Url == "http://hl7.org/fhir/ConceptMap/cm-data-absent-reason-v3");
            cms.Should().NotContain(c => c.Url == "http://hl7.org/fhir/ConceptMap/cm-contact-point-use-v3");
        }

        [TestMethod]
        public void TestGetNamingSystem()
        {
            var resolver = new CorePackageSource();
            var ns = resolver.FindNamingSystem("http://snomed.info/sct");
            ns.Should().NotBeNull();
            ns.UniqueId.Should().Contain(i => i.Value == "http://snomed.info/sct");
            ns.UniqueId.Should().Contain(i => i.Value == "2.16.840.1.113883.6.96");
        }
    }
}
