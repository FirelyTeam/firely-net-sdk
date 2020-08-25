using Hl7.Fhir.Model;
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

        [TestMethod]
        public void TestBuildingContentType()
        {
            var type = ContentType.BuildContentType(ResourceFormat.Json, ModelInfo.Version);
            Assert.AreEqual("application/fhir+json; charset=utf-8; fhirVersion=4.0", type);
        }
    }
}
