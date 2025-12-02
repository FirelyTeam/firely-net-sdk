#pragma warning disable CS0618 // Type or member is obsolete

/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class FhirClientTests
    {
        //public static Uri testEndpoint = new Uri("http://spark-dstu3.furore.com/fhir");
        //public static Uri testEndpoint = new Uri("http://localhost.fiddler:1396/fhir");
        //public static Uri testEndpoint = new Uri("https://localhost:44346/fhir");
        //public static Uri testEndpoint = new Uri("http://localhost:1396/fhir");
        //public static Uri testEndpoint = new Uri("http://test.fhir.org/r3");
        //public static Uri testEndpoint = new Uri("http://localhost:4080");
        //public static Uri testEndpoint = new Uri("https://api.fhir.me");
        //public static Uri testEndpoint = new Uri("http://fhirtest.uhn.ca/baseDstu3");
        //public static Uri testEndpoint = new Uri("http://localhost:49911/fhir");
        //public static Uri testEndpoint = new Uri("http://sqlonfhir-stu3.azurewebsites.net/fhir");
        public static Uri testEndpoint = new Uri("https://server.fire.ly/r3");

        //public static Uri _endpointSupportingSearchUsingPost = new Uri("http://localhost:49911/fhir");
        public static Uri _endpointSupportingSearchUsingPost = new Uri("http://localhost:4080");
        //public static Uri _endpointSupportingSearchUsingPost = new Uri("https://vonk.fire.ly/r3");

        public static Uri TerminologyEndpoint = new Uri("https://stu3.ontoserver.csiro.au/fhir");
        // public static Uri TerminologyEndpoint = new Uri("http://test.fhir.org/r3");

        private static readonly string patientId = "pat1" + ModelInfo.Version;
        private static readonly string locationId = "loc1" + ModelInfo.Version;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            CreateItems().WaitNoResult();
        }

        private static async Tasks.Task CreateItems()
        {
            var client = new FhirClient(testEndpoint);

            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.ReturnPreference = ReturnPreference.Representation;

            var pat = new Patient()
            {
                Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = "Donald",
                    }
                },
                Id = patientId,
                Identifier = new List<Identifier>()
                {
                    new Identifier()
                    {
                        System = "urn:oid:1.2.36.146.595.217.0.1",
                        Value = "12345"
                    }
                }
            };

            var loc = new Location()
            {
                Address = new Address()
                {
                    City = "Den Burg"
                },
                Id = locationId
            };

            // Create the patient
            Console.WriteLine("Creating patient...");
            Patient p = await client.UpdateAsync(pat);
            var l = await client.UpdateAsync(loc);
            Trace.WriteLine(l);
            Assert.IsNotNull(p);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            System.Diagnostics.Trace.WriteLine("Testing against fhir server: " + testEndpoint);
        }

        public static void DebugDumpBundle(Hl7.Fhir.Model.Bundle b)
        {
            System.Diagnostics.Trace.WriteLine(String.Format("--------------------------------------------\r\nBundle Type: {0} ({1} total items, {2} included)", b.Type.ToString(), b.Total, (b.Entry != null ? b.Entry.Count.ToString() : "-")));

            if (b.Entry != null)
            {
                foreach (var item in b.Entry)
                {
                    if (item.Request != null)
                        System.Diagnostics.Trace.WriteLine(String.Format("        {0}: {1}", item.Request.Method.ToString(), item.Request.Url));
                    if (item.Response != null && item.Response.Status != null)
                        System.Diagnostics.Trace.WriteLine(String.Format("        {0}", item.Response.Status));
                    if (item.Resource != null && item.Resource is Hl7.Fhir.Model.DomainResource)
                    {
                        if (item.Resource.Meta != null && item.Resource.Meta.LastUpdated.HasValue)
                            System.Diagnostics.Trace.WriteLine(String.Format("            Last Updated:{0}, [{1}]", item.Resource.Meta.LastUpdated.Value, item.Resource.Meta.LastUpdated.Value.ToString("HH:mm:ss.FFFF")));
                        Hl7.Fhir.Rest.ResourceIdentity ri = new Hl7.Fhir.Rest.ResourceIdentity(item.FullUrl);
                        System.Diagnostics.Trace.WriteLine(String.Format("            {0}", (item.Resource as Hl7.Fhir.Model.DomainResource).ResourceIdentity(ri.BaseUri).OriginalString));
                    }
                }
            }
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task FetchConformanceHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                await TestConformance(client);
            }
        }

        private async Tasks.Task TestConformance(BaseFhirClient client)
        {
            client.Settings.ParserSettings = client.Settings.ParserSettings with { AllowUnrecognizedEnums = true };
            var entry = await client.CapabilityStatementAsync();

            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.FhirVersion);
            // Assert.AreEqual("Spark.Service", c.Software.Name); // This is only for ewout's server
            Assert.AreEqual(CapabilityStatement.RestfulCapabilityMode.Server, entry.Rest[0].Mode.Value);
            Assert.AreEqual("200", client.LastResult.Status);

            entry = await client.CapabilityStatementAsync(SummaryType.True);

            Assert.IsNull(entry.Text); // DSTU2 has this property as not include as part of the summary (that would be with SummaryType.Text)
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.FhirVersion);
            Assert.AreEqual(CapabilityStatement.RestfulCapabilityMode.Server, entry.Rest[0].Mode.Value);
            Assert.AreEqual("200", client.LastResult.Status);

            Assert.IsNotNull(entry.Rest[0].Resource, "The resource property should be in the summary");
            Assert.AreNotEqual(0, entry.Rest[0].Resource.Count, "There is expected to be at least 1 resource defined in the conformance statement");
            Assert.IsTrue(entry.Rest[0].Resource[0].Type.HasValue, "The resource type should be provided");
            Assert.AreNotEqual(0, entry.Rest[0].Operation.Count, "operations should be listed in the summary"); // actually operations are now a part of the summary
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task PatchHttpClient()
        {
            using var client = new FhirClient(testEndpoint);
            await Patch(client);

        }

        private async Tasks.Task Patch(BaseFhirClient client)
        {
            var patchparams = new Parameters();
            patchparams.AddAddPatchParameter("Patient", "birthdate", new Date("1930-01-01"));
            await client.PatchAsync<Patient>("example", patchparams);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CondionalPatchHttpClient()
        {
            using var client = new FhirClient(testEndpoint);
            await ConditionalPatch(client);

        }

        private async Tasks.Task ConditionalPatch(BaseFhirClient client)
        {
            var patchparams = new Parameters();
            patchparams.AddAddPatchParameter("Patient", "birthdate", new Date("1930-01-01"));
            var condition = new SearchParams().Where("name=Donald");
            await client.ConditionalPatchAsync<Patient>(condition, patchparams);
        }
       

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task ReadWithFormatHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                await testReadWithFormat(client);
            }
        }

        private async Tasks.Task testReadWithFormat(BaseFhirClient client)
        {
            client.Settings.UseFormatParameter = true;
            client.Settings.PreferredFormat = ResourceFormat.Json;
            var loc = await client.ReadAsync<Patient>("Patient/example");
            Assert.IsNotNull(loc);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task ReadWrongResourceTypeHttpClient()
        {
            FhirClient client = new FhirClient(testEndpoint);
            await Assert.ThrowsAsync<FhirOperationException>(async () => await testReadWrongResourceType(client));
        }

        private static async Tasks.Task testReadWrongResourceType(BaseFhirClient client)
        {
            var pat = await client.ReadAsync<Patient>("Location/" + locationId);
            Trace.WriteLine(pat);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task ReadHttpClient()
        {
            using var client = new FhirClient(testEndpoint);
            await testReadClientAsync(client);
        }

        private async Tasks.Task testReadClientAsync(BaseFhirClient client)
        {
            var loc = await client.ReadAsync<Location>("Location/" + locationId);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            Assert.AreEqual(locationId, loc.Id);
            Assert.IsNotNull(loc.Meta.VersionId);

            var loc2 = await client.ReadAsync<Location>(ResourceIdentity.Build("Location", locationId, loc.Meta.VersionId));
            Assert.IsNotNull(loc2);
            Assert.AreEqual(loc2.Id, loc.Id);
            Assert.AreEqual(loc2.Meta.VersionId, loc.Meta.VersionId);

            try
            {
                var l = await client.ReadAsync<Location>(new Uri("Location/45qq54", UriKind.Relative));
                Trace.WriteLine(l);
                Assert.Fail();
            }
            catch (FhirOperationException ex)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Status);
                Assert.AreEqual("404", client.LastResult.Status);
            }

            var loc3 = await client.ReadAsync<Location>(ResourceIdentity.Build("Location", locationId, loc.Meta.VersionId));
            Assert.IsNotNull(loc3);
            var jsonSer = new FhirJsonSerializer();
            Assert.AreEqual(await jsonSer.SerializeToStringAsync(loc),
                await jsonSer.SerializeToStringAsync(loc3));

            var loc4 = await client.ReadAsync<Location>(loc.ResourceIdentity());
            Assert.IsNotNull(loc4);
            Assert.AreEqual(await jsonSer.SerializeToStringAsync(loc),
                await jsonSer.SerializeToStringAsync(loc4));
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task ReadRelativeHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testReadRelative(client);
            }
        }

        private async Tasks.Task testReadRelative(BaseFhirClient client)
        {
            var loc = await client.ReadAsync<Location>(new Uri("Location/" + locationId, UriKind.Relative));
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            var ri = ResourceIdentity.Build(testEndpoint, "Location", locationId);
            loc = await client.ReadAsync<Location>(ri);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);
        }

