/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
// Use alias to avoid conflict with Hl7.Fhir.Model.Task
using T = System.Threading.Tasks;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Specification.Tests
{
    // [WMR 20171016] Renamed from: ArtifactResolverTests

    [TestClass]
    public class ConformanceSourceTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext _)
        {
            source = ZipSource.CreateValidationSource();
        }

        static ZipSource source = null;


        [TestMethod]
        public void FindConceptMaps()
        {
            var conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use");
            Assert.AreEqual(3, conceptMaps.Count());
            Assert.IsNotNull(conceptMaps.First().GetOrigin());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v2-0190");
            Assert.AreEqual(1, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v3-AddressUse");
            Assert.AreEqual(2, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/somethingelse");
            Assert.AreEqual(0, conceptMaps.Count());
        }

        [TestMethod]
        public async T.Task FindCodeSystem()
        {
            // A Fhir codesystem
            var vs = await source.FindCodeSystemAsync("http://hl7.org/fhir/contact-point-system");
            Assert.IsNotNull(vs);
            Assert.IsNotNull(vs.GetOrigin());

            // A non-HL7 valueset
            vs = await source.FindCodeSystemAsync("http://dicom.nema.org/resources/ontology/DCM"); // http://nema.org/dicom/dicm");
            Assert.IsNotNull(vs);

            // One from v2-tables
            vs = await source.FindCodeSystemAsync("http://hl7.org/fhir/v2/0145");
            Assert.IsNotNull(vs);

            // One from v3-codesystems
            vs = await source.FindCodeSystemAsync("http://hl7.org/fhir/v3/ActCode");
            Assert.IsNotNull(vs);

            // Something non-existent
            vs = await source.FindCodeSystemAsync("http://nema.org/dicom/dicmQQQQ");
            Assert.IsNull(vs);
        }


        [TestMethod]
        public async T.Task FindValueSets()
        {
            // A Fhir valueset
            var vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/contact-point-system");
            Assert.IsNotNull(vs);
            Assert.IsNotNull(vs.GetOrigin());

            //EK: These seem to be no longer part of the spec
            //// A non-HL7 valueset
            //vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/dicom-dcim");
            //Assert.IsNotNull(vs);

            // One from v2-tables
            vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/v2-0145");
            Assert.IsNotNull(vs);

            // One from v3-codesystems
            vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/v3-ActCode");
            Assert.IsNotNull(vs);

            // Something non-existent
            vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/crapQQQQ");
            Assert.IsNull(vs);
        }

        [TestMethod]
        public void FindNamingSystem()
        {
            var ns = source.FindNamingSystem("2.16.840.1.113883.4.1");
            Assert.IsNotNull(ns);
            Assert.IsNotNull(ns.GetOrigin());

            ns = source.FindNamingSystem("http://hl7.org/fhir/sid/us-ssn");
            Assert.IsNotNull(ns);
            Assert.AreEqual("United States Social Security Number", ns.Name);
        }
    
        // Need to fix this for new conformance stuff in STU3
        [TestMethod]
        public void ListCanonicalUris()
        {
            var sd = source.ListResourceUris(ResourceType.StructureDefinition); Assert.IsTrue(sd.Any());
            var sm = source.ListResourceUris(ResourceType.StructureMap); Assert.IsFalse(sm.Any());
            var cf = source.ListResourceUris(ResourceType.CapabilityStatement); Assert.IsTrue(cf.Any());
            var md = source.ListResourceUris(ResourceType.MessageDefinition); Assert.IsFalse(md.Any());
            var od = source.ListResourceUris(ResourceType.OperationDefinition); Assert.IsTrue(od.Any());
            var sp = source.ListResourceUris(ResourceType.SearchParameter); Assert.IsTrue(sp.Any());
            var cd = source.ListResourceUris(ResourceType.CompartmentDefinition); Assert.IsFalse(md.Any());
            var ig = source.ListResourceUris(ResourceType.ImplementationGuide); Assert.IsFalse(ig.Any());

            var cs = source.ListResourceUris(ResourceType.CodeSystem); Assert.IsTrue(cs.Any());
            var vs = source.ListResourceUris(ResourceType.ValueSet); Assert.IsTrue(vs.Any());
            var cm = source.ListResourceUris(ResourceType.ConceptMap); Assert.IsTrue(cm.Any());
            var ep = source.ListResourceUris(ResourceType.ExpansionProfile); Assert.IsFalse(ep.Any());
            var ns = source.ListResourceUris(ResourceType.NamingSystem); Assert.IsTrue(ns.Any());

            var all = source.ListResourceUris();

            Assert.AreEqual(sd.Count() + sm.Count() + cf.Count() + md.Count() + od.Count() +
                        sp.Count() + cd.Count() + ig.Count() + cs.Count() + vs.Count() + cm.Count() +
                        ep.Count() + ns.Count(), all.Count());

            Assert.IsTrue(sd.Contains("http://hl7.org/fhir/StructureDefinition/shareablevalueset"));
            Assert.IsTrue(cf.Contains("http://hl7.org/fhir/CapabilityStatement/base"));
            Assert.IsTrue(od.Contains("http://hl7.org/fhir/OperationDefinition/ValueSet-validate-code"));
            Assert.IsTrue(sp.Contains("http://hl7.org/fhir/SearchParameter/Condition-onset-info"));
            Assert.IsTrue(cs.Contains("http://hl7.org/fhir/CodeSystem/contact-point-system"));
            Assert.IsTrue(vs.Contains("http://hl7.org/fhir/ValueSet/contact-point-system"));
            Assert.IsTrue(cm.Contains("http://hl7.org/fhir/ConceptMap/cm-name-use-v2"));
            Assert.IsTrue(ns.Contains("http://hl7.org/fhir/NamingSystem/us-ssn"));
        }

        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = ZipSource.CreateValidationSource();

            var vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);
            Assert.IsTrue(vs.GetOrigin().EndsWith("v2-tables.xml"));

            vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/administrative-gender");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/location-status");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Condition");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);
            Assert.IsTrue(rs.GetOrigin().EndsWith("profiles-resources.xml"));

            rs = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/ValueSet");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);

            var dt = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Money");
            Assert.IsNotNull(dt);
            Assert.IsTrue(dt is StructureDefinition);

            // Try to find a core extension
            var ext = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/valueset-history");
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Try to find an additional non-hl7 profile (they are distributed with the spec for now)
            var us = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/cqif-questionnaire");
            Assert.IsNotNull(us);
            Assert.IsTrue(us is StructureDefinition);
        }

        [TestMethod]
        public void TestFilenameDeDuplication()
        {
            var paths = new List<string> { @"c:\blie\bla.txt", @"c:\bla\bla.txt", @"c:\blie\bla.txt", @"c:\yadi.json",
                                @"c:\blie\bit.xml", @"c:\blie\bit.json", @"c:\blie\bit.txt" };

            var res = DirectorySource.ResolveDuplicateFilenames(paths, DirectorySource.DuplicateFilenameResolution.PreferXml);
            Assert.AreEqual(5, res.Count);
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.xml")));
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.txt")));
            Assert.IsFalse(res.Any(p => p.EndsWith("bit.json")));
            Assert.IsTrue(res.Any(p => p.EndsWith("yadi.json")));

            res = DirectorySource.ResolveDuplicateFilenames(paths, DirectorySource.DuplicateFilenameResolution.PreferJson);
            Assert.AreEqual(5, res.Count);
            Assert.IsFalse(res.Any(p => p.EndsWith("bit.xml")));
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.txt")));
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.json")));
            Assert.IsTrue(res.Any(p => p.EndsWith("yadi.json")));

            res = DirectorySource.ResolveDuplicateFilenames(paths, DirectorySource.DuplicateFilenameResolution.KeepBoth);
            Assert.AreEqual(6, res.Count);
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.xml")));
            Assert.IsTrue(res.Any(p => p.EndsWith("bit.json")));
        }

        [TestMethod]
        public void TestIgnoreFilter()
        {
            var files = new[] {
                Path.Combine("c:", "bla"),
                Path.Combine("c:", "bla", "file1.json"),
                Path.Combine("c:", "bla", "file1.xml"),
                Path.Combine("c:", "bla", "bla2", "file1.json"),
                Path.Combine("c:", "bla", "bla2", "file2.xml"),
                Path.Combine("c:", "bla", "bla2", "text1.txt"),
                Path.Combine("c:", "bla", "bla2", "bla3", "test2.jpg"),
                Path.Combine("c:", "blie", "bla.xml") };
            var basef = Path.Combine("c:", "bla");

            // Basic glob filter
            var fi = new FilePatternFilter("*.xml");
            var r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.EndsWith(".xml")));
            Assert.IsTrue(r.All(f => f.StartsWith(basef)));

            // Absolute select 1 file
            fi = new FilePatternFilter("/file1.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(Path.Combine("c:", "bla", "file1.json"), r.Single());

            // Absolute select 1 file - no match
            fi = new FilePatternFilter("/file1.crap");
            r = fi.Filter(basef, files);
            Assert.AreEqual(0, r.Count());

            // Absolute select with glob
            fi = new FilePatternFilter("/*.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(Path.Combine("c:", "bla", "file1.json"), r.Single());

            // Relative select file
            fi = new FilePatternFilter("file1.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.EndsWith("file1.json")));

            // Relative select file
            fi = new FilePatternFilter("**/file1.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.EndsWith("file1.json")));

            // Relative select file with glob
            fi = new FilePatternFilter("**/file1.*");
            r = fi.Filter(basef, files);
            Assert.AreEqual(3, r.Count());
            Assert.IsTrue(r.All(f => f.Contains($"{Path.DirectorySeparatorChar}file1.")));

            // Relative select file with glob
            fi = new FilePatternFilter("**/*.txt");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(Path.Combine("c:", "bla", "bla2", "text1.txt"), r.Single());

            // Relative select file with glob
            fi = new FilePatternFilter("**/file*.xml");
            r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.Contains($"{Path.DirectorySeparatorChar}file") && f.EndsWith(".xml")));

            // Relative select file with glob
            fi = new FilePatternFilter("file1.*");
            r = fi.Filter(basef, files);
            Assert.AreEqual(3, r.Count());
            Assert.IsTrue(r.All(f => f.Contains($"{Path.DirectorySeparatorChar}file1.")));

            // Select whole directory
            fi = new FilePatternFilter("bla2/");
            r = fi.Filter(basef, files);
            Assert.AreEqual(4, r.Count());
            Assert.IsTrue(r.All(f => f.Contains($"{Path.DirectorySeparatorChar}bla2{Path.DirectorySeparatorChar}")));

            // Select whole directory
            fi = new FilePatternFilter("bla2/**");
            r = fi.Filter(basef, files);
            Assert.AreEqual(4, r.Count());
            Assert.IsTrue(r.All(f => f.Contains($"{Path.DirectorySeparatorChar}bla2{Path.DirectorySeparatorChar}")));

            // Select whole directory
            fi = new FilePatternFilter("/bla3/");
            r = fi.Filter(basef, files);
            Assert.AreEqual(0, r.Count());

            // Internal glob dir
            fi = new FilePatternFilter("/bla2/*/*.jpg");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(Path.Combine("c:", "bla", "bla2", "bla3", "test2.jpg"), r.Single());

            // Case-insensitive
            fi = new FilePatternFilter("TEST2.jpg");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(Path.Combine("c:", "bla", "bla2", "bla3", "test2.jpg"), r.Single());
        }


        [TestMethod]
        public async T.Task TestJsonBundleRetrieval()
        {
            var jsonSource = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData"),
                new DirectorySourceSettings()
                {
                    Mask = "*.json",
                    Includes = new[] { "profiles-types.json" },
                    IncludeSubDirectories = false
                });

            var humanName = await jsonSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.HumanName);
            Assert.IsNotNull(humanName);
        }

