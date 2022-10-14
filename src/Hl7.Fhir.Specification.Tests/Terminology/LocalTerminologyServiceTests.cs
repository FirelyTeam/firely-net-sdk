using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class LocalTerminologyServiceTests
    {
        private readonly LocalTerminologyService _service = new(FhirPackageSource.CreateFhirCorePackageSource());

        [TestMethod]
        public async Task CodeNotFoundMessageTest()
        {
            var valueset = "http://hl7.org/fhir/ValueSet/administrative-gender";
            var parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "invalid", context: "context")
                   .Build();

            var result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"Code 'invalid' does not exist in valueset '{valueset}'"));


            parameters = new ValidateCodeParameters()
                   .WithValueSet(valueset)
                   .WithCode(code: "invalid", system: "theSystem")
                   .Build();
            result = await _service.ValueSetValidateCode(parameters);
            result.Parameter.Should().Contain(p => p.Name == "message")
                .Subject.Value.Should().BeEquivalentTo(new FhirString($"Code 'invalid' from system 'theSystem' does not exist in valueset '{valueset}'"));

        }
    }
}
