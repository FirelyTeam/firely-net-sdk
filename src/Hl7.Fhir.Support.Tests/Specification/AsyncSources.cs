using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class AsyncSourcesTests
    {
        [TestMethod]
        public async Task TestAdaptedSyncSource()
        {
            var adaptee = new SyncResolver();
            var adapted = adaptee.AsAsync();

            _ = await adapted.ResolveByUriAsync("");
            _ = await adapted.ResolveByCanonicalUriAsync("");
            _ = await adapted.ResolveByCanonicalUriAsync("");

            Assert.AreEqual(2, adaptee.ByCanonical);
            Assert.AreEqual(1, adaptee.ByUri);

            // Now call the async adapted sync resolver synchronously ;-)
            TaskHelper.Await(() => adapted.ResolveByUriAsync(""));
            Assert.AreEqual(2, adaptee.ByUri);
        }

        [TestMethod]
        public async Task TestAsyncSyncMultiResolver()
        {
            var (sr, ar, sar) = (new SyncResolver(), new AsyncResolver(), new SyncAsyncResolver());
            var multi = new MultiResolver(sr,ar,sar);

            // calling *any* kind of resolve will involve all child resolvers, since they all return null.
            _ = await multi.ResolveByUriAsync("");
#pragma warning disable CS0618 // Type or member is obsolete
            _ = multi.ResolveByCanonicalUri("");
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.AreEqual(1, sr.ByUri);
            Assert.AreEqual(1, sr.ByCanonical);

            Assert.AreEqual(1, ar.ByUriAsync);
            Assert.AreEqual(1, ar.ByCanonicalAsync);

            Assert.AreEqual(0, sar.ByUri);  // multi will always prefer the async
            Assert.AreEqual(0, sar.ByCanonical); // multi will always prefer the async
            Assert.AreEqual(1, sar.ByUriAsync);
            Assert.AreEqual(1, sar.ByCanonicalAsync);
        }


        [TestMethod]
        public async Task TestCacheOnSyncAsync()
        {
            var sync = new SyncResolver(new AResource { Data = 1 });
            var c1 = new CachedResolver(sync);
            await test(c1);
            Assert.AreEqual(1, sync.ByUri);
            Assert.AreEqual(1, sync.ByCanonical);

            var async = new AsyncResolver(new AResource { Data = 1 });
            var c2 = new CachedResolver(async);
            await test(c2);
            Assert.AreEqual(1, async.ByUriAsync);
            Assert.AreEqual(1, async.ByCanonicalAsync);

            var sasync = new SyncAsyncResolver(new AResource { Data = 1 });
            var c3 = new CachedResolver(sasync);
            await test(c3);
            Assert.AreEqual(0, sasync.ByUri); // cache will always prefer the async
            Assert.AreEqual(0, sasync.ByCanonical); // cache will always prefer the async
            Assert.AreEqual(1, sasync.ByUriAsync);
            Assert.AreEqual(1, sasync.ByCanonicalAsync);

            async Task test(CachedResolver r)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                var result = r.ResolveByUri("t") as AResource;
#pragma warning restore CS0618 // Type or member is obsolete
                Assert.AreEqual(1, result.Data);

                result = await r.ResolveByCanonicalUriAsync("t") as AResource;
                Assert.AreEqual(1, result.Data);
            }
        }
      
    }


    internal class SyncResolver : IResourceResolver
    {
        public int ByCanonical;
        public int ByUri;
        public Resource Data;

        public SyncResolver() { }

        public SyncResolver(Resource data)
        {
            Data = data;
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            ByCanonical += 1;
            return Data;
        }

        public Resource ResolveByUri(string uri)
        {
            ByUri += 1;
            return Data;
        }
    }

    internal class AsyncResolver : IAsyncResourceResolver
    {
        public int ByCanonicalAsync;
        public int ByUriAsync;
        public Resource Data;

        public AsyncResolver() { }

        public AsyncResolver(Resource data)
        {
            Data = data;
        }


        public Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            ByCanonicalAsync += 1;
            return Task.FromResult(Data);
        }

        public Task<Resource> ResolveByUriAsync(string uri)
        {
            ByUriAsync += 1;
            return Task.FromResult(Data);
        }
    }


    internal class SyncAsyncResolver : IResourceResolver, IAsyncResourceResolver
    {
        public int ByCanonical;
        public int ByUri;
        public int ByCanonicalAsync;
        public int ByUriAsync;

        public Resource Data;

        public SyncAsyncResolver() { }

        public SyncAsyncResolver(Resource data)
        {
            Data = data;
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            ByCanonical += 1;
            return Data;
        }

        public Resource ResolveByUri(string uri)
        {
            ByUri += 1;
            return Data;
        }

        public Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            ByCanonicalAsync += 1;
            return Task.FromResult(Data);
        }

        public Task<Resource> ResolveByUriAsync(string uri)
        {
            ByUriAsync += 1;
            return Task.FromResult(Data);
        }
    }

    internal class AResource : Resource
    {
        public int Data;

        public override IDeepCopyable DeepCopy() => throw new NotImplementedException();
    }
}