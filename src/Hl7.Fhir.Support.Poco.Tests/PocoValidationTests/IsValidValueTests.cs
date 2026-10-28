using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests
{

    [TestClass]
    public class IsValidValueTests
    {
        [TestMethod]
        public void TestValidPrimitives()
        {
            f(Base64Binary.IsValidValue("hi!"));
            t(Base64Binary.IsValidValue("Y29ycmVjdCE="));

            t(Code.IsValidValue("bla"));
            t(Code.IsValidValue("bla bla"));
            f(Code.IsValidValue("bla  bla"));
            f(Code.IsValidValue(" bla "));

            t(Date.IsValidValue("2020-02-03"));
            f(Date.IsValidValue("2020-02-03+03:00"));

            t(FhirDateTime.IsValidValue("2020"));
            f(FhirDateTime.IsValidValue("2020+01:00"));
            t(FhirDateTime.IsValidValue("2020-01-01"));
            f(FhirDateTime.IsValidValue("2020-01-01+01:00"));
            f(FhirDateTime.IsValidValue("2020-02-31"));
            t(FhirDateTime.IsValidValue("2020-01-01T14:00:00+01:00"));
            t(FhirDateTime.IsValidValue("2020-01-01T14:00:00.123Z"));
            f(FhirDateTime.IsValidValue("2020-01-01T14:00:00"));
            f(FhirDateTime.IsValidValue("2020-01-01T14:00+01:00"));
            f(FhirDateTime.IsValidValue("2020-01-01T14+01:00"));
            f(FhirDateTime.IsValidValue("2020-01-01T14"));

            // FhirDecimal's test are exactly covered by System's Decimal.TryParse
            // I don't know how to make FhirUrl.IsValidValue fail, new Uri() accepts everything.

            t(Id.IsValidValue("."));
            t(Id.IsValidValue("45A"));
            t(Id.IsValidValue("45A-4.2"));
            f(Id.IsValidValue("longerthan64longerthan64longerthan64longerthan64longerthan64longerthan64"));
            f(Id.IsValidValue("Weird!"));

            t(Instant.IsValidValue("2020-02-03T12:34:34.5Z"));
            t(Instant.IsValidValue("2020-02-03T12:34:34Z"));
            f(Instant.IsValidValue("2020-02-03T12:34:34"));
            f(Instant.IsValidValue("2020-02-03T12:34"));

            // Integer's test are exactly covered by System's Integer.TryParse
            // Integer64's test are exactly covered by System's Integer.TryParse

            t(Time.IsValidValue("12:03:04"));
            t(Time.IsValidValue("12:03:04.5"));
            f(Time.IsValidValue("12:03:04-01:00"));
            f(Time.IsValidValue("12:03"));
            f(Time.IsValidValue("12"));
        }

        [TestMethod]
        public void TestTrailingLineBreakIsRejected()
        {
            // "$" matches before a final "\n" in .NET, so these used to be accepted (see #3659)
            t(Code.IsValidValue("abc"));
            f(Code.IsValidValue("abc\n"));

            t(Date.IsValidValue("2024-01-01"));
            f(Date.IsValidValue("2024-01-01\n"));

            t(FhirDateTime.IsValidValue("2024-01-01T10:00:00Z"));
            f(FhirDateTime.IsValidValue("2024-01-01T10:00:00Z\n"));

            t(Id.IsValidValue("abc"));
            f(Id.IsValidValue("abc\n"));

            t(Instant.IsValidValue("2024-01-01T10:00:00Z"));
            f(Instant.IsValidValue("2024-01-01T10:00:00Z\n"));

            t(Oid.IsValidValue("urn:oid:1.2.3"));
            f(Oid.IsValidValue("urn:oid:1.2.3\n"));

            t(Time.IsValidValue("12:00:00"));
            f(Time.IsValidValue("12:00:00\n"));

            t(Uuid.IsValidValue("urn:uuid:157a6966-2b36-44cc-95a5-667e964e3cbc"));
            f(Uuid.IsValidValue("urn:uuid:157a6966-2b36-44cc-95a5-667e964e3cbc\n"));
        }

        private static void t(bool b) => b.Should().BeTrue();
        private static void f(bool b) => b.Should().BeFalse();

        [TestMethod]
        public void TestValidXhtmlXhml()
        {
            t(XHtml.IsValidXml("<hi>hi!</hi>", out _));
            f(XHtml.IsValidXml("<hi>hi!<hi>", out _));
            f(XHtml.IsValidXml("hi!", out _));
            f(XHtml.IsValidNarrativeXhtml("hi!", out _, out _));
            f(XHtml.IsValidNarrativeXhtml("<hi>hi!</hi>", out _, out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _, out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _, out _));
            f(XHtml.IsValidNarrativeXhtml("hi!", out _, out _));
            f(XHtml.IsValidNarrativeXhtml("<hi>hi!</hi>", out _, out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _, out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _, out _));
        }

        public static IEnumerable<object[]> ValidUris => cases(
            "local", "#local", "http://nu.nl#local", "mailto:ewout@fire.ly",
            "Patient/034AB16", "http://hl7.org/fhir/ValueSet/my-valueset|0.8",
            "urx:orx:oerf", "urn:nouri:3")
            .Concat(ValidOids).Concat(ValidUuids);
        public static IEnumerable<object[]> InvalidUris => cases("urn:oid:4.4.5", "urn:uuid:1-2-3-4");
        public static IEnumerable<object[]> ValidOids => cases("urn:oid:1.0", "urn:oid:1.23.4");
        public static IEnumerable<object[]> InvalidOids => cases("urn:oid:1", "urn:oid:4.4.5", "urn:oid:1.00", "urn:oid:", "something", "4.4.5");
        public static IEnumerable<object[]> ValidUuids => cases("urn:uuid:157a6966-2b36-44cc-95a5-667e964e3cbc");
        public static IEnumerable<object[]> InvalidUuids => cases("15706966-2636-44cc-95a5-667e964e3cbc", "something", "urn:uuid:wrong",
            "urn:uuid:157a6966-2b36-44cc-95a5-667e964e3cbc4", "urn:uuid:157z6966-2b36-44cc-95a5-667e964e3cbc", "urn:uuid:157a-2b36-44cc-95a5-667e964e3cbc");

        private static IEnumerable<object[]> cases(params string[] tests) =>
            tests.Select(t => new object[] { t });

        [TestMethod]
        [DynamicData(nameof(ValidOids))]
        public void TestValidOids(string test) => Oid.IsValidValue(test).Should().BeTrue();

        [TestMethod]
        [DynamicData(nameof(InvalidOids))]
        public void TestInvalidOids(string test) => Oid.IsValidValue(test).Should().BeFalse();

        [TestMethod]
        [DynamicData(nameof(ValidUuids))]
        public void TestValidUuids(string test) => Uuid.IsValidValue(test).Should().BeTrue();

        [TestMethod]
        [DynamicData(nameof(InvalidOids))]
        public void TestInvalidUuids(string test) => Uuid.IsValidValue(test).Should().BeFalse();

        [TestMethod]
        [DynamicData(nameof(ValidUris))]
        public void TestValidUri(string test) => FhirUri.IsValidValue(test).Should().BeTrue();

        [TestMethod]
        [DynamicData(nameof(InvalidUris))]
        public void TestInvalidUri(string test) => FhirUri.IsValidValue(test).Should().BeFalse();

        [TestMethod]
        [DynamicData(nameof(ValidUris))]
        public void TestValidCanonical(string test) => Canonical.IsValidValue(test).Should().BeTrue();

        [TestMethod]
        [DynamicData(nameof(InvalidUris))]
        public void TestInvalidCanonical(string test) => Canonical.IsValidValue(test).Should().BeFalse();
    }

}