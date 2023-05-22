#pragma warning disable CS0618 // Type or member is obsolete

/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T = System.Threading.Tasks;

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
        //public static Uri testEndpoint = new Uri("http://localhost:49911/fhir");
        //public static Uri testEndpoint = new Uri("http://sqlonfhir-r4.azurewebsites.net/fhir");

#if R3
        public static readonly Uri TestEndpoint = new Uri("https://server.fire.ly/r3");
        public static readonly string FhirReleaseString =  "R3";
#elif R4
        public static readonly Uri TestEndpoint = new Uri("https://server.fire.ly/r4");
        public static readonly string FhirReleaseString =  "R4";
#elif R4B
        public static readonly Uri TestEndpoint = new Uri("https://server.fire.ly/r4");   // there is no FS for R4B
        public static readonly string FhirReleaseString =  "R4";
#elif R5
        public static readonly Uri TestEndpoint = new Uri("https://server.fire.ly/r5");
        public static readonly string FhirReleaseString = "R5";
#else
        Add another endpoint here
#endif

        public static readonly Uri TerminologyEndpoint = new Uri("https://r4.ontoserver.csiro.au/fhir");
        // public static Uri TerminologyEndpoint = new Uri("http://test.fhir.org/r4");

        internal static readonly string PATIENTID = $"pat1{ModelInfo.Version}.{FhirReleaseString}";
        internal static readonly string PATIENTIDEP = $"Patient/{PATIENTID}";
        internal static readonly string LOCATIONID = $"loc1{ModelInfo.Version}.{FhirReleaseString}";
        internal static readonly string LOCATIONIDEP = $"Location/{LOCATIONID}";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // Ignore SSL certificate errors
            ServicePointManager.ServerCertificateValidationCallback += (a, b, c, d) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls13;

            CreateItems();
        }

        internal static void CreateItems()
        {
            var client = new FhirClient(TestEndpoint);

            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            var pat = new Patient()
            {
                BirthDate = "1972-11-30",
                Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = "Donald",
                    }
                },
                Id = PATIENTID,
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
                Id = LOCATIONID
            };

            // Create the patient
            Console.WriteLine($"Upserting patient {PATIENTID}.");
            Patient p = client.Update(pat);
            Assert.IsNotNull(p);

            Console.WriteLine($"Upserting location {LOCATIONID}.");
            Location l = client.Update(loc);

            Assert.IsNotNull(l);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            Console.WriteLine("Testing against fhir server: " + TestEndpoint);
        }    

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task FetchConformanceHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            client.Settings.ParserSettings.AllowUnrecognizedEnums = true;
            var entry = await client.CapabilityStatementAsync();

            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.FhirVersion);
            Assert.AreEqual(CapabilityStatement.RestfulCapabilityMode.Server, entry.Rest[0].Mode.Value);
            Assert.AreEqual("200", client.LastResult.Status);

            entry = client.CapabilityStatement(SummaryType.True);

            Assert.IsNull(entry.Text); // DSTU2 has this property as not include as part of the summary (that would be with SummaryType.Text)
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.FhirVersion);
            Assert.AreEqual(CapabilityStatement.RestfulCapabilityMode.Server, entry.Rest[0].Mode.Value);
            Assert.AreEqual("200", client.LastResult.Status);

            Assert.IsNotNull(entry.Rest[0].Resource, "The resource property should be in the summary");
            Assert.AreNotEqual(0, entry.Rest[0].Resource.Count, "There is expected to be at least 1 resource defined in the conformance statement");
            Assert.IsTrue(entry.Rest[0].Resource[0].Type is not null, "The resource type should be provided");
            Assert.AreNotEqual(0, entry.Rest[0].Operation.Count, "operations should be listed in the summary"); // actually operations are now a part of the summary
        }

        [TestMethod, Ignore("FS Endpoint returns not implemented"), TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task PatchHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            var patchparams = new Parameters();
            patchparams.AddAddPatchParameter("Patient", "birthdate", new Date("1930-01-01"));
            var patched = await client.PatchAsync<Patient>(PATIENTIDEP, patchparams);
            patched.BirthDate.Should().Be("1930-01-01");
        }

        [TestMethod, Ignore("FS Endpoint does not support conditional PATCH"), TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task CondionalPatchHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            var patchparams = new Parameters();
            patchparams.AddAddPatchParameter("Patient", "birthdate", new Date("1930-01-01"));
            var condition = new SearchParams().Where("name=Donald");
            await client.PatchAsync<Patient>(condition, patchparams);
        }

        [TestMethod(), TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        [DataRow(ResourceFormat.Json)]
        [DataRow(ResourceFormat.Xml)]
        public async T.Task ReadWithFormatHttpClient(ResourceFormat fmt)
        {
            using var client = new FhirClient(TestEndpoint);
            client.Settings.PreferredFormat = fmt;

            var pat = await client.ReadAsync<Patient>(PATIENTIDEP);
            Assert.IsNotNull(pat);
        }

        [TestMethod(), TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task ReadWithCompressionReceive()
        {
            using var client = new FhirClient(TestEndpoint, new FhirClientSettings { PreferCompressedResponses = true });
            var pat = await client.ReadAsync<Patient>(PATIENTIDEP);
            Assert.IsNotNull(pat);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        [ExpectedException(typeof(FhirOperationException))]
        public void ReadWrongResourceTypeHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);
            var l = client.Read<Patient>(LOCATIONIDEP);
            Trace.WriteLine(l);
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task ReadHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            var loc = await client.ReadAsync<Location>(LOCATIONIDEP);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            Assert.AreEqual(LOCATIONID, loc.Id);
            Assert.IsNotNull(loc.Meta.VersionId);

            var loc2 = client.Read<Location>(ResourceIdentity.Build("Location", LOCATIONID, loc.Meta.VersionId));
            Assert.IsNotNull(loc2);
            Assert.AreEqual(loc2.Id, loc.Id);
            Assert.AreEqual(loc2.Meta.VersionId, loc.Meta.VersionId);

            try
            {
                var random = await client.ReadAsync<Location>(new Uri("Location/45qq54", UriKind.Relative));
                Assert.Fail();
            }
            catch (FhirOperationException ex)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Status);
                Assert.AreEqual("404", client.LastResult.Status);
            }

            var loc3 = await client.ReadAsync<Location>(ResourceIdentity.Build("Location", LOCATIONID, loc.Meta.VersionId));
            Assert.IsNotNull(loc3);
            var jsonSer = new FhirJsonSerializer();
            Assert.AreEqual(await jsonSer.SerializeToStringAsync(loc),
                await jsonSer.SerializeToStringAsync(loc3));

            var loc4 = client.Read<Location>(loc.ResourceIdentity());
            Assert.IsNotNull(loc4);
            Assert.AreEqual(await jsonSer.SerializeToStringAsync(loc),
                await jsonSer.SerializeToStringAsync(loc4));
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public void ReadRelativeHttpClient()
        {
            using FhirClient client = new FhirClient(TestEndpoint);

            var loc = client.Read<Location>(new Uri(LOCATIONIDEP, UriKind.Relative));
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            var ri = ResourceIdentity.Build(TestEndpoint, "Location", LOCATIONID);
            loc = client.Read<Location>(ri);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task SearchHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            var result = client.Search<Patient>();
            Assert.IsNotNull(result);
            
            result = await client.SearchAsync<DiagnosticReport>(pageSize: 2);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count <= 2);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchInvalidCriteriaHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);
            var result = client.Search<Patient>(new string[] { "test" });
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        [DataRow(ResourceFormat.Xml)]
        [DataRow(ResourceFormat.Json)]
        public void PagingHttpClient(ResourceFormat format)
        {
            using var client = new FhirClient(TestEndpoint);
            client.Settings.PreferredFormat = format;

            var result = client.Search<Patient>(pageSize: 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(2,result.Entry.Count);

            var firstId = result.Entry.First().Resource.Id;

            // Browse forward
            result = client.Continue(result);
            Assert.IsNotNull(result);
            var nextId = result.Entry.First().Resource.Id;
            Assert.AreNotEqual(firstId, nextId);

            // Browse to first
            result = client.Continue(result, PageDirection.First);
            Assert.IsNotNull(result);
            var prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);

            // Forward, then backwards
            result = client.Continue(result, PageDirection.Next);
            Assert.IsNotNull(result);
            result = client.Continue(result, PageDirection.Previous);
            Assert.IsNotNull(result);
            prevId = result.Entry.First().Resource.Id;
            Assert.AreEqual(firstId, prevId);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task CreateAndFullRepresentationHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;       // which is also the default

            var pat = await client.ReadAsync<Patient>(PATIENTIDEP);
            ResourceIdentity ri = pat.ResourceIdentity().WithBase(client.Endpoint);
            pat.Id = null;
            pat.Identifier.Clear();
            var patC = await client.CreateAsync(pat);
            Assert.IsNotNull(patC);

            client.Settings.PreferredReturn = Prefer.ReturnMinimal;
            patC = await client.CreateAsync(pat);

            Assert.IsNull(patC);

            if (client.LastBody != null && client.LastBody.Length > 0)
            {
                var returned = client.LastBodyAsResource;
                Assert.IsTrue(returned is OperationOutcome);
            }

            // Now validate this resource
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;      // which is also the default
            var p = new Parameters
            {
                { "resource", pat }
            };
            var oI = await client.InstanceOperationAsync(ri.WithoutVersion(), "validate", p);
            oI.Should().BeOfType<OperationOutcome>();
        }

        private Uri createdTestPatientUrl = null;

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task CreateEditDeleteHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);
            await testCreateEditDeleteAsync(client);
        }

        private async T.Task testCreateEditDeleteAsync(BaseFhirClient client)
        {
            var pat = client.Read<Patient>(PATIENTIDEP);
            pat.Id = null;
            pat.Identifier.Clear();
            pat.Identifier.Add(new Identifier("http://hl7.org/test/2", "99999"));

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
            Assert.AreEqual(2, fe2.Identifier.Count);

            fe.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
            var fe3 = client.Update(fe);
            Assert.IsNotNull(fe3);
            Assert.AreEqual(3, fe3.Identifier.Count);

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


        /// <summary>
        /// This test will fail if the system records AuditEvents
        /// and counts them in the WholeSystemHistory
        /// </summary>
        [Ignore("Interestingly, the history count results varies over time.")]
        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task HistoryHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);

            System.Threading.Thread.Sleep(500);
            DateTimeOffset timestampBeforeCreationAndDeletions = DateTimeOffset.Now;
            await testCreateEditDeleteAsync(client); // this test does a create, update, update, delete (4 operations)

            Bundle history = await client.HistoryAsync(createdTestPatientUrl);
            Assert.IsNotNull(history);

            Assert.AreEqual(4, history.Entry.Count);
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            //// Now, assume no one is quick enough to insert something between now and the next
            //// tests....

            history = await client.TypeHistoryAsync("Patient", timestampBeforeCreationAndDeletions.ToUniversalTime());
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entry.Count);   // there's a race condition here, sometimes this is 5.
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            history = await client.TypeHistoryAsync<Patient>(timestampBeforeCreationAndDeletions.ToUniversalTime(), summary: SummaryType.True);
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entry.Count);
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            if (!TestEndpoint.OriginalString.Contains("sqlonfhir-stu3"))
            {
                history = await client.WholeSystemHistoryAsync(timestampBeforeCreationAndDeletions.ToUniversalTime());
                Assert.IsNotNull(history);
                Assert.IsTrue(4 <= history.Entry.Count, "Whole System history should have at least 4 new events");
                // Check that the number of patients that have been created is what we expected
                Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null && entry.Resource is Patient).Count());
                Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted() && entry.Request.Url.Contains("Patient")).Count());
            }
        }

        [Ignore("FS returns not implemented.")]
        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task ManipulateMetaHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);
            
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
            var loc = pat2.ResourceIdentity(TestEndpoint);

            // Meta should be present on created patient
            verifyMeta(pat2.Meta, false, key);

            // Should be present when doing instance Meta()
            var par = await client.MetaAsync(loc);
            verifyMeta(par, false, key);

            // Should be present when doing type Meta()
            par = client.Meta(ResourceType.Patient);  // Sync for a change
            verifyMeta(par, false, key);

            // Should be present when doing System Meta()
            par = await client.MetaAsync();
            verifyMeta(par, false, key);

            // Now add some additional meta to the patient

            var newMeta = new Meta();
            newMeta.ProfileElement.Add(new FhirUri("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
            newMeta.Security.Add(new Coding("http://mysystem.com/sec", "5678-" + key));
            newMeta.Tag.Add(new Coding("http://mysystem.com/tag", "sometag2-" + key));

            client.AddMeta(loc, newMeta);
            var pat3 = await client.ReadAsync<Patient>(loc);

            // New and old meta should be present on instance
            verifyMeta(pat3.Meta, true, key);

            // New and old meta should be present on Meta()
            par = client.Meta(loc);
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
        public async T.Task TestSearchUsingPostMultipleIncludesShouldNotThrowArgumentExceptionHttpClient()
        {
            // This test case proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/1206 is fixed.
            // Previoulsly EntryToHttpExtensions.setBodyAndContentType used a Dictionary which required the
            // name part of the parameters to be unique.
            // Fixed by using IEnumerable<KeyValuePair<string, string>> instead of Dictionary<string, string>
            var client = new FhirClient(TestEndpoint);
           
            var sp = new SearchParams();
            sp.Parameters.Add(new Tuple<string, string>("_id", "8465,8479"));
            sp.Include.Add(("subject", IncludeModifier.Iterate));

            // Add a further include
            sp.Include.Add(("encounter", IncludeModifier.None));
            _ = await client.SearchUsingPostAsync<Procedure>(sp);

            // just shouldn't throw.
        }
                      
        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task CallsCallbacksHttpClientHandler()
        {
            using var handler = new HttpClientEventHandler();

            using (FhirClient client = new FhirClient(TestEndpoint, messageHandler: handler))
                await check(handler, client);

            // And use another on the same handler to ensure that it wasn't disposed :O
            using (FhirClient client = new(TestEndpoint, messageHandler: handler))
                await check(handler, client);               

            static async T.Task check(HttpClientEventHandler handler, FhirClient client)
            {
                client.Settings.ParserSettings.AllowUnrecognizedEnums = true;

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

                var pat = await client.ReadAsync<Patient>(PATIENTIDEP);
                Assert.IsTrue(calledBefore);
                Assert.IsNotNull(status);
                Assert.IsNotNull(body);

                var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                Assert.IsTrue(bodyText.Contains("<Patient"));

                calledBefore = false;
                client.Update(pat); // create cannot be called with an ID (which was retrieved)
                Assert.IsTrue(calledBefore);
                Assert.IsNotNull(bodyOut);

                bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                Assert.IsTrue(bodyText.Contains("<Patient"));
            }
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public void CallsCallbacksHttpClient()
        {
            using (var handler = new HttpClientEventHandler())
            using (var httpClient = new HttpClient(handler))
            {
                using (FhirClient client = new FhirClient(TestEndpoint, httpClient: httpClient))
                {
                    client.Settings.ParserSettings.AllowUnrecognizedEnums = true;

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

                    var pat = client.Read<Patient>("Patient/" + PATIENTID);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.IsTrue(bodyText.Contains("<Patient"));

                    calledBefore = false;
                    client.Update(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.IsTrue(bodyText.Contains("<Patient"));
                }

                // And use another on the same handler to ensure that it wasn't disposed :O
                using (FhirClient client = new FhirClient(TestEndpoint, httpClient: httpClient))
                {
                    client.Settings.ParserSettings.AllowUnrecognizedEnums = true;

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

                    var pat = client.Read<Patient>("Patient/" + PATIENTID);
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(status);
                    Assert.IsNotNull(body);

                    var bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);

                    Assert.IsTrue(bodyText.Contains("<Patient"));

                    calledBefore = false;
                    client.Update(pat); // create cannot be called with an ID (which was retrieved)
                    Assert.IsTrue(calledBefore);
                    Assert.IsNotNull(bodyOut);

                    bodyText = HttpUtil.DecodeBody(body, Encoding.UTF8);
                    Assert.IsTrue(bodyText.Contains("<Patient"));
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
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]   // Currently ignoring, as spark.furore.com returns Status 500.
        public async T.Task TestReceiveHtmlIsHandledHttpClient()
        {
            using var client = new FhirClient("http://nu.nl");        // an address that returns html
            
            try
            {
                var pat = await client.GetAsync("/");
                Assert.Fail("Failed to recognize invalid body contents");
            }
            catch (FhirOperationException fe)
            {
                fe.Message.Should().Contain("Endpoint returned a body with contentType");
            }
        }

        [TestMethod, TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task TestRefreshHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);                   
            var result = client.Read<Patient>("Patient/" + PATIENTID);

            var orig = result.Name[0].FamilyElement.Value;

            result.Name[0].FamilyElement.Value = "overwritten name";

            result = await client.RefreshAsync(result);

            Assert.AreEqual(orig, result.Name[0].FamilyElement.Value);
        }

      
        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async T.Task TestReceiveErrorStatusIsHandledHttpClient()
        {
            using var client = new FhirClient(TestEndpoint);// an address that returns Status 404 with an OperationOutcome
            
            try
            {
                var pat = await client.ReadAsync<Patient>("Patient/doesnotexist");
                Assert.Fail("Failed to throw an Exception on status 404");
            }
            catch (FhirOperationException fe)
            {
                // Expected exception happened
                fe.Status.Should().Be(HttpStatusCode.NotFound, 
                    "Server response of 404 did not result in FhirOperationException with status 404.");

                client.LastResult.Should().NotBeNull("LastResult not set in error case.");

                Bundle.ResponseComponent entryComponent = client.LastResult;

                entryComponent.Status.Should().Be("404");
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to throw FhirOperationException on status 404: " + e.Message);
            }
        }

        [TestMethod, TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async T.Task TestAuthenticationOnBeforeHttpClient()
        {
            using FhirClient client = new FhirClient(TestEndpoint);
            client.RequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "bad-bearer");
            
            try
            {
                var output = await client.ValidateResourceAsync(new Patient());

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
        public async T.Task TestCreatingBinaryResourceHttpClient()
        {
            byte[] arr = File.ReadAllBytes(TestDataHelper.GetFullPathForExample(@"fhir-logo.png"));
            using (var client = new FhirClient(TestEndpoint))
            {
                var binary = new Binary() { Data = arr, ContentType = "image/png" };
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
        public async T.Task TestPreferOperationOutcome()
        {
            FhirClient client = new FhirClient(TestEndpoint);
            client.Settings.PreferredReturn = Prefer.OperationOutcome;

            var pat = new Patient()
            {
                Name = new List<HumanName> { new HumanName().WithGiven("testy").AndFamily("McTestFace") }
            };
            var p = await client.CreateAsync(pat);
            p.Should().BeNull();
            Assert.IsNotNull(client.LastResult.Outcome);
        }


        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async T.Task TestMultipleMessageHandlersInFhirClient()
        {
            var testMessageHandler = new TestMessageHandler();
            var testDegatingHandler = new TestDeligatingHandler()
            {
                InnerHandler = testMessageHandler
            };

            using var client = new FhirClient(TestEndpoint, settings: FhirClientSettings.CreateDefault(), testDegatingHandler);
            var loc = await client.ReadAsync<Location>(LOCATIONIDEP);
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