#if NO_ASYNC_ANYMORE
		[TestMethod, TestCategory("FhirClient")]
		public void ReadRelativeAsync()
		{
			FhirClient client = new FhirClient(testEndpoint);
            testRelativeAsyncClient(client);
		}

        [TestMethod, TestCategory("FhirClient")]
		public void ReadRelativeAsyncHttpClient()
		{
			using (FhirHttpClient client = new FhirHttpClient(testEndpoint))
            {
               testRelativeAsyncClient(client);
            }
        }

        private void testRelativeAsyncClient(BaseFhirClient client)
        {
            var loc = client.ReadAsync<Location>(new Uri("Location/" + locationId, UriKind.Relative)).Result;
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            var ri = ResourceIdentity.Build(testEndpoint, "Location", locationId);
            loc = client.ReadAsync<Location>(ri).Result;
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);
        }
#endif

        [TestMethod, Ignore]   // Something does not work with the gzip
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task SearchHttpClient()
        {
            using (var handler = new HttpClientHandler()
            {
#pragma warning disable SYSLIB0039
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
#pragma warning restore SYSLIB0039
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,                
            })
            using (FhirClient client = new FhirClient(testEndpoint, messageHandler: handler))
            {
                Bundle result;

                handler.AutomaticDecompression = DecompressionMethods.GZip;

                result = await client.SearchAsync<DiagnosticReport>();
                Assert.IsNotNull(result);
                Assert.IsGreaterThan(10, result.Entry.Count, "Test should use testdata with more than 10 reports");

                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


                result = await client.SearchAsync<DiagnosticReport>(pageSize: 10);
                Assert.IsNotNull(result);
                Assert.IsLessThanOrEqualTo(10, result.Entry.Count);

                handler.AutomaticDecompression = DecompressionMethods.GZip;

                var withSubject = result.Entry.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Subject != null);
                Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

                ResourceIdentity ri = withSubject.ResourceIdentity();

                handler.AutomaticDecompression = DecompressionMethods.Deflate;

                result = await client.SearchAsync<Patient>([ "name=Chalmers", "name=Peter" ]);

                Assert.IsNotNull(result);
                Assert.IsNotEmpty(result.Entry);
            }
        }

        [TestMethod, TestCategory("FhirClient")]
        public async Tasks.Task SearchInvalidCriteriaHttpClient()
        {
            var client = new FhirClient(testEndpoint);
            await Assert.ThrowsAsync<ArgumentException>(async () => await testSearchInvalidCriteria(client));
        }

        private async Tasks.Task testSearchInvalidCriteria(BaseFhirClient client)
        {
            var p = await client.SearchAsync<Patient>(["test"]);
            Trace.WriteLine(p);
        }

