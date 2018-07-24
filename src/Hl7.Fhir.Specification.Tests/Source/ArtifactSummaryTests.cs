using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ArtifactSummaryTests
    {

        [TestMethod]
        public void TestPatientXmlSummary() => TestPatientSummary(@"TestData\TestPatient.xml");

        [TestMethod]
        public void TestPatientJsonSummary() => TestPatientSummary(@"TestData\TestPatient.json");

        void TestPatientSummary(string path)
        {
            var summary = assertSummary(path);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
        }

        [TestMethod]
        public void TestPatientXmlSummaryWithCustomHarvester()
            => TestPatientSummaryWithCustomHarvester(@"TestData\TestPatient.xml", "Donald");

        [TestMethod]
        public void TestPatientJsonSummaryWithCustomHarvester()
            => TestPatientSummaryWithCustomHarvester(@"TestData\TestPatient.json", "Chalmers");

        void TestPatientSummaryWithCustomHarvester(string path, params string[] expectedNames)
        {
            // Combine default harvesters and custom harvester
            var harvesters = new ArtifactSummaryHarvester[ArtifactSummaryGenerator.ConformanceHarvesters.Length + 1];
            Array.Copy(ArtifactSummaryGenerator.ConformanceHarvesters, harvesters, ArtifactSummaryGenerator.ConformanceHarvesters.Length);
            harvesters[ArtifactSummaryGenerator.ConformanceHarvesters.Length] = HarvestPatientSummary;

            var summary = assertSummary(path, harvesters);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
            var familyNames = summary.GetValueOrDefault<string[]>(PatientFamilyNameKey);
            Assert.IsNotNull(familyNames);
            Assert.AreEqual(1, familyNames.Length);
            Assert.IsTrue(expectedNames.SequenceEqual(familyNames));
        }

        // Custom artifact summary harvester implementation to extract family name(s) from Patient resources
        const string PatientFamilyNameKey = "Patient.name.family";
        static bool HarvestPatientSummary(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (properties.GetTypeName() == ResourceType.Patient.GetLiteral())
            {
                nav.HarvestValues(properties, PatientFamilyNameKey, "name", "family");
                return true;
            }
            return false;
        }

        [TestMethod]
        public void TestValueSetXmlSummary()
        {
            const string path = @"TestData\validation\SectionTitles.valueset.xml";
            var summary = assertSummary(path);

            // Common properties
            Assert.AreEqual(ResourceType.ValueSet.GetLiteral(), summary.ResourceTypeName);
            Assert.IsTrue(summary.ResourceType == ResourceType.ValueSet);

            // Conformance resource properties
            Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
            Assert.AreEqual(@"http://example.org/ValueSet/SectionTitles", summary.GetConformanceCanonicalUrl());
            Assert.AreEqual("MainBundle Section title codes", summary.GetConformanceName());
            Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

            Assert.IsNotNull(summary.GetValueSetSystem());
        }

        [TestMethod]
        public void TestProfilesTypesJson()
        {
            const string path = @"TestData\profiles-types.json";

            var summaries = ArtifactSummaryGenerator.Generate(path);
            Assert.IsNotNull(summaries);
            Assert.AreNotEqual(0, summaries.Count);
            for (int i = 0; i < summaries.Count; i++)
            {
                var summary = summaries[i];
                Assert.IsFalse(summary.IsFaulted);

                // Common properties
                Assert.AreEqual(path, summary.Origin);

                var fi = new FileInfo(path);
                Assert.AreEqual(fi.Length, summary.FileSize);
                Assert.AreEqual(fi.LastWriteTimeUtc, summary.LastModified);

                Assert.AreEqual(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceTypeName);
                Assert.IsTrue(summary.ResourceType == ResourceType.StructureDefinition);

                // Conformance resource properties
                Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
                Assert.IsTrue(summary.GetConformanceCanonicalUrl().ToString().StartsWith("http://hl7.org/fhir/StructureDefinition/"));
                Assert.IsNotNull(summary.GetConformanceName());
                Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

                //Debug.WriteLine($"{summary.ResourceType} | {summary.Canonical()} | {summary.Name()}");

                // StructureDefinition properties
                Assert.IsNotNull(summary.GetStructureDefinitionFhirVersion());
                Assert.AreEqual(ModelInfo.Version, summary.GetStructureDefinitionFhirVersion());

                Assert.AreEqual(StructureDefinition.StructureDefinitionKind.Datatype.GetLiteral(), summary.GetStructureDefinitionKind());
                // If this is a constraining StructDef, then Base should also be specified
                Assert.IsTrue(summary.GetStructureDefinitionConstrainedType() == null || summary.GetStructureDefinitionBase() != null);
            }
        }

        [TestMethod]
        public void TestProfilesResourcesXml()
        {
            const string path = @"TestData\profiles-resources.xml";

            var summaries = ArtifactSummaryGenerator.Generate(path);
            Assert.IsNotNull(summaries);
            Assert.AreNotEqual(0, summaries.Count);
            for (int i = 0; i < summaries.Count; i++)
            {
                var summary = summaries[i];
                Assert.IsFalse(summary.IsFaulted);

                // Common properties
                Assert.AreEqual(path, summary.Origin);

                var fi = new FileInfo(path);
                Assert.AreEqual(fi.Length, summary.FileSize);
                Assert.AreEqual(fi.LastWriteTimeUtc, summary.LastModified);

                if (StringComparer.Ordinal.Equals(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceTypeName))
                {
                    Assert.IsTrue(summary.ResourceType == ResourceType.StructureDefinition);

                    // Conformance resource properties
                    Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
                    Assert.IsTrue(summary.GetConformanceCanonicalUrl().ToString().StartsWith("http://hl7.org/fhir/StructureDefinition/"));
                    Assert.IsNotNull(summary.GetConformanceName());
                    Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

                    //Debug.WriteLine($"{summary.ResourceType} | {summary.Canonical()} | {summary.Name()}");

                    // StructureDefinition properties
                    Assert.IsNotNull(summary.GetStructureDefinitionFhirVersion());
                    Assert.AreEqual(ModelInfo.Version, summary.GetStructureDefinitionFhirVersion());

                    Assert.AreEqual(StructureDefinition.StructureDefinitionKind.Resource.GetLiteral(), summary.GetStructureDefinitionKind());
                    // If this is a constraining StructDef, then Base should also be specified
                    Assert.IsTrue(summary.GetStructureDefinitionConstrainedType() == null || summary.GetStructureDefinitionBase() != null);

                    // [WMR 20171218] Maturity Level extension
                    Assert.IsNotNull(summary.GetStructureDefinitionMaturityLevel());
                }

            }
        }

        ArtifactSummary assertSummary(string path, params ArtifactSummaryHarvester[] harvesters)
        {
            if (harvesters == null || harvesters.Length == 0)
            {
                harvesters = ArtifactSummaryGenerator.ConformanceHarvesters;
            }
            var summaries = ArtifactSummaryGenerator.Generate(path, harvesters);
            Assert.IsNotNull(summaries);
            Assert.AreEqual(1, summaries.Count);
            var summary = summaries[0];
            Assert.IsFalse(summary.IsFaulted);
            Assert.AreEqual(path, summary.Origin);

            var fi = new FileInfo(path);
            Assert.AreEqual(fi.Length, summary.FileSize);
            Assert.AreEqual(fi.LastWriteTimeUtc, summary.LastModified);

            return summary;
        }

#if NET_COMPRESSION

        [TestMethod]
        public void TestZipSummary()
        {
            var source = ZipSource.CreateValidationSource();
            var summaries = source.ListSummaries().ToList();
            Assert.IsNotNull(summaries);
            Assert.AreEqual(7155, summaries.Count);
            Assert.AreEqual(552, summaries.OfResourceType(ResourceType.StructureDefinition).Count());
            Assert.IsTrue(!summaries.Errors().Any());
        }

        [TestMethod]
        public void TestLoadResourceFromZipSource()
        {
            // ZipSource extracts core ZIP archive to (temp) folder, then delegates to DirectorySource
            // i.e. artifact summaries are harvested from files on disk

            var source = ZipSource.CreateValidationSource();
            var summaries = source.ListSummaries();
            var patientUrl = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient);
            var patientSummary = summaries.FindConformanceResources(patientUrl).FirstOrDefault();
            Assert.IsNotNull(patientSummary);
            Assert.AreEqual(ResourceType.StructureDefinition, patientSummary.ResourceType);
            Assert.AreEqual(patientUrl, patientSummary.GetConformanceCanonicalUrl());

            Assert.IsNotNull(patientSummary.Origin);
            var patientStructure = source.LoadBySummary<StructureDefinition>(patientSummary);
            Assert.IsNotNull(patientStructure);
        }

        [TestMethod]
        public void TestLoadResourceFromZipStream()
        {
            // Harvest summaries and load artifact straight from core ZIP archive

            // Use XmlNavigatorStream to navigate resources stored inside a zip file
            // ZipDeflateStream does not support seeking (forward-only stream)
            // Therefore this only works for the XmlNavigatorStream, as the ctor does NOT (need to) call Reset()
            // JsonNavigatorStream cannot support zip streams; ctor needs to call Reset after scanning resourceType

            ArtifactSummary corePatientSummary;
            var corePatientUrl = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient);
            string zipEntryName = "profiles-resources.xml";

            // Generate summaries from core ZIP resource definitions (extract in memory)
            using (var archive = ZipFile.Open(ZipSource.SpecificationZipFileName, ZipArchiveMode.Read))
            {
                var entry = archive.Entries.FirstOrDefault(e => e.Name == zipEntryName);
                Assert.IsNotNull(entry);

                using (var entryStream = entry.Open())
                using (var navStream = new XmlNavigatorStream(entryStream))
                {
                    var summaries = ArtifactSummaryGenerator.Generate(navStream);
                    Assert.IsNotNull(summaries);
                    corePatientSummary = summaries.FindConformanceResources(corePatientUrl).FirstOrDefault();
                }

            }

            Assert.IsNotNull(corePatientSummary);
            Assert.AreEqual(ResourceType.StructureDefinition, corePatientSummary.ResourceType);
            Assert.AreEqual(corePatientUrl, corePatientSummary.GetConformanceCanonicalUrl());

            // Load core Patient resource from ZIP (extract in memory)
            using (var archive = ZipFile.Open(ZipSource.SpecificationZipFileName, ZipArchiveMode.Read))
            {
                var entry = archive.Entries.FirstOrDefault(e => e.Name == zipEntryName);
                using (var entryStream = entry.Open())
                using (var navStream = new XmlNavigatorStream(entryStream))
                {
                    var nav = navStream.Current;
                    if (nav != null)
                    {
                        // Parse target resource from navigator
                        var parser = new BaseFhirParser();
                        var corePatient = parser.Parse<StructureDefinition>(nav);
                        Assert.IsNotNull(corePatient);
                        Assert.AreEqual(corePatientUrl, corePatient.Url);
                    }
                }
            }

        }

#endif

    }
}
