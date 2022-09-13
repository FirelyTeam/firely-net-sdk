using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks = System.Threading.Tasks;

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
                FhirVersion = "3.0.2",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                BaseDefinition = "http://hl7.org/fhir/StructureDefinition/Observation",
                Abstract = false,
                Differential = new()
                {
                    Element = new()
                    {
                        new()
                        {
                            Path = "Observation.valueQuantity",
                            SliceName = "valueQuantity",
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
                            Path = "Observation.valueQuantity.code",
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

            outcome.Success.Should().Be(false);
        }
    }
}

