/* 
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model
{
    [Bindable(true)]
    public partial class CodeableReference
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
    }
}