#if false
        public void TestSourceSpeedTest()
        {
            var jsonSource = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData"),
                new DirectorySourceSettings()
                {
                    Mask = "*.json",
                    Includes = new[] { "profiles-types.json" },
                    IncludeSubDirectories = false
                });

            Assert.IsNotNull(jsonSource.LoadArtifactByName("profiles-types.json"));

            var xmlSource = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings()
                {
                    Mask = "*.xml",
                    Includes = new[] { "profiles-types.xml" },
                    IncludeSubDirectories = false
                });

            Assert.IsNotNull(xmlSource.LoadArtifactByName("profiles-types.xml"));

            var xmlSourceLarge = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings()
                {
                    Mask = "*.xml",
                    IncludeSubDirectories = true
                });

            Assert.IsNotNull(xmlSourceLarge.LoadArtifactByName("profiles-types.xml"));

            runTest("profiles-types.json", jsonSource, false, 1000);
            runTest("profiles-types.xml", xmlSource, false, 500);
            runTest("all xml examples", xmlSourceLarge, false, 10000);

            runTest("profiles-types.json", jsonSource, true, 1000);
            runTest("profiles-types.xml", xmlSource, true, 500);
            runTest("all xml examples", xmlSourceLarge, true, 10000);

            void runTest(string title, DirectorySource s, bool multiThreaded, long maxDuration)
            {
                var sw = new Stopwatch();
                sw.Start();

                int cnt = 0;
                s.MultiThreaded = multiThreaded;
                for (var repeat = 0; repeat < 10; repeat++)
                {
                    s.Refresh();  // force reload of whole file
                    cnt = s.ListResourceUris().Count();
                }

                sw.Stop();
                Console.WriteLine($"{title} : {(multiThreaded ? "multi" : "single")} threaded, {cnt} resources, duration {sw.ElapsedMilliseconds} ms");
                Assert.IsTrue(sw.ElapsedMilliseconds < maxDuration);
            }
        }
