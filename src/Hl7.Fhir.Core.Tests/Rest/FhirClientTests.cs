/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Net;
using System.IO;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirClientTests
#else
    public class FhirClientTests
#endif
    {
        //Uri testEndpoint = new Uri("http://spark-dstu2.furore.com/fhir");
        // Uri testEndpoint = new Uri("http://localhost.fiddler:1396/fhir");
        // Uri testEndpoint = new Uri("http://localhost:1396/fhir");
        Uri testEndpoint = new Uri("http://fhir2.healthintersections.com.au/open");
        // Uri testEndpoint = new Uri("https://api.fhir.me");
        // Uri testEndpoint = new Uri("http://fhirtest.uhn.ca/baseDstu2");
        //Uri testEndpoint = new Uri("http://localhost:49911/fhir");
        // Uri testEndpoint = new Uri("http://sqlonfhir-dstu2.azurewebsites.net/fhir");

        [TestInitialize]
        public void TestInitialize()
        {
            System.Diagnostics.Trace.WriteLine("Testing against fhir server: " + testEndpoint);
        }

        [TestMethod, TestCategory("FhirClient")]
        public void FetchConformance()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var entry = client.Conformance();

            Assert.IsNotNull(entry);
            // Assert.AreEqual("Spark.Service", c.Software.Name); // This is only for ewout's server
            Assert.AreEqual(Conformance.RestfulConformanceMode.Server, entry.Rest[0].Mode.Value);
            Assert.AreEqual("200", client.LastResult.Status);
        }


        [TestMethod, TestCategory("FhirClient")]
        public void VerifyFormatParamProcessing()
        {
            // XML
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromFormatParam("xml"));
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromFormatParam("text/xml"));
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromFormatParam("application/xml"));
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromFormatParam("application/xml+fhir"));
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromFormatParam("application/fhir+xml"));

            // JSON
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromFormatParam("json"));
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromFormatParam("text/json"));
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromFormatParam("application/json"));
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromFormatParam("application/json+fhir"));
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromFormatParam("application/fhir+json"));
        }

        [TestMethod, TestCategory("FhirClient")]
        public void ReadWithFormat()
        {
            FhirClient client = new FhirClient(testEndpoint);

            client.UseFormatParam = true;
            client.PreferredFormat = ResourceFormat.Json;

            var loc = client.Read<Patient>("Patient/example");
            Assert.IsNotNull(loc);
        }


        [TestMethod, TestCategory("FhirClient")]
        public void Read()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var loc = client.Read<Location>("Location/example");
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            Assert.AreEqual("1", loc.Id);
            Assert.IsNotNull(loc.Meta.VersionId);

            var loc2 = client.Read<Location>(ResourceIdentity.Build("Location", "1", loc.Meta.VersionId));
            Assert.IsNotNull(loc2);
            Assert.AreEqual(loc2.Id, loc.Id);
            Assert.AreEqual(loc2.Meta.VersionId, loc.Meta.VersionId);

            try
            {
                var random = client.Read<Location>(new Uri("Location/45qq54", UriKind.Relative));
                Assert.Fail();
            }
            catch (FhirOperationException)
            {
                Assert.AreEqual("404", client.LastResult.Status);
            }

            var loc3 = client.Read<Location>(ResourceIdentity.Build("Location", "1", loc.Meta.VersionId));
            Assert.IsNotNull(loc3);
            Assert.AreEqual(FhirSerializer.SerializeResourceToJson(loc),
                            FhirSerializer.SerializeResourceToJson(loc3));

            var loc4 = client.Read<Location>(loc.ResourceIdentity());
            Assert.IsNotNull(loc4);
            Assert.AreEqual(FhirSerializer.SerializeResourceToJson(loc),
                            FhirSerializer.SerializeResourceToJson(loc4));
        }


        [TestMethod, TestCategory("FhirClient")]
        public void ReadRelative()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var loc = client.Read<Location>(new Uri("Location/1", UriKind.Relative));
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);

            var ri = ResourceIdentity.Build(testEndpoint, "Location", "1");
            loc = client.Read<Location>(ri);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Address.City);
        }

