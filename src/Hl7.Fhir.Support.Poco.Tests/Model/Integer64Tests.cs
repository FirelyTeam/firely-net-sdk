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

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class Integer64Tests
{
    [TestMethod]
    public void SetValueUpdatesRawValue()
    {
        var c = new Integer64();
        Assert.IsNull(c.JsonValue);
        Assert.IsNull(c.Value);

        c = new Integer64(3);
        Assert.AreEqual("3", c.JsonValue);
        Assert.AreEqual(3, c.Value);

        c.Value = 5;
        Assert.AreEqual("5", c.JsonValue);
        Assert.AreEqual(5, c.Value);
    }


    [TestMethod]
    public void SetRawValueUpdatesValue()
    {
        var c = new Integer64 { JsonValue = "7" };
        Assert.AreEqual(7, c.Value);

        c.JsonValue = null;
        Assert.IsNull(c.Value);

        c.JsonValue = "nonsense";
        Assert.Throws<CodedValidationException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();

        c.JsonValue = 314;
        Assert.Throws<CodedValidationException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();
    }
}