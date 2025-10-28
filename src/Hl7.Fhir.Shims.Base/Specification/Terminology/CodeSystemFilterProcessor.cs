using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using CSDC = Hl7.Fhir.Model.CodeSystem.ConceptDefinitionComponent;
using Tasks = System.Threading.Tasks;

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
        internal async static Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> FilterConceptsFromCodeSystem(string codeSystemUri, IEnumerable<ValueSet.FilterComponent> filters, ValueSetExpanderSettings settings)
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

        private static List<CSDC> applyFilters(IEnumerable<ValueSet.FilterComponent> filters, CodeSystem codeSystem)
        {
            var result = codeSystem.Concept;
            var properties = codeSystem.Property;

            foreach (var filter in filters)
            {
                result = applyFilter(result, properties, filter).ToList();
            }

            return result;
        }

        private static IEnumerable<CSDC> applyFilter(IEnumerable<CSDC> concepts, IEnumerable<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter)
        {
            return filter.Op switch
            {
                FilterOperator.IsA => applyFilterBasedOnHierarchy(concepts, properties, filter, applyIsAfFilterUsingSubsumedBy, applyIsAFilterToANestedHierarchy),
                FilterOperator.IsNotA => applyFilterBasedOnHierarchy(concepts, properties, filter, applyIsNotAFilterUsingSubsumedBy, applyIsNotAFilterToANestedHierarchy),
                FilterOperator.DescendentOf => applyFilterBasedOnHierarchy(concepts, properties, filter, applyDescendantsOfFilterUsingSubsumedBy, applyDescentantsOfFilterToANestedHierarchy),
                _ => throw new ValueSetExpansionTooComplexException($"ConceptSets with filter `{filter.Op.GetLiteral()}` are not yet supported.")
            };
        }

        private static IEnumerable<CSDC> applyFilterBasedOnHierarchy(IEnumerable<CSDC> concepts, IEnumerable<CodeSystem.PropertyComponent> properties, ValueSet.FilterComponent filter, Func<IEnumerable<CSDC>, ValueSet.FilterComponent, IEnumerable<CSDC>> applySubsumedByFilter, Func<IEnumerable<CSDC>, ValueSet.FilterComponent, IEnumerable<CSDC>> applyFilterToANestedHierarchy)
        {
            //find descendants based on subsumedBy
            if (properties.Any(p => p.Code == SUBSUMEDBYCODE))
            {
                //first flatten the codes
                var flattened = concepts.Flatten();
                return applySubsumedByFilter(concepts, filter);
            }
            else
            {
                //SubsumedBy is not used, we should only check for a nested hierarchy, and include the code and it's descendants
                return applyFilterToANestedHierarchy(concepts, filter);
            }
        }


        private static IEnumerable<CSDC> applyDescentantsOfFilterToANestedHierarchy(IEnumerable<CSDC> concepts, ValueSet.FilterComponent filter)
        {
            // look for the code and return it's children (which have their children included already)
            return (filter.Value is not null && concepts.FindCode(filter.Value) is { } concept) ? concept.Concept : [];
        }

        private static IEnumerable<CSDC> applyIsAfFilterUsingSubsumedBy(IEnumerable<CSDC> flattened, ValueSet.FilterComponent filter)
        {
            var result = new List<CSDC>();

            //first find the parent itself (if it's in the CodeSystem)
            var conceptDefinitionComponents = flattened.ToList();
            if (filter.Value is not null && conceptDefinitionComponents.FindCode(filter.Value) is { } concept)
                result.Add(concept);

            //then find the descendants
            List<CSDC> descendants = applyDescendantsOfFilterUsingSubsumedBy(conceptDefinitionComponents, filter);
            result.AddRange(descendants);

            return result;
        }

        private static IEnumerable<CSDC> applyIsAFilterToANestedHierarchy(IEnumerable<CSDC> concepts, ValueSet.FilterComponent filter)
        {
            //just look for the code, because descendants are included
            return filter.Value is not null && concepts.FindCode(filter.Value) is { } concept ? [concept] : [];
        }

        private static IEnumerable<CSDC> applyIsNotAFilterUsingSubsumedBy(IEnumerable<CSDC> flattened, ValueSet.FilterComponent filter)
        {
            var conceptDefinitionComponents = flattened.ToList();
            var result = new List<CSDC>(conceptDefinitionComponents);

            //first find the parent itself (if it's in the CodeSystem) and remove it
            if (filter.Value is not null && conceptDefinitionComponents.FindCode(filter.Value) is { } concept)
                result.Remove(concept);
            //then find the descendants, and remove them
            List<CSDC> descendants = applyDescendantsOfFilterUsingSubsumedBy(conceptDefinitionComponents, filter);

            result.RemoveAll(descendants.Contains);
            return result;
        }

        private static IEnumerable<CSDC> applyIsNotAFilterToANestedHierarchy(IEnumerable<CSDC> concepts, ValueSet.FilterComponent filter)
        {
            //We should only check for a nested hierarchy, and exclude the code and thereby it's included descendants
            return concepts.ToList().RemoveCode(filter.Value ?? throw new InvalidOperationException("Filter value is null"));
        }

        private static List<CSDC> applyDescendantsOfFilterUsingSubsumedBy(IEnumerable<CSDC> flattened, ValueSet.FilterComponent filter)
        {
            //Create a lookup which lists children by parent.              
            var childrenLookup = createSubsumedByLookup(flattened);

            //find descendants based on that lookup
            var descendants = applySubsumedBy(childrenLookup, filter);
            return descendants;
        }

        private static ILookup<string, CSDC> createSubsumedByLookup(IEnumerable<CSDC> flattenedConcepts)
        {
            return flattenedConcepts
                .SelectMany(concept => concept.Property
                    .Where(p => p is { Code: SUBSUMEDBYCODE, Value: Code { Value: not null } })
                    .Select(p => new { SubsumedByValue = ((Code)p.Value!).Value!, Concept = concept }))
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
            if (lookup[parent] is not { } children) return;

            foreach (var child in children)
            {
                result.Add(child);

                if (child.Code is not null)
                {
                    parent = child.Code;
                    addDescendants(lookup, parent, result);
                }
            }
        }

    }
}

#nullable restore