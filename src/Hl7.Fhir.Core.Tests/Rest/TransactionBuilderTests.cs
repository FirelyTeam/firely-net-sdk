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
            
            Assert.AreEqual(Bundle.HTTPVerb.POST, b.Entry[0].Transaction.Method);
            Assert.AreEqual(p, b.Entry[0].Resource);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[1].Transaction.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/7/_history", b.Entry[1].Transaction.Url);

            Assert.AreEqual(Bundle.HTTPVerb.DELETE, b.Entry[2].Transaction.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/8", b.Entry[2].Transaction.Url);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[3].Transaction.Method);
            Assert.AreEqual("http://myserver.org/fhir/Patient/9", b.Entry[3].Transaction.Url);
            Assert.AreEqual("W/bla", b.Entry[3].Transaction.IfNoneMatch);
        }
    }
}
