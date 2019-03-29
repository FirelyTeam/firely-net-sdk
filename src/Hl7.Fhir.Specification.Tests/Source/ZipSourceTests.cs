using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source
{
    [TestClass]
    public class ZipSourceTests
    {
        /// <summary>
        /// This unittest proves issue https://github.com/FirelyTeam/fhir-net-api/issues/883
        /// It will use the zipfile 'ResourcesInSubfolder.zip' in folder TestData"/>  
        /// </summary>
        [TestMethod]
        public void ListSummariesIncludingSubdirectories()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var zip = new ZipSource(zipfile, new DirectorySourceSettings() { IncludeSubDirectories = true });
            var summaries = zip.ListSummaries();

            Assert.IsNotNull(summaries, "Collection of summeries should not be null");
            Assert.AreEqual(20, summaries.Count(), "In the zipfile there are 20 resources distrubuted over several folders in the zipfile.");
        }

        [TestMethod]
        public void ListSummariesExcludingSubdirectories()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var zip = new ZipSource(zipfile, new DirectorySourceSettings() { IncludeSubDirectories = false });
            var summaries = zip.ListSummaries();

            Assert.IsNotNull(summaries, "Collection of summeries should not be null");
            Assert.AreEqual(1, summaries.Count(), "In the zipfile there is 1 resource in the root folder.");
        }
    }
}
