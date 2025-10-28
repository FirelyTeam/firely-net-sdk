/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Hl7.Fhir.ElementModel.Types;

public class String(string value) : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    public string Value { get; } = value;

    public static String Parse(string value) =>
        TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid string.");

    // Actually, it's not that trivial, since CQL strings accept a subset of C#'s escape sequences,
    // we *could* validate those here.
    public static bool TryParse(string representation, [NotNullWhen(true)] out String? value)
    {
        if (representation == null) throw new ArgumentNullException(nameof(representation));

        value = new String(representation);   // a bit obvious
        return true;
    }

    public override bool Equals(object? obj) => obj is Any other && Equals(other, CQL_EQUALS_COMPARISON);
    public static bool operator ==(String a, String b) => Equals(a, b);
    public static bool operator !=(String a, String b) => !Equals(a, b);

    /// <summary>
    /// Compares two strings according to CQL equivalence rules.
    /// </summary>
    public bool Equals(Any other, StringComparison comparisonType)
    {
        if (other is not String otherS) return false;

        if (comparisonType == StringComparison.Unicode)
            return string.CompareOrdinal(Value, otherS.Value) == 0;

        var l = comparisonType.HasFlag(StringComparison.NormalizeWhitespace) ? normalizeWs(Value) : Value;
        var r = comparisonType.HasFlag(StringComparison.NormalizeWhitespace) ? normalizeWs(otherS.Value) : otherS.Value;

        var compareOptions = CompareOptions.None;
        if (comparisonType.HasFlag(StringComparison.IgnoreCase)) compareOptions |= CompareOptions.IgnoreCase;
        if (comparisonType.HasFlag(StringComparison.IgnoreDiacritics)) compareOptions |= CompareOptions.IgnoreNonSpace;

        return string.Compare(l, r, CultureInfo.InvariantCulture, compareOptions) == 0;
    }

    public const StringComparison CQL_EQUALS_COMPARISON = StringComparison.Unicode;
    public const StringComparison CQL_EQUIVALENCE_COMPARISON = StringComparison.IgnoreCase | StringComparison.IgnoreDiacritics | StringComparison.NormalizeWhitespace;

    private static string normalizeWs(string data)
    {
        var dataAsChars = data.ToCharArray();
        for (var ix = 0; ix < dataAsChars.Length; ix++)
        {
            if (char.IsWhiteSpace(dataAsChars[ix]))
                dataAsChars[ix] = ' ';
        }

        return new string(dataAsChars);
    }


    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            String s => string.CompareOrdinal(Value, s.Value),
            _ => throw NotSameTypeComparison(this, obj)
        };
    }

    public static bool operator <(String a, String b) => a.CompareTo(b) < 0;
    public static bool operator <=(String a, String b) => a.CompareTo(b) <= 0;
    public static bool operator >(String a, String b) => a.CompareTo(b) > 0;
    public static bool operator >=(String a, String b) => a.CompareTo(b) >= 0;

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(String s) => s.Value;
    public static explicit operator String(string s) => new(s);
    public static explicit operator Boolean(String s) => RunCast<Boolean>(s);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(String))
            result = this;
        else if (to == typeof(Boolean))
            result = Value.ToLower() switch
            {
                "true" or "t" or "yes" or "y" or "1" or "1.0" => Boolean.True,
                "false" or "f" or "no" or "n" or "0" or "0.0" => Boolean.False,
                _ => null
            };
        else
            _ = TryParseToAny(Value, to, out result);

        return result is not null;
    }

    private static T convertTo<T>(String s) where T:Any =>
        s.TryConvertTo<T>(out var result) ? result : throw new InvalidCastException($"Cannot cast String value {s} to {typeof(T).Name}.");

    public static explicit operator DateTime(String s) => convertTo<DateTime>(s);
    public static explicit operator Date(String s) => convertTo<Date>(s);
    public static explicit operator Time(String s) => convertTo<Time>(s);
    public static explicit operator Decimal(String s) => convertTo<Decimal>(s);
    public static explicit operator Integer(String s) => convertTo<Integer>(s);
    public static explicit operator Long(String s) => convertTo<Long>(s);
    public static explicit operator Quantity(String s) => convertTo<Quantity>(s);

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other, CQL_EQUALS_COMPARISON) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && Equals(other, CQL_EQUIVALENCE_COMPARISON);
    int? ICqlOrderable.CompareTo(Any? other) => other is not null ? CompareTo(other) : null;
}