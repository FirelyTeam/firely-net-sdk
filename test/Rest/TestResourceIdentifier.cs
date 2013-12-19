using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TestResourceIdentifier
    {
        [TestMethod]
        public void TestResourceIdentity()
        {
            ResourceIdentity id = new ResourceIdentity("http://localhost/services/fhir/v012/patient/3");
            Assert.AreEqual("http://localhost/services/fhir/v012/patient/3", id.ToString());
            Assert.AreEqual("patient", id.Collection);
        }

        [TestMethod]
        public void TestCollection()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/patient/3");
            Assert.AreEqual("patient", identity.Collection);

            identity = new ResourceIdentity("http://localhost/fhir/organization/3/_history/98");
            Assert.AreEqual("organization", identity.Collection);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/patient/3/");
            Assert.AreEqual("patient", identity.Collection);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/organization/3/_history/98");
            Assert.AreEqual("organization", identity.Collection);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.Collection);
        }

        [TestMethod]
        public void TestId()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/patient/3");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("http://localhost/fhir/organization/508x/_history/98");
            Assert.AreEqual("508x", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/patient/B256/");
            Assert.AreEqual("B256", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/organization/3/_history/98");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.Id);

        }

        [TestMethod]
        public void TestVersionId()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/patient/3");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/fhir/organization/508x/_history/98");
            Assert.AreEqual("98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/patient/B256/");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/organization/3/_history/X98");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/organization/3/_history/X98/pipo/clown");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.VersionId);
        }

        [TestMethod]
        public void Build()
        {
            ResourceIdentity identity;

            identity = ResourceIdentity.Build("patient", "A100");
            Assert.AreEqual("patient/A100", identity.ToString());

            identity = ResourceIdentity.Build("patient", "A100", "H2");
            Assert.AreEqual("patient/A100/_history/H2", identity.ToString());

            Endpoint endpoint = new Endpoint("http://spark.furore.com/fhir");
            identity = endpoint.ToResourceIdentity("organization", "ORG01");
            Assert.AreEqual("http://spark.furore.com/fhir/organization/ORG01", identity.ToString());

            endpoint = new Endpoint("http://spark.furore.com/fhir/");
            identity = endpoint.ToResourceIdentity("organization", "ORG01", "HV4");
            Assert.AreEqual("http://spark.furore.com/fhir/organization/ORG01/_history/HV4", identity.ToString());

        }

        [TestMethod]
        public void AddVersionNumberToExistingIdentifier()
        {
            var identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/patient/B256/");
            var newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("B256", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            // mh: relativ uri's:

            identity = new ResourceIdentity("organization/3");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);
        }


        [TestMethod]
        public void TestRelativeUri()
        {
            ResourceIdentity identity;
            
            identity = new ResourceIdentity("patient/8");
            Assert.AreEqual("patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);

            identity = new ResourceIdentity("patient/8/_history/H30");
            Assert.AreEqual("patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);
            Assert.AreEqual("H30", identity.VersionId);

            identity = new ResourceIdentity(new Uri("patient/8", UriKind.Relative));
            Assert.AreEqual("patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);

            identity = new ResourceIdentity(new Uri("patient/8/_history/H30", UriKind.Relative));
            Assert.AreEqual("patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);
            Assert.AreEqual("H30", identity.VersionId);
        }

    }
}
