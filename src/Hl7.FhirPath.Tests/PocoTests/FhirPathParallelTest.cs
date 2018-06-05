using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Xunit;
using Xunit.Abstractions;
using T = System.Threading.Tasks;

namespace Vonk.Core.Tests.Support
{
    public class SpecZipResourcesFixture
    {
        public SpecZipResourcesFixture()
        {
            var specSource = ZipSource.CreateValidationSource();

            Resources = specSource.FindAll<DataElement>().ToDictionary<DataElement, string>(sd => sd.Url);
            //By putting all the url's in a dictionary we can be sure there are no duplicates. 
        }
        public Dictionary<string, DataElement> Resources { get; private set; }
    }

    public class FhirPathExtensionsTests : IClassFixture<SpecZipResourcesFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly SpecZipResourcesFixture _fixture;

        public FhirPathExtensionsTests(ITestOutputHelper outputHelper, SpecZipResourcesFixture fixture)
        {
            _outputHelper = outputHelper;
            _fixture = fixture;
        }

        public static IEnumerable<object[]> GetSelectMethods()
        {
            return new[]
            {
                new object[]{ "Api", new Func<IElementNavigator, string, EvaluationContext, IEnumerable<IElementNavigator>>((nav, expr, context) => IValueProviderFPExtensions.Select(nav, expr, context)) },
                new object[]{ "Concurrent", new Func<IElementNavigator, string, EvaluationContext, IEnumerable<IElementNavigator>>((nav, expr, context) => FhirPathExtensions.Select(nav, expr, context))},
            };
        }

        /// <summary>
        /// This test prepares a dictionary of all DataElement resources, with their (canonical) DataElement.Url (read from the POCO) as key.
        /// This way we are certain that all canonical url's are present and unique.
        /// It then uses FhirPath Select with two different implementations of the cache to extract the url property, using max. 100 threads in parallel.
        /// It turns out that the extracted urls (with Select) are not always equal to the Url property of the POCO.
        /// This may indicate a multithreading problem in the FhirPath evaluation.
        /// You may need to run the test in Release mode to reveal the error.
        /// </summary>
        /// <param name="testDescriptor"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(GetSelectMethods))]
        [Trait("TestCategory", "LongRunner")]
        public async T.Task MassiveParallelSelectsShouldBeCorrect(string testDescriptor, Func<IElementNavigator, string, EvaluationContext, IEnumerable<IElementNavigator>> selector)
        {
            var actual = new ConcurrentBag<(string canonical, DataElement resource)>();
            var buffer = new BufferBlock<DataElement>();
            var processor = new ActionBlock<DataElement>(r =>
                {
                    var pocoNav = new PocoNavigator(r);
                    var evalContext = new EvaluationContext(new PocoNavigator(r));
                    var canonical = selector(new PocoNavigator(r), "url", evalContext).Single().Value.ToString();
                    actual.Add((canonical, r));
                }
                ,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });
            buffer.LinkTo(processor, new DataflowLinkOptions { PropagateCompletion = true });
            var resources = _fixture.Resources;

            var sw = new Stopwatch();
            sw.Restart();
            foreach (var resource in resources.Values)
            {
                buffer.Post(resource);
            }
            buffer.Complete();
            await processor.Completion;
            sw.Stop();
            _outputHelper.WriteLine($"{testDescriptor}: Extracting urls took {sw.Elapsed.ToString("c")} ms");

            actual.Count().Should().Be(resources.Count(), $"{testDescriptor}: All Resources should have a url.");
            actual.Select(sd => new { sd.canonical, sd.resource.Url, sd.resource.Id }).Where(check => check.canonical != check.Url).Should().BeEmpty($"{testDescriptor}: Extracted urls should be equal to url property values.");
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


        public static IEnumerable<IElementNavigator> Select(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = GetCompiledExpression(expression);
            return evaluator(input, ctx ?? EvaluationContext.Default);
        }

    }
}

