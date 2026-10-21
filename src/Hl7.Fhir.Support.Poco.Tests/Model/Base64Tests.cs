/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
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
    public void ValidatingDoesNotChangeNonCanonicalJsonValue()
    {
        // Convert.FromBase64String is lenient (it ignores whitespace), but validating must not
        // rewrite the literal the user supplied.
        const string literal = "PFBh cmFt";

        var b = new Base64Binary { JsonValue = literal };
        b.HasValidValue().Should().BeTrue();
        b.JsonValue.Should().Be(literal);

        var b2 = new Base64Binary { JsonValue = literal };
        b2.Validate().Should().BeEmpty();
        b2.JsonValue.Should().Be(literal);

        var b3 = new Base64Binary { JsonValue = literal };
        Encoding.UTF8.GetString(b3.Value!).Should().Be("<Param");
        b3.JsonValue.Should().Be(literal);

        // Same for literals that are padded with leading/trailing whitespace or line breaks.
        var b4 = new Base64Binary { JsonValue = " PFBhcmFt\r\n" };
        b4.HasValidValue().Should().BeTrue();
        b4.JsonValue.Should().Be(" PFBhcmFt\r\n");
    }

    [TestMethod]
    public void ValidatingCanonicalValueStillReleasesJsonValue()
    {
        var b = new Base64Binary { JsonValue = "PFBhcmFt" };
        b.HasValidValue().Should().BeTrue();

        // The literal is regenerated from the parsed bytes.
        b.JsonValue.Should().Be("PFBhcmFt");
        Encoding.UTF8.GetString(b.Value!).Should().Be("<Param");
    }
}
