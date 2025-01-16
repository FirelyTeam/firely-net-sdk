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
using System.Xml;

namespace Hl7.Fhir.ElementModel.Types;

public class Long(long value) : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    public Long() : this(default) { }

    public long Value { get; } = value;

    public static Long Parse(string value) =>
        TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid long integer.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Long? value)
    {
        if (representation == null) throw new ArgumentNullException(nameof(representation));

        var (succ, val) = Any.DoConvert(() => XmlConvert.ToInt64(representation));
        value = succ ? new Long(val) : null;
        return succ;
    }

    /// <summary>
    /// Determines if two 64-bit integers are equal according to CQL equality rules.
    /// </summary>
    /// <remarks>For 64-bits integers, CQL and .NET equality rules are aligned.
    /// </remarks>
    public override bool Equals(object? obj) => obj is Long i && Value == i.Value;
    public static bool operator ==(Long a, Long b) => Equals(a, b);
    public static bool operator !=(Long a, Long b) => !Equals(a, b);

    /// <summary>
    /// Compares two 64-bit integers according to CQL equality rules
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <remarks>For 64-bit integers, CQL and .NET comparison rules are aligned.</remarks>
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            Long i => Value.CompareTo(i.Value),
            _ => throw NotSameTypeComparison(this, obj)
        };
    }

    public static bool operator <(Long a, Long b) => a.CompareTo(b) < 0;
    public static bool operator <=(Long a, Long b) => a.CompareTo(b) <= 0;
    public static bool operator >(Long a, Long b) => a.CompareTo(b) > 0;
    public static bool operator >=(Long a, Long b) => a.CompareTo(b) >= 0;

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => XmlConvert.ToString(Value);

    public static implicit operator long(Long i) => i.Value;
    public static explicit operator Long(long i) => new (i);

    public static implicit operator Decimal(Long i) => new (i.Value);
    public static implicit operator Quantity(Long i) => new (i.Value);

    public static explicit operator Boolean(Long l) =>
        l.Value switch
        {
            1 => Boolean.True,
            0 => Boolean.False,
            _ => throw new InvalidCastException($"Cannot cast Long value {l} to Boolean.")
        };

    public static implicit operator String(Long l) => new(l.ToString());

    public static explicit operator Integer(Long l) =>
        l.Value is >= int.MinValue and <= int.MaxValue
            ? new Integer((int)l.Value)
            : throw new InvalidCastException($"Cannot cast Long value {l} to Integer, it is too large.");

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        if (to == typeof(Long))
        {
            result = this;
            return true;
        }

        if (to == typeof(Integer))
        {
            result = (Integer)this;
            return true;
        }

        if (to == typeof(Decimal))
        {
            result = (Decimal)this;
            return true;
        }

        if (to == typeof(Quantity))
        {
            result = (Quantity)this;
            return true;
        }

        if (to == typeof(Boolean))
        {
            result = (Boolean)this;
            return true;
        }

        if (to == typeof(String))
        {
            result = (String)this;
            return true;
        }

        result = default;
        return false;
    }

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => Equals(other);
    int? ICqlOrderable.CompareTo(Any? other) => other is not null ? CompareTo(other) : null;
}