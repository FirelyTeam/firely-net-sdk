using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using T = System.Threading.Tasks;

namespace Vonk.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathExtensionsTests
    {
        private static Dictionary<string, ValueSet> Resources;
        private static TestContext Context;

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            Context = ctx;
            var specSource = ZipSource.CreateValidationSource();

            Resources = specSource.FindAll<ValueSet>().ToDictionary<ValueSet, string>(sd => sd.Url);
            //By putting all the url's in a dictionary we can be sure there are no duplicates. 

        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async T.Task TestSelectMethods()
        {
            await MassiveParallelSelectsShouldBeCorrect("Api", new Func<ITypedElement, string, EvaluationContext, IEnumerable<ITypedElement>>((nav, expr, context) => IValueProviderFPExtensions.Select(nav, expr, context)));
            await MassiveParallelSelectsShouldBeCorrect("Concurrent", new Func<ITypedElement, string, EvaluationContext, IEnumerable<ITypedElement>>((nav, expr, context) => FhirPathExtensions.Select(nav, expr, context)));
        }

        /// <summary>
        /// This test prepares a dictionary of all DataElement resources, with their (canonical) DataElement.Url (read from the POCO) as key.
        /// This way we are certain that all canonical url's are present and unique.
        /// It then uses FhirPath Select with two different implementations of the cache to extract the url property, using max. 100 threads in parallel.
        /// It turns out that the extracted urls (with Select) are not always equal to the Url property of the POCO.
        /// This may indicate a multithreading problem in the FhirPath evaluation.
        /// You may need to run the test in Release mode to reveal the error.
        /// </summary>
        public static async T.Task MassiveParallelSelectsShouldBeCorrect(string testName, Func<ITypedElement, string, EvaluationContext, IEnumerable<ITypedElement>> selector)
        {
            var actual = new ConcurrentBag<(string canonical, ValueSet resource)>();
            var buffer = new BufferBlock<ValueSet>();
            var processor = new ActionBlock<ValueSet>(r =>
                {
                    var typedElement = r.ToTypedElement();
                    var evalContext = new EvaluationContext(typedElement);
                    var canonical = selector(typedElement, "url", evalContext).Single().Value.ToString();
                    actual.Add((canonical, r));
                }
                ,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });
            buffer.LinkTo(processor, new DataflowLinkOptions { PropagateCompletion = true });
            var resources = Resources;

            var sw = new Stopwatch();
            sw.Restart();
            foreach (var resource in resources.Values)
            {
                buffer.Post(resource);
            }
            buffer.Complete();
            await processor.Completion;
            sw.Stop();
            Context.WriteLine($"Extracting urls took {sw.Elapsed.ToString("c")} ms");

            Assert.AreEqual(actual.Count(), resources.Count(), $"{testName}: All Resources should have a url.");
            Assert.IsFalse(actual
                .Select(sd => new { sd.canonical, sd.resource.Url, sd.resource.Id })
                .Where(check => check.canonical != check.Url)
                .Any(), $"{testName}: Extracted urls should be equal to url property values.");
        }

    }

    internal class CachedDictionary<K, V>
    {
        private ConcurrentDictionary<K, V> _cached = new ConcurrentDictionary<K, V>();

        public Func<K, V> Retrieve { get; }

        public CachedDictionary(Func<K, V> retrieveFunction)
        {
            Retrieve = retrieveFunction;
        }

        public V GetValue(K key)
        {
            if (!_cached.TryGetValue(key, out var result))
            {
                result = Retrieve(key);
                _cached.TryAdd(key, result);
            }
            return result;
        }
    }

    /// <summary>
    /// Alternative implementation of the cache of compiled expressions. 
    /// Because this is more parallelized, it reveals the parallel errors more clearly.
    /// </summary>
    internal static class FhirPathExtensions
    {
        private static CachedDictionary<string, CompiledExpression> _cache = new CachedDictionary<string, CompiledExpression>(expr => Compile(expr));

        private static CompiledExpression Compile(string expression)
        {
            var compiler = new FhirPathCompiler();
            return compiler.Compile(expression);
        }

        private static CompiledExpression GetCompiledExpression(string expression)
        {
            return _cache.GetValue(expression);
        }


        public static IEnumerable<ITypedElement> Select(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = GetCompiledExpression(expression);
            return evaluator(input, ctx ?? EvaluationContext.CreateDefault());
        }

    }
}

