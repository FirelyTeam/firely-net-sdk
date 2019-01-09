using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;


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
    }
}
