using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class ModelFeaturesTest
    {
        [TestMethod]
        public void FindsSystems()
        {
            IIdentifiable<Identifier> t = new SomethingIdentifiable();
            t.TryGetIdentifier("http://sysA", out var found).Should().BeTrue();
            found.Value.Should().Be("A");
            t.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();

            IIdentifiable<List<Identifier>> tl = new SomethingIdentifiable();
            tl.TryGetIdentifier("http://sysA", out found).Should().BeTrue();
            found.Value.Should().Be("A");  // finds only first
            tl.TryGetIdentifier("http://sysB", out found).Should().BeTrue();
            found.Value.Should().Be("B");  // finds only first
            t.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();

            IIdentifiable<string> ts = new SomethingIdentifiable();
            ts.TryGetIdentifier("http://sysA", out var _).Should().BeFalse();

            IIdentifiable ti = new SomethingIdentifiable();
            ti.TryGetIdentifier("http://sysA", out found).Should().BeTrue();
            found.Value.Should().Be("A");  // finds only first
            ti.TryGetIdentifier("http://sysB", out found).Should().BeTrue();
            found.Value.Should().Be("B");  // finds only first
            ti.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();
        }

        [TestMethod]
        public void BuildCodingList()
        {
            DataType d = null;
            d.ToCodings().Should().BeEmpty();

            new Code("bla").ToCodings().Should().BeEquivalentTo(l(c(null, "bla")));
            new Coding("http://nu.nl", "bla").ToCodings().Should().BeEquivalentTo(l(c("http://nu.nl", "bla")));
            new Code<TestAdministrativeGender>(TestAdministrativeGender.Male).ToCodings().Should().BeEquivalentTo(l(new Coding("http://hl7.org/fhir/administrative-gender", "male")));
            new CodeableConcept().Add("http://nu.nl", "bla1").Add("http://nu.nl", "bla2").ToCodings().Should().BeEquivalentTo(new[] { c("http://nu.nl", "bla1"), c("http://nu.nl", "bla2") });
            new FhirString("bla").ToCodings().Should().BeEquivalentTo(l(c(null, "bla")));

            var list = new[]
            {
                new Code<TestAdministrativeGender>(TestAdministrativeGender.Male),
                new Code<TestAdministrativeGender>(TestAdministrativeGender.Other)
            };

            list.ToCodings().Should().BeEquivalentTo(new[] { c("http://hl7.org/fhir/administrative-gender", "male"), c("http://hl7.org/fhir/administrative-gender", "other") });

            var listcc = new[]
            {
                new CodeableConcept().Add("http://nu.nl", "bla1").Add("http://nu.nl", "bla2"),
                new CodeableConcept().Add("http://nu.nl", "bla3").Add("http://nu.nl", "bla4"),
            };

            listcc.ToCodings().Should().BeEquivalentTo(new[] { c("http://nu.nl", "bla1"), c("http://nu.nl", "bla2"), c("http://nu.nl", "bla3"), c("http://nu.nl", "bla4") });

            static Coding c(string s, string v) => new Coding(s, v);
            static IEnumerable<Coding> l(Coding c) => new[] { c };
        }
    }


    public class SomethingIdentifiable : IIdentifiable<Identifier>, IIdentifiable<List<Identifier>>, IIdentifiable<string>
    {
        private List<Identifier> _identifiers = new()
        {
            new("http://sysA", "A"),
            new("http://sysA", "A1"),
            new("http://sysA", "A2"),
            new("http://sysB", "B"),
        };

        Identifier IIdentifiable<Identifier>.Identifier { get => _identifiers[0]; set => _identifiers = new() { value }; }
        List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => _identifiers; set => _identifiers = value; }
        string IIdentifiable<string>.Identifier { get => _identifiers[0].Value; set => _identifiers = new() { new("http://sysX", value) }; }
    }

}
