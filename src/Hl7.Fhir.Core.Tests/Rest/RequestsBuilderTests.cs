/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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
using Hl7.Fhir.Model.DSTU2;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequestsBuilderTests
    {
        [TestMethod]
        public void TestBuild()
        {
            var p = new Patient();
            var b = new RequestsBuilder("http://myserver.org/fhir")
                        .Create(p)
                        .ResourceHistory("Patient","7")
                        .Delete("Patient","8")
                        .Read("Patient","9", versionId: "bla")
                        .ToRequests();

            Assert.AreEqual(4, b.Count);
            
            Assert.AreEqual(HTTPVerb.POST, b[0].Method);
            Assert.AreEqual(p, b[0].Resource);

            Assert.AreEqual(HTTPVerb.GET, b[1].Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/7/_history", b[1].Url);

            Assert.AreEqual(HTTPVerb.DELETE, b[2].Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/8", b[2].Url);

            Assert.AreEqual(HTTPVerb.GET, b[3].Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/9", b[3].Url);
            Assert.AreEqual("W/\"bla\"", b[3].IfNoneMatch);
        }

        [TestMethod]
        public void TestUrlEncoding()
        {
            var tx = new RequestsBuilder("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f");
            tx.Get("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=<=2014-09-08T18:42:02.000Z&context=14187710&_format=json");
            var b = tx.ToRequest();

            byte[] body;

            var req = b.ToHttpRequest(Model.Version.DSTU2, "1.0.2", Prefer.ReturnRepresentation, ResourceFormat.Json, useFormatParameter: true, CompressRequestBody: false, body: out body);

            Assert.AreEqual("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=%3C%3D2014-09-08T18%3A42%3A02.000Z&context=14187710&_format=json&_format=json", req.RequestUri.AbsoluteUri);
        }


        [TestMethod]
        public void TestConditionalCreate()
        {
            var p = new Patient();
            var tx = new RequestsBuilder("http://myserver.org/fhir")
                        .Create(p, new SearchParams().Where("name=foobar"));
            var b = tx.ToRequest();

            Assert.AreEqual("name=foobar",b.IfNoneExist);
        }


        [TestMethod]
        public void TestConditionalUpdate()
        {
            var p = new Patient();
            var tx = new RequestsBuilder("http://myserver.org/fhir")
                        .Update(new SearchParams().Where("name=foobar"), p, versionId: "314");
            var b = tx.ToRequest();

            Assert.AreEqual("W/\"314\"", b.IfMatch);
        }
    }
}
