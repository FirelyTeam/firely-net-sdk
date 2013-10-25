using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public static class BindingConfiguration
    {
        private static IList<IModelClassFactory> _modelClassFactories = new List<IModelClassFactory>() { new DefaultModelClassFactory() };

        public static IList<IModelClassFactory> ModelClassFactories
        {
            get { return _modelClassFactories; }            
        }

        public static IModelClassFactory FindFactory(this IList<IModelClassFactory> list, Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return list.First(fac => fac.CanCreateType(type));
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
