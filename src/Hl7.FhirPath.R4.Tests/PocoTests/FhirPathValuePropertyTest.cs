/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.FhirPath.R4.Tests
{
    /// <summary>
    /// Tests for the .value property on FHIR primitives in FhirPath
    /// See: https://build.fhir.org/fhirpath.html#fn-getValue
    /// </summary>
    [TestClass]
    public class FhirPathValuePropertyTest
    {
        [TestMethod]
        public void ValuePropertyReturnsSystemBooleanFromFhirBoolean()
        {
            var patient = new Patient { Active = true };
            
            // Patient.active returns FHIR.boolean (with potential extensions/id)
            var activeNode = patient.Select("Patient.active").FirstOrDefault();
            activeNode.Should().NotBeNull();
            activeNode.Should().BeOfType<FhirBoolean>();
            
            // Patient.active.value returns system boolean
            var activeValue = patient.Scalar("Patient.active.value");
            activeValue.Should().BeOfType<bool>();
            activeValue.Should().Be(true);
        }

        [TestMethod]
        public void ValuePropertyReturnsSystemStringFromFhirString()
        {
            var patient = new Patient();
            patient.Name.Add(new HumanName { Family = "Smith" });
            
            // Patient.name.family returns FHIR.string
            var familyNode = patient.Select("Patient.name.family").FirstOrDefault();
            familyNode.Should().NotBeNull();
            familyNode.Should().BeOfType<FhirString>();
            
            // Patient.name.family.value returns system string
            var familyValue = patient.Scalar("Patient.name.family.value");
            familyValue.Should().BeOfType<string>();
            familyValue.Should().Be("Smith");
        }

        [TestMethod]
        public void ValuePropertyReturnsSystemIntegerFromFhirInteger()
        {
            var patient = new Patient();
            patient.Telecom.Add(new ContactPoint { Rank = 1 });
            
            // Patient.telecom.rank returns FHIR.integer  
            var rankNode = patient.Select("Patient.telecom.rank").FirstOrDefault();
            rankNode.Should().NotBeNull();
            rankNode.Should().BeOfType<PositiveInt>();
            
            // Patient.telecom.rank.value returns system integer
            var rankValue = patient.Scalar("Patient.telecom.rank.value");
            rankValue.Should().BeOfType<int>();
            rankValue.Should().Be(1);
        }

        [TestMethod]
        public void ValuePropertyReturnsEmptyWhenPrimitiveHasNoValue()
        {
            var patient = new Patient();
            // Create a primitive with only extensions, no value
            patient.ActiveElement = new FhirBoolean();
            patient.ActiveElement.AddExtension("http://example.com/ext", new FhirString("test"));
            
            // Patient.active exists but has no value
            patient.Predicate("Patient.active.exists()").Should().BeTrue();
            patient.Predicate("Patient.active.hasValue()").Should().BeFalse();
            
            // Patient.active.value should be empty
            var activeValue = patient.Select("Patient.active.value");
            activeValue.Should().BeEmpty();
        }

        [TestMethod]
        public void ValuePropertyWorksWithMultipleValues()
        {
            var patient = new Patient();
            patient.Name.Add(new HumanName().WithGiven("John"));
            patient.Name.Add(new HumanName().WithGiven("Jane"));
            
            // Patient.name.given.value returns all system strings
            var givenValues = patient.Select("Patient.name.given.value").ToList();
            givenValues.Should().HaveCount(2);
            givenValues.Select(n => n.ToPocoNode().GetValue()).Should().Equal("John", "Jane");
        }

        [TestMethod]
        public void ValuePropertyDistinguishesFhirTypeFromSystemType()
        {
            var patient = new Patient { Active = true };
            patient.ActiveElement.ElementId = "myid";
            patient.ActiveElement.AddExtension("http://example.com/ext", new FhirString("test"));
            
            // Patient.active returns FHIR.boolean with extensions and id
            var activeNode = patient.Select("Patient.active").FirstOrDefault()?.ToPocoNode();
            activeNode.Should().NotBeNull();
            activeNode.Should().BeOfType<PrimitiveNode>();
            
            // Should have extensions and id available
            var extensions = activeNode.Child("extension");
            extensions.Should().NotBeNull();
            
            var id = activeNode.Child("id");
            id.Should().NotBeNull();
            
            // Patient.active.value returns just the boolean value
            var activeValue = patient.Scalar("Patient.active.value");
            activeValue.Should().BeOfType<bool>();
            activeValue.Should().Be(true);
            
            // The value node should not have extensions or id
            var valueNode = patient.Select("Patient.active.value").FirstOrDefault()?.ToPocoNode();
            valueNode.Should().NotBeNull();
            valueNode.Child("extension").Should().BeNull();
            valueNode.Child("id").Should().BeNull();
        }

        [TestMethod]
        public void OfTypeStillReturnsFhirBoolean()
        {
            var patient = new Patient { Active = true };
            
            // ofType() should still work on the FHIR primitive type
            patient.Predicate("Patient.active.ofType(boolean).exists()").Should().BeTrue();
        }

        [TestMethod]
        public void HasValueAndExistsWorkCorrectly()
        {
            var patient = new Patient();
            
            // No active element at all
            patient.Predicate("Patient.active.hasValue()").Should().BeFalse();
            patient.Predicate("Patient.active.exists()").Should().BeFalse();
            patient.Predicate("Patient.active.empty().not()").Should().BeFalse();
            
            // Active with value
            patient.Active = true;
            patient.Predicate("Patient.active.hasValue()").Should().BeTrue();
            patient.Predicate("Patient.active.exists()").Should().BeTrue();
            patient.Predicate("Patient.active.empty().not()").Should().BeTrue();
            
            // Active with only extensions, no value
            patient.ActiveElement = new FhirBoolean();
            patient.ActiveElement.AddExtension("http://example.com/ext", new FhirString("test"));
            patient.Predicate("Patient.active.hasValue()").Should().BeFalse();
            patient.Predicate("Patient.active.exists()").Should().BeTrue();
            patient.Predicate("Patient.active.empty().not()").Should().BeTrue();
        }

        [TestMethod]
        public void ValuePropertyWorksInComparisonExpressions()
        {
            var patient = new Patient { Active = true };
            
            // Using .value in comparison
            patient.Predicate("Patient.active.value = true").Should().BeTrue();
            patient.Predicate("Patient.active.value = false").Should().BeFalse();
        }

        [TestMethod]
        public void ValuePropertyWorksWithDecimal()
        {
            var observation = new Observation();
            observation.Value = new Quantity { Value = 98.6m };
            
            // Observation.value.value.value should return the decimal
            var value = observation.Scalar("Observation.value.value.value");
            value.Should().BeOfType<decimal>();
            value.Should().Be(98.6m);
        }
    }
}
