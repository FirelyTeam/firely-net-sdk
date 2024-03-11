using System;
using System.Collections.Generic;
using System.Linq;

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
            if (contains.Contains != null && contains.Contains.Any())
                return contains.Contains.FindCode(code, system);
            else
                return null;
        }

        internal static CodeSystem.ConceptDefinitionComponent? FindCode(this IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, string code)
        {
            foreach (var concept in concepts)
            {
                var predicate = () => concept.Code == code;
                var result = concept.findCodeByPredicate(predicate);
                if (result != null) return result;
            }
            return null;
        }

        private static CodeSystem.ConceptDefinitionComponent? findCodeByPredicate(this IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, Func<bool> predicate)
        {
            foreach (var concept in concepts)
            {
                var result = concept.findCodeByPredicate(predicate);
                if (result != null) return result;
            }
            return null;
        }

        private static CodeSystem.ConceptDefinitionComponent? findCodeByPredicate(this CodeSystem.ConceptDefinitionComponent concept, Func<bool> predicate)
        {
            // Direct hit
            if (predicate.Invoke())
                return concept;

            // Not in this node, but this node may have child nodes to check
            if (concept.Concept != null && concept.Concept.Any())
                return concept.Concept.findCodeByPredicate(predicate);
            else
                return null;
        }


        internal static List<CodeSystem.ConceptDefinitionComponent> FilterCodesByProperty(this IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, string property, DataType value)
        {
            Func<CodeSystem.ConceptDefinitionComponent, bool> predicate = concept => concept.Property.Any(p => p.Code == property && p.Value.Matches(value));
            return concepts.filterCodesByPredicate(predicate);
        }


        private static List<CodeSystem.ConceptDefinitionComponent> filterCodesByPredicate(this IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, Func<CodeSystem.ConceptDefinitionComponent, bool> predicate)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();

            foreach (var concept in concepts)
            {
                if (predicate(concept))
                {
                    result.Add(concept);
                }

                if (concept.Concept != null && concept.Concept.Any())
                {
                    result.AddRange(concept.Concept.filterCodesByPredicate(predicate));
                }
            }
            return result;
        }
    }
}

#nullable restore
