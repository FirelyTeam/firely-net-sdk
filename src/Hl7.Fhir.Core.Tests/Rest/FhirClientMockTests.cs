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
using System.Threading.Tasks;
namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class VerifyFhirVersionTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTest()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": ""3.0.2.""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com"),
            };
            mock
               .Protected()
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);
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