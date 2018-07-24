/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    // [WMR 20171016] Renamed from: ArtifactResolverTests

    [TestClass]
    public class ConformanceSourceTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static IConformanceSource source = null;


        [TestMethod]
        public void FindConceptMaps()
        {
            var conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use");
            Assert.AreEqual(2, conceptMaps.Count());
            Assert.IsNotNull(conceptMaps.First().GetOrigin());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v2-0190");
            Assert.AreEqual(1, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v3-AddressUse");
            Assert.AreEqual(1, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/somethingelse");
            Assert.AreEqual(0, conceptMaps.Count());
        }

        [TestMethod]
        public void FindValueSets()
        {
            // A Fhir valueset
            var vs = source.FindValueSetBySystem("http://hl7.org/fhir/contact-point-system");
            Assert.IsNotNull(vs);
            Assert.IsNotNull(vs.GetOrigin());

            // A non-HL7 valueset
            vs = source.FindValueSetBySystem("http://nema.org/dicom/dicm");
            Assert.IsNotNull(vs);

            // One from v2-tables
            vs = source.FindValueSetBySystem("http://hl7.org/fhir/v2/0145");
            Assert.IsNotNull(vs);

            // One from v3-codesystems
            vs = source.FindValueSetBySystem("http://hl7.org/fhir/v3/ActCode");
            Assert.IsNotNull(vs);

            // Something non-existent
            vs = source.FindValueSetBySystem("http://nema.org/dicom/dicmQQQQ");
            Assert.IsNull(vs);
        }

        [TestMethod]
        public void FindNamingSystem()
        {
            var ns = source.FindNamingSystem("2.16.840.1.113883.6.88");
            Assert.IsNotNull(ns);
            Assert.IsNotNull(ns.GetOrigin());

            ns = source.FindNamingSystem("http://www.nlm.nih.gov/research/umls/rxnorm");
            Assert.IsNotNull(ns);
            Assert.AreEqual("RxNorm (US NLM)", ns.Name);
        }


        [TestMethod]
        public void ListCanonicalUris()
        {
            var vs = source.ListResourceUris(ResourceType.ValueSet); Assert.IsTrue(vs.Any());
            var cm = source.ListResourceUris(ResourceType.ConceptMap); Assert.IsTrue(cm.Any());
            var ns = source.ListResourceUris(ResourceType.NamingSystem); Assert.IsTrue(ns.Any());
            var sd = source.ListResourceUris(ResourceType.StructureDefinition); Assert.IsTrue(sd.Any());
            var de = source.ListResourceUris(ResourceType.DataElement); Assert.IsTrue(de.Any());
            var cf = source.ListResourceUris(ResourceType.Conformance); Assert.IsTrue(cf.Any());
            var od = source.ListResourceUris(ResourceType.OperationDefinition); Assert.IsTrue(od.Any());
            var sp = source.ListResourceUris(ResourceType.SearchParameter); Assert.IsTrue(sp.Any());
            var all = source.ListResourceUris();

            Assert.AreEqual(vs.Count() + cm.Count() + ns.Count() + sd.Count() + de.Count() + cf.Count() + od.Count() + sp.Count(), all.Count());

            Assert.IsTrue(vs.Contains("http://hl7.org/fhir/ValueSet/contact-point-system"));
            Assert.IsTrue(cm.Contains("http://hl7.org/fhir/ConceptMap/v2-contact-point-use"));
            Assert.IsTrue(ns.Contains("http://hl7.org/fhir/NamingSystem/tx-rxnorm"));
            Assert.IsTrue(sd.Contains("http://hl7.org/fhir/StructureDefinition/shareablevalueset"));
            Assert.IsTrue(de.Contains("http://hl7.org/fhir/DataElement/Device.manufactureDate"));
            Assert.IsTrue(sp.Contains("http://hl7.org/fhir/SearchParameter/Condition-onset-info"));
            Assert.IsTrue(od.Contains("http://hl7.org/fhir/OperationDefinition/ValueSet-validate-code"));
            Assert.IsTrue(cf.Contains("http://hl7.org/fhir/Conformance/base"));
        }

        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = source;

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
            var ext = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/diagnosticorder-reason");
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Try to find an additional US profile (they are distributed with the spec for now)
            var us = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/uslab-dr");
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
                @"c:\bla\",
                @"c:\bla\file1.json",
                @"c:\bla\file1.xml",
                @"c:\bla\bla2\file1.json",
                @"c:\bla\bla2\file2.xml",
                @"c:\bla\bla2\text1.txt",
                @"c:\bla\bla2\bla3\test2.jpg",
                @"c:\blie\bla.xml" };
            var basef = @"c:\bla";

            // Basic glob filter
            var fi = new FilePatternFilter("*.xml");
            var r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.EndsWith(".xml")));
            Assert.IsTrue(r.All(f => f.StartsWith(basef)));

            // Absolute select 1 file
            fi = new FilePatternFilter("/file1.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(@"c:\bla\file1.json", r.Single());

            // Absolute select 1 file - no match
            fi = new FilePatternFilter("/file1.crap");
            r = fi.Filter(basef, files);
            Assert.AreEqual(0, r.Count());

            // Absolute select with glob
            fi = new FilePatternFilter("/*.json");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(@"c:\bla\file1.json", r.Single());

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
            Assert.IsTrue(r.All(f => f.Contains("\\file1.")));

            // Relative select file with glob
            fi = new FilePatternFilter("**/*.txt");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(@"c:\bla\bla2\text1.txt", r.Single());

            // Relative select file with glob
            fi = new FilePatternFilter("**/file*.xml");
            r = fi.Filter(basef, files);
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(r.All(f => f.Contains("\\file") && f.EndsWith(".xml")));

            // Relative select file with glob
            fi = new FilePatternFilter("file1.*");
            r = fi.Filter(basef, files);
            Assert.AreEqual(3, r.Count());
            Assert.IsTrue(r.All(f => f.Contains("\\file1.")));

            // Select whole directory
            fi = new FilePatternFilter("bla2/");
            r = fi.Filter(basef, files);
            Assert.AreEqual(4, r.Count());
            Assert.IsTrue(r.All(f => f.Contains("\\bla2\\")));

            // Select whole directory
            fi = new FilePatternFilter("bla2/**");
            r = fi.Filter(basef, files);
            Assert.AreEqual(4, r.Count());
            Assert.IsTrue(r.All(f => f.Contains("\\bla2\\")));

            // Select whole directory
            fi = new FilePatternFilter("/bla3/");
            r = fi.Filter(basef, files);
            Assert.AreEqual(0, r.Count());

            // Internal glob dir
            fi = new FilePatternFilter("/bla2/*/*.jpg");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(@"c:\bla\bla2\bla3\test2.jpg", r.Single());

            // Case-insensitive
            fi = new FilePatternFilter("TEST2.jpg");
            r = fi.Filter(basef, files);
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual(@"c:\bla\bla2\bla3\test2.jpg", r.Single());
        }


        [TestMethod]
        public void TestJsonBundleRetrieval()
        {
            var jsonSource = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData"),
                new DirectorySourceSettings()
                {
                    Mask = "*.json",
                    Includes = new[] { "profiles-types.json" },
                    IncludeSubDirectories = false
                });

            var humanName = jsonSource.FindStructureDefinitionForCoreType(FHIRDefinedType.HumanName);
            Assert.IsNotNull(humanName);
        }

        [TestMethod,Ignore]
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

            using (var stream = jsonSource.LoadArtifactByName("profiles-types.json"))
            {
                Assert.IsNotNull(stream);
            }

            var xmlSource = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings()
                {
                    Mask = "*.xml",
                    Includes = new[] { "profiles-types.xml" },
                    IncludeSubDirectories = false
                });

            using (var stream = xmlSource.LoadArtifactByName("profiles-types.xml"))
            {
                Assert.IsNotNull(stream);
            }

            var xmlSourceLarge = new DirectorySource(
                Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings()
                {
                    Mask = "*.xml",
                    IncludeSubDirectories = true
                });

            using (var stream = xmlSourceLarge.LoadArtifactByName("profiles-types.xml"))
            {
                Assert.IsNotNull(stream);
            }

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
                Debug.WriteLine($"{title} : {(multiThreaded ? "multi" : "single")} threaded, {cnt} resources, duration {sw.ElapsedMilliseconds} ms");
                Assert.IsTrue(sw.ElapsedMilliseconds < maxDuration);
            }
        }

        [TestMethod]
        public async Task TestThreadSafety()
        {
            // Verify thread safety by resolving same uri simultaneously from different threads
            // DirectorySource should synchronize access and only call prepare once.

            const int threadCount = 25;
            const string uri = @"http://example.org/fhir/StructureDefinition/human-group";

            var source = new DirectorySource(Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings { IncludeSubDirectories = true });

            var tasks = new Task[threadCount];
            var results = new(Resource resource, ArtifactSummary summary, int threadId, TimeSpan start, TimeSpan stop)[threadCount];

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < threadCount; i++)
            {
                var idx = i;
                tasks[i] = Task.Run(
                    () =>
                    {
#if DOTNETFW
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
#else
                        const int threadId = 0;
#endif
                        var start = sw.Elapsed;
                        var resource = source.ResolveByCanonicalUri(uri);
                        var summary = source.ListSummaries().ResolveByUri(uri);
                        var stop = sw.Elapsed;
                        results[idx] = (resource, summary, threadId, start, stop);
                    }
                );
            }

            await Task.WhenAll(tasks);
            sw.Stop();

            var first = results[0];
            for (int i = 0; i < threadCount; i++)
            {
                var result = results[i];
                var duration = result.stop.Subtract(result.start);
                Debug.WriteLine($"{i:0#} Thread: {result.threadId:00#} | Start: {result.start.TotalMilliseconds:0000.00} | Stop: {result.stop.TotalMilliseconds:0000.00} | Duration: {duration.TotalMilliseconds:0000.00}");
                Assert.IsNotNull(result.resource);
                Assert.IsNotNull(result.summary);
                // Verify that all threads return the same summary instances
                Assert.AreSame(first.summary, result.summary);
            }
        }

        [TestMethod]
        public void TestRefresh()
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

                // Rename file and refresh source
                const string newFileName = "New" + srcFileName;
                var newFilePath = Path.Combine(tmpFolderPath, newFileName);
                File.Move(tmpFilePath, newFilePath);
                source.Refresh(tmpFilePath, newFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(newFileName, fileNames[0]);

                // Delete file and refresh source
                File.Delete(newFilePath);
                source.Refresh(newFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(0, fileNames.Count);

                // Recreate file and refresh source
                File.Copy(srcFilePath, tmpFilePath);
                source.Refresh(tmpFilePath);
                fileNames = source.ListArtifactNames().ToList();
                Assert.AreEqual(1, fileNames.Count);
                Assert.AreEqual(srcFileName, fileNames[0]);
            }
            finally
            {
                Directory.Delete(tmpFolderPath, true);
            }
        }
    }
}