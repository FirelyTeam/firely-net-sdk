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

        [TestMethod]
        public void PrimitiveValuePropertyIsStableAndTracksChanges()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(FhirBoolean), out var mapping));

            var valueProperty = mapping.PrimitiveValueProperty;
            valueProperty.Should().NotBeNull();
            valueProperty!.Name.Should().Be("value");
            valueProperty.RepresentsValueElement.Should().BeTrue();
            mapping.HasPrimitiveValueMember.Should().BeTrue();

            // Repeated reads return the very same mapping (the value is computed once).
            mapping.PrimitiveValueProperty.Should().BeSameAs(valueProperty);

            // But it does follow changes made to the list of property mappings.
            mapping.PropertyMappings.Remove(valueProperty);
            mapping.PrimitiveValueProperty.Should().BeNull();
            mapping.HasPrimitiveValueMember.Should().BeFalse();

            mapping.PropertyMappings.Add(valueProperty);
            mapping.PrimitiveValueProperty.Should().BeSameAs(valueProperty);
            mapping.HasPrimitiveValueMember.Should().BeTrue();

            // Classes without a value element have no primitive value property.
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(Way), out var wayMapping));
            wayMapping.PrimitiveValueProperty.Should().BeNull();
            wayMapping.HasPrimitiveValueMember.Should().BeFalse();
            wayMapping.PrimitiveValueProperty.Should().BeNull();
        }

        [TestMethod]
        public void CreateInstanceAndCreateListReturnNewInstances()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(FhirBoolean), out var mapping));

            var first = mapping.CreateInstance();
            var second = mapping.CreateInstance();
            first.Should().BeOfType<FhirBoolean>();
            second.Should().BeOfType<FhirBoolean>().And.NotBeSameAs(first);

            var list = mapping.CreateList();
            list.Count.Should().Be(0);
            list.Add(first);
            list.Count.Should().Be(1);

            var otherList = mapping.CreateList();
            otherList.Should().NotBeSameAs(list);
            otherList.Count.Should().Be(0);
        }

        [TestMethod]
        public void PrimitiveValuePropertyThrowsOnDuplicateValueElements()
        {
            // A class declaring two value elements is malformed, but PrimitiveValueProperty has
            // always reported that by throwing, so make sure caching the scan did not swallow it.
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);
            var stringMapping = inspector.FindClassMapping("string")!;

            var twoValues = new ClassMapping(inspector, "TwoValues", typeof(DynamicDataType), cm =>
            [
                new PropertyMapping(cm, "value", typeof(FhirString), [stringMapping]) { RepresentsValueElement = true },
                new PropertyMapping(cm, "alsoValue", typeof(FhirString), [stringMapping]) { RepresentsValueElement = true }
            ]);

            // HasPrimitiveValueMember answers the "is there at least one" question, and still does.
            twoValues.HasPrimitiveValueMember.Should().BeTrue();

            // Repeatedly, not just on the first (uncached) read.
twoValues.Invoking(m => m.PrimitiveValueProperty).Should().Throw<InvalidOperationException>().WithMessage("*more than one element*");
twoValues.Invoking(m => m.PrimitiveValueProperty).Should().Throw<InvalidOperationException>().WithMessage("*more than one element*");
        }

        [TestMethod]
        public void PrimitiveValuePropertyIsInvalidatedByClearAndAddRange()
        {
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(FhirBoolean), out var mapping));
            var valueProperty = mapping.PrimitiveValueProperty!;
            valueProperty.Should().NotBeNull();

            var mappings = (PropertyMappingCollection)mapping.PropertyMappings;
            var all = mappings.ToList();

            mappings.Clear();
            mapping.PrimitiveValueProperty.Should().BeNull();
            mapping.HasPrimitiveValueMember.Should().BeFalse();

            mappings.AddRange(all);
            mapping.PrimitiveValueProperty.Should().BeSameAs(valueProperty);
            mapping.HasPrimitiveValueMember.Should().BeTrue();
        }

        [TestMethod]
        public void RemovingAMappingByNameInvalidatesThePrimitiveValueProperty()
        {
            // Mappings are keyed by name (case-insensitively), so Remove() evicts whatever mapping
            // carries the given name - which need not be the very instance passed in. Any cache of
            // the value element has to follow that, or it hands out an evicted mapping.
            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(FhirBoolean), out var mapping));
            var stored = mapping.PrimitiveValueProperty!;
            stored.Should().NotBeNull();
            stored.Name.Should().Be("value");

            var sameNameOtherInstance =
                new PropertyMapping(mapping, "VALUE", typeof(FhirBoolean), [ModelInspector.Base.FindClassMapping("boolean")!]);
            sameNameOtherInstance.Should().NotBeSameAs(stored);

            mapping.PropertyMappings.Remove(sameNameOtherInstance).Should().BeTrue();

            mapping.FindMappedElementByName("value").Should().BeNull();
            mapping.PrimitiveValueProperty.Should().BeNull();
            mapping.HasPrimitiveValueMember.Should().BeFalse();
        }

        [TestMethod]
        public void LazyMembersAreSafeToReadConcurrently()
        {
            // Unlike GetMappingsInParrallel (which only races ClassMapping.TryCreate), this races
            // the lazily initialized members themselves on a mapping that starts out uninitialized.
            const int threads = 64;

            Assert.IsTrue(ClassMapping.TryCreate(ModelInspector.Base, typeof(FhirBoolean), out var mapping));

            var propertyMappings = new ICollection<PropertyMapping>[threads];
            var valueProperties = new PropertyMapping[threads];
            var instances = new Base[threads];
            var lists = new System.Collections.IList[threads];

            var result = Parallel.For(0, threads, new ParallelOptions { MaxDegreeOfParallelism = threads }, i =>
            {
                propertyMappings[i] = mapping.PropertyMappings;
                valueProperties[i] = mapping.PrimitiveValueProperty!;
                instances[i] = mapping.CreateInstance();
                lists[i] = mapping.CreateList();
            });

            result.IsCompleted.Should().BeTrue();

            // Every thread must observe the one published collection and the one value element...
            propertyMappings.Should().AllSatisfy(pm => pm.Should().BeSameAs(propertyMappings[0]));
            valueProperties.Should().AllSatisfy(vp => vp.Should().BeSameAs(valueProperties[0]));
            valueProperties[0].Should().NotBeNull();
            valueProperties[0].Name.Should().Be("value");

            // ...while the factories must still hand out a fresh instance/list to each of them.
            instances.Should().OnlyHaveUniqueItems().And.AllBeOfType<FhirBoolean>();
            lists.Should().OnlyHaveUniqueItems();
            lists.Should().AllSatisfy(l => l.Count.Should().Be(0));
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