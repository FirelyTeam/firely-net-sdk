/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

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

            var p = new Patient();
            tx = new TransactionBuilder("http://myserver.org/fhir").Create(p);
            Assert.AreEqual(InteractionType.Create, tx.ToBundle().Entry[0].Annotation<InteractionType>());

            tx = new TransactionBuilder("http://myserver.org/fhir").Search(new SearchParams().Where("name=ewout"), resourceType: "Patient");
            Assert.AreEqual(InteractionType.Search, tx.ToBundle().Entry[0].Annotation<InteractionType>());
        }


        [TestMethod]
        public void TestConditionalCreate()
        {
            var p = new Patient();
            var tx = new TransactionBuilder("http://myserver.org/fhir")
                        .Create(p, new SearchParams().Where("name=foobar"));
            var b = tx.ToBundle();

            Assert.AreEqual("name=foobar", b.Entry[0].Request.IfNoneExist);
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
            var observation = new Observation
            {
                Status = ObservationStatus.Final,
                Code = new CodeableConcept("http://loinc.org", "29463-7", "Body weight"),
                Value = new Quantity(74, "kg")
            };

            var endpoint = "http://fhirtest.uhn.ca/baseDstu2";

            var client = new FhirClient(endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            
            var transaction = new TransactionBuilder(client.Endpoint)
                .Create(observation)
                .Get("Patient/1");
            var bundle = transaction.ToBundle();
            bundle.Type = Bundle.BundleType.Transaction;

            Assert.AreEqual(2, bundle.Entry.Count);
            Assert.IsFalse(bundle.Entry[0].Request.Url.StartsWith(endpoint), "Entries in the transaction bundle cannot contain absolute url.");
            Assert.AreEqual(nameof(Observation), bundle.Entry[0].Request.Url, "Entry must be a relative url");
        }
    }
}
