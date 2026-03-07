/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class VersionAwarePocoProvTest
    {
        [TestMethod]
        public void TestResourceNameResolving()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            inspector.ImportType(typeof(Way));
            inspector.ImportType(typeof(Way2));

            var way = inspector.FindClassMapping("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual(typeof(Way), way.NativeType);

            var way2 = inspector.FindClassMapping("Way2");
            Assert.IsNotNull(way2);
            Assert.AreEqual(typeof(Way2), way2.NativeType);

            var noway = inspector.FindClassMapping("nonexistent");
            Assert.IsNull(noway);
        }

        [TestMethod]
        public void TestIsBindable()
        {
            ModelInspector.Base.IsBindable("string").Should().BeTrue();
            ModelInspector.Base.IsBindable("uri").Should().BeTrue();
            ModelInspector.Base.IsBindable("Quantity").Should().BeTrue();
            ModelInspector.Base.IsBindable("Extension").Should().BeTrue();
            ModelInspector.Base.IsBindable("Coding").Should().BeTrue();
            ModelInspector.Base.IsBindable("CodeableConcept").Should().BeTrue();
            ModelInspector.Base.IsBindable("CodeableReference").Should().BeTrue();
            ModelInspector.Base.IsBindable("integer").Should().BeFalse();
        }

        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.FindClassMapping("Meta"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Code)));
            Assert.IsNotNull(inspector.FindClassMapping("boolean"));

            // Should also have found the abstract classes
            Assert.IsNotNull(inspector.FindClassMapping("Element"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Resource)));

            // The open generic Code<> should not be there
            var codeOfT = inspector.FindClassMapping(typeof(Code<>));
            Assert.IsNull(codeOfT);
        }

        [TestMethod]
        public void CanManipulateClassMappingsList()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Try to remove a mapping
            var metaMapping = inspector.FindClassMapping("Meta");
            inspector.ClassMappings.Remove(metaMapping);
            inspector.FindClassMapping("Meta").Should().BeNull();

            // And add it back.
            inspector.ClassMappings.Add(metaMapping);
            inspector.FindClassMapping("Meta").Should().NotBeNull();
        }

        /// <summary>
        /// Regression test: types derived from FHIR POCOs (e.g. ValidateCodeParameters which derives
        /// from Parameters) must be importable by ModelInspector even though they do not carry their
        /// own [FhirType] attribute.  The base type's ClassMapping must be returned.
        /// </summary>
        [TestMethod]
        public void FindOrImportClassMappingReturnsMappingForDerivedParametersType()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // ValidateCodeParameters derives from Parameters but has no [FhirType] attribute.
            var mapping = inspector.FindOrImportClassMapping(typeof(ValidateCodeParameters));
            mapping.Should().NotBeNull("a derived FHIR type should fall back to its base type's mapping");
            mapping!.NativeType.Should().Be(typeof(Parameters), "the mapping should belong to the FHIR base type");

            // A second lookup must hit the cache and return the same mapping.
            var mappingAgain = inspector.FindOrImportClassMapping(typeof(ValidateCodeParameters));
            mappingAgain.Should().BeSameAs(mapping);
        }

        /// <summary>
        /// Same as above but for CodeSystemValidateCodeParameters.
        /// </summary>
        [TestMethod]
        public void FindOrImportClassMappingReturnsMappingForDerivedCodeSystemParametersType()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var mapping = inspector.FindOrImportClassMapping(typeof(CodeSystemValidateCodeParameters));
            mapping.Should().NotBeNull();
            mapping!.NativeType.Should().Be(typeof(Parameters));
        }
    }

    [FhirEnumeration("SomeEnum")]
    public enum SomeEnum { Member, AnotherMember }

    public class ActResource
    {
        [FhirEnumeration("SomeOtherEnum")]
        public enum SomeOtherEnum { Member, AnotherMember }
    }
}