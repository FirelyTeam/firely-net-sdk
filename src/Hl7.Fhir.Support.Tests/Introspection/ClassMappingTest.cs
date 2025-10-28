/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class EnumMappingTest
    {
        private enum Random
        {
            Eight,
            Five,
            Three
        }

        [TestMethod]
        public void TestEnumMappingCreation()
        {
            EnumMapping.TryCreate(typeof(EnumMappingTest), out var _).Should().BeFalse();
            EnumMapping.TryCreate(typeof(FilterOperator), out var mapping).Should().BeTrue();

            mapping.Canonical.Should().Be("http://hl7.org/fhir/ValueSet/filter-operator");
            mapping.Name.Should().Be("FilterOperator");

            var values = Enum.GetValues<FilterOperator>();
            mapping.Members.Should().HaveCount(values.Length);
            mapping.Members.Select(kvp => kvp.Value.Value).Should().BeEquivalentTo(values);

            var equals = mapping.Members["="];
            equals.Code.Should().Be("=");
            equals.Value.Should().Be(FilterOperator.Equal);
            equals.Description.Should().Be("Equals");
            equals.System.Should().Be("http://hl7.org/fhir/filter-operator");
        }
    }

    [TestClass]
    public class ClassMappingTest
    {
        [TestMethod]
        public void TestResourceMappingCreation()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(Way), out var mapping));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual("Way", mapping.Name);
            Assert.AreEqual(typeof(Way), mapping.NativeType);

            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(Way2), out mapping));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual("Way2", mapping.Name);
            Assert.AreEqual(typeof(Way2), mapping.NativeType);
        }

        [TestMethod]
        public void Mapping_Creation_Is_Sensitive_To_Fhir_Version()
        {
            var mir3 = new ModelInspector(FhirRelease.STU3);

            Assert.IsTrue(ClassMapping.TryCreate(mir3, typeof(Way2), out var mapping));
            mapping.PropertyMappings.Should().Contain(pm => pm.Name == "original");
            mapping.PropertyMappings.Should().NotContain(pm => pm.Name == "r4");

            var mir4 = new ModelInspector(FhirRelease.R4);
            Assert.IsTrue(ClassMapping.TryCreate(mir4, typeof(Way2), out mapping));
            mapping.PropertyMappings.Should().Contain(pm => pm.Name == "original");
            mapping.PropertyMappings.Should().Contain(pm => pm.Name == "r4");

            var mir5 = new ModelInspector(FhirRelease.R5);
            Assert.IsTrue(ClassMapping.TryCreate(mir5, typeof(Way2), out mapping));
            mapping.PropertyMappings.Should().Contain(pm => pm.Name == "original");
            mapping.PropertyMappings.Should().NotContain(pm => pm.Name == "r4");
        }

        [TestMethod]
        public void TestCqlInformation()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(Way), out var mapping));

            Assert.IsTrue(mapping.IsPatientClass);
            Assert.IsTrue(typeof(Way).IsAssignableTo(typeof(ICoded<string>)));

            var inspector = new ModelInspector(Specification.FhirRelease.STU3);
            inspector.ImportType(typeof(Way));
            inspector.ImportType(typeof(Way2));
        }


        /// <summary>
        /// Test for issue 556 (https://github.com/FirelyTeam/firely-net-sdk/issues/556) 
        /// </summary>
        [TestMethod]
        public void GetMappingsInParrallel()
        {
            var nrOfParrallelTasks = 50;

            var fhirTypesInCommonAssembly = typeof(Base).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<FhirTypeAttribute>().Any() && t != typeof(Code<>));

            var typesToInspect = new List<Type>();
            while (typesToInspect.Count < 500)
                typesToInspect.AddRange(fhirTypesInCommonAssembly);

            // first, check this work without parrallellism
            foreach (var type in typesToInspect) task(type);

            // then do it in parrallel
            var result = Parallel.ForEach(
                    typesToInspect,
                    new ParallelOptions() { MaxDegreeOfParallelism = nrOfParrallelTasks },
                    task);

            Assert.IsTrue(result.IsCompleted);

            // Create mapping (presumably once) && also touch properties to initialize them as well.
            static void task(Type t) => Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, t, result: out var _));
        }

        [TestMethod]
        public void TestDatatypeMappingCreation()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(AnimalName), out var mapping));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(AnimalName), mapping.NativeType);

            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(NewAnimalName), out mapping));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(NewAnimalName), mapping.NativeType);
        }

        [TestMethod]
        public void CanManipulatePropertyMappingsList()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);
            var metaMapping = inspector.FindClassMapping("Meta")!;
            var profileMapping = metaMapping.FindMappedElementByName("profile")!;

            // Try to remove a mapping
            metaMapping.PropertyMappings.Remove(profileMapping);
            metaMapping.FindMappedElementByName("profile").Should().BeNull();

            // And add it back.
            metaMapping.PropertyMappings.Add(profileMapping);
            metaMapping.FindMappedElementByName("profile").Should().NotBeNull();
        }
    }


    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class TestAttribute : Attribute
    {
        public TestAttribute(string data) => PositionalString = data;

        public string PositionalString { get; private set; }
    }

    /*
     * Resource classes for tests 
     */
    [FhirType("Way")]
    [Test("One")]
    [Test("Two")]
    public class Way : Resource, IPatient, ICoded<string>
    {
        [Test("AttrA")]
        [FhirElement("member")]
        public string Member { get; set; }

        [Test("AttrB")]
        [FhirElement("code")]
        public string Code { get; set; }

        public Date BirthDate => new(1972, 11, 30);
        
        public IReadOnlyCollection<Coding> ToCodings() => [new(null, Code)];
        protected internal override Base DeepCopyInternal() => throw new NotImplementedException();
    }

    [FhirType("Way2")]
    public class Way2 : Resource
    {
        [FhirElement("original")]
        public FhirBoolean OriginalElement { get; set; }

        [FhirElement("r4", Since = FhirRelease.R4)]
        [NotMapped(Since = FhirRelease.R5)]
        public FhirBoolean R4Element { get; set; }

        protected internal override Base DeepCopyInternal() => throw new NotImplementedException();
    }

    /* 
     * Datatype classes for tests
     */
    [FhirType("AnimalName")]
    public class AnimalName { }

    [FhirType("AnimalName")]
    public class NewAnimalName : AnimalName { }
}