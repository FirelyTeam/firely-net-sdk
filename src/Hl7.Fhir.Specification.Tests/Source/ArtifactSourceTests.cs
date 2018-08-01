/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ssac = System.Security.AccessControl;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ArtifactSourceTests
    {
        [TestMethod]
        public void ZipCacherShouldCache()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "specification.zip");

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

        private void copy(string dir, string file, string outputDir)
        {
            File.Copy(Path.Combine(dir, file), Path.Combine(outputDir, file));
        }

        private (string path, int numFiles) prepareExampleDirectory()
        {
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "specification.zip");
            var zip = new ZipCacher(zipFile);
            var zipPath = zip.GetContentDirectory();

            var testPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testPath);

            copy(zipPath, "extension-definitions.xml", testPath);
            copy(zipPath, "flag.xsd", testPath);
            copy(zipPath, "patient.sch", testPath);
            copy(@"TestData", "TestPatient.xml", testPath);
            File.WriteAllText(Path.Combine(testPath, "bla.dll"), "This is text, acting as a dll");
            File.WriteAllText(Path.Combine(testPath, "nonfhir.xml"), "<root>this is not a valid FHIR xml resource.</root>");
            File.WriteAllText(Path.Combine(testPath, "invalid.xml"), "<root>this is invalid xml");

            var subPath = Path.Combine(testPath, "sub");
            Directory.CreateDirectory(subPath);
            copy(@"TestData", "TestPatient.json", subPath);

            // If you add or remove files, please correct the numFiles here below
            var numFiles = 8 - 1;   // 8 files - 1 binary (which should be ignored)

            return (testPath, numFiles);
        }


        private string _testPath;
        private int _numFiles;

        [TestInitialize]
        public void SetupExampleDir()
        {
            (_testPath, _numFiles) = prepareExampleDirectory();
        }

        [TestMethod]
        public void UseFileArtifactSource()
        {
            var fa = new DirectorySource(_testPath);
            fa.Mask = "*.xml|*.xsd";
            var names = fa.ListArtifactNames();

            Assert.AreEqual(5, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("flag.xsd"));
            Assert.IsFalse(names.Contains("patient.sch"));
            Assert.IsTrue(names.Contains("TestPatient.xml"));
            Assert.IsTrue(names.Contains("nonfhir.xml"));
            Assert.IsTrue(names.Contains("invalid.xml"));

            using (var stream = fa.LoadArtifactByName("TestPatient.xml"))
            {
                var pat = new FhirXmlParser().Parse<Resource>(SerializationUtil.XmlReaderFromStream(stream));
                Assert.IsNotNull(pat);
            }
        }

        [TestMethod]
        public void UseIncludeExcludeFilter()
        {
            var fa = new DirectorySource(_testPath);
            fa.Includes = new[] { "*.xml", "pa*.sch" };
            fa.Excludes = new[] { "nonfhir*.*" };

            var names = fa.ListArtifactNames();

            Assert.AreEqual(4, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("TestPatient.xml"));
            Assert.IsFalse(names.Contains("nonfhir.xml"));
            Assert.IsTrue(names.Contains("invalid.xml"));
            Assert.IsTrue(names.Contains("patient.sch"));
        }

        [TestMethod]
        public void FileSourceSkipsInvalidXml()
        {
            var fa = new DirectorySource(_testPath);
            fa.Mask = "*.xml";
            var names = fa.ListArtifactNames();

            Assert.AreEqual(4, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("TestPatient.xml"));
            Assert.IsTrue(names.Contains("nonfhir.xml"));
            Assert.IsTrue(names.Contains("invalid.xml"));
            //[WMR 20171020] TODO: Use ArtifactSummary.Error
            //Assert.AreEqual(0, fa.Errors.Length);

            // Call a method on the IConformanceSource interface to trigger prepareResources
            var sd = fa.FindStructureDefinition("http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-discoveryDateTime");
            Assert.IsNotNull(sd);

            var errors = fa.ListSummaryErrors().ToList();
            Assert.AreEqual(1, errors.Count);
            var error = errors[0];
            Debug.Print($"Error in file '{error.Origin}': {error.Error.Message}");
            Assert.AreEqual("invalid.xml", Path.GetFileName(error.Origin));
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
            // var (testPath, numFiles) = prepareExampleDirectory();
            var (testPath, numFiles) = (_testPath, _numFiles);
            var fa = new DirectorySource(testPath, new DirectorySourceSettings() {  IncludeSubDirectories = true });
            var names = fa.ListArtifactNames();

            Assert.AreEqual(numFiles, names.Count());
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
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "specification.zip");
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

        // [WMR 20170817] NEW
        // https://github.com/ewoutkramer/fhir-net-api/issues/410
        // DirectorySource should gracefully handle insufficient access permissions
        // i.e. silently ignore all inaccessible files & folders

        // Can we modify access permissions on the CI build environment...?
        [TestMethod, TestCategory("IntegrationTest")]
        public void TestAccessPermissions()
        {
            // var (testPath, numFiles) = prepareExampleDirectory();
            var (testPath, numFiles) = (_testPath, _numFiles);

            // Additional temporary folder without read permissions
            var subPath2 = Path.Combine(testPath, "sub2");
            var forbiddenDir = Directory.CreateDirectory(subPath2);

            // Additional temporary folder with ordinary permissions
            var subPath3 = Path.Combine(testPath, "sub3");
            Directory.CreateDirectory(subPath3);

            const string srcPath = @"TestData\snapshot-test\WMR\";
            const string srcFile1 = "MyBasic.structuredefinition.xml";
            const string srcFile2 = "MyBundle.structuredefinition.xml";

            const string profileUrl1 = @"http://example.org/fhir/StructureDefinition/MyBasic";
            const string profileUrl2 = @"http://example.org/fhir/StructureDefinition/MyBundle";

            // Create test file in inaccessible subfolder; should be ignored
            copy(srcPath, srcFile1, subPath2);

            // Create hidden test file in accessible subfolder; should also be ignored
            copy(srcPath, srcFile1, subPath3);
            var filePath = Path.Combine(subPath3, srcFile1);
            var attr = File.GetAttributes(filePath);
            File.SetAttributes(filePath, attr | FileAttributes.Hidden);

            // Create regular test file in accessible subfolder; should be included
            copy(srcPath, srcFile2, subPath3);
            numFiles++;

            bool initialized = false;
            try
            {
                // Abort unit test if we can't access folder permissions
                var ds = forbiddenDir.GetAccessControl();

                // Revoke folder read permissions for the current user
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                var rule = new ssac.FileSystemAccessRule(userName, ssac.FileSystemRights.Read, ssac.AccessControlType.Deny);
                ds.AddAccessRule(rule);
                Debug.Print($"Removing read permissions from folder: '{subPath2}' ...");

                // Abort unit test if we can't modify file permissions
                forbiddenDir.SetAccessControl(ds);

                try
                {
                    var forbiddenFile = new FileInfo(Path.Combine(subPath2, srcFile1));

                    // Abort unit test if we can't access file permissions
                    var fs = forbiddenFile.GetAccessControl();
                    
                    // Revoke file read permissions for the current user
                    fs.AddAccessRule(rule);
                    Debug.Print($"Removing read permissions from fole: '{forbiddenFile}' ...");
                    
                    // Abort unit test if we can't modify file permissions
                    forbiddenFile.SetAccessControl(fs);

                    initialized = true;

                    try
                    {
                        // Note: we still have write permissions...

                        var dirSource = new DirectorySource(testPath, new DirectorySourceSettings() { IncludeSubDirectories = true });

                        // [WMR 20170823] Test ListArtifactNames => prepareFiles()
                        var names = dirSource.ListArtifactNames();

                        Assert.AreEqual(numFiles, names.Count());
                        Assert.IsFalse(names.Contains(srcFile1));
                        Assert.IsTrue(names.Contains(srcFile2));

                        // [WMR 20170823] Also test ListResourceUris => prepareResources()
                        var profileUrls = dirSource.ListResourceUris(ResourceType.StructureDefinition);
                        
                        // Materialize the sequence
                        var urlList = profileUrls.ToList();
                        Assert.IsFalse(urlList.Contains(profileUrl1));
                        Assert.IsTrue(urlList.Contains(profileUrl2));
                    }
                    // API *should* grafecully handle security exceptions
                    catch (UnauthorizedAccessException ex)
                    {
                        Assert.Fail($"Failed! Unexpected UnauthorizedAccessException: {ex.Message}");
                    }
                    finally
                    {
                        var result = fs.RemoveAccessRule(rule);
                        Assert.IsTrue(result);
                        Debug.Print($"Restoring file read permissions...");
                        forbiddenFile.SetAccessControl(fs);
                        Debug.Print($"Succesfully restored file read permissions.");

                        // We should be able to delete the file
                        File.Delete(forbiddenFile.FullName);
                    }
                }
                finally
                {
                    var result = ds.RemoveAccessRule(rule);
                    Assert.IsTrue(result);
                    Debug.Print($"Restoring folder read permissions...");
                    forbiddenDir.SetAccessControl(ds);
                    Debug.Print($"Succesfully restored folder read permissions.");

                    // We should be able to delete the subdirectory
                    Directory.Delete(subPath2, true);
                }

            }
            // If acl initialization failed, then consume the exception and return success
            // Preferably, skip this unit test / return unknown result - how?
            catch (Exception ex) when (!initialized)
            {
                Debug.Print($"[{nameof(TestAccessPermissions)}] Could not modify directory access permissions: '{ex.Message}'. Skip unit test...");
            }
        }

   }
}