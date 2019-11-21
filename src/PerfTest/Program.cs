using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using FhirModel = Hl7.Fhir.Model;
using FhirModel2 = Hl7.Fhir.Model.DSTU2;
using FhirSerialization = Hl7.Fhir.Serialization;

namespace PerfTest
{
    class Program
    {
        static void Main(string[] args)
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

            var jsonStreamingSerializer = new FhirSerialization.FhirJsonStreamingSerializer(FhirModel.Version.DSTU2);
            jsonStreamingSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                jsonStreamingSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("JSON new serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            var xmlSerializer = new FhirSerialization.FhirXmlSerializer(FhirModel.Version.DSTU2);
            xmlSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                xmlSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("XML serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            var xmlStreamingSerializer = new FhirSerialization.FhirXmlStreamingSerializer(FhirModel.Version.DSTU2);
            xmlStreamingSerializer.SerializeToString(bundle);
            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                xmlStreamingSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("XML serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);
        }
    }

}
