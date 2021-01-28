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
    public class VerifyFhirVersionTests
    {


        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTest()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": ""3.0.1""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com"),
            };

            var patientResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Patient"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient/1"),
            };

            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com")),  //IsAny<HttpRequestMessage>(),
                          ItExpr.IsAny<CancellationToken>())
                       .ReturnsAsync(response);


            mock
               .Protected()
                       .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                          "SendAsync",
                          ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient/1")),  //IsAny<HttpRequestMessage>(),
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
    }
}