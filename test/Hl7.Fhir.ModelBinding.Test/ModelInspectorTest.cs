using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.ModelBinding.Test
{
    [TestClass]
    public class ModelInspectorTest
    {
        [TestMethod]
        public void TestResourceClassInspection()
        {
            var inspector = new ModelInspector();

            inspector.Inspect(typeof(Street));
            inspector.Inspect(typeof(RoadResource));
            inspector.Inspect(typeof(Way));
            inspector.Inspect(typeof(ProfiledWay));
            inspector.Inspect(typeof(NewStreet));

            inspector.Inspect(typeof(ModelInspectorTest));  // shouldn't give an exception

            var road = inspector.GetMappedClassForResource("roAd");
            Assert.IsNotNull(road);
            Assert.AreEqual(FhirModelConstruct.Resource, road.ModelConstruct);
            Assert.AreEqual("Road", road.Name);
            Assert.IsNull(road.Profile);
            Assert.AreEqual(road.ImplementingType, typeof(RoadResource));

            var way = inspector.GetMappedClassForResource("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual("Way", way.Name);
            Assert.IsNull(way.Profile);
            Assert.AreEqual(way.ImplementingType, typeof(Way));

            var pway = inspector.GetMappedClassForResource("way", "http://nu.nl/profile#street");
            Assert.IsNotNull(pway);
            Assert.AreEqual("Way", pway.Name);
            Assert.AreEqual("http://nu.nl/profile#street", pway.Profile);
            Assert.AreEqual(pway.ImplementingType, typeof(ProfiledWay));

            var pway2 = inspector.GetMappedClassForResource("way", "http://nux.nl/profile#street");
            Assert.IsNotNull(pway2);
            Assert.AreEqual("Way", pway2.Name);
            Assert.IsNull(pway2.Profile);
            Assert.AreEqual(pway2.ImplementingType, typeof(Way));

            var street = inspector.GetMappedClassForResource("Street");
            Assert.IsNotNull(street);
            Assert.AreEqual("Street", street.Name);
            Assert.IsNull(street.Profile);
            Assert.AreEqual(street.ImplementingType, typeof(NewStreet));

            var noway = inspector.GetMappedClassForResource("nonexistent");
            Assert.IsNull(noway);
        }


        [TestMethod]
        public void TestDataTypeInspection()
        {
            var inspector = new ModelInspector();

            inspector.Inspect(typeof(AnimalName));
            inspector.Inspect(typeof(NewAnimalName));
            inspector.Inspect(typeof(ComplexNumber));
            inspector.Inspect(typeof(SomeEnum));
            inspector.Inspect(typeof(ActResource.SomeOtherEnum));

            var result = inspector.GetMappedClassForDataType("animalname");
            Assert.IsNotNull(result);
            Assert.AreEqual(FhirModelConstruct.ComplexType, result.ModelConstruct);
            Assert.AreEqual("AnimalName", result.Name);
            Assert.IsNull(result.Profile);
            Assert.AreEqual(result.ImplementingType, typeof(NewAnimalName));

            result = inspector.GetMappedClassForDataType("cOmpleX");
            Assert.IsNotNull(result);
            Assert.AreEqual(FhirModelConstruct.PrimitiveType, result.ModelConstruct);
            Assert.AreEqual("Complex", result.Name);
            Assert.IsNull(result.Profile);
            Assert.AreEqual(result.ImplementingType, typeof(ComplexNumber));

            result = inspector.GetMappedClassForDataType("SomeEnum");
            Assert.IsNotNull(result);
            Assert.AreEqual(FhirModelConstruct.PrimitiveType, result.ModelConstruct);
            Assert.AreEqual("SomeEnum", result.Name);
            Assert.IsNull(result.Profile);
            Assert.AreEqual(result.ImplementingType, typeof(SomeEnum));

            result = inspector.GetMappedClassForDataType("someOtherenum");
            Assert.IsNotNull(result);
            Assert.AreEqual(FhirModelConstruct.PrimitiveType, result.ModelConstruct);
            Assert.AreEqual("SomeOtherEnum", result.Name);
            Assert.IsNull(result.Profile);
            Assert.AreEqual(result.ImplementingType, typeof(ActResource.SomeOtherEnum));
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMixedDataTypeDetection()
        {
            var inspector = new ModelInspector();

            inspector.Inspect(typeof(Chameleon));
        }

        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector();

            // Inspect the HL7.Fhir.Model assembly
            inspector.Inspect(typeof(Resource).Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.GetMappedClassForResource("patient"));
            Assert.IsNotNull(inspector.GetMappedClassForDataType("HumanName"));
            Assert.IsNotNull(inspector.GetMappedClassForDataType("code"));
            Assert.IsNotNull(inspector.GetMappedClassForDataType("boolean"));

            // Verify presence of nested enumerations
            Assert.IsNotNull(inspector.GetMappedClassForDataType("AddressUse"));

            // Should have skipped abstract classes
            Assert.IsNull(inspector.GetMappedClassForResource("Resource"));
            Assert.IsNull(inspector.GetMappedClassForResource("Element"));
        }
    }

    /*
     * Resource classes for tests 
     */
    public class RoadResource {}
    public class Way : Resource {}
    
    [FhirResource("Way", Profile="http://nu.nl/profile#street")]
    public class ProfiledWay {}

    public class Street : Resource {}

    [FhirResource("Street")]
    public class NewStreet : Resource { }


    /* 
     * Datatype classes for tests
     */
    public class AnimalName : ComplexElement { }

    [FhirComplexType("AnimalName")]
    public class NewAnimalName { }

    [FhirPrimitiveType("Complex")]
    public class ComplexNumber : PrimitiveElement { }

    [FhirComplexType("Chameleon")]
    public class Chameleon : PrimitiveElement { }


    [FhirEnumeration("SomeEnum")]
    public enum SomeEnum { Member, AnotherMember }

    public class ActResource
    {
        [FhirEnumeration("SomeOtherEnum")]
        public enum SomeOtherEnum { Member, AnotherMember }
    }
}
