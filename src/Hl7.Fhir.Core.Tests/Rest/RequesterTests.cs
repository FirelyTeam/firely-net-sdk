/*
 * Copyright(c) 2017, Furore(info @furore.com) and contributors
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
    public class RequesterTests
    {
        [TestMethod]
        public void TestPreferSetting()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p);
            var b = tx.ToBundle();

            var request = b.Entry[0].ToHttpRequest(Prefer.ReturnMinimal, ResourceFormat.Json, false, false, out byte[] dummy);
            Assert.AreEqual("return=minimal", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(Prefer.ReturnRepresentation, ResourceFormat.Json, false, false, out byte[] dummy3);
            Assert.AreEqual("return=representation", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(Prefer.OperationOutcome, ResourceFormat.Json, false, false, out byte[] dummy2);
            Assert.AreEqual("return=OperationOutcome", request.Headers["Prefer"]);
        }
    }
}
 