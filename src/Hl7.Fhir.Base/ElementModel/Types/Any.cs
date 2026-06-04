/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// The base class for all CQL/FhirPath types.
/// </summary>
public abstract class Any
{
    /// <summary>
    /// Returns the concrete subclass of Any that is used to represent the
    /// type given in parmameter <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryGetSystemTypeByName(string name, [NotNullWhen(true)] out Type? result)
    {
        result = get();
        return result != null;

        Type? get() =>
            name switch
            {
                "Any" => typeof(Any),
                "Boolean" => typeof(Boolean),
                "Code" => typeof(Code),
                "Concept" => typeof(Concept),
                "Decimal" => typeof(Decimal),
                "Integer" => typeof(Integer),
                "Long" => typeof(Long),
                "Date" => typeof(Date),
                "DateTime" => typeof(DateTime),
                "Ratio" => typeof(Ratio),
                "Time" => typeof(Time),
                "Quantity" => typeof(Quantity),
                "String" => typeof(String),
                "Void" => typeof(void),
                _ => null,
            };
    }

    /// <summary>
    /// Tries to parse a string representation of a CQL value into an instance of a CQL type.
    /// </summary>
    /// <param name="value">The unparsed representation.</param>
    /// <param name="anyType">The type to parse into.</param>
    /// <param name="parsed">The parsed value, or null if parsing failed.</param>
    /// <exception cref="ArgumentNullException">If the string representation is null.</exception>
    /// <returns>true if parsing succeeded, false otherwise.</returns>
    public static bool TryParseToAny(string value, Type anyType, [NotNullWhen(true)] out Any? parsed)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (!typeof(Any).IsAssignableFrom(anyType)) throw new ArgumentException($"Must be a subclass of {nameof(Any)}.", nameof(anyType));

        parsed = parse();
        return parsed is not null;

        Any? parse()
        {
            if (anyType == typeof(Boolean))
                return Boolean.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Code))
                return Code.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Concept))
                return Concept.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Decimal))
                return Decimal.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Integer))
                return Integer.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Long))
                return Long.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Date))
                return Date.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(DateTime))
                return DateTime.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Time))
                return Time.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Ratio))
                return Ratio.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(Quantity))
                return Quantity.TryParse(value, out var p) ? p : null;
            else if (anyType == typeof(String))
                return String.TryParse(value, out var p) ? p : null;
            else
                return null;
        }
    }

    internal static (bool, T?) DoConvert<T>(Func<T> parser)
    {
        try
        {
            return (true, parser());
        }
        catch (Exception)
        {
            return (false, default);
        }
    }

    /// <summary>
    /// Try to convert a .NET instance to a Cql/FhirPath Any-based type.
    /// </summary>
    public static bool TryConvert(object? value, [NotNullWhen(true)] out Any? primitiveValue)
    {
        primitiveValue = conv();
        return primitiveValue != null;

        Any? conv()
        {
            // NOTE: Keep Any.TryConvertToSystemValue, TypeSpecifier.TryGetNativeType and TypeSpecifier.ForNativeType in sync
            return value switch
            {
                Any a => a,
                bool b => new Boolean(b),
                string s => new String(s),
                char c => new String(new string(c, 1)),
                int _ or short _ or ushort _ or uint _ => new Integer(System.Convert.ToInt32(value)),
                long _ or ulong _ => new Long(System.Convert.ToInt64(value)),
                DateTimeOffset dto => DateTime.FromDateTimeOffset(dto),
                float _ or double _ or decimal _ => new Decimal(System.Convert.ToDecimal(value)),
                Enum en => new String(en.GetLiteral()),
                Uri u => new String(u.OriginalString),
                byte[] bytes => new String(System.Convert.ToBase64String(bytes)),
                _ => null
            };
        }
    }

    /// <summary>
    /// Converts a .NET instance to a Cql/FhirPath Any-based type.
    /// </summary>
    public static Any? Convert(object? value)
    {
        if (value == null) return null;

        if (TryConvert(value, out var result))
            return result;

        throw new NotSupportedException($"There is no known Cql/FhirPath type corresponding to the .NET type {value.GetType().Name} of this instance (with value '{value}').");
    }

    // some utility methods shared by the subclasses
    protected static InvalidOperationException NotSameTypeComparison(object me, object? them) =>
        new($"Cannot compare {me} (of type {me.GetType()}) to {them} (of type {them?.GetType()}), because the types differ.");

    protected static TOut RunCast<TOut>(Any value) =>
        value.TryConvertTo(typeof(TOut), out var r) && r is TOut result
            ? result
            : throw new InvalidCastException($"Cannot cast from {value.GetType()} to {typeof(TOut)}.");

    /// <summary>
    /// Tries to convert one CQL datatype to another, as defined in the CQL specification.
    /// </summary>
    public abstract bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result);
}