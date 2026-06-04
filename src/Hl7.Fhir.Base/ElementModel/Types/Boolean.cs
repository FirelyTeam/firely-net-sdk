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

namespace Hl7.Fhir.ElementModel.Types;

public class Boolean(bool value) : Any, ICqlEquatable
{
    public static Boolean True = new(true);
    public static Boolean False = new(false);
    public const string TRUE_LITERAL = "true";
    public const string FALSE_LITERAL = "false";

    public Boolean() : this(false) { }

    public bool Value { get; } = value;

    public static Boolean Parse(string value) =>
        TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid boolean.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Boolean? value)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        if (representation == TRUE_LITERAL)
        {
            value = True;
            return true;
        }
        else if (representation == FALSE_LITERAL)
        {
            value = False;
            return true;
        }
        else
        {
            value = null;
            return false;
        }
    }

    public override bool Equals(object? obj) => obj is Boolean b && Value == b.Value;
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Boolean a, Boolean b) => Equals(a, b);
    public static bool operator !=(Boolean a, Boolean b) => !Equals(a, b);

    public static implicit operator bool(Boolean b) => b.Value;
    public static explicit operator Boolean(bool b) => new(b);

    public static explicit operator Decimal(Boolean b) => RunCast<Decimal>(b);
    public static explicit operator Integer(Boolean b) => RunCast<Integer>(b);
    public static explicit operator Long(Boolean b) => RunCast<Long>(b);
    public static explicit operator Quantity(Boolean b) => RunCast<Quantity>(b);
    public static explicit operator String(Boolean b) => RunCast<String>(b);

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => Equals(other);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if (to == typeof(Boolean))
            result = this;
        else if(to == typeof(Decimal))
            result = Value switch
            {
                true => new Decimal(1m),
                false => new Decimal(0m),
            };
        else if(to == typeof(Integer))
            result = Value switch
            {
                true => new Integer(1),
                false => new Integer(0),
            };
        else if(to == typeof(Long))
            result = Value switch
            {
                true => new Long(1),
                false => new Long(0),
            };
        else if(to == typeof(Quantity))
            result = Value switch
            {
                true => new Quantity(1.0m),
                false => new Quantity(0.0m),
            };
        else if(to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }

    public override string ToString() => Value ? TRUE_LITERAL : FALSE_LITERAL;
}