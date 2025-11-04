using FluentAssertions;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class FhirVersionTests
    {
        [TestMethod]
        public void TestFhirReleaseFromVersion()
        {
            Assert.AreEqual(FhirRelease.DSTU1, FhirReleaseParser.Parse("0.01"));
            Assert.AreEqual(FhirRelease.DSTU1, FhirReleaseParser.Parse("0.11"));
            Assert.AreEqual(FhirRelease.DSTU1, FhirReleaseParser.Parse("0.0.80"));

            Assert.AreEqual(FhirRelease.DSTU2, FhirReleaseParser.Parse("0.4.0"));
            Assert.AreEqual(FhirRelease.DSTU2, FhirReleaseParser.Parse("1.0.2"));

            Assert.AreEqual(FhirRelease.STU3, FhirReleaseParser.Parse("1.8.0"));
            Assert.AreEqual(FhirRelease.STU3, FhirReleaseParser.Parse("3.0.0"));
            Assert.AreEqual(FhirRelease.STU3, FhirReleaseParser.Parse("3.0.2"));

            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.Parse("3.5a.0"));
            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.Parse("3.6.0"));
            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.Parse("4.0.0"));
            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.Parse("4.0.1"));

            Assert.AreEqual(FhirRelease.R5, FhirReleaseParser.Parse("4.2.0"));
            Assert.AreEqual(FhirRelease.R5, FhirReleaseParser.Parse("4.5.0"));
            Assert.AreEqual(FhirRelease.R5, FhirReleaseParser.Parse("5.0.0"));
        }
        [TestMethod]
        public void TestTryParseFhirReleaseFromVersion()
        {
            FhirRelease? version = null;
            Assert.IsTrue(FhirReleaseParser.TryParse("0.01", out version));
            Assert.AreEqual(FhirRelease.DSTU1, version);
            Assert.IsTrue(FhirReleaseParser.TryParse("1.0.2", out version));
            Assert.AreEqual(FhirRelease.DSTU2, version);
            Assert.IsTrue(FhirReleaseParser.TryParse("3.0.0", out version));
            Assert.AreEqual(FhirRelease.STU3, version);
            Assert.IsTrue(FhirReleaseParser.TryParse("4.0.0", out version));
            Assert.AreEqual(FhirRelease.R4, version);
            Assert.IsTrue(FhirReleaseParser.TryParse("5.0.0", out version));
            Assert.AreEqual(FhirRelease.R5, version);

            Assert.IsFalse(FhirReleaseParser.TryParse("0.0.0.0.1", out version));
            Assert.IsNull(version);
        }


        [TestMethod]
        public void TestsFhirVersionFromRelease()
        {
            Assert.AreEqual("0.0.82", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.DSTU1));
            Assert.AreEqual("1.0.2", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.DSTU2));
            Assert.AreEqual("3.0.2", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.STU3));
            Assert.AreEqual("4.0.1", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.R4));
            Assert.AreEqual("4.3.0", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.R4B));
            Assert.AreEqual("5.0.0", FhirReleaseParser.FhirVersionFromRelease(FhirRelease.R5));
        }

        [TestMethod]
        public void TestsMimeVersionFromRelease()
        {
            Assert.AreEqual("0.0", FhirReleaseParser.MimeVersionFromFhirRelease(FhirRelease.DSTU1));
            Assert.AreEqual("1.0", FhirReleaseParser.MimeVersionFromFhirRelease(FhirRelease.DSTU2));
            Assert.AreEqual("3.0", FhirReleaseParser.MimeVersionFromFhirRelease(FhirRelease.STU3));
            Assert.AreEqual("4.0", FhirReleaseParser.MimeVersionFromFhirRelease(FhirRelease.R4));
            Assert.AreEqual("5.0", FhirReleaseParser.MimeVersionFromFhirRelease(FhirRelease.R5));
        }

        [TestMethod]
        public void TestsFhirVersionFromMimeVersion()
        {
            Assert.AreEqual(FhirRelease.DSTU1, FhirReleaseParser.FhirReleaseFromMimeVersion("0.0"));
            Assert.AreEqual(FhirRelease.DSTU2, FhirReleaseParser.FhirReleaseFromMimeVersion("1.0"));
            Assert.AreEqual(FhirRelease.STU3, FhirReleaseParser.FhirReleaseFromMimeVersion("3.0"));
            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.FhirReleaseFromMimeVersion("4.0"));
            Assert.AreEqual(FhirRelease.R5, FhirReleaseParser.FhirReleaseFromMimeVersion("5.0"));
        }

        [TestMethod]
        public void TestTryFhirVersionFromMimeVersion()
        {
            FhirRelease? version = null;
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("0.0", out version));
            Assert.AreEqual(FhirRelease.DSTU1, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("1.0", out version));
            Assert.AreEqual(FhirRelease.DSTU2, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("3.0", out version));
            Assert.AreEqual(FhirRelease.STU3, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("4.0", out version));
            Assert.AreEqual(FhirRelease.R4, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("5.0", out version));
            Assert.AreEqual(FhirRelease.R5, version);

            Assert.IsFalse(FhirReleaseParser.TryGetFhirReleaseFromMimeVersion("0.0.0.0.1", out version));
            Assert.IsNull(version);
        }

        [TestMethod]
        public void TestFhirCorePackageFromFhirVersion()
        {
            Assert.AreEqual("hl7.fhir.r3.core", FhirReleaseParser.CorePackageNameFromFhirRelease(FhirRelease.STU3));
            Assert.AreEqual("hl7.fhir.r4.core", FhirReleaseParser.CorePackageNameFromFhirRelease(FhirRelease.R4));
            Assert.AreEqual("hl7.fhir.r5.core", FhirReleaseParser.CorePackageNameFromFhirRelease(FhirRelease.R5));
        }

        [TestMethod]
        public void FhirReleaseFromCorePackageName()
        {
            Assert.AreEqual(FhirRelease.STU3, FhirReleaseParser.FhirReleaseFromCorePackageName("hl7.fhir.r3.core"));
            Assert.AreEqual(FhirRelease.R4, FhirReleaseParser.FhirReleaseFromCorePackageName("hl7.fhir.r4.core"));
            Assert.AreEqual(FhirRelease.R5, FhirReleaseParser.FhirReleaseFromCorePackageName("hl7.fhir.r5.core"));
        }

        [TestMethod]
        public void TestTryFhirReleaseFromCorePackageName()
        {
            FhirRelease? version = null;
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromCorePackageName("hl7.fhir.r3.core", out version));
            Assert.AreEqual(FhirRelease.STU3, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromCorePackageName("hl7.fhir.r4.core", out version));
            Assert.AreEqual(FhirRelease.R4, version);
            Assert.IsTrue(FhirReleaseParser.TryGetFhirReleaseFromCorePackageName("hl7.fhir.r5.core", out version));
            Assert.AreEqual(FhirRelease.R5, version);

            Assert.IsFalse(FhirReleaseParser.TryGetFhirReleaseFromCorePackageName("hl7.fhir.core.r3", out version));
            Assert.IsNull(version);

        }

        [TestMethod]
        public void TestOrdeningOfFhirRelease()
        {
            IEnumerable<FhirRelease> listOfReleases = new[]
            {
                FhirRelease.DSTU1,
                FhirRelease.DSTU2,
                FhirRelease.R4,
                FhirRelease.R4B,
                FhirRelease.R5,
            };
            listOfReleases.Should().BeInAscendingOrder();
        }


    }
}
