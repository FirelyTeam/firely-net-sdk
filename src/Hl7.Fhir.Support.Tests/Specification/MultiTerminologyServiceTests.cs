using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Exceptions;
using System;
using System.Data;
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
            var firstFailingTS = setupMockTermService(new Parameters(), firstFailingParameters);
            var secondFailingTS = setupMockTermService(new Parameters(), secondFailingParameters);
            var succeedingTs = setupMockTermService(new Parameters(), succeedingParameters);



            //run tests
            var multits = new MultiTerminologyService(firstFailingTS, secondFailingTS);
            var result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be("this is the first ts that fails");

            multits = new MultiTerminologyService(firstFailingTS, succeedingTs);
            result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be("this is the first ts that fails");
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
            var succeedingTs = setupMockTermService(new Parameters(), succeedingParameters);

            //run tests
            var multits = new MultiTerminologyService(firstExceptionThrowingTs, succeedingTs);
            var result = await multits.ValueSetValidateCode(new Parameters());
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(true);

            multits = new MultiTerminologyService(firstExceptionThrowingTs, secondExceptionThrowingTs);
            var call = async () => await multits.ValueSetValidateCode(new Parameters());
            await call.Should().ThrowAsync<FhirOperationException>().WithMessage("this is the first error message that's thrown");
            await call.Should().ThrowAsync<FhirOperationException>().Where(e => e.Status == HttpStatusCode.NotFound);
            await call.Should().ThrowAsync<FhirOperationException>().Where(e => ((AggregateException)e.InnerException).InnerExceptions.Count == 2);
        }

        [TestMethod]
        [DataRow("http://example.org/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/second-preference", "this is the first ts that fails", DisplayName = "First is prefered")]
        [DataRow("http://example.org/fhir/ValueSet/second-preference", "http://example.org/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/second-preference", "this is the second ts that fails", DisplayName = "Second is prefered")]
        [DataRow("http://example.org/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/*", "http://example.org/fhir/ValueSet/*", "this is the first ts that fails", DisplayName = "Both are prefered")]
        [DataRow("http://fire.ly/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/first-preference", "http://example.org/fhir/ValueSet/second-preference", "this is the first ts that fails", DisplayName = "None are prefered")]
        public async Task RoutingTerminologyServiceTest(string VSInput, string firstTsPreferenceVs, string secondTsPreferenceVs, string resultMessage)
        {
            //define output parameters
            var firstFailingParameters = new Parameters()
                                  .Add("result", new FhirBoolean(false))
                                  .Add("message", new FhirString("this is the first ts that fails"));

            var secondFailingParameters = new Parameters()
                                   .Add("result", new FhirBoolean(false))
                                   .Add("message", new FhirString("this is the second ts that fails"));

            //setup mock services
            var inputParams = new ValidateCodeParameters().WithValueSet(VSInput).Build();
            var firstFailingTS = createTerminologyServiceRoutingSettings(setupMockTermService(inputParams, firstFailingParameters), firstTsPreferenceVs);
            var secondFailingTS = createTerminologyServiceRoutingSettings(setupMockTermService(inputParams, secondFailingParameters), secondTsPreferenceVs);

            //run tests
            var multits = new MultiTerminologyService(firstFailingTS, secondFailingTS);
            var result = await multits.ValueSetValidateCode(inputParams);

            //check results
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be(resultMessage);
        }

        [TestMethod]
        public async Task AddingTerminologyServiceTests()
        {
            //define output parameters
            var firstFailingParameters = new Parameters()
                                  .Add("result", new FhirBoolean(false))
                                  .Add("message", new FhirString("this is the first ts that fails"));

            var secondFailingParameters = new Parameters()
                                   .Add("result", new FhirBoolean(false))
                                   .Add("message", new FhirString("this is the second ts that fails"));

            var thirdFailingParameters = new Parameters()
                                 .Add("result", new FhirBoolean(false))
                                 .Add("message", new FhirString("this is the third ts that fails"));

            //setup mock services
            var inputParams = new ValidateCodeParameters().WithValueSet("http://example.org/fhir/ValueSet/example-vs").Build();

            var firstFailingTS = createTerminologyServiceRoutingSettings(setupMockTermService(inputParams, firstFailingParameters), "http://example.org/fhir/ValueSet/*");
            var secondFailingTS = createTerminologyServiceRoutingSettings(setupMockTermService(inputParams, secondFailingParameters), "http://example.org/*/ValueSet/example-vs");
            var thirdFailingTS = createTerminologyServiceRoutingSettings(setupMockTermService(inputParams, thirdFailingParameters), "http://example.org/fhir/ValueSet/third-preference");

            //create a multitermserver with only the first ts
            var multits = new MultiTerminologyService(new TerminologyServiceRoutingSettings[] { firstFailingTS });
            var result = await multits.ValueSetValidateCode(inputParams);
            //check results
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be("this is the first ts that fails");

            //add the second ts in front of the first
            multits.AddFirst(secondFailingTS);
            result = await multits.ValueSetValidateCode(inputParams);
            //check results
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be("this is the second ts that fails");

            //add the third ts at the back of the order
            multits.AddLast(thirdFailingTS);
            result = await multits.ValueSetValidateCode(inputParams);
            //check results, the second should still be the first to return a result
            result.Parameter.Where(p => p.Name == "result").FirstOrDefault()?.Value.As<FhirBoolean>()?.Value.Should().Be(false);
            result.Parameter.Where(p => p.Name == "message").FirstOrDefault()?.Value.As<FhirString>()?.Value.Should().Be("this is the second ts that fails");
        }

        private static TerminologyServiceRoutingSettings createTerminologyServiceRoutingSettings(ITerminologyService service, string valueSet)
        {
            return new TerminologyServiceRoutingSettings(service)
            {
                PreferredValueSets = [valueSet]
            };
        }

        private static ITerminologyService setupMockTermService(Parameters input, Parameters output)
        {
            var mock = Substitute.For<ITerminologyService>();

            mock.ValueSetValidateCode(Arg.Any<Parameters>(), Arg.Is((string)null), Arg.Is(false)).Returns(output);

            return mock;
        }

        private static ITerminologyService setupExceptionThrowingMockTermService(string message, HttpStatusCode statusCode)
        {
            var mock = Substitute.For<ITerminologyService>();

            mock.ValueSetValidateCode(Arg.Any<Parameters>(), Arg.Is((string)null), Arg.Is(false)).ThrowsAsync(new FhirOperationException(message, statusCode));

            return mock;
        }
    }
}