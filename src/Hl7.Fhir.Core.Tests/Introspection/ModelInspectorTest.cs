/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
	public class ModelInspectorTest
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


        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector();

            // Inspect the HL7.Fhir.Model assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.FindClassMappingForResource("patient"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("HumanName"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("code"));
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("boolean"));

            // Should also have found the abstract classes
            Assert.IsNotNull(inspector.FindClassMappingForFhirDataType("Element"));
            Assert.IsNotNull(inspector.FindClassMappingForResource("Resource"));
           
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
