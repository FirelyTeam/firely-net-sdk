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

        // An application-defined subclass in an assembly that carries no FhirModelAssemblyAttribute,
        // so no ModelInspector can supply metadata for that assembly. Type checks (including the ones
        // that do not match and therefore test the whole hierarchy) must keep working for these.
        private class MyObservation : Observation { }

        [TestMethod]
        [DataRow("Observation", true)]
        [DataRow("DomainResource", true)]
        [DataRow("Resource", true)]
        [DataRow("Base", true)]
        [DataRow("FHIR.Resource", true)]
        [DataRow("Patient", false)]
        [DataRow("MyObservation", false)]
        public void IsWorksForPocoSubclassFromAssemblyWithoutFhirMetadata(string type, bool expected)
        {
            // Evaluate twice: the second round goes through the cache.
            for (var i = 0; i < 2; i++)
            {
                var node = new MyObservation { Id = "obs2" }.ToPocoNode();

                node.Is(type).Should().Be(expected);
                node.Navigate("Resource").Should().ContainSingle().Which.Should().BeSameAs(node);
                evaluate(node, "Resource.id").Should().ContainSingle().Which.GetValue().Should().Be("obs2");
                evaluate(node, "Resource.ofType(Resource)").Should().ContainSingle();
                evaluate(node, "Observation.ofType(Patient)").Should().BeEmpty();
            }
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

        [TestMethod]
        [DataRow("code", "string", true)]
        [DataRow("code", "FHIR.string", true)]
        [DataRow("code", "System.String", false)]
        [DataRow("code", "id", false)]
        [DataRow("id", "string", true)]
        [DataRow("markdown", "string", true)]
        [DataRow("url", "uri", true)]
        [DataRow("canonical", "uri", true)]
        [DataRow("oid", "uri", true)]
        [DataRow("uuid", "uri", true)]
        [DataRow("uuid", "FHIR.uri", true)]
        [DataRow("uri", "url", false)]
        [DataRow("uri", "string", false)]
        [DataRow("positiveInt", "integer", true)]
        [DataRow("unsignedInt", "integer", true)]
        [DataRow("integer", "positiveInt", false)]
        [DataRow("string", "code", false)]
        public void IsFollowsPrimitiveTypeHierarchy(string primitive, string type, bool expected)
        {
            PrimitiveType value = primitive switch
            {
                "code" => new Code("male"),
                "id" => new Id("a1"),
                "markdown" => new Markdown("*hi*"),
                "url" => new FhirUrl("http://example.org"),
                "canonical" => new Canonical("http://example.org/sd"),
                "oid" => new Oid("urn:oid:1.2.3"),
                "uuid" => new Uuid("urn:uuid:c757873d-ec9a-4326-a141-556f43239520"),
                "uri" => new FhirUri("http://example.org"),
                "positiveInt" => new PositiveInt(1),
                "unsignedInt" => new UnsignedInt(0),
                "integer" => new Integer(1),
                "string" => new FhirString("hi"),
                _ => throw new System.ArgumentException(primitive)
            };
            var node = new Parameters { { "p", value } }.ToPocoNode();

            evaluate(node, $"parameter.value.is({type})").Should().ContainSingle().Which.GetValue().Should().Be(expected);
            evaluate(node, $"parameter.value is {type}").Should().ContainSingle().Which.GetValue().Should().Be(expected);
        }

        [TestMethod]
        public void AsAndOfTypeDoNotFollowPrimitiveTypeHierarchy()
        {
            // The official FHIRPath tests (testFHIRPathAsFunction11/16) expect as() and ofType() to match
            // the exact primitive type only, while is() also accepts the base primitive type.
            var node = new Patient { Gender = AdministrativeGender.Male }.ToPocoNode();

            evaluate(node, "Patient.gender.is(string)").Should().ContainSingle().Which.GetValue().Should().Be(true);
            evaluate(node, "Patient.gender.as(string)").Should().BeEmpty();
            evaluate(node, "Patient.gender as string").Should().BeEmpty();
            evaluate(node, "Patient.gender.ofType(string)").Should().BeEmpty();
            evaluate(node, "Patient.gender.as(code)").Should().ContainSingle();
            evaluate(node, "Patient.gender.ofType(code)").Should().ContainSingle();
        }
    }
}
