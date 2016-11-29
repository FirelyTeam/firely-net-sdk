﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Support
{
    internal static class ReflectionHelper
    {
        public static bool CanBeTreatedAsType(this Type CurrentType, Type TypeToCompareWith)
        {
            // Always return false if either Type is null
            if (CurrentType == null || TypeToCompareWith == null)
                return false;
#if PORTABLE45 || NETCore
            // Return the result of the assignability test
            return TypeToCompareWith.GetTypeInfo().IsAssignableFrom(CurrentType.GetTypeInfo());
#else
            // Return the result of the assignability test
            return TypeToCompareWith.IsAssignableFrom(CurrentType);
#endif
        }


        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOnEnum<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
#if PORTABLE45 || NETCore
            var memInfo = type.GetTypeInfo().GetDeclaredField(enumVal.ToString());
#else
            var memInfo = type.GetField(enumVal.ToString());
#endif
            var attributes = memInfo.GetCustomAttributes(typeof(T), false);
            return (attributes.Count() > 0) ? (T)attributes.First() : null;
        }


        public static IEnumerable<PropertyInfo> FindPublicProperties(Type t)
        {
            if (t == null) throw Error.ArgumentNull("t");

#if PORTABLE45 || NETCore
            return t.GetRuntimeProperties(); //(BindingFlags.Instance | BindingFlags.Public);
            // return t.GetTypeInfo().DeclaredProperties.Union(t.GetTypeInfo().BaseType.GetTypeInfo().DeclaredProperties); //(BindingFlags.Instance | BindingFlags.Public);
#else
            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
#endif
        }

        public static PropertyInfo FindPublicProperty(Type t, string name)
        {
            if (t == null) throw Error.ArgumentNull("t");
            if (name == null) throw Error.ArgumentNull("name");

#if PORTABLE45 || NETCore
            return t.GetRuntimeProperty(name);
#else
            return t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
#endif
        }

        internal static MethodInfo FindPublicStaticMethod(Type t, string name, params Type[] arguments)
        {
            if (t == null) throw Error.ArgumentNull("t");
            if (name == null) throw Error.ArgumentNull("name");

#if PORTABLE45 || NETCore
            return t.GetRuntimeMethod(name,arguments);
#else
            return t.GetMethod(name, arguments);
#endif
        }

        internal static bool HasDefaultPublicConstructor(Type t)
        {
            if (t == null) throw Error.ArgumentNull("t");

#if PORTABLE45 || NETCore
            if (t.GetTypeInfo().IsValueType)
                return true;
#else
            if (t.IsValueType)
                return true;
#endif

            return (GetDefaultPublicConstructor(t) != null);
        }

        internal static ConstructorInfo GetDefaultPublicConstructor(Type t)
        {
#if PORTABLE45 || NETCore
            return t.GetTypeInfo().DeclaredConstructors.FirstOrDefault(s => s.GetParameters().Length == 0 && s.IsPublic && !s.IsStatic);
#else
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            return t.GetConstructors(bindingFlags).SingleOrDefault(c => !c.GetParameters().Any());
#endif
        }

        public static bool IsNullableType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

#if PORTABLE45 || NETCore
            return (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
#else
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
#endif
        }

        public static Type GetNullableArgument(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if (IsNullableType(type))
            {
#if PORTABLE45 || NETCore
                return type.GenericTypeArguments[0];
#else
                return type.GetGenericArguments()[0];
#endif
            }
            else
                throw Error.Argument("type", "Type {0} is not a Nullable<T>".FormatWith(type.Name));
        }

        public static bool IsTypedCollection(Type type)
        {
            return type.IsArray || ImplementsGenericDefinition(type, typeof(ICollection<>));
        }


        public static IList CreateGenericList(Type itemType)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
        }


        public static bool IsClosedGenericType(Type type)
        {
#if PORTABLE45 || NETCore
            return type.GetTypeInfo().IsGenericType && !type.GetTypeInfo().ContainsGenericParameters;
#else
            return type.IsGenericType && !type.ContainsGenericParameters;
#endif
        }


        public static bool IsOpenGenericTypeDefinition(Type type)
        {
#if PORTABLE45 || NETCore
            return type.GetTypeInfo().IsGenericTypeDefinition;
#else
            return type.IsGenericTypeDefinition;
#endif
        }

        public static bool IsConstructedFromGenericTypeDefinition(Type type, Type genericBase)
        {
            return type.GetGenericTypeDefinition() == genericBase;
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

#if PORTABLE45 || NETCore
                return genericListType.GetTypeInfo().GenericTypeArguments[0];
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
#else
                return genericListType.GetGenericArguments()[0];
                  }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
