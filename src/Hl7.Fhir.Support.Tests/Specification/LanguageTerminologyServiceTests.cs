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
                   .WithCode(code: "ned", context: "context")
                   .Build();

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"'ned' is not a valid language."))
                .Should().BeTrue();


            parameters = new ValidateCodeParameters()
                   .WithValueSet(LANGUAGE_VS)
                   .WithCode(code: "nl-NL", context: "context")
                   .Build();

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();
            
            parameters = new ValidateCodeParameters()
                .WithValueSet(LANGUAGE_VS)
                .WithCode(code: "fr-CH", context: "context")
                .Build();

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true)).Should()
                .BeTrue();

            parameters = new ValidateCodeParameters()
                   .WithValueSet(ADMINGENDERVS)
                   .WithCode(code: "application/json", context: "context")
                   .Build();

            Func<Task> validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage($"Cannot find valueset '{ADMINGENDERVS}'");

            parameters = new ValidateCodeParameters()
                  .WithCode(code: "application/json")
                  .Build();

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("If a code is provided, a url or a context must be provided");

            parameters = new ValidateCodeParameters()
                  .WithValueSet(LANGUAGE_VS)
                  .WithCode(code: "male", system: "http://hl7.org/fhir/administrative-gender")
                  .Build();

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Unknown system 'http://hl7.org/fhir/administrative-gender'");
        }
    }
}