#if PORTABLE45z
		[TestMethod, TestCategory("FhirClient")]
		public void ReadRelativeAsync()
		{
			FhirClient client = new FhirClient(testEndpoint);

			var loc = client.ReadAsync<Location>(new Uri("Location/1", UriKind.Relative)).Result;
			Assert.IsNotNull(loc);
			Assert.AreEqual("Den Burg", loc.Resource.Address.City);

			var ri = ResourceIdentity.Build(testEndpoint, "Location", "1");
			loc = client.ReadAsync<Location>(ri).Result;
			Assert.IsNotNull(loc);
			Assert.AreEqual("Den Burg", loc.Resource.Address.City);
		}
#endif
        [TestMethod, TestCategory("FhirClient")]
        public void Search()
        {
            FhirClient client = new FhirClient(testEndpoint);
            Bundle result;

            result = client.Search<DiagnosticReport>();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count() > 10, "Test should use testdata with more than 10 reports");

            result = client.Search<DiagnosticReport>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count <= 10);

            var withSubject =
                result.Entry.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Subject != null);
            Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

            ResourceIdentity ri = withSubject.ResourceIdentity();

            result = client.SearchById<DiagnosticReport>(ri.Id,
                        includes: new string[] { "DiagnosticReport:subject" });
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Entry.Count);  // should have subject too

            Assert.IsNotNull(result.Entry.Single(entry => entry.Resource.ResourceIdentity().ResourceType ==
                        typeof(DiagnosticReport).GetCollectionName()));
            Assert.IsNotNull(result.Entry.Single(entry => entry.Resource.ResourceIdentity().ResourceType ==
                        typeof(Patient).GetCollectionName()));

            result = client.Search<Patient>(new string[] { "name=Chalmers", "name=Peter" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count > 0);
        }

