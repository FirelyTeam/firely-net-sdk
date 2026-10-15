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
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.R4.Tests
{
    /// <summary>
    /// The FHIRPath conversion functions (toX() and convertsToX()) return empty/false for
    /// items that are not convertible, instead of throwing. See issue #3636.
    /// </summary>
    [TestClass]
    public class FhirPathConversionFunctionsTests
    {
        private static readonly string[] ConversionTargets =
            ["Boolean", "Integer", "Long", "Decimal", "Quantity", "String", "Date", "DateTime", "Time"];

        private static Patient createPatient() => new()
        {
            Name = [new HumanName { Family = "Doe", Given = ["John"] }],
            ManagingOrganization = new ResourceReference("Organization/1"),
            Active = true,
            BirthDate = "1974-12-25",
            Telecom = [new ContactPoint { Value = "12345" }],
            Extension = [new Extension("http://example.org/ext", new Quantity(5, "mg", "http://unitsofmeasure.org"))]
        };

        // Complex types without a System type mapping: the resource itself, a Reference, a HumanName and a BackboneElement
        [TestMethod]
        [DataRow("Patient")]
        [DataRow("Patient.managingOrganization")]
        [DataRow("Patient.name.first()")]
        [DataRow("Patient.telecom.first()")]
        [DataRow("Patient.extension.first()")]
        public void ToFunctionsReturnEmptyOnComplexTypes(string focus)
        {
            var patient = createPatient();

            foreach (var target in ConversionTargets)
                patient.Select($"{focus}.to{target}()").Should().BeEmpty($"to{target}() on {focus} should be empty");
        }

        [TestMethod]
        [DataRow("Patient")]
        [DataRow("Patient.managingOrganization")]
        [DataRow("Patient.name.first()")]
        [DataRow("Patient.telecom.first()")]
        [DataRow("Patient.extension.first()")]
        public void ConvertsToFunctionsReturnFalseOnComplexTypes(string focus)
        {
            var patient = createPatient();

            foreach (var target in ConversionTargets)
                patient.Scalar($"{focus}.convertsTo{target}()").Should().Be(false, $"convertsTo{target}() on {focus} should be false");
        }

        [TestMethod]
        public void IssueExamples()
        {
            var patient = createPatient();

            patient.Scalar("Patient.name.first().convertsToString()").Should().Be(false);
            patient.Select("Patient.toString()").Should().BeEmpty();
            patient.Select("Patient.managingOrganization.toString()").Should().BeEmpty();
        }

        [TestMethod]
        public void ConversionOnComplexTypeCanBeUsedInExpressions()
        {
            var patient = createPatient();

            patient.Predicate("Patient.name.first().toString().empty()").Should().BeTrue();
            patient.Predicate("Patient.name.first().convertsToString().not()").Should().BeTrue();
            patient.Scalar("Patient.name.first().toString() | Patient.active.toString()").Should().Be("true");
        }

        // Types that do have a System type mapping, and primitives, should keep working as before.
        [TestMethod]
        public void QuantityStillConverts()
        {
            var patient = createPatient();

            patient.Scalar("Patient.extension.value.convertsToQuantity()").Should().Be(true);
            patient.Scalar("Patient.extension.value.toQuantity()").Should().Be(new P.Quantity(5m, "mg"));
            patient.Scalar("Patient.extension.value.toString()").Should().Be("5 'mg'");
            patient.Scalar("Patient.extension.value.convertsToString()").Should().Be(true);
            patient.Scalar("Patient.extension.value.convertsToBoolean()").Should().Be(false);
        }

        [TestMethod]
        public void CodingAndCodeableConceptKeepReturningEmptyOrFalse()
        {
            var coding = new Coding("http://example.org", "code");
            var concept = new CodeableConcept { Coding = [coding] };

            foreach (var target in ConversionTargets)
            {
                coding.Select($"$this.to{target}()").Should().BeEmpty();
                concept.Select($"$this.to{target}()").Should().BeEmpty();
                coding.Scalar($"$this.convertsTo{target}()").Should().Be(false);
                concept.Scalar($"$this.convertsTo{target}()").Should().Be(false);
            }
        }

        [TestMethod]
        public void PrimitivesStillConvert()
        {
            var patient = createPatient();

            patient.Scalar("Patient.active.toString()").Should().Be("true");
            patient.Scalar("Patient.active.convertsToBoolean()").Should().Be(true);
            patient.Scalar("Patient.active.toBoolean()").Should().Be(true);
            patient.Scalar("Patient.active.toInteger()").Should().Be(1);
            patient.Scalar("Patient.birthDate.toString()").Should().Be("1974-12-25");
            patient.Scalar("Patient.birthDate.convertsToDate()").Should().Be(true);
            patient.Scalar("Patient.birthDate.convertsToBoolean()").Should().Be(false);
            patient.Scalar("Patient.name.family.toString()").Should().Be("Doe");
            patient.Scalar("Patient.telecom.value.toInteger()").Should().Be(12345);
            patient.Scalar("Patient.telecom.value.toLong()").Should().Be(12345L);
            patient.Scalar("Patient.telecom.value.toDecimal()").Should().Be(12345m);
            patient.Scalar("Patient.name.family.convertsToInteger()").Should().Be(false);
        }

        [TestMethod]
        public void LiteralsStillConvert()
        {
            var patient = createPatient();

            patient.Scalar("'1'.toInteger()").Should().Be(1);
            patient.Scalar("1.toString()").Should().Be("1");
            patient.Scalar("(1 + 2).convertsToDecimal()").Should().Be(true);
            patient.Scalar("@2020-01-01.toString()").Should().Be("2020-01-01");
            patient.Scalar("5 'mg'.toQuantity()").Should().Be(new P.Quantity(5m, "mg"));
        }

        [TestMethod]
        public void EmptyFocusStaysEmpty()
        {
            var patient = createPatient();

            foreach (var target in ConversionTargets)
            {
                patient.Select($"{{}}.to{target}()").Should().BeEmpty();
                patient.Select($"{{}}.convertsTo{target}()").Should().BeEmpty();
                patient.Select($"Patient.deceased.to{target}()").Should().BeEmpty();
            }
        }

        [TestMethod]
        public void MultipleItemsStillThrow()
        {
            var patient = createPatient();
            patient.Name.Add(new HumanName { Family = "Roe" });

            // toX() on a collection with more than one item is still an error as specified,
            // that behavior is unrelated to the item's type.
            Action act = () => patient.Select("Patient.name.toString()").ToList();
            act.Should().Throw<Exception>();
            act = () => patient.Select("Patient.name.family.toString()").ToList();
            act.Should().Throw<Exception>();
        }
    }
}
