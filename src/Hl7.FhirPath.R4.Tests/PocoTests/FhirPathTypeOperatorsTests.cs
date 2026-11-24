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

        private static PocoNode[] evaluate(PocoNode input, string expression, bool strictAs = false)
            => new FhirPathCompiler().Compile(expression)(input, new FhirEvaluationContext { EnforceAsSingletonRule = strictAs }).ToArray();

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
        public void EnforceAsSingletonRuleRequiresAtMostOneItem()
        {
            var node = new Patient { Name = { new HumanName { Family = "A" }, new HumanName { Family = "B" } } }.ToPocoNode();

            var act = () => evaluate(node, "Patient.name.as(HumanName)", strictAs: true);
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*more than one item*");
            evaluate(node, "Patient.name[0].as(HumanName)", strictAs: true).Should().ContainSingle();

            // By default as() filters collections, since FHIR R4 core invariants (dom-3) rely on that.
            evaluate(node, "Patient.name.as(HumanName)").Should().HaveCount(2);

            act = () => evaluate(node, "Patient.name as HumanName", strictAs: true);
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*more than one item*");

            // By default, the 'as' operator works on collections too, since FHIR R4 search parameters rely on that.
            evaluate(node, "Patient.name as HumanName").Should().HaveCount(2);
            evaluate(node, "Patient.name.ofType(HumanName)").Should().HaveCount(2);
        }

        [TestMethod]
        [DataRow("Patient.gender.as(string1)")]
        [DataRow("Patient.gender as string1")]
        [DataRow("Patient.gender.ofType(string1)")]
        [DataRow("Patient.gender.ofType(FHIR.string1)")]
        [DataRow("Patient.gender.ofType(System.Patient)")]
        [DataRow("Patient.ofType(Observation1)")]
        public void UnknownTypeNameIsAnError(string expression)
        {
            var node = new Patient { Gender = AdministrativeGender.Male }.ToPocoNode();
            var act = () => evaluate(node, expression);
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*Unknown type*");
            act = () => evaluate(node, expression, strictAs: true);
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*Unknown type*");
        }

        [TestMethod]
        public void UnknownTypeNameOnDynamicDataIsNotAnError()
        {
            // Logical models (e.g. CDA) have types no inspector knows about, so we cannot tell whether a name is valid.
            var node = new DynamicResource { DynamicTypeName = "MyLogical" }.ToPocoNode();

            evaluate(node, "ofType(MyLogical)").Should().ContainSingle();
            evaluate(node, "ofType(OtherLogical)").Should().BeEmpty();
            evaluate(node, "as(OtherLogical)", strictAs: true).Should().BeEmpty();

            // System types are fully known, though.
            var act = () => evaluate(node, "ofType(System.Bogus)");
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*Unknown type*");
        }

        [TestMethod]
        [DataRow("Bundle.ofType(Patient)")]
        [DataRow("Bundle.ofType(Patient1)")]
        [DataRow("Bundle.entry.ofType(Patient1)")]
        [DataRow("('a' | 'b').ofType(Patient)")]
        [DataRow("('a' | 'b').ofType(Patient1)")]
        [DataRow("('a' | 'b').as(Patient1)")]
        public void UnknownTypeNameOnBaseModelDataIsNotAnError(string expression)
        {
            // Without an inspector annotation, data that only uses classes from the base assembly cannot tell which
            // FHIR release it belongs to, so the name cannot be verified. The result must not depend on which
            // model inspectors happen to exist in the process.
            var node = new Bundle { Type = Bundle.BundleType.Collection, Entry = { new Bundle.EntryComponent() } }.ToPocoNode();

            evaluate(node, expression).Should().BeEmpty();

            // System types are fully known, though.
            var act = () => evaluate(node, "('a' | 'b').ofType(System.Bogus)");
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*Unknown type*");
        }

        [TestMethod]
        public void BooleanFunctionsOnBooleanWithoutValue()
        {
            var active = new FhirBoolean(null);
            active.AddExtension("http://example.org/ext", new FhirString("x"));
            var node = new Patient { ActiveElement = active }.ToPocoNode();

            evaluate(node, "Patient.active.allTrue()").Single().GetValue().Should().Be(false);
            evaluate(node, "Patient.active.anyTrue()").Single().GetValue().Should().Be(false);
            evaluate(node, "Patient.active.allFalse()").Single().GetValue().Should().Be(false);
            evaluate(node, "Patient.active.anyFalse()").Single().GetValue().Should().Be(false);

            var act = () => evaluate(node, "Patient.active.extension.allTrue()");
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*collection of Booleans*");
        }

        [TestMethod]
        public void UnknownTypeNameIsReportedByTheTypeFunction()
        {
            var node = new Patient { Gender = AdministrativeGender.Male }.ToPocoNode();
            var act = () => evaluate(node, "Patient.gender.ofType(bogus).exists()");
            act.Should().Throw<System.InvalidOperationException>().WithMessage("*'ofType'*Unknown type 'bogus'*");
        }

        [TestMethod]
        [DataRow("Patient.gender.ofType(Observation)")] // the Poco for gender lives in the base assembly
        [DataRow("Patient.gender.ofType(SimpleQuantity)")] // a profile on a core datatype
        [DataRow("Patient.gender.ofType(MoneyQuantity)")]
        [DataRow("Patient.gender.as(DateTime)")] // capitalized FHIR names, as used by STU3 search parameters
        [DataRow("Patient.gender.as(Uri)")]
        [DataRow("Patient.gender.as(Date)")]
        [DataRow("Patient.gender.as(Observation)")]
        [DataRow("Patient.gender.ofType(System.Integer)")]
        [DataRow("Patient.gender.ofType(FHIR.Age)")]
        [DataRow("Patient.gender.ofType(BackboneElement)")]
        [DataRow("Patient.ofType(Observation)")]
        [DataRow("Patient.ofType(Logical.Whatever)")] // types in other namespaces cannot be checked
        [DataRow("('a' | 'b').ofType(Patient)")]
        [DataRow("{}.ofType(string1)")] // nothing to filter
        public void KnownTypeNameWithoutMatchIsEmpty(string expression)
        {
            var node = new Patient { Gender = AdministrativeGender.Male }.ToPocoNode();
            evaluate(node, expression).Should().BeEmpty();
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
