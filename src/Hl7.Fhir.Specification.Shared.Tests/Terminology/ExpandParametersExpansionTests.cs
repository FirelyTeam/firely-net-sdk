using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Specification.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Shared.Tests.Terminology
{
    /// <summary>
    /// Tests for <see cref="ValueSetExpander.ExpandAsync(ValueSet, ExpandParameters)"/>: supported parameters, the echo,
    /// flat expansions (excludeNested, <see cref="ValueSetExpanderSettings.AlwaysExpandFlat"/>) and excludeNotForUI.
    /// </summary>
    [TestClass]
    public class ExpandParametersExpansionTests
    {
        private const string SIMPLE = "http://example.org/fhir/CodeSystem/simple";

        // code1
        // code2 (not selectable)
        //   code2a
        //     code2aI
        //     code2aII
        //   code2b
        // code3
        private static readonly string[] DEPTH_FIRST = ["code1", "code2", "code2a", "code2aI", "code2aII", "code2b", "code3"];

        private static CodeSystem simpleCodeSystem()
        {
            var cs = new CodeSystem { Url = SIMPLE, Version = "1.0.0", Content = CodeSystemContentMode.Complete };
            cs.Property.Add(new CodeSystem.PropertyComponent
            {
                Code = "notSelectable",
                Uri = CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE,
                Type = CodeSystem.PropertyType.Boolean
            });

            var code2 = concept("code2", concept("code2a", concept("code2aI"), concept("code2aII")), concept("code2b"));
            code2.Property.Add(new CodeSystem.ConceptPropertyComponent { Code = "notSelectable", Value = new FhirBoolean(true) });

            cs.Concept.Add(concept("code1"));
            cs.Concept.Add(code2);
            cs.Concept.Add(concept("code3"));
            return cs;

            static CodeSystem.ConceptDefinitionComponent concept(string code, params CodeSystem.ConceptDefinitionComponent[] children)
            {
                var c = new CodeSystem.ConceptDefinitionComponent { Code = code, Display = "Display " + code };
                c.Concept.AddRange(children);
                return c;
            }
        }

        private static ValueSet wholeSystem(string url = "http://example.org/fhir/ValueSet/simple-all") =>
            new ValueSet { Url = url, Status = PublicationStatus.Active }.Includes(i => i.System(SIMPLE));

        private static ValueSet isA(string code) =>
            new ValueSet { Url = "http://example.org/fhir/ValueSet/simple-filter-isa", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Filter.Add(new ValueSet.FilterComponent { Property = "concept", Op = FilterOperator.IsA, Value = code }));

        private static ValueSetExpander expander(InMemoryResourceResolver resolver, bool alwaysFlat = false, int maxSize = ValueSetExpanderSettings.DefaultMaxExpansionSize) =>
            new(new ValueSetExpanderSettings { ValueSetSource = resolver, AlwaysExpandFlat = alwaysFlat, MaxExpansionSize = maxSize });

        private static IEnumerable<string> codes(IEnumerable<ValueSet.ContainsComponent> contains) => contains.Select(c => c.Code);

        private static IEnumerable<string> allCodes(IEnumerable<ValueSet.ContainsComponent> contains) =>
            ValueSetExpander.FlattenContains(contains).Select(c => c.Code);

        [TestMethod]
        public async Tasks.Task WithoutParametersTheExpansionStaysNested()
        {
            var vs = wholeSystem();
            await expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(vs);

            codes(vs.Expansion.Contains).Should().Equal("code1", "code2", "code3");
            vs.Expansion.Contains.Single(c => c.Code == "code2").Abstract.Should().BeTrue();
            vs.Expansion.Total.Should().Be(7);
            vs.Expansion.Parameter.Should().BeEmpty();
        }

        [TestMethod]
        public async Tasks.Task ExcludeNestedReturnsAFlatDepthFirstExpansion()
        {
            var vs = wholeSystem();
            await expander(new InMemoryResourceResolver(simpleCodeSystem()))
                .ExpandAsync(vs, new ExpandParameters { ExcludeNested = new FhirBoolean(true) });

            codes(vs.Expansion.Contains).Should().Equal(DEPTH_FIRST);
            vs.Expansion.Contains.Should().AllSatisfy(c => c.Contains.Should().BeEmpty());
            vs.Expansion.Total.Should().Be(7);
            vs.Expansion.Parameter.Should().ContainSingle()
                .Which.Should().Match<ValueSet.ParameterComponent>(p => p.Name == "excludeNested" && ((FhirBoolean)p.Value).Value == true);
        }

        [TestMethod]
        public async Tasks.Task ExcludeNestedAppliesToFilters()
        {
            var vs = isA("code2");
            await expander(new InMemoryResourceResolver(simpleCodeSystem()))
                .ExpandAsync(vs, new ExpandParameters { ExcludeNested = new FhirBoolean(true) });

            codes(vs.Expansion.Contains).Should().Equal("code2", "code2a", "code2aI", "code2aII", "code2b");
            vs.Expansion.Total.Should().Be(5);
        }

        [TestMethod]
        public async Tasks.Task AlwaysExpandFlatIsAPolicyThatDoesNotChangeTheEcho()
        {
            var resolver = new InMemoryResourceResolver(simpleCodeSystem());

            var vs = wholeSystem();
            await expander(resolver, alwaysFlat: true).ExpandAsync(vs, new ExpandParameters { ExcludeNested = new FhirBoolean(false) });
            codes(vs.Expansion.Contains).Should().Equal(DEPTH_FIRST);
            vs.Expansion.Parameter.Should().ContainSingle()
                .Which.Should().Match<ValueSet.ParameterComponent>(p => p.Name == "excludeNested" && ((FhirBoolean)p.Value).Value == false);

            var vs2 = wholeSystem();
            await expander(resolver, alwaysFlat: true).ExpandAsync(vs2);
            codes(vs2.Expansion.Contains).Should().Equal(DEPTH_FIRST);
            vs2.Expansion.Parameter.Should().BeEmpty();
        }

        [TestMethod]
        public void SettingsCopyAlwaysExpandFlat()
        {
            new ValueSetExpanderSettings { AlwaysExpandFlat = true }.Clone().AlwaysExpandFlat.Should().BeTrue();
        }

        [TestMethod]
        public async Tasks.Task UnsupportedParametersAreRejected()
        {
            var parameters = new ExpandParameters { ExcludeNested = new FhirBoolean(true), ActiveOnly = new FhirBoolean(true) };
            parameters.Add("property", new FhirString("definition"));

            var act = () => expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(wholeSystem(), parameters);

            var exception = (await act.Should().ThrowAsync<FhirOperationException>()).Which;
            exception.Message.Should().Contain("activeOnly").And.Contain("property").And.NotContain("excludeNested");
            exception.Outcome.Issue.Should().ContainSingle().Which.Code.Should().Be(OperationOutcome.IssueType.NotSupported);
        }

        [TestMethod]
        public async Tasks.Task OnlyAppliedParametersAreEchoedInRequestOrder()
        {
            var parameters = new ExpandParameters();
            parameters.Add("url", new FhirUri("http://example.org/fhir/ValueSet/simple-all"));
            parameters.Add("excludeNotForUI", new FhirBoolean(false));
            parameters.Add("valueSetVersion", new FhirString("1"));
            parameters.Add("excludeNested", new FhirBoolean(true));

            var vs = wholeSystem();
            await expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(vs, parameters);

            vs.Expansion.Parameter.Select(p => p.Name).Should().Equal("excludeNotForUI", "excludeNested");
        }

        [TestMethod]
        public async Tasks.Task IncludeDesignationsFromTheRequestWinsOverTheSetting()
        {
            var cs = simpleCodeSystem();
            cs.Concept[0].Designation.Add(new CodeSystem.DesignationComponent { Language = "nl", Value = "Code een" });
            var resolver = new InMemoryResourceResolver(cs);
            var settings = new ValueSetExpanderSettings { ValueSetSource = resolver, IncludeDesignations = true };

            var vs = wholeSystem();
            await new ValueSetExpander(settings).ExpandAsync(vs, new ExpandParameters { IncludeDesignations = new FhirBoolean(false) });
            vs.Expansion.Contains[0].Designation.Should().BeEmpty();

            var vs2 = wholeSystem();
            await new ValueSetExpander(settings).ExpandAsync(vs2, new ExpandParameters());
            vs2.Expansion.Contains[0].Designation.Should().ContainSingle();
            vs2.Expansion.Parameter.Should().BeEmpty("the request did not send includeDesignations");

            var vs3 = wholeSystem();
            await new ValueSetExpander(settings).ExpandAsync(vs3);
            vs3.Expansion.Parameter.Should().ContainSingle().Which.Name.Should().Be("includeDesignations", "the old overload echoes the setting, as before");
        }

        [TestMethod]
        public async Tasks.Task TheSizeCapCountsNestedCodes()
        {
            var resolver = new InMemoryResourceResolver(simpleCodeSystem());

            var nested = () => expander(resolver, maxSize: 6).ExpandAsync(wholeSystem());
            await nested.Should().ThrowAsync<ValueSetExpansionTooBigException>();

            var flat = () => expander(resolver, maxSize: 6).ExpandAsync(wholeSystem(), new ExpandParameters { ExcludeNested = new FhirBoolean(true) });
            await flat.Should().ThrowAsync<ValueSetExpansionTooBigException>();

            var fits = wholeSystem();
            await expander(resolver, maxSize: 7).ExpandAsync(fits);
            fits.Expansion.Total.Should().Be(7);
        }

        [TestMethod]
        public async Tasks.Task IntersectingValueSetsKeepsNestedCodes()
        {
            var all = wholeSystem();
            var some = new ValueSet { Url = "http://example.org/fhir/ValueSet/some", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code2aI", "code3"));
            var both = new ValueSet { Url = "http://example.org/fhir/ValueSet/both", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(all.Url, some.Url));

            await expander(new InMemoryResourceResolver(simpleCodeSystem(), all, some)).ExpandAsync(both);

            codes(both.Expansion.Contains).Should().BeEquivalentTo("code2aI", "code3");
            both.Expansion.Total.Should().Be(2);
        }

        [TestMethod]
        public async Tasks.Task ExcludeNotForUILeavesOutNotSelectableCodesAndKeepsTheirChildren()
        {
            var resolver = new InMemoryResourceResolver(simpleCodeSystem());

            var nested = wholeSystem();
            await expander(resolver).ExpandAsync(nested, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
            codes(nested.Expansion.Contains).Should().Equal("code1", "code2a", "code2b", "code3");
            codes(nested.Expansion.Contains.Single(c => c.Code == "code2a").Contains).Should().Equal("code2aI", "code2aII");
            nested.Expansion.Total.Should().Be(6);

            var flat = wholeSystem();
            await expander(resolver).ExpandAsync(flat, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true), ExcludeNested = new FhirBoolean(true) });
            codes(flat.Expansion.Contains).Should().Equal("code1", "code2a", "code2aI", "code2aII", "code2b", "code3");

            var filtered = isA("code2");
            await expander(resolver).ExpandAsync(filtered, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
            allCodes(filtered.Expansion.Contains).Should().Equal("code2a", "code2aI", "code2aII", "code2b");
        }

        [TestMethod]
        public async Tasks.Task ExcludeNotForUIChecksEnumeratedCodesAgainstTheCodeSystem()
        {
            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/simple-enumerated", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code1", "code2", "code2a"));

            await expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(vs, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
            codes(vs.Expansion.Contains).Should().Equal("code1", "code2a");

            var unknownSystem = new ValueSet { Url = "http://example.org/fhir/ValueSet/unknown", Status = PublicationStatus.Active }
                .Includes(i => i.System("http://example.org/unknown").Concepts("x", "y"));
            await expander(new InMemoryResourceResolver()).ExpandAsync(unknownSystem, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
            codes(unknownSystem.Expansion.Contains).Should().Equal("x", "y");
        }

        [TestMethod]
        public async Tasks.Task ExcludeNotForUIAppliesToImportedValueSetsWithoutChangingThem()
        {
            var all = wholeSystem();
            var importer = new ValueSet { Url = "http://example.org/fhir/ValueSet/importer", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(all.Url));
            var resolver = new InMemoryResourceResolver(simpleCodeSystem(), all, importer);

            await expander(resolver).ExpandAsync(importer, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });

            codes(importer.Expansion.Contains).Should().Equal("code1", "code2a", "code2b", "code3");
            importer.Expansion.Total.Should().Be(6);
            allCodes(all.Expansion.Contains).Should().Contain("code2", "the imported value set keeps its own, unfiltered expansion");
        }

        [TestMethod]
        public async Tasks.Task ExcludedCodesAreRemovedFromAFlatExpansion()
        {
            var vs = wholeSystem().Excludes(e => e.System(SIMPLE).Concepts("code2aI", "code2b"));

            await expander(new InMemoryResourceResolver(simpleCodeSystem()))
                .ExpandAsync(vs, new ExpandParameters { ExcludeNested = new FhirBoolean(true) });

            codes(vs.Expansion.Contains).Should().Equal("code1", "code2", "code2a", "code2aII", "code3");
            vs.Expansion.Total.Should().Be(5);
        }

        [TestMethod]
        public async Tasks.Task ExcludedNotSelectableCodesAreStillRemoved()
        {
            // The stored expansion does not mark code2 as abstract, so the include keeps it; the exclude must still remove it.
            var stored = new ValueSet { Url = "http://example.org/fhir/ValueSet/stored", Status = PublicationStatus.Active, Expansion = new() };
            stored.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            stored.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2" });
            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/importer", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(stored.Url))
                .Excludes(e => e.System(SIMPLE).Concepts("code2"));

            await expander(new InMemoryResourceResolver(simpleCodeSystem(), stored))
                .ExpandAsync(vs, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });

            codes(vs.Expansion.Contains).Should().Equal("code1");
        }

        [TestMethod]
        public async Tasks.Task AnEmptySideOfAnIntersectionGivesAnEmptyResult()
        {
            var all = wholeSystem();
            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/both-sides", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code2").ValueSets(all.Url));
            var resolver = new InMemoryResourceResolver(simpleCodeSystem(), all);

            await expander(resolver).ExpandAsync(vs);
            codes(vs.Expansion.Contains).Should().Equal("code2");

            var notForUI = new ValueSet { Url = vs.Url, Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code2").ValueSets(all.Url));
            await expander(resolver).ExpandAsync(notForUI, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
            notForUI.Expansion.Contains.Should().BeEmpty("code2 is left out on the system side, so the intersection is empty");
        }

        [TestMethod]
        public async Tasks.Task WrongTypedAndDuplicateParametersAreNotApplied()
        {
            var wrongType = new ExpandParameters();
            wrongType.Add("excludeNested", new FhirString("true"));
            var vs = wholeSystem();
            await expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(vs, wrongType);
            codes(vs.Expansion.Contains).Should().Equal("code1", "code2", "code3");
            vs.Expansion.Parameter.Should().BeEmpty();

            var duplicate = new ExpandParameters();
            duplicate.Add("excludeNested", new FhirBoolean(true));
            duplicate.Add("excludeNested", new FhirBoolean(false));
            var act = () => expander(new InMemoryResourceResolver(simpleCodeSystem())).ExpandAsync(wholeSystem(), duplicate);
            await act.Should().ThrowAsync<FhirOperationException>();
        }

        [TestMethod]
        public void SystemVersionParametersUseTheOperationDefinitionNames()
        {
            var parameters = new ExpandParameters
            {
                SystemVersion = [new Canonical("http://loinc.org|2.56")],
                CheckSystemVersion = [new Canonical("http://loinc.org|2.56")],
                ForceSystemVersion = [new Canonical("http://loinc.org|2.56")]
            };

            parameters.Parameter.Select(p => p.Name).Should().Equal("system-version", "check-system-version", "force-system-version");
        }

        [TestMethod]
        public async Tasks.Task LocalTerminologyServiceIgnoresUnsupportedParametersAndDoesNotStoreTheShape()
        {
            var all = wholeSystem();
            var resolver = new InMemoryResourceResolver(simpleCodeSystem(), all);
            var service = new LocalTerminologyService(resolver);

            var parameters = new ExpandParameters().WithValueSet(url: all.Url);
            parameters.ExcludeNested = new FhirBoolean(true);
            parameters.ActiveOnly = new FhirBoolean(true);

            var flat = (ValueSet)await service.Expand(parameters);
            codes(flat.Expansion.Contains).Should().Equal(DEPTH_FIRST);
            flat.Expansion.Parameter.Select(p => p.Name).Should().Equal("excludeNested");

            var nested = (ValueSet)await service.Expand(new ExpandParameters().WithValueSet(url: all.Url));
            codes(nested.Expansion.Contains).Should().Equal("code1", "code2", "code3");

            // Now the resolved value set has a stored (nested) expansion: a flat request reshapes a copy of it.
            var flatAgain = (ValueSet)await service.Expand(parameters);
            codes(flatAgain.Expansion.Contains).Should().Equal(DEPTH_FIRST);
            codes(all.Expansion.Contains).Should().Equal("code1", "code2", "code3");
        }

        [TestMethod]
        public async Tasks.Task ExcludeNotForUIDoesNotDependOnAnEarlierDefaultExpansion()
        {
            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/simple-enumerated", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code1", "code2"));
            var service = new LocalTerminologyService(new InMemoryResourceResolver(simpleCodeSystem(), vs));

            // A default expansion is stored on the resolved value set; enumerated codes are not marked abstract there.
            var plain = (ValueSet)await service.Expand(new ExpandParameters().WithValueSet(url: vs.Url));
            codes(plain.Expansion.Contains).Should().Equal("code1", "code2");

            var parameters = new ExpandParameters().WithValueSet(url: vs.Url);
            parameters.ExcludeNotForUI = new FhirBoolean(true);
            var result = (ValueSet)await service.Expand(parameters);
            codes(result.Expansion.Contains).Should().Equal("code1");
        }

        [TestMethod]
        public async Tasks.Task AStoredExpansionWithoutComposeGetsTheRequestedShape()
        {
            var stored = new ValueSet { Url = "http://example.org/fhir/ValueSet/stored-only", Status = PublicationStatus.Active, Expansion = new() { Total = 4 } };
            var parent = new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2", Abstract = true };
            parent.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2a" });
            parent.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2b" });
            stored.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            stored.Expansion.Contains.Add(parent);

            var service = new LocalTerminologyService(new InMemoryResourceResolver(stored));
            var parameters = new ExpandParameters().WithValueSet(url: stored.Url);
            parameters.ExcludeNested = new FhirBoolean(true);
            parameters.ExcludeNotForUI = new FhirBoolean(true);

            var result = (ValueSet)await service.Expand(parameters);
            codes(result.Expansion.Contains).Should().Equal("code1", "code2a", "code2b");
            result.Expansion.Total.Should().Be(3);
            result.Expansion.Parameter.Select(p => p.Name).Should().Equal("excludeNested", "excludeNotForUI");
            stored.Expansion.Contains.Should().HaveCount(2, "the stored expansion itself is not changed");
        }

        private static ValueSet storedWithGrouping(string url)
        {
            // A grouping entry without a code, holding two codes.
            var stored = new ValueSet { Url = url, Status = PublicationStatus.Active, Expansion = new() { Total = 2 } };
            var group = new ValueSet.ContainsComponent { Abstract = true, Display = "Group" };
            group.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            group.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code3" });
            stored.Expansion.Contains.Add(group);
            return stored;
        }

        [TestMethod]
        public async Tasks.Task RemovingAGroupingEntryDoesNotChangeTheTotal()
        {
            var stored = storedWithGrouping("http://example.org/fhir/ValueSet/grouped");
            var service = new LocalTerminologyService(new InMemoryResourceResolver(stored));
            var parameters = new ExpandParameters().WithValueSet(url: stored.Url);
            parameters.ExcludeNotForUI = new FhirBoolean(true);

            var result = (ValueSet)await service.Expand(parameters);
            codes(result.Expansion.Contains).Should().Equal("code1", "code3");
            result.Expansion.Total.Should().Be(2);
        }

        [TestMethod]
        public async Tasks.Task TheSizeCapCountsOnlyEntriesWithACode()
        {
            var stored = storedWithGrouping("http://example.org/fhir/ValueSet/grouped");
            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/grouped-importer", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(stored.Url));

            await expander(new InMemoryResourceResolver(stored), maxSize: 2).ExpandAsync(vs);
            allCodes(vs.Expansion.Contains).Should().Equal(null, "code1", "code3");
            vs.Expansion.Total.Should().Be(2);
        }

        [TestMethod]
        public async Tasks.Task ExcludeNotForUIDoesNotDependOnTheOrderOfIntersectedValueSets()
        {
            var plain = new ValueSet { Url = "http://example.org/fhir/ValueSet/plain", Status = PublicationStatus.Active, Expansion = new() };
            plain.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            plain.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2" });
            var marked = new ValueSet { Url = "http://example.org/fhir/ValueSet/marked", Status = PublicationStatus.Active, Expansion = new() };
            marked.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            marked.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2", Abstract = true });
            var resolver = new InMemoryResourceResolver(plain, marked);

            foreach (var order in new[] { new[] { plain.Url, marked.Url }, new[] { marked.Url, plain.Url } })
            {
                var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/intersect", Status = PublicationStatus.Active }
                    .Includes(i => i.ValueSets(order));
                await expander(resolver).ExpandAsync(vs, new ExpandParameters { ExcludeNotForUI = new FhirBoolean(true) });
                codes(vs.Expansion.Contains).Should().Equal("code1");
            }
        }

        [TestMethod]
        public async Tasks.Task IncludeDesignationsFromTheRequestAppliesToImportedValueSets()
        {
            var cs = simpleCodeSystem();
            cs.Concept[0].Designation.Add(new CodeSystem.DesignationComponent { Language = "nl", Value = "Code een" });
            var imported = new ValueSet { Url = "http://example.org/fhir/ValueSet/imported", Status = PublicationStatus.Active }
                .Includes(i => i.System(SIMPLE).Concepts("code1"));
            imported.Compose.Include[0].Concept[0].Designation.Add(new ValueSet.DesignationComponent { Language = "nl", Value = "Code een" });
            var resolver = new InMemoryResourceResolver(cs, imported);

            var vs = new ValueSet { Url = "http://example.org/fhir/ValueSet/importer", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(imported.Url));
            await expander(resolver).ExpandAsync(vs, new ExpandParameters { IncludeDesignations = new FhirBoolean(true) });
            vs.Expansion.Contains.Single().Designation.Should().ContainSingle();
            imported.HasExpansion.Should().BeFalse("the request-specific expansion of the import is not stored");

            var without = new ValueSet { Url = vs.Url, Status = PublicationStatus.Active }.Includes(i => i.ValueSets(imported.Url));
            var settings = new ValueSetExpanderSettings { ValueSetSource = resolver, IncludeDesignations = true };
            await new ValueSetExpander(settings).ExpandAsync(without, new ExpandParameters { IncludeDesignations = new FhirBoolean(false) });
            without.Expansion.Contains.Single().Designation.Should().BeEmpty();
        }

        [TestMethod]
        public async Tasks.Task AlwaysExpandFlatFlattensAnExpansionThatCameWithTheValueSet()
        {
            var stored = new ValueSet { Url = "http://example.org/fhir/ValueSet/stored-nested", Status = PublicationStatus.Active, Expansion = new() };
            var parent = new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2" };
            parent.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2a" });
            stored.Expansion.Contains.Add(parent);
            var resolver = new InMemoryResourceResolver(stored);
            var service = new LocalTerminologyService(resolver, new ValueSetExpanderSettings { ValueSetSource = resolver, AlwaysExpandFlat = true });

            var result = (ValueSet)await service.Expand(new ExpandParameters().WithValueSet(url: stored.Url));
            codes(result.Expansion.Contains).Should().Equal("code2", "code2a");
            stored.Expansion.Contains.Should().ContainSingle("the stored expansion itself is not changed");
        }

        private class ResolvingTerminologyService(IAsyncResourceResolver resolver) : BaseTerminologyService
        {
            protected internal override async Tasks.Task<ValueSet> ResolveValueSet(Canonical canonical) =>
                await resolver.FindValueSetAsync(canonical.Uri);
        }

        [TestMethod]
        public async Tasks.Task BaseTerminologyServiceAppliesSupportedParametersAndIgnoresTheRest()
        {
            // BaseTerminologyService resolves only value sets for the expander, so this imports a stored, nested expansion.
            var stored = new ValueSet { Url = "http://example.org/fhir/ValueSet/stored", Status = PublicationStatus.Active, Expansion = new() };
            var parent = new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2" };
            parent.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code2a" });
            stored.Expansion.Contains.Add(new ValueSet.ContainsComponent { System = SIMPLE, Code = "code1" });
            stored.Expansion.Contains.Add(parent);
            var importer = new ValueSet { Url = "http://example.org/fhir/ValueSet/importer", Status = PublicationStatus.Active }
                .Includes(i => i.ValueSets(stored.Url));
            var service = new ResolvingTerminologyService(new InMemoryResourceResolver(stored, importer));

            var parameters = new ExpandParameters().WithValueSet(url: importer.Url);
            parameters.ExcludeNested = new FhirBoolean(true);
            parameters.Add("property", new FhirString("definition"));

            var result = (ValueSet)await service.Expand(parameters);
            codes(result.Expansion.Contains).Should().Equal("code1", "code2", "code2a");
            result.Expansion.Parameter.Select(p => p.Name).Should().Equal("excludeNested");
            importer.HasExpansion.Should().BeFalse("the shaped expansion is made on a copy");
        }
    }
}
