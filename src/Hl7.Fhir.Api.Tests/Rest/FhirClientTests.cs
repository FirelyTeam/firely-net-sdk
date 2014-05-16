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
using Hl7.Fhir.Search;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirClientTests
#else
	public class FhirClientTests
#endif
    {    
        Uri testEndpoint = new Uri("http://spark.furore.com/fhir");
        // Uri testEndpoint = new Uri("http://localhost.fiddler:1396/fhir");
        // Uri testEndpoint = new Uri("http://localhost:1396/fhir");
        //Uri testEndpoint = new Uri("http://fhir.healthintersections.com.au/open");
        // Uri testEndpoint = new Uri("https://api.fhir.me");

		[TestInitialize]
		public void TestInitialize()
		{
			System.Diagnostics.Trace.WriteLine("Testing against fhir server: " + testEndpoint.OriginalString);
		}

        [TestMethod, TestCategory("FhirClient")]
        public void FetchConformance()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var entry = client.Conformance();
            var c = entry.Resource;

            Assert.IsNotNull(c);
            // Assert.AreEqual("Spark.Service", c.Software.Name); // This is only for ewout's server
            Assert.AreEqual(Conformance.RestfulConformanceMode.Server, c.Rest[0].Mode.Value);
            Assert.AreEqual(HttpStatusCode.OK, client.LastResponseDetails.Result);

            // Does't currently work on Grahame's server
            Assert.AreEqual(ContentType.XML_CONTENT_HEADER, client.LastResponseDetails.ContentType.ToLower());
        }


        [TestMethod, TestCategory("FhirClient")]
        public void ReadWithFormat()
        {
            FhirClient client = new FhirClient(testEndpoint);

            client.UseFormatParam = true;
            client.PreferredFormat = ResourceFormat.Json;

            var loc = client.Read("Patient/1");

            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromContentType(client.LastResponseDetails.ContentType));
        }


        class FhirClientWithHooks : FhirClient
        {
            internal FhirClientWithHooks(Uri endpoint) : base(endpoint) { }

            internal bool HitBeforeRequest { get; set; }
            internal bool HitAfterRequest { get; set; }

            protected override void BeforeRequest(HttpWebRequest request)
            {
                HitBeforeRequest = true;
            }

            protected override void AfterResponse(WebResponse request)
            {
                HitAfterRequest = true;
            }
        }

        [TestMethod, TestCategory("FhirClient")]
        public void ReadCallsHooks()
        {
            FhirClientWithHooks client = new FhirClientWithHooks(testEndpoint);

            var loc = client.Read("Patient/1");

            Assert.IsTrue(client.HitBeforeRequest);
            Assert.IsTrue(client.HitAfterRequest);
        }


        [TestMethod, TestCategory("FhirClient")]
        public void ReadCallsHookedEvents()
        {
            FhirClient client = new FhirClient(testEndpoint);

            bool hitBeforeRequest = false;
            bool hitAfterRequest = false;

            client.OnBeforeRequest += (o,p) => hitBeforeRequest = true;
            client.OnAfterResponse += (o,p) => hitAfterRequest = true;

            var loc = client.Read("Patient/1");

            Assert.IsTrue(hitBeforeRequest);
            Assert.IsTrue(hitAfterRequest);
        }



        [TestMethod, TestCategory("FhirClient")]
        public void Read()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var loc = client.Read<Location>("Location/1");
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Resource.Address.City);

            string version = new ResourceIdentity(loc.SelfLink).VersionId;
            Assert.IsNotNull(version);
            string id = new ResourceIdentity(loc.Id).Id;
            Assert.AreEqual("1", id);

            try
            {
                var random = client.Read(new Uri("Location/45qq54", UriKind.Relative));
                Assert.Fail();
            }
            catch (FhirOperationException)
            {
                Assert.IsTrue(client.LastResponseDetails.Result == HttpStatusCode.NotFound);
            }

            var loc2 = client.Read<Location>(ResourceIdentity.Build("Location","1", version));
            Assert.IsNotNull(loc2);
            Assert.AreEqual(FhirSerializer.SerializeBundleEntryToJson(loc),
                            FhirSerializer.SerializeBundleEntryToJson(loc2));

            var loc3 = client.Read<Location>(loc.SelfLink);
            Assert.IsNotNull(loc3);
            Assert.AreEqual(FhirSerializer.SerializeBundleEntryToJson(loc),
                            FhirSerializer.SerializeBundleEntryToJson(loc3));        
        }


        [TestMethod, TestCategory("FhirClient")]
        public void ReadRelative()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var loc = client.Read<Location>(new Uri("Location/1", UriKind.Relative));
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Resource.Address.City);            

            var ri = ResourceIdentity.Build(testEndpoint, "Location", "1");
            loc = client.Read<Location>(ri);
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Resource.Address.City);            
        }

