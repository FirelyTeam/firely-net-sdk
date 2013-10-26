using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class DefaultModelFactory : IModelClassFactory
    {
        public bool CanCreateType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            // Can create any type, as long as a public default constructor is present
            var canCreate = ReflectionHelper.HasDefaultPublicConstructor(type) ||
                        (ReflectionHelper.IsTypedCollection(type) && !type.IsArray);

            return canCreate;
        }

        public object Create(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            var typeToCreate = type;

            // For nullable types, create an instance of the type that
            // is made nullable, since otherwise you'll create an (untyped) null
            if (ReflectionHelper.IsNullableType(type))
                typeToCreate = ReflectionHelper.GetNullableArgument(type);

            // If type is a typed collection (but not an array), and the type
            // is not a concrete collection type, but an interface, create a new List of
            // the given type.
            if(ReflectionHelper.IsTypedCollection(type) && !type.IsArray && type.IsInterface)
            {
                var elementType = ReflectionHelper.GetCollectionItemType(type);
                typeToCreate = typeof(List<>).MakeGenericType(elementType);
            }
                             
            return Activator.CreateInstance(typeToCreate);
        }
    }
}
