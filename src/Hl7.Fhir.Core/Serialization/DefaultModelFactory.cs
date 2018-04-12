/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class DefaultModelFactory : IModelClassFactory
    {
        public DefaultModelFactory()
        {
        }

     
        public bool CanCreate(Type type)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));

            // Can create any type, as long as a public default constructor is present
            var canCreate = ReflectionHelper.HasDefaultPublicConstructor(type) ||
                        (ReflectionHelper.IsTypedCollection(type) && !type.IsArray);

            return canCreate;
        }


        public object Create(Type type)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));
            //   if (!type.CanBeTreatedAsType(typeof(Base))) throw Error.Argument(nameof(type), "type argument must be a subclass of Base");

            // var typeToCreate = findTypeSubstitution(type);
            var typeToCreate = type;

            // For nullable types, create an instance of the type that
            // is made nullable, since otherwise you'll create an (untyped) null
            if (ReflectionHelper.IsNullableType(typeToCreate))
                typeToCreate = ReflectionHelper.GetNullableArgument(typeToCreate);

            // If type is a typed collection (but not an array), and the type
            // is not a concrete collection type, but an interface, create a new List of
            // the given type.
            if (ReflectionHelper.IsTypedCollection(typeToCreate) && !typeToCreate.IsArray && typeToCreate.GetTypeInfo().IsInterface)
			{
                var elementType = ReflectionHelper.GetCollectionItemType(typeToCreate);
                typeToCreate = typeof(List<>).MakeGenericType(elementType);
            }
                             
            return Activator.CreateInstance(typeToCreate);
        }
    }
}
