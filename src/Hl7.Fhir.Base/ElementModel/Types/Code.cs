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

public class Code(string? system, string code, string? display = null, string? version = null) : Any
{
    public string? System { get; } = system;
    public string Value { get; } = code ?? throw new ArgumentNullException(nameof(code));
    public string? Display { get; } = display;
    public string? Version { get; } = version;

    public static Code Parse(string value) => throw new NotImplementedException();
    public static bool TryParse(string representation, [NotNullWhen(true)] out Code? value) => throw new NotImplementedException();

    public override bool Equals(object? obj) => obj is Code c
                                                && System == c.System && Value == c.Value && Display == c.Display && Version == c.Version;
    public override int GetHashCode() => (System, Value, Display, Version).GetHashCode();

    public static bool operator ==(Code left, Code right) => left.Equals(right);
    public static bool operator !=(Code left, Code right) => !left.Equals(right);

    public static explicit operator Concept(Code c) => RunCast<Concept>(c);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(Code))
            result = this;
        else if (to == typeof(Concept))
            result = new Concept([this], Display);

        return result is not null;
    }

    public override string ToString() => $"{Value}@{System} " + Display;

    // Does not support equality, equivalence and ordering in the CQL sense, so no explicit implementations of these interfaces
}