#if NO_ASYNC_ANYMORE
        [TestMethod, TestCategory("FhirClient")]
        public void SearchAsync()
        {
            FhirClient client = new FhirClient(testEndpoint);
            testSearchAsyncHttpClient(client);
        }



        public void SearchAsyncHttpClient()
        {
            using(FhirHttpClient client = new FhirHttpClient(testEndpoint))
            {
                testSearchAsyncHttpClient(client);
            }
        }

        private void testSearchAsyncHttpClient(BaseFhirClient client)
        {
            Bundle result;

            result = client.SearchAsync<DiagnosticReport>().Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count() > 10, "Test should use testdata with more than 10 reports");

            result = client.SearchAsync<DiagnosticReport>(pageSize: 10).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count <= 10);

            var withSubject =
                result.Entry.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Subject != null);
            Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

            ResourceIdentity ri = new ResourceIdentity(withSubject.Id);

            result = client.SearchByIdAsync<DiagnosticReport>(ri.Id,
                        includes: new string[] { "DiagnosticReport.subject" }).Result;
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Entry.Count);  // should have subject too

            Assert.IsNotNull(result.Entry.Single(entry => new ResourceIdentity(entry.Resource.Id).Collection ==
                        typeof(DiagnosticReport).GetCollectionName()));
            Assert.IsNotNull(result.Entry.Single(entry => new ResourceIdentity(entry.Resource.Id).Collection ==
                        typeof(Patient).GetCollectionName()));

            result = client.SearchAsync<Patient>(new string[] { "name=Everywoman", "name=Eve" }).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count > 0);
        }

        public void SearchAsyncHttpClient()
        {
            using(TestClient client = new TestClient(testEndpoint))
            {
                Bundle result;

                result = client.SearchAsync<DiagnosticReport>().Result;
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Entry.Count() > 10, "Test should use testdata with more than 10 reports");

                result = client.SearchAsync<DiagnosticReport>(pageSize: 10).Result;
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Entry.Count <= 10);

                var withSubject =
                    result.Entry.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Resource.Subject != null);
                Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

                ResourceIdentity ri = new ResourceIdentity(withSubject.Id);

                result = client.SearchByIdAsync<DiagnosticReport>(ri.Id,
                            includes: new string[] { "DiagnosticReport.subject" }).Result;
                Assert.IsNotNull(result);

                Assert.AreEqual(2, result.Entry.Count);  // should have subject too

                Assert.IsNotNull(result.Entry.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                            typeof(DiagnosticReport).GetCollectionName()));
                Assert.IsNotNull(result.Entry.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                            typeof(Patient).GetCollectionName()));

                result = client.SearchAsync<Patient>(new string[] { "name=Everywoman", "name=Eve" }).Result;

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Entry.Count > 0);
            }
        }
