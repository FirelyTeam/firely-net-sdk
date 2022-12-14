/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Linq;
using T = System.Threading.Tasks;
using Hl7.Fhir.Rest;
using System.Net.Http;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ResolverTests
    {
        const int DefaultTimeOut = 15 * 1000; // 15 seconds

        [ClassInitialize]
        public static void SetupSource(TestContext _)
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

            var dirSource = new DirectorySource(Path.Combine("TestData", "validation"));
            extDefn = dirSource.ResolveByCanonicalUri("http://example.com/StructureDefinition/patient-telecom-reslice-ek|1.0");

            Assert.ThrowsException<ArgumentException>(() => dirSource.ResolveByCanonicalUri("http://example.com/StructureDefinition/patient-telecom-reslice-ek|1.0|"));
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

            // namingsystems have disappeared in STU3?
            //extDefn = source.ResolveByUri("http://hl7.org/fhir/NamingSystem/tx-rxnorm");
            //Assert.IsNotNull(extDefn);
            //Assert.IsInstanceOfType(extDefn, typeof(NamingSystem));
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public void RetrieveWebArtifact()
        {
            var wa = new WebResolver() { TimeOut = DefaultTimeOut };

            var artifact = wa.ResolveByUri("http://test.fhir.org/r3/StructureDefinition/Observation");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Observation", ((StructureDefinition)artifact).Name);

            // var ci = artifact.Annotation<OriginAnnotation>();
            // Assert.AreEqual("http://test.fhir.org/r3/StructureDefinition/Observation", ci.Origin);
            Assert.AreEqual("http://test.fhir.org/r3/StructureDefinition/Observation", artifact.GetOrigin());
        }

        private class TestFhirClient : FhirClient
        {
            private int _status = 0;

            public int Status
            {
                get { return _status; }
                set { _status = value; }
            }

            public TestFhirClient(string endpoint, FhirClientSettings settings = null, HttpMessageHandler handler = null) : base(endpoint, settings, handler) { Status = 1; }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void RetrieveWebArtifactCustomFhirClient()
        {
            using (var handler = new HttpClientEventHandler())
            {
                using (var client = new TestFhirClient("http://test.fhir.org", handler: handler))
                {
                    client.Settings.Timeout = 10;

                    handler.OnBeforeRequest += (sender, e) =>
                    {
                        client.Status = 2;
                    };

                    handler.OnAfterResponse += (sender, e) =>
                    {
                        client.Status = 3;
                    };

                    var wa = new WebResolver(id => client);                   

                    var artifact = wa.ResolveByUri("http://vonk.fire.ly/StructureDefinition/Patient");

                    Assert.IsNotNull(client);
                    Assert.AreEqual(client.Status, 3);

                    Assert.IsNotNull(artifact);
                    Assert.IsTrue(artifact is StructureDefinition);
                    Assert.AreEqual("Patient", ((StructureDefinition)artifact).Name);
                }
            }          
        }

        [TestMethod,TestCategory("IntegrationTest")]
        public async T.Task RetrieveArtifactMulti()
        {
            var resolver = new MultiResolver(source, new WebResolver() { TimeOut = DefaultTimeOut });

            var vs = await resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var artifact = await resolver.ResolveByUriAsync("http://test.fhir.org/r3/StructureDefinition/Patient");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Patient", ((StructureDefinition)artifact).Name);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public async T.Task TestSourceCaching()
        {
            var src = new CachedResolver(
                new MultiResolver(
                    ZipSource.CreateValidationSource(),
                    new WebResolver() { TimeOut = DefaultTimeOut }));

            Stopwatch sw1 = new Stopwatch();

            // Ensure looking up a failed endpoint repeatedly does not cost much time
            sw1.Start();
            await src.ResolveByUriAsync("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            await src.ResolveByUriAsync("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw2.Stop();

            Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
            Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

            // Now try an existing artifact
            sw1.Restart();
            await src.ResolveByUriAsync("http://hl7.org/fhir/ValueSet/v2-0292");
            sw1.Stop();

            sw2.Restart();
            await src.ResolveByUriAsync("http://hl7.org/fhir/ValueSet/v2-0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);
        }

        [TestMethod]
        public async T.Task TestCacheInvalidation()
        {
            var src = new CachedResolver(new MultiResolver(ZipSource.CreateValidationSource()));
            CachedResolver.LoadResourceEventArgs eventArgs = null;
            src.Load += (sender, args) => { eventArgs = args; };

            // Verify that the Load event is fired on the initial load
            const string resourceUri = "http://hl7.org/fhir/ValueSet/v2-0292";
            var resource = await src.ResolveByUriAsync(resourceUri);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(resourceUri, eventArgs.Url);
            Assert.AreEqual(resource, eventArgs.Resource);

            // Verify that the Load event is not fired on subsequent load
            eventArgs = null;
            resource = await src.ResolveByUriAsync(resourceUri);
            Assert.IsNull(eventArgs);

            // Verify that we can remove the cache entry
            var result = src.InvalidateByUri(resourceUri);
            Assert.IsTrue(result);

            // Verify that the cache entry has been removed
            result = src.InvalidateByUri(resourceUri);
            Assert.IsFalse(result);

            // Verify that the Load event is fired again on the next load
            var resource2 = await src.ResolveByUriAsync(resourceUri);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(resourceUri, eventArgs.Url);
            Assert.AreEqual(resource2, eventArgs.Resource);
            
            // Verify that the cache returned a new instance with exact same value
            Assert.AreNotEqual(resource2.GetHashCode(), resource.GetHashCode());
            Assert.IsTrue(resource.IsExactly(resource2));
        }

        [TestMethod]
        public async T.Task TestCacheLoadingStrategy()
        {
            const string resourceUri = "http://hl7.org/fhir/ValueSet/v2-0292";

            // Create empty in-memory resolver
            var mem = new InMemoryProfileResolver();
            var cache = new CachedResolver(mem);
            
            // Load on demand should return null
            var resource = await cache.ResolveByCanonicalUriAsync(resourceUri);
            Assert.IsNull(resource);

            // Resolve core resource from ZIP and refresh in-memory resolver
            var zipSource = ZipSource.CreateValidationSource();
            var original = zipSource.ResolveByUri(resourceUri) as ValueSet;
            Assert.IsNotNull(original);
            mem.Reload(original);

            // Load on demand should still return null
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri);
            Assert.IsNull(resource);

            // Invalidate the cache, delete existing cache entry
            cache.InvalidateByCanonicalUri(resourceUri);

            // Load from cache should still return null
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri, CachedResolverLoadingStrategy.LoadFromCache);
            Assert.IsNull(resource);

            // Load on demand should now resolve instance and update cache
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri);
            Assert.IsNotNull(resource);
            Assert.AreEqual(original, resource);
            Assert.IsTrue(cache.IsCachedCanonicalUri(resourceUri));

            // Update in-memory resolver with new, modified instance (same url)
            var modified = (ValueSet)original.DeepCopy();
            modified.Name = "MODIFIED";
            mem.Reload(modified);

            // Load on demand should still return the original, unmodified instance from cache
            // As the cache is unaware that the internal source has changed
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri);
            Assert.IsNotNull(resource);
            Assert.AreEqual(original, resource);

            // Forced load should update cache and return new, modified instance
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri, CachedResolverLoadingStrategy.LoadFromSource);
            Assert.IsNotNull(resource);
            Assert.AreEqual(modified, resource);

            // Clear in-memory resolver; i.e. simulate delete file from disk
            mem.Clear();

            // Load on demand should still return the modified instance from cache
            // As the cache is unaware that the internal source has changed
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri);
            Assert.IsNotNull(resource);
            Assert.AreEqual(modified, resource);
            Assert.IsTrue(cache.IsCachedCanonicalUri(resourceUri));

            // Forced load should update cache and now return null
            resource = await cache.ResolveByCanonicalUriAsync(resourceUri, CachedResolverLoadingStrategy.LoadFromSource);
            Assert.IsNull(resource);
            Assert.IsFalse(cache.IsCachedCanonicalUri(resourceUri));
        }

        [TestMethod]
        public void TestSetupIsOnce()
        {
            var fa = ZipSource.CreateValidationSource();

            var sw = new Stopwatch();
            sw.Start();
            fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw.Stop();

            var sw2 = new Stopwatch();
            sw2.Start();
            fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw.ElapsedMilliseconds);
            Debug.WriteLine(String.Format("First time {0}, second time {1}", sw.ElapsedMilliseconds, sw2.ElapsedMilliseconds));
        }


        // [WMR 20160823] NEW - Verify FileDirectoryArtifactSource & ResolvingConflictException
        [TestMethod]
        public async T.Task TestCanonicalUrlConflicts()
        {
            //const string srcFileName = "extension-definitions.xml";
            const string dupFileName = "diagnosticorder-reason-duplicate";
            const string url = "http://hl7.org/fhir/StructureDefinition/procedurerequest-reasonRejected";

            var za = ZipSource.CreateValidationSource();

            // Try to find a core extension
            var ext = za.ResolveByCanonicalUri(url);
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Save back to disk to create a conflicting duplicate
            var b = new Bundle();
            b.AddResourceEntry(ext, url);
            var xml = await new FhirXmlSerializer().SerializeToStringAsync(b);
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
            catch (ResolvingConflictException ex)
            {
                Debug.WriteLine("{0}:\r\n{1}", ex.GetType().Name, ex.Message);
                Assert.IsNotNull(ex.Conflicts);
                Assert.AreEqual(1, ex.Conflicts.Length);
                var conflict = ex.Conflicts[0];
                Assert.AreEqual(url, conflict.Identifier);
                Assert.IsTrue(conflict.Origins.Contains(filePath));
                Assert.IsTrue(conflict.Origins.Contains(filePath2));
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
}
