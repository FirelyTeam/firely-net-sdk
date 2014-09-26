/* 
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
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
#if PORTABLE45
	public class PortableArtifactStorageTest
#else
    public class ArtifactStorageTest
#endif
    {
#if !PORTABLE45
        [TestMethod]
        public void ZipCacherShouldCache()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(),"validation.zip");

            var fa = new ZipCacher(zipFile,cacheKey);

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

            fa = new ZipCacher(zipFile,cacheKey);

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

        [TestMethod]
        public void RecreatingCoreZipArtifact()
        {
            var fa = new CoreZipArtifactSource();

            fa.Prepare(); // First time might be expensive...

            Stopwatch sw = new Stopwatch();

            for (var loop = 0; loop < 50; loop++)
            {
                fa = new CoreZipArtifactSource();
                fa.Prepare();
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 20 * 50);
        }

        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var za = new CoreZipArtifactSource();
            za.Prepare();

            using (var a = za.ReadContentArtifact("patient.sch"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.ReadContentArtifact("core-valuesets-v3.xml"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.ReadContentArtifact("patient.xsd"))
            {
                Assert.IsNotNull(a);
            }
        }


        [TestMethod]
        public void GetFileArtifacts()
        {
            var testPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testPath);

            // Add a "user" file before preparing;
            File.WriteAllText(Path.Combine(testPath,"userfile.txt"), @"Hello, world");
            File.WriteAllText(Path.Combine(testPath, "userfile.jrg"), @"Hello, world");

            // Copy a resource from the zip to a subdirectory
            var resPath = Path.Combine(testPath, "resources");
            Directory.CreateDirectory(resPath);

            Patient p = new Patient();
            File.WriteAllText(Path.Combine(resPath,"patientNL.xml"), FhirSerializer.SerializeResourceToXml(p));

            var fa = new FileArtifactSource(testPath, includeSubdirectories:true);
            fa.Mask = "*.txt | *.xml";
            fa.Prepare();

            // Check whether the .jrg file was indeed exluded
            Assert.AreEqual(2, fa.ArtifactFiles.Count());
            
            using (var a = fa.ReadContentArtifact("userfile.txt"))
            {
                Assert.IsNotNull(a);
            }

            var pat = fa.ReadResourceArtifact(new Uri("http://nu.nl/fhir/patientNL"));
            Assert.IsNotNull(pat);

            // Check that, without a Mask, all files are found
            var fa2 = new FileArtifactSource(testPath, includeSubdirectories: true);
            Assert.AreEqual(3, fa2.ArtifactFiles.Count());

            // Check that, when exluding subdirectories, we will only find 2 files again
            var fa3 = new FileArtifactSource(testPath, includeSubdirectories: false);
            Assert.AreEqual(2, fa3.ArtifactFiles.Count());
            Assert.IsTrue(fa3.ArtifactFiles.All(f => f.Contains("userfile.")));
        }

        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = new CoreZipArtifactSource();

            var vs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/v2/vs/0292"));
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/vs/location-status"));
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/Profile/Condition"));
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is Profile);

            rs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/Profile/ValueSet"));
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is Profile);

            var dt = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/Profile/Money"));
            Assert.IsNotNull(rs);
            Assert.IsTrue(dt is Profile);
        }

        [TestMethod]
        public void RetrieveWebArtifact()
        {
            var wa = new WebArtifactSource();

            var artifact = wa.ReadResourceArtifact(new Uri("http://fhir.healthintersections.com.au/open/Profile/Alert"));

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is Profile);
            Assert.AreEqual("alert", ((Profile) artifact).Name);
        }

        [TestMethod]
        public void RetrieveArtifactMulti()
        {
            var resolver = ArtifactResolver.CreateDefault();

            resolver.Prepare();

            var vs = resolver.ReadResourceArtifact(new Uri("http://hl7.org/fhir/v2/vs/0292"));
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            using (var a = resolver.ReadContentArtifact("patient.sch"))
            {

                Assert.IsNotNull(a);
            }

            var artifact = resolver.ReadResourceArtifact(new Uri("http://fhir.healthintersections.com.au/open/Profile/alert"));

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is Profile);
            Assert.AreEqual("alert", ((Profile)artifact).Name);
        }

        [TestMethod]
        public void TestSourceCaching()
        {
            var src = new CachedArtifactSource(ArtifactResolver.CreateDefault());

            src.Prepare();

            Stopwatch sw1 = new Stopwatch();

            // Ensure looking up a failed endpoint repeatedly does not cost much time
            sw1.Start();
            src.ReadResourceArtifact(new Uri("http://some.none.existant.address.nl"));
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            src.ReadResourceArtifact(new Uri("http://some.none.existant.address.nl"));
            sw2.Stop();

            Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
            Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

            // Now try an existing artifact
            sw1.Restart();
            src.ReadResourceArtifact(new Uri("http://hl7.org/fhir/v2/vs/0292"));
            sw1.Stop();

            sw2.Restart();
            src.ReadResourceArtifact(new Uri("http://hl7.org/fhir/v2/vs/0292"));
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

       }
    }
}