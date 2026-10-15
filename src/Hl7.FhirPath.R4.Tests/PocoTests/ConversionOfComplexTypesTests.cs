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

namespace Hl7.FhirPath.R4.Tests
{
    // The conversion functions convert a single item of the types listed for each function. "If the item is not one
    // of the above types, the result is empty" for toX(), and false for convertsToX().
    // https://hl7.org/fhirpath/STU3/#conversion
    [TestClass]
    public class ConversionOfComplexTypesTests
    {
        private static Patient createPatient() => new()
        {
            Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">John Doe</div>" },
            Extension = [new Extension("http://example.org/extensions/test", new FhirString("x"))],
            Active = true,
            Name = [new HumanName { Family = "Doe", Given = ["John"] }],
            ManagingOrganization = new ResourceReference("Organization/1"),
            MaritalStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "M")
        };

        [TestMethod]
        public void ConvertsToStringIsFalseForAComplexType()
        {
            // the example from the spec for convertsToString()
            createPatient().Scalar("Patient.name.first().convertsToString()").Should().Be(false);
        }

        [TestMethod]
        [DataRow("Patient.name.first()")]
        [DataRow("Patient.managingOrganization")]
        [DataRow("Patient.extension.first()")]
        [DataRow("Patient.text")]
        [DataRow("Patient")]
        public void ToStringIsEmptyForAComplexType(string item)
        {
            createPatient().Select($"{item}.toString()").Should().BeEmpty();
        }

        [TestMethod]
        [DataRow("Boolean")]
        [DataRow("Integer")]
        [DataRow("Long")]
        [DataRow("Decimal")]
        [DataRow("Quantity")]
        [DataRow("String")]
        [DataRow("Date")]
        [DataRow("DateTime")]
        [DataRow("Time")]
        public void ConversionFunctionsDoNotConvertAComplexType(string type)
        {
            var patient = createPatient();

            patient.Select($"Patient.name.first().to{type}()").Should().BeEmpty();
            patient.Scalar($"Patient.name.first().convertsTo{type}()").Should().Be(false);
        }

        [TestMethod]
        public void QuantityConvertsUnlessItHasNoSystemTypeEquivalent()
        {
            var observation = new Observation { Value = new Quantity(70.5m, "kg") };

            observation.Scalar("Observation.value.toString()").Should().Be("70.5 'kg'");
            observation.Scalar("Observation.value.convertsToString()").Should().Be(true);
            observation.Scalar("Observation.value.convertsToQuantity()").Should().Be(true);

            // a Quantity with a comparator cannot be represented as a System Quantity
            observation.Value = new Quantity(70.5m, "kg") { Comparator = Quantity.QuantityComparator.LessThan };

            observation.Select("Observation.value.toString()").Should().BeEmpty();
            observation.Scalar("Observation.value.convertsToQuantity()").Should().Be(false);
        }

        [TestMethod]
        public void ConceptsDoNotConvertToString()
        {
            var patient = createPatient();

            // a CodeableConcept has a System type equivalent (Concept), but that does not convert to a string
            patient.Select("Patient.maritalStatus.toString()").Should().BeEmpty();
            patient.Scalar("Patient.maritalStatus.convertsToString()").Should().Be(false);

            // a Coding without a code has no System type equivalent at all
            patient.MaritalStatus.Coding[0].Code = null;

            patient.Select("Patient.maritalStatus.coding.toString()").Should().BeEmpty();
            patient.Scalar("Patient.maritalStatus.coding.convertsToString()").Should().Be(false);
        }

        [TestMethod]
        public void TypeGuardsOnAChoiceElement()
        {
            // xor has to evaluate both operands, so toString() is invoked on the Period
            var observation = new Observation { Effective = new Period { Start = "2026-01-01", End = "2026-01-02" } };

            observation.Scalar("Observation.effective.select($this is dateTime implies $this.toString().length() >= 10)").Should().Be(true);
            observation.Select("Observation.effective.select(($this is dateTime) xor ($this.toString().length() >= 10))").Should().BeEmpty();
            observation.Select("Observation.effective.toString().length()").Should().BeEmpty();
        }

        [TestMethod]
        public void SpecExamplesForPrimitivesAreUnchanged()
        {
            var patient = createPatient();

            patient.Scalar("@2025-01-15.convertsToString()").Should().Be(true);
            patient.Scalar("1.convertsToString()").Should().Be(true);
            patient.Select("{}.convertsToString()").Should().BeEmpty();
            patient.Scalar("(53 'km').toString()").Should().Be("53 'km'");
            patient.Scalar("Patient.active.toString()").Should().Be("true");
        }

        [TestMethod]
        public void MultipleItemsAreStillAnError()
        {
            var patient = createPatient();
            patient.Name.Add(new HumanName { Family = "Roe" });

            patient.Invoking(p => p.Select("Patient.name.toString()")).Should().Throw<InvalidOperationException>()
                .WithMessage("*'toString'*'collection of HumanName' to 'any type'*");
        }
    }
}
