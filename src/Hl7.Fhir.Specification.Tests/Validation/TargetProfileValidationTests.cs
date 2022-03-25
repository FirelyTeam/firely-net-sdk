using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System.Collections.Generic;
using Xunit;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class TargetProfileValidationTests
    {
        private class TestResolver : IResourceResolver
        {
            public static string Url = "http://test.org/fhir/Organization/3141";

            private static Organization dummy = new() { Id = "3141", Name = "Dummy" };

            public Resource ResolveByCanonicalUri(string uri) => ResolveByUri(uri);
            public Resource ResolveByUri(string uri) =>
                uri == Url ? dummy : null;
        }


        [Fact]
        public void AvoidsRedoingProfileValidation()
        {
            var all = new Bundle() { Type = Bundle.BundleType.Batch };
            var org1 = new Organization() { Id = "org1", Name = "Organization 1" };
            var org2 = new Organization() { Id = "org2", Name = "Organization 2", Meta = new() { Profile = new[] { TestProfileArtifactSource.PROFILED_ORG_URL } } };

            all.Entry.Add(new() { FullUrl = refr("org1"), Resource = org1 });
            all.Entry.Add(new() { FullUrl = refr("org2"), Resource = org2 });

            var bothRef = new List<ResourceReference>()
            {
                new ResourceReference("#refme"),
                new ResourceReference(refr("org1")),
                new ResourceReference(refr("org2")),
                new ResourceReference("http://test.org/fhir/Organization/3141")
            };

            var pat1 = new Patient()
            {
                Meta = new() { Profile = new[] { "http://validationtest.org/fhir/StructureDefinition/PatientWithReferences" } },
                Id = "pat1",
                GeneralPractitioner = bothRef,
                ManagingOrganization = new(refr("org1")),
                BirthDate = new("2011crap")
            };

            pat1.Contained.Add(new Organization { Id = "refme", Name = "referred" });

            var pat2 = new Patient()
            {
                Meta = new() { Profile = new[] { "http://validationtest.org/fhir/StructureDefinition/PatientWithReferences" } },
                Id = "pat2",
                GeneralPractitioner = bothRef,
                ManagingOrganization = new(refr("org2"))
            };

            pat2.Contained.Add(new Organization { Id = "refme", Name = "referred" });

            all.Entry.Add(new() { FullUrl = refr("pat1"), Resource = pat1 });
            all.Entry.Add(new() { FullUrl = refr("pat2"), Resource = pat2 });

            var visitResolver = new TestResolver();

            var cr = new SnapshotSource(
                      new CachedResolver(
                        new MultiResolver(
                          new TestProfileArtifactSource(),
                          new ZipSource("specification.zip"),
                          visitResolver)));

            var validatorSettings = new ValidationSettings
            {
                GenerateSnapshot = true,
                ResourceResolver = cr,
                ResolveExternalReferences = true
            };
            var validator = new Validator(validatorSettings);

            var result = validator.Validate(all);
            result.Success.Should().Be(false);
            result.Errors.Should().Be(1);
            result.ToString().Should().Contain("does not match regex");

            var validationState = result.Annotation<ValidationState>();

            validationState.Global.ResourcesValidated.Value.Should().Be(16);
            validationState.Instance.InternalValidations.Count.Should().Be(8);

            static string refr(string x) => "http://test.org/fhir/" + x;
        }
    }
}
