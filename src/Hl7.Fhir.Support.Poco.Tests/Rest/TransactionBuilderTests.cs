﻿/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TransactionBuilderTests
    {
        [TestMethod]
        public void TestBuild()
        {
            var p = new TestPatient();
            var b = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p)
                        .ResourceHistory("Patient", "7")
                        .Delete("Patient", "8")
                        .Read("Patient", "9", versionId: "bla")
                        .ToBundle();

            Assert.AreEqual(4, b.Entry.Count);

            Assert.AreEqual(Bundle.HTTPVerb.POST, b.Entry[0].Request.Method);
            Assert.AreEqual(p, b.Entry[0].Resource);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[1].Request.Method);
            Assert.AreEqual("Patient/7/_history", b.Entry[1].Request.Url);

            Assert.AreEqual(Bundle.HTTPVerb.DELETE, b.Entry[2].Request.Method);
            Assert.AreEqual("Patient/8", b.Entry[2].Request.Url);

            Assert.AreEqual(Bundle.HTTPVerb.GET, b.Entry[3].Request.Method);
            Assert.AreEqual("Patient/9", b.Entry[3].Request.Url);
            Assert.AreEqual("W/\"bla\"", b.Entry[3].Request.IfNoneMatch);
        }

        [TestMethod]
        public void TestInteractionType()
        {
            // just try a few
            var tx = new TransactionBuilder("http://myserver.org/fhir").ServerHistory();
            Assert.AreEqual(InteractionType.History, tx.ToBundle().Entry[0].Annotation<InteractionType>());

            tx = new TransactionBuilder("http://myserver.org/fhir").ServerOperation("$everything", null);
            Assert.AreEqual(InteractionType.Operation, tx.ToBundle().Entry[0].Annotation<InteractionType>());

            var p = new TestPatient();
            tx = new TransactionBuilder("http://myserver.org/fhir").Create(p);
            Assert.AreEqual(InteractionType.Create, tx.ToBundle().Entry[0].Annotation<InteractionType>());

            tx = new TransactionBuilder("http://myserver.org/fhir").Search(new SearchParams().Where("name=ewout"), resourceType: "Patient");
            Assert.AreEqual(InteractionType.Search, tx.ToBundle().Entry[0].Annotation<InteractionType>());
        }


        [TestMethod]
        public void TestConditionalCreate()
        {
            var p = new TestPatient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p, new SearchParams().Where("name=foobar"));
            var b = tx.ToBundle();

            Assert.AreEqual("name=foobar", b.Entry[0].Request.IfNoneExist);
        }


        [TestMethod]
        public void TestConditionalUpdate()
        {
            var p = new TestPatient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Update(new SearchParams().Where("name=foobar"), p, versionId: "314");
            var b = tx.ToBundle();

            Assert.AreEqual("W/\"314\"", b.Entry[0].Request.IfMatch);
        }

        /// <summary>
        /// Unit test to prove issue 536: 
        /// https://github.com/FirelyTeam/firely-net-sdk/issues/536
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

        [TestMethod]
        public void TestTransactionWithAbsoluteUri()
        {
            var patient = new TestPatient();
            var endpoint = "http://fhirtest.uhn.ca/baseDstu2";

            var transaction = new TransactionBuilder(endpoint)
                .Create(patient)
                .Get("Patient/1");
            var bundle = transaction.ToBundle();
            bundle.Type = Bundle.BundleType.Transaction;

            Assert.AreEqual(2, bundle.Entry.Count);
            Assert.IsFalse(bundle.Entry[0].Request.Url.StartsWith(endpoint), "Entries in the transaction bundle cannot contain absolute url.");
            Assert.AreEqual("Patient", bundle.Entry[0].Request.Url, "Entry must be a relative url");
        }

        [TestMethod]
        public void TransactionBuilderWithFullUrlTest()
        {
            var fullUrl = "http://myserver.org/fhir/123";
            var p = new TestPatient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Update(new SearchParams().Where("name=foobar"), p, versionId: "314", fullUrl);
            var bundle = tx.ToBundle();

            bundle.Entry.Should().ContainSingle().Which.FullUrl.Should().Be(fullUrl);

            tx.Read("TestPatient", "123");

            bundle = tx.ToBundle();

            bundle.Entry.Skip(1).Should().ContainSingle().Which.FullUrl.Should().BeNull();

        }
    }
}
