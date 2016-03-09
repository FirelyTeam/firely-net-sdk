using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //prg.readTurtleWriteN3();
            prg.fhir();
        }

        private void fhir()
        {
            var pat = new Patient();
            pat.Name.Add(HumanName.ForFamily("Kramer").WithGiven("Ewout"));

            Console.WriteLine("---- json ----");
            Console.WriteLine(FhirSerializer.SerializeResourceToJson(pat));

            Console.WriteLine("---- turtle ----");
            Console.WriteLine(FhirSerializer.SerializeToTurtle(pat));

            Console.WriteLine("---- read XML resource and write as turtle to file ----");
            var resource = FhirParser.ParseFromXml(System.IO.File.ReadAllText(@"C:\VisualStudio Projects\fhir-net-api\src\Hl7.Fhir.Core.Tests\TestData\observation-example-bloodpressure.xml"));
            var turtle = FhirSerializer.SerializeToTurtle(resource);
            System.IO.File.WriteAllText(@"c:\temp\output.ttl", turtle);

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