#if PORTABLE45
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
            Assert.IsTrue(result.Entries.Count() > 10, "Test should use testdata with more than 10 reports");

            result = client.Search<DiagnosticReport>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count <= 10);

            var withSubject = 
                result.Entries.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Resource.Subject != null);
            Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

            ResourceIdentity ri = new ResourceIdentity(withSubject.Id);

            result = client.SearchById<DiagnosticReport>(ri.Id, 
                        includes: new string[] { "DiagnosticReport.subject" });
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Entries.Count);  // should have subject too

            Assert.IsNotNull(result.Entries.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                        typeof(DiagnosticReport).GetCollectionName()));
            Assert.IsNotNull(result.Entries.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                        typeof(Patient).GetCollectionName()));

            result = client.Search<Patient>(new string[] { "name=Everywoman", "name=Eve" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count > 0);
        }

#if PORTABLE45
        [TestMethod, TestCategory("FhirClient")]
        public void SearchAsync()
        {
            FhirClient client = new FhirClient(testEndpoint);
            Bundle result;

            result = client.SearchAsync<DiagnosticReport>().Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count() > 10, "Test should use testdata with more than 10 reports");

            result = client.SearchAsync<DiagnosticReport>(pageSize: 10).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count <= 10);

            var withSubject = 
                result.Entries.ByResourceType<DiagnosticReport>().FirstOrDefault(dr => dr.Resource.Subject != null);
            Assert.IsNotNull(withSubject, "Test should use testdata with a report with a subject");

            ResourceIdentity ri = new ResourceIdentity(withSubject.Id);

            result = client.SearchByIdAsync<DiagnosticReport>(ri.Id, 
                        includes: new string[] { "DiagnosticReport.subject" }).Result;
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Entries.Count);  // should have subject too

            Assert.IsNotNull(result.Entries.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                        typeof(DiagnosticReport).GetCollectionName()));
            Assert.IsNotNull(result.Entries.Single(entry => new ResourceIdentity(entry.Id).Collection ==
                        typeof(Patient).GetCollectionName()));

            result = client.SearchAsync<Patient>(new string[] { "name=Everywoman", "name=Eve" }).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count > 0);
        }
