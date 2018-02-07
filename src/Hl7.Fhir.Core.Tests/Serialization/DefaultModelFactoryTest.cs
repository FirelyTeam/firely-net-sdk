/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Collections;
using Hl7.Fhir.Support;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
	public class DefaultModelFactoryTest
    {    
        [TestMethod]
        public void TestSupportedTypes()
        {
            var factory = new DefaultModelFactory();

            // Test creation of a class with no constructor
            Assert.IsTrue(factory.CanCreate(typeof(TestCreate)));
            Assert.IsNotNull(factory.Create(typeof(TestCreate)));
            
            // Test creation of class with a public no-args constructor
            Assert.IsTrue(factory.CanCreate(typeof(TestCreatePublicConstructor)));
            Assert.IsNotNull(factory.Create(typeof(TestCreatePublicConstructor)));
            
            // Test creation of primitives
            Assert.IsTrue(factory.CanCreate(typeof(int)));
            Assert.IsNotNull(factory.Create(typeof(int)));

            // Test creation of Nullable<T>
            Assert.IsTrue(factory.CanCreate(typeof(int?)));
            Assert.IsNotNull(factory.Create(typeof(int?)));

            // Test handling of collection interfaces
            object collection = null;
            Assert.IsTrue(factory.CanCreate(typeof(ICollection<string>)));
            collection = factory.Create(typeof(ICollection<string>));
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection is List<string>);

            Assert.IsTrue(factory.CanCreate(typeof(IList<HumanName>)));
            Assert.IsNotNull(factory.Create(typeof(ICollection<HumanName>)));
            
            Assert.IsTrue(factory.CanCreate(typeof(IList<int?>)));
            collection = factory.Create(typeof(ICollection<int?>));
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection is List<int?>);

            // Test handling of closed generics
            Assert.IsTrue(factory.CanCreate(typeof(Code<Address.AddressUse>)));
            Assert.IsNotNull(factory.Create(typeof(Code<Address.AddressUse>)));
        }

        [TestMethod]
        public void TestUnsupportedTypes()
        {
            var factory = new DefaultModelFactory();

            Assert.IsFalse(factory.CanCreate(typeof(TestCreatePrivateConstructor)));
            Assert.IsFalse(factory.CanCreate(typeof(TestCreateArgConstructor)));

            // Cannot create interface types
            Assert.IsFalse(factory.CanCreate(typeof(IEnumerable)));

            // Cannot create arrays, since we don't know size upfront
            Assert.IsFalse(factory.CanCreate(typeof(int[])));
        }
    }

    [FhirType("Patient")]   // implicitly, this is a resource
    public class NewPatient : Patient { }

    public class TestCreate 
    {
    }

    public class TestCreatePublicConstructor 
    {
        public TestCreatePublicConstructor()
        {
        }
    }

    public class TestCreateArgConstructor
    {
        public TestCreateArgConstructor(int test)
        {
        }
    }

    public class TestCreatePrivateConstructor
    {
        private TestCreatePrivateConstructor() { }
    }
}
