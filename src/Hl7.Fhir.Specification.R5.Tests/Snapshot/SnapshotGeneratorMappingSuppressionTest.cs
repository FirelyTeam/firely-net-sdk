/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class SnapshotGeneratorMappingSuppressionTest
    {
        private class ElementBaseAnnotation(ElementDefinition baseElemDef)
        {
            public ElementDefinition BaseElementDefinition { get; } = baseElemDef;
        }

        private const string MARKDOWN_COMMENT = "Systems are not required to have markdown support, so the text should be readable without markdown processing. The markdown syntax is GFM - see https://github.github.com/gfm/";

        [TestMethod]
        public async System.Threading.Tasks.Task CodeSystemCopyrightCommentIssueTest()
        {
            const string copyrightComment = "... Sometimes, the copyright differs between the code system and the codes that are included. The copyright statement should clearly differentiate between these when required.";

            await copyrightCommentIssueTest("CodeSystem", mergeAppendText(MARKDOWN_COMMENT, copyrightComment));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CapabilityStatementCopyrightCommentIssueTest()
        {
            const string copyrightComment = "...";

            await copyrightCommentIssueTest("CapabilityStatement", mergeAppendText(MARKDOWN_COMMENT, copyrightComment));
        }

        private static async System.Threading.Tasks.Task copyrightCommentIssueTest(string resource, string expectedComment)
        {
            var zipSource = ZipSource.CreateValidationSource();
            var resolver = new CachedResolver(zipSource);
            var settings = new SnapshotGeneratorSettings
            {
                ForceRegenerateSnapshots = true,
                GenerateAnnotationsOnConstraints = false,
                GenerateExtensionsOnConstraints = false,
                GenerateElementIds = true,
                GenerateSnapshotForExternalProfiles = true
            };
            var sd = new StructureDefinition
            {
                Type = resource,
                BaseDefinition = $"http://hl7.org/fhir/StructureDefinition/{resource}",
                Name = $"My{resource}",
                Url = $"http://example.org/fhir/StructureDefinition/My{resource}",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                FhirVersion = FHIRVersion.N5_0_0,
            };

            var generator = new SnapshotGenerator(resolver, settings);

            generator.PrepareElement += addElementBaseAnnotation;

            var elems = await generator.GenerateAsync(sd);

            generator.PrepareElement -= addElementBaseAnnotation;

            var element = elems.FirstOrDefault(e => e.ElementId == $"{resource}.copyright");

            element.Should().NotBeNull();
            element.Comment.Should().Be(expectedComment);

            var baseElement = element.Annotation<ElementBaseAnnotation>()?.BaseElementDefinition;

            baseElement.Should().NotBeNull();
            element.Comment.Should().Be(baseElement.Comment);
        }

        private static string mergeAppendText(string s1, string s2)
        {
            if (!s2.StartsWith("..."))
                return s2;

            return string.IsNullOrEmpty(s1) 
                ? s2[3..] 
                : s1 + "\r\n" + s2[3..];
        }

        private static void addElementBaseAnnotation(object sender, SnapshotElementEventArgs e)
        {
            var elem = e.Element;

            var ann = elem.Annotation<ElementBaseAnnotation>();

            if (ann != null)
                elem.RemoveAnnotations<ElementBaseAnnotation>();

            var baseDef = e.BaseElement;

            elem.AddAnnotation(new ElementBaseAnnotation(baseDef));
        }

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
                                            //Url = SnapshotGeneratorExtensions.ELEMENTDEFINITION_SUPPRESS_EXT,
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
        [TestMethod]
        public async System.Threading.Tasks.Task TestExampleInheritanceWithoutSuppression()
        {
            // Create a base profile with an example that already has snapshot
            var baseProfile = CreateBaseProfileWithExample();
            baseProfile.Snapshot = new StructureDefinition.SnapshotComponent
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient")
                    {
                        Example = new List<ElementDefinition.ExampleComponent>()
                        {
                            new ElementDefinition.ExampleComponent()
                            {
                                Label = "test-example",
                                Value = new FhirString("Example patient name")
                            }
                        }
                    }
                }
            };

            // Create a derived profile without suppress extension
            var derivedProfile = CreateDerivedProfileWithoutExampleSuppression();

            // Mock resolver to return base profile when requested
            var mockResolver = new InMemoryResourceResolver();
            mockResolver.Add(baseProfile);

            // Create snapshot generator
            var generator = new SnapshotGenerator(mockResolver, SnapshotGeneratorSettings.CreateDefault());

            // Generate snapshot for the derived profile  
            generator.Update(derivedProfile);

            // Assert that the derived profile inherited the example from the base
            Assert.IsNotNull(derivedProfile.Snapshot);
            var patientElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient");
            Assert.IsNotNull(patientElement);
            Assert.IsNotNull(patientElement.Example);
            Assert.AreEqual(1, patientElement.Example.Count);
            Assert.AreEqual("test-example", patientElement.Example[0].Label);
            Assert.AreEqual("Example patient name", (patientElement.Example[0].Value as FhirString)?.Value);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestExampleSuppressionExtension()
        {
            // Create a base profile with an example that already has snapshot
            var baseProfile = CreateBaseProfileWithExample();
            baseProfile.Snapshot = new StructureDefinition.SnapshotComponent
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient")
                    {
                        Example = new List<ElementDefinition.ExampleComponent>()
                        {
                            new ElementDefinition.ExampleComponent()
                            {
                                Label = "test-example",
                                Value = new FhirString("Example patient name")
                            }
                        }
                    }
                }
            };

            // Create a derived profile that suppresses the inherited example
            var derivedProfile = CreateDerivedProfileWithExampleSuppression();

            // Mock resolver to return base profile when requested
            var mockResolver = new InMemoryResourceResolver();
            mockResolver.Add(baseProfile);

            // Create snapshot generator
            var generator = new SnapshotGenerator(mockResolver, SnapshotGeneratorSettings.CreateDefault());

            // Generate snapshot for the derived profile
            generator.Update(derivedProfile);

            // Assert that the derived profile did NOT inherit the example (it was suppressed)
            Assert.IsNotNull(derivedProfile.Snapshot);
            var patientElement = derivedProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient");
            Assert.IsNotNull(patientElement);

            // The example should be absent because it was suppressed
            Assert.IsTrue(patientElement.Example == null || patientElement.Example.Count == 0);
        }

        private StructureDefinition CreateBaseProfileWithExample()
        {
            return new StructureDefinition()
            {
                Id = "base-patient-profile-with-example",
                Url = "http://example.org/fhir/StructureDefinition/base-patient-with-example",
                Name = "BasePatientProfileWithExample",
                Status = PublicationStatus.Active,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                Type = "Patient",
                BaseDefinition = "http://hl7.org/fhir/StructureDefinition/Patient",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Short = "Base patient profile with example",
                            Example = new List<ElementDefinition.ExampleComponent>()
                            {
                                new ElementDefinition.ExampleComponent()
                                {
                                    Label = "test-example",
                                    Value = new FhirString("Example patient name")
                                }
                            }
                        }
                    }
                }
            };
        }

        private StructureDefinition CreateDerivedProfileWithoutExampleSuppression()
        {
            return new StructureDefinition()
            {
                Id = "derived-patient-profile-no-example-suppression",
                Url = "http://example.org/fhir/StructureDefinition/derived-patient-no-example-suppression",
                Name = "DerivedPatientProfileNoExampleSuppression",
                Status = PublicationStatus.Active,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                Type = "Patient",
                BaseDefinition = "http://example.org/fhir/StructureDefinition/base-patient-with-example",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Short = "Derived patient profile without example suppression"
                        }
                    }
                }
            };
        }

        private StructureDefinition CreateDerivedProfileWithExampleSuppression()
        {
            return new StructureDefinition()
            {
                Id = "derived-patient-profile-with-example-suppression",
                Url = "http://example.org/fhir/StructureDefinition/derived-patient-with-example-suppression",
                Name = "DerivedPatientProfileWithExampleSuppression",
                Status = PublicationStatus.Active,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Abstract = false,
                Type = "Patient",
                BaseDefinition = "http://example.org/fhir/StructureDefinition/base-patient-with-example",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient")
                        {
                            Short = "Derived patient profile with example suppression",
                            Example = new List<ElementDefinition.ExampleComponent>()
                            {
                                new ElementDefinition.ExampleComponent()
                                {
                                    Label = "test-example",
                                    Value = new FhirString("Example patient name"),
                                    Extension = new List<Extension>()
                                    {
                                        new Extension()
                                        {
                                            //Url = SnapshotGeneratorExtensions.ELEMENTDEFINITION_SUPPRESS_EXT,
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