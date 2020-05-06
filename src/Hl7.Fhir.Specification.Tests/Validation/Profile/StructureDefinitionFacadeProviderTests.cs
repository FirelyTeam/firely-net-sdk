using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Validation.Model;
using Hl7.Fhir.Validation.Model;
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
            var mapper = new StructureDefMapper();
            var context = new MappingContext();
            var universal = structureDef.Map(context, mapper);
            structureDef = universal.Map(context, mapper);
        }

        [Fact] // todo: theory
        public void RoundTripTest()
        {
            


        }
    }
}