#endif
        [TestMethod]
        public async T.Task TestThreadSafety()
        {
            // Verify thread safety by resolving same uri simultaneously from different threads
            // DirectorySource should synchronize access and only call prepare once.

            const int threadCount = 25;
            const string uri = @"http://example.org/fhir/StructureDefinition/human-group";

            var source = new DirectorySource(Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings { IncludeSubDirectories = true });

            var tasks = new T.Task[threadCount];
            var results = new(Resource resource, ArtifactSummary summary, int threadId, TimeSpan start, TimeSpan stop)[threadCount];

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < threadCount; i++)
            {
                var idx = i;
                tasks[i] = T.Task.Run(
                    () =>
                    {
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var start = sw.Elapsed;
                        var resource = source.ResolveByCanonicalUri(uri);
                        var summary = source.ListSummaries().ResolveByUri(uri);
                        var stop = sw.Elapsed;
                        results[idx] = (resource, summary, threadId, start, stop);
                    }
                );
            }

            await T.Task.WhenAll(tasks);
            sw.Stop();

            var first = results[0];
            for (int i = 0; i < threadCount; i++)
            {
                var (resource, summary, threadId, start, stop) = results[i];
                var duration = stop.Subtract(start);
                Debug.WriteLine($"{i:0#} Thread: {threadId:00#} | Start: {start.TotalMilliseconds:0000.00} | Stop: {stop.TotalMilliseconds:0000.00} | Duration: {duration.TotalMilliseconds:0000.00}");
                Assert.IsNotNull(resource);
                Assert.IsNotNull(summary);
                // Verify that all threads return the same summary instances
                Assert.AreSame(first.summary, summary);
            }
        }

        [TestMethod]
        public async T.Task TestRefreshAll() => await TestRefreshAsync(true);

        [TestMethod]
        public async T.Task TestRefreshFile() => await TestRefreshAsync(false);

        async T.Task TestRefreshAsync(bool refreshAll)
        {
            // Create a temporary folder with a single artifact file
            const string srcFileName = "TestPatient.xml";
            var srcFilePath = Path.Combine(DirectorySource.SpecificationDirectory, "TestData", srcFileName);
            var tmpFolderPath = Path.Combine(DirectorySource.SpecificationDirectory, "ConformanceSourceTestData");
            try
            {
                Directory.CreateDirectory(tmpFolderPath);
                var tmpFilePath = Path.Combine(tmpFolderPath, srcFileName);
                File.Copy(srcFilePath, tmpFilePath);

                // Initialize source and verify index
                var source = new DirectorySource(tmpFolderPath);
                var fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(srcFileName, fileNames[0]);

                void Refresh(params string[] files)
                {
                    if (refreshAll)
                    {
                        source.Refresh();
                    }
                    else
                        source.Refresh(files);
                }

                // Rename file and refresh source
                const string newFileName = "New" + srcFileName;
                var newFilePath = Path.Combine(tmpFolderPath, newFileName);
                File.Move(tmpFilePath, newFilePath);
                Refresh(tmpFilePath, newFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(newFileName, fileNames[0]);

                // Delete file and refresh source
                File.Delete(newFilePath);
                Refresh(newFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(0, fileNames.Count);

                // Recreate file and refresh source
                File.Copy(srcFilePath, tmpFilePath);
                Refresh(tmpFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(srcFileName, fileNames[0]);

                // [WMR 20190528] Update file and refresh source
                var summaries = source.ListSummaries().ToList();
                Assert.IsNotNull(summaries);
                Assert.AreEqual(1, summaries.Count);
                var summary = summaries[0];
                var uri = summary.ResourceUri;
                const string uriPrefix = @"http://example.org/Patient/";
                Assert.IsTrue(uri.StartsWith(uriPrefix));
                var id = uri.Substring(uriPrefix.Length);
                Assert.AreEqual("pat1", id);

                // Update the resource id
                Patient patient = null;
                using (var nav = DefaultNavigatorStreamFactory.Create(tmpFilePath))
                {
                    Assert.IsTrue(nav.MoveNext());
                    var node = nav.Current;
                    Assert.IsNotNull(node);
                    patient = node.ToPoco<Patient>();
                }
                Assert.IsNotNull(patient);
                Assert.AreEqual(patient.Id, "pat1");
                patient.Id = "CHANGED";
                var serializer = new FhirXmlSerializer();
                var xml = await serializer.SerializeToStringAsync(patient);
                File.WriteAllText(tmpFilePath, xml);

                // Verify that Refresh updates the summary information
                Refresh(tmpFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(srcFileName, fileNames[0]);

                summaries = source.ListSummaries().ToList();
                Assert.AreEqual(1, summaries.Count);
                summary = summaries[0];
                Assert.AreEqual(uriPrefix + patient.Id, summary.ResourceUri);

            }
            finally
            {
                Directory.Delete(tmpFolderPath, true);
            }
        }

        [TestMethod]
        public async T.Task TestParserSettings()
        {
            // Create an invalid patient resource on disk
            var obs = new Observation()
            {
                Id = "1",               
                Comment = " " // Illegal empty value
            };
            var nav = obs.ToTypedElement();
            var xml = await nav.ToXmlAsync();

            var folderPath = Path.Combine(Path.GetTempPath(), "TestDirectorySource");
            var filePath = Path.Combine(folderPath, "TestPatient.xml");

            try
            {

                Directory.CreateDirectory(folderPath);
                File.WriteAllText(filePath, xml);

                // Try to access using DirectorySource with default settings, but with PermissiveParsing switch off.
                var settings = DirectorySourceSettings.CreateDefault();
                settings.XmlParserSettings.PermissiveParsing = false;
                var src = new DirectorySource(folderPath, settings);

                var uri = NavigatorStreamHelper.FormatCanonicalUrlForBundleEntry(obs.TypeName, obs.Id);
                Assert.AreEqual(@"http://example.org/Observation/1", uri);

                // Expecting resolving to fail, because of illegal empty value
                Assert.ThrowsException<FormatException>(() => { src.ResolveByUri(uri); });

                // Bypass all verification; specifically, accept empty values
                src.XmlParserSettings.PermissiveParsing = true;

                // Expecting resolving to succeed
                var result = src.ResolveByUri(uri);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Observation));

            }
            finally
            {
                // Clean up temporary files
                Directory.Delete(folderPath, true);
            }
        }
    }
}
