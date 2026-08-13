using BenchmarkDotNet.Attributes;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.IO;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Firely.Sdk.Benchmarks
{
    /// <summary>
    /// Measures the cost - and above all the allocation - that the model validator adds to a parse.
    /// </summary>
    /// <remarks>
    /// <para>The validator runs on every property and every object of a parse, and is on in every
    /// deserialization mode but <c>SyntaxOnly</c>/<c>Ostrich</c>, so it is on the hot path of every consumer
    /// of the SDK. Each pair of benchmarks below parses the same data with the validator on and off: the
    /// difference between the two is what validation costs, and comparing that difference between two
    /// versions of the SDK is how a change to the validation path is judged.</para>
    /// <para>Two payloads, since the shape of the data determines the mix: a small, flat CodeSystem and a
    /// StructureDefinition with a snapshot (deeply nested, thousands of repeating elements). Both are taken
    /// from the specification.zip that ships with the SDK, so they match the model exactly and neither
    /// variant pays for reporting errors - which is checked in the setup below.</para>
    /// </remarks>
    [MemoryDiagnoser]
    public class ValidatingDeserializationBenchmarks
    {
        internal string SmallResourceJson;
        internal string LargeResourceJson;

        internal FhirJsonDeserializer Validating;
        internal FhirJsonDeserializer NotValidating;

        [GlobalSetup]
        public async Task BenchmarkSetup()
        {
            // The mode firely-car (and anything wanting a guaranteed overflow-free POCO) parses with:
            // model validation on, recoverable issues ignored.
            Validating = new FhirJsonDeserializer(new DeserializerSettings().UsingMode(DeserializationMode.NoOverflow));

            // The same, but with model validation switched off - the reference point for what
            // validation costs.
            NotValidating = new FhirJsonDeserializer(
                new DeserializerSettings().UsingMode(DeserializationMode.NoOverflow) with { Validator = null });

            var source = ZipSource.CreateValidationSource();
            var serializer = new FhirJsonSerializer();

            SmallResourceJson = serializer.SerializeToString(
                await resolve("http://hl7.org/fhir/administrative-gender"));
            LargeResourceJson = serializer.SerializeToString(
                await resolve("http://hl7.org/fhir/StructureDefinition/Patient"));

            verifyParsesCleanly(SmallResourceJson);
            verifyParsesCleanly(LargeResourceJson);

            Console.WriteLine($"Payloads: small {SmallResourceJson.Length} chars, " +
                              $"large {LargeResourceJson.Length} chars.");
            return;

            async System.Threading.Tasks.Task<Resource> resolve(string canonical) =>
                await source.ResolveByCanonicalUriAsync(canonical)
                ?? throw new InvalidDataException($"Cannot find '{canonical}' in the specification.zip.");
        }

        [Benchmark(Baseline = true)]
        public Resource SmallResourceValidating() => parse(Validating, SmallResourceJson);

        [Benchmark]
        public Resource SmallResourceNotValidating() => parse(NotValidating, SmallResourceJson);

        [Benchmark]
        public Resource LargeResourceValidating() => parse(Validating, LargeResourceJson);

        [Benchmark]
        public Resource LargeResourceNotValidating() => parse(NotValidating, LargeResourceJson);

        private static Resource parse(FhirJsonDeserializer deserializer, string json)
        {
            var reader = SerializationUtil.Utf8JsonReaderFromJsonText(json);
            deserializer.TryDeserializeResource(ref reader, out var instance, out _);
            return instance;
        }

        /// <summary>
        /// A payload that produces errors would have the validating variant pay for constructing them,
        /// which is not what this benchmark is trying to measure - so refuse to run on such data.
        /// </summary>
        private void verifyParsesCleanly(string json)
        {
            var reader = SerializationUtil.Utf8JsonReaderFromJsonText(json);

            if (!Validating.TryDeserializeResource(ref reader, out _, out var issues))
                throw new InvalidDataException("Benchmark data does not parse cleanly: " +
                                               string.Join(", ", issues.Select(i => i.Message)));
        }
    }
}
