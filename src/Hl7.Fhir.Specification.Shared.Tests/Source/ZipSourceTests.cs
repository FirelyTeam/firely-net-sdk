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
        public void TestConcurrentUnpackAttempts()
        {
            var zipFile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var extractDir  = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var startingSecond = (System.DateTime.UtcNow.Second) % 60;
            Console.Error.WriteLine(extractDir);
            
            var tasks = Enumerable.Range(0, 10).Select(i => new Task(() => _doUnpackAttempt(startingSecond, zipFile, extractDir)));
            
            Assert.IsTrue(Task.WaitAll(tasks.ToArray(), 5000)); 
        }

        private static bool _canStart(int seconds)
        {
            return System.DateTime.UtcNow.Second >= seconds;
        }

        private static void _doUnpackAttempt(int seconds, string zipFile, string extractDir)
        {
            var zip = new ZipSource(zipFile, extractDir,
                            new DirectorySourceSettings() { IncludeSubDirectories = false });
            
            while (!_canStart(seconds)) {}

            var summaries = zip.ListSummaries();
            Console.Error.WriteLine(summaries);
        }
    }
}
