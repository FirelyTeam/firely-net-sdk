#nullable enable

/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Expands valuesets by processing their <c>include</c> and <c>exclude</c> filters. Will create an in-place expansion.
    /// </summary>
    public class ValueSetExpander
    {
        /// <summary>
        /// Settings to control the behaviour of the expansion.
        /// </summary>
        public ValueSetExpanderSettings Settings { get; }

        /// <summary>
        /// Create a new expander with specific settings.
        /// </summary>
        /// <param name="settings"></param>
        public ValueSetExpander(ValueSetExpanderSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Create a new expander with default settings
        /// </summary>
        public ValueSetExpander() : this(ValueSetExpanderSettings.CreateDefault())
        {
            // nothing
        }

        /// <summary>
        /// Expand the <c>include</c> and <c>exclude</c> filters. Creates the <c></c>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Tasks.Task ExpandAsync(ValueSet source) => expandAsync(source, new());

        private async Tasks.Task expandAsync(ValueSet source, Stack<string> inclusionChain)
        {
            // Note we are expanding the valueset in-place, so it's up to the caller to decide whether
            // to clone the valueset, depending on store and performance requirements.
            source.Expansion = ValueSet.ExpansionComponent.Create();
            setExpansionParameters(source.Expansion);

            try
            {
                inclusionChain.Push(source.Url!);
                await handleCompose(source, inclusionChain).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Expansion failed - remove (partial) expansion
                source.Expansion = null;
                throw;
            }
            finally
            {
                inclusionChain.Pop();
            }
        }

        private void setExpansionParameters(ValueSet.ExpansionComponent ec)
        {
            if (Settings.IncludeDesignations)
            {
                ec.Parameter.Add(new ValueSet.ParameterComponent
                {
                    Name = "includeDesignations",
                    Value = new FhirBoolean(true)
                });
            }
            //TODO add more parameters to the valuset here when we implement them.
        }

        private async Tasks.Task handleCompose(ValueSet source, Stack<string> inclusionChain)
        {
            if (source.Compose == null) return;

            await handleInclude(source, inclusionChain).ConfigureAwait(false);
            await handleExclude(source, inclusionChain).ConfigureAwait(false);

            //This is expensive, but we should not have any duplicates
            //TODO something like: source.Expansion.Contains = source.Expansion.Contains.Distinct
        }


        private class SystemAndCodeComparer : IEqualityComparer<ValueSet.ContainsComponent>
        {
            public bool Equals(ValueSet.ContainsComponent? x, ValueSet.ContainsComponent? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null || y is null) return false;

                return x.Code == y.Code && x.System == y.System;
            }

            public int GetHashCode(ValueSet.ContainsComponent obj) => (obj.Code ?? "").GetHashCode() ^ (obj.System ?? "").GetHashCode();
        }

        private static readonly IEqualityComparer<ValueSet.ContainsComponent> _systemAndCodeComparer = new SystemAndCodeComparer();

        // This function contains the main logic of expanding an include/exclude ConceptSet.
        // It processes mainly two parts, which each return 0..* expanded ContainsComponents:
        // * The "System" group (system + filter + concepts).
        // * The "ValueSet" group (valueset)
        // The results of both of these parts are then intersected.
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processConceptSet(ValueSet.ConceptSetComponent conceptSet, Stack<string> inclusionChain)
        {
            // vsd-1
            if (!conceptSet.ValueSetElement.Any() && conceptSet.System == null)
                throw Error.InvalidOperation($"Encountered a ConceptSet with neither a 'system' nor a 'valueset'");

            // Process the system group
            var systemResult = await processSystemGroup(conceptSet).ConfigureAwait(false);

            // Process the ValueSet group
            var valueSetResult = await processValueSetGroup(conceptSet, inclusionChain).ConfigureAwait(false);

            // > For each compose.include: (...) Add the intersection of the result set from the system(step 1) and all of the result sets from the value sets(step 2) to the expansion.
            // Most of the time, the expansion contains stuff from either the system (+enumerated concepts) or the valuesets.
            // If that is the case, return the result directly. If both were specified, we need to calculate the intersection.
            return (systemResult, valueSetResult) switch
            {
                { systemResult.Count: 0, valueSetResult.Count: 0 } => systemResult, // just return an empty list
                { systemResult.Count: > 0, valueSetResult.Count: 0 } => systemResult,
                { systemResult.Count: 0, valueSetResult.Count: > 0 } => valueSetResult,
                _ => systemResult.Intersect(valueSetResult, _systemAndCodeComparer).ToList()
            };
        }

        // > For each valueSet, find the referenced value set by ValueSet.url, expand that
        // > to produce a collection of result sets.This means that expansion across imports is a recursive process.
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processValueSetGroup(ValueSet.ConceptSetComponent conceptSet, Stack<string> inclusionChain)
        {
            var result = new List<ValueSet.ContainsComponent>();

            if (conceptSet.ValueSetElement.Any())
            {
                // > valueSet(s) only: Codes are 'selected' for inclusion if they are in all the referenced value sets
                // "all the referenced sets" means we need to calculate the intersection of the expanded valuesets.
                var expanded = await Tasks.Task.WhenAll(
                    conceptSet.ValueSet!.Where(vs => vs is not null)
                        .Select(vs => expandValueSetAndFilterOnSystem(vs!))).ConfigureAwait(false);
                var concepts = expanded.Length == 1 ? expanded.Single() : expanded.Aggregate((l, r) => l.Intersect(r, _systemAndCodeComparer));

                addCapped(result, concepts, $"Import of valuesets '{string.Join(",", conceptSet.ValueSet!)}' would result in an expansion larger than the maximum expansion size.");

                // > valueSet and System: Codes are 'selected' for inclusion if they are selected by the code system selection (after checking for concept and filter) and if they are in all the referenced value sets
                // If a System was specified, simulate a intersection between the codesystem and the valuesets by filtering on the
                // codesystem's canonical. See previous if.
                IEnumerable<ValueSet.ContainsComponent> filterOnSystem(IEnumerable<ValueSet.ContainsComponent> innerConcepts) =>
                    conceptSet.System is not null ? innerConcepts.Where(c => c.System == conceptSet.System) : innerConcepts;

                async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> expandValueSetAndFilterOnSystem(string canonical)
                {
                    var expansion = await getExpansionForValueSet(canonical, inclusionChain).ConfigureAwait(false);
                    return filterOnSystem(expansion);
                }
            }

            return result;
        }

        // > If there is a system, identify the correct version of the code system, and then:
        // > * If there are no codes or filters, add every code in the code system to the result set.
        // > * If codes are listed, check that they are valid, and check their active status, and if ok, add them to the result set(the parameters to the $expand operation may be used to control whether active codes are included).
        // > * If any filters are present, process them in order(as explained above), and add the intersection of their results to the result set.
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processSystemGroup(ValueSet.ConceptSetComponent conceptSet)
        {
            var result = new List<ValueSet.ContainsComponent>();

            if (conceptSet.System != null)
            {
                // We should probably really have to look this code up in the original codesystem to know something about 'abstract'
                // and what would we do with a hierarchy if we encountered that in the include?
                // Filter and Concept are mutually exclusive (vsd-3)
                if (conceptSet.Filter.Any())
                {
                    var filteredConcepts = await CodeSystemFilterProcessor.FilterConceptsFromCodeSystem(conceptSet.System, conceptSet.Filter, Settings);
                    addCapped(result, filteredConcepts, $"Adding the filtered concepts to the expansion would result in a valueset larger than the maximum expansion size.");
                }

                else if (conceptSet.Concept.Any())
                {
                    var convertedConcepts = conceptSet.Concept.Select(c =>
                        ContainsSetExtensions.BuildContainsComponent(conceptSet.System, conceptSet.Version!, c.Code!, c.Display!, Settings.IncludeDesignations ? c.Designation : null));

                    addCapped(result, convertedConcepts, $"Adding the enumerated concepts to the expansion would result in a valueset larger than the maximum expansion size.");
                }
                else if (!conceptSet.ValueSetElement.Any())
                {
                    // Do a full import of the codesystem. Conceptually, if a ValueSet is specified, we should include the
                    // *intersection* of the ValueSets and the System. That is computationally expensive, so instead we will not
                    // include the Codesystem at all if there are valuesets, but include the ValueSets instead, filtering them
                    // on the given system instead (see next if). This is not the same if there are codes in the valueset that
                    // use a system, but are not actually defined within that codesystem, but that sounds illegal to me anyway.
                    var importedConcepts = await getAllConceptsFromCodeSystem(conceptSet.System).ConfigureAwait(false);
                    addCapped(result, importedConcepts, $"Import of full codesystem '{conceptSet.System}' would result in an expansion larger than the maximum expansion size.");
                }
            }

            return result;
        }

        private void addCapped(List<ValueSet.ContainsComponent> dest, IEnumerable<ValueSet.ContainsComponent> source, string message)
        {
            var capacityLeft = Settings.MaxExpansionSize - dest.Count;
            var cappedSource = source.Take(capacityLeft + 1).ToList();

            if (cappedSource.Count == capacityLeft + 1)
                throw new ValueSetExpansionTooBigException(message);

            dest.AddRange(cappedSource);
        }

        private async Tasks.Task handleInclude(ValueSet source, Stack<string> inclusionChain)
        {
            if (source.Compose?.Include.Any() != true) return;

            int csIndex = 0;
            foreach (var include in source.Compose.Include)
            {
                var includedConcepts = await processConceptSet(include, inclusionChain).ConfigureAwait(false);

                // Yes, exclusion could make this smaller again, but alas, before we have processed those we might have run out of memory
                addCapped(source.Expansion!.Contains, includedConcepts, $"Inclusion of {includedConcepts.Count} concepts from conceptset #{csIndex}' to  " +
                        $"valueset '{source.Url}' ({source.Expansion.Total} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original + includedConcepts.CountConcepts();
                csIndex += 1;
            }
        }

        private async Tasks.Task handleExclude(ValueSet source, Stack<string> inclusionChain)
        {
            if (source.Compose?.Exclude.Any() != true) return;

            foreach (var exclude in source.Compose.Exclude)
            {
                var excludedConcepts = await processConceptSet(exclude, inclusionChain).ConfigureAwait(false);

                source.Expansion!.Contains.Remove(excludedConcepts);

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original - excludedConcepts.CountConcepts();
            }
        }

        private async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> getExpansionForValueSet(string uri, Stack<string> inclusionChain)
        {
            if (inclusionChain.Contains(uri))
                throw new TerminologyServiceException($"ValueSet expansion encountered a cycling dependency from {inclusionChain.Peek()} back to {uri}.");

            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve valueset '{uri}', so the expansion cannot be completed.");

            var importedVs = await Settings.ValueSetSource.AsAsync().FindValueSetAsync(uri).ConfigureAwait(false)
                ?? throw new ValueSetUnknownException($"The ValueSet expander cannot find valueset '{uri}', so the expansion cannot be completed.");
            if (!importedVs.HasExpansion) await expandAsync(importedVs, inclusionChain).ConfigureAwait(false);

            return importedVs.HasExpansion
                ? importedVs.Expansion!.Contains
                : throw new ValueSetUnknownException($"Expansion returned neither an error, nor an expansion for ValueSet with canonical reference '{uri}'");
        }

        private async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> getAllConceptsFromCodeSystem(string uri)
        {
            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve codesystem '{uri}', so the expansion cannot be completed.");

            var importedCs = await Settings.ValueSetSource.AsAsync().FindCodeSystemAsync(uri).ConfigureAwait(false)
                ?? throw new ValueSetUnknownException($"The ValueSet expander cannot find codesystem '{uri}', so the expansion cannot be completed.");

            if(importedCs.Compositional is true)
                throw new ValueSetExpansionTooComplexException($"The ValueSet expander cannot expand compositional code system '{uri}', so the expansion cannot be completed.");

#if STU3
            var contentNotPresent = importedCs.Content == CodeSystem.CodeSystemContentMode.NotPresent;
#else
            var contentNotPresent = importedCs.Content == CodeSystemContentMode.NotPresent;
#endif
            if(contentNotPresent)
                throw new ValueSetExpansionTooComplexException($"The ValueSet expander cannot expand code system '{uri}' without content, so the expansion cannot be completed.");

            return importedCs.Concept.Select(c => c.ToContainsComponent(importedCs, Settings));
        }
    }


    public static class ContainsSetExtensions
    {
        internal static ValueSet.ContainsComponent BuildContainsComponent(string system, string version, string code, string display, List<ValueSet.DesignationComponent>? designations = null, IEnumerable<ValueSet.ContainsComponent>? children = null)
        {
            return new ValueSet.ContainsComponent
            {
                System = system,
                Code = code,
                Display = display,
                Version = version,
                Designation = designations!,
                Contains = children?.ToList()!
            };

        }

        public static ValueSet.ContainsComponent Add(this List<ValueSet.ContainsComponent> dest, string system, string version, string code, string display, List<ValueSet.DesignationComponent>? designations = null, IEnumerable<ValueSet.ContainsComponent>? children = null)
        {
            var newContains = BuildContainsComponent(system, version, code, display, designations, children);
            dest.Add(newContains);

            return newContains;
        }

        public static void Remove(this List<ValueSet.ContainsComponent> dest, string? system, string? code)
        {
            var children = dest.Where(c => c.System == system && c.Code == code).SelectMany(c => c.Contains).ToList();
            dest.RemoveAll(c => c.System == system && c.Code == code);

            //add children back to the list, they do not necessarily need to be removed when the parent is removed.
            dest.AddRange(children);

            // Look for this code in children too
            foreach (var component in dest)
            {
                if (component.Contains.Any())
                    component.Contains.Remove(system, code);
            }
        }

        public static void Remove(this List<ValueSet.ContainsComponent> dest, List<ValueSet.ContainsComponent> source)
        {
            foreach (var sourceConcept in source)
            {
                dest.Remove(sourceConcept.System, sourceConcept.Code);

                //check if there are children that need to be removed too.
                if (sourceConcept.Contains.Any())
                    dest.Remove(sourceConcept.Contains);
            }

        }

        internal static ValueSet.ContainsComponent ToContainsComponent(this CodeSystem.ConceptDefinitionComponent source, CodeSystem system, ValueSetExpanderSettings settings)
        {
            var newContains = new ValueSet.ContainsComponent
            {
                System = system.Url,
                Version = system.Version,
                Code = source.Code,
                Display = source.Display
            };

            newContains.System = system.Url;
            newContains.Version = system.Version;
            newContains.Code = source.Code;
            newContains.Display = source.Display;
            if (settings.IncludeDesignations)
                newContains.Designation = source.Designation.toValueSetDesignations();

            var abstractProperty = source.ListConceptProperties(system, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).SingleOrDefault();
            if (abstractProperty?.Value is FhirBoolean isAbstract)
                newContains.Abstract = isAbstract.Value;

            var inactiveProperty = source.ListConceptProperties(system, CodeSystem.CONCEPTPROPERTY_STATUS).SingleOrDefault();
            if (inactiveProperty?.Value is FhirBoolean isInactive)
                newContains.Inactive = isInactive.Value;

#if !STU3
            if (source.Property.Any())
            {
                newContains.Property = source.Property.Select(p => new ValueSet.ConceptPropertyComponent { Code = p.Code, Value = p.Value }).ToList();
            }
#endif

            if (source.Concept.Any())
                newContains.Contains.AddRange(
                    source.Concept.Select(c => c.ToContainsComponent(system, settings)));

            return newContains;
        }

        private static List<ValueSet.DesignationComponent> toValueSetDesignations(this List<CodeSystem.DesignationComponent> csDesignations)
        {
            var vsDesignations = new List<ValueSet.DesignationComponent>();
            csDesignations.ForEach(d => vsDesignations.Add(d.toValueSetDesignation()));
            return vsDesignations;
        }

        private static ValueSet.DesignationComponent toValueSetDesignation(this CodeSystem.DesignationComponent csDesignation)
        {
            return new ValueSet.DesignationComponent
            {
                Language = csDesignation.Language,
                Use = csDesignation.Use,
                Value = csDesignation.Value
            };
        }



    }
}

#nullable restore