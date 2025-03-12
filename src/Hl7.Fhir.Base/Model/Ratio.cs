/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using P=Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

public partial class Ratio: P.IToSystemPrimitive
{
    public Ratio()
    {
        // Nothing
    }

    public Ratio(Quantity numerator, Quantity denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    /// <summary>
    /// Converts this Ratio to a <see cref="P.Ratio" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The nominator or denominator is null
    /// or cannot be converted to System quantities.</exception>
    public P.Ratio ToSystemRatio()
    {
        var (v, e) = tryConvert();
        return v! ?? throw e!;
    }

    private (P.Ratio? value, Exception? e) tryConvert()
    {
        if(Numerator is not P.IToSystemPrimitive numerator)
            return (null, new InvalidOperationException($"Numerator cannot be null."));

        if(Denominator is not P.IToSystemPrimitive denominator)
            return (null, new InvalidOperationException($"Denominator cannot be null."));

        if(!numerator.TryConvertToSystemType(out var num))
            return (null, new InvalidOperationException($"Conversion of Numerator failed."));

        if(!denominator.TryConvertToSystemType(out var den))
            return (null, new InvalidOperationException($"Conversion of Denominator failed."));

        return (new P.Ratio((P.Quantity)num, (P.Quantity)den), null);
    }

    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        var (v, e) = tryConvert();
        result = v;
        return e is null;
    }
}