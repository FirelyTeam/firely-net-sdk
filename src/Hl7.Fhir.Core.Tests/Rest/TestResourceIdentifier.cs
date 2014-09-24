/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
#if PORTABLE45
	public class PortableTestResourceIdentifier
#else
	public class TestResourceIdentifier
#endif
    {
        [TestMethod]
        public void TestResourceIdentity()
        {
            ResourceIdentity id = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3");
            Assert.AreEqual("http://localhost/services/fhir/v012/Patient/3", id.ToString());
            Assert.AreEqual("Patient", id.Collection);
        }

        [TestMethod]
        public void TestCollection()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual("Patient", identity.Collection);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/3/_history/98");
            Assert.AreEqual("Organization", identity.Collection);

			// Test that the case sensitivity of the resource name is honoured
            //EK: Still think that's not ResourceIdentifier's problem, but the calling subsystem should check with available metadata
            //EK: Hardwiring this into ResourceIdentifier means you cannot use it in "dynamic" scenario's where you don't have compile-time metadata
//			identity = new ResourceIdentity("http://localhost/fhir/organization/3/_history/98");
//			Assert.AreEqual(null, identity.Collection);

			identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/3/");
			Assert.AreEqual("Patient", identity.Collection);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/98");
            Assert.AreEqual("Organization", identity.Collection);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.Collection);

			identity = new ResourceIdentity("http://localhost/fhir/_history");
			Assert.AreEqual(null, identity.Collection);

			// This is expected to return null, as this is not a valid use of the Resource Identity class
			identity = new ResourceIdentity("http://localhost/fhir/organization/_history");
			Assert.AreEqual(null, identity.Collection);
		}

        [TestMethod]
        public void TestId()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
            Assert.AreEqual("508x", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            Assert.AreEqual("B256", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/98");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.Id);

        }

        [TestMethod]
        public void TestVersionId()
        {
            ResourceIdentity identity;

            identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
            Assert.AreEqual("98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98/pipo/clown");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/fhir");
            Assert.AreEqual(null, identity.VersionId);
        }

		[TestMethod]
		public void TestEndpoint()
		{
			ResourceIdentity identity;

			identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
			Assert.AreEqual("http://localhost/fhir/", identity.Endpoint.OriginalString);

			identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
			Assert.AreEqual("http://localhost/fhir/", identity.Endpoint.OriginalString);

			identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
			Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.Endpoint.OriginalString);

			identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
			Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.Endpoint.OriginalString);

			identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98/pipo/clown");
			Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.Endpoint.OriginalString);

			identity = new ResourceIdentity("http://localhost/fhir");
			Assert.IsNull(identity.Endpoint);
		}

		[TestMethod]
        public void Build()
        {
            ResourceIdentity identity;

            identity = ResourceIdentity.Build("Patient", "A100");
            Assert.AreEqual("Patient/A100", identity.ToString());

            identity = ResourceIdentity.Build("Patient", "A100", "H2");
            Assert.AreEqual("Patient/A100/_history/H2", identity.ToString());
        }

        [TestMethod]
        public void AddVersionNumberToExistingIdentifier()
        {
            var identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            var newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("B256", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            // mh: relativ uri's:

            identity = new ResourceIdentity("Organization/3");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("Organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);
        }


        [TestMethod]
        public void TestRelativeUri()
        {
            ResourceIdentity identity;
            
            identity = new ResourceIdentity("Patient/8");
            Assert.AreEqual("Patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);

            identity = new ResourceIdentity("Patient/8/_history/H30");
            Assert.AreEqual("Patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);
            Assert.AreEqual("H30", identity.VersionId);

            identity = new ResourceIdentity(new Uri("Patient/8", UriKind.Relative));
            Assert.AreEqual("Patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);

            identity = new ResourceIdentity(new Uri("Patient/8/_history/H30", UriKind.Relative));
            Assert.AreEqual("Patient", identity.Collection);
            Assert.AreEqual("8", identity.Id);
            Assert.AreEqual("H30", identity.VersionId);
        }

    }
}
