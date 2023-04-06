#nullable enable

/*
 * Copyright(c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
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
        private static readonly string TESTVERSION = "3.0.1";
        private static readonly IFhirSerializationEngine ELEMENTENGINE = FhirSerializationEngine.ElementModel(TESTINSPECTOR, new());
        private static readonly IFhirSerializationEngine POCOENGINE = FhirSerializationEngine.Poco(TESTINSPECTOR);

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
        private HttpContent makeXmlContent(string? xml = null) => 
            new StringContent(xml ?? DEFAULT_XML, Encoding.UTF8, ContentType.XML_CONTENT_HEADER);
        private HttpResponseMessage makeXmlMessage(HttpStatusCode status = HttpStatusCode.OK, string? xml = null) => 
            new(status) { Content = makeXmlContent(xml), RequestMessage = new HttpRequestMessage(HttpMethod.Get, REQUEST_URI) };

        private const string DEFAULT_JSON = """{"resourceType":"Patient","active":true}""";

        private HttpContent makeJsonContent(string? json = null) =>
            new StringContent(json ?? DEFAULT_JSON, Encoding.UTF8, ContentType.JSON_CONTENT_HEADER);
        private HttpResponseMessage makeJsonMessage(HttpStatusCode status = HttpStatusCode.OK, string? json = null) =>
            new(status) { Content = makeJsonContent(json), RequestMessage = new HttpRequestMessage(HttpMethod.Get, REQUEST_URI) };

        [TestMethod]
        public async Tasks.Task SetAndExtractRelevantHeaders()
        {           
            var engine = FhirSerializationEngine.Poco(TESTINSPECTOR);
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

        private async Task check(string status, HttpResponseMessage response, IFhirSerializationEngine engine, ResourceFormat format, bool hasResource=true)
        {
            var components = await response.ExtractResponseData(engine);

            components.Response.Status.Should().Be(status);
            components.BodyData.Should().BeEquivalentTo(await response.Content.ReadAsByteArrayAsync());
            components.BodyText.Should().Be(await response.Content.ReadAsStringAsync());

            if (hasResource)
            {
                components.BodyResource.Should().NotBeNull();

                if(format == ResourceFormat.Xml)
                    engine.SerializeToXml(components.BodyResource!).Should().Be(components.BodyText);
                else
                    engine.SerializeToJson(components.BodyResource!).Should().Be(components.BodyText);

                components.BodyResource!.ResourceBase.Should().Be(REQUEST_URI);
            }
            else
                components.BodyResource.Should().BeNull();
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task GetResponseWithCorrectXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage();
            await check("200", response, engine, ResourceFormat.Xml);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task GetResponseWithCorrectJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage();
            await check("200", response, engine, ResourceFormat.Json);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithXmlButNotXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.Accepted, "this is not xml");
            await Assert.ThrowsExceptionAsync<UnsupportedBodyTypeException>( () => response.ExtractResponseData(engine) );
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleSuccessResponseWithJsonButNotJson(IFhirSerializationEngine engine)
        {
            var response = makeJsonMessage(HttpStatusCode.Accepted, "this is not json");
            await Assert.ThrowsExceptionAsync<UnsupportedBodyTypeException>(() => response.ExtractResponseData(engine));
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleFailureResponseWithXmlButNotXml(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.Forbidden, "this is not xml");
            await check("403", response, engine, ResourceFormat.Xml, false);
        }

        [TestMethod]
        [DynamicData(nameof(GetEngines), DynamicDataSourceType.Method)]
        public async Task HandleFailureResponseWithJsonButNotJson(IFhirSerializationEngine engine)
        {
            var response = makeXmlMessage(HttpStatusCode.Forbidden, "this is not xml");
            await check("403", response, engine, ResourceFormat.Json, false);
        }

        public static IEnumerable<object[]> GetEngines()
        {
            yield return new object[] { POCOENGINE };
            yield return new object[] { ELEMENTENGINE };
        }
     
        public void GetSuccessResponseWithInvalidXml()
        {
        }

        public void GetSuccessResponseWithInvalidJson()
        {
        }

        public void GetSuccessResponseWithNonFhirPayload()
        {
        }

    }
}

#nullable restore