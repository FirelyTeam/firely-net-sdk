using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Collections;

namespace Hl7.Fhir.Serialization.Test
{
    [TestClass]
    public class DefaultModelFactoryTest
    {    
        [TestMethod]
        public void TestSupportedTypes()
        {
            var factory = new DefaultModelFactory();

            // Test creation of a class with no constructor
            Assert.IsTrue(factory.CanCreateType(typeof(TestCreate)));
            Assert.IsNotNull(factory.Create(typeof(TestCreate)));
            
            // Test creation of class with a public no-args constructor
            Assert.IsTrue(factory.CanCreateType(typeof(TestCreatePublicConstructor)));
            Assert.IsNotNull(factory.Create(typeof(TestCreatePublicConstructor)));
            
            // Test creation of primitives
            Assert.IsTrue(factory.CanCreateType(typeof(int)));
            Assert.IsNotNull(factory.Create(typeof(int)));

            // Test creation of Nullable<T>
            Assert.IsTrue(factory.CanCreateType(typeof(int?)));
            Assert.IsNotNull(factory.Create(typeof(int?)));

            // Test handling of collection interfaces
            object collection = null;
            Assert.IsTrue(factory.CanCreateType(typeof(ICollection<string>)));
            collection = factory.Create(typeof(ICollection<string>));
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection is List<string>);

            Assert.IsTrue(factory.CanCreateType(typeof(IList<HumanName>)));
            Assert.IsNotNull(factory.Create(typeof(ICollection<HumanName>)));
            
            Assert.IsTrue(factory.CanCreateType(typeof(IList<int?>)));
            collection = factory.Create(typeof(ICollection<int?>));
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection is List<int?>);
        }

        [TestMethod]
        public void TestUnsupportedTypes()
        {
            var factory = new DefaultModelFactory();

            Assert.IsFalse(factory.CanCreateType(typeof(TestCreatePrivateConstructor)));
            Assert.IsFalse(factory.CanCreateType(typeof(TestCreateArgConstructor)));

            // Cannot create interface types
            Assert.IsFalse(factory.CanCreateType(typeof(ICloneable)));

            // Cannot create arrays, since we don't know size upfront
            Assert.IsFalse(factory.CanCreateType(typeof(int[])));
        }
    }

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
