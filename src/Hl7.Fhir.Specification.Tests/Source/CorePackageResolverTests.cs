using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Firely.Fhir.Packages.Tests
{
    [TestClass]
    public class CorePackageResolverTests
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
    }
}
