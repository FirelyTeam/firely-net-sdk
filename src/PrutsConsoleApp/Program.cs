﻿using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.IO;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace PrutsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var prg = new Program();
            //prg.resourceFromFhirTurtleByMichael();
            //prg.resourceFromFhirTurtleByGrahame();
            //prg.inspectorTest();
            prg.fhirTestWriteTurtle();
            //prg.resourceFromFhirTurtleTest();
        }

        private void inspectorTest()
        {
            ModelInspector insp = new ModelInspector();
            insp.Import(typeof(Resource).Assembly);

            ClassMapping clsMap = insp.FindClassMappingForResource("Patient");

            PropertyMapping prtMap = clsMap.FindMappedElementByName("name");

            Console.WriteLine(prtMap.ElementType);
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void resourceFromFhirTurtleTest()
        {
            SerializationConfig.AcceptUnknownMembers = false;
            string turtle = File.ReadAllText(@"C:\Users\zelm\AppData\Local\Temp\FHIRRoundTripTest\FromXml\intermediate1\address.profile.ttl");
            var resource = FhirParser.ParseResourceFromTurtle(turtle);

            var xml = FhirSerializer.SerializeToXml(resource);
            File.WriteAllText(@"c:\temp\output-michael.xml", xml);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void resourceFromFhirTurtleByMichael()
        {
            SerializationConfig.AcceptUnknownMembers = true;
            string turtle = File.ReadAllText(@"C:\ownCloud\__HL7\FHIR RDF W3C (2016-jan)\WIP\obs-ex-bp-v2(fhir-net-api)-adjusted.ttl");
            var resource = FhirParser.ParseResourceFromTurtle(turtle);

            var xml = FhirSerializer.SerializeToXml(resource);
            File.WriteAllText(@"c:\temp\output-michael.xml", xml);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void resourceFromFhirTurtleByGrahame()
        {
            SerializationConfig.AcceptUnknownMembers = true;
            string turtle = File.ReadAllText(@"C:\VisualStudio Projects\fhir-net-api\src\Hl7.Fhir.Core.Tests\TestData\observation-example-bloodpressure+types.ttl");
            var resource = FhirParser.ParseResourceFromTurtle(turtle);

            var xml = FhirSerializer.SerializeToXml(resource);
            File.WriteAllText(@"c:\temp\output-grahame.xml", xml);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void findRdfTypeInTurtle()
        {
            IGraph g = new Graph();
            g.NamespaceMap.AddNamespace("fhir", UriFactory.Create("http://hl7.org/fhir/"));
            g.NamespaceMap.AddNamespace("sct", UriFactory.Create("http://snomed.info/sct/"));
            g.NamespaceMap.AddNamespace("loinc", UriFactory.Create("http://loinc.org/"));

            TurtleParser turtleParse = new TurtleParser();
            turtleParse.Load(g, @"C:\VisualStudio Projects\fhir-net-api\src\Hl7.Fhir.Core.Tests\TestData\observation-example-bloodpressure.ttl");

            IUriNode pred = g.CreateUriNode("rdf:type");

            foreach (Triple t in g.Triples)
            {
                if (pred.Equals(t.Predicate))
                {
                    Console.WriteLine(t.ToString());
                }
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void fhirTestPatient()
        {
            var pat = new Patient();
            pat.Name.Add(HumanName.ForFamily("Kramer").WithGiven("Ewout"));

            Console.WriteLine("---- json ----");
            Console.WriteLine(FhirSerializer.SerializeResourceToJson(pat));

            Console.WriteLine("---- turtle ----");
            Console.WriteLine(FhirSerializer.SerializeToTurtle(pat));
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void fhirTestWriteTurtle()
        {
            Console.WriteLine("---- read XML resource and write as turtle to file ----");
            var resource = FhirParser.ParseFromXml(System.IO.File.ReadAllText(@"C:\VisualStudio Projects\fhir-net-api\src\Hl7.Fhir.Core.Tests\TestData\observation-example-bloodpressure.xml"));
            var turtle = FhirSerializer.SerializeToTurtle(resource);
            File.WriteAllText(@"c:\temp\output2.ttl", turtle);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void readTurtleWriteN3()
        {
            IGraph g = new Graph();

            TurtleParser turtleParse = new TurtleParser();
            turtleParse.Load(g, @"C:\VisualStudio Projects\fhir-net-api\src\Hl7.Fhir.Core.Tests\TestData\observation-example-bloodpressure.ttl");

/*            foreach (Triple t in g.Triples)
            {
                Console.WriteLine(t.ToString());
            }
            Console.ReadLine();*/

            NTriplesWriter n3Writer = new NTriplesWriter();
            n3Writer.Save(g, @"c:\temp\output.n3");
        }
    }
}
