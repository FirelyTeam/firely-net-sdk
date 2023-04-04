using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var responseMessage = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage()
            };
            responseMessage.Headers.Add("LOWERCASE", "value");
            responseMessage.Headers.Add("ANOTHERHEADER", new[] { "value1", "value2" });

            // act
            var headersExtension = "http://hl7.org/fhir/StructureDefinition/http-response-header";
            var entryResponse = responseMessage.ExtractResponseComponent();
            var headers = entryResponse.GetExtensions(headersExtension).OfType<FhirString>().Select(s => s.Value).ToList();

            // assert
            headers.Should().BeEquivalentTo("lowercase:value", "anotherheader:value1");
        }
    }
}
