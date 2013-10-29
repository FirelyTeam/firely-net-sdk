using Hl7.Fhir.ModelBinding.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public static class BindingConfiguration
    {
        private static Lazy<IList<IModelClassFactory>> _modelClassFactories = createDefaultClassFactoryList();

        private static Lazy<IList<IModelClassFactory>> createDefaultClassFactoryList()
        {
            return new Lazy<IList<IModelClassFactory>>(() =>
                new List<IModelClassFactory> { new DefaultModelFactory() });
        }

        public static IList<IModelClassFactory> ModelClassFactories
        {
            get { return _modelClassFactories.Value; }            
        }

        public static IModelClassFactory FindFactory(this IList<IModelClassFactory> list, Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return list.First(fac => fac.CanCreateType(type));
        }

        public static object InvokeFactory(this IList<IModelClassFactory> list, Type type)
        {
            var chosenFactory = list != null ? list.FindFactory(type) : null;
            if (type == null) throw Error.ArgumentNull("type");
            if (chosenFactory == null) throw Error.InvalidOperation(Messages.NoSuchClassFactory, type.Name);

            object result = chosenFactory.Create(type);
            if (result == null) throw Error.InvalidOperation(Messages.FactoryCreationFailed);

            return result;
        }

        private static Lazy<IList<Assembly>> _modelAssemblies = createModelAssemblyList();

        private static Lazy<IList<Assembly>> createModelAssemblyList()
        {
            //TODO: Probably at least try to locate the default FHIR model dll
            return new Lazy<IList<Assembly>>(() => new List<Assembly>());
        }

        public static IList<Assembly> ModelAssemblies
        {
            get { return _modelAssemblies.Value; }
        }



        private static bool _acceptUnknownMembers = false;

        public static bool AcceptUnknownMembers
        {
            get { return _acceptUnknownMembers; }
            set { _acceptUnknownMembers = value; }
        }
    }


    public interface IModelClassFactory
    {
        bool CanCreateType(Type type);
        object Create(Type type);
    }
}
