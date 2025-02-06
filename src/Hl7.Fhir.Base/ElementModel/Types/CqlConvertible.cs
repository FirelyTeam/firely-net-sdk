/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.ElementModel.Types;

public static class CqlConvertible
{
    // <inheritdoc cref="Any.TryConvertTo{T}(Any, out T)"/>
    public static bool TryConvertTo<T>(this Any source, [NotNullWhen(true)] out T? result) where T : Any
    {
        var success = source.TryConvertTo(typeof(T), out var any) && any is T;

        switch (success)
        {
            case true:
                result = (T)any!;
                return true;
            default:
                result = null;
                return false;
        }
    }
}