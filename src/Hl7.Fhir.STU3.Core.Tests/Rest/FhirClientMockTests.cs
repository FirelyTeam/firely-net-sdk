﻿using FluentAssertions;
using Hl7.Fhir.ElementModel;
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

        private static async System.Threading.Tasks.Task mockVersionResponse(string capabilityStatementResponseJson, string patientResponseJson, bool verifyFhirVersion = true)
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(capabilityStatementResponseJson, Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com"),
            };

            var patientResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(patientResponseJson, Encoding.UTF8, "application/json"),
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

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = verifyFhirVersion }, mock.Object);
            await client.ReadAsync<Patient>("Patient/1");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTest()
        {
            // the usual use case
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + ModelInfo.Version + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            Func<System.Threading.Tasks.Task> act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);

            await act
               .Should().NotThrowAsync();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTestUnknownVersion()
        {
            // Verify server version with an unknow version
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": ""0.0.0""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            Func<System.Threading.Tasks.Task> act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);
            await act
                .Should().ThrowAsync<NotSupportedException>()
                .WithMessage($"This client supports FHIR version {ModelInfo.Version} but the server uses version 0.0.0");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionTestNoVersion()
        {
            // Verify server version with no version returned
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            Func<System.Threading.Tasks.Task> act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);
            await act
                .Should().ThrowAsync<NotSupportedException>()
                .WithMessage("This CapabilityStatement of the server doesn't state its FHIR version");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task NoVerifyFhirVersionWithIncorrectPatient()
        {
            // No server version check, but incorrect patient. This could be a wrong FHIR version. So we check the extra appended message
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + ModelInfo.Version + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:"", ""unknownMember"": ""value""}";
            Func<System.Threading.Tasks.Task> act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson, false);
            await act
                .Should().ThrowAsync<StructuralTypeException>()
                .Where(e => e.Message.EndsWith("FHIR server with the correct FHIR version."));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task VerifyFhirVersionWithIncorrectPatient()
        {
            // Server version check with an incorrect patient. So the error is legit
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + ModelInfo.Version + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:"", ""unknownMember"": ""value""}";
            Func<System.Threading.Tasks.Task> act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson, true);
            await act
                .Should().ThrowAsync<StructuralTypeException>()
                .Where(e => !e.Message.EndsWith("FHIR server with the correct FHIR version."));
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

        [TestMethod]
        [ExpectedException(typeof(FhirOperationException))]
        public async System.Threading.Tasks.Task TestUnauthorizedWithANonFhirJsonBody()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(@"{""foo"": ""bar"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry")
            };

            var authValue = AuthenticationHeaderValue.Parse("foo");
            response.RequestMessage.Headers.Authorization = authValue;

            mock
             .Protected()
                     .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/Patient?name=henry")),
                        ItExpr.IsAny<CancellationToken>())
                     .ReturnsAsync(response);

            using var client = new FhirClient("http://example.com", new FhirClientSettings { VerifyFhirVersion = false }, mock.Object);
            client.RequestHeaders.Authorization = authValue;

            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestOperationWithEmptyBody()
        {
            var mock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Parameters"",  ""parameter"": [ { ""name"": ""result"", ""valueString"": ""connected""}]  }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/$ping")
            };


            mock
             .Protected()
                     .Setup<System.Threading.Tasks.Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri("http://example.com/fhir/$ping")),
                        ItExpr.IsAny<CancellationToken>())
                     .ReturnsAsync(response);

            using var client = new FhirClient("http://example.com/fhir/", new FhirClientSettings { VerifyFhirVersion = false }, mock.Object);

            var parameters = await client.OperationAsync(new Uri("http://example.com/fhir/$ping")) as Parameters;

            parameters.Parameter.FirstOrDefault().Value.Should().BeOfType<FhirString>().Which.Value.Should().Be("connected");

        }
    }
}