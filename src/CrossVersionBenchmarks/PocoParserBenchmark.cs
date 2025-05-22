using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using Firely.Sdk.Benchmarks.Configuration;
using Hl7.Fhir.Serialization;
using System.Buffers;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Firely.Sdk.Benchmarks;

[MemoryDiagnoser]
[Config(typeof(Sdk5To6Config))]
[HideColumns(Column.Arguments)]
public class PocoParserBenchmark
{
    private ReadOnlySequence<byte> _payloadJson;
    private string _payloadXml;
    private FhirXmlPocoDeserializer _xmlDeserializer;
#if SDK6_ALPHA3 || SDK6_ALPHA2
    private FhirJsonDeserializer _jsonDeserializer;
#else
    private FhirJsonPocoDeserializer _jsonDeserializer;
#endif
    [GlobalSetup]
    public void BenchmarkSetup()
    {
        _payloadJson = new(Encoding.UTF8.GetBytes(File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"))));
        _payloadXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
        
        var xmlOpt = new FhirXmlPocoDeserializerSettings() { Validator = null };
        _xmlDeserializer = new FhirXmlPocoDeserializer(xmlOpt);
#if SDK6_ALPHA3 || SDK6_ALPHA2
        var options = new DeserializerSettings() { Validator = null };
        _jsonDeserializer = new FhirJsonDeserializer(options);
#else
        var jsonOpt = new FhirJsonPocoDeserializerSettings() { Validator = null };
        _jsonDeserializer = new FhirJsonPocoDeserializer(jsonOpt);
#endif
    }

    [Benchmark]
    public bool DeserializeJson()
    {
        var reader = new Utf8JsonReader(_payloadJson, new JsonReaderOptions() { CommentHandling = JsonCommentHandling.Skip });
        return _jsonDeserializer.TryDeserializeResource(ref reader, out _, out _);
    }

    [Benchmark]
    public bool DeserializeXml()
    {
        return _xmlDeserializer.TryDeserializeResource(XmlReader.Create(new StringReader(_payloadXml)), out _, out _);
    }
}