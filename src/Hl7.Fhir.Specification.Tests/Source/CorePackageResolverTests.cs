using FluentAssertions;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Firely.Fhir.Packages.Tests
{
    [TestClass]
    public class CorePackageResolverTests
    {
        [TestMethod]
        public async Task ResolvesStructureDefinitions()
        {
            var resolver = new CorePackageSource();

            var pat = await resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/StructureDefinition/Patient");
            pat.Should().NotBeNull();
        }
    }
}
