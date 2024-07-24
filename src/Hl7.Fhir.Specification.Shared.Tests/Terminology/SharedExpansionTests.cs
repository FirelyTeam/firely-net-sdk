using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    public class SharedExpansionTests
    {
        [Fact]
        public async Tasks.Task CanCallThroughLocalTerminologyService()
        {
            var vs1 = new ValueSet { Url = "http://source", Status = PublicationStatus.Active }
                .Includes(i => i.System("http://nu.nl").Concepts("1", "2", "3"));
            var vs2 = new ValueSet() { Url = "http://toexpand", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets("http://source"));

            var resolver = new InMemoryResourceResolver(vs1, vs2);
            var lts = new LocalTerminologyService(resolver);

            var p = new ExpandParameters().WithValueSet(url: vs1.Url);

            var vs = await lts.Expand(p) as ValueSet;
            vs.Expansion.Contains.Select(c => c.Code).Should().BeEquivalentTo("1", "2", "3");
        }

        [Fact]
        public async Tasks.Task CanCombineValueSets()
        {
            var vs1 = buildVs("http://valuesetA", "1", "2", "3").Includes(i => i.System("http://systemX").Concepts("A"));
            var vs2 = buildVs("http://valuesetB", "2", "3", "4");
            var vsCombined = new ValueSet() { Url = "http://combined", Status = PublicationStatus.Active }.Includes(i => i.ValueSets("http://valuesetA", "http://valuesetB"));

            var resolver = new InMemoryResourceResolver(vs1, vs2);
            var expander = new ValueSetExpander(new() { ValueSetSource = resolver });
            await expander.ExpandAsync(vsCombined);

            vsCombined.Expansion.Contains.Select(c => c.Code).Should().BeEquivalentTo(new[] { "2", "3" }, because: "specifying multiple valuesets should return an intersection.");
            vsCombined.Expansion.Contains.Should().AllSatisfy(c => c.System.Should().Be("http://system"));

            var vsFiltered = new ValueSet() { Url = "http://filtered", Status = PublicationStatus.Active }.Includes(i => i.ValueSets("http://valuesetA").System("http://systemX"));
            await expander.ExpandAsync(vsFiltered);
            vsFiltered.Expansion.Contains.Select(c => c.Code).Should().BeEquivalentTo(new[] { "A" }, because: "filtering on systemX only returns codes from systemX");
        }

        private static ValueSet buildVs(string canonical, params string[] codes)
        {
            var concepts = codes.Select(c => new ValueSet.ConceptReferenceComponent() { Code = c }).ToList();

            return new ValueSet()
            {
                Url = canonical,
                Version = "2022-08-01",
                Status = PublicationStatus.Active,
            }.Includes(i => i.System("http://system").Concepts(concepts));
        }

        [Fact]
        public async Tasks.Task CatchesCyclicExpansions()
        {
            var resolver = new InMemoryResourceResolver();

            var vs1 = new ValueSet()
            {
                Url = "http://nu.nl/refers-to-other",
                Version = "2022-08-01",
                Status = PublicationStatus.Active,
                Compose = new()
                {
                    Include = new() { new() { ValueSet = new[] { "http://nu.nl/other" } } }
                }
            };

            var vs2 = new ValueSet()
            {
                Url = "http://nu.nl/other",
                Version = "2022-08-01",
                Status = PublicationStatus.Active,
                Compose = new()
                {
                    Include = new() { new() { ValueSet = new[] { vs1.Url } } }
                }
            };

            resolver.Add(vs1, vs2);
            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = resolver });
            await Assert.ThrowsAsync<TerminologyServiceException>(() => expander.ExpandAsync(vs1));
        }

        [Fact]
        public async Tasks.Task TestExpandingVsWithUnknownSystem()
        {

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = new InMemoryResourceResolver() });
            var vs = new ValueSet
            {
                Compose = new()
                {
                    Include = new List<ValueSet.ConceptSetComponent>
                    {
                        new()
                        {
                            System = "http://www.unknown.org/"
                        }
                    }
                }
            };

            var job = async () => await expander.ExpandAsync(vs);
            await job.Should().ThrowAsync<ValueSetUnknownException>().WithMessage("The ValueSet expander cannot find codesystem 'http://www.unknown.org/', so the expansion cannot be completed.");
        }

        [Fact]
        public async Tasks.Task TestExcludingOnlyParent()
        {
            var resolver = new InMemoryResourceResolver();
            var vs = new ValueSet
            {
                Compose = new()
                {
                    Include = new List<ValueSet.ConceptSetComponent>
                    {
                        new()
                        {
                            System = "http://www.unknown.org/",
                            Filter = new List<ValueSet.FilterComponent>
                            {
                                new()
                                {
                                    Property = "concept",
                                    Op = FilterOperator.IsA,
                                    Value = "parent"
                                }
                            }
                        }
                    },
                    Exclude = new List<ValueSet.ConceptSetComponent>
                    {
                        new()
                        {
                            System = "http://www.unknown.org/",
                            Concept = new List<ValueSet.ConceptReferenceComponent>
                            {
                                new()
                                {
                                    Code = "parent"
                                }
                            }
                        }
                    }
                }
            };

            var cs = new CodeSystem
            {
                Url = "http://www.unknown.org/",
                Content = CodeSystemContentMode.Complete,
                Concept = new List<CodeSystem.ConceptDefinitionComponent>
                {
                    new()
                    {
                        Code = "parent",
                        Concept = new List<CodeSystem.ConceptDefinitionComponent>
                        {
                            new()
                            {
                                Code = "child1"
                            },
                            new()
                            {
                                Code = "child2"
                            }
                        }
                    }
                }
            };

            resolver.Add(cs, vs);

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = resolver });
            await expander.ExpandAsync(vs);
            vs.Expansion.Contains.Select(c => c.Code).Should().BeEquivalentTo(["child1", "child2"]);


            vs.Compose.Exclude = new List<ValueSet.ConceptSetComponent>
                    {
                        new()
                        {
                            System = "http://www.unknown.org/",
                            Concept = new List<ValueSet.ConceptReferenceComponent>
                            {
                                new()
                                {
                                    Code = "parent"
                                },
                                new()
                                {
                                    Code = "child1"
                                }
                            }
                        }
                    };

            resolver.Reload(cs, vs);
            await expander.ExpandAsync(vs);
            vs.Expansion.Contains.Select(c => c.Code).Should().BeEquivalentTo(["child2"]);

            vs.Compose.Exclude = new List<ValueSet.ConceptSetComponent>
                    {
                        new()
                        {
                            System = "http://www.unknown.org/",
                            Filter = new List<ValueSet.FilterComponent>
                            {
                                new()
                                {
                                    Property = "concept",
                                    Op = FilterOperator.IsA,
                                    Value = "parent"
                                }
                            }
                        }
                    };

            resolver.Reload(cs, vs);
            await expander.ExpandAsync(vs);
            vs.Expansion.Contains.Select(c => c.Code).Should().BeEmpty();

        }
    }
}