#if PORTABLE45z
        [TestMethod, TestCategory("FhirClient")]
        public void SearchAsync()
        {
            FhirClient client = new FhirClient(testEndpoint);
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
#endif

        [TestMethod, TestCategory("FhirClient")]
        public void Paging()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var result = client.Search<DiagnosticReport>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count <= 10);

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

        [TestMethod, TestCategory("FhirClient")]
        public void PagingInJson()
        {
            FhirClient client = new FhirClient(testEndpoint);
            client.PreferredFormat = ResourceFormat.Json;

            var result = client.Search<DiagnosticReport>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entry.Count <= 10);

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


        [TestMethod, Ignore]
        public void StackOverflow()
        {
            // Test doesn't assert anything, nor have a pre defined set of data
            var client = new FhirClient("http://spark.furore.com/fhir");
            var pat = client.Read<Patient>("Patient/1"); // "/_history/spark680");
        }

        [TestMethod, TestCategory("FhirClient")]
        public void CreateAndFullRepresentation()
        {
            FhirClient client = new FhirClient(testEndpoint);
            client.ReturnFullResource = true;       // which is also the default

            var pat = client.Read<Patient>("Patient/example");
            ResourceIdentity ri = pat.ResourceIdentity().WithBase(client.Endpoint);
            pat.Id = null;
            pat.Identifier.Clear();
            var patC = client.Create<Patient>(pat);
            Assert.IsNotNull(patC);

            client.ReturnFullResource = false;
            patC = client.Create<Patient>(pat);

            Assert.IsNull(patC);

            if (client.LastBody != null)
            {
                var returned = client.LastBodyAsResource;
                Assert.IsTrue(returned is OperationOutcome);
            }

            // Now validate this resource
            client.ReturnFullResource = true;       // which is also the default
            Parameters p = new Parameters();
          //  p.Add("mode", new FhirString("create"));
            p.Add("resource", pat);
            OperationOutcome ooI = (OperationOutcome)client.InstanceOperation(ri.WithoutVersion(), "validate", p);
            Assert.IsNotNull(ooI);
        }




        private Uri createdTestPatientUrl = null;

        /// <summary>
        /// This test is also used as a "setup" test for the History test.
        /// If you change the number of operations in here, this will make the History test fail.
        /// </summary>
        [TestMethod, TestCategory("FhirClient")]
        public void CreateEditDelete()
        {
            FhirClient client = new FhirClient(testEndpoint);
            var pat = client.Read<Patient>("Patient/example");
            pat.Id = null;
            pat.Identifier.Clear();
            pat.Identifier.Add(new Identifier("http://hl7.org/test/2", "99999"));

            var fe = client.Create(pat); // Create as we are not providing the ID to be used.
            Assert.IsNotNull(fe);
            Assert.IsNotNull(fe.Id);
            Assert.IsNotNull(fe.Meta.VersionId);
            createdTestPatientUrl = fe.ResourceIdentity();

            fe.Identifier.Add(new Identifier("http://hl7.org/test/2", "3141592"));
            var fe2 = client.Update(fe);

            Assert.IsNotNull(fe2);
            Assert.AreEqual(fe.Id, fe2.Id);
            Assert.AreNotEqual(fe.ResourceIdentity(), fe2.ResourceIdentity());
            Assert.AreEqual(2, fe2.Identifier.Count);

            fe.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
            var fe3 = client.Update(fe);
            Assert.IsNotNull(fe3);
            Assert.AreEqual(3, fe3.Identifier.Count);

            client.Delete(fe3);

            try
            {
                // Get most recent version
                fe = client.Read<Patient>(fe.ResourceIdentity().WithoutVersion());
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(client.LastResult.Status == "410");
            }
        }

        [TestMethod, TestCategory("FhirClient")]
        //Test for github issue https://github.com/ewoutkramer/fhir-net-api/issues/145
        public void Create_ObservationWithValueAsSimpleQuantity_ReadReturnsValueAsQuantity()
        {
            FhirClient client = new FhirClient(testEndpoint);
            var observation = new Observation();
            observation.Status = Observation.ObservationStatus.Preliminary;
            observation.Code = new CodeableConcept("http://loinc.org", "2164-2");
            observation.Value = new SimpleQuantity()
            {
                System = "http://unitsofmeasure.org",
                Value = 23,
                Code = "mg",
                Unit = "miligram"
            };
            observation.BodySite = new CodeableConcept("http://snomed.info/sct", "182756003");
            var fe = client.Create(observation);
            fe = client.Read<Observation>(fe.ResourceIdentity().WithoutVersion());
            Assert.IsInstanceOfType(fe.Value, typeof(Quantity));
        }

#if PORTABLE45z
		/// <summary>
		/// This test is also used as a "setup" test for the History test.
		/// If you change the number of operations in here, this will make the History test fail.
		/// </summary>
		[TestMethod, TestCategory("FhirClient")]
		public void CreateEditDeleteAsync()
		{
			var furore = new Organization
			{
				Name = "Furore",
				Identifier = new List<Identifier> { new Identifier("http://hl7.org/test/1", "3141") },
				Telecom = new List<Contact> { new Contact { System = Contact.ContactSystem.Phone, Value = "+31-20-3467171" } }
			};

			FhirClient client = new FhirClient(testEndpoint);
			var tags = new List<Tag> { new Tag("http://nu.nl/testname", Tag.FHIRTAGSCHEME_GENERAL, "TestCreateEditDelete") };

			var fe = client.CreateAsync<Organization>(furore, tags: tags, refresh: true).Result;

			Assert.IsNotNull(furore);
			Assert.IsNotNull(fe);
			Assert.IsNotNull(fe.Id);
			Assert.IsNotNull(fe.SelfLink);
			Assert.AreNotEqual(fe.Id, fe.SelfLink);
			Assert.IsNotNull(fe.Tags);
			Assert.AreEqual(1, fe.Tags.Count(), "Tag count on new organization record don't match");
			Assert.AreEqual(fe.Tags.First(), tags[0]);
			createdTestOrganizationUrl = fe.Id;

			fe.Resource.Identifier.Add(new Identifier("http://hl7.org/test/2", "3141592"));
			var fe2 = client.UpdateAsync(fe, refresh: true).Result;

			Assert.IsNotNull(fe2);
			Assert.AreEqual(fe.Id, fe2.Id);
			Assert.AreNotEqual(fe.SelfLink, fe2.SelfLink);
			Assert.AreEqual(2, fe2.Resource.Identifier.Count);

			Assert.IsNotNull(fe2.Tags);
			Assert.AreEqual(1, fe2.Tags.Count(), "Tag count on updated organization record don't match");
			Assert.AreEqual(fe2.Tags.First(), tags[0]);

			fe.Resource.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
			var fe3 = client.UpdateAsync(fe2.Id, fe.Resource, refresh: true).Result;
			Assert.IsNotNull(fe3);
			Assert.AreEqual(3, fe3.Resource.Identifier.Count);

			client.DeleteAsync(fe3).Wait();

			try
			{
				// Get most recent version
				fe = client.ReadAsync<Organization>(new ResourceIdentity(fe.Id)).Result;
				Assert.Fail();
			}
			catch
			{
				Assert.IsTrue(client.LastResponseDetails.Result == HttpStatusCode.Gone);
			}
		}
