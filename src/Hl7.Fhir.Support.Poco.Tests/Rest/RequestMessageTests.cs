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
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequestMessageTests
    {
        private static readonly Uri ENDPOINT = new("http://myserver.org/fhir/");
        private static readonly ModelInspector TESTINSPECTOR = ModelInfo.ModelInspector;
        private static readonly IFhirSerializationEngine TESTENGINE = FhirSerializationEngineFactory.Strict(TESTINSPECTOR);
        private static readonly string TESTVERSION = "3.0.1";

        private static HttpRequestMessage makeMessage(
            FhirClientSettings? settings = null,
            Resource? resource = null,
            InteractionType interaction = InteractionType.Search,
            Bundle.HTTPVerb method = Bundle.HTTPVerb.GET,
            Bundle.RequestComponent? request = null)
        {
            var entryToConvert = new Bundle.EntryComponent
            {
                Request = request ?? new Bundle.RequestComponent()
                {
                    Url = "test",
                    Method = method,
                },
                Resource = resource
            };

            entryToConvert.SetAnnotation(interaction);
            settings ??= new();

            return entryToConvert.ToHttpRequestMessage(
                ENDPOINT,
                FhirSerializationEngineFactory.Strict(TESTINSPECTOR),
                TESTVERSION,
                settings);
        }

        [TestMethod]
        public void TestPreferSettingHttpClient()
        {
            var settings = new FhirClientSettings { ReturnPreference = ReturnPreference.Minimal };
            var request = build(InteractionType.Create);
            request.Headers.GetValues("Prefer").Should().BeEquivalentTo("return=minimal");

            settings.UseAsync = true;
            request = build(InteractionType.Create);
            request.Headers.GetValues("Prefer").Should().BeEquivalentTo("return=minimal", "respond-async");

            settings.PreferredParameterHandling = SearchParameterHandling.Lenient;
            request = build(InteractionType.Create);

            // because: "SearchParamHandling only relevant for search"
            request.Headers.GetValues("Prefer").Should().BeEquivalentTo("return=minimal", "respond-async");

            request = build(InteractionType.Search);
            // because: SearchParamHandling is relevant for search, return is not relevant for search, async is.
            request.Headers.GetValues("Prefer").Should().BeEquivalentTo("respond-async", "handling=lenient");

            HttpRequestMessage build(InteractionType interaction) => makeMessage(settings, interaction: interaction);
        }

        [TestMethod]
        public void TestRequestUrl()
        {
            var url = new Uri(ENDPOINT, "test");

            var request = makeMessage();
            request.RequestUri!.Should().Be(url);

            var settings = new FhirClientSettings { UseFormatParameter = true, BinaryReceivePreference = BinaryTransferBehaviour.UseResource };
            request = makeMessage(settings);
            request.RequestUri!.Should().Be(url + "?_format=xml");

            var entry = new Bundle.RequestComponent
            {
                Url = "http://nu.nl",
                Method = Bundle.HTTPVerb.GET
            };
            request = makeMessage(request: entry);
            request.RequestUri!.Should().Be(entry.Url);
        }

        [TestMethod]
        public void TestEntryRequestHeadersHttpClient()
        {
            string testIfMatch = "W/\"23/\"";
            var testIfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan());
            string testIfNoneExists = "testifnneexists";
            string testIfNoneMatch = "W/\"28/\"";

            var entry = new Bundle.RequestComponent
            {
                Url = "test",
                Method = Bundle.HTTPVerb.GET,
                IfMatch = testIfMatch,
                IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan()),
                IfNoneExist = testIfNoneExists,
                IfNoneMatch = testIfNoneMatch
            };

            var request = makeMessage(request: entry);
            request.Headers.IfMatch.Single().ToString().Should().Be(testIfMatch);
            request.Headers.IfModifiedSince.Should().Be(testIfModifiedSince);
            request.Headers.GetValues("If-None-Exist").Should().BeEquivalentTo(testIfNoneExists);
            request.Headers.IfNoneMatch.Single().ToString().Should().Be(testIfNoneMatch);

            entry.IfMatch = null;
            entry.IfModifiedSince = null;
            request = makeMessage(request: entry);
            request.Headers.IfMatch.Should().BeEmpty();
            request.Headers.IfModifiedSince.Should().BeNull();
        }

        [TestMethod]
        public void TestSetAgentHttpClient()
        {
            var request = makeMessage();
            request.Headers.UserAgent.Single().Product!.Name.Should().Be("firely-sdk-client");
        }

        [TestMethod]
        [DataRow(ResourceFormat.Xml)]
        [DataRow(ResourceFormat.Json)]
        public void SetAccept(ResourceFormat fmt)
        {
            var settings = new FhirClientSettings { PreferredFormat = fmt, BinaryReceivePreference = BinaryTransferBehaviour.UseResource };
            var request = makeMessage(settings: settings, method: Bundle.HTTPVerb.POST);
            request.Headers.Accept.Single().ToString().Should().Be(ContentType.BuildContentType(fmt, TESTVERSION, true));
            request.Headers.AcceptEncoding.Should().BeEmpty();

            settings.PreferCompressedResponses = true;
            request = makeMessage(settings: settings, method: Bundle.HTTPVerb.POST);
            request.Headers.Accept.Single().ToString().Should().Be(ContentType.BuildContentType(fmt, TESTVERSION, true));
            request.Headers.AcceptEncoding.Select(h => h.Value).Should().BeEquivalentTo("gzip", "deflate");
        }

        private static readonly Binary testBinary = new Binary
        {
#if STU3
                    Data = Encoding.UTF8.GetBytes("test body"),
#else
            Content = Encoding.UTF8.GetBytes("test body"),
#endif
            ContentType = "text/plain"
        };

        [TestMethod]
        public async Task TestBinaryAsBinary()
        {
            var entryRequest = makeMessage(new FhirClientSettings { BinarySendBehaviour = BinaryTransferBehaviour.UseData }, resource: testBinary, interaction: InteractionType.Create);
            Assert.IsNotNull(entryRequest.Content);
            Assert.AreEqual(testBinary.ContentType, entryRequest.Content.Headers.ContentType!.MediaType);
            Assert.AreEqual("test body", await entryRequest.Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task TestBinaryAsResource()
        {
            var entryRequest = makeMessage(new FhirClientSettings { BinarySendBehaviour = BinaryTransferBehaviour.UseResource }, resource: testBinary, interaction: InteractionType.Create);

            var resource = await entryRequest.Content!.ReadResourceFromMessage(TESTENGINE);
            var binary = resource.Should().BeOfType<Binary>().Subject;

            binary.ContentType.Should().Be("text/plain");
            (binary.Data ?? binary.Content).Should().BeEquivalentTo(testBinary.Content ?? testBinary.Data);
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task TestResourceBody(bool hasLu)
        {
            var pat = new Patient
            {
                Active = true,
            };

            var now = DateTimeOffset.Now;

            if (hasLu) pat.Meta = new Meta { LastUpdated = now };

            var entryRequest = makeMessage(resource: pat, method: Bundle.HTTPVerb.POST);
            entryRequest.Content!.Headers.ContentType!.ToString().Should().Be(ContentType.BuildContentType(ResourceFormat.Xml, TESTVERSION));
            var xml = await entryRequest.Content.ReadAsStringAsync();

            xml.Should().StartWith("<Patient");

            if (!hasLu)
                entryRequest.Content!.Headers.LastModified.Should().Be(null);
            else
                entryRequest.Content!.Headers.LastModified.Should().Be(now);
        }

        [TestMethod]
        public async Task TestBodyCompression()
        {
            var pat = new Patient
            {
                Active = true,
            };

            var settings = new FhirClientSettings { RequestBodyCompressionMethod = DecompressionMethods.GZip };
            var entryRequest = makeMessage(settings: settings, resource: pat, method: Bundle.HTTPVerb.POST);

            entryRequest.Content!.Headers.ContentType!.ToString().Should().Be(ContentType.BuildContentType(ResourceFormat.Xml, TESTVERSION));
            entryRequest.Content!.Headers.ContentEncoding.Single().Should().Be("gzip");

            using var compressed = await entryRequest.Content.ReadAsStreamAsync();
            using var uncompressed = new GZipStream(compressed, CompressionMode.Decompress, false);
            using var stream = new StreamReader(uncompressed);
            var xml = stream.ReadToEnd();
            xml.Should().StartWith("<Patient");
        }

        [TestMethod]
        public async Task TestParametersAsNormalCreate()
        {
            var pars = new Parameters();
            pars.Parameter.Add(new() { Name = "par1" });
            pars.Parameter.Add(new() { Name = "par2" });

            var entryRequest = makeMessage(resource: pars, method: Bundle.HTTPVerb.POST, interaction: InteractionType.Create);

            entryRequest.Content!.Headers.ContentType!.ToString().Should().Be(ContentType.BuildContentType(ResourceFormat.Xml, TESTVERSION));
            var xml = await entryRequest.Content.ReadAsStringAsync();

            xml.Should().StartWith("<Parameters");
        }

        [TestMethod]
        public async Task TestParametersAsPost()
        {
            var pars = new Parameters();
            pars.Parameter.Add(new() { Name = "par1", Value = new FhirBoolean(true) });
            pars.Parameter.Add(new() { Name = "par2" });
            pars.Parameter.Add(new() { Name = "par3", Value = new FhirString("Thee") });

            var entryRequest = makeMessage(resource: pars, method: Bundle.HTTPVerb.POST, interaction: InteractionType.Search);

            entryRequest.Content!.Headers.ContentType!.ToString().Should().Be(ContentType.FORM_URL_ENCODED);
            var url = await entryRequest.Content.ReadAsStringAsync();

            url.Should().Be("par1=true&par3=Thee");
        }

        [TestMethod]
        [DataRow(Bundle.HTTPVerb.GET, null, "GET")]
        [DataRow(Bundle.HTTPVerb.POST, null, "POST")]
        [DataRow(Bundle.HTTPVerb.DELETE, null, "DELETE")]
        [DataRow(Bundle.HTTPVerb.PATCH, null, "PATCH")]
        [DataRow(Bundle.HTTPVerb.PUT, null, "PUT")]
        [DataRow(Bundle.HTTPVerb.PUT, InteractionType.Create, "PUT")]
        [DataRow(Bundle.HTTPVerb.PUT, InteractionType.Transaction, "PUT")]
        [DataRow(Bundle.HTTPVerb.PUT, InteractionType.Patch, "PATCH")]
        [DataRow(Bundle.HTTPVerb.HEAD, InteractionType.Patch, "HEAD")]
        public void MethodConversion(Bundle.HTTPVerb verb, InteractionType interaction, string method)
        {
            var request = makeMessage(method: verb, interaction: interaction);
            request.Method.Method.Should().Be(method);
        }
    }
}

#nullable restore