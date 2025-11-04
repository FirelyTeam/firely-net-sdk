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

    public static IEnumerable<object[]> ConversionData()
    {
        var cc = new CodeableConcept([new Coding("http://nu.nl", "code")]);
        var cpt = new P.Concept([new P.Code("http://nu.nl", "code")]);
        var now = DateTimeOffset.UtcNow;

        return
        [
            [Base64Binary.FromBase64String("SGkh"), new P.String("SGkh"), true],
            [new Base64Binary(), null, false],
            [new Canonical("http://nu.nl"), new P.String("http://nu.nl"), true],
            [new Canonical(), null, false],
            [new Code("code"), new P.Code(null, "code"), true],
            [new Code(), null, false],
            [
                new Code<AdministrativeGender>(AdministrativeGender.Female),
                new P.Code("http://hl7.org/fhir/administrative-gender", "female"), true
            ],
            [new Code<AdministrativeGender>(), null, false],
            [new Coding("http://nu.nl", "code"), new P.Code("http://nu.nl", "code"), true],
            [new Coding(), null, false],
            [cc,cpt,true],
            [new CodeableConcept([new Coding(null, null)]), null, false],
            [new CodeableReference(new ResourceReference("http://nu.nl")), new P.String("http://nu.nl"), true],
            [new CodeableReference(cc), cpt, true],
            [new CodeableReference(), null, false],
            [new CodeableReference(new ResourceReference { Type = "Patient" }), null, false],
            [new Date("2021-01-01"), P.Date.Parse("2021-01-01"), true],
            [new Date("2021-15-01"), null, false],
            [new Date(), null, false],
            [new FhirBoolean(true), new P.Boolean(true), true],
            [new FhirBoolean(), null, false],
            [new FhirDateTime("2021-01-01T00:00:00Z"), P.DateTime.Parse("2021-01-01T00:00:00Z"), true],
            [new FhirDateTime("2021-15-01T00:00:00Z"), null, false],
            [new FhirDateTime(), null, false],
            [new FhirDecimal(3.14m), new P.Decimal(3.14m), true],
            [new FhirDecimal(), null, false],
            [new FhirString("hi!"), new P.String("hi!"), true],
            [new FhirString(), null, false],
            [new FhirUri("http://nu.nl"), new P.String("http://nu.nl"), true],
            [new FhirUri(), null, false],
            [new FhirUrl("http://nu.nl"), new P.String("http://nu.nl"), true],
            [new FhirUrl(), null, false],
            [new Id("id"), new P.String("id"), true],
            [new Id(), null, false],
            [new Instant(now), P.DateTime.FromDateTimeOffset(now), true],
            [new Instant(), null, false],
            [new Integer(42), new P.Integer(42), true],
            [new Integer(), null, false],
            [new Integer64(42), new P.Integer(42), true],
            [new Integer64(), null, false],
            [new Markdown("markdown"), new P.String("markdown"), true],
            [new Markdown(), null, false],
            [new Oid("urn:oid:1.2.3"), new P.String("urn:oid:1.2.3"), true],
            [new Oid(), null, false],
            [new PositiveInt(42), new P.Integer(42), true],
            [new PositiveInt(), null, false],
            [new Quantity(3.14m, "kg", "http://unitsofmeasure.org"), new P.Quantity(3.14m, "kg"), true],
            [new Quantity(3.14m, "kg", "http://unitsofmeasure.org") { Comparator = Quantity.QuantityComparator.GreaterThan }, null, false],
            [new Quantity(), null, false],
            [new Ratio(new Quantity(1, "kg"), new Quantity(2, "kg")), new P.Ratio(new P.Quantity(1, "kg"), new P.Quantity(2, "kg")), true],
            [new Ratio(new Quantity(1, "kg") { Comparator = Quantity.QuantityComparator.Ad }, new Quantity(2, "kg")), null, false],
            [new Ratio(), null, false],
            [new Time(14, 30, 0), P.Time.Parse("14:30:00"), true],
            [new Time(24, 30, 0), null, false],
            [new Time(), null, false],
            [new UnsignedInt(42), new P.Integer(42), true],
            [new UnsignedInt(), null, false],
            [new Uuid("urn:uuid:550e8400-e29b-41d4-a716-446655440000"), new P.String("urn:uuid:550e8400-e29b-41d4-a716-446655440000"), true],
            [new Uuid(), null, false],
        ];
    }
}