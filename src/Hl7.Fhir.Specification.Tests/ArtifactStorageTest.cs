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

namespace Hl7.Fhir.Specification.Tests
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

        private void copy(string dir, string file, string outputDir)
        {
            File.Copy(Path.Combine(dir, file), Path.Combine(outputDir, file));
        }

        private string prepareExampleDirectory()
        {
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "validation.zip");
            var zip = new ZipCacher(zipFile);
            var zipPath = zip.GetContentDirectory();

            var testPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testPath);

            copy(zipPath, "extension-definitions.xml", testPath);
            copy(zipPath, "alert.xsd", testPath);
            copy(zipPath, "patient.sch", testPath);
            copy(@"TestData", "TestPatient.xml", testPath);
            copy(@"TestData", "TestValueSet.xml", testPath);

            return testPath;
        }


        [TestMethod]
        public void UseFileArtifactSource()
        {
            var testPath = prepareExampleDirectory();
            var fa = new FileArtifactSource(testPath);
            fa.Mask = "*.xml|*.xsd";
            var names = fa.ListArtifactNames();

            Assert.AreEqual(4, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("alert.xsd"));
            Assert.IsFalse(names.Contains("patient.sch"));

            using (var stream = fa.ReadContentArtifact("TestPatient.xml"))
            {
                var pat = (new FhirParser()).ParseResource(FhirParser.XmlReaderFromStream(stream));
                Assert.IsNotNull(pat);
            }

            var vs = fa.ReadConformanceResource("urn:uuid:256a5231-a2bb-49bd-9fea-f349d428b70d") as ValueSet;
            Assert.IsNotNull(vs);

            var ed = fa.ReadConformanceResource("http://hl7.org/fhir/ExtensionDefinition/openEHR-location") as ExtensionDefinition;
            Assert.IsNotNull(ed);
        }


        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var za = new CoreZipArtifactSource();

            using (var a = za.ReadContentArtifact("patient.sch"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.ReadContentArtifact("v3-codesystems.xml"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = za.ReadContentArtifact("patient.xsd"))
            {
                Assert.IsNotNull(a);
            }
        }


        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = new CoreZipArtifactSource();

            var vs = fa.ReadConformanceResource("http://hl7.org/fhir/v2/vs/0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.ReadConformanceResource("http://hl7.org/fhir/vs/location-status");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.ReadConformanceResource("http://hl7.org/fhir/Profile/Condition");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is Profile);

            rs = fa.ReadConformanceResource("http://hl7.org/fhir/Profile/ValueSet");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is Profile);

            var dt = fa.ReadConformanceResource("http://hl7.org/fhir/Profile/Money");
            Assert.IsNotNull(rs);
            Assert.IsTrue(dt is Profile);
        }


       // [TestMethod]
       // public void RecreatingCoreZipArtifact()
       // {
       //     var fa = new CoreZipArtifactSource();

       //     fa.Prepare(); // First time might be expensive...

       //     Stopwatch sw = new Stopwatch();

       //     for (var loop = 0; loop < 50; loop++)
       //     {
       //         fa = new CoreZipArtifactSource();
       //         fa.Prepare();
       //     }

       //     sw.Stop();

       //     Assert.IsTrue(sw.ElapsedMilliseconds < 20 * 50);
       // }

     




    

       // //Re-enable when servers support DSTU2
       // [TestMethod,Ignore]
       // public void RetrieveWebArtifact()
       // {
       //     var wa = new WebArtifactSource();

       //     var artifact = wa.ReadConformanceResource(new Uri("http://fhir.healthintersections.com.au/open/Profile/Alert"));

       //     Assert.IsNotNull(artifact);
       //     Assert.IsTrue(artifact is Profile);
       //     Assert.AreEqual("alert", ((Profile) artifact).Name);
       // }

       // [TestMethod]
       // public void RetrieveArtifactMulti()
       // {
       //     var resolver = ArtifactResolver.CreateDefault();

       //     resolver.Prepare();

       //     var vs = resolver.ReadConformanceResource(new Uri("http://hl7.org/fhir/v2/vs/0292"));
       //     Assert.IsNotNull(vs);
       //     Assert.IsTrue(vs is ValueSet);

       //     using (var a = resolver.ReadContentArtifact("patient.sch"))
       //     {

       //         Assert.IsNotNull(a);
       //     }

       //     //TODO: Re-enable when servers support DSTU2

       //     //var artifact = resolver.ReadResourceArtifact(new Uri("http://fhir.healthintersections.com.au/open/Profile/alert"));

       //     //Assert.IsNotNull(artifact);
       //     //Assert.IsTrue(artifact is Profile);
       //     //Assert.AreEqual("alert", ((Profile)artifact).Name);
       // }

       // [TestMethod]
       // public void TestSourceCaching()
       // {
       //     var src = new CachedArtifactSource(ArtifactResolver.CreateDefault());

       //     src.Prepare();

       //     Stopwatch sw1 = new Stopwatch();

       //     // Ensure looking up a failed endpoint repeatedly does not cost much time
       //     sw1.Start();
       //     src.ReadConformanceResource(new Uri("http://some.none.existant.address.nl"));
       //     sw1.Stop();

       //     var sw2 = new Stopwatch();

       //     sw2.Start();
       //     src.ReadConformanceResource(new Uri("http://some.none.existant.address.nl"));
       //     sw2.Stop();

       //     Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
       //     Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

       //     // Now try an existing artifact
       //     sw1.Restart();
       //     src.ReadConformanceResource(new Uri("http://hl7.org/fhir/v2/vs/0292"));
       //     sw1.Stop();

       //     sw2.Restart();
       //     src.ReadConformanceResource(new Uri("http://hl7.org/fhir/v2/vs/0292"));
       //     sw2.Stop();

       //     Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

       //}
    }
}