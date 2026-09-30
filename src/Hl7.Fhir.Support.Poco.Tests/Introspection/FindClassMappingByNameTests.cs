using FluentAssertions;
using Hl7.Fhir.Introspection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Poco.Tests
{
    /// <summary>
    /// The span overload of <see cref="ModelInspector.FindClassMapping(ReadOnlySpan{char})"/> has a separate,
    /// allocation-free implementation on .NET 10+. These tests check it against the string overload.
    /// </summary>
    [TestClass]
    public class FindClassMappingByNameTests
    {
        private static readonly ModelInspector Inspector = ModelInspector.Base;

        [TestMethod]
        public void SpanLookupMatchesStringLookupForEveryMapping()
        {
            Inspector.ClassMappings.Should().NotBeEmpty();

            foreach (var mapping in Inspector.ClassMappings)
            {
                Inspector.FindClassMapping(mapping.Name.AsSpan())
                    .Should().BeSameAs(Inspector.FindClassMapping(mapping.Name), $"lookup of '{mapping.Name}' should match");
            }
        }

        [TestMethod]
        [DataRow("Extension")]
        [DataRow("extension")]
        [DataRow("EXTENSION")]
        public void SpanLookupIsCaseInsensitive(string name)
        {
            var expected = Inspector.FindClassMapping("Extension");
            expected.Should().NotBeNull();

            Inspector.FindClassMapping(name.AsSpan()).Should().BeSameAs(expected);
        }

        [TestMethod]
        public void SpanLookupWorksOnASliceOfALongerString()
        {
            // The typical allocation-free use: looking up the type name part of a path without substringing it.
            var path = "Extension.url";
            var name = path.AsSpan(0, path.IndexOf('.'));

            Inspector.FindClassMapping(name).Should().BeSameAs(Inspector.FindClassMapping("Extension"));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("NoSuchType")]
        [DataRow("Extension.url")]
        public void SpanLookupReturnsNullForUnknownNames(string name)
        {
            Inspector.FindClassMapping(name.AsSpan()).Should().BeNull();
        }
    }
}
