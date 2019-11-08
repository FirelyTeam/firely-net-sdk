using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
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

            const int count = 1_000;

            var watch = Stopwatch.StartNew();
            for (var i=0; i<count; i++)
            {
                var jsonSerializer = new FhirSerialization.FhirJsonSerializer(FhirModel.Version.DSTU2);
                jsonSerializer.SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("Serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);

            watch.Restart();
            for (var i = 0; i < count; i++)
            {
                SerializeToString(bundle);
            }
            watch.Stop();
            Console.WriteLine("New serialize X {1:N0}: {0:N1}ms", watch.ElapsedMilliseconds, count);
        }

        private static string SerializeToString(FhirModel2.Bundle bundle)
        {
            var writer = new StringWriter();
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                bundle.Serialize(jsonWriter);
            }
            writer.Flush();
            return writer.ToString();
        }
    }

}
