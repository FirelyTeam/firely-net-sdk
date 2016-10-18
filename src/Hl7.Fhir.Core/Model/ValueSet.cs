/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    public partial class ValueSet : Hl7.Fhir.Model.DomainResource
    {
        [Obsolete("This property was renamed in DSTU2 to CodeSystem", true)]
        public CodeSystemComponent Define { get; set; }

        public bool HasExpansion => Expansion != null;

        public int CountCodes()
        {
            if (!HasExpansion)
                throw Error.InvalidOperation($"ValueSet '{Url}' has no expansion, generate the expansion first before calling this function");

            return countCodes(Expansion.Contains);
        }

        private int countCodes(IEnumerable<ValueSet.ContainsComponent> contains)
        {
            return contains.Where(ct => ct.Contains.Any())
                            .Aggregate(contains.Count(), (r, ct) => r + countCodes(ct.Contains));
        }

        public bool CodeInExpansion(String code, string system = null)
        {
            if (!HasExpansion)
                throw Error.InvalidOperation($"ValueSet '{Url}' has no expansion, generate the expansion first before calling this function");

            return FindInExpansion(code, system) != null;

        }

        public ValueSet.ContainsComponent FindInExpansion(String code, string system = null)
        {
            if (!HasExpansion)
                throw Error.InvalidOperation($"ValueSet '{Url}' has no expansion, generate the expansion first before calling this function");

            return Expansion.Contains.FindCode(code, system);
        }

    }
}
