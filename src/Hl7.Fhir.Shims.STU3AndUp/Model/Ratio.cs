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
    public P.Ratio ToSystemRatio() => new(numerator: Numerator.ToSystemQuantity(), denominator: Denominator.ToSystemQuantity());

    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        try
        {
            result = ToSystemRatio();
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}