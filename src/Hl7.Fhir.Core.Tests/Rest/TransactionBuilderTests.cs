/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TransactionBuilderTests
    {
        [TestMethod]
        public void TestBuild()
        {
            var p = new Patient();
            var b = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p)
                        .ResourceHistory("Patient","7")
                        .Delete("Patient","8")
                        .Read("Patient","9", versionId: "bla")
                        .ToBundle();

            Assert.AreEqual(4, b.Entry.Count);
            
            Assert.AreEqual(Bundle.HTTPVerb.POST, b.Entry[0].Request.Method);
            Assert.AreEqual(p, b.Entry[0].Resource);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[1].Request.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/7/_history", b.Entry[1].Request.Url);

            Assert.AreEqual(Bundle.HTTPVerb.DELETE, b.Entry[2].Request.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/8", b.Entry[2].Request.Url);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[3].Request.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/9", b.Entry[3].Request.Url);
            Assert.AreEqual("W/\"bla\"", b.Entry[3].Request.IfNoneMatch);
        }

        [TestMethod]
        public void TestFormBody()
        {
            string expected = "given=test&Key=Value&Active=true";

            string endpoint = "http://myserver.org/fhir";
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("given", "test"));
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            parameters.Add(new Tuple<string, string>("Active", "true"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            Bundle bundle = new TransactionBuilder(endpoint).SearchUsingPost(searchParams, resourceType).ToBundle();
            byte[] body;
            HttpWebRequest request = bundle.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            var bodyText = HttpToEntryExtensions.DecodeBody(body, Encoding.UTF8);

            Assert.AreEqual(bodyText, expected);
        }
        
        [TestMethod]
        public void TestUrlEncoding()
        {
            var tx = new TransactionBuilder("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f");
            tx.Get("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=<=2014-09-08T18:42:02.000Z&context=14187710&_format=json");
            var b = tx.ToBundle();

            byte[] body;

            var req = b.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, useFormatParameter: true, CompressRequestBody: false, body: out body);

            Assert.AreEqual("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=%3C%3D2014-09-08T18%3A42%3A02.000Z&context=14187710&_format=json&_format=json", req.RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void TestFormUrlEncoding()
        {
            string expected = "Key=%3C%26%3E%22%27%C3%A4%C3%AB%C3%AFo%C3%A6%C3%B8%C3%A5%E2%82%AC%24%C2%A3%40%21%23%C2%A4%25%2F%28%29%3D%3F%7C%C2%A7%C2%A8%5E%5C%5B%5D%7B%7D";

            string specialCharacters = "<&>\"'äëïoæøå€$£@!#¤%/()=?|§¨^\\[]{}";
            string endpoint = "http://myserver.org/fhir";
            string resourceType = "Patient";
            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", specialCharacters));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            Bundle bundle = new TransactionBuilder(endpoint).SearchUsingPost(searchParams, resourceType).ToBundle();
            byte[] body;
            bundle.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            string actual = Encoding.UTF8.GetString(body);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestSearchUsingPost_MethodIsPost()
        {
            string expected = "POST";

            string endpoint = "http://myserver.org/fhir";
            string resourceType = "Patient";
            
            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            Bundle bundle = new TransactionBuilder(endpoint).SearchUsingPost(searchParams, resourceType).ToBundle();
            byte[] body;
            HttpWebRequest request = bundle.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            Assert.AreEqual(expected, request.Method);
        }

        [TestMethod]
        public void TestSearchUsingPost_ContentTypeIsFormUrlEncoded()
        {
            string expected = "application/x-www-form-urlencoded";

            string endpoint = "http://myserver.org/fhir";
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "Value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            Bundle bundle = new TransactionBuilder(endpoint).SearchUsingPost(searchParams, resourceType).ToBundle();
            byte[] body;
            HttpWebRequest request = bundle.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            Assert.AreEqual(expected, request.ContentType);
        }

        [TestMethod]
        public void TestSearchUsingPost_UrlIsSuffixedWith_search()
        {
            string expected = "http://myserver.org/fhir/Patient/_search?_format=json";

            string endpoint = "http://myserver.org/fhir";
            string resourceType = "Patient";

            var parameters = new List<Tuple<string, string>>();
            parameters.Add(new Tuple<string, string>("Key", "value"));
            SearchParams searchParams = SearchParams.FromUriParamList(parameters);

            Bundle bundle = new TransactionBuilder(endpoint).SearchUsingPost(searchParams, resourceType).ToBundle();
            byte[] body;
            HttpWebRequest request = bundle.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, true, false, out body);

            string actual = request.RequestUri.AbsoluteUri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConditionalCreate()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p, new SearchParams().Where("name=foobar"));
            var b = tx.ToBundle();

            Assert.AreEqual("name=foobar",b.Entry[0].Request.IfNoneExist);
        }


        [TestMethod]
        public void TestConditionalUpdate()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Update(new SearchParams().Where("name=foobar"), p, versionId: "314");
            var b = tx.ToBundle();

            Assert.AreEqual("W/\"314\"", b.Entry[0].Request.IfMatch);
        }

        /// <summary>
        /// Unit test to prove issue 536: 
        /// https://github.com/ewoutkramer/fhir-net-api/issues/536
        /// </summary>
        [TestMethod]
        public void TestTransactionWithForwardSlash()
        {
            var tx2 = new TransactionBuilder("http://myserver.org/fhir/");
            var bundle = tx2.Get("@Patient/1").ToBundle();

            var tx = new TransactionBuilder("http://myserver.org/fhir/")
                .Transaction(bundle);

            var b = tx.ToBundle();
            Assert.IsFalse(b.Entry[0].Request.Url.EndsWith(@"/"), "Url cannot end with forward slash");
        }
    }
}
