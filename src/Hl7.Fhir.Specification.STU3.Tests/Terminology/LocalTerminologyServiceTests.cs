using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using ValidateCodeResult = Hl7.Fhir.Specification.Terminology.ValidateCodeResult;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class LocalTerminologyServiceTests
    {
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
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", "context", null, "UCUM Codes")]
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", null, "theSystem", "UCUM Codes")]
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
                 .WithValueSet("http://www.rfc-editor.org/bcp/bcp13.txt")
                 .WithCode(code: "application/json", context: "context", inferSystem: true);

            var result = await service.ValueSetValidateCode(parameters);

            result.Parameter.Should().Contain(p => p.Name == "result")
               .Subject.Value.IsExactly(new FhirBoolean(true)).Should().BeTrue();
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
        public async Task ValidateCodeReturnsCodingDetailsAndIssues()
        {
            const string system = "http://example.org/fhir/CodeSystem/shape";
            var codeSystem = new CodeSystem
            {
                Url = system,
                Version = "1.0.0",
                Status = PublicationStatus.Active,
                Content = CodeSystem.CodeSystemContentMode.Complete,
                Concept = [new() { Code = "code1", Display = "Display 1" }]
            };
            var valueSet = new ValueSet
            {
                Url = "http://example.org/fhir/ValueSet/shape-all",
                Status = PublicationStatus.Active,
                Compose = new() { Include = [new() { System = system }] }
            };
            var service = new LocalTerminologyService(new InMemoryResourceResolver(codeSystem, valueSet));

            var valid = new ValidateCodeResult(await service.ValueSetValidateCode(
                new ValidateCodeParameters().WithValueSet(valueSet.Url).WithCoding(new Coding(system, "code1", "Wrong"))));

            valid.Result!.Value.Should().BeTrue();
            valid.Code!.Value.Should().Be("code1");
            valid.System!.Value.Should().Be(system);
            valid.Version!.Value.Should().Be("1.0.0");
            valid.Display!.Value.Should().Be("Display 1");
            var displayIssue = ((OperationOutcome)valid.Issues!).Issue.Should().ContainSingle().Subject;
            displayIssue.Details.Coding.Single().Code.Should().Be("invalid-display");
            displayIssue.Expression.Should().Equal("Coding.display");

            var invalid = new ValidateCodeResult(await service.ValueSetValidateCode(
                new ValidateCodeParameters().WithValueSet(valueSet.Url).WithCode("codeX", system)));

            invalid.Result!.Value.Should().BeFalse();
            invalid.Code!.Value.Should().Be("codeX");
            invalid.System!.Value.Should().Be(system);
            ((OperationOutcome)invalid.Issues!).Issue.Select(i => i.Details.Coding.Single().Code).Should().Equal(
                "not-in-vs",
                "invalid-code");
        }
    }
}
