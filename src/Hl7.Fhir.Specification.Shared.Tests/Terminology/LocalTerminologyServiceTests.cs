using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class LocalTerminologyServiceTests
    {
        private const string NonexistentValueSetUrl = "http://hl7.org/fhir/ValueSet/nonexistent";

        private readonly LocalTerminologyService _service = new(
            new CachedResolver(
                new MultiResolver(
                    ZipSource.CreateValidationSource(),
                    new InMemoryResourceResolver(new ValueSet() { Url = "http://example.com/an-exotic-valuset" })
                )
            )
        );

        [TestMethod]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "invalid", "context", null, "AdministrativeGender")]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "invalid", null, "theSystem", "AdministrativeGender")]
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", "context", null, "Common UCUM Codes for Age")]
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", null, "theSystem", "Common UCUM Codes for Age")]
        public async Task CodeNotFoundMessageTest(string valueset, string code, string context, string system, string valuesetTitle)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset);

            parameters = !string.IsNullOrEmpty(context)
                ? parameters.WithCode(code: code, context: context, inferSystem: true)
                : parameters.WithCode(code: code, system: system);

            var withSystem = string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'";
            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"Code '{code}'{withSystem} does not exist in the value set '{valuesetTitle}' ({valueset})"))
                .Should().BeTrue();
        }

        [TestMethod]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "not-human", "http://hl7.org/fhir/ValueSet/account-type", "Not existing code for gender")]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "not-human", "http://example.com/an-exotic-valuset", "Not existing code for gender")]
        public async Task CodingWithValuesetAsSystem(string valueset, string code, string system, string display)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCoding(new Coding(system, code, display));

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"The Coding references a value set, not a code system ('{system}')"))
                .Should().BeTrue();
        }

        [TestMethod]
        public async Task DefaultCoreServiceTest()
        {
            var resolver = new CachedResolver(ZipSource.CreateValidationSource());
            var service = LocalTerminologyService.CreateDefaultForCore(resolver);

            var parameters = new ValidateCodeParameters()
                .WithValueSet("http://hl7.org/fhir/ValueSet/mimetypes")
                .WithCode(code: "application/json", context: "context", inferSystem: true);
            
            var result = await service.ValueSetValidateCode(parameters);

            result.Parameter.Should().Contain(p => p.Name == "result")
               .Subject.Value.IsExactly(new FhirBoolean(true)).Should().BeTrue();
        }

        [TestMethod]
        public async Task CheckErrorBarrier()
        {

            var codeSystem = new CodeSystem()
            {
                Url = "http://fire.ly/CodeSystem/a-complicated-codesystem",
                Name = "A Complicated CodeSystem",
                Compositional = true,
                Content = CodeSystemContentMode.NotPresent
            };

            var valueSet = new ValueSet()
            {
                Url = "http://fire.ly/ValueSet/an-entire-complicated-codesystem",
                Compose = new ValueSet.ComposeComponent()
                {
                    Include = new System.Collections.Generic.List<ValueSet.ConceptSetComponent>()
                    {
                        new () {System = "http://fire.ly/CodeSystem/a-complicated-codesystem" }
                    }
                }
            };

            LocalTerminologyService _service = new(
                new CachedResolver(
                    new MultiResolver(
                        new InMemoryResourceResolver(valueSet, codeSystem)
                    )
                ));


            var parameters = new ValidateCodeParameters()
                .WithValueSet("http://fire.ly/ValueSet/an-entire-complicated-codesystem")
                .WithCode("255848005", context: "AllergyIntolerance.code.coding[0].code", inferSystem: true);



            var ac = () => _service.ValueSetValidateCode(parameters);

            var ex = await ac.Should().ThrowAsync<FhirOperationException>();
            ex.WithMessage("*compositional code system*");
        }

        [TestMethod]
        [DataRow("code", null, null, null, true)]
        [DataRow("code", "<ValueSet />", null, null, false)]
        [DataRow("code", null, "http://nu.nl/valueset", null, false)]
        [DataRow("code", null, null, "context", false)]
        [DataRow("code", "<ValueSet />", null, "context", false)]
        public void CheckValidateCodeParams(string code, string valueset, string url, string context, bool throws)
        {
            var parameters = new Parameters();
            parameters.Add("code", code is not null ? new Code(code) : null);
            parameters.Add("url", url is not null ? new FhirUri("http://hl7.org/fhir/ValueSet/administrative-gender") : null );
            parameters.Add("context", context is not null ? new FhirUri("context") : null);
            parameters.Add("inferSystem", new FhirBoolean(true));
            parameters.Add("valueSet", valueset is not null ? new ValueSet() : null);

            Action validate = () => TerminologyValidationHelpers.ValidateValueSetValidateCodeParameters(new(parameters));

            if (!throws)
                validate.Should().NotThrow();
            else
                validate.Should().Throw<FhirOperationException>();
        }

        [TestMethod]
        [DataRow("http://hl7.org/fhir/ValueSet/vs", null, "http://hl7.org/fhir/ValueSet/vs")]
        [DataRow("http://hl7.org/fhir/ValueSet/vs|1.0", null, "http://hl7.org/fhir/ValueSet/vs|1.0")]
        [DataRow("http://hl7.org/fhir/ValueSet/vs", "2.0", "http://hl7.org/fhir/ValueSet/vs|2.0")]
        [DataRow("http://hl7.org/fhir/ValueSet/vs|2.0", "3.0", "http://hl7.org/fhir/ValueSet/vs|3.0")]
        public async Task PicksUpValidationVersionInUri(string url, string vsVersion, string resolved)
        {
            var parameters = new ValidateCodeParameters();
            parameters.Add("code", new Code("code"));
            parameters.Add("system", new FhirUri("test"));
            parameters.Add("url", new FhirUri(url));

            if(vsVersion is not null)
                parameters.Add("valueSetVersion", new FhirString(vsVersion));

            var resolver = Substitute.For<IAsyncResourceResolver>();
            var localTs = new LocalTerminologyService(resolver);

            // because we're not returning a valueset in the mock, we should get an error.
            var validate = async () => await localTs.ValueSetValidateCode(parameters);
            await validate.Should().ThrowAsync<FhirOperationException>();

            // but we're called with the correct version before that.
            await resolver.Received().FindValueSetAsync(Arg.Is<string>(u => u == resolved));
        }

        [TestMethod]
        public async Task Expand_PreservesStatus422ForInvalidValueSet()
        {
            // Test for issue: LocalTerminologyService.Expand hides internally reported 422 HttpStatus FhirOperationException
            var localTerminology = new LocalTerminologyService(ZipSource.CreateValidationSource());

            var valueSet = new ValueSet
            {
                Url = NonexistentValueSetUrl,
                Compose = new ValueSet.ComposeComponent
                {
                    Include = new System.Collections.Generic.List<ValueSet.ConceptSetComponent>
                    {
                        new ValueSet.ConceptSetComponent { System = "http://hl7.org/fhir/CodeSystem/nonexistent" }
                    }
                }
            };

            var expandAction = async () => await localTerminology.Expand(
                new ExpandParameters().WithValueSet(valueSet: valueSet));

            var ex = await expandAction.Should().ThrowAsync<FhirOperationException>();
            ex.Which.Status.Should().Be((System.Net.HttpStatusCode)422);
        }

        // Regression test for https://github.com/FirelyTeam/firely-net-sdk/issues/3582:
        // validating a multi-coding CodeableConcept used to fan out with Task.WhenAll, so a
        // non-thread-safe caller-supplied resolver (e.g. a scoped EF Core DbContext) could see
        // overlapping calls from a single logical validate-code operation.
        [TestMethod]
        public async Task ValidateCodeVsDoesNotCallResolverConcurrentlyForMultiCodingCodeableConcept()
        {
            var resolver = new ConcurrencyTrackingResolver();
            var service = new LocalTerminologyService(resolver);

            var valueSet = new ValueSet
            {
                Url = "http://fire.ly/ValueSet/empty-vs",
                Expansion = new ValueSet.ExpansionComponent()
            };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://fire.ly/CodeSystem/a", "code-a"));
            cc.Coding.Add(new Coding("http://fire.ly/CodeSystem/b", "code-b"));
            cc.Coding.Add(new Coding("http://fire.ly/CodeSystem/c", "code-c"));

            var parameters = new ValidateCodeParameters()
                .WithValueSet(url: valueSet.Url, valueSet: valueSet)
                .WithCodeableConcept(cc);

            await service.ValueSetValidateCode(parameters);

            resolver.MaxObservedConcurrency.Should().Be(1);
        }

        private class ConcurrencyTrackingResolver : IAsyncResourceResolver
        {
            private int _active;

            public int MaxObservedConcurrency { get; private set; }

            public System.Threading.Tasks.Task<Resource> ResolveByUriAsync(string uri) => throw new NotImplementedException();

            public System.Threading.Tasks.Task<Resource> ResolveByCanonicalUriAsync(string uri) => throw new NotImplementedException();

            public async System.Threading.Tasks.Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri)
            {
                var current = Interlocked.Increment(ref _active);
                MaxObservedConcurrency = Math.Max(MaxObservedConcurrency, current);
                try
                {
                    // Mimics a non-thread-safe resolver (e.g. a scoped EF Core DbContext): if this
                    // method were ever re-entered concurrently, MaxObservedConcurrency would exceed 1.
                    await System.Threading.Tasks.Task.Delay(20).ConfigureAwait(false);
                    return ResolverException.NotFound();
                }
                finally
                {
                    Interlocked.Decrement(ref _active);
                }
            }
        }

        // Regression test for https://github.com/FirelyTeam/firely-net-sdk/issues/3582:
        // expanding a "grouping" value set (compose.include[].valueSet listing 2+ canonicals) used to
        // fan out with Task.WhenAll in ValueSetExpander.processValueSetGroup, so a non-thread-safe
        // caller-supplied resolver could see overlapping calls from a single logical expand operation.
        [TestMethod]
        public async Task Expand_GroupingValueSet_DoesNotCallResolverConcurrently()
        {
            var vsA = new ValueSet
            {
                Url = "http://fire.ly/ValueSet/vs-a",
                Expansion = new ValueSet.ExpansionComponent
                {
                    Contains = new System.Collections.Generic.List<ValueSet.ContainsComponent>
                    {
                        new() { System = "http://fire.ly/CodeSystem/x", Code = "1" }
                    }
                }
            };
            var vsB = new ValueSet
            {
                Url = "http://fire.ly/ValueSet/vs-b",
                Expansion = new ValueSet.ExpansionComponent
                {
                    Contains = new System.Collections.Generic.List<ValueSet.ContainsComponent>
                    {
                        new() { System = "http://fire.ly/CodeSystem/x", Code = "1" }
                    }
                }
            };

            var groupingVs = new ValueSet
            {
                Url = "http://fire.ly/ValueSet/grouping-vs",
                Compose = new ValueSet.ComposeComponent
                {
                    Include = new System.Collections.Generic.List<ValueSet.ConceptSetComponent>
                    {
                        new() { ValueSet = new[] { vsA.Url, vsB.Url } }
                    }
                }
            };

            var resolver = new ConcurrencyTrackingCanonicalResolver(vsA, vsB);
            var service = new LocalTerminologyService(resolver);

            var parameters = new ExpandParameters().WithValueSet(valueSet: groupingVs);
            await service.Expand(parameters);

            resolver.MaxObservedConcurrency.Should().Be(1);
        }

        private class ConcurrencyTrackingCanonicalResolver : IAsyncResourceResolver
        {
            private readonly System.Collections.Generic.Dictionary<string, Resource> _resources;
            private int _active;

            public int MaxObservedConcurrency { get; private set; }

            public ConcurrencyTrackingCanonicalResolver(params ValueSet[] valueSets) =>
                _resources = valueSets.ToDictionary(vs => vs.Url, vs => (Resource)vs);

            public System.Threading.Tasks.Task<Resource> ResolveByUriAsync(string uri) => throw new NotImplementedException();

            public System.Threading.Tasks.Task<Resource> ResolveByCanonicalUriAsync(string uri) => throw new NotImplementedException();

            public async System.Threading.Tasks.Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri)
            {
                var current = Interlocked.Increment(ref _active);
                MaxObservedConcurrency = Math.Max(MaxObservedConcurrency, current);
                try
                {
                    // Mimics a non-thread-safe resolver: if this method were ever re-entered
                    // concurrently, MaxObservedConcurrency would exceed 1.
                    await System.Threading.Tasks.Task.Delay(20).ConfigureAwait(false);
                    return _resources.TryGetValue(uri, out var resource) ? resource : ResolverException.NotFound();
                }
                finally
                {
                    Interlocked.Decrement(ref _active);
                }
            }
        }
    }
}