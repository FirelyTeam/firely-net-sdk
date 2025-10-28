/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Types;

public class Concept(IEnumerable<Code> codes, string? display = null) : Any
{
    public IReadOnlyCollection<Code> Codes { get; } = codes.ToArray();

    public string? Display { get; } = display;

    public static Concept Parse(string representation) => throw new NotImplementedException();
    public static bool TryParse(string representation, [NotNullWhen(true)] out Concept? value) => throw new NotImplementedException();

    public override bool Equals(object? obj) => obj is Concept c && Codes.SequenceEqual(c.Codes) && Display == c.Display;
    public override int GetHashCode() => (Codes, Display).GetHashCode();
    public static bool operator ==(Concept left, Concept right) => Equals(left, right);
    public static bool operator !=(Concept left, Concept right) => !Equals(left, right);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;
        return false;
    }

    public override string ToString() => string.Join(", ", Codes) + (Display != null ? $" \"{Display}\"" : "");

    // Does not support equality, equivalence and ordering in the CQL sense, so no explicit implementations of these interfaces
}