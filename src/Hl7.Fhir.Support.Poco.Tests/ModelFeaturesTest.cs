using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            found!.Value.Should().Be("A");
            t.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();

            IIdentifiable<List<Identifier>> tl = new SomethingIdentifiable();
            tl.TryGetIdentifier("http://sysA", out found).Should().BeTrue();
            found!.Value.Should().Be("A");  // finds only first
            tl.TryGetIdentifier("http://sysB", out found).Should().BeTrue();
            found!.Value.Should().Be("B");  // finds only first
            t.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();

            IIdentifiable<string> ts = new SomethingIdentifiable();
            ts.TryGetIdentifier("http://sysA", out var _).Should().BeFalse();

            IIdentifiable ti = new SomethingIdentifiable();
            ti.TryGetIdentifier("http://sysA", out found).Should().BeTrue();
            found!.Value.Should().Be("A");  // finds only first
            ti.TryGetIdentifier("http://sysB", out found).Should().BeTrue();
            found!.Value.Should().Be("B");  // finds only first
            ti.TryGetIdentifier("http://sysY", out var _).Should().BeFalse();
        }

        [TestMethod]
        public void BuildCodingFromResources()
        {
            ICoded mr1 = new ResourceWithChoiceReference(new FhirBoolean(false));
            mr1.ToCodings().Should().BeEmpty();

            var cd = new Coding("http://nu.nl", "bla");
            ICoded mr2 = new ResourceWithChoiceReference(cd);
            mr2.ToCodings().Should().BeEquivalentTo([cd]);
        }

        private class ResourceWithChoiceReference(DataType medication) : ICoded<DataType>
        {
            DataType ICoded<DataType>.Code { get => medication; set => throw new NotImplementedException(); }
            IReadOnlyCollection<Coding> ICoded.ToCodings() => medication.ToCodings();
        }

        [TestMethod]
        public void BuildCodingList()
        {
            ((DataType)null).ToCodings().Should().BeEmpty();

            new Code("bla").ToCodings().IsExactly(l(c(null, "bla"))).Should().BeTrue();
            new Coding("http://nu.nl", "bla").ToCodings().IsExactly(l(c("http://nu.nl", "bla"))).Should().BeTrue();
            new Code<AdministrativeGender>(AdministrativeGender.Male).ToCodings()
                .IsExactly(l(new Coding("http://hl7.org/fhir/administrative-gender", "male"))).Should().BeTrue();
            new CodeableConcept().Add("http://nu.nl", "bla1").Add("http://nu.nl", "bla2").ToCodings()
                .IsExactly([c("http://nu.nl", "bla1"), c("http://nu.nl", "bla2")]).Should().BeTrue();
            new FhirString("bla").ToCodings().IsExactly(l(c(null, "bla"))).Should().BeTrue();
            new CodeableReference(new CodeableConcept().Add("http://nu.nl", "bla1")).ToCodings()
                .IsExactly([c("http://nu.nl", "bla1")]).Should().BeTrue();

            var list = new[]
            {
                new Code<AdministrativeGender>(AdministrativeGender.Male),
                new Code<AdministrativeGender>(AdministrativeGender.Other)
            };

            list.ToCodings().IsExactly([c("http://hl7.org/fhir/administrative-gender", "male"), c("http://hl7.org/fhir/administrative-gender", "other")
            ]).Should().BeTrue();

            var listcc = new[]
            {
                new CodeableConcept().Add("http://nu.nl", "bla1").Add("http://nu.nl", "bla2"),
                new CodeableConcept().Add("http://nu.nl", "bla3").Add("http://nu.nl", "bla4"),
            };

            listcc.ToCodings().IsExactly([c("http://nu.nl", "bla1"), c("http://nu.nl", "bla2"), c("http://nu.nl", "bla3"), c("http://nu.nl", "bla4")
            ]).Should().BeTrue();

            static Coding c(string s, string v) => new(s, v);
            static IReadOnlyCollection<Coding> l(Coding c) => [c];
        }
    }


    public class SomethingIdentifiable : IIdentifiable<Identifier>, IIdentifiable<List<Identifier>>, IIdentifiable<string>
    {
        private List<Identifier> _identifiers =
        [
            new("http://sysA", "A"),
            new("http://sysA", "A1"),
            new("http://sysA", "A2"),
            new("http://sysB", "B")
        ];

        Identifier IIdentifiable<Identifier>.Identifier { get => _identifiers[0]; set => _identifiers = [value]; }
        List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => _identifiers; set => _identifiers = value; }
        string IIdentifiable<string>.Identifier { get => _identifiers[0].Value; set => _identifiers =
            [new Identifier("http://sysX", value)]; }
    }

}