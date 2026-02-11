using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class LanguageTerminologyServiceTests
    {
        private readonly LanguageTerminologyService _service = new();
        private const string LANGUAGE_VS = "http://hl7.org/fhir/ValueSet/all-languages";
        private const string ADMINGENDERVS = "http://hl7.org/fhir/ValueSet/administrative-gender";

        [TestMethod]
        public async Task LanguageValidationTest()
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(LANGUAGE_VS)
                   .WithCode(code: "ned", context: "context", system: LanguageTerminologyService.LANGUAGE_SYSTEM);

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"'ned' is not a valid language."))
                .Should().BeTrue();
            
            parameters = new ValidateCodeParameters()
                   .WithValueSet(LANGUAGE_VS)
                   .WithCode(code: "nl-NL", context: "context", system: LanguageTerminologyService.LANGUAGE_SYSTEM);

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();
            
            parameters = new ValidateCodeParameters()
                .WithValueSet(LANGUAGE_VS)
                .WithCode(code: "fr-CH", context: "context", system: LanguageTerminologyService.LANGUAGE_SYSTEM);

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true)).Should()
                .BeTrue();

            parameters = new ValidateCodeParameters()
                   .WithValueSet(ADMINGENDERVS)
                   .WithCode(code: "application/json", context: "context", system: LanguageTerminologyService.LANGUAGE_SYSTEM);

            Func<Task> validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage($"Cannot find valueset '{ADMINGENDERVS}'");

            parameters = new ValidateCodeParameters()
                  .WithCode(code: "application/json", system: LanguageTerminologyService.LANGUAGE_SYSTEM);

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("At least one of 'url', 'context' or a 'valueSet' must be provided.");

            parameters = new ValidateCodeParameters()
                  .WithValueSet(LANGUAGE_VS)
                  .WithCode(code: "male", system: "http://hl7.org/fhir/administrative-gender");

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Unknown system 'http://hl7.org/fhir/administrative-gender'");
        }

        [TestMethod]
        public async Task CodeSystemValidateCodeTest()
        {
            var parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(LanguageTerminologyService.LANGUAGE_SYSTEM)
                .WithCode(code: "ned");

            var result = await _service.CodeSystemValidateCode(parameters);
            var messageParam = result.Parameter.Should().Contain(p => p.Name == "message").Subject;
            messageParam.Value.IsExactly(new FhirString($"'ned' is not a valid language."))
                .Should().BeTrue();

            parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(LanguageTerminologyService.LANGUAGE_SYSTEM)
                .WithCode(code: "nl-NL");

            result = await _service.CodeSystemValidateCode(parameters);
            var resultParam = result.Parameter.Should().Contain(p => p.Name == "result").Subject;
            resultParam.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();

            parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(LanguageTerminologyService.LANGUAGE_SYSTEM)
                .WithCode(code: "fr-CH");

            result = await _service.CodeSystemValidateCode(parameters);
            var secondResultParam = result.Parameter.Should().Contain(p => p.Name == "result").Subject;
            secondResultParam.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();

            var csParameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem("http://hl7.org/fhir/administrative-gender")
                .WithCode(code: "male");

            Func<Task> validateCode = async () => await _service.CodeSystemValidateCode(csParameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Unknown code system 'http://hl7.org/fhir/administrative-gender'");

            // Test that system is required when using a Coding without system
            var codingWithoutSystem = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(LanguageTerminologyService.LANGUAGE_SYSTEM)
                .WithCoding(new Coding { Code = "nl-NL" }); // Coding without system

            validateCode = async () => await _service.CodeSystemValidateCode(codingWithoutSystem);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Must have a coding with both code and system to be validated.");
        }
    }
}