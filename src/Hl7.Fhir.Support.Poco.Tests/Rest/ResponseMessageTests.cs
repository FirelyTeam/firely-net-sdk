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
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        [TestMethod]
        public async Tasks.Task SetAndExtractRelevantHeaders()
        {
            var xml = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\" /></Patient>";
            var engine = FhirSerializationEngine.Poco(TESTINSPECTOR);
            var xmlContent = new StringContent(xml, Encoding.UTF8,  ContentType.XML_CONTENT_HEADER);
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

            var extracted = await response.ExtractResponseComponents(engine);

            extracted.BodyText.Should().Be(xml);
            engine.SerializeToXml(extracted.BodyResource!).Should().Be(xml);
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
            var components = await response.ExtractResponseComponents(POCOENGINE);

            components.Response.Status.Should().Be("409");
            components.BodyData.Should().BeNull();
            components.BodyText.Should().BeNull();
            components.BodyResource.Should().BeNull();
        }


        // probe isfhirxml
        // probe isfhirjson
        
        public void GetResponseWithCorrectXml()
        {

        }

        public void GetResponseWithCorrectJson()
        {
            // both parsers
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