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
using System.Net;

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
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(),"validation.xml.zip");

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
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "validation.xml.zip");
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
            var fa = new FileDirectoryArtifactSource(_testPath);
            fa.Mask = "*.xml|*.xsd";
            var names = fa.ListArtifactNames();

            Assert.AreEqual(3, names.Count());
            Assert.IsTrue(names.Contains("extension-definitions.xml"));
            Assert.IsTrue(names.Contains("flag.xsd"));
            Assert.IsFalse(names.Contains("patient.sch"));

            using (var stream = fa.LoadArtifactByName("TestPatient.xml"))
            {
                var pat = FhirParser.ParseResource(FhirParser.XmlReaderFromStream(stream));
                Assert.IsNotNull(pat);
            }

            var vs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/iso21090-preferred") as StructureDefinition;
           
            Assert.IsNotNull(vs);

            var cis = fa.ListConformanceResources();
            foreach (var ci in cis) Debug.WriteLine(ci.ToString());
        }

        [TestMethod]
        public void FileSourceSkipsExecutables()
        {
            var fa = new FileDirectoryArtifactSource(_testPath);
            Assert.IsFalse(fa.ListArtifactNames().Any(name => name.EndsWith(".dll")));
            Assert.IsFalse(fa.ListArtifactNames().Any(name => name.EndsWith(".exe")));
        }

        [TestMethod]
        public void ReadsSubdirectories()
        {
            var testPath = prepareExampleDirectory();
            var fa = new FileDirectoryArtifactSource(testPath, includeSubdirectories:true);
            var names = fa.ListArtifactNames();

            Assert.AreEqual(5,names.Count());
            Assert.IsTrue(names.Contains("TestPatient.json"));
        }

        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var za = ZipArtifactSource.CreateValidationSource();

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
        public void GetSomeArtifactsById()
        {
            var fa = ZipArtifactSource.CreateValidationSource();

            var vs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/administrative-gender");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/location-status");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/Condition");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);

            rs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/ValueSet");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);

            var dt = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/Money");
            Assert.IsNotNull(dt);
            Assert.IsTrue(dt is StructureDefinition);

            // Try to find a core extension
            var ext = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/diagnosticorder-reason");
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Try to find an additional US profile (they are distributed with the spec for now)
            var us = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/StructureDefinition/uslab-dr");
            Assert.IsNotNull(us);
            Assert.IsTrue(us is StructureDefinition);           
        }


        [TestMethod]
        public void ValidateConformanceInformationScanner()
        {
            var fa = ZipArtifactSource.CreateValidationSource();

            var allConformanceInformation = fa.ListConformanceResources();

            var dicomVS = allConformanceInformation.Where(ci => ci.ValueSetSystem == "http://nema.org/dicom/dicm").SingleOrDefault();
            Assert.IsNotNull(dicomVS);
            Assert.AreEqual("http://hl7.org/fhir/ValueSet/dicom-dcim",dicomVS.Url);
            Assert.AreEqual("DICOM Controlled Terminology Definitions",dicomVS.Name);

            var moneySD = allConformanceInformation.Where(ci => ci.Url == "http://hl7.org/fhir/StructureDefinition/Money").SingleOrDefault();
            Assert.IsNotNull(moneySD);
            Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/Money", moneySD.Url);
            Assert.AreEqual("Money", moneySD.Name);

            var reasonEXT = allConformanceInformation.Where(ci => ci.Url == "http://hl7.org/fhir/StructureDefinition/diagnosticorder-reason").SingleOrDefault();
            Assert.IsNotNull(reasonEXT);
            Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/diagnosticorder-reason", reasonEXT.Url);
            Assert.AreEqual("Reason for this order", reasonEXT.Name);
        }


        [TestMethod]
        public void TestSetupIsOnce()
        {
            var fa = ZipArtifactSource.CreateValidationSource();

            var sw = new Stopwatch();
            sw.Start();
            var vs = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/v2/vs/0292");
            sw.Stop();

            var sw2 = new Stopwatch();
            sw2.Start();
            var vs2 = fa.LoadConformanceResourceByUrl("http://hl7.org/fhir/v2/vs/0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw.ElapsedMilliseconds);
            Debug.WriteLine(String.Format("First time {0}, second time {1}", sw.ElapsedMilliseconds, sw2.ElapsedMilliseconds));
        }


        [TestMethod]
        public void RetrieveWebArtifact()
        {
            var wa = new WebArtifactSource();

            var artifact = wa.LoadConformanceResourceByUrl("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/Flag");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }

        private class TestFhirClient : Rest.FhirClient
        {
            private int _status = 0;

            public int Status 
            { 
                get { return _status; }
                private set { _status = value; }
            }

            public TestFhirClient(Uri endpoint) : base(endpoint) { Status = 1; }

            protected override void BeforeRequest(HttpWebRequest rawRequest, byte[] body)
            {
                Status = 2;
                base.BeforeRequest(rawRequest, body);
            }

            protected override void AfterResponse(HttpWebResponse webResponse, byte[] body)
            {
                Status = 3;
                base.AfterResponse(webResponse, body);
            }
        }

        [TestMethod]
        public void RetrieveWebArtifactCustomFhirClient()
        {
            TestFhirClient client = null;

            var wa = new WebArtifactSource(id => client = new TestFhirClient(id));

            Assert.IsNull(client);

            var artifact = wa.LoadConformanceResourceByUrl("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/Flag");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Status, 3);

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }

        [TestMethod]
        public void RetrieveArtifactMulti()
        {
            var resolver = new MultiArtifactSource(ZipArtifactSource.CreateValidationSource(), new WebArtifactSource());

            var vs = resolver.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            using (var a = resolver.LoadArtifactByName("patient.sch"))
            {
                Assert.IsNotNull(a);
            }

            var artifact = resolver.LoadConformanceResourceByUrl("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/flag");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }

        [TestMethod]
        public void TestSourceCaching()
        {
            var src = new CachedArtifactSource(new MultiArtifactSource(ZipArtifactSource.CreateValidationSource(), new WebArtifactSource()));

            Stopwatch sw1 = new Stopwatch();

            // Ensure looking up a failed endpoint repeatedly does not cost much time
            sw1.Start();
            src.LoadConformanceResourceByUrl("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            src.LoadConformanceResourceByUrl("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw2.Stop();

            Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
            Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

            // Now try an existing artifact
            sw1.Restart();
            src.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/v2-0292");
            sw1.Stop();

            sw2.Restart();
            src.LoadConformanceResourceByUrl("http://hl7.org/fhir/ValueSet/v2-0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

        }
   
    }
}