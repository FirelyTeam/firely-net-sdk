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

    public P.Concept ToSystemConcept()
    {
        if(Concept is null) throw new InvalidOperationException("CodeableReference does not contain a CodeableConcept" +
                                                                "and can therefore not be converted to a System Concept.");

        return Concept.ToSystemConcept();
    }

    public P.String ToSystemString() =>
        getReferenceString() ??
        throw new InvalidOperationException("CodeableReference does not contain a Reference uri or identifier" +
                                            " and can therefore not be converted to a System String.");

    private P.String? getReferenceString() =>
        (Reference.Reference ?? Reference.Identifier.Value) is { } reference
            ? new P.String(reference)
            : null;

    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        if (Concept is not null)
        {
            result = ToSystemConcept();
            return true;
        }

        if (getReferenceString() is {} reference)
        {
            result = reference;
            return true;
        }

        result = null;
        return false;
    }


    public IEnumerable<Coding> ToCodings() => Concept?.ToCodings() ?? [];
}