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
                   .WithCode(code: "invalid", context: "context")
                   .Build();

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"'invalid' is not a valid MIME type."));


            parameters = new ValidateCodeParameters()
                   .WithValueSet(MIMETYPEVS)
                   .WithCode(code: "application/json", context: "context")
                   .Build();

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.Should().BeEquivalentTo(new FhirBoolean(true));

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
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("If a code is provided, a system or a context must be provided");

            parameters = new ValidateCodeParameters()
                  .WithValueSet(MIMETYPEVS)
                  .WithCode(code: "male", system: "http://hl7.org/fhir/administrative-gender")
                  .Build();

            validateCode = async () => await _service.ValueSetValidateCode(parameters);
            await validateCode.Should().ThrowAsync<FhirOperationException>().WithMessage("Unknown system 'http://hl7.org/fhir/administrative-gender'");
        }

        [DataRow(MIMETYPE_VERSIONED_VS)]
        [DataRow(MIMETYPE_VS_STU3)]
        [DataTestMethod]
        public async Task MimeTypeValidationAlternativeValueSet(string valueset)
        {
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "invalid", context: "context")
                   .Build();

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"'invalid' is not a valid MIME type."));


            parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "application/json", context: "context")
                   .Build();

            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "result")
                .Subject.Value.Should().BeEquivalentTo(new FhirBoolean(true));
        }
    }
}
