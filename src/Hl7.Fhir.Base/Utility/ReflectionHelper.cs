/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Utility
{
    public static class ReflectionHelper
    {
        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static bool IsAValueType(this Type t) => t.GetTypeInfo().IsValueType;

        /// <summary>
        /// Determines whether the specified type is a subclass of the type in <paramref name="typeToCompareWith"/>.
        /// </summary>
        /// <remarks>This function simply inverts the arguments for <see cref="Type.IsAssignableFrom(Type)"/>
        /// for better readability.</remarks>
        public static bool CanBeTreatedAsType(this Type currentType, Type typeToCompareWith) =>
            typeToCompareWith.IsAssignableFrom(currentType);

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T? GetAttributeOnEnum<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetTypeInfo().GetDeclaredField(enumVal.ToString())!;
            var attributes = memInfo.GetCustomAttributes(typeof(T), false);

            return (T?)attributes.FirstOrDefault();
        }

        public static PropertyInfo? FindProperty(Type t, string name) =>
            t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        /// <summary>
        /// Returns all public, non-static properties for the given type.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> FindPublicProperties(Type t)
        {
            if (t == null) throw Error.ArgumentNull("t");

            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// Returns true if the type is a primitive value type that has a parameterless public constructor.
        /// </summary>
        public static bool HasDefaultPublicConstructor(Type t) =>
            t.IsValueType || GetDefaultPublicConstructor(t) != null;

        internal static ConstructorInfo? GetDefaultPublicConstructor(Type t)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            return t.GetConstructors(bindingFlags).SingleOrDefault(c => !c.GetParameters().Any());
        }

        /// <summary>
        /// Returns true if the type is a <see cref="Nullable{T}"/>.
        /// </summary>
        public static bool IsNullableType(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        /// <summary>
        /// If the given type is a <see cref="Nullable{T}"/>, this function will return T.
        /// </summary>
        public static Type GetNullableArgument(Type type) =>
            IsNullableType(type)
                ? type.GenericTypeArguments[0]
                : throw Error.Argument("type", $"Type {type.Namespace} is not a Nullable<T>");

        /// <summary>
        /// Returns true if the given type is a .NET2.0+ typed collection.
        /// </summary>
        public static bool IsTypedCollection(Type type) =>
            type.IsArray || ImplementsGenericDefinition(type, typeof(ICollection<>));


        public static IList CreateGenericList(Type itemType) =>
            (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType))!;


        public static bool IsClosedGenericType(Type type) =>
            type.IsGenericType && !type.GetTypeInfo().ContainsGenericParameters;

        public static bool IsOpenGenericTypeDefinition(Type type) => type.IsGenericTypeDefinition;

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static bool IsConstructedFromGenericTypeDefinition(Type type, Type genericBase) =>
            type.GetGenericTypeDefinition() == genericBase;

        /// <summary>
        /// Gets the type of the typed collection's items.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type of the typed collection's items.</returns>
        public static Type? GetCollectionItemType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if (type.IsArray)
            {
                return type.GetElementType();
            }
            else if (ImplementsGenericDefinition(type, typeof(ICollection<>), out var genericListType))
            {
                return genericListType.GenericTypeArguments[0];
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return null;
            }
            else
            {
                throw Error.Argument("type", $"Type {type.Name} is not a collection.");
            }
        }

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition) =>
            ImplementsGenericDefinition(type, genericInterfaceDefinition, out _);

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, [NotNullWhen(true)] out Type? implementingType)
        {
            if (type == null) throw Error.ArgumentNull("type");
            if (genericInterfaceDefinition == null) throw Error.ArgumentNull("genericInterfaceDefinition");

            if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
                throw Error.Argument("genericInterfaceDefinition", "'{0}' is not a generic interface definition.".FormatWith(genericInterfaceDefinition.Name));

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

            foreach (Type i in type.GetTypeInfo().ImplementedInterfaces)
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

        public static bool IsEnum(this Type t) => t.GetTypeInfo().IsEnum;


        internal static T? GetAttribute<T>(MemberInfo t, FhirRelease version) where T : Attribute => GetAttributes<T>(t, version).LastOrDefault();

        internal static IEnumerable<T> GetAttributes<T>(MemberInfo t, FhirRelease version) where T : Attribute
        {
            return t.GetCustomAttributes<T>().Where(isRelevant);

            bool isRelevant(Attribute a) => a is not IFhirVersionDependent || a.AppliesToRelease(version);
        }

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static T? GetAttribute<T>(MemberInfo member) where T : Attribute => member.GetCustomAttribute<T>();

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static IEnumerable<Attribute> GetAttributes(MemberInfo member) => member.GetCustomAttributes();

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static IEnumerable<Attribute> GetAttributes(Type type) => type.GetCustomAttributes();

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static IEnumerable<T> GetAttributes<T>(MemberInfo member) where T : Attribute
            => member.GetCustomAttributes<T>();

        internal static IEnumerable<FieldInfo> FindEnumFields(Type t) =>
            t.GetTypeInfo().DeclaredFields.Where(a => a is { IsPublic: true, IsStatic: true });

        public static bool IsRepeatingElement(object value) => IsRepeatingElement(value, out _);

        public static bool IsRepeatingElement(object value, [NotNullWhen(true)] out ICollection? element)
        {
            element = value as ICollection;
            return element is not null && !element.GetType().IsArray;
        }

        [Obsolete("This helper method is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public static bool IsArray(object value) => value.GetType().IsArray;

        public static string PrettyTypeName(Type t)
        {
            // http://stackoverflow.com/questions/1533115/get-generictype-name-in-good-format-using-reflection-on-c-sharp#answer-25287378 
            return t.GetTypeInfo().IsGenericType ?
                $"{t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.CurrentCulture))}<{string.Join(", ", t.GetTypeInfo().GenericTypeParameters.ToList().Select(PrettyTypeName))}>"
                : t.Name;
        }

        public static string GetProductVersion(Assembly a)
        {
            var versionInfo = a.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var cleanedInformationalVersion = new string(versionInfo!.InformationalVersion.TakeWhile(c => c != '+').ToArray());

            return cleanedInformationalVersion;
        }
    }
}