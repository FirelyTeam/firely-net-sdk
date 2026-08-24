using FluentAssertions;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        // exact matches
        [DataRow("value", "value", true)]
        [DataRow("value", "valueString", false)]
        [DataRow("valueString", "value", false)]
        // wildcard prefixes, as used for value[x] elements
        [DataRow("valueString", "value*", true)]
        [DataRow("valueQuantity", "value*", true)]
        [DataRow("value", "value*", true)]
        [DataRow("other", "value*", false)]
        [DataRow("anything", "*", true)]
        // an empty prefix is not a wildcard: it only matches empty text
        [DataRow("anything", "", false)]
        [DataRow("", "", true)]
        public void MatchesPrefixComparesTextToPrefix(string text, string prefix, bool expected) =>
            text.MatchesPrefix(prefix).Should().Be(expected);

        [TestMethod]
        public void MatchesPrefixTreatsNullPrefixAsNoFilter()
        {
            // Callers such as DomNode.Children(string name = null) rely on a null prefix
            // meaning "no filter", so every text must match.
            "anything".MatchesPrefix(null).Should().BeTrue();
            "".MatchesPrefix(null).Should().BeTrue();
        }

        [TestMethod]
        [DataRow("value", "value", true)]
        [DataRow("valueString", "value", false)]
        [DataRow("valueString", "value*", true)]
        [DataRow("anything", "", false)]
        [DataRow("", "", true)]
        public void MatchesPrefixOnSpansBehavesLikeTheStringOverload(string text, string prefix, bool expected) =>
            text.AsSpan().MatchesPrefix(prefix.AsSpan()).Should().Be(expected);
    }
}
