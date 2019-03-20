/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Text;
using System.Net;
using static Hl7.Fhir.Model.DSTU2.Observation;
using Hl7.Fhir.Model.DSTU2;
using System.Linq;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequestsBuilderTests
    {
        [TestMethod]
        public void TestBuild()
        {
            var p = new Patient();
            var b = new RequestsBuilder("http://myserver.org/fhir", Model.Version.DSTU2)
                        .Create(p)
                        .ResourceHistory("Patient","7")
                        .Delete("Patient","8")
                        .Read("Patient","9", versionId: "bla")
                        .ToRequests();

            Assert.AreEqual(4, b.Count);
            
            Assert.AreEqual(HTTPVerb.POST, b[0].Method);
            Assert.AreEqual(p, b[0].Resource);

            Assert.AreEqual(HTTPVerb.GET, b[1].Method);
            Assert.AreEqual("Patient/7/_history", b[1].Url);

            Assert.AreEqual(HTTPVerb.DELETE, b[2].Method);
            Assert.AreEqual("Patient/8", b[2].Url);

            Assert.AreEqual(HTTPVerb.GET, b[3].Method);
            Assert.AreEqual("Patient/9", b[3].Url);
            Assert.AreEqual("W/\"bla\"", b[3].IfNoneMatch);
        }

        [TestMethod]
        public void TestFormBody()
        {
            string expected = "given=test&Key=Value&Active=true";

            var endpoint = new Uri("http://myserver.org/fhir");
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("given", "test"));
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            parameters.Add(new Tuple<string, string>("Active", "true"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            var request = new RequestsBuilder(endpoint, Model.Version.DSTU2).SearchUsingPost(searchParams, resourceType).ToRequest();
            byte[] body;
            var httpRequest = request.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            var bodyText = Response.DecodeBody(body, Encoding.UTF8);

            Assert.AreEqual(bodyText, expected);
        }

        [TestMethod]
        public void TestUrlEncoding()
        {
            var endpoint = new Uri("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f");

            var tx = new RequestsBuilder(endpoint, Model.Version.DSTU2);
            tx.Get("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=<=2014-09-08T18:42:02.000Z&context=14187710&_format=json");
            var b = tx.ToRequest();

            byte[] body;

            var req = b.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, useFormatParameter: true, CompressRequestBody: false, body: out body);

            Assert.AreEqual("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=%3C%3D2014-09-08T18%3A42%3A02.000Z&context=14187710&_format=json&_format=json", req.RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void TestFormUrlEncoding()
        {
            string expected = "Key=%3C%26%3E%22%27%C3%A4%C3%AB%C3%AFo%C3%A6%C3%B8%C3%A5%E2%82%AC%24%C2%A3%40%21%23%C2%A4%25%2F%28%29%3D%3F%7C%C2%A7%C2%A8%5E%5C%5B%5D%7B%7D";

            string specialCharacters = "<&>\"'äëïoæøå€$£@!#¤%/()=?|§¨^\\[]{}";
            var  endpoint = new Uri("http://myserver.org/fhir");
            string resourceType = "Patient";
            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", specialCharacters));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            var request = new RequestsBuilder(endpoint, Model.Version.DSTU2).SearchUsingPost(searchParams, resourceType).ToRequest();
            byte[] body;
            request.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            string actual = Encoding.UTF8.GetString(body);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchUsingPost_MethodIsPost()
        {
            string expected = "POST";

            var endpoint = new Uri("http://myserver.org/fhir");
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            var request = new RequestsBuilder(endpoint, Model.Version.DSTU2).SearchUsingPost(searchParams, resourceType).ToRequest();
            byte[] body;
            var httpRequest = request.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            Assert.AreEqual(expected, httpRequest.Method);
        }

        [TestMethod]
        public void TestSearchUsingPost_ContentTypeIsFormUrlEncoded()
        {
            string expected = "application/x-www-form-urlencoded";

            var  endpoint = new Uri("http://myserver.org/fhir");
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            var request = new RequestsBuilder(endpoint, Model.Version.DSTU2).SearchUsingPost(searchParams, resourceType).ToRequest();
            byte[] body;
            var httpRequest = request.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            Assert.AreEqual(expected, httpRequest.ContentType);
        }

        [TestMethod]
        public void TestSearchUsingPost_UrlIsSuffixedWith_search()
        {
            string expected = "http://myserver.org/fhir/Patient/_search?_format=json";

            var endpoint = new Uri("http://myserver.org/fhir");
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            var request = new RequestsBuilder(endpoint, Model.Version.DSTU2).SearchUsingPost(searchParams, resourceType).ToRequest();
            byte[] body;
            HttpWebRequest httpRequest = request.ToHttpRequest(endpoint, Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            string actual = httpRequest.RequestUri.AbsoluteUri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConditionalCreate()
        {
            var p = new Patient();
            var tx = new RequestsBuilder("http://myserver.org/fhir", Model.Version.DSTU2)
                        .Create(p, new SearchParams().Where("name=foobar"));
            var b = tx.ToRequest();

            Assert.AreEqual("name=foobar",b.IfNoneExist);
        }


        [TestMethod]
        public void TestConditionalUpdate()
        {
            var p = new Patient();
            var tx = new RequestsBuilder("http://myserver.org/fhir", Model.Version.DSTU2)
                        .Update(new SearchParams().Where("name=foobar"), p, versionId: "314");
            var b = tx.ToRequest();

            Assert.AreEqual("W/\"314\"", b.IfMatch);
        }

        /// <summary>
        /// Unit test to prove issue 536: 
        /// https://github.com/FirelyTeam/fhir-net-api/issues/536
        /// </summary>
        [TestMethod]
        public void TestTransactionWithForwardSlash()
        {
            var tx2 = new RequestsBuilder("http://myserver.org/fhir/", Model.Version.DSTU2);
            var requests = tx2.Get("@Patient/1").ToRequests();

            var bundle = new Bundle
            {
                Entry = requests
                    .Select(
                        r => new Bundle.EntryComponent
                        {
                            Resource = r.Resource,
                            Request = new Bundle.RequestComponent
                            {
                                Method = r.Method,
                                Url = r.Url,
                                IfMatch = r.IfMatch,
                                IfNoneExist = r.IfNoneExist,
                                IfModifiedSince = r.IfModifiedSince,
                                IfNoneMatch = r.IfNoneMatch
                            }
                        }
                    )
                    .ToList()
            };

            var tx = new RequestsBuilder("http://myserver.org/fhir/", Model.Version.DSTU2)
                .Transaction(bundle);

            var b = tx.ToRequest();
            Assert.IsFalse(b.Url.EndsWith(@"/"), "Url cannot end with forward slash");
        }

        [TestMethod]
        public void TestTransactionWithAbsoluteUri()
        {
            var observation = new Observation
            {
                Status = ObservationStatus.Final,
                Code = new CodeableConcept("http://loinc.org", "29463-7", "Body weight"),
                Value = new Quantity(74, "kg")
            };

            var endpoint = "http://fhirtest.uhn.ca/baseDstu2";

            var client = new FhirDstu2Client(endpoint)
            {
                PreferredFormat = ResourceFormat.Json
            };

            var transaction = new RequestsBuilder(client.Endpoint, Model.Version.DSTU2)
                .Create(observation)
                .Get("Patient/1");
            var requests = transaction.ToRequests();

            Assert.AreEqual(2, requests.Count);
            Assert.IsFalse(requests[0].Url.StartsWith(endpoint), "Entries in the transaction bundle cannot contain absolute url.");
            Assert.AreEqual(nameof(Observation), requests[0].Url, "Entry must be a relative url");
        }
    }
}
