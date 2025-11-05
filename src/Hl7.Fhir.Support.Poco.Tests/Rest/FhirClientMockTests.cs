#nullable enable

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
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
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class FhirClientMockTest
    {
        private static readonly ModelInspector TESTINSPECTOR = ModelInfo.ModelInspector;
        private static readonly string TESTVERSION = "3.0.1";

        private static async Task mockVersionResponse(string capabilityStatementResponseJson, string patientResponseJson, bool verifyFhirVersion = true)
        {
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
            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/metadata?_summary=true"))
                .Send(patientResponse, h => h.RequestUri == new Uri("http://example.com/Patient/1"))
                .AsClient(s => s.VerifyFhirVersion = verifyFhirVersion);

            await client.ReadAsync<Patient>("Patient/1");
        }

        [TestMethod]
        public async Task VerifyFhirVersionTest()
        {
            // the usual use case
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + TESTVERSION + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            var act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);

            await act
               .Should().NotThrowAsync();
        }

        [TestMethod]
        public async Task VerifyFhirVersionTestUnknownVersion()
        {
            // Verify server version with an unknow version
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": ""0.0.0""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            var act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);
            await act
                .Should().ThrowAsync<NotSupportedException>()
                .WithMessage($"This client supports FHIR version {TESTVERSION} but the server uses version 0.0.0");
        }

        [TestMethod]
        public async Task VerifyFhirVersionTestNoVersion()
        {
            // Verify server version with no version returned
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:""}";
            var act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson);
            await act
                .Should().ThrowAsync<NotSupportedException>()
                .WithMessage("This CapabilityStatement of the server doesn't state its FHIR version");
        }

        [TestMethod]
        public async Task NoVerifyFhirVersionWithIncorrectPatient()
        {
            // No server version check, but incorrect patient. This could be a wrong FHIR version. So we check the extra appended message
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + TESTVERSION + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:"", ""unknownMember"": ""value""}";
            var act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson, false);
            await act
                .Should().ThrowAsync<StructuralTypeException>()
                .Where(e => e.Message.EndsWith("FHIR server with the correct FHIR version."));
        }

        [TestMethod]
        public async Task VerifyFhirVersionWithIncorrectPatient()
        {
            // Server version check with an incorrect patient. So the error is legit
            var capabilityStatementJson = @"{""resourceType"": ""CapabilityStatement"",  ""id"": ""example:"", ""fhirVersion"": """ + TESTVERSION + @"""}";
            var patientResponseJson = @"{""resourceType"": ""Patient"",  ""id"": ""example:"", ""unknownMember"": ""value""}";
            var act = () => mockVersionResponse(capabilityStatementJson, patientResponseJson, true);
            await act
                .Should().ThrowAsync<StructuralTypeException>()
                .Where(e => !e.Message.EndsWith("FHIR server with the correct FHIR version."));
        }

        [TestMethod]
        public async Task LocationHeaderTest()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry"),
            };

            response.Headers.Add("Location", "https://example.com/fhir/Bundle/example");

            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/Patient?name=henry"))
                .AsClient();
            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });

            client.LastResult!.Location.Should().Be("https://example.com/fhir/Bundle/example");
            patient!.ResourceBase.Should().Be(new Uri("https://example.com/fhir/"));
        }

        [TestMethod]
        [DataRow(true, DisplayName = "Use FhirVersion in Accept header")]
        [DataRow(false, DisplayName = "Don't use FhirVersion in Accept header")]
        public async Task AcceptHeaderTest(bool useFhirVersionHeader)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry"),
            };

            using var client = new SubstituteBuilder()
                .Send(response,
                    h => h.RequestUri == new Uri("http://example.com/Patient?name=henry") &&
                        findInAcceptHeader(h.Headers.Accept, "fhirVersion", useFhirVersionHeader))
                .AsClient(s => { s.VerifyFhirVersion = false; s.UseFhirVersionInAcceptHeader = useFhirVersionHeader; });

            var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });
        }

        private static bool findInAcceptHeader(HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> acceptHeader, string headerName, bool exists)
            => acceptHeader.SelectMany(a => a.Parameters).Any(p => p.Name == headerName) == exists;

        public static IEnumerable<object?[]> GetData()
        {
            yield return new object[] { "http://example.com/Patient/example/_history", "HistoryAsync", "Patient/example" };
            yield return new object[] { "http://example.com/Patient/example/_history", "HistoryAsync", new Uri("http://example.com/Patient/example") };
            yield return new object[] { "http://example.com/Patient/_history", "TypeHistoryAsync", "Patient" };
            yield return new object?[] { "http://example.com/_history", "WholeSystemHistoryAsync", null };
        }

        [TestMethod]
        [DynamicData(nameof(GetData))]
        public void HistoryContainsNoSummaryParameter(string expectedRequest, string methodName, object? parameter)
        {
            Uri expectedRequestUri = new(expectedRequest);
            var requests = new List<HttpRequestMessage>();

            using var client = setupClient();

            var methods = typeof(BaseFhirClient).GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var method = parameter is null
                ? methods.SingleOrDefault(m => m.Name == methodName)
                : methods.SingleOrDefault(m => m.Name == methodName && m.GetParameters().First().ParameterType == parameter.GetType());

            method.Should().NotBeNull($"{methodName} should be a method of FhirClient");

            var p = parameter is null ? Enumerable.Empty<object?>() : new[] { parameter };
            p = p.Concat(method!.GetParameters().Skip(parameter is null ? 0 : 1).Select(param => param.HasDefaultValue ? param.DefaultValue : null));
            method.Invoke(client, p.ToArray());

            requests.Should().OnlyContain(req => req.RequestUri == expectedRequestUri);

            BaseFhirClient setupClient()
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""resourceType"": ""Bundle"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"), // not interested in the Content
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, expectedRequestUri),
                };

                return new SubstituteBuilder()
                    .Send(response, r => requests.Add(r))
                    .AsClient(baseUri: new(expectedRequestUri.GetLeftPart(UriPartial.Authority)));
            }
        }

        [TestMethod]
        public async Task TestUnauthorizedWithANonFhirJsonBody()
        {
            await Assert.ThrowsAsync<FhirOperationException>(async () =>
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent(@"{""foo"": ""bar"",  ""id"": ""example:""}", Encoding.UTF8, "application/json"),
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://example.com/Patient?name=henry")
                };

                var authValue = AuthenticationHeaderValue.Parse("foo");
                response.RequestMessage.Headers.Authorization = authValue;

                using var client = new SubstituteBuilder()
                    .Send(response, h => h.RequestUri == new Uri("http://example.com/Patient?name=henry"))
                    .AsClient();
                client.RequestHeaders!.Authorization = authValue;

                var patient = await client.SearchAsync<Patient>(new string[] { "name=henry" });
            });
        }

        [TestMethod]
        public async Task TestOperationWithEmptyBody()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Parameters"",  ""parameter"": [ { ""name"": ""result"", ""valueString"": ""connected""}]  }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/$ping")
            };

            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/fhir/$ping"))
                .AsClient();

            var parameters = await client.OperationAsync(new Uri("http://example.com/fhir/$ping")) as Parameters;

            parameters!.Parameter.FirstOrDefault()!.Value.Should().BeOfType<FhirString>().Which.Value.Should().Be("connected");
        }

        [TestMethod]
        public async Task TestProcessMessage()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"" }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/$process-message")
            };

            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/$process-message"))
                .AsClient();

            var message = new TransactionBuilder("http://example.com/", Bundle.BundleType.Message).ToBundle();

            var bundle = await client.ProcessMessageAsync(message);

            bundle.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestProcessMessageParameters()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""resourceType"": ""Bundle"" }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/$process-message")
            };

            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/$process-message?async=true&response-url=http%3A%2F%2Fresponseurl.com"))
                .AsClient();

            var message = new TransactionBuilder("http://example.com/", Bundle.BundleType.Message).ToBundle();

            var bundle = await client.ProcessMessageAsync(message, true, "http://responseurl.com");

            bundle.Should().NotBeNull();
        }

        [TestMethod]
        public async Task WillFetchFullRepresentation()
        {
            var patientInstanceUri = new Uri("http://example.com/fhir/Patient/3141");
            // Send back an empty body with a location on a post, to force the client (configured to need the full representation)
            // to go back out and fetch the resource.
            var postResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/Patient")
            };
            postResponse.Headers.Location = patientInstanceUri;

            var patientResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("""{"resourceType": "Patient",  "id": "example"}""", Encoding.UTF8, ContentType.JSON_CONTENT_HEADER),
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, patientInstanceUri)
            };

            using var client = new SubstituteBuilder()
                .Send(postResponse)
                .Send(patientResponse, m => m.Method == HttpMethod.Get && m.RequestUri == patientInstanceUri)
                .AsClient(s => s.ReturnPreference = ReturnPreference.Representation, baseUri: new("http://example.com/fhir/"));

            var pat = await client.CreateAsync(new Patient { Id = "example" });
            pat!.Id.Should().Be("example");
        }

        [TestMethod]
        public async Task WillThrowWhenUnexpectedResourceTypeReceived()
        {
            using var client = sendBack("Organization");
            var act = () => client.ReadAsync<Patient>("Patient/example");
            await act.Should().ThrowAsync<FhirOperationException>().WithMessage("*expected a body of type Patient*");
        }

        [TestMethod]
        public async Task WillReturnOperationOutcomeOnOOEndpoint()
        {
            using var client = sendBack("OperationOutcome");
            var oo = await client.ReadAsync<OperationOutcome>("OperationOutcome/example");
            oo!.Id.Should().Be("example");
        }

        [TestMethod]
        public async Task WillReturnOperationOutcomeOnOtherEndpoint()
        {
            using var client = sendBack("OperationOutcome");

            var oo = await client.ReadAsync<Patient>("Patient/example");
            oo.Should().BeNull();
            client.LastBodyAsResource.Should().BeOfType<OperationOutcome>().Which.Id.Should().Be("example");
            client.LastResult!.Outcome.Should().BeOfType<OperationOutcome>().Which.Id.Should().Be("example");
        }


        [TestMethod]
        public async Task TestOperationResponseCodes()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(@"{""resourceType"": ""Parameters"",  ""parameter"": [ { ""name"": ""result"", ""valueString"": ""connected""}]  }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/$ping")
            };

            using var client = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/fhir/$ping"))
                .AsClient();

            var parameters = await client.OperationAsync(new Uri("http://example.com/fhir/$ping")) as Parameters;
            client.LastResult?.Status.Should().Be("201");

            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Accepted,
                Content = new StringContent(@"{""resourceType"": ""Parameters"",  ""parameter"": [ { ""name"": ""result"", ""valueString"": ""connected""}]  }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/$ping")
            };

            using var client2 = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/fhir/$ping"))
                .AsClient();

            parameters = await client2.OperationAsync(new Uri("http://example.com/fhir/$ping")) as Parameters;
            client2.LastResult?.Status.Should().Be("202");


            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(@"{""resourceType"": ""Parameters"",  ""parameter"": [ { ""name"": ""result"", ""valueString"": ""connected""}]  }", Encoding.UTF8, "application/json"),
                RequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://example.com/fhir/$ping")
            };

            using var client3 = new SubstituteBuilder()
                .Send(response, h => h.RequestUri == new Uri("http://example.com/fhir/$ping"))
                .AsClient();

            var function = () => client3.OperationAsync(new Uri("http://example.com/fhir/$ping"));
            await function.Should().ThrowAsync<FhirOperationException>().WithMessage("Operation was unsuccessful because of a client error (NotFound). Body contains a Parameters.");

        }

        private static BaseFhirClient sendBack(string resourceType)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://example.com/fhir/{resourceType}/example"),
                Content = new StringContent($$"""{"resourceType": "{{resourceType}}",  "id": "example"}""", Encoding.UTF8, ContentType.JSON_CONTENT_HEADER),
            };

            return new SubstituteBuilder().Send(response).AsClient(baseUri: new("http://example.com/fhir/"));
        }

        [TestMethod]
        public async Task TestCanMockFhirClient()
        {
            var mock = Substitute.For<BaseFhirClient>(new object[] { new Uri("http://example.org"), TESTINSPECTOR, FhirClientSettings.CreateDefault() });
            _ = await mock.ReadAsync<Patient>("http://example.org/fhir");
            await mock.Received(1).ReadAsync<Patient>(Arg.Any<string>());
        }

        [TestMethod]
        public void TestFhirClientSettings()
        {
            var fhirClient1 = new BaseFhirClient(
                new Uri("http://example.org"), TESTINSPECTOR, new FhirClientSettings { SerializationEngine = FhirSerializationEngineFactory.Strict(TESTINSPECTOR) }
            );
            
            MethodInfo? serializationEngineField = fhirClient1.GetType().GetMethod("getSerializationEngine", BindingFlags.NonPublic | BindingFlags.Instance);
            serializationEngineField.Should().NotBeNull();

            (serializationEngineField!.Invoke(fhirClient1, []) is PocoSerializationEngine).Should().BeTrue();

            var fhirClient2 = new BaseFhirClient(
                new Uri("http://example.org"), TESTINSPECTOR)
            ;
            
            (serializationEngineField!.Invoke(fhirClient2, []) is ElementModelSerializationEngine).Should().BeTrue();
            
            fhirClient2.Settings.SerializationEngine = FhirSerializationEngineFactory.Strict(TESTINSPECTOR);
            
            (serializationEngineField!.Invoke(fhirClient2, []) is PocoSerializationEngine).Should().BeTrue();
        }


        [TestMethod]
        public async Task TestCanCancelInteraction()
        {
            var mock = Substitute.For<SubstituteBuilder.TestHttpMessageHandler>();
            mock.SendAsyncPublic(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(args => blocker((CancellationToken)args[1]));

            bool isBlocking = false;

            async Task<HttpResponseMessage> blocker(CancellationToken ct)
            {
                isBlocking = true;
                await Task.Delay(2000, ct);

                Assert.Fail("Operation was not cancelled - it should never have gotten here.");

                // Unreachable code
                return new HttpResponseMessage();
            }

            using var client = new BaseFhirClient(new("http://example.com/fhir/"), mock, TESTINSPECTOR, new FhirClientSettings { ExplicitFhirVersion = TESTVERSION, VerifyFhirVersion = false });

            var cts = new CancellationTokenSource();

            // Start the task and wait until it is "blocking"
            var blockingTask = client.OperationAsync(new Uri("http://example.com/fhir/$ping"), ct: cts.Token);
            while (!isBlocking);

            // now cancel it.
            cts.Cancel();

            var act = async () => await blockingTask;
            await act.Should().ThrowAsync<OperationCanceledException>();
        }

        [TestMethod]
        public async Task TestCustomJsonPatch()
        {
            var body = """
                       [
                       	{
                       		"path": "/name/0/id",
                       		"op": "test",
                       		"value": "12804999"
                       	},
                       	{
                       		"path": "/name/0/given",
                       		"op": "replace",
                       		"value": [
                       			"Beulah",
                       			"Z"
                       		]
                       	}
                       ]
                       """; // A JSON Patch operation\

            var handlerMock = Substitute.For<SubstituteBuilder.TestHttpMessageHandler>();
            handlerMock.SendAsyncPublic(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(new HttpResponseMessage());
            
            var client = new BaseFhirClient(new ("http://example.com/fhir/"), handlerMock, TESTINSPECTOR, new FhirClientSettings { ExplicitFhirVersion = TESTVERSION, VerifyFhirVersion = false });

            _ = await client.PatchAsync<Patient>("1", body, ResourceFormat.Json);

            await handlerMock.Received(1).SendAsyncPublic(
                Arg.Is<HttpRequestMessage>(req =>
                    req.Content!.Headers.ContentType!.ToString() == "application/json-patch+json" &&
                    req.Content.ReadAsStringAsync().Result == body &&
                    req.RequestUri!.ToString().Contains("_format=json")
                    ),
                Arg.Any<CancellationToken>());
        }
    }
}

#nullable restore