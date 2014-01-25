using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Hl7.Fhir.Client;
using Hl7.Fhir.Model;
using System.Net;
using Hl7.Fhir.Support;
using Hl7.Fhir.Serializers;
using Hl7.Fhir.Support.Search;
using System.IO;
using Hl7.Fhir.Parsers;

namespace Hl7.Fhir.Tests
{
 //  [TestClass]
    public class FhirClientTests
    {
        //Uri testEndpoint = new Uri("http://fhir.furore.com/fhir");
        //Uri testEndpoint = new Uri("http://hl7connect.healthintersections.com.au/svc/fhir");
        Uri testEndpoint = new Uri("https://api.fhir.me");

        [TestMethod]
        public void FetchConformance()
        {
            FhirClient client = new FhirClient(testEndpoint);

            Conformance c = client.Conformance().Resource;

            Assert.IsNotNull(c);
            Assert.AreEqual("HL7Connect", c.Software.Name);
            Assert.AreEqual(Conformance.RestfulConformanceMode.Server, c.Rest[0].Mode.Value);
            Assert.AreEqual(ContentType.XML_CONTENT_HEADER, client.LastResponseDetails.ContentType.ToLower());
            Assert.AreEqual(HttpStatusCode.OK, client.LastResponseDetails.Result);
        }


        [TestMethod]
        public void TryParse()
        {
            var client = new FhirClient(new Uri("http://spark.furore.com/fhir"));

            var history = client.History<Patient>("15");
        }


        [TestMethod]
        public void Read()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var loc = client.Read<Location>("1");
            Assert.IsNotNull(loc);
            Assert.AreEqual("Den Burg", loc.Resource.Address.City);

            string version = new ResourceLocation(loc.SelfLink).VersionId;               
            Assert.AreEqual("1", version);

            string id = new ResourceLocation(loc.Id).Id;
            Assert.AreEqual("1", id);

            try
            {
                var random = client.Read<Location>("45qq54");
                Assert.Fail();
            }
            catch (FhirOperationException)
            {
                Assert.IsTrue(client.LastResponseDetails.Result == HttpStatusCode.NotFound);
            }

            var loc2 = client.VRead<Location>("1", version);
            Assert.IsNotNull(loc2);
            Assert.AreEqual(FhirSerializer.SerializeBundleEntryToJson(loc),
                            FhirSerializer.SerializeBundleEntryToJson(loc2));

            var loc3 = client.Fetch<Location>(loc.SelfLink);
            Assert.IsNotNull(loc3);
            Assert.AreEqual(FhirSerializer.SerializeBundleEntryToJson(loc),
                            FhirSerializer.SerializeBundleEntryToJson(loc3));

        }


        [TestMethod]
        public void SearchDavid()
        {
            //Uri te = new Uri("http://hl7connect.healthintersections.com.au/svc/fhir");
            //FhirClient client = new FhirClient(te);
            //Bundle data = client.Search(testEndpoint,"name","int");
            //Console.Out.WriteLine(data.Entries.Count);
        }

        [TestMethod]
        public void Search()
        {
            FhirClient client = new FhirClient(testEndpoint);
            Bundle result;

            result = client.Search(ResourceType.DiagnosticReport);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count > 0);
            Assert.IsTrue(result.Entries[0].Id.ToString().EndsWith("@101"));
            Assert.IsTrue(result.Entries.Count() > 10, "Test should use testdata with more than 10 reports");

            result = client.Search(ResourceType.DiagnosticReport,count:10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count <= 10);
            Assert.IsTrue(result.Entries[0].Id.ToString().EndsWith("@101"));

            //result = client.SearchById<DiagnosticReport>("101", "DiagnosticReport/subject");
            result = client.Search(ResourceType.DiagnosticReport, "_id", "101", includes: new string[] { "DiagnosticReport.subject" } );
            Assert.IsNotNull(result);

            Assert.AreEqual(1,
                    result.Entries.Where(entry => entry.Links.SelfLink.ToString()
                        .Contains("diagnosticreport")).Count());

            Assert.IsTrue(result.Entries.Any(entry =>
                    entry.Links.SelfLink.ToString().Contains("patient/@pat2")));

