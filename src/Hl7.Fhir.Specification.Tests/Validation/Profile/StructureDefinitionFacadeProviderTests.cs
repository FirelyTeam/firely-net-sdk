using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Validation.Profile;
using Hl7.Fhir.Validation.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hl7.Fhir.Specification.Tests.Validation.Profile
{
    public class StructureDefinitionFacadeProviderTests: IClassFixture<ValidationFixture>
    {
        private readonly IResourceResolver _resolver;

        public StructureDefinitionFacadeProviderTests(ValidationFixture fixture)
        {
            _resolver = new CachedResolver(new SnapshotSource(fixture.Resolver));
        }

        [Fact]
        public void EmptyStructureDefinitionTest()
        {
            var structureDef = new StructureDefinition();
            var facade = new StructureDefinitionFacade(structureDef);
            facade.Url = "http://test";
            structureDef.Url.Should().Be("http://test");
            structureDef.Url = "http://test2";
            facade.Url.Should().Be("http://test2");
        }

        [Fact] // todo: theory
        public void RoundTripTest()
        {
            var canonical = "";

            var source = (StructureDefinition)_resolver.ResolveByCanonicalUri(canonical);
            var sourceFacade = new StructureDefinitionFacade(source);

            var target = new StructureDefinition();
            var targetFacade = new StructureDefinitionFacade(target);


        }
    }
}
