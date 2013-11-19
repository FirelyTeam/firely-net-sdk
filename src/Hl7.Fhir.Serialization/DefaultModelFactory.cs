using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class DefaultModelFactory : IModelClassFactory
    {
        private ModelInspector _inspector;

        public DefaultModelFactory(ModelInspector inspector)
        {
            _inspector = inspector;
        }

        public DefaultModelFactory()
        {
            _inspector = null;
        }

        //private Type findTypeSubstitution(Type type)
        //{
        //    if (_inspector == null) return type;

        //    var mapping = _inspector.FindClassMappingByType(type);

        //    // The given type has no type mapping, so there is no type substitution for it
        //    if (mapping == null) return type;

        //    var name = mapping.Name;

        //    if (mapping.IsResource)
        //        mapping = _inspector.FindClassMappingForResource(name);
        //    else
        //        mapping = _inspector.FindClassMappingForFhirDataType(name);

        //    // Return the mapping found, unless there's no mapping, then return the original type
        //    return mapping.NativeType ?? type;
        //}


        public bool CanCreate(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            var typeToCreate = type;
//            var typeToCreate = findTypeSubstitution(type);

            // Can create any type, as long as a public default constructor is present
            var canCreate = ReflectionHelper.HasDefaultPublicConstructor(typeToCreate) ||
                        (ReflectionHelper.IsTypedCollection(typeToCreate) && !type.IsArray);

            return canCreate;
        }


        public object Create(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

           // var typeToCreate = findTypeSubstitution(type);
            var typeToCreate = type;

            // For nullable types, create an instance of the type that
            // is made nullable, since otherwise you'll create an (untyped) null
            if (ReflectionHelper.IsNullableType(typeToCreate))
                typeToCreate = ReflectionHelper.GetNullableArgument(typeToCreate);

            // If type is a typed collection (but not an array), and the type
            // is not a concrete collection type, but an interface, create a new List of
            // the given type.
            if(ReflectionHelper.IsTypedCollection(typeToCreate) && !typeToCreate.IsArray && typeToCreate.IsInterface)
            {
                var elementType = ReflectionHelper.GetCollectionItemType(typeToCreate);
                typeToCreate = typeof(List<>).MakeGenericType(elementType);
            }
                             
            return Activator.CreateInstance(typeToCreate);
        }
    }
}
