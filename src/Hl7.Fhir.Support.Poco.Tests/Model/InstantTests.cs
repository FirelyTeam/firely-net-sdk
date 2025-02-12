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
public class InstantTests
{
    [TestMethod]
    public void SetValueUpdatesRawValue()
    {
        var c = new Instant();
        Assert.IsNull(c.ObjectValue);
        Assert.IsNull(c.Value);

        c = new Instant(DateTimeOffset.UnixEpoch);
        Assert.AreEqual(ElementModel.Types.DateTime.FormatDateTimeOffset(DateTimeOffset.UnixEpoch), c.ObjectValue);
        Assert.AreEqual(DateTimeOffset.UnixEpoch, c.Value);

        var now = DateTimeOffset.Now;
        c.Value = now;
        Assert.AreEqual(ElementModel.Types.DateTime.FormatDateTimeOffset(now), c.ObjectValue);
        Assert.AreEqual(now, c.Value);
    }


    [TestMethod]
    public void SetRawValueUpdatesValue()
    {
        var c = new Instant { ObjectValue = ElementModel.Types.DateTime.FormatDateTimeOffset(DateTimeOffset.UnixEpoch) };
        Assert.AreEqual(DateTimeOffset.UnixEpoch, c.Value);

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