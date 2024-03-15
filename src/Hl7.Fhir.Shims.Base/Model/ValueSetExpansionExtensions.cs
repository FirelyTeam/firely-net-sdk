using System;
using System.Collections.Generic;
using System.Linq;
using CSDC = Hl7.Fhir.Model.CodeSystem.ConceptDefinitionComponent;

#nullable enable

namespace Hl7.Fhir.Model
{
    public static class ValueSetExpansionExtensions
    {
        public static ValueSet.ContainsComponent? FindCode(this IEnumerable<ValueSet.ContainsComponent> cnt, string code, string? system = null)
        {
            foreach (var contains in cnt)
            {
                var result = contains.FindCode(code, system);
                if (result != null) return result;
            }

            return null;
        }
        public static ValueSet.ContainsComponent? FindCode(this ValueSet.ContainsComponent contains, string code, string? system = null)
        {
            // Direct hit
            if (code == contains.Code && (system == null || system == contains.System))
                return contains;

            // Not in this node, but this node may have child nodes to check
            if (contains.Contains?.Any() == true)
                return contains.Contains.FindCode(code, system);
            else
                return null;
        }

        internal static CSDC? FindCode(this IEnumerable<CSDC> concepts, string code)
        {
            return concepts.findCodeByPredicate(c => c.Code == code);
        }

        private static CSDC? findCodeByPredicate(this IEnumerable<CSDC> concepts, Predicate<CSDC> predicate)
        {
            foreach (var concept in concepts)
            {
                var result = concept.findCodeByPredicate(predicate);
                if (result != null) return result;
            }
            return null;
        }

        private static CSDC? findCodeByPredicate(this CSDC concept, Predicate<CSDC> predicate)
        {
            // Direct hit
            if (predicate(concept))
                return concept;

            // Not in this node, but this node may have child nodes to check
            if (concept.Concept?.Any() == true)
                return concept.Concept.findCodeByPredicate(predicate);
            else
                return null;
        }
    }
}
