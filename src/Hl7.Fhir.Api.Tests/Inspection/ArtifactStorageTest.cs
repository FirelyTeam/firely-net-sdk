/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Api.Introspection;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
#if PORTABLE45
	public class PortableArtifactStorageTest
#else
    public class ArtifactStorageTest
#endif
    {
        [TestMethod]
        public void ZipCacherShouldCache()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "validation.zip");

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


        [TestMethod]
        public void RecreatingFileArtifact()
        {
            var fa = new FileArtifactSource();

            fa.Prepare(); // First time might be expensive...

            Stopwatch sw = new Stopwatch();

            for (var loop = 0; loop < 50; loop++)
            {
                fa = new FileArtifactSource();
                fa.Prepare();
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 20*50);
        }

        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var fa = new FileArtifactSource();

            // Add a "user" file before preparing;
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "userfile.txt"), @"Hello, world");

            fa.Prepare();

            using (var a = fa.ReadContentArtifact("patient.sch"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = fa.ReadContentArtifact("core-valuesets-v3.xml"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = fa.ReadContentArtifact("patient.xsd"))
            {
                Assert.IsNotNull(a);
            }

            using (var a = fa.ReadContentArtifact("userfile.txt"))
            {
                Assert.IsNotNull(a);
            }
        }


        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = new FileArtifactSource();

            var vs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/v2/vs/0292"));
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/profile/condition"));
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is Profile);

            var dt = fa.ReadResourceArtifact(new Uri("http://hl7.org/fhir/profile/money"));
            Assert.IsNotNull(rs);
            Assert.IsTrue(dt is Profile);
        }

        [TestMethod]
        public void RetrieveWebArtifact()
        {
            var wa = new WebArtifactSource();

            var artifact = wa.ReadResourceArtifact(new Uri("http://fhir.healthintersections.com.au/open/Profile/alert"));

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is Profile);
            Assert.AreEqual("alert", ((Profile) artifact).Name);
        }

        [TestMethod]
        public void RetrieveArtifactMulti()
        {
            var resolver = new MultiArtifactSource(new FileArtifactSource(), new WebArtifactSource());

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
    }
}