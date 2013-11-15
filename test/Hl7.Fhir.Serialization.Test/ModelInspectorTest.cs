using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization.Test
{
    [TestClass]
    public class ModelInspectorTest
    {
        [TestMethod]
        public void TestResourceNameResolving()
        {
            var inspector = new ModelInspector();

            inspector.Import(typeof(Road));
            inspector.Import(typeof(Way));
            inspector.Import(typeof(ProfiledWay));
            inspector.Import(typeof(NewStreet));
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


        [TestMethod]
        public void TypeDataTypeNameResolving()
        {
            var inspector = new ModelInspector();

            inspector.Import(typeof(AnimalName));
            inspector.Import(typeof(NewAnimalName));

            var result = inspector.FindClassMappingForFhirDataType("animalname");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.NativeType, typeof(NewAnimalName));
        }


        [TestMethod]
        public void TestEnumTypeResolving()
        {
            var inspector = new ModelInspector();

            inspector.Import(typeof(SomeEnum));
            inspector.Import(typeof(ActResource.SomeOtherEnum));

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
