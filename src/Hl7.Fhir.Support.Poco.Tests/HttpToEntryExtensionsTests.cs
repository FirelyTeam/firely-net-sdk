using FluentAssertions;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace Hl7.Fhir.Support.Poco.Tests
{

    [TestClass]
    public class HttpToEntryExtensionsTests
    {

        [TestMethod]
        public void ResponseHeadersCaseInsensitiveTest()
        {
            var headers = new WebHeaderCollection
            {
                { "LOWERCASE", "value" }
            };

            var mock = new Mock<HttpWebResponse>();
            mock.Setup(c => c.Headers).Returns(headers);
            var httpWebResponse = mock.Object;

            // a bit of an hack to make HttpWebResponse happy
            var field = typeof(HttpWebResponse).GetField("_httpResponseMessage",
                         BindingFlags.NonPublic |
                         BindingFlags.Instance);
            field.SetValue(httpWebResponse, new HttpResponseMessage());

            // act
            var entryResponse = httpWebResponse.ToEntryResponse(Array.Empty<byte>());

            // assert
            entryResponse.Headers.Should().BeEquivalentTo(new Dictionary<string, string> { { "lowercase", "value" } });
        }

        [TestMethod]
        public void ResponseHeadersCaseInsensitiveTest2()
        {
            var responseMessage = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage()
            };
            responseMessage.Headers.Add("LOWERCASE", "value");
            responseMessage.Headers.Add("ANOTHERHEADER", new[] { "value1", "value2" });

            // act
            var entryResponse = responseMessage.ToEntryResponse(Array.Empty<byte>());

            // assert
            entryResponse.Headers.Should().BeEquivalentTo(new Dictionary<string, string>
            {   { "lowercase", "value" },
                { "anotherheader", "value1"}  // value2 is ignored 
            });
        }
    }
}
