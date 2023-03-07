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
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequesterTests
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
                FhirSerializationEngine.Poco(TESTINSPECTOR),
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
        [DataRow(Prefer.ReturnRepresentation, ReturnPreference.Representation, false)]
        [DataRow(Prefer.OperationOutcome, ReturnPreference.OperationOutcome, false)]
        [DataRow(Prefer.ReturnMinimal, ReturnPreference.Minimal, false)]
        [DataRow(Prefer.RespondAsync, null, true)]
        [DataRow(null, null, false)]
        public void TestConvertPreferredReturn(Prefer? setting, ReturnPreference? pref, bool isAsync)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var settings = new FhirClientSettings { PreferredReturn = setting };
            settings.ReturnPreference.Should().Be(pref);
            settings.UseAsync.Should().Be(isAsync);
#pragma warning restore CS0618 // Type or member is obsolete
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
#pragma warning disable CS0618 // Type or member is obsolete
            var settings = new FhirClientSettings { ReturnPreference = pref, UseAsync = isAsync };
            settings.PreferredReturn.Should().Be(setting);
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

            if (hasLu) pat.Meta = new Meta { LastUpdated = DateTimeOffset.Now };

            var entryRequest = makeMessage(resource: pat, method: Bundle.HTTPVerb.POST);
            entryRequest.Content!.Headers.ContentType!.ToString().Should().Be(ContentType.BuildContentType(ResourceFormat.Xml, TESTVERSION));
            var xml = await entryRequest.Content.ReadAsStringAsync();

            xml.Should().StartWith("<Patient");
            if (!hasLu)
                xml.Should().NotContain("<lastUpdated>");
            else
                xml.Should().Contain("<lastUpdated");
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

        #region STILL TODO - EntryResponse To TypedEntryResponse


        [TestMethod]
        public async Tasks.Task TestToTypedEntryResponse()
        {
            var xml = "<Patient xmlns=\"http://hl7.org/fhi\"><active value=\"true\" /></Patient>";
            var response = new EntryResponse
            {
                ContentType = "text/xml; charset=us-ascii",
                Etag = "Test-Etag",
                LastModified = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan()),
                Location = "Test-Location",
                ResponseUri = new Uri("http://www.myserver.com"),
                Status = "200",
                Headers = new Dictionary<string, string>() { { "Test-key", "Test-value" } },
                Body = Encoding.UTF8.GetBytes(xml),
            };

            var result = await response.ToTypedEntryResponseAsync(TESTINSPECTOR);

            var typedElementXml = await result.TypedElement.ToXmlAsync();
            Assert.AreEqual(xml, typedElementXml);
            Assert.AreEqual(response.ContentType, result.ContentType);
            Assert.AreEqual(response.Etag, result.Etag);
            Assert.AreEqual(response.LastModified, result.LastModified);
            Assert.AreEqual(response.Location, result.Location);
            Assert.AreEqual(response.ResponseUri, result.ResponseUri);
            Assert.AreEqual(response.Status, result.Status);
            Assert.AreEqual(response.GetBodyAsText(), result.GetBodyAsText());
            Assert.AreEqual(response.Headers["Test-key"], result.Headers["Test-key"]);
        }

        #endregion

        #region TypedEntryResponse To BundleEntryResponse

        [TestMethod]
        public async Tasks.Task TestTypedEntryResponseToBundle()
        {
            var xml = "<Patient xmlns=\"http://hl7.org/fhi\"><active value=\"true\" /></Patient>";
            var response = new EntryResponse
            {
                ContentType = "text/xml; charset=us-ascii",
                Etag = "Test-Etag",
                LastModified = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan()),
                Location = "Test-Location",
                ResponseUri = new Uri("http://www.myserver.com"),
                Status = "200",
                Headers = new Dictionary<string, string>() { { "Test-key", "Test-value" } },
                Body = Encoding.UTF8.GetBytes(xml),
            };
            var typedresponse = await response.ToTypedEntryResponseAsync(TESTINSPECTOR);

            var settings = new ParserSettings
            {
                AcceptUnknownMembers = false,
                AllowUnrecognizedEnums = false,
                DisallowXsiAttributesOnRoot = true,
                PermissiveParsing = false
            };

            var bundleresponse = typedresponse.ToBundleEntry(TESTINSPECTOR, settings);

            Assert.AreEqual(bundleresponse.Response.Etag, response.Etag);
            Assert.AreEqual(bundleresponse.Response.LastModified, response.LastModified);
            Assert.AreEqual(bundleresponse.Response.Status, response.Status);
            Assert.AreEqual(bundleresponse.Response.Location, response.Location);
            Assert.AreEqual(bundleresponse.Response.GetBody(), response.Body);
        }

        #endregion
    }
}

#nullable restore