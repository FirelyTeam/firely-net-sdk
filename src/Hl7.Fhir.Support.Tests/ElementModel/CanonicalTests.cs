using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class CanonicalTests
    {
        [TestMethod]
        public void SeesAnchorandVersion()
        {
            var testee = new Canonical("http://example.org/test");
            testee.Value.Should().Be("http://example.org/test");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeTrue();

            testee = new Canonical("http://example.org/test|3.4.5");
            testee.Value.Should().Be("http://example.org/test|3.4.5");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeTrue();
            testee.HasAnchor.Should().BeFalse();
            testee.Version.Should().Be("3.4.5");
            testee.IsAbsolute.Should().BeTrue();

            testee = new Canonical("http://example.org/test#anchor");
            testee.Value.Should().Be("http://example.org/test#anchor");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeTrue();
            testee.Fragment.Should().Be("anchor");
            testee.IsAbsolute.Should().BeTrue();

            testee = new Canonical("http://example.org/test|3.4.5#anchor");
            testee.Value.Should().Be("http://example.org/test|3.4.5#anchor");
            testee.Uri.Should().Be("http://example.org/test");
            testee.HasVersion.Should().BeTrue();
            testee.Version.Should().Be("3.4.5");
            testee.HasAnchor.Should().BeTrue();
            testee.Fragment.Should().Be("anchor");
            testee.IsAbsolute.Should().BeTrue();
            
            // no path
            testee = new Canonical("http://example.org|3.4.5#anchor");
            testee.Value.Should().Be("http://example.org|3.4.5#anchor");
            testee.Uri.Should().Be("http://example.org");
            testee.HasVersion.Should().BeTrue();
            testee.Version.Should().Be("3.4.5");
            testee.HasAnchor.Should().BeTrue();
            testee.Fragment.Should().Be("anchor");
            testee.IsAbsolute.Should().BeTrue();
        }

        [TestMethod]
        public void RecognizesUriForm()
        {
            var testee = new Canonical("urn:a:b:c");
            testee.Value.Should().Be("urn:a:b:c");
            testee.Uri.Should().Be("urn:a:b:c");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeTrue();

            testee = new Canonical("local");
            testee.Value.Should().Be("local");
            testee.Uri.Should().Be("local");
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeFalse();
            testee.IsAbsolute.Should().BeFalse();

            testee = new Canonical("#anchor");
            testee.Value.Should().Be("#anchor");
            testee.Uri.Should().BeNull();
            testee.HasVersion.Should().BeFalse();
            testee.HasAnchor.Should().BeTrue();
            testee.Fragment.Should().Be("anchor");
            testee.IsAbsolute.Should().BeFalse();
        }

        [TestMethod]
        public void TestDeconstruction()
        {
            var (uri, version, anchor) = new Canonical("http://example.org/test|3.4.5#anchor");
            uri.Should().Be("http://example.org/test");
            version.Should().Be("3.4.5");
            anchor.Should().Be("anchor");

            (uri, version, anchor) = new Canonical("http://example.org/test");
            uri.Should().Be("http://example.org/test");
            version.Should().BeNull();
            anchor.Should().BeNull();
        }

        [TestMethod]
        public void TestConversion()
        {
            var testee = (Canonical)"http://example.org/test|3.4.5";
            testee.Value.Should().Be("http://example.org/test|3.4.5");

            var asstring = (string)testee;
            asstring.Should().Be("http://example.org/test|3.4.5");

            asstring = testee.ToString();
            asstring.Should().Be("http://example.org/test|3.4.5");

            var uri = testee.ToUri();
            uri.OriginalString.Should().Be("http://example.org/test|3.4.5");
        }
    }
}
