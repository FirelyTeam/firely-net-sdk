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
public class InstantTests
{
    [TestMethod]
    public void SetValueUpdatesRawValue()
    {
        var c = new Instant();
        Assert.IsNull(c.JsonValue);
        Assert.IsNull(c.Value);

        c = new Instant(DateTimeOffset.UnixEpoch);
        Assert.AreEqual(ElementModel.Types.DateTime.FormatDateTimeOffset(DateTimeOffset.UnixEpoch), c.JsonValue);
        Assert.AreEqual(DateTimeOffset.UnixEpoch, c.Value);

        var now = DateTimeOffset.Now;
        c.Value = now;
        Assert.AreEqual(ElementModel.Types.DateTime.FormatDateTimeOffset(now), c.JsonValue);
        Assert.AreEqual(now, c.Value);
    }


    [TestMethod]
    public void SetRawValueUpdatesValue()
    {
        var c = new Instant { JsonValue = ElementModel.Types.DateTime.FormatDateTimeOffset(DateTimeOffset.UnixEpoch) };
        Assert.AreEqual(DateTimeOffset.UnixEpoch, c.Value);

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