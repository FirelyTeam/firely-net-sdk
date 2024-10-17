using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

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

        [DataTestMethod]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "invalid", "context", null, "AdministrativeGender")]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "invalid", null, "theSystem", "AdministrativeGender")]
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", "context", null, "Common UCUM Codes for Age")]
        [DataRow("http://hl7.org/fhir/ValueSet/age-units", "invalid", null, "theSystem", "Common UCUM Codes for Age")]
        public async Task CodeNotFoundMessageTest(string valueset, string code, string context, string system, string valuesetTitle)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset);

            parameters = !string.IsNullOrEmpty(context)
                ? parameters.WithCode(code: code, context: context)
                : parameters.WithCode(code: code, system: system);

            var withSystem = string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'";
            var result = await _service.ValueSetValidateCode(parameters.Build());
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"Code '{code}'{withSystem} does not exist in the value set '{valuesetTitle}' ({valueset})"));
        }

        [DataTestMethod]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "not-human", "http://hl7.org/fhir/ValueSet/account-type", "Not existing code for gender")]
        [DataRow("http://hl7.org/fhir/ValueSet/administrative-gender", "not-human", "http://example.com/an-exotic-valuset", "Not existing code for gender")]
        public async Task CodingWithValuesetAsSystem(string valueset, string code, string system, string display)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCoding(new Coding(system, code, display));

            var result = await _service.ValueSetValidateCode(parameters.Build());
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"The Coding references a value set, not a code system ('{system}')"));
        }

        [TestMethod]
        public async Task DefaultCoreServiceTest()
        {
            var resolver = new CachedResolver(ZipSource.CreateValidationSource());
            var service = LocalTerminologyService.CreateDefaultForCore(resolver);

            var parameters = new ValidateCodeParameters()
                 .WithValueSet("http://hl7.org/fhir/ValueSet/mimetypes")
                 .WithCode(code: "application/json", context: "context")
                 .Build();

            var result = await service.ValueSetValidateCode(parameters);

            result.Parameter.Should().Contain(p => p.Name == "result")
               .Subject.Value.Should().BeEquivalentTo(new FhirBoolean(true));
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
                .WithCode("255848005", context: "AllergyIntolerance.code.coding[0].code");



            var ac = () => _service.ValueSetValidateCode(parameters.Build());

            var ex = await ac.Should().ThrowAsync<FhirOperationException>();
            ex.WithMessage("*compositional code system*");
        }
    }
}
