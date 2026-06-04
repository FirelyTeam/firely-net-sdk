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

public class Integer(int value) : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    public Integer() : this(default) { }

    public int Value { get; } = value;

    public static Integer Parse(string value) =>
        TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid integer.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Integer? value)
    {
        if (representation == null) throw new ArgumentNullException(nameof(representation));

        var (succ, val) = DoConvert(() => XmlConvert.ToInt32(representation));
        value = succ ? new Integer(val) : null;
        return succ;
    }

    /// <summary>
    /// Determines if two integers are equal according to CQL equality rules.
    /// </summary>
    /// <remarks>For integers, CQL and .NET equality rules are aligned.
    /// </remarks>
    public override bool Equals(object? obj) => obj is Integer i && Value == i.Value;

    public static bool operator ==(Integer a, Integer b) => Equals(a, b);
    public static bool operator !=(Integer a, Integer b) => !Equals(a, b);

    /// <summary>
    /// Compares two integers, according to CQL equality rules
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <remarks>For integers, CQL and .NET comparison rules are aligned.</remarks>
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            Integer i => Value.CompareTo(i.Value),
            _ => throw NotSameTypeComparison(this, obj)
        };
    }

    public static bool operator <(Integer a, Integer b) => a.CompareTo(b) < 0;
    public static bool operator <=(Integer a, Integer b) => a.CompareTo(b) <= 0;
    public static bool operator >(Integer a, Integer b) => a.CompareTo(b) > 0;
    public static bool operator >=(Integer a, Integer b) => a.CompareTo(b) >= 0;


    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => XmlConvert.ToString(Value);

    public static implicit operator int(Integer i) => i.Value;
    public static explicit operator Integer(int i) => new(i);
    public static explicit operator Long(Integer i) => RunCast<Long>(i);
    public static explicit operator Decimal(Integer i) => RunCast<Decimal>(i);
    public static explicit operator Quantity(Integer i) => RunCast<Quantity>(i);
    public static explicit operator Boolean(Integer i) => RunCast<Boolean>(i);
    public static explicit operator String(Integer i) => RunCast<String>(i);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

       if (to == typeof(Integer))
            result = this;
       else if (to == typeof(Long))
           result = new Long(Value);
       else if (to == typeof(Decimal))
           result = new Decimal(Value);
       else if (to == typeof(Quantity))
           result = new Quantity(Value);
       else if (to == typeof(Boolean))
           result = Value switch
           {
               1 => Boolean.True,
               0 => Boolean.False,
               _ => null
           };
       else if (to == typeof(String))
           result = new String(ToString());

        return result is not null;
    }

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => Equals(other);
    int? ICqlOrderable.CompareTo(Any? other) => other is not null ? CompareTo(other) : null;


}