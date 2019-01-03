using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]
    public class CacheTests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private readonly Cache<string, string> _cache;

        public CacheTests()
        {
            _cache = new Cache<string, string>(expr => expr.ToUpper(), new CacheSettings() { MaxCacheSize = 500 });
        }

        [TestMethod]
        public void AddingItems()
        {
            Cache<string, string> _cache = new Cache<string, string>(expr => expr.ToUpper(), new CacheSettings() { MaxCacheSize = 10 });

            for (int i = 0; i < 20; i++)
            {
                var value = _cache.GetValue($"item{i}");

                Assert.AreEqual($"ITEM{i}", value);
            }

            var result = _cache.GetValue("fhir");
            Assert.AreEqual("FHIR", result);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void AddingItemsParallel()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var actionBlock = new ActionBlock<string>(
                 input =>
                 {
                     _cache.GetValue(input);
                 }, 
                 new ExecutionDataflowBlockOptions()
                 {
                     MaxDegreeOfParallelism = 20
                 });


            for (int i = 0; i < 20_000; i++)
            {
                actionBlock.Post($"item{i}");
            }
            actionBlock.Complete();
            actionBlock.Completion.Wait();

            stopwatch.Stop();
            TestContext.WriteLine($"Using cache with 20.000 items takes {stopwatch.Elapsed.TotalMilliseconds}ms");
        }
    }
}
