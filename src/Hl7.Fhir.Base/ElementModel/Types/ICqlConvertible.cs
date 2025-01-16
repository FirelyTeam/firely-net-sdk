/*
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.ElementModel.Types;

public interface ICqlConvertible
{
    /// <summary>
    /// Tries to convert this object into a CQL/FhirPath type.
    /// </summary>
    /// <param name="to">The subclass of Any to convert to.</param>
    /// <param name="result">If succesful, the converted object, otherwise null.</param>
    bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result);
}


public static class CqlConvertible
{
    public static bool TryConvertTo<T>(this ICqlConvertible source, [NotNullWhen(true)] out T? result) where T : Any
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