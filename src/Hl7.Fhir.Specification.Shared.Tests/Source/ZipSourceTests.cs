using FluentAssertions;
using FluentAssertions.Extensions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Specification.Tests.Source
{
    [TestClass]
    public partial class ZipSourceTests
    {
        /// <summary>
        /// This unittest proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/883
        /// It will use the zipfile 'ResourcesInSubfolder.zip' in folder TestData"/>  
        /// </summary>
        [TestMethod]
        public void ListSummariesIncludingSubdirectories()
        {
            var zip = unpackTestData(new DirectorySourceSettings { IncludeSubDirectories = true });
            var summaries = zip.ListSummaries();

            Assert.IsNotNull(summaries, "Collection of summaries should not be null");
            Assert.AreEqual(20, summaries.Count(), "In the zipfile there are 20 resources distrubuted over several folders in the zipfile.");
            summaries.First().Origin.Should().StartWith(zip.ExtractPath);
        }

        [TestMethod]
        public void ListSummariesExcludingSubdirectories()
        {
            var zip = unpackTestData(new DirectorySourceSettings { IncludeSubDirectories = false });
            var summaries = zip.ListSummaries();
            summaries.First().Origin.StartsWith(zip.ExtractPath).Should().BeTrue();

            Assert.IsNotNull(summaries, "Collection of summaries should not be null");
            Assert.AreEqual(1, summaries.Count(), "In the zipfile there is 1 resource in the root folder.");
            summaries.First().Origin.Should().StartWith(zip.ExtractPath);
        }

        private static ZipSource unpackTestData(DirectorySourceSettings settings)
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            return new ZipSource(zipfile, settings);
        }

        [TestMethod]
        public void UnpacksToSpecificDirectory()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var zip = new ZipSource(zipfile, extractDir, new DirectorySourceSettings { IncludeSubDirectories = false });
            var summaries = zip.ListSummaries();
            summaries.First().Origin.Should().StartWith(extractDir);
        }

        [TestMethod]
        // If this test fails, the specification might have changed. This filename is hardcoded into the validation of a successful ZIP unpack
        // (firely-net-sdk/src/Hl7.Fhir.Base/Specification/Source/ZipCacher.cs:74)
        public void TestFilePrescence()
        {
            var zip = ZipSource.CreateValidationSource();
            zip.ListSummaries(); // make sure the zip is unpacked, we don't need the return value
            // if extractpath is null, something went seriously wrong
            File.Exists(Path.Combine(zip.ExtractPath!, "profiles-types.xml")).Should().BeTrue();
        }

        [TestMethod]
        public void ExtractionRetriesAfterClearingCache()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var zip = new ZipSource(zipfile, extractDir, new DirectorySourceSettings { IncludeSubDirectories = true });

            var summaries = zip.ListSummaries();
            Assert.IsNotNull(summaries, "Initial extraction should succeed");
            Assert.AreEqual(20, summaries.Count(), "ResourcesInSubfolder.zip contains 20 resources");

            var cachedDir = Path.Combine(extractDir, "ResourcesInSubfolder");
            Directory.Exists(cachedDir).Should().BeTrue();

            Directory.Delete(cachedDir, recursive: true);
            Directory.Exists(cachedDir).Should().BeFalse();

            var zip2 = new ZipSource(zipfile, extractDir, new DirectorySourceSettings { IncludeSubDirectories = true });
            var summaries2 = zip2.ListSummaries();

            Assert.AreEqual(summaries.Count(), summaries2.Count(), "Cache should be repopulated with same resources");
            Directory.Exists(cachedDir).Should().BeTrue();

            Directory.Delete(extractDir, recursive: true);
        }

        [TestMethod]
        public void ExtractionFailsBothTimesWithCorruptedZip()
        {
            var tempZipPath = Path.Combine(Path.GetTempPath(), "corrupted_" + Guid.NewGuid() + ".zip");
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            try
            {
                File.WriteAllText(tempZipPath, "This is not a valid zip file");
                var zip = new ZipSource(tempZipPath, extractDir);
                
                var action = () => zip.ListSummaries();
                action.Should().Throw<Exception>("corrupted zip file should fail extraction");
            }
            finally
            {
                if (File.Exists(tempZipPath))
                    File.Delete(tempZipPath);
                if (Directory.Exists(extractDir))
                    Directory.Delete(extractDir, recursive: true);
            }
        }
    }
}
