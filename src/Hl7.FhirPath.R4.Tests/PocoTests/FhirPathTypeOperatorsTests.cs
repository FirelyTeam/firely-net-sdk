/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathTypeOperatorsTests
    {
        private static Observation createObservation() => new()
        {
            Id = "obs1",
            Status = ObservationStatus.Final,
            Code = new CodeableConcept("http://loinc.org", "1234-5"),
            Value = new Quantity(5.0m, "mg"),
            Component = { new Observation.ComponentComponent { Code = new CodeableConcept("http://loinc.org", "6789-0"), Value = new FhirString("hi") } }
        };

        private static PocoNode[] evaluate(PocoNode input, string expression)
            => new FhirPathCompiler().Compile(expression)(input, new FhirEvaluationContext()).ToArray();

        [TestMethod]
        public void BinaryAsIsRegisteredOnce()
        {
            // A duplicate registration forces the evaluator to dispatch dynamically for every evaluation.
            var candidates = FhirPathCompiler.DefaultSymbolTable.Filter("binary.as", 3);
            candidates.Count().Should().Be(1);
        }

        [TestMethod]
        [DataRow("Observation", true)]
        [DataRow("DomainResource", true)]
        [DataRow("Resource", true)]
        [DataRow("Base", true)]
        [DataRow("FHIR.Observation", true)]
        [DataRow("FHIR.DomainResource", true)]
        [DataRow("FHIR.Resource", true)]
        [DataRow("Patient", false)]
        [DataRow("Quantity", false)]
        [DataRow("System.Observation", false)]
        [DataRow("observation", false)]
        public void IsMatchesSelfAndBaseClasses(string type, bool expected)
        {
            var node = createObservation().ToPocoNode();

            // Evaluate twice, the second call goes through the cache of base class names.
            node.Is(type).Should().Be(expected);
            node.Is(type).Should().Be(expected);
        }

        [TestMethod]
        [DataRow("Quantity", true)]
        [DataRow("FHIR.Quantity", true)]
        [DataRow("Element", true)]
        [DataRow("DataType", true)]
        [DataRow("Base", true)]
        [DataRow("Observation", false)]
        [DataRow("System.Quantity", false)]
        public void IsMatchesDataTypes(string type, bool expected)
        {
            var node = createObservation().Value.ToPocoNode();
            node.Is(type).Should().Be(expected);
        }

        [TestMethod]
        [DataRow("string", "string", true)]
        [DataRow("string", "FHIR.string", true)]
        [DataRow("string", "System.string", false)]
        [DataRow("Quantity", "FHIR.Quantity", true)]
        [DataRow("Quantity", "Quantity", true)]
        [DataRow("Quantity", "System.Quantity", false)]
        [DataRow("System.Integer", "Integer", true)]
        [DataRow("System.Integer", "System.Integer", true)]
        [DataRow("System.Integer", "FHIR.Integer", false)]
        [DataRow("FHIR.Integer", "Integer", true)]
        [DataRow("FHIR.Integer", "FHIR.Integer", true)]
        [DataRow("Integer", "System.Integer", false)]
        [DataRow("Integer", "Integer", true)]
        [DataRow(null, "Integer", false)]
        [DataRow(null, "FHIR.Integer", false)]
        public void IsOnTypeNames(string instanceType, string declaredType, bool expected)
        {
            TypeOperators.Is(instanceType, declaredType).Should().Be(expected);
        }

        [TestMethod]
        public void NavigateAcceptsRootTypeAndBaseTypes()
        {
            var node = createObservation().ToPocoNode();

            node.Navigate("Observation").Should().ContainSingle().Which.Should().BeSameAs(node);
            node.Navigate("DomainResource").Should().ContainSingle().Which.Should().BeSameAs(node);
            node.Navigate("Resource").Should().ContainSingle().Which.Should().BeSameAs(node);
            node.Navigate("Patient").Should().BeEmpty();

            evaluate(node, "Observation.status").Should().ContainSingle().Which.GetValue().Should().Be("final");
            evaluate(node, "Resource.id").Should().ContainSingle().Which.GetValue().Should().Be("obs1");
            evaluate(node, "Patient.id").Should().BeEmpty();
        }

        [TestMethod]
        public void AsOperatorAndFunction()
        {
            var node = createObservation().ToPocoNode();

            evaluate(node, "Observation.value as Quantity").Should().ContainSingle()
                .Which.GetValue().Should().BeNull();
            evaluate(node, "Observation.value as Quantity").Single().Is("Quantity").Should().BeTrue();
            evaluate(node, "Observation.value as string").Should().BeEmpty();
            evaluate(node, "Observation.value.as(Quantity)").Should().ContainSingle();
            evaluate(node, "Observation.value.as(string)").Should().BeEmpty();
            evaluate(node, "Observation.component.value as string").Should().ContainSingle()
                .Which.GetValue().Should().Be("hi");
            evaluate(node, "Observation.component.value.as(string)").Should().ContainSingle();
        }

        [TestMethod]
        public void OfTypeAndIsOperators()
        {
            var node = createObservation().ToPocoNode();

            evaluate(node, "Observation.value.ofType(Quantity)").Should().ContainSingle();
            evaluate(node, "Observation.value.ofType(string)").Should().BeEmpty();
            evaluate(node, "Observation.ofType(DomainResource)").Should().ContainSingle();
            evaluate(node, "Observation.ofType(Resource)").Should().ContainSingle();
            evaluate(node, "Observation.ofType(Patient)").Should().BeEmpty();

            evaluate(node, "Observation is Observation").Should().ContainSingle().Which.GetValue().Should().Be(true);
            evaluate(node, "Observation is DomainResource").Should().ContainSingle().Which.GetValue().Should().Be(true);
            evaluate(node, "Observation is Resource").Should().ContainSingle().Which.GetValue().Should().Be(true);
            evaluate(node, "Observation is Patient").Should().ContainSingle().Which.GetValue().Should().Be(false);
            evaluate(node, "Observation.value is Quantity").Should().ContainSingle().Which.GetValue().Should().Be(true);
            evaluate(node, "Observation.value is string").Should().ContainSingle().Which.GetValue().Should().Be(false);
        }
    }
}
