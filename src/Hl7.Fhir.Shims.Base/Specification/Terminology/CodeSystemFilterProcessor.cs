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
        private static readonly ReadOnlyCollection<FilterOperator?> SUPPORTEDFILTERS = new(new List<FilterOperator?> { FilterOperator.IsA });
        private const string SUBSUMEDBYCODE = "subsumedBy";

        /// <summary>
        /// Retrieve codes from a CodeSystem resource bases on one or multiple filters.
        /// </summary>
        /// <param name="codeSystemUri">Uri of the CodeSystem</param>
        /// <param name="filters">Filters to be applied</param>
        /// <param name="settings">ValueSetExpanderSettings </param>
        /// <returns></returns>
        /// <exception cref="ValueSetExpansionTooComplexException">Thrown when a filter is applied that is not supported (yet)</exception>
        /// <exception cref="CodeSystemUnknownException">Thrown when no resource resolver was set in ValueSetExpanderSettings.ValueSetSource</exception>
        /// <exception cref="CodeSystemUnknownException">Thrown when the requested CodeSystem can not be found by the resource resolver in ValueSetExpanderSettings</exception>
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
                _ => throw new InvalidOperationException("no filter was selected")
            };
        }

        private static IEnumerable<CodeSystem.ConceptDefinitionComponent> applyIsAFilter(List<CodeSystem.ConceptDefinitionComponent> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();

            //find descendants based on subsumedBy
            if (properties.Any(p => p.Code == SUBSUMEDBYCODE))
            {
                //first find the parent itself (if it's in the CodeSystem)
                if (concepts.FindCode(filter.Value) is { } concept)
                    result.Add(concept);

                //Create a dictionary which lists children by parent.
                var dict = new Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>>();
                addConceptsToSubsumedByDict(concepts, dict);

                //find descendants based on that dictionary
                var descendants = applySubsumedBy(dict, filter);
                result.AddRange(descendants);
            }
            else
            {
                //SubsumedBy is not used, we should only check for a nested hierarchy, and include the code and it's descendants
                if (concepts.FindCode(filter.Value) is { } concept)
                    result.Add(concept);
            }
            return result;
        }

        private static void addConceptsToSubsumedByDict(List<CodeSystem.ConceptDefinitionComponent> concepts, Dictionary<string, List<CodeSystem.ConceptDefinitionComponent>> dict)
        {
            foreach (var concept in concepts)
            {
                //find all properties that are parents.
                var parents = concept.Property
                                       .Where(p => p.Code == SUBSUMEDBYCODE && p.Value is Code && ((Code)p.Value).Value is not null)
                                       .Select(p => ((Code)p.Value).Value);

                //Find all their children
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

        //recursively loop through all the children to eventually find all descendants.
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
