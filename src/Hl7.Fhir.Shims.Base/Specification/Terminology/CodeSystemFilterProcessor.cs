using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using T = System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Terminology
{

    internal static class CodeSystemFilterProcessor
    {
        private static readonly ReadOnlyCollection<FilterOperator?> SUPPORTEDFILTERS = new(new List<FilterOperator?> { FilterOperator.IsA, FilterOperator.IsNotA });

        internal async static T.Task<IEnumerable<ValueSet.ContainsComponent>> FilterConceptsFromCodeSystem(string codeSystemUri, List<ValueSet.FilterComponent> filters, ValueSetExpanderSettings settings)
        {

            if (filters.Any(f => !SUPPORTEDFILTERS.Contains(f.Op)))
            {
                string supportedFiltersString = string.Join(", ", SUPPORTEDFILTERS.Select(f => $"'{f.GetLiteral()}'"));
                throw new ValueSetExpansionTooComplexException($"ConceptSets with a filter other than {SUPPORTEDFILTERS} are not yet supported.");
            }

            if (settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve codesystem '{codeSystemUri}', so the expansion cannot be completed.");

            var codeSystem = await settings.ValueSetSource.AsAsync().FindCodeSystemAsync(codeSystemUri).ConfigureAwait(false)
                  ?? throw new CodeSystemUnknownException($"Cannot find codesystem '{codeSystemUri}', so the defined filter(s) cannot be applied.");

            var result = applyFilters(filters, codeSystem);

            return result.Select(c => c.ToContainsComponent(codeSystem, settings));
        }

        private static List<CodeSystem.ConceptDefinitionComponent> applyFilters(List<ValueSet.FilterComponent> filters, CodeSystem codeSystem)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();
            var properties = codeSystem.Property;
            bool first = true;

            foreach (var filter in filters)
            {
                //If multiple filters are specified within the include, they SHALL all be true. So the second filter, can just filter the result of the first etc.
                var newResult = first
                                    ? applyFilter(codeSystem.Concept, properties, filter)
                                    : applyFilter(result, properties, filter);

                if (first) first = false;
                result = newResult.ToList();
            }

            return result;
        }

        private static IEnumerable<CodeSystem.ConceptDefinitionComponent> applyFilter(List<CodeSystem.ConceptDefinitionComponent> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            return filter.Op switch
            {
                FilterOperator.IsA => applyIsAFilter(concepts, properties, filter),
                FilterOperator.IsNotA => applyIsNotAFilter(concepts, properties, filter),
                _ => throw new InvalidOperationException("no filter was selected")
            };
        }

        private static IEnumerable<CodeSystem.ConceptDefinitionComponent> applyIsNotAFilter(List<CodeSystem.ConceptDefinitionComponent> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            throw new NotImplementedException();
        }


        private static IEnumerable<CodeSystem.ConceptDefinitionComponent> applyIsAFilter(List<CodeSystem.ConceptDefinitionComponent> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();

            if (properties.Any(p => p.Code == "subsumedBy"))
            {
                //first find the parent itslef (if it's in the CodeSystem)
                if (concepts.FindCode(filter.Value) is { } concept)
                    result.Add(concept);

                var dict = new Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>>();
                addConceptsToSubsumedByDict(concepts, dict);
                result.AddRange(applySubsumedBy(dict, filter));
            }
            else
            {
                //SubsumedBy is not used, we should only check for a nested hierarchie, and include the code and it's descendants
                if (concepts.FindCode(filter.Value) is { } concept)
                    result.Add(concept);
            }
            return result;
        }

        private static void addConceptsToSubsumedByDict(List<CodeSystem.ConceptDefinitionComponent> concepts, Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>> dict)
        {

            foreach (var concept in concepts)
            {
                var parents = concept.Property
                                       .Where(p => p.Code == "subsumedBy" && p.Value is Code && ((Code)p.Value).Value is not null)
                                       .Select(p => ((Code)p.Value).Value);

                foreach (var parent in parents)
                {
                    if (dict.ContainsKey(parent))
                    {
                        dict[parent].Add(concept);
                    }
                    else
                    {
                        dict.Add(parent, new List<CodeSystem.ConceptDefinitionComponent> { concept });
                    }
                }

                if (concept.Concept is not null && concept.Concept.Any())
                {
                    addConceptsToSubsumedByDict(concept.Concept, dict);
                }
            }
        }

        private static List<CodeSystem.ConceptDefinitionComponent> applySubsumedBy(Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>> dict, ValueSet.FilterComponent filter)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();
            var root = filter.Value;
            if (root != null)
            {
                addDescendants(dict, root, result);
            }
            return result;
        }

        private static void addDescendants(Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>> dict, string parent, List<CodeSystem.ConceptDefinitionComponent> result)
        {
            if (dict.TryGetValue(parent, out var descendants))
            {
                foreach (var descendant in descendants)
                {
                    result.Add(descendant);
                    parent = descendant.Code;
                    addDescendants(dict, parent, result);
                }
            }
        }
    }
}

#nullable restore
