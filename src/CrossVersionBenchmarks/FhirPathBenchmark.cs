using BenchmarkDotNet.Attributes;
using Firely.Sdk.Benchmarks.Configuration;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace Firely.Sdk.Benchmarks;

[CrossVersionConfiguration(Baseline: "5.8.2-20240513.1", Versions: 
    [
        // "5.8.2-20240521.2",
        // "5.8.2-20240514.3",
        // "5.8.2-20240513.1",
        "5.12.0",
    ], AddProjectReference: true)]
public class FhirPathBenchmark
{
    private readonly FhirJsonParser _jsonParser;
    private Patient _patient;
    private Bundle _bundle;

    // Cache for FHIRPath expressions
    private FhirPathCompiler _compiler;
    private readonly ITypedElement _patientElement;

    public FhirPathBenchmark()
    {
        _jsonParser = new FhirJsonParser();
        _patient = _jsonParser.Parse<Patient>(TestData.TestData.GetPatientJson());
        _bundle = _jsonParser.Parse<Bundle>(TestData.TestData.GetLargePatientBundle());
#pragma warning disable SDK0001
        _patientElement = _patient.ToTypedElement();
#pragma warning restore SDK0001

        // Initialize FHIRPath compiler
        _compiler = new FhirPathCompiler();
    }
    
    [Benchmark]
    public object EvaluateSimplePatientExpression()
    {
        // A simple expression to get the patient's name
        return _patient.Select("name.given");
    }
    
    [Benchmark]
    public object EvaluateComplexPatientExpression()
    {
        // A more complex expression that filters telecom entries
        return _patient.Select("telecom.where(system = 'phone' and use = 'mobile')");
    }
    
    [Benchmark]
    public object EvaluateBundleExpression()
    {
        // Query to extract all patient names from the bundle
        return _bundle.Select("entry.resource.ofType(Patient).name.given");
    }

    [Benchmark]
    public object EvaluateWithPreCompiledExpression()
    {
        // Using pre-compiled expression which is how the SDK caches FHIRPath expressions
        var expression = _compiler.Compile("name.where(use = 'official').given");
        #if SDK6
        return expression.Invoke(_patientElement.ToPocoNode(), new EvaluationContext());
        #else
        return expression.Invoke(_patientElement, new EvaluationContext());
        #endif
    }
}