#endif

        [TestMethod, TestCategory("FhirClient"), Ignore]
        public void History()
        {
            DateTimeOffset timestampBeforeCreationAndDeletions = DateTimeOffset.Now;

            CreateEditDelete(); // this test does a create, update, update, delete (4 operations)

            FhirClient client = new FhirClient(testEndpoint);
            Bundle history = client.History(createdTestPatientUrl);
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entry.Count());
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());            
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            //// Now, assume no one is quick enough to insert something between now and the next
            //// tests....

            history = client.TypeHistory("Patient", timestampBeforeCreationAndDeletions);
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entry.Count());
            Assert.AreEqual(3, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());

            history = client.WholeSystemHistory(timestampBeforeCreationAndDeletions);
            Assert.IsNotNull(history);
            Assert.AreEqual(3, history.Entry.Count());
            Assert.AreEqual(2, history.Entry.Where(entry => entry.Resource != null).Count());
            Assert.AreEqual(1, history.Entry.Where(entry => entry.IsDeleted()).Count());
        }


        [TestMethod]
        [TestCategory("FhirClient")]
        public void TestWithParam()
        {
            var client = new FhirClient(testEndpoint);
            var res = client.Get("ValueSet/patient-contact-relationship/$validate-code?system=http://hl7.org/fhir/patient-contact-relationship&code=emergency");
            Assert.IsNotNull(res);
        }

        [TestMethod, TestCategory("FhirClient")]
        public void ManipulateMeta()
        {
           FhirClient client = new FhirClient(testEndpoint);

            var pat = new Patient();
            pat.Meta = new Meta();
           var key = new Random().Next();
            pat.Meta.ProfileElement.Add(new FhirUri("http://someserver.org/fhir/StructureDefinition/XYZ1-" + key));
            pat.Meta.Security.Add(new Coding("http://mysystem.com/sec", "1234-" + key));
            pat.Meta.Tag.Add(new Coding("http://mysystem.com/tag", "sometag1-" + key));

          //Before we begin, ensure that our new tags are not actually used when doing System Meta()
          var wsm = client.Meta();
            Assert.IsNotNull(wsm);

            Assert.IsFalse(wsm.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ1-" + key));
            Assert.IsFalse(wsm.Security.Select(c => c.Code + "@" + c.System).Contains("1234-" + key + "@http://mysystem.com/sec"));
            Assert.IsFalse(wsm.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag1-" + key + "@http://mysystem.com/tag"));

            Assert.IsFalse(wsm.Profile.Contains("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
            Assert.IsFalse(wsm.Security.Select(c => c.Code + "@" + c.System).Contains("5678-" + key + "@http://mysystem.com/sec"));
            Assert.IsFalse(wsm.Tag.Select(c => c.Code + "@" + c.System).Contains("sometag2-" + key + "@http://mysystem.com/tag"));


          // First, create a patient with the first set of meta
          var pat2 = client.Create(pat);
          var loc = pat2.ResourceIdentity(testEndpoint);          

          // Meta should be present on created patient
            verifyMeta(pat2.Meta, false, key);

          // Should be present when doing instance Meta()
          var par = client.Meta(loc);
            verifyMeta(par, false, key);

          // Should be present when doing type Meta()
          par = client.Meta(ResourceType.Patient);
            verifyMeta(par, false, key);

          // Should be present when doing System Meta()
          par = client.Meta();
            verifyMeta(par, false, key);

          // Now add some additional meta to the patient

          var newMeta = new Meta();
            newMeta.ProfileElement.Add(new FhirUri("http://someserver.org/fhir/StructureDefinition/XYZ2-" + key));
          newMeta.Security.Add(new Coding("http://mysystem.com/sec", "5678-" + key));
          newMeta.Tag.Add(new Coding("http://mysystem.com/tag", "sometag2-" + key));       

          
          client.AddMeta(loc, newMeta);
          var pat3 = client.Read<Patient>(loc);

          // New and old meta should be present on instance
          verifyMeta(pat3.Meta, true, key);

          // New and old meta should be present on Meta()
          par = client.Meta(loc);
            verifyMeta(par, true, key);

          // New and old meta should be present when doing type Meta()
          par = client.Meta(ResourceType.Patient);
            verifyMeta(par, true, key);

          // New and old meta should be present when doing system Meta()
          par = client.Meta();
            verifyMeta(par, true, key);

          // Now, remove those new meta tags
          client.DeleteMeta(loc, newMeta);

          // Should no longer be present on instance
          var pat4 = client.Read<Patient>(loc);
          verifyMeta(pat4.Meta, false, key);

          // Should no longer be present when doing instance Meta()
          par = client.Meta(loc);
            verifyMeta(par, false, key);

          // Should no longer be present when doing type Meta()
          par = client.Meta(ResourceType.Patient);
            verifyMeta(par, false, key);

          // clear out the client that we created, no point keeping it around
          client.Delete(pat4);

          // Should no longer be present when doing System Meta()
          par = client.Meta();
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
        [TestCategory("FhirClient")]
        public void TestSearchByPersonaCode()
        {
            var client = new FhirClient(testEndpoint);

            var pats =
              client.Search<Patient>(
                new[] { string.Format("identifier={0}|{1}", "urn:oid:1.2.36.146.595.217.0.1", "12345") });
            var pat = (Patient)pats.Entry.First().Resource;
        }


        [TestMethod]
        [TestCategory("FhirClient")]
        public void CreateDynamic()
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

            FhirClient client = new FhirClient(testEndpoint);

            var fe = client.Create(furore);
            Assert.IsNotNull(fe);
        }

        [TestMethod]
        [TestCategory("FhirClient")]
        public void CallsCallbacks()
        {
            FhirClient client = new FhirClient(testEndpoint);

            bool calledBefore = false;
            HttpStatusCode? status = null;
            byte[] body = null;
            byte[] bodyOut = null;

            client.OnBeforeRequest += (sender, e) =>
                {
                    calledBefore = true;
                    bodyOut = e.Body;
                };

            client.OnAfterResponse += (sender, e) =>
                {
                    body = e.Body;
                    status = e.RawResponse.StatusCode;
                };

            var pat = client.Read<Patient>("Patient/example");
            Assert.IsTrue(calledBefore);
            Assert.IsNotNull(status);
            Assert.IsNotNull(body);

            var bodyText = HttpToEntryExtensions.DecodeBody(body, Encoding.UTF8);

            Assert.IsTrue(bodyText.Contains("<Patient"));

            calledBefore = false;
            client.Create(pat);
            Assert.IsTrue(calledBefore);
            Assert.IsNotNull(bodyOut);

            bodyText = HttpToEntryExtensions.DecodeBody(body, Encoding.UTF8);
            Assert.IsTrue(bodyText.Contains("<Patient"));

        }

        [TestMethod]
        public void TestBinaryDetection()
        {
            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary", null));
            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary?param=x", null));
            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/_history", null));

            Assert.IsTrue(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2", null));
            Assert.IsTrue(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2/_history/1", null));

            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2", "application/xml+fhir"));
            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/Binary/2/_history/1", "application/json+fhir"));

            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/ValueSet/extensional-case-1/$expand?filter=f", null));
            Assert.IsFalse(HttpToEntryExtensions.IsBinaryResponse("http://server.org/fhir/ValueSet/extensional-case-1/$expand%3Ffilter=f", null));
        }

        [TestMethod]
        [TestCategory("FhirClient")]
        public void RequestFullResource()
        {
            var client = new FhirClient(testEndpoint);
            var minimal = false;
            client.OnBeforeRequest += (object s, BeforeRequestEventArgs e) => e.RawRequest.Headers["Prefer"] = minimal ? "return=minimal" : "return=representation";

            var result = client.Read<Patient>("Patient/example");
            Assert.IsNotNull(result);
            result.Id = null;
            result.Meta = null;

            client.ReturnFullResource = true;
            minimal = false;
            var posted = client.Create(result);
            Assert.IsNotNull(posted, "Patient example not found");

            minimal = true;     // simulate a server that does not return a body, even if ReturnFullResource = true
            posted = client.Create(result);
            Assert.IsNotNull(posted, "Did not return a resource, even when ReturnFullResource=true");

            client.ReturnFullResource = false;
            minimal = true;
            posted = client.Create(result);
            Assert.IsNull(posted);
        }

        void client_OnBeforeRequest(object sender, BeforeRequestEventArgs e)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [TestCategory("FhirClient")]
        public void TestReceiveHtmlIsHandled()
        {
            var client = new FhirClient("http://spark.furore.com/");        // an address that returns html

            try
            {
                var pat = client.Read<Patient>("Patient/1");
                Assert.Fail("Failed to throw an Exception on illegal body");
            }
            catch (FormatException fe)
            {
                if (!fe.Message.Contains("a valid FHIR xml/json body type was expected") && !fe.Message.Contains("not recognized as either xml or json"))
                    Assert.Fail("Failed to recognize invalid body contents");
            }
            catch (Exception)
            {
                Assert.Fail("Failed to throw FormatException on illegal body");
            }
        }

        [TestMethod]
        [TestCategory("FhirClient")]
        public void FhirVersionIsChecked()
        {
            var testEndpointDSTU2 = new Uri("http://spark-dstu2.furore.com/fhir");
            var testEndpointDSTU1 = new Uri("http://spark.furore.com/fhir");
            var testEndpointDSTU12 = new Uri("http://fhirtest.uhn.ca/baseDstu1");
            var testEndpointDSTU22 = new Uri("http://fhirtest.uhn.ca/baseDstu2");
            var testEndpointDSTU23 = new Uri("http://fhir-dev.healthintersections.com.au/open");

            var client = new FhirClient(testEndpointDSTU1);

            Conformance p;

            try
            {
                client = new FhirClient(testEndpointDSTU23, verifyFhirVersion: true);
                p = client.Conformance();
            }
            catch (NotSupportedException)
            {
                //Client uses 1.0.1, server states 1.0.0-7104
            }

            client = new FhirClient(testEndpointDSTU23);
            p = client.Conformance();

            //client = new FhirClient(testEndpointDSTU2);
            //p = client.Read<Patient>("Patient/example");
            //p = client.Read<Patient>("Patient/example");

            //client = new FhirClient(testEndpointDSTU22, verifyFhirVersion:true);
            //p = client.Read<Patient>("Patient/example");
            //p = client.Read<Patient>("Patient/example");


            client = new FhirClient(testEndpointDSTU12);
                       
            try
            {
                p = client.Conformance();
                Assert.Fail("Getting DSTU1 data using DSTU2 parsers should have failed");
            }
            catch (FormatException)
            {
                // OK
            }
        }
    }
}
