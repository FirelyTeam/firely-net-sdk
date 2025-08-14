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

#if SDK6
    private FhirXmlDeserializer _xmlDeserializer;
    private FhirJsonDeserializer _jsonDeserializer;
#else
    private FhirJsonPocoDeserializer _jsonDeserializer;
    private FhirXmlPocoDeserializer _xmlDeserializer;
#endif
    [GlobalSetup]
    public void BenchmarkSetup()
    {
        _payloadJson = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes(File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"))));
        _payloadXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

#if SDK6
        var options = new DeserializerSettings() { Validator = null };
        _jsonDeserializer = new FhirJsonDeserializer(options);
        _xmlDeserializer = new FhirXmlDeserializer(options);
#else
        var xmlOpt = new FhirXmlPocoDeserializerSettings() { Validator = null };
        _xmlDeserializer = new FhirXmlPocoDeserializer(xmlOpt);
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