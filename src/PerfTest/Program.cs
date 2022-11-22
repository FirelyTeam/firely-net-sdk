using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using FhirModel = Hl7.Fhir.Model;
using FhirModel2 = Hl7.Fhir.Model.DSTU2;
using FhirModel4 = Hl7.Fhir.Model.R4;
using FhirSerialization = Hl7.Fhir.Serialization;

namespace PerfTest
{
    class Program
    {
        static void Main()
        {
            ParseJson();
        }

        static void ParseJson()
        {
            var json = File.ReadAllText(@"bundle.json");

            const int count = 100;

            JsonSerializer.Deserialize<FhirModel4.Bundle>(json, FhirSerialization.JsonSerializerOptionsExtensions.ForFhir(new JsonSerializerOptions(), FhirModel.Version.R4));
            var initialMemory = GC.GetAllocatedBytesForCurrentThread();
            var watch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                JsonSerializer.Deserialize<FhirModel4.Bundle>(json, FhirSerialization.JsonSerializerOptionsExtensions.ForFhir(new JsonSerializerOptions(), FhirModel.Version.R4));
            }
            watch.Stop();
            var memoryUsed = GC.GetAllocatedBytesForCurrentThread() - initialMemory; 
            Console.WriteLine("JSON fast parse X {1:N0}: {0:N1}ms, {2:N0} bytes", watch.ElapsedMilliseconds, count, memoryUsed);

            var jsonParser = new FhirSerialization.FhirJsonParser(FhirModel.Version.R4);
            jsonParser.Parse<FhirModel4.Bundle>(json);
            initialMemory = GC.GetAllocatedBytesForCurrentThread();
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                jsonParser.Parse<FhirModel4.Bundle>(json);
            }
            watch.Stop();
            memoryUsed = GC.GetAllocatedBytesForCurrentThread() - initialMemory;
            Console.WriteLine("JSON parse X {1:N0}: {0:N1}ms, {2:N0} bytes", watch.ElapsedMilliseconds, count, memoryUsed);
        }

        static void Serialize()
        {
            var xml = File.ReadAllText(@"bundle.xml");

            var xmlParser = new FhirSerialization.FhirXmlParser(FhirModel.Version.DSTU2);
            var bundle = xmlParser.Parse<FhirModel2.Bundle>(xml);

            const int count = 10_000;

            var watch = Stopwatch.StartNew();

            var jsonSerializer = new FhirSerialization.FhirJsonSerializer(FhirModel.Version.DSTU2);
            jsonSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                jsonSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("JSON serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            var jsonFastSerializer = new FhirSerialization.FhirJsonFastSerializer(FhirModel.Version.DSTU2);
            jsonFastSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                jsonFastSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("JSON fast serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            var xmlSerializer = new FhirSerialization.FhirXmlSerializer(FhirModel.Version.DSTU2);
            xmlSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                xmlSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("XML serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            var xmlFastSerializer = new FhirSerialization.FhirXmlFastSerializer(FhirModel.Version.DSTU2);
            xmlFastSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                xmlFastSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("XML fast serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);
        }
    }

}
