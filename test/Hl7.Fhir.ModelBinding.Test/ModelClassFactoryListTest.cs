using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.ModelBinding.Test
{
   
    [TestClass]
    public class ClassFactoryListTest
    {
        [TestMethod]
        public void FindModelClassFactory()
        {
            BindingConfiguration.ModelClassFactories.Clear();

            var specificFactory = new SpecificModelClassFactory();
            BindingConfiguration.ModelClassFactories.Add(specificFactory);
            var defaultFactory = new DefaultModelFactory();
            BindingConfiguration.ModelClassFactories.Add(defaultFactory);

            var selectedFactory = BindingConfiguration.ModelClassFactories.FindFactory(typeof(SpecificModelClass));
            Assert.AreEqual(specificFactory, selectedFactory);

            selectedFactory = BindingConfiguration.ModelClassFactories.FindFactory(typeof(GenericModelClass));
            Assert.AreEqual(defaultFactory, selectedFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWhenNoFactoryFound()
        {
            BindingConfiguration.ModelClassFactories.Clear();

            BindingConfiguration.ModelClassFactories.Add(new SpecificModelClassFactory());

            var result = BindingConfiguration.ModelClassFactories.FindFactory(typeof(GenericModelClass));
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
        public bool CanCreateType(Type type)
        {
            return type == typeof(SpecificModelClass);
        }

        public object Create(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
