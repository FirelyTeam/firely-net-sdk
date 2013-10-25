using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.ModelBinding.Test
{
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

    [TestClass]
    public class DefaultModelClassFactoryTest
    {    
        [TestMethod]
        public void CanCreateClassWithDefaultConstructor()
        {
            var factory = new DefaultModelClassFactory();

            Assert.IsTrue(factory.CanCreateType(typeof(TestCreate)));
            Assert.IsNotNull(factory.Create(typeof(TestCreate)));
            Assert.IsTrue(factory.CanCreateType(typeof(TestCreatePublicConstructor)));
            Assert.IsNotNull(factory.Create(typeof(TestCreatePublicConstructor)));
        }

        [TestMethod]
        public void CannotCreateClassWithoutPublicConstructor()
        {
            var factory = new DefaultModelClassFactory();

            Assert.IsFalse(factory.CanCreateType(typeof(TestCreatePrivateConstructor)));
            Assert.IsFalse(factory.CanCreateType(typeof(TestCreateArgConstructor)));
            Assert.IsFalse(factory.CanCreateType(typeof(IList<string>)));

        }
    }
}
