using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class DefaultModelClassFactory : IModelClassFactory
    {
        public bool CanCreateType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            // Can create any type, as long as a public default constructor is present
            return ReflectionHelper.HasDefaultPublicConstructor(type);
        }

        public object Create(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return Activator.CreateInstance(type);
        }
    }
}
