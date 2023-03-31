using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests.Source
{
    public partial class ZipSourceTests
    {
        //issue #2031
        [TestMethod]
        public void TestIncorrectFullUrlForValuesetComposeIncludeValueSetTitle()
        {
            var resolver = ZipSource.CreateValidationSource();
            var sd = resolver.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle") as StructureDefinition;
            sd.Url.Should().Be("http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle");
            sd.Id.Should().Be("valueset-compose-include-valueSetTitle");
        }
    }
}
