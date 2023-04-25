#nullable enable

/*
 * Copyright(c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class ResponseMessageTests
    {
        private static readonly Uri ENDPOINT = new("http://myserver.org/fhir/");
        private static readonly ModelInspector TESTINSPECTOR = ModelInspector.ForType(typeof(TestPatient));
        private static readonly IFhirSerializationEngine ELEMENTENGINE = FhirSerializationEngineFactory.Legacy.Permissive(TESTINSPECTOR);
        private static readonly IFhirSerializationEngine POCOENGINE = FhirSerializationEngineFactory.Strict(TESTINSPECTOR);

        [TestMethod]
        public void CanRoundtripHeaders()
        {
            var msg = new HttpResponseMessage();
            msg.Headers.Location = new Uri("http://nu.nl");
            msg.Headers.AcceptRanges.Add("bytes");
            msg.Headers.AcceptRanges.Add("none");

            var target = new Bundle.ResponseComponent();
            target.SetHttpHeaders(msg.Headers);
            var headers = target.GetHttpHeaders();

            headers.Should().BeEquivalentTo(msg.Headers);
        }

        [TestMethod]
        public void GetJustContentType()
        {
            var content = new StringContent("Test");
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Xml, "4.1");
            content.GetContentType().Should().Be(ContentType.XML_CONTENT_HEADER);
        }

        [TestMethod]
        public void GetVersionFromETagReturnsVersionOnly()
        {
            var msg = new HttpResponseMessage();
            msg.Headers.ETag = new EntityTagHeaderValue("\"314\"", true);

            msg.GetVersionFromETag().Should().Be("314");
        }

        private const string DEFAULT_XML = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\" /></Patient>";
        private static readonly Uri REQUEST_URI = new("http://server.nl/fhir/SomeResource/1", UriKind.Absolute);
        private static HttpContent makeXmlContent(string? xml = null) =>
            new StringContent(xml ?? DEFAULT_XML, Encoding.UTF8, ContentType.XML_CONTENT_HEADER);
        private static HttpResponseMessage makeXmlMessage(HttpStatusCode status = HttpStatusCode.OK, string? xml = null) =>
            new(status) { Content = makeXmlContent(xml), RequestMessage = new HttpRequestMessage(HttpMethod.Get, REQUEST_URI) };

        private static HttpResponseMessage makeEmptyXmlMessage(HttpStatusCode status = HttpStatusCode.OK) =>
            new(status) { RequestMessage = new HttpRequestMessage(HttpMethod.Get, REQUEST_URI) };

        private const string DEFAULT_JSON = """{"resourceType":"Patient","active":true}""";

        private static HttpContent makeJsonContent(string? json = null) =>
            new StringContent(json ?? DEFAULT_JSON, Encoding.UTF8, ContentType.JSON_CONTENT_HEADER);
        private static HttpResponseMessage makeJsonMessage(HttpStatusCode status = HttpStatusCode.OK, string? json = null) =>
            new(status) { Content = makeJsonContent(json), RequestMessage = new HttpRequestMessage(HttpMethod.Get, REQUEST_URI) };

        [TestMethod]
        public async Tasks.Task SetAndExtractRelevantHeaders()
        {
            var engine = FhirSerializationEngineFactory.Strict(TESTINSPECTOR);
            var xmlContent = makeXmlContent();
            xmlContent.Headers.LastModified = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan());

            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.myserver.com");
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = request,
                Content = xmlContent,
            };

            var etag = "W/\"Test-Etag\"";
            response.Headers.ETag = EntityTagHeaderValue.Parse(etag);
            response.Headers.Location = new Uri("http://nu.nl");
            response.Headers.TryAddWithoutValidation("Test-key", "Test-value");

            var extracted = await response.ExtractResponseData(engine);

            extracted.BodyText.Should().Be(DEFAULT_XML);
            engine.SerializeToXml(extracted.BodyResource!).Should().Be(DEFAULT_XML);
            extracted.Response.Etag.Should().Be(etag);
            extracted.Response.LastModified.Should().Be(response.Content.Headers.LastModified);
            extracted.Response.Location.Should().Be(response.Headers.Location.OriginalString);
            response.GetRequestUri().Should().Be(response.RequestMessage.RequestUri);
            Enum.Parse<HttpStatusCode>(extracted.Response.Status).Should().Be(response.StatusCode);
            extracted.Response.GetHttpHeaders().GetValues("Test-key").Should().BeEquivalentTo("Test-value");
        }

        [TestMethod]
        public async Task GetEmptyResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict);
            var components = await response.ExtractResponseData(POCOENGINE);

            components.Response.Status.Should().Be("409");
            components.BodyData.Should().BeNull();
            components.BodyText.Should().BeNull();
            components.BodyResource.Should().BeNull();
        }

        private static async Task check(HttpResponseMessage response, IFhirSerializationEngine engine,
            bool hasResource = false, Type? expectedIssue = null, string? messagePattern = null, string? notMessagePattern = null)
        {
            var components = await response.ExtractResponseData(engine);
            await checkResult(response, components, engine, hasResource, expectedIssue, messagePattern, notMessagePattern);
        }

        private static async Task checkResult(HttpResponseMessage response, HttpContentParsers.ResponseData components,
            IFhirSerializationEngine engine,
            bool hasResource = false, Type? expectedIssue = null, string? messagePattern = null, string? notMessagePattern = null)
        {
            components.Response.Status.Should().Be(((int)response.StatusCode).ToString());

            var expectedData = await response.Content.ReadAsByteArrayAsync();
            if (expectedData.Length == 0)
                components.BodyData.Should().BeNull();
            else
                components.BodyData.Should().BeEquivalentTo(expectedData);

            var expectedText = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(expectedText))
                components.BodyText.Should().BeNull();
            else
                components.BodyText.Should().Be(expectedText);

            if (hasResource)
            {
                var format = ContentType.GetResourceFormatFromContentType(response.Content.GetContentType());
                components.BodyResource.Should().NotBeNull();

                if (format == ResourceFormat.Xml)
                    engine.SerializeToXml(components.BodyResource!).Should().Be(components.BodyText);
                else
                    engine.SerializeToJson(components.BodyResource!).Should().Be(components.BodyText);

                components.BodyResource!.ResourceBase.Should().Be(REQUEST_URI);
            }
            else
                components.BodyResource.Should().BeNull();

            if (expectedIssue is not null)
            {
                components.Issue.Should().BeOfType(expectedIssue);
                if (messagePattern is not null) components.Issue!.Message.Should().MatchEquivalentOf(messagePattern);
                if (notMessagePattern is not null) components.Issue!.Message.Should().NotMatchEquivalentOf(notMessagePattern);
            }
            else
                components.Issue.Should().BeNull();
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task GetResponseWithCorrectXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage();
            await check(response, engine, hasResource: true);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithIncorrectXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(xml: """<Unknown><active value="true" /></Unknown>""");
            await check(response, engine, expectedIssue: typeof(DeserializationFailedException));

            response = makeXmlMessage(xml: """<Patient><activex value="true" /></Patient>""");
            await check(response, engine, expectedIssue: typeof(DeserializationFailedException));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task GetResponseWithCorrectJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage();
            await check(response, engine, hasResource: true);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithIncorrectJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage(json: """{ "resourceType": "UnknownResource" }""");
            await check(response, engine, expectedIssue: typeof(DeserializationFailedException));

            response = makeJsonMessage(json: """{ "resourceType": "Patient", "activex": 4 }""");
            await check(response, engine, expectedIssue: typeof(DeserializationFailedException));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithXmlButNotXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.Accepted, "this is not xml");
            await check(response, engine, expectedIssue: typeof(UnsupportedBodyTypeException));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithJsonButNotJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage(HttpStatusCode.Accepted, "this is not json");
            await check(response, engine, expectedIssue: typeof(UnsupportedBodyTypeException));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleFailureResponseWithXmlButNotXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.Forbidden, "this is not xml");
            await check(response, engine);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleFailureResponseWithJsonButNotJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage(HttpStatusCode.Forbidden, "this is not json");
            await check(response, engine);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithUnknown(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(xml: "this is not xml or json");
            response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
            await check(response, engine, expectedIssue: typeof(UnsupportedBodyTypeException));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleFailureResponseWithUnknown(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.InternalServerError, "this is not xml or json");
            response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
            await check(response, engine);
        }

        public static IEnumerable<object[]> GetEngines()
        {
            yield return new object[] { POCOENGINE };
            yield return new object[] { ELEMENTENGINE };
        }

        [TestMethod]
        public async Task TurnsNonSuccessIntoFOE()
        {
            var response = makeEmptyXmlMessage(status: HttpStatusCode.NotFound);
            await assertIssue<FhirOperationException>(response, "Operation was unsuccessful because of a client error*");
        }

        [TestMethod]
        public async Task TurnsUnexpectedStatusIntoFOE()
        {
            var response = makeEmptyXmlMessage(status: HttpStatusCode.Accepted);
            await assertIssue<FhirOperationException>(response, "Operation concluded successfully, but the return status * was unexpected");
        }

        [TestMethod]
        public async Task TurnsUnexpectedBodyTypeIntoFOE()
        {
            var response = makeXmlMessage(xml: "this is not xml");
            await assertIssue<FhirOperationException>(response, "Operation was unsuccessful, and returned status OK. " +
                "OperationOutcome:*but the body is not recognized as either xml or json.*");

            response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
            await assertIssue<FhirOperationException>(response, "Operation was unsuccessful, and returned status OK. " +
                "OperationOutcome:*returned a body with contentType 'text/plain', while a valid FHIR xml/json body type was expected*");
        }

        [TestMethod]
        public async Task TurnsNewParsingFailureIntoDFE()
        {
            var response = makeXmlMessage(xml: """<Unknown><active value="true" /></Unknown>""");
            await assertIssue<DeserializationFailedException>(response, "*Unknown type 'Unknown' found in root property*", engine: POCOENGINE, version: "1.0.0");
        }

        [TestMethod]
        public async Task TurnsLegacyParsingFailureIntoFE()
        {
            var response = makeXmlMessage(xml: """<Unknown><active value="true" /></Unknown>""");
            await assertIssue<FormatException>(response, "*Cannot locate type information for type 'Unknown'*", engine: ELEMENTENGINE, notmatch: "*with FHIR version 1.0.0*");
            await assertIssue<StructuralTypeException>(response, "*Cannot locate type information for type 'Unknown'*" +
                "Are you connected to a FHIR server with FHIR version 1.0.0*", version: "1.0.0", engine: ELEMENTENGINE);

            response = makeJsonMessage(json: """{ "resourceType": "Patient", "activex": 4 }""");
            await assertIssue<StructuralTypeException>(response, "*Encountered unknown element 'activex' at location*", engine: ELEMENTENGINE);
        }

        private static async Task assertIssue<T>(HttpResponseMessage response, string match, string? version = null,
            IFhirSerializationEngine? engine = null, string? notmatch = null)
                where T : Exception
        {
            var result = await BaseFhirClient.ValidateResponse(response, new[] { HttpStatusCode.OK }, engine ?? POCOENGINE, version);
            await checkResult(response, result, engine ?? POCOENGINE, hasResource: false, expectedIssue: typeof(T), messagePattern: match, notMessagePattern: notmatch);
        }
    }
}

#nullable restore