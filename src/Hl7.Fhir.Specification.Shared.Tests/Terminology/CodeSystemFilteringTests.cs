using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Shared.Tests.Terminology
{
    [TestClass]
    public class CodeSystemFilteringTests
    {
        [TestMethod]
        public async Tasks.Task TestHierarchicalIsAFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.HierarchicalCodeSystem();
            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsA, "A") }).ToList();


            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "A")
                                              .Which.Contains.Should().Contain(c => c.Code == "AA")
                                              .Which.Contains.Should().Contain(c => c.Code == "AAA");
            concepts.Should().NotContain(c => c.Code == "C");

            //VS has a single include, with a two filter, both filters should be true;
            filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsA, "A"), (FilterOperator.IsA, "B") }).ToList();

            concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            //No code, which are both A & B;
            concepts.Should().BeEmpty();
        }

        [TestMethod]
        public async Tasks.Task TestSubsumbedByIsAFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.SubsumedByCodeSystem();

            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsA, "A") }).ToList();
            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "A")
                                              .And.Contain(c => c.Code == "AA")
                                              .And.Contain(c => c.Code == "AAA")
                                              .And.Contain(c => c.Code == "AB")
                                              .And.NotContain(c => c.Code == "B");


            ///VS has a single include, with a two filter, both filters should be true;
            filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsA, "A"), (FilterOperator.IsA, "B") }).ToList();
            concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().OnlyContain(c => c.Code == "AB");
        }

        [TestMethod]
        public async Tasks.Task TestHierarchicalDescendantOfFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.HierarchicalCodeSystem();
            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.DescendentOf, "A") }).ToList();


            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "AA")
                                              .Which.Contains.Should().Contain(c => c.Code == "AAA");
            concepts.Should().NotContain(c => c.Code == "C");
            concepts.Should().NotContain(c => c.Code == "A");
        }

        [TestMethod]
        public async Tasks.Task TestSubsumbedByDescendantOfFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.SubsumedByCodeSystem();

            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.DescendentOf, "A") }).ToList();
            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "AA")
                                              .And.Contain(c => c.Code == "AAA")
                                              .And.Contain(c => c.Code == "AB")
                                              .And.NotContain(c => c.Code == "B");


            ///VS has a single include, with a two filter, both filters should be true;
            filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.DescendentOf, "A"), (FilterOperator.DescendentOf, "B") }).ToList();
            concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().OnlyContain(c => c.Code == "AB");
        }


        [TestMethod]
        public void FlattenConceptTest()
        {
            var codeSystem = TestTerminologyCreator.HierarchicalCodeSystem();

            var flattened = codeSystem.Concept.Flatten();

            flattened.Should().Contain(c => c.Code == "A")
                          .And.Contain(c => c.Code == "AA")
                          .And.Contain(c => c.Code == "AAA")
                          .And.Contain(c => c.Code == "AB")
                          .And.Contain(c => c.Code == "B")
                          .And.Contain(c => c.Code == "BA")
                          .And.Contain(c => c.Code == "BAA")
                          .And.Contain(c => c.Code == "BB")
                          .And.Contain(c => c.Code == "C")
                          .And.Contain(c => c.Code == "CA")
                          .And.Contain(c => c.Code == "CAA")
                          .And.Contain(c => c.Code == "CB");


            flattened.Should().NotContain(c => c.Concept.Any());
        }

        [TestMethod]
        public async Tasks.Task TestHierarchicalIsNotAFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.HierarchicalCodeSystem();
            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsNotA, "AA") }).ToList();


            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "A")
                                        .Which.Contains.Should().Contain(c => c.Code == "AB")
                                        .And.NotContain(c => c.Code == "AA");

            //VS has a single include, with a two filter, both filters should be true;
            filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsNotA, "A"), (FilterOperator.IsNotA, "B") }).ToList();

            concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            //No code, which are both A & B;
            concepts.Should().OnlyContain(c => c.Code == "C");
        }

        [TestMethod]
        public async Tasks.Task TestFlatIsNotAFilter()
        {
            var resolver = new InMemoryResourceResolver();
            var codeSystem = TestTerminologyCreator.GetCodeSystem("http://foo.bar/fhir/CodeSystem/example")
                                       .WithConcept("A")
                                       .WithConcept("B")
                                       .WithConcept("C");
            resolver.Add(codeSystem);

            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsNotA, "C") }).ToList();

            var result = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            result.Should().NotContain(c => c.Code == "C");
        }

        [TestMethod]
        public async Tasks.Task TestSubsumbedByIsNotAFilter()
        {
            var resolver = new InMemoryResourceResolver();

            var codeSystem = TestTerminologyCreator.SubsumedByCodeSystem();

            resolver.Add(codeSystem);

            //VS has a single include, with a single filter
            var filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsNotA, "A") }).ToList();
            var concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().Contain(c => c.Code == "B")
                                              .And.Contain(c => c.Code == "B")
                                              .And.Contain(c => c.Code == "BB")
                                              .And.Contain(c => c.Code == "C")
                                              .And.NotContain(c => c.Code == "A")
                                              .And.NotContain(c => c.Code == "AA")
                                              .And.NotContain(c => c.Code == "AB")
                                              .And.NotContain(c => c.Code == "AAA");

            ///VS has a single include, with a two filter, both filters should be true;
            filters = TestTerminologyCreator.CreateFilters(new() { (FilterOperator.IsNotA, "A"), (FilterOperator.IsNotA, "B") }).ToList();
            concepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem("http://foo.bar/fhir/CodeSystem/example", filters, new ValueSetExpanderSettings { ValueSetSource = resolver });

            concepts.Should().OnlyContain(c => c.Code == "C");
        }

    }

    internal static class TestTerminologyCreator
    {
        internal static CodeSystem HierarchicalCodeSystem()
        {
            return GetCodeSystem("http://foo.bar/fhir/CodeSystem/example")
                                       .WithConcept("A",
                                                   children: new() { CreateConcept("AA", children: new(){ CreateConcept("AAA") })
                                                            , CreateConcept("AB")})
                                       .WithConcept("B",
                                                   children: new() { CreateConcept("BA", children: new(){ CreateConcept("BAA") })
                                                            , CreateConcept("BB")})
                                       .WithConcept("C",
                                                   children: new() { CreateConcept("CA", children: new(){ CreateConcept("CAA") })
                                                            , CreateConcept("CB")});
        }

        internal static CodeSystem SubsumedByCodeSystem()
        {
            return GetCodeSystem("http://foo.bar/fhir/CodeSystem/example")
                                        .WithProperty("subsumedBy")
                                        .WithConcept("A")
                                        .WithConcept("AA", null, [("subsumedBy", "A")])
                                        .WithConcept("AAA", null, [("subsumedBy", "AA")])
                                        .WithConcept("B")
                                        .WithConcept("BB", null, [("subsumedBy", "B")])
                                        .WithConcept("AB", null, [("subsumedBy", "A"), ("subsumedBy", "B")])
                                        .WithConcept("C");
        }

        internal static IEnumerable<ValueSet.FilterComponent> CreateFilters(List<(FilterOperator op, string value)> filters)
        {
            return filters.Select(f => new ValueSet.FilterComponent { Op = f.op, Value = f.value });
        }

        internal static CodeSystem GetCodeSystem(string url)
        {
            return new CodeSystem { Url = url, Content = CodeSystemContentMode.Complete };

        }

        internal static CodeSystem WithProperty(this CodeSystem cs, string code)
        {
            cs.Property.Add(new CodeSystem.PropertyComponent { Code = code });
            return cs;
        }

        internal static CodeSystem WithConcept(this CodeSystem cs, string name, List<CodeSystem.ConceptDefinitionComponent> children = null, List<(string code, string value)> properties = null)
        {
            cs.Concept = cs.Concept.WithConcept(name, children, properties);
            return cs;
        }

        internal static List<CodeSystem.ConceptDefinitionComponent> WithConcept(this List<CodeSystem.ConceptDefinitionComponent> concepts, string name, List<CodeSystem.ConceptDefinitionComponent> children = null, List<(string code, string value)> properties = null)
        {
            concepts.Add(CreateConcept(name, children, properties));
            return concepts;
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