#endif


        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task PagingHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testPaging(client);
            }
        }

        private async Tasks.Task testPaging(BaseFhirClient client)
        {
            var result = await client.SearchAsync<Patient>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsLessThanOrEqualTo(10, result.Entry.Count);

            var firstId = result.Entry.First().Resource.Id;

            // Browse forward

            result = await client.ContinueAsync(result);

            Assert.IsNotNull(result);
            var nextId = result.Entry.First().Resource.Id;
            Assert.AreNotEqual(firstId, nextId);

            // Browse to first
            result = await client.ContinueAsync(result, PageDirection.First);
            Assert.IsNotNull(result);
            var prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);

            // Forward, then backwards
            result = await client.ContinueAsync(result, PageDirection.Next);
            Assert.IsNotNull(result);
            result = await client.ContinueAsync(result, PageDirection.Previous);
            Assert.IsNotNull(result);
            prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task PagingInJsonHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testPagingInJson(client);
            }
        }

        private static async Tasks.Task testPagingInJson(BaseFhirClient client)
        {
            client.Settings.PreferredFormat = ResourceFormat.Json;

            var result = await client.SearchAsync<Patient>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsLessThanOrEqualTo(10, result.Entry.Count);

            var firstId = result.Entry.First().Resource.Id;

            // Browse forward
            result = await client.ContinueAsync(result);
            Assert.IsNotNull(result);
            var nextId = result.Entry.First().Resource.Id;
            Assert.AreNotEqual(firstId, nextId);

            // Browse to first
            result = await client.ContinueAsync(result, PageDirection.First);
            Assert.IsNotNull(result);
            var prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);

            // Forward, then backwards
            result = await client.ContinueAsync(result, PageDirection.Next);
            Assert.IsNotNull(result);
            result = await client.ContinueAsync(result, PageDirection.Previous);
            Assert.IsNotNull(result);
            prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CreateAndFullRepresentationHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testCreateAndFullRepresentation(client);
            }
        }

        private static async Tasks.Task testCreateAndFullRepresentation(BaseFhirClient client)
        {
            client.Settings.ReturnPreference = ReturnPreference.Representation;       // which is also the default

            var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
            ResourceIdentity ri = pat.ResourceIdentity().WithBase(client.Endpoint);
            pat.Id = null;
            pat.Identifier.Clear();
            var patC = await client.CreateAsync<Patient>(pat);
            Assert.IsNotNull(patC);

            client.Settings.ReturnPreference = ReturnPreference.Minimal;
            patC = await client.CreateAsync<Patient>(pat);

            Assert.IsNull(patC);

            if (client.LastBody != null && client.LastBody.Length > 0)
            {
                var returned = client.LastBodyAsResource;
                Assert.IsTrue(returned is OperationOutcome);
            }

            // Now validate this resource
            client.Settings.ReturnPreference = ReturnPreference.Representation;      // which is also the default
            var p = new Parameters();
            //  p.Add("mode", new FhirString("create"));
            p.Add("resource", pat);
            OperationOutcome ooI = (OperationOutcome)await client.InstanceOperationAsync(ri.WithoutVersion(), "validate", p);
            Assert.IsNotNull(ooI);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CreateEditDeleteHttpClient()
        {
            using (var handler = new HttpClientHandler()
            {
#pragma warning disable SYSLIB0039
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
#pragma warning restore SYSLIB0039
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,                
            })
            using (FhirClient client = new FhirClient(testEndpoint, messageHandler: handler))
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                // client.CompressRequestBody = true;
                await testCreateEditDeleteAsync(client);
            }
        }

        private Uri createdTestPatientUrl = null;
        /// <summary>
        /// This test is also used as a "setup" test for the History test.
        /// If you change the number of operations in here, this will make the History test fail.
        /// </summary>
        private async Tasks.Task testCreateEditDeleteAsync(BaseFhirClient client)
        {
            // client.CompressRequestBody = true;

            var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
            pat.Id = null;
            pat.Identifier.Clear();
            pat.Identifier.Add(new Identifier("http://hl7.org/test/2", "99999"));

            System.Diagnostics.Trace.WriteLine(await new FhirXmlSerializer().SerializeToStringAsync(pat));

            var fe = await client.CreateAsync(pat); // Create as we are not providing the ID to be used.
            Assert.IsNotNull(fe);
            Assert.IsNotNull(fe.Id);
            Assert.IsNotNull(fe.Meta.VersionId);
            createdTestPatientUrl = fe.ResourceIdentity();

            fe.Identifier.Add(new Identifier("http://hl7.org/test/2", "3141592"));
            var fe2 = await client.UpdateAsync(fe);

            Assert.IsNotNull(fe2);
            Assert.AreEqual(fe.Id, fe2.Id);
            Assert.AreNotEqual(fe.ResourceIdentity(), fe2.ResourceIdentity());
            Assert.HasCount(2, fe2.Identifier);

            fe.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
            var fe3 = await client.UpdateAsync(fe);
            Assert.IsNotNull(fe3);
            Assert.HasCount(3, fe3.Identifier);

            await client.DeleteAsync(fe3);

            try
            {
                // Get most recent version
                fe = await client.ReadAsync<Patient>(fe.ResourceIdentity().WithoutVersion());
                Assert.Fail();
            }
            catch (FhirOperationException ex)
            {
                Assert.AreEqual(HttpStatusCode.Gone, ex.Status, "Expected the record to be gone");
                Assert.AreEqual("410", client.LastResult.Status);
            }
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        //Test for github issue https://github.com/FirelyTeam/firely-net-sdk/issues/145
        public async Tasks.Task Create_ObservationWithValueAsSimpleQuantity_ReadReturnsValueAsQuantityHttpClient()
        {
            using var client = new FhirClient(testEndpoint);
            await testCreateObservationWithQuantity(client);
        }

        private static async Tasks.Task testCreateObservationWithQuantity(BaseFhirClient client)
        {
            var observation = new Observation
            {
                Status = ObservationStatus.Preliminary,
                Code = new CodeableConcept("http://loinc.org", "2164-2"),
                Value = new Quantity()
                {
                    System = "http://unitsofmeasure.org",
                    Value = 23,
                    Code = "mg",
                    Unit = "miligram"
                },
                BodySite = new CodeableConcept("http://snomed.info/sct", "182756003")
            };
            var fe = await client.CreateAsync(observation);
            fe = await client.ReadAsync<Observation>(fe.ResourceIdentity().WithoutVersion());
            Assert.IsInstanceOfType(fe.Value, typeof(Quantity));
        }


#if NO_ASYNC_ANYMORE
		/// <summary>
		/// This test is also used as a "setup" test for the History test.
		/// If you change the number of operations in here, this will make the History test fail.
		/// </summary>
		[TestMethod, TestCategory("FhirClient")]
		public void CreateEditDeleteAsync()
        {
            FhirClient client = new FhirClient(testEndpoint);
            testCreateEditDeleteAsync(client);
        }

        /// <summary>
        /// This test is also used as a "setup" test for the History test.
        /// If you change the number of operations in here, this will make the History test fail.
        /// </summary>
        [TestMethod, TestCategory("FhirClient")]
        public void CreateEditDeleteAsyncHttpClient()
        {

            using (FhirHttpClient client = new FhirHttpClient(testEndpoint))
            {
                testCreateEditDeleteAsync(client);
            }
        }

        private static void testCreateEditDeleteAsync(BaseFhirClient client)
        {
            var furore = new Organization
            {
                Name = "Furore",
                Identifier = new List<Identifier> { new Identifier("http://hl7.org/test/1", "3141") },
                Telecom = new List<ContactPoint> { new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = "+31-20-3467171" } }
            };

            var fe = client.CreateAsync<Organization>(furore).Result;

            Assert.IsNotNull(furore);
            Assert.IsNotNull(fe);
            Assert.IsNotNull(fe.Id);

            var createdTestOrganizationUrl = fe.Id;

            fe.Identifier.Add(new Identifier("http://hl7.org/test/2", "3141592"));
            var fe2 = client.UpdateAsync(fe).Result;

            Assert.IsNotNull(fe2);
            Assert.AreEqual(fe.Id, fe2.Id);



            fe.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
            var fe3 = client.UpdateAsync(fe2).Result;
            Assert.IsNotNull(fe3);
            Assert.AreEqual(3, fe3.Identifier.Count);

            client.DeleteAsync(fe3).Wait();

            try
            {
                // Get most recent version
                fe = client.ReadAsync<Organization>(new ResourceIdentity(fe.Id)).Result;
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(client.LastResult.Status == HttpStatusCode.Gone.ToString());
            }
        }
