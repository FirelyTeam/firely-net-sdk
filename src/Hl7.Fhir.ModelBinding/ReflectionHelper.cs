using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    internal static class ReflectionHelper
    {
        internal static IEnumerable<PropertyInfo> FindPublicProperties(Type t)
        {
            if(t == null) throw Error.ArgumentNull("t");

            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        internal static bool HasDefaultPublicConstructor(Type t)
        {
            if (t == null) throw Error.ArgumentNull("t");

            if (t.IsValueType)
                return true;

            return (GetDefaultPublicConstructor(t) != null);
        }

        internal static ConstructorInfo GetDefaultPublicConstructor(Type t)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            return t.GetConstructors(bindingFlags).SingleOrDefault(c => !c.GetParameters().Any());
        }

        public static bool IsNullableType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type GetNullableArgument(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            else
                throw Error.Argument("type", "Type {0} is not a Nullable<T>", type.Name);
        }

        public static bool IsTypedCollection(Type type)
        {
            return type.IsArray || ImplementsGenericDefinition(type, typeof(ICollection<>));
        }


        /// <summary>
        /// Gets the type of the typed collection's items.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type of the typed collection's items.</returns>
        public static Type GetCollectionItemType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            Type genericListType;

            if (type.IsArray)
            {
                return type.GetElementType();
            }
            else if (ImplementsGenericDefinition(type, typeof(ICollection<>), out genericListType))
            {
                //EK: If I look at ImplementsGenericDefinition, I don't think this can actually occur.
                //if (genericListType.IsGenericTypeDefinition)
                //throw Error.Argument("type", "Type {0} is not a collection.", type.Name);

                return genericListType.GetGenericArguments()[0];
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return null;
            }
            else
            {
                throw Error.Argument("type", "Type {0} is not a collection.", type.Name);
            }
        }

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
        {
            Type implementingType;
            return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
        }

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
        {
            if (type == null) throw Error.ArgumentNull("type");
            if (genericInterfaceDefinition == null) throw Error.ArgumentNull("genericInterfaceDefinition");

            if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
               throw Error.Argument("genericInterfaceDefinition", "'{0}' is not a generic interface definition.",genericInterfaceDefinition.Name);

            if (type.IsInterface)
            {
                if (type.IsGenericType)
                {
                    Type interfaceDefinition = type.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition)
                    {
                        implementingType = type;
                        return true;
                    }
                }
            }

            foreach (Type i in type.GetInterfaces())
            {
                if (i.IsGenericType)
                {
                    Type interfaceDefinition = i.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition)
                    {
                        implementingType = i;
                        return true;
                    }
                }
            }

            implementingType = null;
            return false;
        }

        internal static bool IsEnum(Type type)
        {
            return type.IsEnum;
        }

    }
}
