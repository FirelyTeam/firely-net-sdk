using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using Xunit;

namespace Hl7.Fhir.Validation
{
    public partial class ParseExtensionsTests
    {
        [Fact]
        public void TestParseCodeableReference()
        {
            var i = new CodeableReference
            {
                Reference = new ResourceReference("http://example.org/fhir/Patient/1"),
                Concept = new CodeableConcept("http://nu.nl", "bla")
            };
            
            var node = i.ToTypedElement();
            var p = node.ParseBindable();

            p.Should().BeEquivalentTo(new { Coding = new List<Coding> { new Coding("http://nu.nl", "bla") }  });
        }
    }
}
