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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Api.Profiles;
using System.Diagnostics;
using System.IO;

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
        public void FileArtifactPreparationShouldCache()
        {
            var fa = new FileArtifactStore();

            var sw = new Stopwatch();

            sw.Start();
            fa.Prepare();
            sw.Stop();

            var firstRun = sw.ElapsedMilliseconds;

            sw.Restart();
            fa.Prepare();
            sw.Stop();

            var secondRun = sw.ElapsedMilliseconds;

            Assert.IsTrue(firstRun > secondRun);

            fa.Clear();

            sw.Restart();
            fa.Prepare();
            sw.Stop();

            var thirdRun = sw.ElapsedMilliseconds;
            Assert.IsTrue(thirdRun > secondRun);
        }


        [TestMethod]
        public void RecreatingFileArtifact()
        {
            var fa = new FileArtifactStore();

            fa.Prepare(); // First time might be expensive...

            Stopwatch sw = new Stopwatch();

            for (var loop = 0; loop < 50; loop++)
            {
                fa = new FileArtifactStore();
                fa.Prepare();
            }

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 20*50);
        }

        [TestMethod]
        public void GetSomeBundledArtifacts()
        {
            var fa = new FileArtifactStore();

            // Add a "user" file before preparing;
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "userfile.txt"), "Hello, world");

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
            var fa = new FileArtifactStore();

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
    }
}