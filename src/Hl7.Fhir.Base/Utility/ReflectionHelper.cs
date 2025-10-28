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

namespace Hl7.Fhir.Utility;

/// <summary>
/// A set of helper methods to make working with the FHIR reflection model metadata easier.
/// </summary>
internal static class ReflectionHelper
{
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
        var memInfo = type.GetTypeInfo().GetDeclaredField(enumVal.ToString());
        var attributes = memInfo?.GetCustomAttributes(typeof(T), false);

        return (T?)attributes?.FirstOrDefault();
    }

    public static PropertyInfo? FindProperty(Type t, string name) =>
        t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    /// <summary>
    /// Returns all public, non-static properties for the given type.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static PropertyInfo[] FindPublicProperties(Type t) =>
        t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

    public static bool IsClosedGenericType(Type type) =>
        type is { IsGenericType: true, ContainsGenericParameters: false };

    /// <summary>
    /// Gets the type of the typed collection's items.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The type of the typed collection's items.</returns>
    public static Type GetRepeatingElementType(Type type) =>
        TryGetRepeatingElementType(type, out var itemType) ? itemType :
            throw Error.Argument("type", $"Type {type.Name} is not a typed collection.");

    public static bool TryGetRepeatingElementType(Type type, [NotNullWhen(true)] out Type? itemType)
    {
        if (type.IsArray)
        {
            itemType = null;
            return false;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            itemType = type.GenericTypeArguments[0];
            return true;
        }

        itemType = null;
        return false;
    }

    internal static IEnumerable<FieldInfo> FindEnumFields(Type t) =>
        t.GetTypeInfo().DeclaredFields.Where(a => a is { IsPublic: true, IsStatic: true });

    public static bool IsRepeatingElement(object? value, [NotNullWhen(true)] out ICollection? element)
    {
        element = value as ICollection;
        return element is not null && !element.GetType().IsArray;
    }

    public static string GetProductVersion(Assembly a)
    {
        var versionInfo = a.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var cleanedInformationalVersion = new string(versionInfo!.InformationalVersion.TakeWhile(c => c != '+').ToArray());

        return cleanedInformationalVersion;
    }

    /// <summary>
    /// Gets an attribute of <typeparamref name="T"/> or subclasses on a given member that is relevant for the
    /// given <paramref name="version" />. Returns the last one if there are multiple matching attributes.
    /// </summary>
    public static T? GetFhirModelAttribute<T>(this MemberInfo t, FhirRelease version) where T : FhirModelAttribute =>
        t.GetFhirModelAttributes<T>(version).LastOrDefault();

    /// <summary>
    /// Gets all attribute of <typeparamref name="T"/> or subclasses on a given member that is relevant for the
    /// given <paramref name="version" />.
    /// </summary>
    public static IEnumerable<T> GetFhirModelAttributes<T>(this MemberInfo t, FhirRelease version) where T : FhirModelAttribute
    {
        return t.GetCustomAttributes<T>().Where(isRelevant).OrderBy(att => att.Since);

        bool isRelevant(FhirModelAttribute a) => a.AppliesToRelease(version);
    }

    /// <summary>
    /// Gets all <see cref="ValidatingFhirModelAttribute"/> attributes (including) subclasses on a given member
    /// that is relevant for the given <paramref name="version" />. Will return at most one result per type of the attribute.
    /// </summary>
    public static IEnumerable<ValidatingFhirModelAttribute> GetValidatingAttributes(this MemberInfo t, FhirRelease version) =>
        GetFhirModelAttributes<ValidatingFhirModelAttribute>(t, version)
            .GroupBy(att => att.GetType())
            .Select(g => g.LastOrDefault())
            .OfType<ValidatingFhirModelAttribute>();
}