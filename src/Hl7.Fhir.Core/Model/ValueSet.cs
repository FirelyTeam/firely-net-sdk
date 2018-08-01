/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    public partial class ValueSet : Hl7.Fhir.Model.DomainResource
    {
        [NotMapped]
        [Obsolete("This property was renamed in DSTU2 to CodeSystem", true)]
        public CodeSystemComponent Define { get; set; }

        [NotMapped]
        public bool HasExpansion => Expansion != null;

        public int ExpansionSize()
        {
            ensureExpansion();

            return countCodes(Expansion.Contains);
        }

        private int countCodes(IEnumerable<ValueSet.ContainsComponent> contains)
        {
            return contains.Where(ct => ct.Contains.Any())
                            .Aggregate(contains.Count(), (r, ct) => r + countCodes(ct.Contains));
        }

        public bool CodeInExpansion(String code, string system = null)
        {
            ensureExpansion();

            return FindInExpansion(code, system) != null;

        }

        public ValueSet.ContainsComponent FindInExpansion(String code, string system = null)
        {
            ensureExpansion();

            return Expansion.Contains.FindCode(code, system);
        }

        public void ImportExpansion(ValueSet other)
        {
            other.ensureExpansion();

            var combinedExpansion = ExpansionComponent.Create();

            // Todo: worry about duplicates
            if (this.HasExpansion)
            {
                combinedExpansion.Parameter.AddRange(this.Expansion.Parameter);
                combinedExpansion.Contains.AddRange(this.Expansion.Contains);
            }

            combinedExpansion.Parameter.AddRange(other.Expansion.Parameter);
            combinedExpansion.Contains.AddRange(other.Expansion.Contains);

            combinedExpansion.Total = countCodes(combinedExpansion.Contains);
            combinedExpansion.Offset = 0;

            Expansion = combinedExpansion;
        }


        private void ensureExpansion()
        {
            if (!HasExpansion)
                throw Error.InvalidOperation($"ValueSet '{Url}' has no expansion, generate the expansion first before calling this function");
        }


        public partial class ExpansionComponent
        {
            public static ExpansionComponent Create()
            {
                var expansion = new ExpansionComponent();
                expansion.TimestampElement = FhirDateTime.Now();
                expansion.IdentifierElement = Uuid.Generate().AsUri();

                return expansion;
            }
        }

    }
}
