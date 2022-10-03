using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Validator = Hl7.Fhir.Validation.Validator;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class IssuesTests
    {
        /// <summary>
        /// See https://github.com/FirelyTeam/firely-net-sdk/issues/474
        /// </summary>
        [TestMethod]
        public async Tasks.Task Issue474StartdateIs0001_01_01()
        {
            var json = "{ \"resourceType\": \"Patient\", \"active\": true, \"contact\": [{\"organization\": {\"reference\": \"Organization/1\", \"display\": \"Walt Disney Corporation\" }, \"period\": { \"start\": \"0001-01-01\", \"end\": \"2018\" } } ],}";

            var ctx = new ValidationSettings()
            {
                ResourceResolver = ZipSource.CreateValidationSource(),
            };

            var validator = new Validator(ctx);

            var pat = await new FhirJsonParser().ParseAsync<Patient>(json);

            var report = validator.Validate(pat);
            Assert.IsTrue(report.Success);
        }


        //Test for issue #2183 on Github, type check didn't fail because definition has children.
        [TestMethod]
        public void Issue2183TypeCheck()
        {

            var observationDef = new StructureDefinition()
            {
                Url = "http://fire.ly/fhir/Sd/observationQuantity",
                Name = "observationQuantity",
                Type = "Observation",
                Status = PublicationStatus.Draft,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                FhirVersion = FHIRVersion.N4_0_1,
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                BaseDefinition = "http://hl7.org/fhir/StructureDefinition/Observation",
                Abstract = false,
                Differential = new()
                {
                    Element = new()
                    {
                        new()
                        {
                            Path = "Observation.value[x]",
                            Type = new()
                            {
                                new()
                                {
                                    Code = "Quantity"
                                }
                            }
                        },
                        new()
                        {
                            Path = "Observation.value[x].code",
                            Fixed = new Code("mm[Hg]")
                        }
                    }
                }
            };

            var observation = new Observation
            {
                Meta = new()
                {
                    Profile = new[] { "http://fire.ly/fhir/Sd/observationQuantity" }
                },
                Status = ObservationStatus.Unknown,
                Code = new()
                {
                    Text = "test"
                },
                Value = new FhirString("foo")
            };

            var settings = new ValidationSettings()
            {
                ResourceResolver = new MultiResolver(new InMemoryProfileResolver(observationDef), ZipSource.CreateValidationSource()),
                GenerateSnapshot = true,
                GenerateSnapshotSettings = new()
                {
                    GenerateSnapshotForExternalProfiles = true,
                    ForceRegenerateSnapshots = true
                }
            };

            var validator = new Validator(settings);
            var outcome = validator.Validate(observation);
            outcome.Issue.Should().OnlyContain(i => i.Details.Coding.Any(c => c.Code == Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE.Code.ToString()));
        }

        [TestMethod]
        public async Tasks.Task Issue2260LogicalModelValidationEld20Test()
        {
            // Arrange
            var resolver = new MultiResolver(
                new DirectorySource(@"TestData\validation"),
                ZipSource.CreateValidationSource());

            var settings = ValidationSettings.CreateDefault();
            settings.ResourceResolver = resolver;
            settings.GenerateSnapshot = true;
            settings.ResolveExternalReferences = true;
            settings.TerminologyService = new LocalTerminologyService(resolver.AsAsync());
            settings.GenerateSnapshotSettings = new SnapshotGeneratorSettings
            {
                GenerateSnapshotForExternalProfiles = true,
                ForceRegenerateSnapshots = true,
                GenerateAnnotationsOnConstraints = false,
                GenerateElementIds = true,
                GenerateExtensionsOnConstraints = false,
            };

            var validator = new Validator(settings);

            var resource = await resolver.ResolveByCanonicalUriAsync("https://fhir.healthdata.be/StructureDefinition/LogicalModel/HdBe-AbilityToDressOneself");

            // Act
            var result = validator.Validate(resource).ToTypedElement();

            // Assert
            var issues = result.Select("issue");

            issues.Should().HaveCount(0);
        }
    }
}