#endif

        /// <summary>
        /// This test will fail if the system records AuditEvents
        /// and counts them in the WholeSystemHistory
        /// </summary>
        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest"), Ignore]     // Keeps on failing periodically. Grahames server?
        public async Tasks.Task HistoryHttpClient()
        {
            FhirClient client = new FhirClient(testEndpoint);
            await testHistoryAsync(client);
        }

        private async Tasks.Task testHistoryAsync(BaseFhirClient client)
        {
            System.Threading.Thread.Sleep(500);
            DateTimeOffset timestampBeforeCreationAndDeletions = DateTimeOffset.Now;
            await testCreateEditDeleteAsync(client); // this test does a create, update, update, delete (4 operations)

            System.Diagnostics.Trace.WriteLine("History of this specific patient since just before the create, update, update, delete (4 operations)");

            Bundle history = await client.HistoryAsync(createdTestPatientUrl);
            Assert.IsNotNull(history);
            DebugDumpBundle(history);

            Assert.HasCount(4, history.Entry);
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            //// Now, assume no one is quick enough to insert something between now and the next
            //// tests....


            System.Diagnostics.Trace.WriteLine("\r\nHistory on the patient type");
            history = await client.TypeHistoryAsync("Patient", timestampBeforeCreationAndDeletions.ToUniversalTime());
            Assert.IsNotNull(history);
            DebugDumpBundle(history);
            Assert.HasCount(4, history.Entry);   // there's a race condition here, sometimes this is 5.
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());


            System.Diagnostics.Trace.WriteLine("\r\nHistory on the patient type (using the generic method in the client)");
            history = await client.TypeHistoryAsync<Patient>(timestampBeforeCreationAndDeletions.ToUniversalTime(), summary: SummaryType.True);
            Assert.IsNotNull(history);
            DebugDumpBundle(history);
            Assert.HasCount(4, history.Entry);
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            if (!testEndpoint.OriginalString.Contains("sqlonfhir-stu3"))
            {
                System.Diagnostics.Trace.WriteLine("\r\nWhole system history since the start of this test");
                history = await client.WholeSystemHistoryAsync(timestampBeforeCreationAndDeletions.ToUniversalTime());
                Assert.IsNotNull(history);
                DebugDumpBundle(history);
                Assert.IsLessThanOrEqualTo(history.Entry.Count, 4, "Whole System history should have at least 4 new events");
                // Check that the number of patients that have been created is what we expected
                Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null && entry.Resource is Patient).Count());
                Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted() && entry.Request.Url.Contains("Patient")).Count());
            }
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestWithParamHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                await gettWithParam(client);
            }
        }

        private static async Tasks.Task gettWithParam(BaseFhirClient client)
        {
            var res = await client.GetAsync("ValueSet/v2-0131/$validate-code?system=http://hl7.org/fhir/v2/0131&code=ep");
            Assert.IsNotNull(res);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task ManipulateMetaHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testManipulateMeta(client);
            }
        }

        private async Tasks.Task testManipulateMeta(BaseFhirClient client)
        {
            var pat = new Patient
            {
                Meta = new Meta()
            };
            var key = new Random().Next();
            pat.Meta.ProfileElement.Add(new FhirUri("http://someserver.org/fhir/StructureDefinition/XYZ1-" + key));
            pat.Meta.Security.Add(new Coding("http://mysystem.com/sec", "1234-" + key));
            pat.Meta.Tag.Add(new Coding("http://mysystem.com/tag", "sometag1-" + key));

            //Before we begin, ensure that our new tags are not actually used when doing System Meta()
            var wsm = await client.MetaAsync();
            Assert.IsNotNull(wsm);

            Assert.IsFalse(wsm.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ1-" + key));
            Assert.IsFalse(wsm.Security.Select(c => c.Code + "@" + c.System).Contains("1234-" + key + "@http://mysystem.com/sec"));
            Assert.IsFalse(wsm.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag1-" + key + "@http://mysystem.com/tag"));

            Assert.IsFalse(wsm.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
            Assert.IsFalse(wsm.Security.Select(c => c.Code + "@" + c.System).Contains("5678-" + key + "@http://mysystem.com/sec"));
            Assert.IsFalse(wsm.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag2-" + key + "@http://mysystem.com/tag"));


            // First, create a patient with the first set of meta
            var pat2 = await client.CreateAsync(pat);
            var loc = pat2.ResourceIdentity(testEndpoint);

            // Meta should be present on created patient
            verifyMeta(pat2.Meta, false, key);

            // Should be present when doing instance Meta()
            var par = await client.MetaAsync(loc);
            verifyMeta(par, false, key);

            // Should be present when doing type Meta()
            par = await client.MetaAsync(ResourceType.Patient);
            verifyMeta(par, false, key);

            // Should be present when doing System Meta()
            par = await client.MetaAsync();
            verifyMeta(par, false, key);

            // Now add some additional meta to the patient

            var newMeta = new Meta();
            newMeta.ProfileElement.Add(new FhirUri("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
            newMeta.Security.Add(new Coding("http://mysystem.com/sec", "5678-" + key));
            newMeta.Tag.Add(new Coding("http://mysystem.com/tag", "sometag2-" + key));


            await client.AddMetaAsync(loc, newMeta);
            var pat3 = await client.ReadAsync<Patient>(loc);

            // New and old meta should be present on instance
            verifyMeta(pat3.Meta, true, key);

            // New and old meta should be present on Meta()
            par = await client.MetaAsync(loc);
            verifyMeta(par, true, key);

            // New and old meta should be present when doing type Meta()
            par = await client.MetaAsync(ResourceType.Patient);
            verifyMeta(par, true, key);

            // New and old meta should be present when doing system Meta()
            par = await client.MetaAsync();
            verifyMeta(par, true, key);

            // Now, remove those new meta tags
            await client.DeleteMetaAsync(loc, newMeta);

            // Should no longer be present on instance
            var pat4 = await client.ReadAsync<Patient>(loc);
            verifyMeta(pat4.Meta, false, key);

            // Should no longer be present when doing instance Meta()
            par = await client.MetaAsync(loc);
            verifyMeta(par, false, key);

            // Should no longer be present when doing type Meta()
            par = await client.MetaAsync(ResourceType.Patient);
            verifyMeta(par, false, key);

            // clear out the client that we created, no point keeping it around
            await client.DeleteAsync(pat4);

            // Should no longer be present when doing System Meta()
            par = await client.MetaAsync();
            verifyMeta(par, false, key);
        }

        private void verifyMeta(Meta meta, bool hasNew, int key)
        {
            Assert.IsTrue(meta.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ1-" + key));
            Assert.IsTrue(meta.Security.Select(c => c.Code + "@" + c.System).Contains("1234-" + key + "@http://mysystem.com/sec"));
            Assert.IsTrue(meta.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag1-" + key + "@http://mysystem.com/tag"));

            if (hasNew)
            {
                Assert.IsTrue(meta.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
                Assert.IsTrue(meta.Security.Select(c => c.Code + "@" + c.System).Contains("5678-" + key + "@http://mysystem.com/sec"));
                Assert.IsTrue(meta.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag2-" + key + "@http://mysystem.com/tag"));
            }

            if (!hasNew)
            {
                Assert.IsFalse(meta.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
                Assert.IsFalse(meta.Security.Select(c => c.Code + "@" + c.System).Contains("5678-" + key + "@http://mysystem.com/sec"));
                Assert.IsFalse(meta.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag2-" + key + "@http://mysystem.com/tag"));
            }
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestSearchUsingPostMultipleIncludesShouldNotThrowArgumentExceptionHttpClient()
        {
            // This test case proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/1206 is fixed.
            // Previoulsly EntryToHttpExtensions.setBodyAndContentType used a Dictionary which required the
            // name part of the parameters to be unique.
            // Fixed by using IEnumerable<KeyValuePair<string, string>> instead of Dictionary<string, string>
            var client = new FhirClient(testEndpoint);
            await searchUsingPostWithIncludes(client);
        }

        private static async Tasks.Task searchUsingPostWithIncludes(BaseFhirClient client)
        {
            var sp = new SearchParams();
            sp.Parameters.Add(new Tuple<string, string>("_id", "8465,8479"));
            sp.Include.Add(("subject", IncludeModifier.Iterate));

            // Add a further include
            sp.Include.Add(("encounter", IncludeModifier.None));
            await client.SearchUsingPostAsync<Procedure>(sp);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestSearchByPersonaCodeHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                await searchByPersonaCode(client);
            }
        }

        private static async Tasks.Task searchByPersonaCode(BaseFhirClient client)
        {
            var pats =
            await client.SearchAsync<Patient>(new[] { string.Format("identifier={0}|{1}", "urn:oid:1.2.36.146.595.217.0.1", "12345") });
            var pat = (Patient)pats.Entry.First().Resource;
            Trace.WriteLine(pat);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestSearchUsingPostByPersonaCodeHttpClient()
        {
            using (var client = new FhirClient(_endpointSupportingSearchUsingPost))
            {
                await searchByPersonaCodeUsingPost(client);
            }
        }

        private static async Tasks.Task searchByPersonaCodeUsingPost(BaseFhirClient client)
        {
            var pats = await client.SearchUsingPostAsync<Patient>(new[] { string.Format("identifier={0}|{1}", "urn:oid:1.2.36.146.595.217.0.1", "12345") }, new[] { "generalPractitioner" }, null, null, null);
            var pat = (Patient)pats.Entry.First().Resource;
            Trace.WriteLine(pat);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CreateDynamicHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                await testCreateDynamicHttpClientAsync(client);
            }
        }

        private static async Tasks.Task testCreateDynamicHttpClientAsync(BaseFhirClient client)
        {
            Resource furore = new Organization
            {
                Name = "Furore",
                Identifier = new List<Identifier> { new Identifier("http://hl7.org/test/1", "3141") },
                Telecom = new List<ContactPoint> {
                    new ContactPoint { System = ContactPoint.ContactPointSystem.Phone, Value = "+31-20-3467171", Use = ContactPoint.ContactPointUse.Work },
                    new ContactPoint { System = ContactPoint.ContactPointSystem.Fax, Value = "+31-20-3467172" }
                }
            };

            System.Diagnostics.Trace.WriteLine(await new FhirXmlSerializer().SerializeToStringAsync(furore));
            var fe = await client.CreateAsync(furore);
            Assert.IsNotNull(fe);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CallsCallbacksHttpClientHandler()
        {
            using (var handler = new HttpClientEventHandler()
            {
#pragma warning disable SYSLIB0039
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
#pragma warning restore SYSLIB0039
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,                
            })
            {
                using (FhirClient client = new FhirClient(testEndpoint, messageHandler: handler))
                {
                    client.Settings.ParserSettings = client.Settings.ParserSettings with { AllowUnrecognizedEnums = true };

                    bool calledBefore = false;
                    HttpStatusCode? status = null;
                    byte[] body = null;
                    byte[] bodyOut = null;

                    handler.OnBeforeRequest += (sender, e) =>
                    {
                        calledBefore = true;
                        bodyOut = e.Body;
                    };

                    handler.OnAfterResponse += (sender, e) =>
                    {
                        body = e.Body;
                        status = e.RawResponse.StatusCode;
                    };

                    var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.Contains("<Patient", bodyText);

                    calledBefore = false;
                    await client.UpdateAsync(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.Contains("<Patient", bodyText);
                }

                // And use another on the same handler to ensure that it wasn't disposed :O
                using (FhirClient client = new FhirClient(testEndpoint, messageHandler: handler))
                {
                    client.Settings.ParserSettings = client.Settings.ParserSettings with { AllowUnrecognizedEnums = true };

                    bool calledBefore = false;
                    HttpStatusCode? status = null;
                    byte[] body = null;
                    byte[] bodyOut = null;

                    handler.OnBeforeRequest += (sender, e) =>
                    {
                        calledBefore = true;
                        bodyOut = e.Body;
                    };

                    handler.OnAfterResponse += (sender, e) =>
                    {
                        body = e.Body;
                        status = e.RawResponse.StatusCode;
                    };

                    var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.Contains("<Patient", bodyText);

                    calledBefore = false;
                    await client.UpdateAsync(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.Contains("<Patient", bodyText);
                }
            }
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task CallsCallbacksHttpClient()
        {
            using (var handler = new HttpClientEventHandler()
            {
#pragma warning disable SYSLIB0039
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
#pragma warning restore SYSLIB0039
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,                
            })
            using (var httpClient = new HttpClient(handler))
            {
                using (FhirClient client = new FhirClient(testEndpoint, httpClient: httpClient))
                {
                    client.Settings.ParserSettings = client.Settings.ParserSettings with { AllowUnrecognizedEnums = true };

                    bool calledBefore = false;
                    HttpStatusCode? status = null;
                    byte[] body = null;
                    byte[] bodyOut = null;

                    handler.OnBeforeRequest += (sender, e) =>
                    {
                        calledBefore = true;
                        bodyOut = e.Body;
                    };

                    handler.OnAfterResponse += (sender, e) =>
                    {
                        body = e.Body;
                        status = e.RawResponse.StatusCode;
                    };

                    var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.Contains("<Patient", bodyText);

                    calledBefore = false;
                    await client.UpdateAsync(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.Contains("<Patient", bodyText);
                }

                // And use another on the same handler to ensure that it wasn't disposed :O
                using (FhirClient client = new FhirClient(testEndpoint, httpClient: httpClient))
                {
                    client.Settings.ParserSettings = client.Settings.ParserSettings with { AllowUnrecognizedEnums = true };

                    bool calledBefore = false;
                    HttpStatusCode? status = null;
                    byte[] body = null;
                    byte[] bodyOut = null;

                    handler.OnBeforeRequest += (sender, e) =>
                    {
                        calledBefore = true;
                        bodyOut = e.Body;
                    };

                    handler.OnAfterResponse += (sender, e) =>
                    {
                        body = e.Body;
                        status = e.RawResponse.StatusCode;
                    };

                    var pat = await client.ReadAsync<Patient>("Patient/" + patientId);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.Contains("<Patient", bodyText);

                    calledBefore = false;
                    await client.UpdateAsync(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.Contains("<Patient", bodyText);
                }
            }
        }

        //[TestMethod]
        //public void TestBinaryDetection()
        //{
        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary", null));
        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary?param=x", null));
        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/_history", null));

        //    Assert.IsTrue(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2", null));
        //    Assert.IsTrue(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2/_history/1", null));

        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2", "application/fhir+xml"));
        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2/_history/1", "application/fhir+json"));

        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/ValueSet/extensional-case-1/$expand?filter=f", null));
        //    Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/ValueSet/extensional-case-1/$expand%3Ffilter=f", null));
        //}

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task RequestFullResourceHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                await testRequestFullResource(client);
            }
        }

        private static async Tasks.Task testRequestFullResource(BaseFhirClient client)
        {
            var result = await client.ReadAsync<Patient>("Patient/glossy");
            Assert.IsNotNull(result);
            result.Id = null;
            result.Meta = null;

            client.Settings.ReturnPreference = ReturnPreference.Representation;
            var posted = await client.CreateAsync(result);
            Assert.IsNotNull(posted, "Patient example not found");

            posted = await client.CreateAsync(result);
            Assert.IsNotNull(posted, "Did not return a resource, even when ReturnFullResource=true");

            client.Settings.ReturnPreference = ReturnPreference.Minimal;
            posted = await client.CreateAsync(result);
            Assert.IsNull(posted);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]   // Currently ignoring, as spark.furore.com returns Status 500.
        public async Tasks.Task TestReceiveHtmlIsHandledHttpClient()
        {
            using var client = new FhirClient("http://spark.furore.com/");        // an address that returns html
            
            try
            {
                var pat = await client.ReadAsync<Patient>("Patient/1");
                Trace.WriteLine(pat);
            }
            catch (FhirOperationException fe)
            {
                if (!fe.Message.Contains("a valid FHIR xml/json body type was expected") && !fe.Message.Contains("not recognized as either xml or json"))
                    Assert.Fail("Failed to recognize invalid body contents");
            }
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestRefreshHttpClient()
        {
            using var client = new FhirClient(testEndpoint);
            await clientReadRefresh(client);
        }

        private static async Tasks.Task clientReadRefresh(BaseFhirClient client)
        {
            var result = await client.ReadAsync<Patient>("Patient/" + patientId);

            var orig = result.Name[0].FamilyElement.Value;

            result.Name[0].FamilyElement.Value = "overwritten name";

            result = await client.RefreshAsync(result);

            Assert.AreEqual(orig, result.Name[0].FamilyElement.Value);
        }

        private static async Tasks.Task testHandlingHtmlErrorStatus(BaseFhirClient client)
        {
            try
            {
                var pat = await client.ReadAsync<Patient>("Patient/1");
                Trace.WriteLine(pat);
                Assert.Fail("Failed to throw an Exception on status 500");
            }
            catch (FhirOperationException fe)
            {
                // Expected exception happened
                if (fe.Status != HttpStatusCode.InternalServerError)
                    Assert.Fail("Server response of 500 did not result in FhirOperationException with status 500.");

                if (client.LastResult == null)
                    Assert.Fail("LastResult not set in error case.");

                if (client.LastResult.Status != "500")
                    Assert.Fail("LastResult.Status is not 500.");

                if (!fe.Message.Contains("a valid FHIR xml/json body type was expected") && !fe.Message.Contains("not recognized as either xml or json"))
                    Assert.Fail("Failed to recognize invalid body contents");

                // Check that LastResult is of type OperationOutcome and properly filled.
                OperationOutcome operationOutcome = client.LastBodyAsResource as OperationOutcome;
                Assert.IsNotNull(operationOutcome, "Returned resource is not an OperationOutcome");

                Assert.IsNotEmpty(operationOutcome.Issue, "OperationOutcome does not contain an issue");

                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, operationOutcome.Issue[0].Severity, "OperationOutcome is not of severity 'error'");

                string message = operationOutcome.Issue[0].Diagnostics;
                if (!message.Contains("a valid FHIR xml/json body type was expected") && !message.Contains("not recognized as either xml or json"))
                    Assert.Fail("Failed to carry error message over into OperationOutcome");
            }
            catch (Exception)
            {
                Assert.Fail("Failed to throw FhirOperationException on status 500");
            }
        }


        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Tasks.Task TestReceiveErrorStatusWithOperationOutcomeIsHandledHttpClient()
        {
            using var client = new FhirClient("http://test.fhir.org/r3");// an address that returns Status 404 with an OperationOutcome
            await testHandlingErrorStatusAsOperationOutcome(client);
        }

        private static async Tasks.Task testHandlingErrorStatusAsOperationOutcome(BaseFhirClient client)
        {
            try
            {
                var pat = await client.ReadAsync<Patient>("Patient/doesnotexist");
                Trace.WriteLine(pat);
                Assert.Fail("Failed to throw an Exception on status 404");
            }
            catch (FhirOperationException fe)
            {
                // Expected exception happened
                if (fe.Status != HttpStatusCode.NotFound)
                    Assert.Fail("Server response of 404 did not result in FhirOperationException with status 404.");

                if (client.LastResult == null)
                    Assert.Fail("LastResult not set in error case.");

                Bundle.ResponseComponent entryComponent = client.LastResult;

                if (entryComponent.Status != "404")
                    Assert.Fail("LastResult.Status is not 404.");

                // Check that LastResult is of type OperationOutcome and properly filled.
                OperationOutcome operationOutcome = client.LastBodyAsResource as OperationOutcome;
                Assert.IsNotNull(operationOutcome, "Returned resource is not an OperationOutcome");

                Assert.IsNotEmpty(operationOutcome.Issue, "OperationOutcome does not contain an issue");

                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, operationOutcome.Issue[0].Severity, "OperationOutcome is not of severity 'error'");
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to throw FhirOperationException on status 404: " + e.Message);
            }
        }

        [TestMethod, TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Tasks.Task TestAuthenticationOnBeforeHttpClient()
        {
            using (FhirClient client = new FhirClient(testEndpoint))
            {
                client.RequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "bad-bearer");
                await testAuthentication(client);
            }
        }

        private static async Tasks.Task testAuthentication(BaseFhirClient validationFhirClient)
        {
            try
            {
                var pat = await validationFhirClient.ValidateResourceAsync(new Patient());
                Trace.WriteLine(pat);

            }
            catch (FhirOperationException ex)
            {
                Assert.IsTrue(ex.Status == HttpStatusCode.Forbidden || ex.Status == HttpStatusCode.Unauthorized, "Excpeted a security exception");
            }
        }

        /// <summary>
        /// Test for showing issue https://github.com/FirelyTeam/firely-net-sdk/issues/128
        /// </summary>
        [TestMethod, TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Tasks.Task TestCreatingBinaryResourceHttpClient()
        {
            byte[] arr = File.ReadAllBytes(TestDataHelper.GetFullPathForExample(@"fhir-logo.png"));
            using (var client = new FhirClient(testEndpoint))
            {
                var binary = new Binary() { Content = arr, ContentType = "image/png" };
                var result = await client.CreateAsync(binary);

                Assert.IsNotNull(result);

                var result2 = await client.GetAsync($"Binary/{result.Id}");
                Assert.IsNotNull(result2);
                Assert.IsInstanceOfType(result2, typeof(Binary));
                Assert.IsNotNull(result2.Id, "Binary resource should have an Id");
                Assert.AreEqual(result2.Id, result.Id);
                Assert.IsNotNull(result2.Meta?.VersionId, "Binary resource should have an Version");
            }
        }

        /// <summary>
        /// Test for showing issue https://github.com/FirelyTeam/firely-net-sdk/issues/1681
        /// </summary>
        [TestMethod, TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Tasks.Task TestPreferOperationOutcome()
        {
            var client = new FhirClient(testEndpoint);
            client.Settings.ReturnPreference = ReturnPreference.OperationOutcome;

            var pat = new Patient()
            {
                Name = new List<HumanName> { new HumanName().WithGiven("testy").AndFamily("McTestFace") }
            };
            var pat2 = await client.CreateAsync(pat);
            Trace.WriteLine(pat2);

            Assert.IsNotNull(client.LastResult.Outcome);
        }

        private static async Tasks.Task testOpEverything(BaseFhirClient client)
        {

            // GET operation $everything without parameters
            var loc = await client.TypeOperationAsync<Patient>("everything", null, useGet: true);
            Assert.IsNotNull(loc);

            // POST operation $everything without parameters
            loc = await client.TypeOperationAsync<Patient>("everything", null, useGet: false);
            Assert.IsNotNull(loc);



            // GET operation $everything with 1 primitive parameter
            loc = await client.TypeOperationAsync<Patient>("everything", new Parameters().Add("start", new Date(2017, 11)), useGet: true);
            Assert.IsNotNull(loc);

            // GET operation $everything with 1 primitive2token parameter
            loc = await client.TypeOperationAsync<Patient>("everything", new Parameters().Add("start", new Identifier("", "example")), useGet: true);
            Assert.IsNotNull(loc);

            // GET operation $everything with 1 resource parameter
            try
            {
                loc = await client.TypeOperationAsync<Patient>("everything", new Parameters().Add("start", new Patient()), useGet: true);
                Assert.Fail("An InvalidOperationException was expected here");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException), ex.Message);
            }

            // GET operation $everything with 1 complex parameter
            try
            {
                loc = await client.TypeOperationAsync<Patient>("everything", new Parameters().Add("start", new Annotation() { Text = "test" }), useGet: true);
                Assert.Fail("An InvalidOperationException was expected here");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException), ex.Message);
            }

            // POST operation $everything with 1 parameter
            loc = await client.TypeOperationAsync<Patient>("everything", new Parameters().Add("start", new Date(2017, 10)), useGet: false);
            Assert.IsNotNull(loc);
        }

        [TestMethod, TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Tasks.Task TestMultipleMessageHandlersInFhirClient()
        {
            var testMessageHandler = new TestMessageHandler()
            {
#pragma warning disable SYSLIB0039
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
#pragma warning restore SYSLIB0039
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,                
            };
            var testDegatingHandler = new TestDeligatingHandler()
            {
                InnerHandler = testMessageHandler
            };

            using var client = new FhirClient(testEndpoint, settings: FhirClientSettings.CreateDefault(), testDegatingHandler);
            var l = await client.ReadAsync<Location>("Location/" + locationId);
            Trace.WriteLine(l);
            Assert.IsNotNull(testDegatingHandler.LastRequest);
            Assert.IsNotNull(testMessageHandler.LastResponse);
        }
    }

    internal class TestDeligatingHandler : DelegatingHandler
    {
        public HttpRequestMessage LastRequest { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
    internal class TestMessageHandler : HttpClientHandler
    {
        public HttpResponseMessage LastResponse { get; set; }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            LastResponse = response;
            return response;
        }
    }


}
#pragma warning restore CS0618 // Type or member is obsolete