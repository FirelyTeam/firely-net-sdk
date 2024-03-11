using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Shared.Tests.Terminology
{
    [TestClass]
    public class CodeSystemFilteringTests
    {
        [TestMethod]
        public async T.Task ExpandFilterIsAWithHierarchicalCodeSystem()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.GetCodeSystem("http://foo.bar/fhir/CodeSystem/example")
                                        .WithConcept("A",
                                                    children: new() { TestTerminologyCreator.CreateConcept("AA", children: new(){TestTerminologyCreator.CreateConcept("AAA") })
                                                                    , TestTerminologyCreator.CreateConcept("AB")})
                                        .WithConcept("B",
                                                    children: new() { TestTerminologyCreator.CreateConcept("BA", children: new(){TestTerminologyCreator.CreateConcept("BAA") })
                                                                    , TestTerminologyCreator.CreateConcept("BB")})
                                        .WithConcept("C",
                                                    children: new() { TestTerminologyCreator.CreateConcept("CA", children: new(){TestTerminologyCreator.CreateConcept("CAA") })
                                                                    , TestTerminologyCreator.CreateConcept("CB")});

            resolver.Add(codeSystem);
            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = resolver });

            //VS has a single include, with a single filter
            var testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A") });

            await expander.ExpandAsync(testVs);

            testVs.Expansion.Contains.Should()
                .Contain(c => c.Code == "A")
                .Which.Contains.Should().Contain(c => c.Code == "AA")
                .Which.Contains.Should().Contain(c => c.Code == "AAA");

            testVs.Expansion.Contains.Should().NotContain(c => c.Code == "C");

            //VS has a single include, with a two filter, both filters should be true;
            testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A"), (FilterOperator.IsA, "B") });

            await expander.ExpandAsync(testVs);

            //No code, which are both A & B;
            testVs.Expansion.Contains.Should().BeEmpty();

            //VS has two includes, both with a filter;
            testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A") })
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "B") });


            await expander.ExpandAsync(testVs);

            testVs.Expansion.Contains.Should()
               .Contain(c => c.Code == "A")
               .Which.Contains.Should().Contain(c => c.Code == "AA")
               .Which.Contains.Should().Contain(c => c.Code == "AAA");

            testVs.Expansion.Contains.Should()
              .Contain(c => c.Code == "B")
              .Which.Contains.Should().Contain(c => c.Code == "BA")
              .Which.Contains.Should().Contain(c => c.Code == "BAA");

            testVs.Expansion.Contains.Should().NotContain(c => c.Code == "C");

            resolver.Clear();
        }

        [TestMethod]
        public async T.Task ExpandFilterIsAWithSubsumesHierarchieCodeSystem()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.GetCodeSystem("http://foo.bar/fhir/CodeSystem/example")
                                        .WithProperty("subsumedBy")
                                        .WithConcept("A")
                                        .WithConcept("AA", null, [("subsumedBy", "A")])
                                        .WithConcept("AAA", null, [("subsumedBy", "AA")])
                                        .WithConcept("B")
                                        .WithConcept("BB", null, [("subsumedBy", "B")])
                                        .WithConcept("AB", null, [("subsumedBy", "A"), ("subsumedBy", "B")])
                                        .WithConcept("C");

            resolver.Add(codeSystem);
            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = resolver });

            //VS has a single include, with a single filter
            var testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A") });

            await expander.ExpandAsync(testVs);

            testVs.Expansion.Contains.Should().Contain(c => c.Code == "A")
                                              .And.Contain(c => c.Code == "AA")
                                              .And.Contain(c => c.Code == "AAA")
                                              .And.Contain(c => c.Code == "AB")
                                              .And.NotContain(c => c.Code == "B");


            ///VS has a single include, with a two filter, both filters should be true;
            testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                               .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A"), (FilterOperator.IsA, "B") });

            await expander.ExpandAsync(testVs);

            testVs.Expansion.Contains.Should().OnlyContain(c => c.Code == "AB");

            //VS has two includes, both with a filter;
            testVs = TestTerminologyCreator.GetValueSet("http://foo.bar/fhir/ValueSet/example")
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "A") })
                                      .WithInclude("http://foo.bar/fhir/CodeSystem/example", filters: new() { (FilterOperator.IsA, "B") });


            await expander.ExpandAsync(testVs);

            testVs.Expansion.Contains.Should().Contain(c => c.Code == "A")
                                              .And.Contain(c => c.Code == "AA")
                                              .And.Contain(c => c.Code == "AAA")
                                              .And.Contain(c => c.Code == "AB")
                                              .And.Contain(c => c.Code == "B")
                                              .And.Contain(c => c.Code == "BB")
                                              .And.NotContain(c => c.Code == "C");
        }

    }






    internal static class TestTerminologyCreator
    {
        internal static ValueSet GetValueSet(string url)
        {
            return new ValueSet
            {
                Url = url,
            };
        }

        internal static ValueSet WithInclude(this ValueSet vs, string codeSystemUrl, List<(FilterOperator op, string value)> filters)
        {
            if (vs.Compose is null)
            {
                vs.Compose = new();
            }

            vs.Compose.Include.Add(
                new()
                {
                    System = codeSystemUrl,
                    Filter = CreateFilters(filters).ToList()
                }
                );
            return vs;
        }

        internal static IEnumerable<ValueSet.FilterComponent> CreateFilters(List<(FilterOperator op, string value)> filters)
        {
            return filters.Select(f => new ValueSet.FilterComponent { Op = f.op, Value = f.value });
        }

        internal static CodeSystem GetCodeSystem(string url)
        {
            return new CodeSystem { Url = url };

        }

        internal static CodeSystem WithProperty(this CodeSystem cs, string code)
        {
            cs.Property.Add(new CodeSystem.PropertyComponent { Code = code });
            return cs;
        }

        internal static CodeSystem WithConcept(this CodeSystem cs, string name, List<CodeSystem.ConceptDefinitionComponent> children = null, List<(string code, string value)> properties = null)
        {
            cs.Concept.Add(CreateConcept(name, children, properties));
            return cs;
        }

        internal static CodeSystem.ConceptDefinitionComponent CreateConcept(string name, List<CodeSystem.ConceptDefinitionComponent> children = null, List<(string code, string value)> properties = null)
        {
            var concept = new CodeSystem.ConceptDefinitionComponent
            {
                Code = name
            };
            children?.ForEach(c => concept.Concept.Add(c));

            if (properties is not null)
            {
                concept.Property = CreateCsProperties(properties).ToList();
            }

            return concept;
        }

        internal static IEnumerable<CodeSystem.ConceptPropertyComponent> CreateCsProperties(List<(string code, string value)> properties = null)
        {
            if (properties == null)
                return null;
            return properties.Select(p => new CodeSystem.ConceptPropertyComponent { Code = p.code, Value = new Code(p.value) });
        }
    }


}


