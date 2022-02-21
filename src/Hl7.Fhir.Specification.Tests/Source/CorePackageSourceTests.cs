using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Firely.Fhir.Packages.Tests
{
    [TestClass]
    public class CorePackageSourceTests
    {
        private readonly CorePackageSource _resolver = new();

        [TestMethod]
        public async System.Threading.Tasks.Task TestResolveByCanonicalUri()
        {
            //check StructureDefinitions
            var pat = await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/StructureDefinition/Patient").ConfigureAwait(false) as StructureDefinition;
            pat.Should().NotBeNull();
            pat.Url.Should().Be("http://hl7.org/fhir/StructureDefinition/Patient");

            //check expansions
            var adm_gender = await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/administrative-gender").ConfigureAwait(false) as ValueSet;
            adm_gender.Should().NotBeNull();
            adm_gender.Expansion.Contains.Should().Contain(c => c.System == "http://hl7.org/fhir/administrative-gender" && c.Code == "other");
        }

        [TestMethod]
        public void TestListFileNames()
        {
            //check StructureDefinitions
            var names = _resolver.ListArtifactNames();
            names.Should().Contain("StructureDefinition-Patient.json");
        }

        [TestMethod]
        public void TestLoadArtifactByName()
        {
            //check StructureDefinitions
            var stream = _resolver.LoadArtifactByName("StructureDefinition-Patient.json");

            using var reader = new StreamReader(stream);
            var artifact = reader.ReadToEnd();

            artifact.Should().Contain("resourceType\" : \"StructureDefinition\"");
            artifact.Should().Contain("\"id\" : \"Patient\"");
        }

        [TestMethod]
        public void TestLoadArtifactByPath()
        {
            //check StructureDefinitions
            var stream = _resolver.LoadArtifactByPath("package/StructureDefinition-Patient.json");

            using var reader = new StreamReader(stream);
            var artifact = reader.ReadToEnd();

            artifact.Should().Contain("resourceType\" : \"StructureDefinition\"");
            artifact.Should().Contain("\"id\" : \"Patient\"");
        }

        [TestMethod]
        public void TestListResourceUris()
        {
            //check StructureDefinitions
            var names = _resolver.ListResourceUris();
            names.Should().Contain("http://hl7.org/fhir/StructureDefinition/Patient");
            names.Should().Contain("http://hl7.org/fhir/administrative-gender");
        }

        [TestMethod]
        public void TestGetCodeSystemByValueSet()
        {
            var cs = _resolver.FindCodeSystemByValueSet("http://hl7.org/fhir/ValueSet/address-type");
            cs.Should().NotBeNull();
            cs.Url.Should().Be("http://hl7.org/fhir/address-type");
        }

        [TestMethod]
        public void TestGetConceptMap()
        {
            var cms = _resolver.FindConceptMaps(sourceUri: "http://hl7.org/fhir/ValueSet/data-absent-reason", targetUri: "http://terminology.hl7.org/ValueSet/v3-NullFlavor");
            cms.Should().NotBeEmpty();
            cms.Should().Contain(c => c.Url == "http://hl7.org/fhir/ConceptMap/cm-data-absent-reason-v3");
            cms.Should().NotContain(c => c.Url == "http://hl7.org/fhir/ConceptMap/cm-contact-point-use-v3");
        }

        [TestMethod]
        public void TestGetNamingSystem()
        {
            var ns = _resolver.FindNamingSystem("http://snomed.info/sct");
            ns.Should().NotBeNull();
            ns.UniqueId.Should().Contain(i => i.Value == "http://snomed.info/sct");
            ns.UniqueId.Should().Contain(i => i.Value == "2.16.840.1.113883.6.96");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestGetArtifactByUri()
        {
            var pat = await _resolver.ResolveByUriAsync("StructureDefinition/Patient").ConfigureAwait(false) as StructureDefinition;
            pat.Should().NotBeNull();
            pat.Url.Should().Be("http://hl7.org/fhir/StructureDefinition/Patient");
        }

        //If this fails, valueset-currencies.json in the fhir-core-expansions package doesn't contain the correct display value for "STN" in the expansion.
        [TestMethod]
        public async System.Threading.Tasks.Task CheckCorrectMoneyDescription()
        {
            var vs = await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/currencies").ConfigureAwait(false) as ValueSet;
            vs.Expansion.Should().NotBeNull();
            var stn = vs.Expansion.Contains.Where(c => c.Code == "STN").FirstOrDefault();
            stn.Display.Should().Be("São Tomé and Príncipe dobra");
        }
    }
}
