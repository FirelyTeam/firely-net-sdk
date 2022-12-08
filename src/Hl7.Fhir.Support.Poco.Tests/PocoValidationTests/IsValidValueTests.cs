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

            t(FhirBoolean.IsValidValue("true"));
            f(FhirBoolean.IsValidValue("True"));

            t(FhirDateTime.IsValidValue("2020"));
            f(FhirDateTime.IsValidValue("2020+01:00"));
            t(FhirDateTime.IsValidValue("2020-01-01T14+01:00"));
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

            t(PositiveInt.IsValidValue("4"));
            f(PositiveInt.IsValidValue("0"));
            f(PositiveInt.IsValidValue("-1"));

            t(UnsignedInt.IsValidValue("4"));
            t(UnsignedInt.IsValidValue("0"));
            f(UnsignedInt.IsValidValue("-1"));

            t(Time.IsValidValue("12:03:04"));
            f(Time.IsValidValue("12:03:04-01:00"));
        }

        private static void t(bool b) => b.Should().BeTrue();
        private static void f(bool b) => b.Should().BeFalse();

        [TestMethod]
        public void TestValidXhtmlXhml()
        {
            t(XHtml.IsValidXml("<hi>hi!</hi>", out _));
            f(XHtml.IsValidXml("<hi>hi!<hi>", out _));
            f(XHtml.IsValidXml("hi!", out _));
            f(XHtml.IsValidNarrativeXhtml("hi!", out _));
            f(XHtml.IsValidNarrativeXhtml("<hi>hi!</hi>", out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _));
            t(XHtml.IsValidNarrativeXhtml("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>", out _));
#pragma warning disable CS0618 // Type or member is obsolete
            f(XHtml.IsValidValue("hi!"));
            f(XHtml.IsValidValue("<hi>hi!</hi>"));
            t(XHtml.IsValidValue("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>"));
            t(XHtml.IsValidValue("<div xmlns=\"http://www.w3.org/1999/xhtml\">some text</div>"));
#pragma warning restore CS0618 // Type or member is obsolete
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

        [DataTestMethod]
        [DynamicData(nameof(ValidOids))]
        public void TestValidOids(string test) => Oid.IsValidValue(test).Should().BeTrue();

        [DataTestMethod]
        [DynamicData(nameof(InvalidOids))]
        public void TestInvalidOids(string test) => Oid.IsValidValue(test).Should().BeFalse();

        [DataTestMethod]
        [DynamicData(nameof(ValidUuids))]
        public void TestValidUuids(string test) => Uuid.IsValidValue(test).Should().BeTrue();

        [DataTestMethod]
        [DynamicData(nameof(InvalidOids))]
        public void TestInvalidUuids(string test) => Uuid.IsValidValue(test).Should().BeFalse();

        [DataTestMethod]
        [DynamicData(nameof(ValidUris))]
        public void TestValidUri(string test) => FhirUri.IsValidValue(test).Should().BeTrue();

        [DataTestMethod]
        [DynamicData(nameof(InvalidUris))]
        public void TestInvalidUri(string test) => FhirUri.IsValidValue(test).Should().BeFalse();

        [DataTestMethod]
        [DynamicData(nameof(ValidUris))]
        public void TestValidCanonical(string test) => Canonical.IsValidValue(test).Should().BeTrue();

        [DataTestMethod]
        [DynamicData(nameof(InvalidUris))]
        public void TestInvalidCanonical(string test) => Canonical.IsValidValue(test).Should().BeFalse();
    }

}
