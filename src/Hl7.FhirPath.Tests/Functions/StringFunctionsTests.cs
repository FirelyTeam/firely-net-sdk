using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HL7.FhirPath.Tests.Functions
{
    [TestClass]
    public class StringFunctionsTests
    {
        [TestMethod]
        public void EncodeBase64()
        {
            StringOperators.EncodeBase64(null).Should().BeNull();
            StringOperators.EncodeBase64("").Should().Be("");
            StringOperators.EncodeBase64("Ewout").Should().Be("RXdvdXQ=");
            StringOperators.EncodeBase64("Ewout>").Should().Be("RXdvdXQ+");
            StringOperators.EncodeBase64("编码方式").Should().Be("57yW56CB5pa55byP");

            StringOperators.FpEncode("Ewout", "base64").Should().Be("RXdvdXQ=");
        }


        [TestMethod]
        public void DecodeBase64()
        {
            StringOperators.DecodeBase64(null).Should().BeNull();
            StringOperators.DecodeBase64("").Should().Be("");
            StringOperators.DecodeBase64("RXdvdXQ=").Should().Be("Ewout");
            StringOperators.DecodeBase64("RXdvdXQ+").Should().Be("Ewout>");                
            StringOperators.DecodeBase64("57yW56CB5pa55byP").Should().Be("编码方式");

            StringOperators.FpDecode("RXdvdXQ=", "base64").Should().Be("Ewout");
        }


        [TestMethod]
        public void EncodeBase64Url()
        {
            StringOperators.EncodeUrlBase64(null).Should().BeNull();
            StringOperators.EncodeUrlBase64("").Should().Be("");
            //            StringOperators.EncodeUrlBase64("Ewout").Should().Be("RXdvdXQ");
            StringOperators.EncodeUrlBase64("Ewout").Should().Be("RXdvdXQ=");
            StringOperators.EncodeUrlBase64("Ewout>").Should().Be("RXdvdXQ-");
            StringOperators.EncodeUrlBase64("编码方式").Should().Be("57yW56CB5pa55byP");

            StringOperators.FpEncode("Ewout", "urlbase64").Should().Be("RXdvdXQ=");
            //StringOperators.FpEncode("Ewout", "urlbase64").Should().Be("RXdvdXQ");
        }

        [TestMethod]
        public void DencodeBase64Url()
        {
            StringOperators.DecodeUrlBase64(null).Should().BeNull();
            StringOperators.DecodeUrlBase64("").Should().Be("");
            StringOperators.DecodeUrlBase64("RXdvdXQ").Should().Be("Ewout");
            StringOperators.DecodeUrlBase64("RXdvdXQ-").Should().Be("Ewout>");
            StringOperators.DecodeUrlBase64("RXdvdXQ=").Should().Be("Ewout");
            StringOperators.DecodeUrlBase64("57yW56CB5pa55byP").Should().Be("编码方式");

            StringOperators.FpDecode("RXdvdXQ", "urlbase64").Should().Be("Ewout");
        }

        [TestMethod]
        public void EncodeHex()
        {
            StringOperators.EncodeHex(null).Should().BeNull();
            StringOperators.EncodeHex("").Should().Be("");
            StringOperators.EncodeHex("Ewout").Should().Be("45776f7574");
            StringOperators.EncodeHex("编码方式").Should().Be("e7bc96e7a081e696b9e5bc8f");

            StringOperators.FpEncode("Ewout", "hex").Should().Be("45776f7574");
        }

        [TestMethod]
        public void DecodeHex()
        {
            StringOperators.DecodeHex(null).Should().BeNull();
            StringOperators.DecodeHex("").Should().Be("");
            StringOperators.DecodeHex("45776f7574").Should().Be("Ewout");
            StringOperators.DecodeHex("e7bc96e7a081e696b9e5bc8f").Should().Be("编码方式");

            StringOperators.FpDecode("45776f7574", "hex").Should().Be("Ewout");
        }

        [TestMethod]
        public void UnknownEncoding()
        {
            Action act = () => StringOperators.FpEncode("Ewout", "reverse_polish");
            act.Should().Throw<ArgumentException>().Which.Message.StartsWith("Unknown encoding 'reverse_polish'.");

            act = () => StringOperators.FpDecode("Ewout", "reverse_polish");
            act.Should().Throw<ArgumentException>().Which.Message.StartsWith("Unknown encoding 'reverse_polish'.");
        }

        [TestMethod]
        public void EscapeJson()
        {
            StringOperators.EscapeJson("hi\t\"there\"! \\/").Should().Be("hi\\t\\\"there\\\"! \\\\/");

            StringOperators.FpEscape("hi\nthere", "json").Should().Be("hi\\nthere");
        }

        [TestMethod]
        public void UnescapeJson()
        {
            StringOperators.UnescapeJson("hi\\t\\\"there\\\"! \\\\/").Should().Be("hi\t\"there\"! \\/");

            StringOperators.FpUnescape("hi\\nthere", "json").Should().Be("hi\nthere");
        }

        [TestMethod]
        public void EscapeHtml()
        {
            StringOperators.EscapeHtml("\"Me < Me & Jou, Jou > Me.\", said he.").Should().Be("&quot;Me &lt; Me &amp; Jou, Jou &gt; Me.&quot;, said he.");

            StringOperators.FpEscape("1 < 5", "html").Should().Be("1 &lt; 5");
        }

        [TestMethod]
        public void UnescapeHtml()
        {
            StringOperators.UnescapeHtml("&quot;Me &lt; Me &amp; Jou, Jou &gt; Me.&quot;, said he.").Should().Be("\"Me < Me & Jou, Jou > Me.\", said he.");

            StringOperators.FpUnescape("1 &lt; 5", "html").Should().Be("1 < 5");
        }

        [TestMethod]
        public void UnknownEscape()
        {
            Action act = () => StringOperators.FpEscape("Ewout", "reverse_polish");
            act.Should().Throw<ArgumentException>().Which.Message.StartsWith("Unknown escaping method 'reverse_polish'.");

            act = () => StringOperators.FpUnescape("Ewout", "reverse_polish");
            act.Should().Throw<ArgumentException>().Which.Message.StartsWith("Unknown escaping method 'reverse_polish'.");
        }

    }

}
