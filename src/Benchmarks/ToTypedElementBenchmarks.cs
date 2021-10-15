using BenchmarkDotNet.Attributes;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;

namespace Firely.Sdk.Benchmarks
{
    [MemoryDiagnoser]
    public class ToTypedElementBenchmarks
    {
        private readonly IStructureDefinitionSummaryProvider _provider;
        private readonly string _json = "{ \"resourceType\": \"Patient\", \"active\": true, \"contact\": [{\"organization\": {\"reference\": \"Organization/1\", \"display\": \"Walt Disney Corporation\" }, \"period\": { \"start\": \"0001-01-01\", \"end\": \"2018\" } } ],}";

        public ToTypedElementBenchmarks()
        {
            _provider = new StructureDefinitionSummaryProvider(ZipSource.CreateValidationSource());
        }


        [Benchmark]
        public void ToTypedElementOnSourceNode()
        {
            for (int i = 0; i < 10; i++)
            {
                var patient = FhirJsonNode.Parse(_json).ToTypedElement(_provider);

                patient.VisitAll();

                // second time should be faster, when the children and values are cached
                patient.VisitAll();
            }
        }
    }
}
