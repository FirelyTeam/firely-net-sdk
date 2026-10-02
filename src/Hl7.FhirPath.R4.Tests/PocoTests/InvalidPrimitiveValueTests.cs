/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class InvalidPrimitiveValueTests
    {
        private static PrimitiveType createPrimitive(string type, string literal) => type switch
        {
            "instant" => new Instant { JsonValue = literal },
            "integer64" => new Integer64 { JsonValue = literal },
            "integer" => new Integer { JsonValue = literal },
            "positiveInt" => new PositiveInt { JsonValue = literal },
            "unsignedInt" => new UnsignedInt { JsonValue = literal },
            "date" => new Date(literal),
            "dateTime" => new FhirDateTime(literal),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        [TestMethod]
        [DataRow("instant", "2026-01-20T09:05:06")]
        [DataRow("integer64", "12abc")]
        [DataRow("integer", "12abc")]
        [DataRow("positiveInt", "12abc")]
        [DataRow("unsignedInt", "12abc")]
        [DataRow("date", "2026-02-30")]
        [DataRow("dateTime", "6-08-05T14:32:39Z")]
        public void InvalidLiteralDegradesToTheUnparsedString(string type, string literal)
        {
            var primitive = createPrimitive(type, literal);

            primitive.Scalar("$this").Should().Be(literal);
            primitive.Scalar("$this.toString()").Should().Be(literal);
            primitive.IsTrue("$this = $this").Should().BeTrue();
            primitive.Select("$this.where($this is Reference)").Should().BeEmpty();
            primitive.Select("$this.ofType(Reference)").Should().BeEmpty();
        }

        [TestMethod]
        public void DescendantsSweepSucceedsOverAnInvalidInstant()
        {
            var patient = new Patient
            {
                Meta = new Meta { LastUpdatedElement = new Instant { JsonValue = "2026-01-20T09:05:06" } },
                ManagingOrganization = new ResourceReference("Organization/1")
            };

            patient.Select("descendants().where($this is Reference)").Should().ContainSingle()
                .Which.Should().BeOfType<ResourceReference>();
        }

        [TestMethod]
        public void ValueGetterStillThrowsForAnInvalidInstant()
        {
            var instant = new Instant { JsonValue = "2026-01-20T09:05:06" };

            instant.Invoking(i => i.Value).Should().Throw<Hl7.Fhir.Validation.CodedValidationException>();
        }
    }
}
