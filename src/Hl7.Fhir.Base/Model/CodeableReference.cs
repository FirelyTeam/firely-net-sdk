/* 
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

public partial class CodeableReference : ICoded, P.IToSystemPrimitive
{
    public CodeableReference()
    {
        // Nothing
    }

    public CodeableReference(CodeableConcept concept)
    {
        Concept = concept;
    }

    public CodeableReference(ResourceReference reference)
    {
        Reference = reference;
    }

    /// <summary>
    /// Converts the concept part of this CodeableReference to a <see cref="P.Concept" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The concept part of this CodeableReference is null,
    /// which is not valid for System Concepts.</exception>
    public P.Concept ToSystemConcept() =>
        ToSystemConceptInternal() ??
        throw new InvalidOperationException("CodeableReference does not contain a CodeableConcept " +
                                            "and can therefore not be converted to a System Concept.");

    internal P.Concept? ToSystemConceptInternal() =>
        ((P.IToSystemPrimitive?)Concept)?.TryConvertToSystemType(out var result) == true ? (P.Concept)result : null;

    /// <summary>
    /// Converts the reference part of this CodeableReference to a <see cref="P.String" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The reference part of this CodeableReference is null,
    /// which is not valid for System Strings.</exception>
    public P.String ToSystemString() =>
        ToSystemStringInternal() ??
        throw new InvalidOperationException("CodeableReference does not contain a Reference uri or identifier" +
                                            " and can therefore not be converted to a System String.");

    internal P.String? ToSystemStringInternal() =>
        (Reference?.Reference ?? Reference?.Identifier?.Value) is { } reference
            ? new P.String(reference)
            : null;

    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        if (ToSystemConceptInternal() is {} concept)
        {
            result = concept;
            return true;
        }

        if (ToSystemStringInternal() is {} reference)
        {
            result = reference;
            return true;
        }

        result = null;
        return false;
    }

    /// <inheritdoc cref="ICoded.ToCodings"/>
    public IReadOnlyCollection<Coding> ToCodings() => Concept?.ToCodings() ?? [];
}