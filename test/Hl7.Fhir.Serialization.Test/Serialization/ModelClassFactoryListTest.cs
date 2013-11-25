using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Test.Serialization
{
   
    [TestClass]
    public class ClassFactoryListTest
    {
        [TestMethod]
        public void FindModelClassFactory()
        {
            SerializationConfig.ModelClassFactories.Clear();

            var specificFactory = new SpecificModelClassFactory();
            SerializationConfig.ModelClassFactories.Add(specificFactory);
            var defaultFactory = new DefaultModelFactory();
            SerializationConfig.ModelClassFactories.Add(defaultFactory);

            var selectedFactory = SerializationConfig.ModelClassFactories.FindFactory(typeof(SpecificModelClass));
            Assert.AreEqual(specificFactory, selectedFactory);

            selectedFactory = SerializationConfig.ModelClassFactories.FindFactory(typeof(GenericModelClass));
            Assert.AreEqual(defaultFactory, selectedFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWhenNoFactoryFound()
        {
            SerializationConfig.ModelClassFactories.Clear();

            SerializationConfig.ModelClassFactories.Add(new SpecificModelClassFactory());

            var result = SerializationConfig.ModelClassFactories.FindFactory(typeof(GenericModelClass));
        }
    }


    public class SpecificModelClass
    {
    }

    public class GenericModelClass
    {
    }

    public class SpecificModelClassFactory : IModelClassFactory
    {
        public bool CanCreate(Type type)
        {
            return type == typeof(SpecificModelClass);
        }

        public object Create(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
