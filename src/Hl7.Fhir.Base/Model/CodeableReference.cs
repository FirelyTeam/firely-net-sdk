/* 
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;

namespace Hl7.Fhir.Model;

public partial class CodeableReference : ICoded
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

    public IEnumerable<Coding> ToCodings() => Concept?.ToCodings() ?? [];
}