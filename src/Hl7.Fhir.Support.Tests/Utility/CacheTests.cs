using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
// TODO: Find alternative for .NET 4.0
#if !NET40
using System.Threading.Tasks.Dataflow;
#endif

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

        public CacheTests()
        {
        }

        [TestMethod]
        public void AddItem()
        {
            var called = 0;
            var cache = new Cache<string, string>(factory, new CacheSettings() { MaxCacheSize = 10 });

            var value = cache.GetValue($"item");
            Assert.AreEqual($"ITEM", value);
            Assert.AreEqual(1, called);

            called = 0;

            var secondValue = cache.GetValue($"item");
            Assert.IsTrue(object.ReferenceEquals(value,secondValue));
            Assert.AreEqual(0, called);

            string factory(string input)
            {
                called += 1;
                return input.ToUpper();
            }
        }

        [TestMethod]
        public void AddItemsWithoutRetriever()
        {
            var cache = new Cache<string, string>();

            Assert.IsNull(cache.GetValue("item1"));
            Assert.IsNull(cache.GetValue("item1"));  // and again, no change

            var item = "contents";
            Assert.IsTrue(object.ReferenceEquals(item, cache.GetValueOrAdd("item1", item)));
            Assert.IsTrue(object.ReferenceEquals(item, cache.GetValue("item1")));
        }

        [TestMethod]
        public void AddingItems()
        {
            var called = 0;
            var cache = new Cache<string, string>(factory, new CacheSettings() { MaxCacheSize = 10 });
            var insertedValues = new List<string>();
            
            for (int i = 0; i < 20; i++)
            {
                var value = cache.GetValue($"item{i}");

                Assert.AreEqual($"ITEM{i}", value);
                insertedValues.Add(value);
            }
            Assert.AreEqual(20, called);
            
            // Item 0-9 are expelled from cache

            called = 0;
            for (int i = 19; i >= 0; i--)
            {
                var value = cache.GetValue($"item{i}");

                // Last 10 items should still be there
                if(i >= 10)
                    Assert.IsTrue(object.ReferenceEquals(insertedValues[i], value));
                else
                    Assert.IsFalse(object.ReferenceEquals(insertedValues[i], value));
            }

            Assert.AreEqual(10, called);

            string factory(string input)
            {
                called += 1;
                return input.ToUpper();
            }
        }

#if !NET40
        [TestMethod]
        [TestCategory("LongRunner")]
        public void AddingItemsParallel()
        {
            var cache = new Cache<string, string>(expr => expr.ToUpper(), new CacheSettings() { MaxCacheSize = 500 });
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var actionBlock = new ActionBlock<string>(
                 input =>
                 {
                     cache.GetValue(input);
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
#endif
    }
}
