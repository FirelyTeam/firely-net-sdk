using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source
{
    [TestClass]
    public class ZipSourceTests
    {
        /// <summary>
        /// This unittest proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/883
        /// It will use the zipfile 'ResourcesInSubfolder.zip' in folder TestData"/>  
        /// </summary>
        [TestMethod]
        public void ListSummariesIncludingSubdirectories()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var zip = new ZipSource(zipfile, extractDir, new DirectorySourceSettings() { IncludeSubDirectories = true });
            var summaries = zip.ListSummaries();

            Assert.IsNotNull(summaries, "Collection of summeries should not be null");
            Assert.AreEqual(20, summaries.Count(), "In the zipfile there are 20 resources distrubuted over several folders in the zipfile.");
        }

        [TestMethod]
        public void ListSummariesExcludingSubdirectories()
        {
            var zipfile = Path.Combine("TestData", "ResourcesInSubfolder.zip");
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var zip = new ZipSource(zipfile, extractDir, new DirectorySourceSettings() { IncludeSubDirectories = false });
            var summaries = zip.ListSummaries();

            Assert.IsNotNull(summaries, "Collection of summeries should not be null");
            Assert.AreEqual(1, summaries.Count(), "In the zipfile there is 1 resource in the root folder.");
        }


        //issue #2031
        [TestMethod]
        public void TestIncorrectFullUrlForValuesetComposeIncludeValueSetTitle()
        {
            var _resolver = ZipSource.CreateValidationSource();
            var stream = _resolver.LoadArtifactByName("extension-definitions.xml");
            var text = new StreamReader(stream).ReadToEnd();
            var bundle = new FhirXmlParser().Parse<Bundle>(text);

            var extensionEntry = bundle.Entry.Where(e => e.FullUrl == "http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle").FirstOrDefault();
            extensionEntry.Should().NotBeNull();
            var sd = extensionEntry.Resource as StructureDefinition;
            sd.Url.Should().Be("http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle");
            sd.Id.Should().Be("valueset-compose-include-valueSetTitle");
        }

    }
}
