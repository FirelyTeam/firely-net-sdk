/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class Base64Tests
{
    [TestMethod]
    public void SetValueUpdatesRawValue()
    {
        var c = new Base64Binary();
        Assert.IsNull(c.JsonValue);
        Assert.IsNull(c.Value);

        var bytes = "Hi!"u8.ToArray();
        c.Value = bytes;
        c.JsonValue.Should().Be("SGkh");

        c.Value = null;
        c.JsonValue.Should().BeNull();
    }


    [TestMethod]
    public void SetRawValueUpdatesValue()
    {
        var c = new Base64Binary { JsonValue = "SGkh" };
        Encoding.UTF8.GetString(c.Value).Should().Be("Hi!");

        // Value gets recomputed when we change it.
        // Since base64binary will only keep the parsed value or the original value,
        // try to see if both remain in sync.
        c.JsonValue = "dGhlcmU=";
        Encoding.UTF8.GetString(c.Value).Should().Be("there");
        c.JsonValue.Should().Be("dGhlcmU=");
        Encoding.UTF8.GetString(c.Value).Should().Be("there");

        c.JsonValue = null;
        c.Value.Should().BeNull();

        c.JsonValue = "Hoi";
        Assert.Throws<CodedValidationException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();

        c.JsonValue = 314;
        Assert.Throws<CodedValidationException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void StringAccessDoesNotDecode(bool xml)
    {
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Ostrich);
        var binary = (Binary)(xml
            ? new FhirXmlDeserializer(settings).DeserializeResource(
                """<Binary xmlns="http://hl7.org/fhir"><contentType value="application/octet-stream"/><content value="/wAB"/></Binary>""")
            : new FhirJsonDeserializer(settings).DeserializeResource(
                """{"resourceType":"Binary","contentType":"application/octet-stream","content":"/wAB"}"""));
        var value = binary.ContentElement;

        var getParsedBytes = PropertyInfoExtensions.GetField<Base64Binary, byte[]>("_parsedValue");
        getParsedBytes(value).Should().BeNull();
        var originalText = (string)value.JsonValue;

        IValue<string> text = value;
        var result = text.Value;

        result.Should().Be("/wAB");
        result.Should().BeSameAs(originalText);
        getParsedBytes(value).Should().BeNull();
    }

    [TestMethod]
    public void BytesReturnBase64()
    {
        var value = new Base64Binary([255, 0, 1]);
        IValue<string> text = value;

        text.Value.Should().Be("/wAB");
        value.Value.Should().Equal(255, 0, 1);
    }

    [TestMethod]
    public void StringAndBytesStayInSync()
    {
        var value = new Base64Binary();
        IValue<string> text = value;
        IValue<byte[]> bytes = value;
        text.Value.Should().BeNull();

        text.Value = "/wAB";
        bytes.Value.Should().Equal(255, 0, 1);
        text.Value.Should().Be("/wAB");

        bytes.Value = [1, 2, 3];
        text.Value.Should().Be("AQID");
        bytes.Value.Should().Equal(1, 2, 3);

        text.Value = null;
        bytes.Value.Should().BeNull();
        text.Value.Should().BeNull();

        text.Value = "AQID";
        bytes.Value = null;
        text.Value.Should().BeNull();
        bytes.Value.Should().BeNull();
    }
}