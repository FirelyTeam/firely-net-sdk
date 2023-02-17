using FluentAssertions;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class ContentTypeTests
    {
        [TestMethod]
        public void GetResourceFormatFromContentType_handles_invalidcontenttype()
        {
            Assert.AreEqual(ResourceFormat.Json, ContentType.GetResourceFormatFromContentType("application/fhir+json"));
            Assert.AreEqual(ResourceFormat.Xml, ContentType.GetResourceFormatFromContentType("application/fhir+xml"));
            Assert.AreEqual(ResourceFormat.Unknown, ContentType.GetResourceFormatFromContentType("application/blah"));
            Assert.AreEqual(ResourceFormat.Unknown, ContentType.GetResourceFormatFromContentType("abc123"));
            Assert.AreEqual(ResourceFormat.Unknown, ContentType.GetResourceFormatFromContentType("\"application\blah"));
        }

        [DataTestMethod]
        [DataRow(ResourceFormat.Json, "3.0", "application/fhir+json; charset=utf-8; fhirVersion=3.0")]
        [DataRow(ResourceFormat.Xml, "3.0", "application/fhir+xml; charset=utf-8; fhirVersion=3.0")]
        [DataRow(ResourceFormat.Json, "4.0", "application/fhir+json; charset=utf-8; fhirVersion=4.0")]
        [DataRow(ResourceFormat.Json, "", "application/fhir+json; charset=utf-8")]
        public void TestBuildingContentType(ResourceFormat format, string fhirVersion, string expected)
        {
            ContentType.BuildContentType(format, fhirVersion).Should().Be(expected);
        }
    }
}
