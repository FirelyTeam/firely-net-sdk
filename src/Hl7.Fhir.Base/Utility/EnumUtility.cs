#nullable enable

/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Hl7.Fhir.Utility;

/// <summary>
/// A set of utility methods to work with serialized enumeration values.
/// </summary>
public static class EnumUtility
{
    /// <summary>
    /// Retrieves the literal value for the code represented by this enum value, or the member name itself if there
    /// is no literal value defined.
    /// </summary>
    public static string GetLiteral(this Enum e) =>   getEnumMapping(e.GetType()).GetLiteral(e);

    /// <summary>
    /// Retrieves the literal value for the code represented by this enum <typeparamref name="T"/>, or the member name itself if there
    /// is no literal value defined.
    /// </summary>
    public static string GetLiteral<T>(this T e) where T : struct, Enum => EnumMappingCache<T>.GetLiteral(e);

    /// <summary>
    /// Retrieves the literal value for the code represented by this nullable enum <typeparamref name="T"/>, or the member name itself if there
    /// is no literal value defined, or null if the enum does not have a value.
    /// </summary>
    public static string? GetLiteral<T>(this T? e) where T : struct, Enum => e?.GetLiteral();

    /// <summary>
    /// Retrieves the system canonical for the code represented by this enum value, or <c>null</c> if there is no system defined.
    /// </summary>
    public static string? GetSystem(this Enum e) => e.GetAttributeOnEnum<EnumLiteralAttribute>()?.System ?? e.GetType().GetCustomAttribute<FhirEnumerationAttribute>()?.DefaultCodeSystem;

    /// <summary>
    /// Retrieves the system canonical for the code represented by this enum <typeparamref name="T"/>, or <c>null</c> if there is no system defined.
    /// </summary>
    public static string? GetSystem<T>(this T e) where T : struct, Enum => EnumMappingCache<T>.GetSystem(e);

    /// <summary>
    /// Retrieves the description for this enum value or the enumeration value itself if there is no description defined.
    /// </summary>
    public static string GetDocumentation(this Enum e) =>
        e.GetAttributeOnEnum<DescriptionAttribute>()?.Description ?? e.ToString();

    private static readonly ConcurrentDictionary<Type, EnumMapping> CACHE = new();

    /// <summary>
    /// Finds an enumeration value from <paramref name="enumType"/> where the literal is the same as <paramref name="rawValue"/>.
    /// </summary>
    public static Enum? ParseLiteral(string? rawValue, Type enumType, bool ignoreCase = false)
        => getEnumMapping(enumType).ParseLiteral(rawValue, ignoreCase);

    /// <summary>
    /// Finds an enumeration value from enum <typeparamref name="T"/> where the literal is the same as <paramref name="rawValue"/>.
    /// </summary>
    public static T? ParseLiteral<T>(string? rawValue, bool ignoreCase = false) where T : struct, Enum
        => EnumMappingCache<T>.ParseLiteral(rawValue, ignoreCase);

    /// <summary>
    /// Gets the human readable name defined for the enumeration <paramref name="enumType"/>.
    /// </summary>
    public static string GetName(Type enumType) => getEnumMapping(enumType).Name;

    /// <summary>
    /// Gets the human readable name defined for the enumeration <typeparamref name="T"/>.
    /// </summary>
    public static string GetName<T>() where T : struct, Enum => EnumMappingCache<T>.Name;

    private static EnumMapping getEnumMapping(Type enumType)
        => CACHE.GetOrAdd(enumType, EnumMapping.Create);

    private static class EnumMappingCache<TEnum>
        where TEnum : struct, Enum
    {
        static EnumMappingCache()
        {
            var t = typeof(TEnum);
            var enumAttr = t.GetTypeInfo().GetCustomAttribute<FhirEnumerationAttribute>();
            Name = enumAttr?.BindingName ?? t.Name;
            DefaultCodeSystem = enumAttr?.DefaultCodeSystem;
            foreach (var enumValue in ReflectionHelper.FindEnumFields(t))
            {
                var attr = enumValue.GetCustomAttribute<EnumLiteralAttribute>();
                string literal = attr?.Literal ?? enumValue.Name;

                var value = (TEnum)enumValue.GetValue(null)!;

                _enumToLiteral.Add(value, literal);
                _literalToEnum.Add(literal, value);
                _caseInsensitiveLiteralToEnum.Add(literal, value);
                if (attr?.System is { } systemVal)
                {
                    _enumToSystem.Add(value, systemVal);
                }
            }
        }

        public static string Name { get; }

        public static string? DefaultCodeSystem { get; }

        private static readonly Dictionary<string, TEnum> _literalToEnum = new();
        private static readonly Dictionary<string, TEnum> _caseInsensitiveLiteralToEnum = new(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<TEnum, string> _enumToLiteral = new();
        private static readonly Dictionary<TEnum, string> _enumToSystem = new();

        public static string? GetSystem(TEnum value) =>
            !_enumToSystem.TryGetValue(value, out string? result)
                ? DefaultCodeSystem
                : result;

        public static string GetLiteral(TEnum value) =>
            !_enumToLiteral.TryGetValue(value, out string? result)
                ? throw new InvalidOperationException($"Should only pass enum values that are member of the given enum: {value} is not a member of {Name}.")
                : result;

        public static TEnum? ParseLiteral(string? literal, bool ignoreCase)
        {
            if (literal is null) return null;

            var success = ignoreCase
                ? _caseInsensitiveLiteralToEnum.TryGetValue(literal, out TEnum result)
                : _literalToEnum.TryGetValue(literal, out result);

            return success ? result : null;
        }
    }

    internal class EnumMapping
    {
        internal EnumMapping(string name, Type enumType)
        {
            Name = name;
            EnumType = enumType;
        }

        // Symbolic name of the enumeration
        public string Name { get; }

        // .NET enumeration type
        public Type EnumType { get; private set; }

        private readonly Dictionary<string, Enum> _literalToEnum = new();
        private readonly Dictionary<string, Enum> _caseInsensitiveLiteralToEnum = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<Enum, string> _enumToLiteral = new();

        public string GetLiteral(Enum value) =>
            !_enumToLiteral.TryGetValue(value, out string? result)
                ? throw new InvalidOperationException($"Should only pass enum values that are member of the given enum: {value} is not a member of {Name}.")
                : result;

        public Enum? ParseLiteral(string? literal, bool ignoreCase)
        {
            if (literal is null) return null;

            var success = ignoreCase
                ? _caseInsensitiveLiteralToEnum.TryGetValue(literal, out Enum? result)
                : _literalToEnum.TryGetValue(literal, out result);

            return success ? result : null;
        }

        public static EnumMapping Create(Type enumType)
        {
            if (enumType is null) throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum) throw new ArgumentException($"Type {enumType.Name} is not an enumerated type", nameof(enumType));

            var result = new EnumMapping(getEnumName(enumType), enumType);

            foreach (var enumValue in ReflectionHelper.FindEnumFields(enumType))
            {
                var attr = enumValue.GetCustomAttribute<EnumLiteralAttribute>();
                string literal = attr?.Literal ?? enumValue.Name;
                var value = (Enum)enumValue.GetValue(null)!;

                result._enumToLiteral.Add(value, literal);
                result._literalToEnum.Add(literal, value);
                result._caseInsensitiveLiteralToEnum.Add(literal, value);
            }

            return result;

            static string getEnumName(Type t)
            {
                var attr = t.GetTypeInfo().GetCustomAttribute<FhirEnumerationAttribute>();
                return attr != null ? attr.BindingName : t.Name;
            }
        }
    }
}