using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(
                          It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/metadata?_summary=true")), //the call to check capabilitystatement
                          It.IsAny<CancellationToken>()))
                       .ReturnsAsync(response);


            mock
               .Protected()
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(
                          It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient/1")),  //the GET Patient
                          It.IsAny<CancellationToken>()))
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
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(
                          It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/metadata?_summary=true")), //the call to check capabilitystatement
                          It.IsAny<CancellationToken>()))
                       .ReturnsAsync(response);

            mock
               .Protected()
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(
                          It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient/1")),  //the GET Patient
                          It.IsAny<CancellationToken>()))
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
                     .As<IHttpResponseMessage>()
                     .Setup(m => m.SendAsync(
                        It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient?name=henry")),
                        It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = false }, mock.Object);

            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });

            Assert.AreEqual("/fhir/*/Bundle/example", client.LastResult.Location);
        }


        [DataTestMethod]
        [DataRow(true, DisplayName = "Use FhirVersion in Accept header")]
        [DataRow(false, DisplayName = "Don't use FhirVersion in Accept header")]
        public async System.Threading.Tasks.Task AcceptHeaderTest(bool useFhirVersionHeader)
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry"),
            };

            mock
             .Protected()
                         .As<IHttpResponseMessage>()
                         .Setup(m => m.SendAsync(
                            It.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient?name=henry")),
                            It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            using var client = new FhirClient("http://example.com", new FhirClientSettings
            {
                VerifyFhirVersion = false,
                UseFhirVersionInAcceptHeader = useFhirVersionHeader
            }, mock.Object);

            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });

            mock
                .Protected()
                .As<IHttpResponseMessage>()
                .Verify(m => m.SendAsync(
                    It.Is<HttpRequestMessage>(h => findInAcceptHeader(h.Headers.Accept, "fhirVersion", useFhirVersionHeader)),
                    It.IsAny<CancellationToken>()));
        }

        private static bool findInAcceptHeader(HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> acceptHeader, string headerName, bool exists)
            => acceptHeader.SelectMany(a => a.Parameters).Any(p => p.Name == headerName) == exists;

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { "http://example.com/Patient/example/_history", "HistoryAsync", "Patient/example" };
            yield return new object[] { "http://example.com/Patient/example/_history", "HistoryAsync", new Uri("http://example.com/Patient/example") };
            yield return new object[] { "http://example.com/Patient/example/_history", "History", "Patient/example" };
            yield return new object[] { "http://example.com/Patient/example/_history", "History", new Uri("http://example.com/Patient/example") };
            yield return new object[] { "http://example.com/Patient/_history", "TypeHistory", "Patient" };
            yield return new object[] { "http://example.com/Patient/_history", "TypeHistoryAsync", "Patient" };
            yield return new object[] { "http://example.com/_history", "WholeSystemHistory", null };
            yield return new object[] { "http://example.com/_history", "WholeSystemHistoryAsync", null };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void HistoryContainsNoSummaryParameter(string expectedRequest, string methodName, object parameter)
        {
            Uri expectedRequestUri = new(expectedRequest);
            var requests = new List<HttpRequestMessage>();

            var client = setupClient();

            var methods = typeof(FhirClient).GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var method = parameter is null
                ? methods.SingleOrDefault(m => m.Name == methodName)
                : methods.SingleOrDefault(m => m.Name == methodName && m.GetParameters().First().ParameterType == parameter.GetType());

            method.Should().NotBeNull($"{methodName} should be a method of FhirClient");

            var p = parameter is null ? Enumerable.Empty<object>() : new[] { parameter };
            p = p.Concat(method.GetParameters().Skip(parameter is null ? 0 : 1).Select(param => param.HasDefaultValue ? param.DefaultValue : null));
            method.Invoke(client, p.ToArray());


            requests.Should().OnlyContain(req => req.RequestUri == expectedRequestUri);

            FhirClient setupClient()
            {
                var mock = new Mock<HttpMessageHandler>();

                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"), // not interested in the Content
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, expectedRequestUri),
                };

                mock
                 .Protected()
                         .As<IHttpResponseMessage>()
                         .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                         .Callback<HttpRequestMessage, CancellationToken>((request, token) => requests.Add(request))
                         .ReturnsAsync(response);

                return new FhirClient(expectedRequestUri.GetLeftPart(UriPartial.Authority), new FhirClientSettings { VerifyFhirVersion = false }, mock.Object);
            }
        }

        /// <summary>
        /// Used only for intelligence help
        /// </summary>
        private interface IHttpResponseMessage
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token);
        }
    }
}