#endif

                return null;
            }
            else
            {
                throw Error.Argument("type", "Type {0} is not a collection.".FormatWith(type.Name));
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

#if PORTABLE45 || NETCore
            if (!genericInterfaceDefinition.GetTypeInfo().IsInterface || !genericInterfaceDefinition.GetTypeInfo().IsGenericTypeDefinition)
#else
            if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
#endif
                throw Error.Argument("genericInterfaceDefinition", "'{0}' is not a generic interface definition.".FormatWith(genericInterfaceDefinition.Name));

#if PORTABLE45 || NETCore
            if (type.GetTypeInfo().IsInterface)
#else
            if (type.IsInterface)
#endif
            {
#if PORTABLE45 || NETCore
                if (type.GetTypeInfo().IsGenericType)
#else
                if (type.IsGenericType)
#endif
                {
                    Type interfaceDefinition = type.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition)
                    {
                        implementingType = type;
                        return true;
                    }
                }
            }

#if PORTABLE45 || NETCore
            foreach (Type i in type.GetTypeInfo().ImplementedInterfaces)
#else
            foreach (Type i in type.GetInterfaces())
#endif
            {
#if PORTABLE45 || NETCore
                if (i.GetTypeInfo().IsGenericType)
#else
                if (i.IsGenericType)
#endif
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

#region << Extension methods to make the handling of PCL easier >>

#if PORTABLE45
		internal static bool IsDefined(this Type t, Type attributeType, bool inherit)
		{
			return t.GetTypeInfo().IsDefined(attributeType, inherit);
		}
#endif

#if PORTABLE45 && !DOTNET
        internal static bool IsAssignableFrom(this Type t, Type otherType)
		{
			return t.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
		}
#endif

        internal static bool IsEnum(this Type t)
        {
#if PORTABLE45 || NETCore
            return t.GetTypeInfo().IsEnum;
#else
            return t.IsEnum;
#endif
        }
#endregion

#if PORTABLE45
		internal static T GetAttribute<T>(Type type) where T : Attribute
		{
			var attr = type.GetTypeInfo().GetCustomAttribute<T>();
			return (T)attr;
		}
#endif

        internal static T GetAttribute<T>(MemberInfo member) where T : Attribute
        {
#if PORTABLE45 || NETCore
            var attr = member.GetCustomAttribute<T>();
#else
            var attr = Attribute.GetCustomAttribute(member, typeof(T));
#endif
            return (T)attr;
        }
#if NETCore
        internal static T GetAttribute<T>(Type member) where T : Attribute
        {

            var attr = member.GetTypeInfo().GetCustomAttribute<T>();
            return (T)attr;
        }
#endif

        internal static ICollection<T> GetAttributes<T>(MemberInfo member) where T : Attribute
        {
#if PORTABLE45 || NETCore
            var attr = member.GetCustomAttributes<T>();
#else
            var attr = Attribute.GetCustomAttributes(member, typeof(T));
#endif
            return (ICollection<T>)attr.Select(a => (T)a);
        }


        internal static IEnumerable<FieldInfo> FindEnumFields(Type t)
        {
            if (t == null) throw Error.ArgumentNull("t");

#if PORTABLE45 || NETCore
            return t.GetTypeInfo().DeclaredFields.Where(a => a.IsPublic && a.IsStatic);
#else
            return t.GetFields(BindingFlags.Public | BindingFlags.Static);
#endif
        }

        internal static bool IsArray(object value)
        {
            if (value == null) throw Error.ArgumentNull("value");

            return value.GetType().IsArray;
        }

        public static string PrettyTypeName(Type t)
        {
            // http://stackoverflow.com/questions/1533115/get-generictype-name-in-good-format-using-reflection-on-c-sharp#answer-25287378 
#if PORTABLE45 || NETCore
            return t.GetTypeInfo().IsGenericType ? string.Format( 
                "{0}<{1}>", t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.CurrentCulture)), 
                string.Join(", ", t.GetTypeInfo().GenericTypeParameters.ToList().Select(PrettyTypeName))) 
#else
            return t.IsGenericType ? string.Format(
                "{0}<{1}>", t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture)),
                string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName)))
#endif
            : t.Name;
        }
    }
}
