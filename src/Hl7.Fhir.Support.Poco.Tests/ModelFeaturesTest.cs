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
    }


    public class SomethingIdentifiable : IIdentifiable<Identifier>, IIdentifiable<List<Identifier>>, IIdentifiable<string>
    {
        private List<Identifier> identifiers = new()
        {
            new("http://sysA", "A"),
            new("http://sysA", "A1"),
            new("http://sysA", "A2"),
            new("http://sysB", "B"),
        };

        Identifier IIdentifiable<Identifier>.Identifier { get => identifiers[0]; set => identifiers = new() { value }; }
        List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => identifiers; set => identifiers = value; }
        string IIdentifiable<string>.Identifier { get => identifiers[0].Value; set => identifiers = new() { new("http://sysX", value) }; }
    }
}
