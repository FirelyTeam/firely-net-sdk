using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class FhirClientMockTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTest()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + ModelInfo.Version + @"""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com"),
            };

            var patientResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Patient"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient/1"),
            };

            //Two mocks, since response messages get disposed after each "SendAsync()", and the test required two rest calls.
            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/metadata?_summary=true")), //the call to check capabilitystatement
                          ItExpr.IsAny<CancellationToken>())
                       .ReturnsAsync(response);


            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient/1")),  //the GET Patient
                          ItExpr.IsAny<CancellationToken>())
                       .ReturnsAsync(patientResponse);

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = true }, mock.Object);
            try
            {
                await client.ReadAsync<Patient>("Patient/1");
            }
            catch (NotSupportedException ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public async System.Threading.Tasks.Task VerifyFhirVersionTestErrorThrown()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": ""0.0.0""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com"),
            };

            var patientResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Patient"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient/1"),
            };

            //Two mocks, since response messages get disposed after each "SendAsync()", and the test required two rest calls.
            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/metadata?_summary=true")), //the call to check capabilitystatement
                          ItExpr.IsAny<CancellationToken>())
                       .ReturnsAsync(response);


            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient/1")),  //the GET Patient
                          ItExpr.IsAny<CancellationToken>())
                       .ReturnsAsync(patientResponse);

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = true }, mock.Object);

            var patient = await client.ReadAsync<Patient>("Patient/1");
        }


        [TestMethod]
        public async System.Threading.Tasks.Task LocationHeaderTest()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry"),
            };

            response.Headers.Add("Location", "/fhir/*/Bundle/example");

            mock
             .Protected()
                     .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient?name=henry")), 
                        ItExpr.IsAny<CancellationToken>())
                     .ReturnsAsync(response);

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = false }, mock.Object);

            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });

            Assert.AreEqual("/fhir/*/Bundle/example", client.LastResult.Location);
        }
    }
}