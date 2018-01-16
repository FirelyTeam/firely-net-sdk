using Hl7.Fhir.Specification.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class TestSerialization
    {
        [TestMethod]
        public void SerializeSchema()
        {
            var sub = new ElementSchema("#sub", new Trace("In a subschema"));

            var main = new ElementSchema("http://root.nl/schema1",
                new Definitions(sub),
                new Fail(),
                new ElementSchema("#nested", new Undecided()),
                new ReferenceAssertion(sub),
                new SliceAssertion(false,
                    @default: new Trace("this is the default"),
                    new SliceAssertion.Slice("und", new Undecided(), new Trace("I really don't know")),
                    new SliceAssertion.Slice("fail", new Fail(), new Trace("This always fails"))
                    ),
                new Children(
                    ("child1", new ElementSchema(new Fail())),
                    ("child2", new Trace("in child 2")))
                );

            var result = main.ToJson().ToString();
        }
    }
}