#endif

        [TestMethod, TestCategory("FhirClient")]
        public void Paging()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var result = client.Search<DiagnosticReport>(pageSize: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count <= 10);

            var firstId = result.Entries.First().Id;

            // Browse forward
            result = client.Continue(result);
            Assert.IsNotNull(result);
            var nextId = result.Entries.First().Id;
            Assert.AreNotEqual(firstId, nextId);

            // Browse to first
            result = client.Continue(result, PageDirection.First);
            Assert.IsNotNull(result);
            var prevId = result.Entries.First().Id;
            Assert.AreEqual(firstId, prevId);

            // Forward, then backwards
            result = client.Continue(result, PageDirection.Next);
            Assert.IsNotNull(result);
            result = client.Continue(result, PageDirection.Previous);
            Assert.IsNotNull(result);
            prevId = result.Entries.First().Id;
            Assert.AreEqual(firstId, prevId);
        }



        private Uri createdTestOrganizationUrl = null;

		/// <summary>
		/// This test is also used as a "setup" test for the History test.
		/// If you change the number of operations in here, this will make the History test fail.
		/// </summary>
        [TestMethod, TestCategory("FhirClient")]
        public void CreateEditDelete()
        {
            var furore = new Organization
            {
                Name = "Furore",
                Identifier = new List<Identifier> { new Identifier("http://hl7.org/test/1", "3141") },
                Telecom = new List<Contact> { new Contact { System = Contact.ContactSystem.Phone, Value = "+31-20-3467171" } }
            };

            FhirClient client = new FhirClient(testEndpoint);
            var tags = new List<Tag> { new Tag("http://nu.nl/testname", Tag.FHIRTAGSCHEME_GENERAL, "TestCreateEditDelete") };

            var fe = client.Create<Organization>(furore, tags:tags, refresh: true);

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
            var fe2 = client.Update(fe, refresh: true);
            
            Assert.IsNotNull(fe2);
            Assert.AreEqual(fe.Id, fe2.Id);
            Assert.AreNotEqual(fe.SelfLink, fe2.SelfLink);
            Assert.AreEqual(2, fe2.Resource.Identifier.Count);

            Assert.IsNotNull(fe2.Tags);
			Assert.AreEqual(1, fe2.Tags.Count(), "Tag count on updated organization record don't match");
            Assert.AreEqual(fe2.Tags.First(), tags[0]);

            fe.Resource.Identifier.Add(new Identifier("http://hl7.org/test/3", "3141592"));
            var fe3 = client.Update(fe2.Id, fe.Resource, refresh: true);
            Assert.IsNotNull(fe3);
            Assert.AreEqual(3, fe3.Resource.Identifier.Count);

            client.Delete(fe3);

            try
            {
                // Get most recent version
                fe = client.Read<Organization>(new ResourceIdentity(fe.Id));
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(client.LastResponseDetails.Result == HttpStatusCode.Gone);
            }
        }

#if PORTABLE45
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

        [TestMethod, TestCategory("FhirClient")]
        public void History()
        {
            DateTimeOffset timestampBeforeCreationAndDeletions = DateTimeOffset.Now;

            CreateEditDelete(); // this test does a create, update, update, delete (4 operations)

            FhirClient client = new FhirClient(testEndpoint);
            Bundle history = client.History(createdTestOrganizationUrl);
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entries.Count());
            Assert.AreEqual(3, history.Entries.Where(entry => entry is ResourceEntry).Count());
            Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());

            // Now, assume no one is quick enough to insert something between now and the next
            // tests....

			history = client.TypeHistory<Organization>(timestampBeforeCreationAndDeletions);
            Assert.IsNotNull(history);
            Assert.AreEqual(4, history.Entries.Count());
            Assert.AreEqual(3, history.Entries.Where(entry => entry is ResourceEntry).Count());
            Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());

            //EK: Our server can't yet do this
            //history = client.WholeSystemHistory(now);
            //Assert.IsNotNull(history);
            //Assert.AreEqual(3, history.Entries.Count());
            //Assert.AreEqual(2, history.Entries.Where(entry => entry is ResourceEntry).Count());
            //Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());
        }




        [TestMethod, TestCategory("FhirClient")]
        public void ReadTags()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var tags = new List<Tag>() { new Tag("http://readtag.nu.nl", Tag.FHIRTAGSCHEME_GENERAL, "readTagTest") };
            var identity = ResourceIdentity.Build("Location", "1");

            client.AffixTags(identity, tags);

            var affixedEntry = client.Read(identity);
			IEnumerable<Tag> list;
			//BP/EK: Our server can't yet do this (as noted above in the history test)
			// list = client.WholeSystemTags();
            // Assert.IsTrue(list.Any(t => t.Equals(tags.First())));

            list = client.TypeTags<Location>();
            Assert.IsTrue(list.Any(t => t.Equals(tags.First())));

            list = client.Tags(affixedEntry.SelfLink);
            Assert.IsTrue(list.Any(t => t.Equals(tags.First())));

			// BP: Furore server having issues with this at present (17 April 2014)
			// causes the test to fail
            client.DeleteTags(affixedEntry.SelfLink, tags);
            //TODO: verify tags have really been removed. Should generate random tag so this is repeatable
        }
    }
}
