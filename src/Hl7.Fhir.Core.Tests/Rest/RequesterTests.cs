/*
 * Copyright(c) 2017, Furore(info @furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RequesterTests
    {
        [TestMethod]
        public void SetsInteractionType()
        {
            // just try a few
            var tx = new TransactionBuilder("http://myserver.org/fhir").ServerHistory();
            Assert.AreEqual(TransactionBuilder.InteractionType.History, tx.ToBundle().Entry[0].Annotation<TransactionBuilder.InteractionType>());

            tx = new TransactionBuilder("http://myserver.org/fhir").ServerOperation("$everything", null);
            Assert.AreEqual(TransactionBuilder.InteractionType.Operation, tx.ToBundle().Entry[0].Annotation<TransactionBuilder.InteractionType>());

            var p = new Patient();
            tx = new TransactionBuilder("http://myserver.org/fhir").Create(p);
            Assert.AreEqual(TransactionBuilder.InteractionType.Create, tx.ToBundle().Entry[0].Annotation<TransactionBuilder.InteractionType>());

            tx = new TransactionBuilder("http://myserver.org/fhir").Search(new SearchParams().Where("name=ewout"), resourceType: "Patient");
            Assert.AreEqual(TransactionBuilder.InteractionType.Search, tx.ToBundle().Entry[0].Annotation<TransactionBuilder.InteractionType>());
        }

        [TestMethod]
        public void TestPreferSettingWebRequester()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p);
            var b = tx.ToBundle();
            byte[] dummy;

            var request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Lenient, Prefer.ReturnMinimal, ResourceFormat.Json, false, false, out dummy);
            Assert.AreEqual("return=minimal", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Strict, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false, out dummy);
            Assert.AreEqual("return=representation", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Strict, Prefer.OperationOutcome, ResourceFormat.Json, false, false, out dummy);
            Assert.AreEqual("return=OperationOutcome", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Strict, null, ResourceFormat.Json, false, false, out dummy);
            Assert.IsNull(request.Headers["Prefer"]);

            tx = new TransactionBuilder("http://myserver.org/fhir").Search(new SearchParams().Where("name=ewout"), resourceType: "Patient");
            b = tx.ToBundle();

            request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Lenient, Prefer.ReturnMinimal, ResourceFormat.Json, false, false, out dummy);
            Assert.AreEqual("handling=lenient", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(SearchParameterHandling.Strict, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false, out dummy);
            Assert.AreEqual("handling=strict", request.Headers["Prefer"]);

            request = b.Entry[0].ToHttpRequest(null, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false, out dummy);
            Assert.IsNull(request.Headers["Prefer"]);        
        }

        [TestMethod]
        public void TestPreferSettingHttpRequester()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p);
            var b = tx.ToBundle();
            byte[] dummy;

            var request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Lenient, Prefer.ReturnMinimal, ResourceFormat.Json, false, false);
            Assert.AreEqual("return=minimal", request.Headers.GetValues("Prefer").FirstOrDefault());

            request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Strict, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false);
            Assert.AreEqual("return=representation", request.Headers.GetValues("Prefer").FirstOrDefault());

            request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Strict, Prefer.OperationOutcome, ResourceFormat.Json, false, false);
            Assert.AreEqual("return=OperationOutcome", request.Headers.GetValues("Prefer").FirstOrDefault());

            request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Strict, null, ResourceFormat.Json, false, false);
            request.Headers.TryGetValues("Prefer", out var preferHeader);
            Assert.IsNull(preferHeader);

            tx = new TransactionBuilder("http://myserver.org/fhir").Search(new SearchParams().Where("name=ewout"), resourceType: "Patient");
            b = tx.ToBundle();

            request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Lenient, Prefer.ReturnMinimal, ResourceFormat.Json, false, false);
            Assert.AreEqual("handling=lenient", request.Headers.GetValues("Prefer").FirstOrDefault());

            request = b.Entry[0].ToHttpRequestMessage(SearchParameterHandling.Strict, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false);
            Assert.AreEqual("handling=strict", request.Headers.GetValues("Prefer").FirstOrDefault());

            request = b.Entry[0].ToHttpRequestMessage(null, Prefer.ReturnRepresentation, ResourceFormat.Json, false, false);
            request.Headers.TryGetValues("Prefer", out preferHeader);
            Assert.IsNull(preferHeader);        
        }
    }
}
 