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
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Way), out var mapping));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual("Way", mapping.Name);
            Assert.AreEqual(typeof(Way), mapping.NativeType);

            Assert.IsTrue(ClassMapping.TryCreate(typeof(Way2), out mapping));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual("Way2", mapping.Name);
            Assert.AreEqual(typeof(Way2), mapping.NativeType);

            Assert.IsFalse(ClassMapping.TryCreate(typeof(Way2), out _, Specification.FhirRelease.DSTU1));
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Way2), out _, Specification.FhirRelease.DSTU2));
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Way2), out _, Specification.FhirRelease.STU3));
        }

        [TestMethod]
        public void TestCqlInformation()
        {
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Way), out var mapping));

            Assert.IsTrue(mapping.IsPatientClass);
            Assert.IsTrue(mapping.PrimaryCodePath?.Name == "code");

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
            static void task(Type t) => Assert.IsTrue(ClassMapping.TryCreate(t, out var map) && map.PropertyMappings != null);
        }





        [TestMethod]
        public void TestDatatypeMappingCreation()
        {
            Assert.IsTrue(ClassMapping.TryCreate(typeof(AnimalName), out var mapping, (Specification.FhirRelease)int.MaxValue));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(AnimalName), mapping.NativeType);

            Assert.IsTrue(ClassMapping.TryCreate(typeof(NewAnimalName), out mapping));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(NewAnimalName), mapping.NativeType);

            Assert.IsFalse(ClassMapping.TryCreate(typeof(ComplexNumber), out _, Specification.FhirRelease.DSTU1));
            Assert.IsTrue(ClassMapping.TryCreate(typeof(ComplexNumber), out mapping, Specification.FhirRelease.R5));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("Complex", mapping.Name);
            Assert.AreEqual(typeof(ComplexNumber), mapping.NativeType);
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
    public class Way : Resource, IPatient
    {
        [Test("AttrA")]
        [FhirElement("member")]
        [CqlElement(IsBirthDate = true)]
        public string Member { get; set; }

        [Test("AttrB")]
        [FhirElement("code")]
        [CqlElement(IsPrimaryCodePath = true)]
        public string Code { get; set; }

        public Date BirthDate => new(1972, 11, 30);

        public override IDeepCopyable DeepCopy() => throw new NotImplementedException();
    }

    [FhirType("Way2", Since = Specification.FhirRelease.DSTU2)]
    public class Way2 : Resource
    {
        public override IDeepCopyable DeepCopy() { throw new NotImplementedException(); }
    }

    /* 
     * Datatype classes for tests
     */
    [FhirType("AnimalName")]
    public class AnimalName { }

    [FhirType("AnimalName")]
    public class NewAnimalName : AnimalName { }

    [FhirType("Complex", Since = Specification.FhirRelease.DSTU2)]
    public class ComplexNumber { }
}
