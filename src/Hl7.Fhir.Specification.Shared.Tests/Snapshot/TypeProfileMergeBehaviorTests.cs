/* 
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Tests.Snapshot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    /// <summary>
    /// Tests documenting the type profile merge behavior discussed in FHIR-9791.
    /// See https://jira.hl7.org/browse/FHIR-9791 for background.
    /// </summary>
    [TestClass]
    public class TypeProfileMergeBehaviorTests
    {
        private IResourceResolver _testResolver;
        private SnapshotGenerator _generator;

        [TestInitialize]
        public void Setup()
        {
            _testResolver = new CachedResolver(
                new MultiResolver(
                    ZipSource.CreateValidationSource(),
                    new TestProfileArtifactSource()
                )
            );
        }

        /// <summary>
        /// Tests that TypeFirst strategy is the default and works correctly.
        /// TypeFirst means: Base profile → Type profile → Differential
        /// </summary>
        [TestMethod]
        public async Tasks.Task TestTypeFirstStrategy_IsDefault()
        {
            // Arrange
            var settings = SnapshotGeneratorSettings.CreateDefault();
            
            // Assert
            settings.TypeProfileMergeStrategy.Should().Be(TypeProfileMergeStrategy.TypeFirst,
                "TypeFirst should be the default to maintain backward compatibility");

            // Should not throw
            _generator = new SnapshotGenerator(_testResolver, settings);
            await Tasks.Task.CompletedTask;
        }

        /// <summary>
        /// Tests that BaseFirst strategy throws NotImplementedException as it's not yet implemented.
        /// BaseFirst would mean: Type profile → Base profile → Differential
        /// </summary>
        [TestMethod]
        public void TestBaseFirstStrategy_ThrowsNotImplemented()
        {
            // Arrange
            var settings = SnapshotGeneratorSettings.CreateDefault();
            settings.TypeProfileMergeStrategy = TypeProfileMergeStrategy.BaseFirst;

            // Act & Assert
            Action act = () => _generator = new SnapshotGenerator(_testResolver, settings);
            act.Should().Throw<NotImplementedException>()
                .WithMessage("*BaseFirst is not yet implemented*")
                .WithMessage("*FHIR-9791*");
        }

        /// <summary>
        /// Demonstrates the current TypeFirst merge behavior with a concrete example.
        /// 
        /// Scenario:
        /// - Base profile (Patient) defines Patient.address with definition "Base: address def"
        /// - Type profile (AddressNL) defines Address.line with definition "Type: line def"
        /// - Derived profile adds differential constraint with definition "Diff: address def override"
        /// 
        /// Current behavior (TypeFirst = Base → Type → Diff):
        /// Result: "Diff: address def override" (differential wins)
        /// </summary>
        [TestMethod]
        public async Tasks.Task DocumentCurrentMergeBehavior_TypeFirst()
        {
            // This test documents the current behavior
            // In the future, when BaseFirst is implemented, we can add a comparison test
            
            // Arrange: Create profiles demonstrating the merge scenario
            var typeProfile = CreateAddressTypeProfile();
            var baseProfile = CreatePatientWithAddressConstraint();
            var derivedProfile = CreateDerivedPatientProfile();

            var resolver = new InMemoryResourceResolver(typeProfile, baseProfile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            var settings = SnapshotGeneratorSettings.CreateDefault();
            settings.TypeProfileMergeStrategy = TypeProfileMergeStrategy.TypeFirst; // Explicit for clarity
            _generator = new SnapshotGenerator(multiResolver, settings);

            // Act
            await _generator.UpdateAsync(derivedProfile);

            // Assert
            derivedProfile.Snapshot.Should().NotBeNull();
            var addressElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.address");
            addressElement.Should().NotBeNull("address element should exist in snapshot");

            // Document current behavior: Differential constraints override all
            // (This is the expected behavior with TypeFirst strategy)
            if (!string.IsNullOrEmpty(derivedProfile.Differential.Element.FirstOrDefault(e => e.Path == "Patient.address")?.Definition))
            {
                addressElement.Definition.Should().Contain("Diff:",
                    "because differential constraints should override both base and type profile constraints in TypeFirst mode");
            }
        }

        /// <summary>
        /// This test documents what BaseFirst behavior WOULD be, once implemented.
        /// It is currently SKIPPED because BaseFirst is not implemented yet.
        /// </summary>
        [TestMethod]
        [Ignore("BaseFirst strategy not yet implemented - see FHIR-9791")]
        public async Tasks.Task DocumentExpectedMergeBehavior_BaseFirst_WhenImplemented()
        {
            // Arrange: Same profiles as TypeFirst test
            var typeProfile = CreateAddressTypeProfile();
            var baseProfile = CreatePatientWithAddressConstraint();
            var derivedProfile = CreateDerivedPatientProfile();

            var resolver = new InMemoryResourceResolver(typeProfile, baseProfile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            var settings = SnapshotGeneratorSettings.CreateDefault();
            settings.TypeProfileMergeStrategy = TypeProfileMergeStrategy.BaseFirst;
            _generator = new SnapshotGenerator(multiResolver, settings);

            // Act - This would throw NotImplementedException currently
            await _generator.UpdateAsync(derivedProfile);

            // Assert - Expected behavior once BaseFirst is implemented
            // With BaseFirst: Type → Base → Diff
            // Base profile constraints would override type profile constraints
            // But differential would still win overall
            derivedProfile.Snapshot.Should().NotBeNull();
            var addressElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.address");
            
            // In BaseFirst mode, if base profile has stronger constraints than type profile,
            // the base constraints would take precedence (before differential overrides)
            // TODO: Define exact expected behavior once requirements are clearer
        }

        #region Helper Methods to Create Test Profiles

        private StructureDefinition CreateAddressTypeProfile()
        {
            return new StructureDefinition
            {
                Url = "http://example.org/fhir/StructureDefinition/AddressNL",
                Name = "AddressNL",
                Status = PublicationStatus.Draft,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Abstract = false,
                Type = "Address",
                BaseDefinition = "http://hl7.org/fhir/StructureDefinition/Address",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>
                    {
                        new ElementDefinition
                        {
                            Path = "Address",
                            Definition = "Type: Dutch address constraints"
                        },
                        new ElementDefinition
                        {
                            Path = "Address.line",
                            Definition = "Type: address line with Dutch constraints"
                        }
                    }
                }
            };
        }

        private StructureDefinition CreatePatientWithAddressConstraint()
        {
            return new StructureDefinition
            {
                Url = "http://example.org/fhir/StructureDefinition/PatientWithAddress",
                Name = "PatientWithAddress",
                Status = PublicationStatus.Draft,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                Type = "Patient",
                BaseDefinition = "http://hl7.org/fhir/StructureDefinition/Patient",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>
                    {
                        new ElementDefinition
                        {
                            Path = "Patient",
                        },
                        new ElementDefinition
                        {
                            Path = "Patient.address",
                            Definition = "Base: Patient address from base profile"
                        }
                    }
                }
            };
        }

        private StructureDefinition CreateDerivedPatientProfile()
        {
            return new StructureDefinition
            {
                Url = "http://example.org/fhir/StructureDefinition/DerivedPatient",
                Name = "DerivedPatient",
                Status = PublicationStatus.Draft,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                Type = "Patient",
                BaseDefinition = "http://example.org/fhir/StructureDefinition/PatientWithAddress",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>
                    {
                        new ElementDefinition
                        {
                            Path = "Patient"
                        },
                        new ElementDefinition
                        {
                            Path = "Patient.address",
                            Type = new System.Collections.Generic.List<ElementDefinition.TypeRefComponent>
                            {
                                new ElementDefinition.TypeRefComponent
                                {
                                    Code = "Address",
                                    Profile = new System.Collections.Generic.List<string>
                                    {
                                        "http://example.org/fhir/StructureDefinition/AddressNL"
                                    }
                                }
                            },
                            Definition = "Diff: address with Dutch constraints from AddressNL"
                        }
                    }
                }
            };
        }

        #endregion
    }
}
