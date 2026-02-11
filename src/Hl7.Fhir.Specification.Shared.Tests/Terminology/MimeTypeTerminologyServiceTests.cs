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
    public class MimeTypeTerminologyServiceTests
    {
        private readonly MimeTypeTerminologyService _service = new();
        private const string MIMETYPEVS = "http://hl7.org/fhir/ValueSet/mimetypes";
        private const string MIMETYPE_VERSIONED_VS = "http://hl7.org/fhir/ValueSet/mimetypes|4.0.1";
        private const string MIMETYPE_VS_STU3 = "http://www.rfc-editor.org/bcp/bcp13.txt";
        private const string ADMINGENDERVS = "http://hl7.org/fhir/ValueSet/administrative-gender";

        [TestMethod]
        public async Task MimeTypeValidationTest()
        {
            var parameters = new ValidateCodeParameters()
                .WithValueSet(MIMETYPEVS)
                .WithCode(code: "invalid", context: "context", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"'invalid' is not a valid MIME type."))
                .Should().BeTrue();


            parameters = new ValidateCodeParameters()
                .WithValueSet(MIMETYPEVS)
                .WithCode(code: "application/json", context: "context", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();

            parameters = new ValidateCodeParameters()
                .WithValueSet(MIMETYPEVS)
                .WithCode(code: "json", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);
            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.Should().BeEquivalentTo(new FhirBoolean(true));

            parameters = new ValidateCodeParameters()
                .WithValueSet(ADMINGENDERVS)
                .WithCode(code: "application/json", context: "context", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            Func<Task> validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage($"Cannot find valueset '{ADMINGENDERVS}'");

            parameters = new ValidateCodeParameters()
                    .WithCode(code: "application/json", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("At least one of 'url', 'context' or a 'valueSet' must be provided.");

            parameters = new ValidateCodeParameters()
                  .WithValueSet(MIMETYPEVS)
                  .WithCode(code: "male", system: "http://hl7.org/fhir/administrative-gender");

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("This service only supports code system 'urn:ietf:bcp:13'.");



            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("This service only supports code system 'urn:ietf:bcp:13'.");
        }

        [DataRow(MIMETYPE_VERSIONED_VS)]
        [DataRow(MIMETYPE_VS_STU3)]
        [TestMethod]
        public async Task MimeTypeValidationAlternativeValueSet(string valueset)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "invalid", context: "context", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.IsExactly(new FhirString($"'invalid' is not a valid MIME type."))
                .Should().BeTrue();

            parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "application/json", context: "context", system: MimeTypeTerminologyService.MIMETYPE_SYSTEM);

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();
        }

        [TestMethod]
        public async Task CodeSystemValidateCodeTest()
        {
            var parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(MimeTypeTerminologyService.MIMETYPE_SYSTEM)
                .WithCode(code: "invalid");

            var result = await _service.CodeSystemValidateCode(parameters);
            var messageParam = result.Parameter.Should().Contain(p => p.Name == "message").Subject;
            messageParam.Value.IsExactly(new FhirString($"'invalid' is not a valid MIME type."))
                .Should().BeTrue();

            parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(MimeTypeTerminologyService.MIMETYPE_SYSTEM)
                .WithCode(code: "application/json");

            result = await _service.CodeSystemValidateCode(parameters);
            var resultParam = result.Parameter.Should().Contain(p => p.Name == "result").Subject;
            resultParam.Value.IsExactly(new FhirBoolean(true))
                .Should().BeTrue();

            parameters = new CodeSystemValidateCodeParameters()
                .WithCodeSystem(MimeTypeTerminologyService.MIMETYPE_SYSTEM)
                .WithCode(code: "json");

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
                .WithCodeSystem(MimeTypeTerminologyService.MIMETYPE_SYSTEM)
                .WithCoding(new Coding { Code = "application/json" }); // Coding without system

            validateCode = async () => await _service.CodeSystemValidateCode(codingWithoutSystem);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Must have a coding with both code and system to be validated.");
        }
    }
}