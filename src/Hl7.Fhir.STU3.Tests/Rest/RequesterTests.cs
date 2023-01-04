/*
 * Copyright(c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequesterTests
    {
        private readonly Uri _endpoint = new("http://myserver.org/fhir");

        private static readonly EntryRequest ENTRY = new(HTTPVerb.GET, "test", InteractionType.Create);

        private static readonly FhirClientSettings SETTINGS =
            new()
            {
                PreferredParameterHandling = SearchParameterHandling.Lenient,
                PreferredReturn = Prefer.ReturnMinimal,
                PreferredFormat = ResourceFormat.Json,
                UseFormatParameter = false
            };

        #region EntryRequest To Httpclient
        [TestMethod]
        public void TestPreferSettingHttpClient()
        {
            var entry = ENTRY;
            var settings = SETTINGS;

            var request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual("return=minimal", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.PreferredReturn = Prefer.RespondAsync;
            request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual("respond-async", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.PreferredReturn = null;
            request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.IsNull(request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value);
        }

        [TestMethod]
        public void TestPreferSettingHttpClientWithSearch()
        {
            var settings = SETTINGS;

            var searchEntry = ENTRY with { Type = InteractionType.Search };
            settings.PreferredReturn = Prefer.OperationOutcome;
            var request = searchEntry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual("handling=lenient", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.PreferredReturn = Prefer.RespondAsync;
            request = searchEntry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual("handling=lenient, respond-async", request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value.FirstOrDefault());

            settings.PreferredReturn = Prefer.ReturnRepresentation;
            settings.PreferredParameterHandling = null;
            request = searchEntry.ToHttpRequestMessage(_endpoint, settings);
            Assert.IsNull(request.Headers.Where(h => h.Key == "Prefer").FirstOrDefault().Value);
        }

        [TestMethod]
        public void TestFormatParametersHttpClient()
        {
            var entry = ENTRY;
            var settings = SETTINGS;

            settings.UseFormatParameter = true;
            var request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.IsTrue(request.RequestUri.ToString().Contains("_format=json"));
        }

        [TestMethod]
        public void TestEntryRequestHeadersHttpClient()
        {
            var entry = ENTRY;

            string testIfMatch = "W/\"23/\"";
            var testIfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan());
            string testIfNoneExists = "testifnneexists";
            string testIfNoneMatch = "W/\"28/\"";

            entry.Headers = new EntryRequestHeaders
            {
                IfMatch = testIfMatch,
                IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan()),
                IfNoneExist = testIfNoneExists,
                IfNoneMatch = testIfNoneMatch
            };

            var settings = SETTINGS;

            var request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual(testIfMatch, request.Headers.IfMatch.ToString());
            Assert.AreEqual(testIfModifiedSince.ToString(), request.Headers.IfModifiedSince.ToString());
            Assert.AreEqual(testIfNoneExists, request.Headers.Where(h => h.Key == "If-None-Exist").FirstOrDefault().Value.FirstOrDefault());
            Assert.AreEqual(testIfNoneMatch, request.Headers.IfNoneMatch.ToString());
        }

        [TestMethod]
        public void TestSetAgentHttpClient()
        {
            var entry = ENTRY with { FhirVersion = "3.1415" };
            var settings = SETTINGS;

            var request = entry.ToHttpRequestMessage(_endpoint, settings);
            Assert.AreEqual(".NET FhirClient for FHIR 3.1415", request.Headers.UserAgent.ToString());
        }

        #endregion       

        #region Bundle.EntryComponent To EntryRequest

        [TestMethod]
        public async Tasks.Task TestBundleToEntryRequest()
        {
            var bundleComponent = new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.GET,
                    Url = "test/Url",
                    IfMatch = "test-ifMatch",
                    IfNoneExist = "test-ifNoneExists",
                    IfNoneMatch = "test-ifNoneMatch",
                    IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan())
                }
            };
            bundleComponent.AddAnnotation(InteractionType.Search);

            var entryRequest = await bundleComponent.ToEntryRequestAsync(SETTINGS, BaseFhirClient.GetDefaultElementModelSerializers(ModelInfo.ModelInspector), ModelInfo.Version);

            Assert.IsNotNull(entryRequest);
            Assert.AreEqual(bundleComponent.Request.Url, entryRequest.Url);
            Assert.AreEqual(bundleComponent.Request.Method, (Bundle.HTTPVerb)entryRequest.Method);
            Assert.AreEqual(bundleComponent.Request.IfMatch, entryRequest.Headers.IfMatch);
            Assert.AreEqual(bundleComponent.Request.IfModifiedSince, entryRequest.Headers.IfModifiedSince);
            Assert.AreEqual(bundleComponent.Request.IfNoneExist, entryRequest.Headers.IfNoneExist);
            Assert.AreEqual(bundleComponent.Request.IfNoneMatch, entryRequest.Headers.IfNoneMatch);
            Assert.AreEqual(InteractionType.Search, entryRequest.Type);
            Assert.IsNull(entryRequest.RequestBodyContent);
        }

        [TestMethod]
        public async Tasks.Task TestPatchBundleToEntryRequest()
        {
            var bundleComponent = new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.PUT,
                    Url = "test/Url",
                    IfMatch = "test-ifMatch",
                    IfNoneExist = "test-ifNoneExists",
                    IfNoneMatch = "test-ifNoneMatch",
                    IfModifiedSince = new DateTimeOffset(new DateTime(2012, 01, 01), new TimeSpan())
                }
            };
            bundleComponent.AddAnnotation(InteractionType.Patch);

            var entryRequest = await bundleComponent.ToEntryRequestAsync(SETTINGS, getSerializationEngine(), ModelInfo.Version);

            Assert.IsNotNull(entryRequest);
            Assert.AreEqual(bundleComponent.Request.Url, entryRequest.Url);
            Assert.AreEqual(HTTPVerb.PATCH, entryRequest.Method);
            Assert.AreEqual(bundleComponent.Request.IfMatch, entryRequest.Headers.IfMatch);
            Assert.AreEqual(bundleComponent.Request.IfModifiedSince, entryRequest.Headers.IfModifiedSince);
            Assert.AreEqual(bundleComponent.Request.IfNoneExist, entryRequest.Headers.IfNoneExist);
            Assert.AreEqual(bundleComponent.Request.IfNoneMatch, entryRequest.Headers.IfNoneMatch);
            Assert.AreEqual(InteractionType.Patch, entryRequest.Type);
            Assert.IsNull(entryRequest.RequestBodyContent);
        }

        private static IFhirSerializationEngine getSerializationEngine(ParserSettings settings=null) => BaseFhirClient.GetDefaultElementModelSerializers(ModelInfo.ModelInspector, settings);

        [TestMethod]
        public async Tasks.Task TestBundleToEntryRequestBody()
        {
            var bundleComponent = new Bundle.EntryComponent
            {
                Resource = new Binary
                {
                    Content = Encoding.UTF8.GetBytes("test body"),
                    ContentType = "test content type"
                },
                Request = new Bundle.RequestComponent
                {
                    //Method = Bundle.HTTPVerb.GET
                }
            };
            bundleComponent.AddAnnotation(InteractionType.Search);

            var entryRequest = await bundleComponent.ToEntryRequestAsync(SETTINGS, getSerializationEngine(), ModelInfo.Version);
            Assert.IsNotNull(entryRequest);
            Assert.IsNotNull(entryRequest.RequestBodyContent);
            Assert.AreEqual("test content type", entryRequest.ContentType);
            Assert.AreEqual("test body", Encoding.UTF8.GetString(entryRequest.RequestBodyContent));
        }

        #endregion

        #region EntryResponse To TypedEntryResponse

        [TestMethod]
        public async Tasks.Task TestToTypedEntryResponse()
        {
            var xml = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\" /></Patient>";
            var contentType = "text/xml; charset=us-ascii";
            var result = EntryToTypedEntryExtensions.DecodeBodyToResource(xml, contentType, getSerializationEngine());
            
            var typedElementXml = await result.ToXmlAsync();
            Assert.AreEqual(xml, typedElementXml);
        }

        #endregion

        #region TypedEntryResponse To BundleEntryResponse

        [TestMethod]
        public void TestTypedEntryResponseToBundle()
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

            var settings = new ParserSettings
            {
                AcceptUnknownMembers = false,
                AllowUnrecognizedEnums = false,
                DisallowXsiAttributesOnRoot = true,
                PermissiveParsing = false
            };

            var resourceBody = EntryToTypedEntryExtensions.DecodeBodyToResource(xml, response.ContentType, getSerializationEngine(settings));         
            var bundleresponse = response.ToBundleEntry(FhirRelease.STU3, resourceBody);

            Assert.AreEqual(bundleresponse.Response.Etag, response.Etag);
            Assert.AreEqual(bundleresponse.Response.LastModified, response.LastModified);
            Assert.AreEqual(bundleresponse.Response.Status, response.Status);
            Assert.AreEqual(bundleresponse.Response.Location, response.Location);
            Assert.AreEqual(bundleresponse.Response.GetBody(), response.Body);
        }

        #endregion
    }
}

