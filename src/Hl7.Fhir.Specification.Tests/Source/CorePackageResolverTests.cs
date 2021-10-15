using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Firely.Fhir.Packages.Tests
{
    [TestClass]
    public class CorePackageResolverTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task ResolvesStructureDefinitions()
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
    }
}
