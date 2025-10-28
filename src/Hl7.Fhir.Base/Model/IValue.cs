/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

namespace Hl7.Fhir.Model;

/// <summary>Common generic Value property interface.</summary>
/// <typeparam name="T">The value type.</typeparam>
public interface IValue<T>
{
    /// <summary>Gets or sets the value</summary>
    T? Value { get; set; }
}

/// <summary>Common generic nullable value property interface.</summary>
/// <typeparam name="T">The value type.</typeparam>
public interface INullableValue<T> : IValue<T?> where T : struct;