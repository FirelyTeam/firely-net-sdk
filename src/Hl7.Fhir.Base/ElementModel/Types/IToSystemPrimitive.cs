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

/// <summary>
/// An interface to convert a model POCO into a CQL/FhirPath type.
/// </summary>
public interface IToSystemPrimitive
{
    /// <summary>
    /// Tries to convert this object into a CQL/FhirPath type.
    /// </summary>
    /// <param name="result">If succesful, the converted object, otherwise null.</param>
    /// <returns>true if this model object has a CQL equivalent and the conversion succeeded, otherwise false.</returns>
    bool TryConvertToSystemType([NotNullWhen(true)] out Any? result);
}