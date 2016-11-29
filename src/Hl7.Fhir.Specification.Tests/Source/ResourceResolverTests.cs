﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Specification.Source;
using System.Net;
using Hl7.Fhir.Support;
using System.Xml.Linq;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableResourceResolverTests
#else
    public class ResolverTests
#endif
    {
#if !PORTABLE45
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static IResourceResolver source = null;
        
        [TestMethod]
        public void ResolveByCanonicalFromZip()
        {
            var extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));
        }

        [TestMethod]
        public void ResolveByUriFromZip()
        {
            var extDefn = source.ResolveByUri("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByUri("http://hl7.org/fhir/NamingSystem/tx-rxnorm");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(NamingSystem));
        }


        [TestMethod]
        public void RetrieveWebArtifact()
        {
            var wa = new WebResolver();

            var artifact = wa.ResolveByUri("http://fhir2.healthintersections.com.au/open/StructureDefinition/Observation");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Observation", ((StructureDefinition)artifact).Name);

            var ci = artifact.Annotation<OriginInformation>();
            Assert.AreEqual("http://fhir2.healthintersections.com.au/open/StructureDefinition/Observation", ci.Origin);
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

            var wa = new WebResolver(id => client = new TestFhirClient(id));

            Assert.IsNull(client);

            var artifact = wa.ResolveByUri("http://fhir2.healthintersections.com.au/open/StructureDefinition/Flag");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Status, 3);

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }

        [TestMethod]
        public void RetrieveArtifactMulti()
        {
            var resolver = new MultiResolver(source, new WebResolver());

            var vs = resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var artifact = resolver.ResolveByUri("http://fhir2.healthintersections.com.au/open/StructureDefinition/flag");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }


        [TestMethod]
        public void TestSourceCaching()
        {
            var src = new CachedResolver(new MultiResolver(ZipSource.CreateValidationSource(), new WebResolver()));

            Stopwatch sw1 = new Stopwatch();

            // Ensure looking up a failed endpoint repeatedly does not cost much time
            sw1.Start();
            src.ResolveByUri("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            src.ResolveByUri("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw2.Stop();

            Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
            Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

            // Now try an existing artifact
            sw1.Restart();
            src.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            sw1.Stop();

            sw2.Restart();
            src.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);
        }

        [TestMethod]
        public void TestCacheInvalidation()
        {
            var src = new CachedResolver(new MultiResolver(ZipSource.CreateValidationSource()));
            CachedResolver.LoadResourceEventArgs eventArgs = null;
            CachedResolver.LoadResourceEventHandler handler = (sender, args) => { eventArgs = args; };
            src.Load += handler;

            // Verify that the Load event is fired on the initial load
            const string resourceUri = "http://hl7.org/fhir/ValueSet/v2-0292";
            var resource = src.ResolveByUri(resourceUri);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(resourceUri, eventArgs.Url);
            Assert.AreEqual(resource, eventArgs.Resource);

            // Verify that the Load event is not fired on subsequent load
            eventArgs = null;
            resource = src.ResolveByUri(resourceUri);
            Assert.IsNull(eventArgs);

            // Verify that we can remove the cache entry
            var result = src.InvalidateUri(resourceUri);
            Assert.IsTrue(result);

            // Verify that the cache entry has been removed
            result = src.InvalidateUri(resourceUri);
            Assert.IsFalse(result);

            // Verify that the Load event is fired again on the next load
            var resource2 = src.ResolveByUri(resourceUri);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(resourceUri, eventArgs.Url);
            Assert.AreEqual(resource2, eventArgs.Resource);
            
            // Verify that the cache returned a new instance with exact same value
            Assert.AreNotEqual(resource2.GetHashCode(), resource.GetHashCode());
            Assert.IsTrue(resource.IsExactly(resource2));
        }

        [TestMethod]
        public void TestSetupIsOnce()
        {
            var fa = ZipSource.CreateValidationSource();

            var sw = new Stopwatch();
            sw.Start();
            var vs = fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw.Stop();

            var sw2 = new Stopwatch();
            sw2.Start();
            var vs2 = fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw.ElapsedMilliseconds);
            Debug.WriteLine(String.Format("First time {0}, second time {1}", sw.ElapsedMilliseconds, sw2.ElapsedMilliseconds));
        }


        // [WMR 20160823] NEW - Verify FileDirectoryArtifactSource & CanonicalUrlConflictException
        [TestMethod]
        public void TestCanonicalUrlConflicts()
        {
            //const string srcFileName = "extension-definitions.xml";
            const string dupFileName = "diagnosticorder-reason-duplicate";
            const string url = "http://hl7.org/fhir/StructureDefinition/diagnosticorder-reason";

            var za = ZipSource.CreateValidationSource();

            // Try to find a core extension
            var ext = za.ResolveByCanonicalUri(url);
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Save back to disk to create a conflicting duplicate
            var b = new Bundle();
            b.AddResourceEntry(ext, url);
            var xml = FhirSerializer.SerializeToXml(b);
            var filePath = Path.Combine(DirectorySource.SpecificationDirectory, dupFileName) + ".xml";
            var filePath2 = Path.Combine(DirectorySource.SpecificationDirectory, dupFileName) + "2.xml";
            File.WriteAllText(filePath, xml);
            File.WriteAllText(filePath2, xml);

            bool conflictException = false;
            try
            {
                var fa = new DirectorySource();
                var res = fa.ResolveByCanonicalUri(url);
            }
            catch (CanonicalUrlConflictException ex)
            {
                Debug.Write(string.Format("{0}:\r\n{1}", ex.GetType().Name, ex.Message));
                Assert.IsNotNull(ex.Conflicts);
                Assert.AreEqual(1, ex.Conflicts.Length);
                var conflict = ex.Conflicts[0];
                Assert.AreEqual(url, conflict.Url);
                Assert.IsTrue(conflict.FilePaths.Contains(filePath));
                Assert.IsTrue(conflict.FilePaths.Contains(filePath2));
                conflictException = true;
            }
            finally
            {
                try { File.Delete(filePath); } catch { }
                File.Delete(filePath2);
            }
            Assert.IsTrue(conflictException);
        }

    }
#endif
}