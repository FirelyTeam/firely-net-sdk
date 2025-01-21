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
using System.Collections.Generic;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Support.Poco.Tests.Model;

[TestClass]
public class ToSystemTypeTests
{
    [TestMethod]
    [DynamicData(nameof(ConversionData))]
    public void TryConvertTypeToSystemType(Base from, P.Any expected, bool success)
    {
        var toSystem = (P.IToSystemPrimitive)from;
        var actualSuccess = toSystem.TryConvertToSystemType(out var actual);

        actualSuccess.Should().Be(success);
        if (actualSuccess)
        {
            actual.ToString().Should().Be(expected.ToString());
        }
    }

    public static IEnumerable<object[]> ConversionData =>
            [
                [new Canonical("http://nu.nl"), new P.String("http://nu.nl"), true],
                [new Code("code"), new P.Code(null,"code"), true],
            ];
}