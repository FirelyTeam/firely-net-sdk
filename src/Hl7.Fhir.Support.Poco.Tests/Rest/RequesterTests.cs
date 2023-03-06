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
using System.Linq;
using System.Net.Http;
using System.Text;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequesterTests
    {
        private static readonly Uri ENDPOINT = new("http://myserver.org/fhir");
        private static Bundle.RequestComponent newEntry() => new()
        {
            Url = "test",
            Method = Bundle.HTTPVerb.GET,
        };

        private FhirClientSettings newSettings() => new()
        {
            PreferredParameterHandling = SearchParameterHandling.Lenient,
            ReturnPreference = ReturnPreference.Minimal,
            PreferredFormat = ResourceFormat.Json,
            UseFormatParameter = false
        };

        private static IFhirSerializationEngine getSerializationEngine(ParserSettings settings = null) =>
            FhirSerializationEngine.ElementModel(testInspector, settings);

        private static HttpRequestMessage toMessage(Bundle.RequestComponent entry, FhirClientSettings settings, Resource resource = null, InteractionType interaction = InteractionType.Search) =>
            entry.ToHttpRequestMessage(ENDPOINT, resource, interaction, settings.PreferredFormat, FhirSerializationEngine.Poco(testInspector), testVersion, settings.UseFormatParameter, settings.ReturnPreference, settings.UseAsync, settings.PreferredParameterHandling, settings.PreferCompressedResponses);

        #region EntryRequest To Httpclient
        [TestMethod]
        public void TestPreferSettingHttpClient()
        {
            var entry = newEntry();
            var settings = newSettings();

            var request = toMessage(entry, settings);
            Assert.AreEqual("return=minimal", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.UseAsync = true;
            request = toMessage(entry, settings);
            Assert.AreEqual("respond-async", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.ReturnPreference = ReturnPreference.OperationOutcome;
            request = toMessage(entry, settings);
            request.Headers.Where(h => h.Key == "Prefer").Should().BeEquivalentTo("handling=lenient", "return=operationoutcome");

            settings.UseAsync = true;
            request = toMessage(entry, settings);
            Assert.AreEqual("handling=lenient, respond-async", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.ReturnPreference = ReturnPreference.Representation;
            settings.PreferredParameterHandling = null;
            request = toMessage(entry, settings);
            Assert.IsNull(request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value);
        }


        // TODO: test PreferreteReturn <-> UseAsync/ReturnPreference

        [TestMethod]
        public void TestFormatParametersHttpClient()
        {
            var entry = newEntry();
            var settings = newSettings();

            settings.UseFormatParameter = true;
            var request = toMessage(entry, settings);
            Assert.IsTrue(request.RequestUri.ToString().Contains("_format=json"));
        }

        [TestMethod]
        public void TestEntryRequestHeadersHttpClient()
        {
            string testIfMatch = "W/\"23/\"";
            var testIfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan());
            string testIfNoneExists = "testifnneexists";
            string testIfNoneMatch = "W/\"28/\"";

            var entry = newEntry();
            entry.IfMatch = testIfMatch;
            entry.IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan());
            entry.IfNoneExist = testIfNoneExists;
            entry.IfNoneMatch = testIfNoneMatch;

            var settings = newSettings();

            var request = toMessage(entry, settings);
            Assert.AreEqual(testIfMatch, request.Headers.IfMatch.ToString());
            Assert.AreEqual(testIfModifiedSince.ToString(), request.Headers.IfModifiedSince.ToString());
            Assert.AreEqual(testIfNoneExists, request.Headers.Where(h => h.Key == "If-None-Exist").FirstOrDefault().Value.FirstOrDefault());
            Assert.AreEqual(testIfNoneMatch, request.Headers.IfNoneMatch.ToString());
        }

        [TestMethod]
        public void TestSetAgentHttpClient()
        {
            var entry = newEntry();
            var settings = newSettings();

            var request = toMessage(entry, settings);
            Assert.AreEqual(".NET FhirClient for FHIR testAgent", request.Headers.UserAgent.ToString());
        }

        #endregion       

        #region Bundle.EntryComponent To EntryRequest

        [TestMethod]
        public void TestBundleToEntryRequest()
        {
            var requestComponent = new Bundle.RequestComponent
            {
                Method = Bundle.HTTPVerb.GET,
                Url = "test/Url",
                IfMatch = "test-ifMatch",
                IfNoneExist = "test-ifNoneExists",
                IfNoneMatch = "test-ifNoneMatch",
                IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan())
            };

            var entry = newEntry();
            var settings = newSettings();
            var entryRequest = toMessage(entry, settings);

            Assert.AreEqual(requestComponent.Url, entryRequest.RequestUri);
            Assert.AreEqual(requestComponent.Method, entryRequest.Method);
            Assert.AreEqual(requestComponent.IfMatch, entryRequest.Headers.IfMatch);
            Assert.AreEqual(requestComponent.IfModifiedSince, entryRequest.Headers.IfModifiedSince);
            Assert.AreEqual(requestComponent.IfNoneExist, entryRequest.Headers.GetValues("IfNoneExist"));
            Assert.AreEqual(requestComponent.IfNoneMatch, entryRequest.Headers.IfNoneMatch);
            Assert.IsNull(entryRequest.Content);
        }

        [TestMethod]
        public void TestPatchBundleToEntryRequest()
        {
            var bundleRequest = new Bundle.RequestComponent
            {
#if R4
                    Method = Bundle.HTTPVerb.PATCH,
#else
                Method = Bundle.HTTPVerb.PUT,
#endif
                Url = "test/Url",
                IfMatch = "test-ifMatch",
                IfNoneExist = "test-ifNoneExists",
                IfNoneMatch = "test-ifNoneMatch",
                IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan())
            };


            var entry = newEntry();
            var settings = newSettings();
            var entryRequest = toMessage(entry, settings, interaction: InteractionType.Patch);

            Assert.IsNotNull(entryRequest);
            Assert.AreEqual(bundleRequest.Url, entryRequest.RequestUri);
            Assert.AreEqual(HTTPVerb.PATCH, entryRequest.Method);
            Assert.AreEqual(bundleRequest.IfMatch, entryRequest.Headers.IfMatch);
            Assert.AreEqual(bundleRequest.IfModifiedSince, entryRequest.Headers.IfModifiedSince);
            Assert.AreEqual(bundleRequest.IfNoneExist, entryRequest.Headers.GetValues("IfNoneExist"));
            Assert.AreEqual(bundleRequest.IfNoneMatch, entryRequest.Headers.IfNoneMatch);
            Assert.IsNull(entryRequest.Content);
        }

        [TestMethod]
        public void TestBundleToEntryRequestBody()
        {
            var bundleRequest = new Bundle.RequestComponent
            {
                Method = Bundle.HTTPVerb.GET
            };

            var bin = new Binary
            {
#if STU3
                    Data = Encoding.UTF8.GetBytes("test body"),
#else
                Content = Encoding.UTF8.GetBytes("test body"),
#endif
                ContentType = "test content type"
            };

            var entry = newEntry();
            var settings = newSettings();
            var entryRequest = toMessage(entry, settings, bin, interaction: InteractionType.Patch);
            Assert.IsNotNull(entryRequest.Content);
            Assert.AreEqual("test content type", entryRequest.Content.Headers.ContentType);
            Assert.AreEqual("test body", entryRequest.Content.ReadAsStringAsync());
        }

        #endregion

        #region EntryResponse To TypedEntryResponse


        private static readonly ModelInspector testInspector = ModelInspector.ForType(typeof(TestPatient));
        private static string testVersion = "3.0.1";

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

            var result = await response.ToTypedEntryResponseAsync(testInspector);

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
            var typedresponse = await response.ToTypedEntryResponseAsync(testInspector);

            var settings = new ParserSettings
            {
                AcceptUnknownMembers = false,
                AllowUnrecognizedEnums = false,
                DisallowXsiAttributesOnRoot = true,
                PermissiveParsing = false
            };

            var bundleresponse = typedresponse.ToBundleEntry(testInspector, settings);

            Assert.AreEqual(bundleresponse.Response.Etag, response.Etag);
            Assert.AreEqual(bundleresponse.Response.LastModified, response.LastModified);
            Assert.AreEqual(bundleresponse.Response.Status, response.Status);
            Assert.AreEqual(bundleresponse.Response.Location, response.Location);
            Assert.AreEqual(bundleresponse.Response.GetBody(), response.Body);
        }

        #endregion
    }
}
