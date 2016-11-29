﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableArtifactSourceTests
#else
    public class ArtifactSourceTests
#endif
    {
#if !PORTABLE45
        [TestMethod]
        public void ZipCacherShouldCache()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var zipFile = Path.Combine(DirectoryExtensions.GetCurrentBinDirectory(), "specification.zip");

            var fa = new ZipCacher(zipFile, cacheKey);

            Assert.IsFalse(fa.IsActual());

            var sw = new Stopwatch();

            sw.Start();
            fa.GetContents();
            sw.Stop();

            var firstRun = sw.ElapsedMilliseconds;

            Assert.IsTrue(fa.IsActual());

            sw.Restart();
            fa.GetContents();
            sw.Stop();

            var secondRun = sw.ElapsedMilliseconds;

            Assert.IsTrue(firstRun > secondRun);

            fa = new ZipCacher(zipFile, cacheKey);

            Assert.IsTrue(fa.IsActual());

            sw.Start();
            fa.GetContents();
            sw.Stop();

            var thirdRun = sw.ElapsedMilliseconds;
            Assert.IsTrue(thirdRun < firstRun);

            fa.Clear();
            Assert.IsFalse(fa.IsActual());

            sw.Restart();
            fa.GetContents();
            sw.Stop();

            var fourthRun = sw.ElapsedMilliseconds;
            Assert.IsTrue(fourthRun > secondRun);

            File.SetLastWriteTime(zipFile, DateTime.Now);
            Assert.IsFalse(fa.IsActual());
        }
#endif

        private void copy(string dir, string file, string outputDir)
        {
            File.Copy(Path.Combine(dir, file), Path.Combine(outputDir, file));
        }

        private string prepareExampleDirectory()
        {
            var zipFile = Path.Combine(DirectoryExtensions.GetCurrentBinDirectory(), "specification.zip");
            var zip = new ZipCacher(zipFile);
            var zipPath = zip.GetContentDirectory();

            var testPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testPath);

            copy(zipPath, "extension-definitions.xml", testPath);
            copy(zipPath, "flag.xsd", testPath);
            copy(zipPath, "patient.sch", testPath);
            copy(@"TestData", "TestPatient.xml", testPath);
            File.WriteAllText(Path.Combine(testPath, "bla.dll"), "This is text, acting as a dll");

            Directory.CreateDirectory(Path.Combine(testPath, "sub"));
            copy(@"TestData", "TestPatient.json", testPath);

            return testPath;
        }


        private string _testPath;

        [TestInitialize]
        public void SetupExampleDir()
        {
            _testPath = prepareExampleDirectory();
        }

        [TestMethod]
        public void UseFileArtifactSource()
        {
            var fa = new DirectorySource(_testPath);
            fa.Mask = "*.xml|*.xsd";
            var names = fa.ListArtifactNames();

            Assert.AreEqual(3, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("flag.xsd"));
            Assert.IsFalse(names.Contains("patient.sch"));

            using (var stream = fa.LoadArtifactByName("TestPatient.xml"))
            {
                var pat = new FhirXmlParser().Parse<Resource>(SerializationUtil.XmlReaderFromStream(stream));
                Assert.IsNotNull(pat);
            }
        }

        [TestMethod]
        public void FileSourceSkipsExecutables()
        {
            var fa = new DirectorySource(_testPath);
            Assert.IsFalse(fa.ListArtifactNames().Any(name => name.EndsWith(".dll")));
            Assert.IsFalse(fa.ListArtifactNames().Any(name => name.EndsWith(".exe")));
        }

        [TestMethod]
        public void ReadsSubdirectories()
        {
            var testPath = prepareExampleDirectory();
            var fa = new DirectorySource(testPath, includeSubdirectories: true);
            var names = fa.ListArtifactNames();

            Assert.AreEqual(5, names.Count());
            Assert.IsTrue(names.Contains("TestPatient.json"));
        }

        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var za = ZipSource.CreateValidationSource();

            using (var a = za.LoadArtifactByName("patient.sch"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.LoadArtifactByName("v3-codesystems.xml"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.LoadArtifactByName("patient.xsd"))
            {
                Assert.IsNotNull(a);
            }
        }

        [TestMethod]
        public void TestZipSourceMask()
        {
            var zipFile = Path.Combine(DirectoryExtensions.GetCurrentBinDirectory(), "specification.zip");
            Assert.IsTrue(File.Exists(zipFile), "Error! specification.zip is not available.");
            var za = new ZipSource(zipFile);
            za.Mask = "profiles-types.xml";

            var artifacts = za.ListArtifactNames().ToArray();
            Assert.AreEqual(1, artifacts.Length);
            Assert.AreEqual("profiles-types.xml", artifacts[0]);

            var resourceIds = za.ListResourceUris(ResourceType.StructureDefinition).ToArray();
            Assert.IsNotNull(resourceIds);
            Assert.IsTrue(resourceIds.Length > 0);
            Assert.IsTrue(resourceIds.All(url => url.StartsWith("http://hl7.org/fhir/StructureDefinition/")));

            // + total number of known FHIR core types
            // - total number of known (concrete) resources
            // - 1 for abstract type Resource
            // - 1 for abstract type DomainResource
            // + 1 xhtml (not present as FhirCsType)
            // =======================================
            //   total number of known FHIR (complex & primitive) datatypes
            var coreDataTypes = ModelInfo.FhirCsTypeToString.Where(kvp => !ModelInfo.IsKnownResource(kvp.Key)
                                                                            && kvp.Value != "Resource"
                                                                            && kvp.Value != "DomainResource"
                                                                            )
                                                            .Select(kvp => kvp.Value).Concat(new[] { "xhtml" });
            var numCoreDataTypes = coreDataTypes.Count();

            Assert.AreEqual(resourceIds.Length, numCoreDataTypes);

            // Assert.IsTrue(resourceIds.All(url => ModelInfo.CanonicalUriForFhirCoreType));
            var coreTypeUris = coreDataTypes.Select(typeName => ModelInfo.CanonicalUriForFhirCoreType(typeName)).ToArray();
            // Boths arrays should contains same urls, possibly in different order
            Assert.AreEqual(coreTypeUris.Length, resourceIds.Length);
            Assert.IsTrue(coreTypeUris.All(url => resourceIds.Contains(url)));
            Assert.IsTrue(resourceIds.All(url => coreTypeUris.Contains(url)));

        }

    }
}