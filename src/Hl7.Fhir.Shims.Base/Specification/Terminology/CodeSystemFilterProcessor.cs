using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Terminology
{

    internal static class CodeSystemFilterProcessor
    {
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
            if (settings.CodeSystemsTooComplexToFilter.Contains(codeSystemUri))
            {
                throw new ValueSetExpansionTooComplexException($"Filtering codes from complex CodeSystem {codeSystemUri} is not supported");
            }

            if (settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve codesystem '{codeSystemUri}', so the expansion cannot be completed.");

            var codeSystem = await settings.ValueSetSource.AsAsync().FindCodeSystemAsync(codeSystemUri).ConfigureAwait(false)
                  ?? throw new CodeSystemUnknownException($"Cannot find codesystem '{codeSystemUri}', so the defined filter(s) cannot be applied.");

            if (codeSystem.Content.GetLiteral() != "complete")
                throw new CodeSystemIncompleteException($"CodeSystem {codeSystemUri} is marked incomplete, so the defines filter(s) cannot be applied.");


            var result = applyFilters(filters, codeSystem);

            return result.Select(c => c.ToContainsComponent(codeSystem, settings));
        }

        private static List<CodeSystem.ConceptDefinitionComponent> applyFilters(List<ValueSet.FilterComponent> filters, CodeSystem codeSystem)
        {
            var result = codeSystem.Concept;
            var properties = codeSystem.Property;

            foreach (var filter in filters)
            {
                result = applyFilter(result, properties, filter).ToList();
            }

            return result;
        }

        private static IEnumerable<CodeSystem.ConceptDefinitionComponent> applyFilter(List<CodeSystem.ConceptDefinitionComponent> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            return filter.Op switch
            {
                FilterOperator.IsA => applyIsAFilter(concepts, properties, filter),
                _ => throw new ValueSetExpansionTooComplexException($"ConceptSets with a filter {filter.Op} are not yet supported.")
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

                //Create a lookup which lists children by parent.
                var flattened = concepts.Flatten();
                var childrenLookup = CreateSubsumedByLookup(flattened);

                //find descendants based on that lookup
                var descendants = applySubsumedBy(childrenLookup, filter);
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

        private static ILookup<string, CodeSystem.ConceptDefinitionComponent> CreateSubsumedByLookup(List<CodeSystem.ConceptDefinitionComponent> flattenedConcepts)
        {
            return flattenedConcepts
                .SelectMany(concept => concept.Property
                    .Where(p => p.Code == SUBSUMEDBYCODE && p.Value is Code && ((Code)p.Value).Value is not null)
                    .Select(p => new { SubsumedByValue = ((Code)p.Value).Value, Concept = concept }))
                .ToLookup(x => x.SubsumedByValue, x => x.Concept);
        }

        private static List<CodeSystem.ConceptDefinitionComponent> applySubsumedBy(ILookup<string, CodeSystem.ConceptDefinitionComponent> lookup, ValueSet.FilterComponent filter)
        {
            var result = new List<CodeSystem.ConceptDefinitionComponent>();
            var root = filter.Value;
            if (root != null)
            {
                addDescendants(lookup, root, result);
            }
            return result;
        }

        //recursively loop through all the children to eventually find all descendants.
        private static void addDescendants(ILookup<string, CodeSystem.ConceptDefinitionComponent> lookup, string parent, List<CodeSystem.ConceptDefinitionComponent> result)
        {
            if (lookup[parent] is { } children)
            {
                foreach (var child in children)
                {
                    result.Add(child);
                    parent = child.Code;
                    addDescendants(lookup, parent, result);
                }
            }
        }

    }
}

#nullable restore
