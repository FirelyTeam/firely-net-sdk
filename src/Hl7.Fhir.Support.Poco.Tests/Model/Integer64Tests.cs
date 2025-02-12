/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
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
        Assert.IsNull(c.ObjectValue);
        Assert.IsNull(c.Value);

        c = new Integer64(3);
        Assert.AreEqual("3", c.ObjectValue);
        Assert.AreEqual(3, c.Value);

        c.Value = 5;
        Assert.AreEqual("5", c.ObjectValue);
        Assert.AreEqual(5, c.Value);
    }


    [TestMethod]
    public void SetRawValueUpdatesValue()
    {
        var c = new Integer64 { ObjectValue = "7" };
        Assert.AreEqual(7, c.Value);

        c.ObjectValue = null;
        Assert.IsNull(c.Value);

        c.ObjectValue = "nonsense";
        Assert.ThrowsException<InvalidCastException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();

        c.ObjectValue = 314;
        Assert.ThrowsException<InvalidCastException>(() => c.Value);
        c.HasValidValue().Should().BeFalse();
    }
}