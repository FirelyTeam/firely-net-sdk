/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class SnapshotGeneratorMappingSuppressionTest
    {

        [TestMethod]
        public async System.Threading.Tasks.Task TestMappingInheritanceWithoutSuppression()
        {
            // Create a base profile with a mapping that already has snapshot
            var baseProfile = CreateBaseProfileWithMapping();
            baseProfile.Snapshot = new StructureDefinition.SnapshotComponent
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient")
                    {
                        Mapping = new List<ElementDefinition.MappingComponent>()
                        {
                            new ElementDefinition.MappingComponent()
                            {
                                Identity = "test-identity",
                                Map = "TestMapping.Patient"
                            }
                        }
                    }
                }
            };
            
            // Create a derived profile without suppress extension
            var derivedProfile = CreateDerivedProfileWithoutSuppression();
            
            // Mock resolver to return base profile when requested
            var mockResolver = new InMemoryResourceResolver();
            mockResolver.Add(baseProfile);
            
            // Generate snapshot
            var generator = new SnapshotGenerator(mockResolver, new SnapshotGeneratorSettings());
            await generator.UpdateAsync(derivedProfile);

            // Verify that mapping is inherited
            var rootElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient");
            Assert.IsNotNull(rootElement, "Should have Patient root element");
            Assert.IsNotNull(rootElement.Mapping, "Mapping should be inherited from base profile");
            Assert.AreEqual(1, rootElement.Mapping.Count, "Should have inherited one mapping");
            Assert.AreEqual("test-identity", rootElement.Mapping[0].Identity, "Should have inherited the correct mapping");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestMappingSuppressionWithExtension()
        {
            // Create a base profile with a mapping that already has snapshot
            var baseProfile = CreateBaseProfileWithMapping();
            baseProfile.Snapshot = new StructureDefinition.SnapshotComponent
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient")
                    {
                        Mapping = new List<ElementDefinition.MappingComponent>()
                        {
                            new ElementDefinition.MappingComponent()
                            {
                                Identity = "test-identity",
                                Map = "TestMapping.Patient"
                            }
                        }
                    }
                }
            };
            
            // Create a derived profile with suppress extension on mapping
            var derivedProfile = CreateDerivedProfileWithSuppressedMapping();
            
            // Mock resolver to return base profile when requested
            var mockResolver = new InMemoryResourceResolver();
            mockResolver.Add(baseProfile);
            
            // Generate snapshot
            var generator = new SnapshotGenerator(mockResolver, new SnapshotGeneratorSettings());
            await generator.UpdateAsync(derivedProfile);

            // Verify that mapping is NOT inherited due to suppression
            var rootElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient");
            Assert.IsNotNull(rootElement, "Should have Patient root element");
            var inheritedMapping = rootElement.Mapping?.FirstOrDefault(m => m.Identity == "test-identity");
            Assert.IsNull(inheritedMapping, "Mapping with suppress extension should not be inherited");
        }

        private StructureDefinition CreateBaseProfileWithMapping()
        {
            return new StructureDefinition()
            {
                Type = "Patient",
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType("Patient"),
                Name = "BasePatientWithMapping",
                Url = @"http://example.org/fhir/StructureDefinition/BasePatientWithMapping",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Mapping = new List<ElementDefinition.MappingComponent>()
                            {
                                new ElementDefinition.MappingComponent()
                                {
                                    Identity = "test-identity",
                                    Map = "TestMapping.Patient"
                                }
                            }
                        }
                    }
                }
            };
        }

        private StructureDefinition CreateDerivedProfileWithoutSuppression()
        {
            return new StructureDefinition()
            {
                Type = "Patient",
                BaseDefinition = @"http://example.org/fhir/StructureDefinition/BasePatientWithMapping",
                Name = "DerivedPatientWithoutSuppression",
                Url = @"http://example.org/fhir/StructureDefinition/DerivedPatientWithoutSuppression",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Short = "Derived patient profile"
                        }
                    }
                }
            };
        }

        private StructureDefinition CreateDerivedProfileWithSuppressedMapping()
        {
            return new StructureDefinition()
            {
                Type = "Patient",
                BaseDefinition = @"http://example.org/fhir/StructureDefinition/BasePatientWithMapping",
                Name = "DerivedPatientWithSuppressedMapping",
                Url = @"http://example.org/fhir/StructureDefinition/DerivedPatientWithSuppressedMapping",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Short = "Derived patient profile",
                            Mapping = new List<ElementDefinition.MappingComponent>()
                            {
                                new ElementDefinition.MappingComponent()
                                {
                                    Identity = "test-identity",
                                    Map = "TestMapping.Patient",
                                    Extension = new List<Extension>()
                                    {
                                        new Extension()
                                        {
                                            Url = SnapshotGeneratorExtensions.ELEMENTDEFINITION_SUPPRESS_EXT,
                                            Value = new FhirBoolean(true)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}