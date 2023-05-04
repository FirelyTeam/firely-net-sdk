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
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequestMessageTests
    {
        private static readonly Uri ENDPOINT = new("http://myserver.org/fhir/");
        private static readonly ModelInspector TESTINSPECTOR = ModelInspector.ForType(typeof(TestPatient));
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

#pragma warning disable CS0618 // Type or member is obsolete
        [TestMethod]
        [DataRow(Prefer.ReturnRepresentation, ReturnPreference.Representation, false)]
        [DataRow(Prefer.OperationOutcome, ReturnPreference.OperationOutcome, false)]
        [DataRow(Prefer.ReturnMinimal, ReturnPreference.Minimal, false)]
        [DataRow(Prefer.RespondAsync, null, true)]
        [DataRow(null, null, false)]
        public void TestConvertPreferredReturn(Prefer? setting, ReturnPreference? pref, bool isAsync)
        {
            var settings = new FhirClientSettings { PreferredReturn = setting };
            settings.ReturnPreference.Should().Be(pref);
            settings.UseAsync.Should().Be(isAsync);
        }

        [TestMethod]
        [DataRow(null, false, null)]
        [DataRow(null, true, Prefer.RespondAsync)]
        [DataRow(ReturnPreference.Minimal, false, Prefer.ReturnMinimal)]
        [DataRow(ReturnPreference.Representation, false, Prefer.ReturnRepresentation)]
        [DataRow(ReturnPreference.OperationOutcome, false, Prefer.OperationOutcome)]
        [DataRow(ReturnPreference.OperationOutcome, true, Prefer.RespondAsync)]
        public void TestConvertReturnPreference(ReturnPreference? pref, bool isAsync, Prefer? setting)
        {
            var settings = new FhirClientSettings { ReturnPreference = pref, UseAsync = isAsync };
            settings.PreferredReturn.Should().Be(setting);
        }
#pragma warning restore CS0618 // Type or member is obsolete

        [TestMethod]
        [DataRow(false, DecompressionMethods.None)]
        [DataRow(true, DecompressionMethods.GZip)]
        public void ConvertCompressionRequestBody(bool compress, DecompressionMethods method)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var settings = new FhirClientSettings { CompressRequestBody = compress };
#pragma warning restore CS0618 // Type or member is obsolete
            settings.RequestBodyCompressionMethod.Should().Be(method);
        }

        [TestMethod]
        [DataRow(DecompressionMethods.None, false)]
        [DataRow(DecompressionMethods.GZip, true)]
        [DataRow(DecompressionMethods.Deflate, true)]
        public void ConvertRequestBodyCompression(DecompressionMethods method, bool compress)
        {
            var settings = new FhirClientSettings { RequestBodyCompressionMethod = method };
#pragma warning disable CS0618 // Type or member is obsolete
            settings.CompressRequestBody.Should().Be(compress);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [TestMethod]
        public void TestRequestUrl()
        {
            var url = new Uri(ENDPOINT, "test");

            var request = makeMessage();
            request.RequestUri!.Should().Be(url);

            var settings = new FhirClientSettings { UseFormatParameter = true };
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
            var settings = new FhirClientSettings { PreferredFormat = fmt };
            var request = makeMessage(settings: settings, method: Bundle.HTTPVerb.POST);
            request.Headers.Accept.Single().ToString().Should().Be(ContentType.BuildContentType(fmt, TESTVERSION));
            request.Headers.AcceptEncoding.Should().BeEmpty();

            settings.PreferCompressedResponses = true;
            request = makeMessage(settings: settings, method: Bundle.HTTPVerb.POST);
            request.Headers.Accept.Single().ToString().Should().Be(ContentType.BuildContentType(fmt, TESTVERSION));
            request.Headers.AcceptEncoding.Select(h => h.Value).Should().BeEquivalentTo("gzip", "deflate");
        }

        [TestMethod]
        public async Task TestBinaryBody()
        {
            var bin = new Binary
            {
#if STU3
                    Data = Encoding.UTF8.GetBytes("test body"),
#else
                Content = Encoding.UTF8.GetBytes("test body"),
#endif
                ContentType = "text/plain"
            };

            var entryRequest = makeMessage(resource: bin, interaction: InteractionType.Create);
            Assert.IsNotNull(entryRequest.Content);
            Assert.AreEqual(bin.ContentType, entryRequest.Content.Headers.ContentType!.MediaType);
            Assert.AreEqual("test body", await entryRequest.Content.ReadAsStringAsync());
        }


        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task TestResourceBody(bool hasLu)
        {
            var pat = new TestPatient
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
            var pat = new TestPatient
            {
                Active = true,
            };

            var settings = new FhirClientSettings {  RequestBodyCompressionMethod = DecompressionMethods.GZip };
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