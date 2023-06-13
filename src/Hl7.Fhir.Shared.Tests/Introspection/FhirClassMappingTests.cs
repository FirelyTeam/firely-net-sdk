using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class FhirClassMappingTests
    {
        [TestMethod]
        public void VerifyNestedTypeDeterminiation()
        {
            Assert.IsFalse(isNested(typeof(Patient)));
            Assert.IsFalse(isNested(typeof(Timing)));
            Assert.IsFalse(isNested(typeof(FhirBoolean)));
            Assert.IsFalse(isNested(typeof(Element)));
            Assert.IsFalse(isNested(typeof(BackboneElement)));
            Assert.IsFalse(isNested(typeof(Base)));
            Assert.IsFalse(isNested(typeof(DataType)));
            Assert.IsFalse(isNested(typeof(Meta)));
            Assert.IsFalse(isNested(typeof(Narrative)));

            Assert.IsTrue(isNested(typeof(Patient.ContactComponent)));
            Assert.IsTrue(isNested(typeof(DataRequirement.CodeFilterComponent)));

            bool isNested(Type testee)
            {
                _ = ClassMapping.TryCreate(testee, out var cm);
                return cm.IsBackboneType;
            }
        }

        [TestMethod]
        public void HidesPocoClassNames()
        {
            _ = ClassMapping.TryCreate(typeof(Patient.ContactComponent), out var mapping);
            Assert.AreEqual("BackboneElement", getName(mapping));

            _ = ClassMapping.TryCreate(typeof(DataRequirement.CodeFilterComponent), out mapping);
            Assert.AreEqual("Element", getName(mapping));

            _ = ClassMapping.TryCreate(typeof(Code<AdministrativeGender>), out mapping);
            Assert.AreEqual("code", getName(mapping));

            string getName(ClassMapping mp) => ((IStructureDefinitionSummary)mp).TypeName;
        }

        [TestMethod]
        public void TestPerformanceOfMappingAllClasses()
        {
            // just a random list of POCO types available in common
            var typesToTest = ModelInfo.FhirCsTypeToString.Keys;
            Console.WriteLine($"Creating a mapping for all {typesToTest.Count} pocos.");

            int numRepeats = 10;

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < numRepeats; i++)
                foreach (var testee in typesToTest)
                    createMapping(testee);
            sw.Stop();
            Console.WriteLine($"No props: {sw.ElapsedMilliseconds / (float)numRepeats}ms");

            sw.Restart();
            for (int i = 0; i < numRepeats; i++)
                foreach (var testee in typesToTest)
                    createMapping(testee, touchProps: true);
            sw.Stop();
            Console.WriteLine($"With props: {sw.ElapsedMilliseconds / (float)numRepeats}ms");

            int createMapping(Type t, bool touchProps = false)
            {
                ClassMapping.TryCreate(t, out var mapping);
                return touchProps ? mapping.PropertyMappings.Count : -1;
            }
        }

        [TestMethod]
        public void LoadsDependentSatelliteAssemblies()
        {
            var satellite = typeof(ModelInfo).Assembly;
            var inspector = ModelInspector.ForAssembly(satellite);

            inspector.FindClassMapping(typeof(Patient)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(StructureDefinition)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(ValueSet)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(OperationOutcome)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(DomainResource)).Should().NotBeNull();
        }


        [TestMethod]
        public void LoadsDependentConformanceAssemblies()
        {
            var satellite = typeof(StructureDefinition).Assembly;
            var inspector = ModelInspector.ForAssembly(satellite);

            inspector.FindClassMapping(typeof(StructureDefinition)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(ValueSet)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(OperationOutcome)).Should().NotBeNull();
            inspector.FindClassMapping(typeof(DomainResource)).Should().NotBeNull();
        }


        [TestMethod]
        public void FindsCorrectFhirVersion()
        {
            var satellite = typeof(ModelInfo).Assembly;
            IModelInfo mi = ModelInspector.ForAssembly(satellite);  // R5 is arbitrary here

            mi.FhirVersion.Should().Be(ModelInfo.Version);
        }
    }
}