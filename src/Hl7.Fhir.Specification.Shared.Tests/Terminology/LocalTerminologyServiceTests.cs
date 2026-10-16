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
using ValidateCodeResult = Hl7.Fhir.Specification.Terminology.ValidateCodeResult;

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
            var vcr = new ValidateCodeResult(result);
            vcr.Result!.Value.Should().BeFalse();
            vcr.Message!.Value.Should().StartWith($"Code '{code}'{withSystem} does not exist in the value set '{valuesetTitle}' ({valueset})");

            var issues = ((OperationOutcome)vcr.Issues!).Issue;
            issues.Should().Contain(i => i.Details.Coding.Any(c => c.Code == "not-in-vs"));

            if (system is not null)
            {
                // 'theSystem' cannot be resolved, so this is reported as well
                issues.Should().Contain(i => i.Details.Coding.Any(c => c.Code == "not-found")
                                             && i.Expression.Single() == "system");
                vcr.Message.Value.Should().Contain($"A definition for CodeSystem '{system}' could not be found");
            }
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

        #region Shape of the $validate-code result

        private const string ShapeSystem = "http://example.org/fhir/CodeSystem/shape";
        private const string ShapeAllVs = "http://example.org/fhir/ValueSet/shape-all";
        private const string ShapeCode1Vs = "http://example.org/fhir/ValueSet/shape-code1";

        private static LocalTerminologyService createShapeService()
        {
            var codeSystem = new CodeSystem
            {
                Url = ShapeSystem,
                Version = "1.0.0",
                Name = "Shape",
                Status = PublicationStatus.Active,
                Content = CodeSystemContentMode.Complete,
                Property =
                [
                    new() { Code = "notSelectable", Uri = CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE, Type = CodeSystem.PropertyType.Boolean },
                    new() { Code = "inactive", Uri = CodeSystem.CONCEPTPROPERTY_INACTIVE, Type = CodeSystem.PropertyType.Boolean },
                    new() { Code = "status", Uri = CodeSystem.CONCEPTPROPERTY_STATUS, Type = CodeSystem.PropertyType.Code },
                ],
                Concept =
                [
                    new() { Code = "code1", Display = "Display 1" },
                    new() { Code = "code2", Display = "Display 2" },
                    new()
                    {
                        Code = "abstract", Display = "Abstract",
                        Property = [new() { Code = "notSelectable", Value = new FhirBoolean(true) }]
                    },
                    new()
                    {
                        Code = "inactive", Display = "Inactive",
                        Property = [new() { Code = "inactive", Value = new FhirBoolean(true) }]
                    },
                    new()
                    {
                        Code = "deprecated", Display = "Deprecated",
                        Property = [new() { Code = "status", Value = new Code("deprecated") }]
                    },
                ]
            };

            var allVs = new ValueSet
            {
                Url = ShapeAllVs,
                Name = "ShapeAll",
                Status = PublicationStatus.Active,
                Compose = new() { Include = [new() { System = ShapeSystem }] }
            };

            var code1Vs = new ValueSet
            {
                Url = ShapeCode1Vs,
                Name = "ShapeCode1",
                Status = PublicationStatus.Active,
                Compose = new() { Include = [new() { System = ShapeSystem, Concept = [new() { Code = "code1" }] }] }
            };

            return new LocalTerminologyService(new InMemoryResourceResolver(codeSystem, allVs, code1Vs));
        }

        private static async System.Threading.Tasks.Task<ValidateCodeResult> validateShape(string valueSet, Action<ValidateCodeParameters> configure)
        {
            var parameters = new ValidateCodeParameters().WithValueSet(valueSet);
            configure(parameters);
            return new ValidateCodeResult(await createShapeService().ValueSetValidateCode(parameters));
        }

        private static OperationOutcome.IssueComponent[] issues(ValidateCodeResult result) =>
            (result.Issues as OperationOutcome)?.Issue.ToArray() ?? [];

        private static string txIssueType(OperationOutcome.IssueComponent issue) =>
            issue.Details.Coding.Single(c => c.System == "http://hl7.org/fhir/tools/CodeSystem/tx-issue-type").Code;

        [TestMethod]
        public async Task ValidCodeReturnsCodeSystemVersionAndDisplay()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCode("code1", ShapeSystem));

            result.Result!.Value.Should().BeTrue();
            result.Code!.Value.Should().Be("code1");
            result.System!.Value.Should().Be(ShapeSystem);
            result.Version!.Value.Should().Be("1.0.0");
            result.Display!.Value.Should().Be("Display 1");
            result.Message.Should().BeNull();
            result.Issues.Should().BeNull();
            result.Inactive.Should().BeNull();
            result.Status.Should().BeNull();
        }

        [TestMethod]
        public async Task WrongDisplayIsAWarning()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCoding(new Coding(ShapeSystem, "code1", "Wrong")));

            result.Result!.Value.Should().BeTrue();
            result.Display!.Value.Should().Be("Display 1");
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Warning);
            txIssueType(issue).Should().Be("invalid-display");
            issue.Expression.Should().Equal("Coding.display");
            result.Message!.Value.Should().Be(issue.Details.Text);
        }

        [TestMethod]
        public async Task WrongDisplayWithLenientDisplayValidationIsAWarning()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCoding(new Coding(ShapeSystem, "code1", "Wrong"))
                .WithLenientDisplayValidation(true));

            result.Result!.Value.Should().BeTrue();
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Warning);
            txIssueType(issue).Should().Be("invalid-display");
        }

        [TestMethod]
        public async Task WrongDisplayWithoutLenientDisplayValidationIsAnError()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCoding(new Coding(ShapeSystem, "code1", "Wrong"))
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            result.Code!.Value.Should().Be("code1");
            result.Display!.Value.Should().Be("Display 1");
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Error);
            issue.Code.Should().Be(OperationOutcome.IssueType.Invalid);
            txIssueType(issue).Should().Be("invalid-display");
            issue.Expression.Should().Equal("Coding.display");
            result.Message!.Value.Should().Be(issue.Details.Text);
        }

        [TestMethod]
        public async Task WrongDisplayOfCodeParameterWithoutLenientDisplayValidationIsAnError()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCode("code1", ShapeSystem, display: "Wrong")
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Error);
            issue.Expression.Should().Equal("display");
        }

        [TestMethod]
        public async Task RightDisplayWithoutLenientDisplayValidationIsValid()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCoding(new Coding(ShapeSystem, "code1", "Display 1"))
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeTrue();
            result.Issues.Should().BeNull();
        }

        [TestMethod]
        public async Task CodeableConceptWithOneWrongDisplayWithoutLenientDisplayValidationIsValidWhenAnotherCodingIsValid()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCodeableConcept(new CodeableConcept { Coding = [new Coding(ShapeSystem, "code1", "Wrong"), new Coding(ShapeSystem, "code2", "Display 2")] })
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeTrue();
            result.Code!.Value.Should().Be("code2");
        }

        [TestMethod]
        public async Task CodeableConceptWithOnlyAWrongDisplayWithoutLenientDisplayValidationIsInvalid()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCodeableConcept(new CodeableConcept { Coding = [new Coding(ShapeSystem, "code1", "Wrong")] })
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Error);
            txIssueType(issue).Should().Be("invalid-display");
            issue.Expression.Should().Equal("CodeableConcept.coding[0].display");
        }

        [TestMethod]
        public async Task CodeableConceptWithOnlyWrongDisplaysWithoutLenientDisplayValidationDoesNotSayTheCodesAreNotInTheValueSet()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCodeableConcept(new CodeableConcept { Coding = [new Coding(ShapeSystem, "code1", "Wrong"), new Coding(ShapeSystem, "code2", "Wrong")] })
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            issues(result).Select(txIssueType).Should().Equal("invalid-display", "invalid-display");
            issues(result).Select(i => i.Expression.Single()).Should().Equal("CodeableConcept.coding[0].display", "CodeableConcept.coding[1].display");
            result.Message!.Value.Should().Be(string.Join("; ", issues(result).Select(i => i.Details.Text)));
        }

        [TestMethod]
        public async Task TextOnlyCodeableConceptWithoutLenientDisplayValidationSaysItIsNotInTheValueSet()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCodeableConcept(new CodeableConcept { Text = "Only text" })
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            issues(result).Select(txIssueType).Should().Equal("not-in-vs");
            result.Message!.Value.Should().Be(issues(result).Single().Details.Text);
        }

        [TestMethod]
        public async Task AbstractCodeWithWrongDisplayWithoutLenientDisplayValidationReportsBothErrors()
        {
            var result = await validateShape(ShapeAllVs, p => p
                .WithCoding(new Coding(ShapeSystem, "abstract", "Wrong"))
                .WithAbstract(false)
                .WithLenientDisplayValidation(false));

            result.Result!.Value.Should().BeFalse();
            issues(result).Select(txIssueType).Should().Equal("code-rule", "invalid-display");
            issues(result).Should().OnlyContain(i => i.Severity == OperationOutcome.IssueSeverity.Error);
            result.Message!.Value.Should().Be(string.Join("; ", issues(result).Select(i => i.Details.Text)));
        }

        [TestMethod]
        public void LenientDisplayValidationParameterIsSetAndCleared()
        {
            var vsParameters = new ValidateCodeParameters().WithLenientDisplayValidation(false);
            vsParameters.LenientDisplayValidation!.Value.Should().BeFalse();
            vsParameters.GetSingleValue<FhirBoolean>("lenient-display-validation")!.Value.Should().BeFalse();
            vsParameters.WithLenientDisplayValidation(null).LenientDisplayValidation.Should().BeNull();

            var csParameters = new CodeSystemValidateCodeParameters().WithLenientDisplayValidation(true);
            csParameters.LenientDisplayValidation!.Value.Should().BeTrue();
            csParameters.GetSingleValue<FhirBoolean>("lenient-display-validation")!.Value.Should().BeTrue();
            csParameters.WithLenientDisplayValidation(null).LenientDisplayValidation.Should().BeNull();
        }

        [TestMethod]
        public async Task CodeInCodeSystemButNotInValueSet()
        {
            var result = await validateShape(ShapeCode1Vs, p => p.WithCoding(new Coding(ShapeSystem, "code2")));

            result.Result!.Value.Should().BeFalse();
            result.Code!.Value.Should().Be("code2");
            result.System!.Value.Should().Be(ShapeSystem);
            result.Version!.Value.Should().Be("1.0.0");
            result.Display!.Value.Should().Be("Display 2");
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Error);
            issue.Code.Should().Be(OperationOutcome.IssueType.CodeInvalid);
            txIssueType(issue).Should().Be("not-in-vs");
            issue.Expression.Should().Equal("Coding.code");
            result.Message!.Value.Should().Be(issue.Details.Text);
        }

        [TestMethod]
        public async Task CodeUnknownInCompleteCodeSystem()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCode("codeX", ShapeSystem));

            result.Result!.Value.Should().BeFalse();
            result.Code!.Value.Should().Be("codeX");
            result.System!.Value.Should().Be(ShapeSystem);
            issues(result).Select(txIssueType).Should().Equal(
                "not-in-vs",
                "invalid-code");
            issues(result).Should().OnlyContain(i => i.Expression.Single() == "code");
            result.Message!.Value.Should().Be(string.Join("; ", issues(result).Select(i => i.Details.Text)));
        }

        [TestMethod]
        public async Task UnknownCodeSystem()
        {
            const string unknownSystem = "http://example.org/fhir/CodeSystem/unknown";
            var result = await validateShape(ShapeAllVs, p => p.WithCoding(new Coding(unknownSystem, "code1")));

            result.Result!.Value.Should().BeFalse();
            result.System!.Value.Should().Be(unknownSystem);
            result.UnknownSystem!.Value.Should().Be(unknownSystem);
            var notFound = issues(result).Should().ContainSingle(i => i.Code == OperationOutcome.IssueType.NotFound).Subject;
            txIssueType(notFound).Should().Be("not-found");
            notFound.Expression.Should().Equal("Coding.system");
        }

        [TestMethod]
        public async Task AbstractCodeNotAllowed()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCoding(new Coding(ShapeSystem, "abstract")).WithAbstract(false));

            result.Result!.Value.Should().BeFalse();
            result.Code!.Value.Should().Be("abstract");
            var issue = issues(result).Should().ContainSingle().Subject;
            txIssueType(issue).Should().Be("code-rule");
            issue.Code.Should().Be(OperationOutcome.IssueType.BusinessRule);
            result.Message!.Value.Should().Contain("abstract");
        }

        [TestMethod]
        public async Task InactiveCodeIsValidWithAWarning()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCoding(new Coding(ShapeSystem, "inactive")));

            result.Result!.Value.Should().BeTrue();
            result.Inactive!.Value.Should().BeTrue();
            var issue = issues(result).Should().ContainSingle().Subject;
            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Warning);
            txIssueType(issue).Should().Be("code-comment");
            issue.Expression.Should().Equal("Coding");
            result.Message!.Value.Should().Be(issue.Details.Text);
        }

        [TestMethod]
        public async Task DeprecatedCodeReturnsStatus()
        {
            var result = await validateShape(ShapeAllVs, p => p.WithCode("deprecated", ShapeSystem));

            result.Result!.Value.Should().BeTrue();
            result.Status!.Value.Should().Be("deprecated");
            result.Inactive.Should().BeNull();
            var issue = issues(result).Should().ContainSingle().Subject;
            txIssueType(issue).Should().Be("code-comment");
            issue.Expression.Should().Equal("code");
        }

        [TestMethod]
        public async Task ValidCodeableConceptEchoesConceptAndReturnsMatchingCoding()
        {
            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding(ShapeSystem, "codeX"));
            cc.Coding.Add(new Coding(ShapeSystem, "code1", "Wrong"));

            var result = await validateShape(ShapeAllVs, p => p.WithCodeableConcept(cc));

            result.Result!.Value.Should().BeTrue();
            result.CodeableConcept!.Matches(cc).Should().BeTrue();
            result.CodeableConcept.Should().NotBeSameAs(cc);
            result.Code!.Value.Should().Be("code1");
            result.System!.Value.Should().Be(ShapeSystem);
            result.Display!.Value.Should().Be("Display 1");
            issues(result).Should().ContainSingle().Which.Expression.Should().Equal("CodeableConcept.coding[1].display");
        }

        [TestMethod]
        public async Task InvalidCodeableConceptReportsAllCodings()
        {
            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding(ShapeSystem, "codeX"));
            cc.Coding.Add(new Coding(ShapeSystem, "code2"));

            var result = await validateShape(ShapeCode1Vs, p => p.WithCodeableConcept(cc));

            result.Result!.Value.Should().BeFalse();
            result.CodeableConcept!.Matches(cc).Should().BeTrue();
            result.CodeableConcept.Should().NotBeSameAs(cc);
            result.Code.Should().BeNull();
            result.System.Should().BeNull();

            var all = issues(result);
            txIssueType(all[0]).Should().Be("not-in-vs");
            all[0].Expression.Should().BeEmpty();
            all.Should().Contain(i => i.Expression.SingleOrDefault() == "CodeableConcept.coding[0].code"
                                      && txIssueType(i) == "invalid-code");
            all.Should().Contain(i => i.Expression.SingleOrDefault() == "CodeableConcept.coding[1].code"
                                      && txIssueType(i) == "not-in-vs");
            result.Message!.Value.Should().StartWith("None of the Codings in the CodeableConcept were valid");
        }

        [TestMethod]
        public async Task InvalidSingleCodingCodeableConceptReturnsThatCodingsResult()
        {
            var cc = new CodeableConcept(ShapeSystem, "code2");

            var result = await validateShape(ShapeCode1Vs, p => p.WithCodeableConcept(cc));

            result.Result!.Value.Should().BeFalse();
            result.CodeableConcept!.Matches(cc).Should().BeTrue();
            var issue = issues(result).Should().ContainSingle().Subject;
            txIssueType(issue).Should().Be("not-in-vs");
            issue.Expression.Should().Equal("CodeableConcept.coding[0].code");
            result.Message!.Value.Should().Be(issue.Details.Text);
        }

        [TestMethod]
        public async Task InvalidCodeableConceptWithUnknownSystemReportsIt()
        {
            const string unknownSystem = "http://example.org/fhir/CodeSystem/unknown";
            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding(unknownSystem, "a"));
            cc.Coding.Add(new Coding(unknownSystem, "b"));

            var result = await validateShape(ShapeAllVs, p => p.WithCodeableConcept(cc));

            result.Result!.Value.Should().BeFalse();
            result.UnknownSystem!.Value.Should().Be(unknownSystem);
            issues(result).Where(i => txIssueType(i) == "not-found")
                .Select(i => i.Expression.Single())
                .Should().Equal("CodeableConcept.coding[0].system", "CodeableConcept.coding[1].system");
        }

        [TestMethod]
        public async Task EnumeratedConceptGetsDisplayAndVersionFromCodeSystem()
        {
            var result = await validateShape(ShapeCode1Vs, p => p.WithCoding(new Coding(ShapeSystem, "code1", "Wrong")));

            result.Result!.Value.Should().BeTrue();
            result.Display!.Value.Should().Be("Display 1");
            result.Version!.Value.Should().Be("1.0.0");
            var issue = issues(result).Should().ContainSingle().Subject;
            txIssueType(issue).Should().Be("invalid-display");
            issue.Details.Text.Should().Contain("should be 'Display 1'");
        }

        [TestMethod]
        public async Task StatusComesFromTheCodeSystemVersionInTheExpansion()
        {
            CodeSystem version(string v, string status) => new()
            {
                Url = ShapeSystem,
                Version = v,
                Status = PublicationStatus.Active,
                Content = CodeSystemContentMode.Complete,
                Property = [new() { Code = "status", Uri = CodeSystem.CONCEPTPROPERTY_STATUS, Type = CodeSystem.PropertyType.Code }],
                Concept = [new() { Code = "code1", Display = "Display 1", Property = [new() { Code = "status", Value = new Code(status) }] }]
            };

            var expanded = new ValueSet
            {
                Url = "http://example.org/fhir/ValueSet/pre-expanded",
                Status = PublicationStatus.Active,
                Expansion = new()
                {
                    Timestamp = "2026-10-14",
                    Contains = [new() { System = ShapeSystem, Version = "1.0.0", Code = "code1", Display = "Display 1" }]
                }
            };

            // the resolver finds version 2.0.0 when asked without a version
            var service = new LocalTerminologyService(new InMemoryResourceResolver(version("2.0.0", "retired"), version("1.0.0", "active"), expanded));
            var result = new ValidateCodeResult(await service.ValueSetValidateCode(
                new ValidateCodeParameters().WithValueSet(expanded.Url).WithCode("code1", ShapeSystem)));

            result.Result!.Value.Should().BeTrue();
            result.Version!.Value.Should().Be("1.0.0");
            result.Status.Should().BeNull(); // version 1.0.0 is active, 2.0.0 would have been retired
            result.Inactive.Should().BeNull();
            result.Issues.Should().BeNull();
        }

        #endregion
    }
}
