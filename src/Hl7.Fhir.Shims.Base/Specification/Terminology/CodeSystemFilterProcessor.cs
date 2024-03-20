using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;
using CSDC = Hl7.Fhir.Model.CodeSystem.ConceptDefinitionComponent;
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
            if (settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve codesystem '{codeSystemUri}', so the expansion cannot be completed.");

            var codeSystem = await settings.ValueSetSource.AsAsync().FindCodeSystemAsync(codeSystemUri).ConfigureAwait(false)
                  ?? throw new CodeSystemUnknownException($"Cannot find codesystem '{codeSystemUri}', so the defined filter(s) cannot be applied.");

            if (codeSystem.Content.GetLiteral() != "complete")
                throw new CodeSystemIncompleteException($"CodeSystem `{codeSystemUri}` is marked incomplete, so the defined filter(s) cannot be applied.");


            var result = applyFilters(filters, codeSystem);

            return result.Select(c => c.ToContainsComponent(codeSystem, settings));
        }

        private static List<CSDC> applyFilters(List<ValueSet.FilterComponent> filters, CodeSystem codeSystem)
        {
            var result = codeSystem.Concept;
            var properties = codeSystem.Property;

            foreach (var filter in filters)
            {
                result = applyFilter(result, properties, filter).ToList();
            }

            return result;
        }

        private static IEnumerable<CSDC> applyFilter(List<CSDC> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            return filter.Op switch
            {
                FilterOperator.IsA => applyIsAFilter(concepts, properties, filter),
                FilterOperator.DescendentOf => applyDescentantOfFilter(concepts, properties, filter),
                _ => throw new ValueSetExpansionTooComplexException($"ConceptSets with filter `{filter.Op.GetLiteral()}` are not yet supported.")
            };
        }

        private static IEnumerable<CSDC> applyDescentantOfFilter(List<CSDC> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            var result = new List<CSDC>();
            //find descendants based on subsumedBy
            if (properties.Any(p => p.Code == SUBSUMEDBYCODE))
            {
                //first flatten the codes
                var flattened = concepts.Flatten();

                //then find the descendants, based on subsumbedBy
                List<CSDC> descendants = findDescendantsUsingSubsumedBy(filter, flattened);
                result.AddRange(descendants);
            }
            else
            {
                //SubsumedBy is not used, we should only check for a nested hierarchy, and find the code and include it's descendants
                if (concepts.FindCode(filter.Value) is { } concept)
                    result.AddRange(concept.Concept);
            }

            return result;
        }

        private static IEnumerable<CSDC> applyIsAFilter(List<CSDC> concepts, List<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            var result = new List<CSDC>();

            //find descendants based on subsumedBy
            if (properties.Any(p => p.Code == SUBSUMEDBYCODE))
            {
                //first flatten the codes
                var flattened = concepts.Flatten();

                //first find the parent itself (if it's in the CodeSystem)
                if (flattened.FindCode(filter.Value) is { } concept)
                    result.Add(concept);

                //then find the descendants
                List<CSDC> descendants = findDescendantsUsingSubsumedBy(filter, flattened);
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

        private static List<CSDC> findDescendantsUsingSubsumedBy(ValueSet.FilterComponent filter, List<CSDC> flattened)
        {
            //Create a lookup which lists children by parent.              
            var childrenLookup = CreateSubsumedByLookup(flattened);

            //find descendants based on that lookup
            var descendants = applySubsumedBy(childrenLookup, filter);
            return descendants;
        }

        private static ILookup<string, CSDC> CreateSubsumedByLookup(List<CSDC> flattenedConcepts)
        {
            return flattenedConcepts
                .SelectMany(concept => concept.Property
                    .Where(p => p.Code == SUBSUMEDBYCODE && p.Value is Code && ((Code)p.Value).Value is not null)
                    .Select(p => new { SubsumedByValue = ((Code)p.Value).Value, Concept = concept }))
                .ToLookup(x => x.SubsumedByValue, x => x.Concept);
        }

        private static List<CSDC> applySubsumedBy(ILookup<string, CSDC> lookup, ValueSet.FilterComponent filter)
        {
            var result = new List<CSDC>();
            var root = filter.Value;
            if (root != null)
            {
                addDescendants(lookup, root, result);
            }
            return result;
        }

        //recursively loop through all the children to eventually find all descendants.
        private static void addDescendants(ILookup<string, CSDC> lookup, string parent, List<CSDC> result)
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
