/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TestResourceIdentifier
    {
        [TestMethod]
        public void TestBuild()
        {
            var id = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3");
            var idb = ResourceIdentity.Build(new Uri("http://localhost/services/fhir/v012"), "Patient", "3");
            Assert.AreEqual("http://localhost/services/fhir/v012/Patient/3", id.ToString());
            Assert.AreEqual(id, idb);

            id = new ResourceIdentity("Patient/3");
            idb = ResourceIdentity.Build("Patient", "3");
            Assert.AreEqual("Patient/3", id.ToString());
            Assert.AreEqual(id, idb);

            id = ResourceIdentity.Build("Patient", "A100", "H2");
            Assert.AreEqual("Patient/A100/_history/H2", id.ToString());

            id = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            idb = ResourceIdentity.Build(UrnType.OID, "1.2.3.4.5.6");
            Assert.AreEqual("urn:oid:1.2.3.4.5.6", id.ToString());
            Assert.AreEqual(id, idb);

            id = new ResourceIdentity("#myid");
            idb = ResourceIdentity.Build("myid");
            Assert.AreEqual("#myid", id.ToString());
            Assert.AreEqual(id, idb);
        }


        [TestMethod]
        public void TestBase()
        {
            var identity = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3");
            Assert.AreEqual(new Uri("http://localhost/services/fhir/v012/"), identity.BaseUri);

            identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual("http://localhost/fhir/", identity.BaseUri.OriginalString);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
            Assert.AreEqual("http://localhost/fhir/", identity.BaseUri.OriginalString);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.BaseUri.OriginalString);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
            Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.BaseUri.OriginalString);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98/pipo/clown");
            Assert.AreEqual("http://localhost/some/sub/path/fhir/", identity.BaseUri.OriginalString);

            identity = new ResourceIdentity("Patient/3");
            Assert.IsNull(identity.BaseUri);
            Assert.IsFalse(identity.HasBaseUri);

            identity = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            Assert.AreEqual(new Uri("urn:oid:"), identity.BaseUri);

            identity = new ResourceIdentity("#myid");
            Assert.IsFalse(identity.HasBaseUri);
        }

        [TestMethod]
        public void TestResourceType()
        {
            var identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual("Patient", identity.ResourceType);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/3/_history/98");
            Assert.AreEqual("Organization", identity.ResourceType);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/3/");
            Assert.AreEqual("Patient", identity.ResourceType);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/98");
            Assert.AreEqual("Organization", identity.ResourceType);

            identity = new ResourceIdentity("Patient/3");
            Assert.AreEqual("Patient", identity.ResourceType);

            identity = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            Assert.IsNull(identity.ResourceType);

            identity = new ResourceIdentity("#myid");
            Assert.IsNull(identity.ResourceType);

            identity = new ResourceIdentity("http://localhost/fhir/Patient/45?param=x");
            Assert.AreEqual("Patient", identity.ResourceType);
        }

        [TestMethod]
        public void TestCoreIdentifiers()
        {
            var patientId = ResourceIdentity.Core(FHIRDefinedType.Patient);
            Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/Patient", patientId.ToString());

            var oidId = ResourceIdentity.Core(FHIRDefinedType.Oid);
            Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/oid", oidId.ToString());

            var observationId = ResourceIdentity.Core("Observation");
            Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/Observation", observationId.ToString());
        }

        [TestMethod]
        public void TestId()
        {
            var identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
            Assert.AreEqual("508x", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            Assert.AreEqual("B256", identity.Id);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/98");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("Patient/3");
            Assert.AreEqual("3", identity.Id);

            identity = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            Assert.AreEqual("1.2.3.4.5.6", identity.Id);

            identity = new ResourceIdentity("#myid");
            Assert.AreEqual("myid", identity.Id);
        }

        [TestMethod]
        public void TestVersionId()
        {
            var identity = new ResourceIdentity("http://localhost/fhir/Patient/3");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/fhir/Organization/508x/_history/98");
            Assert.AreEqual("98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            Assert.AreEqual(null, identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98/pipo/clown");
            Assert.AreEqual("X98", identity.VersionId);

            identity = new ResourceIdentity("Patient/3");
            Assert.IsNull(identity.VersionId);

            identity = new ResourceIdentity("Patient/3/_history/123");
            Assert.AreEqual("123", identity.VersionId);

            identity = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            Assert.IsNull(identity.VersionId);

            identity = new ResourceIdentity("#myid");
            Assert.IsNull(identity.VersionId);
        }

        [TestMethod]
        public void WithVersion()
        {
            var identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Patient/B256/");
            var newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("B256", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("http://localhost/some/sub/path/fhir/Organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");

            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("Organization/3");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            identity = new ResourceIdentity("Organization/3/_history/X98");
            newIdentity = identity.WithVersion("3141");
            Assert.AreEqual("3", newIdentity.Id);
            Assert.AreEqual("3141", newIdentity.VersionId);

            var identity2 = identity.WithoutVersion();
            Assert.AreEqual("Organization/3", identity2.ToString());
        }

        [TestMethod]
        public void WithBase()
        {
            var id = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3");
            var id1 = id.WithBase("http://nu.nl/fhir");
            Assert.AreEqual("http://nu.nl/fhir/Patient/3", id1.ToString());

            var id2 = new ResourceIdentity("Patient/3").WithBase("http://nu.nl/fhir");
            Assert.AreEqual("http://nu.nl/fhir/Patient/3", id2.ToString());

            var id3 = id2.MakeRelative();
            Assert.AreEqual("Patient/3", id3.ToString());
        }

        [TestMethod]
        public void TestForTarget()
        {
            var id = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3");
            Assert.IsTrue(id.IsTargetOf("http://localhost/services/fhir/v012/Patient/3"));
            Assert.IsTrue(id.IsTargetOf("Patient/3"));

            id = new ResourceIdentity("http://localhost/services/fhir/v012/Patient/3/_history/50");
            Assert.IsTrue(id.IsTargetOf("http://localhost/services/fhir/v012/Patient/3"));
            Assert.IsTrue(id.IsTargetOf("Patient/3/_history/50"));
            
            id = new ResourceIdentity("Patient/3");
            Assert.IsFalse(id.IsTargetOf("http://localhost/services/fhir/v012/Patient/3"));
            Assert.IsTrue(id.IsTargetOf("Patient/3"));

            id = new ResourceIdentity("urn:oid:1.2.3.4.5.6");
            Assert.IsTrue(id.IsTargetOf("urn:oid:1.2.3.4.5.6"));
            Assert.IsFalse(id.IsTargetOf("1.2.3.4.5.6"));

            id = new ResourceIdentity("#myid");
            Assert.IsTrue(id.IsTargetOf("#myid"));
        }

        [TestMethod]
        public void TestIsResourceId()
        {
            Assert.IsTrue(ResourceIdentity.IsRestResourceIdentity("Patient/4"));
            Assert.IsTrue(ResourceIdentity.IsRestResourceIdentity("Patient/4/_history/5"));
            Assert.IsTrue(ResourceIdentity.IsRestResourceIdentity("http://nu.nl/fhir/Patient/4"));
            Assert.IsFalse(ResourceIdentity.IsRestResourceIdentity("http://nu.nl/fhir/crap"));
            Assert.IsFalse(ResourceIdentity.IsRestResourceIdentity("x/y/z/4"));
            Assert.IsFalse(ResourceIdentity.IsRestResourceIdentity("urn:oid:1.2.3.4.5"));
        }

    }
}
