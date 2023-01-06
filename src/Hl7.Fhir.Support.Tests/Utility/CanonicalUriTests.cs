using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class CanonicalUriTests
    {
        [TestMethod]
        public void SeesAnchorandVersion()
        {
            var testee = new CanonicalUri("http://example.org/test");
            testee.Original.Should().Be("http://example.org/test");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeTrue();

            testee = new CanonicalUri("http://example.org/test|3.4.5");
            testee.Original.Should().Be("http://example.org/test|3.4.5");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeTrue();
            testee.HasAnchor.Should().BeFalse();
            testee.Version.Should().Be("3.4.5");
            testee.IsAbsolute.Should().BeTrue();

            testee = new CanonicalUri("http://example.org/test#anchor");
            testee.Original.Should().Be("http://example.org/test#anchor");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeTrue();
            testee.Anchor.Should().Be("anchor");
            testee.IsAbsolute.Should().BeTrue();

            testee = new CanonicalUri("http://example.org/test|3.4.5#anchor");
            testee.Original.Should().Be("http://example.org/test|3.4.5#anchor");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeTrue();
            testee.Version.Should().Be("3.4.5");
            testee.HasAnchor.Should().BeTrue();
            testee.Anchor.Should().Be("anchor");
            testee.IsAbsolute.Should().BeTrue();
        }

        [TestMethod]
        public void RecognizesUriForm()
        {
            var testee = new CanonicalUri("urn:a:b:c");
            testee.Original.Should().Be("urn:a:b:c");
            testee.Uri.Should().Be("urn:a:b:c");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeTrue();

            testee = new CanonicalUri("local");
            testee.Original.Should().Be("local");
            testee.Uri.Should().Be("local");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeFalse();

            testee = new CanonicalUri("#anchor");
            testee.Original.Should().Be("#anchor");
            testee.Uri.Should().BeNull();
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeTrue();
            testee.Anchor.Should().Be("anchor");
            testee.IsAbsolute.Should().BeFalse();
        }

        [TestMethod]
        public void TestDeconstruction()
        {
            var (uri, version, anchor) = new CanonicalUri("http://example.org/test|3.4.5#anchor");
            uri.Should().Be("http://example.org/test");
            version.Should().Be("3.4.5");
            anchor.Should().Be("anchor");

            (uri, version, anchor) = new CanonicalUri("http://example.org/test");
            uri.Should().Be("http://example.org/test");
            version.Should().BeNull();
            anchor.Should().BeNull();
        }

        [TestMethod]
        public void TestConversion()
        {
            var testee = (CanonicalUri)"http://example.org/test|3.4.5";
            testee.Original.Should().Be("http://example.org/test|3.4.5");

            var asstring = (string)testee;
            asstring.Should().Be("http://example.org/test|3.4.5");

            asstring = testee.ToString();
            asstring.Should().Be("http://example.org/test|3.4.5");

            var uri = testee.ToUri();
            uri.OriginalString.Should().Be("http://example.org/test|3.4.5");
        }

        [TestMethod]
        public void TestEquivalence()
        {
            var t1 = new CanonicalUri("http://example.org/test|3.4.5");
            var t2 = new CanonicalUri("http://example.org/test|3.4.5");
            var t3 = new CanonicalUri("http://example.org/test");

            (t1 == t2).Should().BeTrue();
            t1.Equals(t2).Should().BeTrue();
            (t1 == t3).Should().BeFalse();
            t1.Equals(t3).Should().BeFalse();
        }


    }
}
