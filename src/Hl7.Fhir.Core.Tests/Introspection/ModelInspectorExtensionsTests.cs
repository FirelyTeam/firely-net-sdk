using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Introspection.Tests
{
    [TestClass()]
    public class ModelInspectorExtensionsTests
    {
        private static readonly ModelInspector COMMON_INSPECTOR = ModelInspector.ForAssembly(typeof(IModelInfo).Assembly);
        private static readonly ModelInspector MIDDLE_INSPECTOR = ModelInspector.ForAssembly(typeof(Bundle).Assembly);
        private static readonly ModelInspector STATELLITE_INSPECTOR = ModelInspector.ForAssembly(typeof(Patient).Assembly);

        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void ForInstanceTest(Base instance, ModelInspector expectedResult)
        {
            ModelInspectorExtensions.ForInstance(instance).Should().Be(expectedResult);
        }

        private static IEnumerable<object[]> getTestData()
        {
            yield return new object[] { new Patient(), STATELLITE_INSPECTOR };
            yield return new object[] { new Bundle(), MIDDLE_INSPECTOR };
            yield return new object[] { new FhirString(), COMMON_INSPECTOR };

            yield return new object[] { new Bundle()  {
                                                Entry = new List<Bundle.EntryComponent> {
                                                    new Bundle.EntryComponent {
                                                        Resource = new Patient()
                                                    }
                                                }
                                        }, STATELLITE_INSPECTOR };

            yield return new object[] { new Bundle()  {
                                                Entry = new List<Bundle.EntryComponent> {
                                                    new Bundle.EntryComponent {
                                                        Resource = new OperationOutcome() {
                                                            Contained = new List<Resource> { new Patient() }
                                                        }
                                                    }
                                                }
                                        }, STATELLITE_INSPECTOR };

            yield return new object[] { new Bundle()  {
                                                Entry = new List<Bundle.EntryComponent> {
                                                    new Bundle.EntryComponent {
                                                        Resource = new OperationOutcome() {
                                                            Contained = new List<Resource> { new OperationOutcome() }
                                                        },
                                                        Response = new Bundle.ResponseComponent
                                                        {
                                                            Outcome = new Patient()
                                                        }
                                                    }
                                                }
                                        }, STATELLITE_INSPECTOR };

            yield return new object[] {  new OperationOutcome() {
                                             Contained = new List<Resource> { new StructureDefinition() }
                                        }, MIDDLE_INSPECTOR };

            yield return new object[] {  new Patient() {
                                             Contained = new List<Resource> { new StructureDefinition() }
                                        }, STATELLITE_INSPECTOR };


            /* TODO BIG_COMMON
             * Open types should be moved to Common. Then we have these problems anymore
             */
            /*
            yield return new object[] { new Extension() {
                                                Value = new TriggerDefinition()
                                            }, _statelliteInspector };

            yield return new object[] { new ElementDefinition() {
                                                Fixed = new TriggerDefinition()
                                            }, _statelliteInspector };

            yield return new object[] { new StructureDefinition() {
                                                Snapshot = new StructureDefinition.SnapshotComponent
                                                {
                                                    Element = new List<ElementDefinition>
                                                    {
                                                        new ElementDefinition
                                                        {
                                                            DefaultValue = new TriggerDefinition()
                                                        }
                                                    }
                                                }
                                            }, _statelliteInspector };
        */
        }

        [TestMethod]
        public void CheckResourceChoiceClass()
        {
            // Find all attributes of the ModelInspector that have the type Resource
            var attributes = (from classMapping in ModelInfo.ModelInspector.ClassMappings
                              from propMapping in classMapping.PropertyMappings
                              where propMapping.Name != "contained" && propMapping.Choice == ChoiceType.ResourceChoice
                              select classMapping.Name + "." + propMapping.Name).ToList();

            attributes.Should().BeEquivalentTo(new[]
            {
                "Bundle#Entry.resource",
                "Bundle#Response.outcome",
                "Parameters#Parameter.resource"
            });
        }
    }
}