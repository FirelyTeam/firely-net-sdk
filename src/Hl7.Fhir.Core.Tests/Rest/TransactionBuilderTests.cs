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

namespace Hl7.Fhir.Test
{
    [TestClass]
#if PORTABLE45
	public class PortableTransactionBuilderTests
#else
    public class TransactionBuilderTests
#endif
    {
        [TestMethod]
        public void TestBuild()
        {
            var p = new Patient();
            var b = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p)
                        .ResourceHistory("Patient","7")
                        .Delete("Patient","8")
                        .Read("Patient","9", ifNoneMatch: "W/bla")
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
            Assert.AreEqual("W/bla", b.Entry[3].Request.IfNoneMatch);
        }

        [TestMethod]
        public void TestUrlEncoding()
        {
            var tx = new TransactionBuilder("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f");
            tx.Get("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=<=2014-09-08T18:42:02.000Z&context=14187710&_format=json");
            var b = tx.ToBundle();

            byte[] body;

            var req = b.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, useFormatParameter: true, body: out body);

            Assert.AreEqual("https://fhir.sandboxcernerpowerchart.com/may2015/open/d075cf8b-3261-481d-97e5-ba6c48d3b41f/MedicationPrescription?patient=1316024&status=completed%2Cstopped&_count=25&scheduledtiming-bounds-end=%3C%3D2014-09-08T18%3A42%3A02.000Z&context=14187710&_format=json&_format=json", req.RequestUri.AbsoluteUri);
        }

    }
}
