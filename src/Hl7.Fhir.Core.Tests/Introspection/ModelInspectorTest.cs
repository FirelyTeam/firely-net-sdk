/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
#if PORTABLE45
	public class PortableModelInspectorTest
#else
	public class ModelInspectorTest
#endif
    {
        [TestMethod]
        public void TestResourceNameResolving()
        {
            var inspector = new ModelInspector();

            inspector.ImportType(typeof(Road));
            inspector.ImportType(typeof(Way));
            inspector.ImportType(typeof(ProfiledWay));
            inspector.ImportType(typeof(NewStreet));
            //inspector.Process();

            var road = inspector.FindClassMappingForResource("roAd");
            Assert.IsNotNull(road);
            Assert.AreEqual(road.NativeType, typeof(Road));

            var way = inspector.FindClassMappingForResource("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual(way.NativeType, typeof(Way));

            var pway = inspector.FindClassMappingForResource("way", "http://nu.nl/profile#street");
            Assert.IsNotNull(pway);
            Assert.AreEqual(pway.NativeType, typeof(ProfiledWay));

            var pway2 = inspector.FindClassMappingForResource("way", "http://nux.nl/profile#street");
            Assert.IsNotNull(pway2);
            Assert.AreEqual(pway2.NativeType, typeof(Way));

            var street = inspector.FindClassMappingForResource("Street");
            Assert.IsNotNull(street);
            Assert.AreEqual(street.NativeType, typeof(NewStreet));

            var noway = inspector.FindClassMappingForResource("nonexistent");
            Assert.IsNull(noway);
        }


        [TestMethod,Ignore]
        public void TypeDataTypeNameResolving()
        {
            var inspector = new ModelInspector();

            inspector.ImportType(typeof(AnimalName));
            inspector.ImportType(typeof(NewAnimalName));

            var result = inspector.FindClassMappingForFhirDataType("animalname");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.NativeType, typeof(NewAnimalName));

            // Validate a mapping for a type will return the newest registration
            result = inspector.FindClassMappingByType(typeof(AnimalName));
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(NewAnimalName),result.NativeType);
        }


        [TestMethod]
        public void TestEnumTypeResolving()
        {
            var inspector = new ModelInspector();

            inspector.ImportEnum(typeof(SomeEnum));
            inspector.ImportEnum(typeof(ActResource.SomeOtherEnum));

            var result = inspector.FindEnumMappingByType(typeof(SomeEnum));
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(SomeEnum), result.EnumType);

            result = inspector.FindEnumMappingByType(typeof(ActResource.SomeOtherEnum));
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ActResource.SomeOtherEnum), result.EnumType);
        }

        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector();

            // Inspect the HL7.Fhir.Model assembly
            inspector.Import(typeof(Resource).Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.FindClassMappingForResource("patient"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("HumanName"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("code"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("boolean"));

            // Verify presence of nested enumerations
            Assert.IsNotNull(inspector.FindEnumMappingByType(typeof(Address.AddressUse)));

            // Should have skipped abstract classes
            Assert.IsNull(inspector.FindClassMappingForResource("ComplexElement"));
            Assert.IsNull(inspector.FindClassMappingForResource("Element"));
            Assert.IsNull(inspector.FindClassMappingForResource("Resource"));
           
            // The open generic Code<> should not be there
            var codeOfT = inspector.FindClassMappingByType(typeof(Code<>));
            Assert.IsNull(codeOfT);
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
