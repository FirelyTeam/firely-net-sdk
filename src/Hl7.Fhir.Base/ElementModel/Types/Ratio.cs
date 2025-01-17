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

public class Ratio(Quantity numerator, Quantity denominator) : Any
{
    public Quantity Numerator { get; } = numerator ?? throw new ArgumentNullException(nameof(numerator));
    public Quantity Denominator { get; } = denominator ?? throw new ArgumentNullException(nameof(denominator));

    public static Ratio Parse(string representation) =>
        TryParse(representation, out var result) ? result! : throw new FormatException($"String '{representation}' was not recognized as a valid ratio.");
    public static bool TryParse(string representation, [NotNullWhen(true)] out Ratio? value)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        value = null;

        // Not too sure if quantities cannot contain colons themselves, but I have
        // no time to worry about that now.
        var components = representation.Split(':');
        if (components.Length != 2) return false;

        if (!Quantity.TryParse(components[0].Trim(), out var numerator)) return false;
        if (!Quantity.TryParse(components[1].Trim(), out var denumerator)) return false;

        value = new Ratio(numerator, denumerator);
        return true;
    }

    public override bool Equals(object? obj) => obj is Ratio r && Numerator == r.Numerator && Denominator == r.Denominator;

    public override int GetHashCode() => (Numerator, Denominator).GetHashCode();
    public override string ToString() => $"{Numerator}:{Denominator}";

    public static explicit operator String(Ratio r) => RunCast<String>(r);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(Ratio))
            result = this;
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }

    public static bool operator ==(Ratio left, Ratio right) => left.Equals(right);
    public static bool operator !=(Ratio left, Ratio right) => !Equals(left, right);
}