            result = client.Search(ResourceType.DiagnosticReport, new SearchParam[] 
                {
                    new SearchParam("name", new StringParamValue("Everywoman")),
                    new SearchParam("name", new StringParamValue("Eve")) 
                });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries[0].Links.SelfLink.ToString().Contains("patient/@1"));
        }

        [TestMethod]
        public void Paging()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var result = client.Search(ResourceType.DiagnosticReport, count: 10);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Entries.Count <= 10);

            var firstId = result.Entries.First().Id;

            // Browse forward
            result = client.Continue(result);
            Assert.IsNotNull(result);
            var nextId = result.Entries.First().Id;
            Assert.AreNotEqual(firstId, nextId);

            // Browse backward
            //result = client.Continue(result, PageDirection.Previous);
            result = client.Continue(result, PageDirection.First);
            Assert.IsNotNull(result);
            var prevId = result.Entries.First().Id;
            Assert.AreEqual(firstId, prevId);
        }



        private Uri createdTestOrganization = null;

        [TestMethod]
        public void CreateEditDelete()
        {
            var furore = new Organization
            {
                Name = "Furore",
                Identifier = new List<Identifier> { new Identifier("http://hl7.org/test/1", "3141") },
                Telecom = new List<Contact> { new Contact { System = Contact.ContactSystem.Phone, Value = "+31-20-3467171" } }
            };

            FhirClient client = new FhirClient(testEndpoint);
            var tags = new List<Tag> { new Tag("http://nu.nl/testname", Tag.FHIRTAGNS, "TestCreateEditDelete") };

            var fe = client.Create(furore,tags);

            Assert.IsNotNull(furore);
            Assert.IsNotNull(fe);
            Assert.IsNotNull(fe.Id);
            Assert.IsNotNull(fe.SelfLink);
            Assert.AreNotEqual(fe.Id,fe.SelfLink);
            Assert.IsNotNull(fe.Tags);
            Assert.AreEqual(1, fe.Tags.Count());
            Assert.AreEqual(fe.Tags.First(), tags[0]);
            createdTestOrganization = fe.Id;

            fe.Resource.Identifier.Add(new Identifier("http://hl7.org/test/2", "3141592"));

            var fe2 = client.Update(fe);

            Assert.IsNotNull(fe2);
            Assert.AreEqual(fe.Id, fe2.Id);
            Assert.AreNotEqual(fe.SelfLink, fe2.SelfLink);
            Assert.IsNotNull(fe2.Tags);
            Assert.AreEqual(1, fe2.Tags.Count());
            Assert.AreEqual(fe2.Tags.First(), tags[0]);

            client.Delete(fe2.Id);

            try
            {
                fe = client.Read<Organization>(ResourceLocation.GetIdFromResourceId(fe.Id));
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(client.LastResponseDetails.Result == HttpStatusCode.Gone);
            }
            
            Assert.IsNull(fe);
        }


        [TestMethod]
        public void History()
        {
            DateTimeOffset now = DateTimeOffset.Now;

            CreateEditDelete();

            FhirClient client = new FhirClient(testEndpoint);
            Bundle history = client.History(createdTestOrganization);
            Assert.IsNotNull(history);
            Assert.AreEqual(3, history.Entries.Count());
            Assert.AreEqual(2, history.Entries.Where(entry => entry is ResourceEntry).Count());
            Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());

            // Now, assume no one is quick enough to insert something between now and the next
            // tests....

            history = client.History<Organization>(now);
            Assert.IsNotNull(history);
            Assert.AreEqual(3, history.Entries.Count());
            Assert.AreEqual(2, history.Entries.Where(entry => entry is ResourceEntry).Count());
            Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());

            history = client.History(now);
            Assert.IsNotNull(history);
            Assert.AreEqual(3, history.Entries.Count());
            Assert.AreEqual(2, history.Entries.Where(entry => entry is ResourceEntry).Count());
            Assert.AreEqual(1, history.Entries.Where(entry => entry is DeletedEntry).Count());
        }


        //[TestMethod]
        //public void ParseForPPT()
        //{
        //    ErrorList errors = new ErrorList();

        //    // Create a file-based reader for Xml
        //    XmlReader xr = XmlReader.Create(
        //        new StreamReader(@"publish\observation-example.xml"));

        //    // Create a file-based reader for Xml
        //    var obs = (Observation)FhirParser.ParseResource(xr, errors);

        //    // Modify some fields of the observation
        //    obs.Status = ObservationStatus.Amended;
        //    obs.Value = new Quantity() { Value = 40, Units = "g" };

        //    // Serialize the in-memory observation to Json
        //    var jsonText = FhirSerializer.SerializeResourceToJson(obs);

        //}


        [TestMethod]
        public void ReadTags()
        {
            FhirClient client = new FhirClient(testEndpoint);

            var tags = new List<Tag>() { new Tag("http://readtag.nu.nl", Tag.FHIRTAGNS, "readTagTest") };

            client.AffixTags(tags, ResourceType.Location, "1");

            var list = client.GetTags();
            Assert.IsTrue(list.Any(t => t == tags.First()));

            list = client.GetTags(ResourceType.Location);
            Assert.IsTrue(list.Any(t => t == tags.First()));

            list = client.GetTags(ResourceType.Location, "1", "1");
            Assert.IsTrue(list.Any(t => t == tags.First()));

            client.DeleteTags(tags, ResourceType.Location, "1", "1");

            list = client.GetTags();
            Assert.IsFalse(list.Any(t => t == tags.First()));
        }
    }
}
