/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Utility;

internal static class ReflectionHelper
{
    public static bool CanBeTreatedAsType(this Type? currentType, Type? typeToCompareWith)
    {
        // Always return false if either Type is null
        if (currentType == null || typeToCompareWith == null)
            return false;

        // Return the result of the assignability test
        return typeToCompareWith.IsAssignableFrom(currentType);
    }

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
            throw Error.Argument("type", "Type {0} is not a typed collection.".FormatWith(type.Name));

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
}