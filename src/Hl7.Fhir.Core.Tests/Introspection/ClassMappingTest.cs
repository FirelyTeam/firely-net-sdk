/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
	public class ClassMappingTest
    {
        [TestMethod]
        public void TestResourceMappingCreation()
        {
            var mapping = ClassMapping.Create(typeof(Road));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.All, mapping.Version);
            Assert.AreEqual("Road", mapping.Name);
            Assert.AreEqual(typeof(Road), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(Way));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.All, mapping.Version);
            Assert.AreEqual("Way", mapping.Name);
            Assert.AreEqual(typeof(Way), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(ProfiledWay));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.All, mapping.Version);
            Assert.AreEqual("Way", mapping.Name);
            Assert.AreEqual(typeof(ProfiledWay), mapping.NativeType);
            Assert.IsNotNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(NewStreet));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.All, mapping.Version);
            Assert.AreEqual("Street", mapping.Name);
            Assert.AreEqual(typeof(NewStreet), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(LaneV2));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.DSTU2, mapping.Version);
            Assert.AreEqual("Lane", mapping.Name);
            Assert.AreEqual(typeof(LaneV2), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(LaneV3));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.STU3, mapping.Version);
            Assert.AreEqual("Lane", mapping.Name);
            Assert.AreEqual(typeof(LaneV3), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(LaneV4));
            Assert.IsTrue(mapping.IsResource);
            Assert.AreEqual(Fhir.Model.Version.R4, mapping.Version);
            Assert.AreEqual("Lane", mapping.Name);
            Assert.AreEqual(typeof(LaneV4), mapping.NativeType);
            Assert.IsNull(mapping.Profile);
        }


        [TestMethod]
        public void TestDatatypeMappingCreation()
        {
            var mapping = ClassMapping.Create(typeof(AnimalName));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(AnimalName), mapping.NativeType);
            Assert.IsNull(mapping.Profile);

            mapping = ClassMapping.Create(typeof(NewAnimalName));
            Assert.IsFalse(mapping.IsResource);
            Assert.AreEqual("AnimalName", mapping.Name);
            Assert.AreEqual(typeof(NewAnimalName), mapping.NativeType);
            Assert.IsNull(mapping.Profile);
        }


        [TestMethod]
        public void TestMixedDataTypeDetection()
        {
            Assert.ThrowsException<ArgumentException>(() => ClassMapping.Create(typeof(ComplexNumber)));
            // cannot have a datatype with a profile....
        }

   }


    /*
     * Resource classes for tests 
     */
    [FhirType(IsResource=true)]
    public class Road {}

    [FhirType]
    public class Way : Resource { public override IDeepCopyable DeepCopy() { throw new NotImplementedException(); } }
    
    [FhirType(Fhir.Model.Version.All, "Way", Profile="http://nu.nl/profile#street")]
    public class ProfiledWay : Resource { public override IDeepCopyable DeepCopy() { throw new NotImplementedException(); } }

    [FhirType(Fhir.Model.Version.All, "Street", IsResource=true)]
    public class NewStreet { }

    [FhirType(Fhir.Model.Version.DSTU2, "Lane", IsResource = true)]
    public class LaneV2 { }

    [FhirType(Fhir.Model.Version.STU3, "Lane", IsResource = true)]
    public class LaneV3 { }

    [FhirType(Fhir.Model.Version.R4, "Lane", IsResource = true)]
    public class LaneV4 { }

    /* 
     * Datatype classes for tests
     */
    [FhirType]
    public class AnimalName { }

    [FhirType(Fhir.Model.Version.All, "AnimalName")]
    public class NewAnimalName : AnimalName { }

    [FhirType(Fhir.Model.Version.All, "Complex", Profile = "http://hl7.org/profiles/test")]
    public class ComplexNumber { }
}
