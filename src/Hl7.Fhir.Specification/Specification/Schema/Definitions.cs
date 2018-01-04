using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Definitions : Assertion
    {
        public readonly ElementSchema[] Schemas;

        public Definitions(params ElementSchema[] schemas) : this(schemas.AsEnumerable())
        {

        }

        public Definitions(IEnumerable<ElementSchema> schemas)
        {
            Schemas = schemas.ToArray();
        }

        public override IEnumerable<Assertions> Collect() => Assertions.Empty.Collection;

        public override JToken ToJson() =>
            new JProperty("definitions", new JArray(
                Schemas.Select(s => s.ToJson())));
    }
}
