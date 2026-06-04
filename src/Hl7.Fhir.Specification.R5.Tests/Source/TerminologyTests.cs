using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using System.Collections.Generic;
using Xunit;
using Tasks=System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    public partial class TerminologyTests
    {
        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async Tasks.Task ExternalServiceClosureExample()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            // Step 1
            var parametersStep1 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52");

            var resultStep1 = await svc.Closure(parametersStep1) as ConceptMap;

            Assert.NotNull(resultStep1);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep1.Name);
            Assert.Equal("1", resultStep1.Version);

            // Step 2
            var conceptStep2 = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "22298006",
            };
            var parametersStep2 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep2 });

            var resultStep2 = await svc.Closure(parametersStep2) as ConceptMap;

            Assert.NotNull(resultStep2);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep2.Name);
            Assert.Equal("2", resultStep2.Version);

            // Step 3
            var conceptStep3 = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "128599005",
            };
            var parametersStep3 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep3 });

            var resultStep3 = await svc.Closure(parametersStep3) as ConceptMap;

            Assert.NotNull(resultStep3);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep3.Name);
            Assert.Equal("3", resultStep3.Version);
            Assert.Collection(resultStep3.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMap.ConceptMapRelationship.Equivalent, target.Relationship);
                                });
                        });
                });
            /* 
            // Step 4
            var conceptStep4A = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "301095005",
            };
            var conceptStep4B = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "298705000",
            };
            var conceptStep4C = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "282729004",
            };
            var parametersStep4 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep4A, conceptStep4B, conceptStep4C });

            var resultStep4 = await svc.Closure(parametersStep4) as ConceptMap;

            Assert.NotNull(resultStep4);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep4.Name);
            Assert.Equal("4", resultStep4.Version);
            Assert.Collection(resultStep4.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("128599005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("301095005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("282729004", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        });
                });

            var parametersStep5 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithVersion("0");

            var resultStep5 = await svc.Closure(parametersStep5) as ConceptMap;

            Assert.NotNull(resultStep5);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep5.Name);
            Assert.Equal("4", resultStep5.Version);
            Assert.Collection(resultStep5.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("298705000", element.Code);
                        },
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("128599005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("301095005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("282729004", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        });
                });
            */
        }

    }
}