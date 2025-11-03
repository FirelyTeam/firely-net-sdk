/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.Model;

/// <summary>
/// Contains both a value for an overflow and its nature.
/// </summary>
/// <remarks>Internal use only. This class keeps track of why an item appears in overflow so validation
/// can provide the correct error message.</remarks>
public abstract record OverflowEntry(object Value)
{
    private readonly static object OVERFLOW_MARKER = new();

    public static T GetOverflowMarker<T>() where T:class => Unsafe.As<T>(OVERFLOW_MARKER);
    public static bool IsOverflow(object marker) => ReferenceEquals(marker, OVERFLOW_MARKER);

    public virtual CodedValidationException? Validate(ValidationContext context) => null;
}

public record UnknownElementOverflow(object Value) : OverflowEntry(Value);

public record IncorrectTypeOverflow(object Value, Type ExpectedType) : OverflowEntry(Value);