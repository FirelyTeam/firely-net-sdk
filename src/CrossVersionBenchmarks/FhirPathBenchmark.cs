using BenchmarkDotNet.Attributes;
using Firely.Sdk.Benchmarks.Configuration;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;

namespace Firely.Sdk.Benchmarks;

[CrossVersionConfiguration(typeof(FhirPathBenchmark))]
[PackageVersion("5.8.2-20240513.1", Baseline = true)]
[PackageVersion("5.8.2-20240521.2")]
[PackageVersion("5.8.2-20240514.3")]
[PackageVersion("5.12.0")]
[PackageVersion("6.0.0-beta1")]
[ProjectReference]
public class FhirPathBenchmark
{
    private readonly FhirJsonDeserializer _jsonDeserializer;
    private readonly Patient _patient;
    private readonly Bundle _bundle;

    // Cache for FHIRPath expressions
    private readonly FhirPathCompiler _compiler;
    private readonly FhirPathCompilerCache _compilerCache;
    #if SDK6
    private readonly PocoNode _patientElement;
    #else
    private readonly ITypedElement _patientElement;
    #endif
    
    public FhirPathBenchmark()
    {
        _jsonDeserializer = new();
        _patient = _jsonDeserializer.Deserialize<Patient>(TestData.TestData.GetPatientJson());
        _bundle = _jsonDeserializer.Deserialize<Bundle>(TestData.TestData.GetLargePatientBundle());
#if SDK6
 #pragma warning disable SDK0001
        _patientElement = _patient.ToPocoNode(ModelInfo.ModelInspector);
 #pragma warning restore SDK0001
#else
        _patientElement = _patient.ToTypedElement();
#endif

        // Initialize FHIRPath compiler
        _compiler = new FhirPathCompiler();
        _compilerCache = new FhirPathCompilerCache(_compiler);
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
        return expression.Invoke(_patientElement, new EvaluationContext());
    }

    [Benchmark]
    public object EvaluateWithPreCompiledExpressionCached()
    {
        // Using pre-compiled expression which is how the SDK caches FHIRPath expressions
        var expression = _compilerCache.GetCompiledExpression("name.where(use = 'official').given");
        return expression.Invoke(_patientElement, new EvaluationContext());
    }
}