using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Support.Tests.Specification
{
    [TestClass]
    public class MultiTerminologyServiceTests
    {
        [TestMethod]
        public async Task MultipleTerminologyServicesCodeValidationTest()
        {
            //define output params
            var firstFailingParameters = new Parameters()
                                   .Add("result", new FhirBoolean(false))
                                   .Add("message", new FhirString("this is the first ts that fails"));


            var secondFailingParameters = new Parameters()
                                   .Add("result", new FhirBoolean(false))
                                   .Add("message", new FhirString("this is the second ts that fails"));

            var succeedingParameters = new Parameters()
                                  .Add("result", new FhirBoolean(true));

            //setup mock services
            var firstFailingTS = setupMockTermService(firstFailingParameters);
            var secondFailingTS = setupMockTermService(secondFailingParameters);
            var succeedingTs = setupMockTermService(succeedingParameters);


            //run tests
            var multits = new MultiTerminologyService(firstFailingTS, secondFailingTS);
            var result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault().Value.As<FhirBoolean>().Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault().Value.As<FhirString>().Value.Should().Be("this is the first ts that fails");

            multits = new MultiTerminologyService(firstFailingTS, succeedingTs);
            result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault().Value.As<FhirBoolean>().Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault().Value.As<FhirString>().Value.Should().Be("this is the first ts that fails");
        }

        [TestMethod]
        public async Task MultipleTerminologyServicesErrorHandlingTest()
        {
            //define output params
            var succeedingParameters = new Parameters()
                                  .Add("result", new FhirBoolean(true));

            //setup mock services
            var firstExceptionThrowingTs = setupExceptionThrowingMockTermService("this is the first error message that's thrown", HttpStatusCode.NotFound);
            var secondExceptionThrowingTs = setupExceptionThrowingMockTermService("this is the second error message that's thrown", HttpStatusCode.BadRequest);
            var succeedingTs = setupMockTermService(succeedingParameters);

            //run tests
            var multits = new MultiTerminologyService(firstExceptionThrowingTs, succeedingTs);
            var result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault().Value.As<FhirBoolean>().Value.Should().Be(true);

            multits = new MultiTerminologyService(firstExceptionThrowingTs, secondExceptionThrowingTs);
            var call = async () => await multits.ValueSetValidateCode(new Parameters());
            await call.Should().ThrowAsync<FhirOperationException>().WithMessage("this is the first error message that's thrown");
            await call.Should().ThrowAsync<FhirOperationException>().Where(e => e.Status == HttpStatusCode.NotFound);
            await call.Should().ThrowAsync<FhirOperationException>().Where(e => ((AggregateException)e.InnerException).InnerExceptions.Count == 2);
        }

        private ITerminologyService setupMockTermService(Parameters output)
        {
            var mock = new Mock<ITerminologyService>();

            mock.Setup(m => m.ValueSetValidateCode(new Parameters(), null, false))
                .ReturnsAsync(output);

            return mock.Object;
        }

        private ITerminologyService setupExceptionThrowingMockTermService(string message, HttpStatusCode statusCode)
        {
            var mock = new Mock<ITerminologyService>();

            mock.Setup(m => m.ValueSetValidateCode(new Parameters(), null, false))
                .Throws(new FhirOperationException(message, statusCode));

            return mock.Object;
        }
    }
}
