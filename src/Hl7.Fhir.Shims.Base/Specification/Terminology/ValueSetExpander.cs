#nullable enable

/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
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
        /// The names of the $expand parameters this expander accepts in <see cref="ExpandAsync(ValueSet, ExpandParameters)"/>.
        /// </summary>
        /// <remarks>
        /// <c>url</c>, <c>valueSet</c>, <c>valueSetVersion</c> and <c>tx-resource</c> select the value set to expand. The caller
        /// resolves them before calling the expander, so they are accepted but neither applied nor echoed. The other names
        /// are applied to the expansion and echoed in <c>expansion.parameter</c> when the request sends them.
        /// </remarks>
        public static readonly IReadOnlyList<string> SupportedParameters =
        [
            ExpandParameters.URL_ATTRIBUTE, ExpandParameters.VALUE_SET_ATTRIBUTE, ExpandParameters.VALUE_SET_VERSION_ATTRIBUTE,
            ExpandParameters.TX_RESOURCE_ATTRIBUTE, ExpandParameters.INCLUDE_DESIGNATIONS_ATTRIBUTE, ExpandParameters.EXCLUDE_NESTED_ATTRIBUTE,
            ExpandParameters.EXCLUDE_NOT_FOR_UI_ATTRIBUTE
        ];

        // The supported parameters that change the expansion, and are therefore echoed in expansion.parameter.
        private static readonly HashSet<string> _appliedParameters =
        [
            ExpandParameters.INCLUDE_DESIGNATIONS_ATTRIBUTE, ExpandParameters.EXCLUDE_NESTED_ATTRIBUTE, ExpandParameters.EXCLUDE_NOT_FOR_UI_ATTRIBUTE
        ];

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
        /// Expand the <c>include</c> and <c>exclude</c> filters, using only the <see cref="Settings"/>. Creates the <c>expansion</c> in place.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Tasks.Task ExpandAsync(ValueSet source) => expandAsync(source, parametersFromSettings(), new());

        /// <summary>
        /// Expand the <c>include</c> and <c>exclude</c> filters, applying the $expand parameters of the request. Creates the
        /// <c>expansion</c> in place, and echoes every applied parameter in <c>expansion.parameter</c>.
        /// </summary>
        /// <param name="source">The value set to expand.</param>
        /// <param name="parameters">The $expand request. Every parameter must be in <see cref="SupportedParameters"/>.</param>
        /// <exception cref="FhirOperationException">The request contains parameters that are not in <see cref="SupportedParameters"/>.</exception>
        public async Tasks.Task ExpandAsync(ValueSet source, ExpandParameters parameters)
        {
            if (parameters is null) throw Error.ArgumentNull(nameof(parameters));

            parameters.NoDuplicates(ExpandParameters.KnownNoDuplicates);

            var unsupported = parameters.Parameter
                .Select(p => p.Name)
                .Where(name => !SupportedParameters.Contains(name))
                .Distinct()
                .ToList();

            if (unsupported.Count > 0)
                throw FhirOperationException.NotSupported($"The parameter(s) {string.Join(", ", unsupported)} are not supported by the ValueSet expander.");

            await expandAsync(source, parameters, new()).ConfigureAwait(false);
        }

        /// <summary>
        /// Applies the shape the request asks for (flat, without not-for-UI codes) to an expansion the value set already has,
        /// and echoes those parameters. Used for stored expansions that are not regenerated.
        /// </summary>
        /// <remarks>The value set is changed in place; pass a copy when the value set is shared.</remarks>
        internal void ApplyToStoredExpansion(ValueSet valueSet, ExpandParameters parameters)
        {
            if (valueSet.Expansion is null) return;

            IEnumerable<ValueSet.ContainsComponent> contains = valueSet.Expansion.Contains;
            if (expandFlat(parameters)) contains = FlattenContains(contains);
            if (excludeNotForUI(parameters)) contains = RemoveNotForUI(contains);

            var reshaped = contains.ToList();
            var removed = CountCodes(valueSet.Expansion.Contains) - CountCodes(reshaped);
            valueSet.Expansion.Contains = reshaped;
            if (valueSet.Expansion.Total is { } total) valueSet.Expansion.Total = total - removed;

            var echo = new ValueSet.ExpansionComponent();
            setExpansionParameters(echo, parameters);
            foreach (var parameter in echo.Parameter)
            {
                if (parameter.Name == ExpandParameters.INCLUDE_DESIGNATIONS_ATTRIBUTE) continue; // not applied to a stored expansion
                valueSet.Expansion.Parameter.RemoveAll(p => p.Name == parameter.Name);
                valueSet.Expansion.Parameter.Add(parameter);
            }
        }

        private ExpandParameters parametersFromSettings()
        {
            var parameters = new ExpandParameters();
            if (Settings.IncludeDesignations) parameters.IncludeDesignations = new FhirBoolean(true);
            return parameters;
        }

        private bool includeDesignations(ExpandParameters parameters) => parameters.IncludeDesignations?.Value ?? Settings.IncludeDesignations;

        private bool expandFlat(ExpandParameters parameters) => Settings.AlwaysExpandFlat || parameters.ExcludeNested?.Value == true;

        private static bool excludeNotForUI(ExpandParameters parameters) => parameters.ExcludeNotForUI?.Value == true;

        // Imported value sets are expanded with the settings only, as before, since the expander stores their expansion in
        // place and later expansions reuse it. Flattening and leaving out not-for-UI codes is applied to the imported codes instead.
        private ExpandParameters importParameters() => parametersFromSettings();

        private async Tasks.Task expandAsync(ValueSet source, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            // Note we are expanding the valueset in-place, so it's up to the caller to decide whether
            // to clone the valueset, depending on store and performance requirements.
            source.Expansion = ValueSet.ExpansionComponent.Create();
            setExpansionParameters(source.Expansion, parameters);

            try
            {
                inclusionChain.Push(source.Url!);
                await handleCompose(source, parameters, inclusionChain).ConfigureAwait(false);
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

        private static void setExpansionParameters(ValueSet.ExpansionComponent ec, ExpandParameters parameters)
        {
            // Echo every applied parameter, in the order of the request, with the value that was sent.
            foreach (var parameter in parameters.Parameter)
            {
                // All applied parameters are booleans; a value of another type is not applied, so it is not echoed either.
                if (parameter.Name is null || !_appliedParameters.Contains(parameter.Name) || parameter.Value is not FhirBoolean) continue;

                ec.Parameter.Add(new ValueSet.ParameterComponent
                {
                    Name = parameter.Name,
                    Value = (DataType)parameter.Value.DeepCopy()
                });
            }
        }

        private async Tasks.Task handleCompose(ValueSet source, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            if (source.Compose == null) return;

            await handleInclude(source, parameters, inclusionChain).ConfigureAwait(false);
            await handleExclude(source, parameters, inclusionChain).ConfigureAwait(false);

            if (source.Compose.Include.Any())
                source.Expansion!.Total = CountCodes(source.Expansion.Contains);

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
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processConceptSet(ValueSet.ConceptSetComponent conceptSet, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            // vsd-1
            if (!conceptSet.ValueSetElement.Any() && conceptSet.System == null)
                throw Error.InvalidOperation($"Encountered a ConceptSet with neither a 'system' nor a 'valueset'");

            // Process the system group
            var systemResult = await processSystemGroup(conceptSet, parameters).ConfigureAwait(false);

            // Process the ValueSet group
            var valueSetResult = await processValueSetGroup(conceptSet, parameters, inclusionChain).ConfigureAwait(false);

            // > For each compose.include: (...) Add the intersection of the result set from the system(step 1) and all of the result sets from the value sets(step 2) to the expansion.
            // Most of the time, the expansion contains stuff from either the system (+enumerated concepts) or the valuesets.
            // If that is the case, return the result directly. If both select codes, we need to calculate the intersection,
            // over all codes (nested ones included), so the intersection is flat. This is decided on what the conceptSet
            // specifies, not on the results: an empty result on one side means an empty intersection.
            // A system without concepts or filters next to valuesets selects nothing itself; the valueset group already
            // filters on that system (see processValueSetGroup).
            var systemSelects = conceptSet.System is not null && (conceptSet.Concept.Any() || conceptSet.Filter.Any());
            var valueSetsSelect = conceptSet.ValueSetElement.Any();

            return (systemSelects, valueSetsSelect) switch
            {
                (true, true) => FlattenContains(systemResult).Intersect(FlattenContains(valueSetResult), _systemAndCodeComparer).ToList(),
                (false, true) => valueSetResult,
                _ => systemResult
            };
        }

        // > For each valueSet, find the referenced value set by ValueSet.url, expand that
        // > to produce a collection of result sets.This means that expansion across imports is a recursive process.
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processValueSetGroup(ValueSet.ConceptSetComponent conceptSet, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            var result = new List<ValueSet.ContainsComponent>();

            if (conceptSet.ValueSetElement.Any())
            {
                // > valueSet(s) only: Codes are 'selected' for inclusion if they are in all the referenced value sets
                // "all the referenced sets" means we need to calculate the intersection of the expanded valuesets.
                // These are expanded sequentially (rather than in parallel) since the caller-supplied
                // ValueSetSource used to resolve them is not guaranteed to be safe for concurrent use, and
                // the recursive expansion below shares the (non-thread-safe) inclusionChain across calls.
                var canonicals = conceptSet.ValueSet!.Where(vs => vs is not null).ToList();

                // Intersections and the system filter work on every code, so nested codes are flattened first.
                var flatten = expandFlat(parameters) || canonicals.Count > 1 || conceptSet.System is not null;

                var expanded = new List<IEnumerable<ValueSet.ContainsComponent>>();
                foreach (var vs in canonicals)
                {
                    // Not-for-UI entries are removed from each input before intersecting, so an entry marked abstract in one
                    // value set cannot survive through the metadata of another.
                    var imported = await expandValueSetAndFilterOnSystem(vs!).ConfigureAwait(false);
                    expanded.Add(excludeNotForUI(parameters) ? RemoveNotForUI(imported) : imported);
                }
                var concepts = expanded.Count == 1 ? expanded.Single() : expanded.Aggregate((l, r) => l.Intersect(r, _systemAndCodeComparer));

                addCapped(result, concepts, $"Import of valuesets '{string.Join(",", conceptSet.ValueSet!)}' would result in an expansion larger than the maximum expansion size.");

                // > valueSet and System: Codes are 'selected' for inclusion if they are selected by the code system selection (after checking for concept and filter) and if they are in all the referenced value sets
                // If a System was specified, simulate a intersection between the codesystem and the valuesets by filtering on the
                // codesystem's canonical. See previous if.
                IEnumerable<ValueSet.ContainsComponent> filterOnSystem(IEnumerable<ValueSet.ContainsComponent> innerConcepts) =>
                    conceptSet.System is not null ? innerConcepts.Where(c => c.System == conceptSet.System) : innerConcepts;

                async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> expandValueSetAndFilterOnSystem(string canonical)
                {
                    var expansion = await getExpansionForValueSet(canonical, parameters, inclusionChain).ConfigureAwait(false);
                    return filterOnSystem(flatten ? FlattenContains(expansion) : expansion);
                }
            }

            return result;
        }

        // > If there is a system, identify the correct version of the code system, and then:
        // > * If there are no codes or filters, add every code in the code system to the result set.
        // > * If codes are listed, check that they are valid, and check their active status, and if ok, add them to the result set(the parameters to the $expand operation may be used to control whether active codes are included).
        // > * If any filters are present, process them in order(as explained above), and add the intersection of their results to the result set.
        private async Tasks.Task<List<ValueSet.ContainsComponent>> processSystemGroup(ValueSet.ConceptSetComponent conceptSet, ExpandParameters parameters)
        {
            var result = new List<ValueSet.ContainsComponent>();

            if (conceptSet.System != null)
            {
                // We should probably really have to look this code up in the original codesystem to know something about 'abstract'
                // and what would we do with a hierarchy if we encountered that in the include?
                // Filter and Concept are mutually exclusive (vsd-3)
                if (conceptSet.Filter.Any())
                {
                    var (codeSystem, filteredConcepts) = await CodeSystemFilterProcessor.FilterCodeSystemConcepts(conceptSet.System, conceptSet.Filter, Settings).ConfigureAwait(false);
                    addCapped(result, toContainsComponents(filteredConcepts, codeSystem, parameters), $"Adding the filtered concepts to the expansion would result in a valueset larger than the maximum expansion size.");
                }

                else if (conceptSet.Concept.Any())
                {
                    var designations = includeDesignations(parameters);
                    IEnumerable<ValueSet.ConceptReferenceComponent> enumerated = conceptSet.Concept;

                    if (excludeNotForUI(parameters))
                        enumerated = await removeNotSelectable(conceptSet.System, conceptSet.Version, enumerated).ConfigureAwait(false);

                    var convertedConcepts = enumerated.Select(c =>
                        ContainsSetExtensions.BuildContainsComponent(conceptSet.System, conceptSet.Version!, c.Code!, c.Display!, designations ? c.Designation : null));

                    addCapped(result, convertedConcepts, $"Adding the enumerated concepts to the expansion would result in a valueset larger than the maximum expansion size.");
                }
                else if (!conceptSet.ValueSetElement.Any())
                {
                    // Do a full import of the codesystem. Conceptually, if a ValueSet is specified, we should include the
                    // *intersection* of the ValueSets and the System. That is computationally expensive, so instead we will not
                    // include the Codesystem at all if there are valuesets, but include the ValueSets instead, filtering them
                    // on the given system instead (see next if). This is not the same if there are codes in the valueset that
                    // use a system, but are not actually defined within that codesystem, but that sounds illegal to me anyway.
                    var importedConcepts = await getAllConceptsFromCodeSystem(conceptSet.System, parameters).ConfigureAwait(false);
                    addCapped(result, importedConcepts, $"Import of full codesystem '{conceptSet.System}' would result in an expansion larger than the maximum expansion size.");
                }
            }

            return result;
        }

        // Leaves out enumerated codes that the code system marks as not selectable. When the code system cannot be
        // resolved, the codes are kept as listed.
        private async Tasks.Task<IEnumerable<ValueSet.ConceptReferenceComponent>> removeNotSelectable(string system, string? version, IEnumerable<ValueSet.ConceptReferenceComponent> concepts)
        {
            if (Settings.ValueSetSource is null) return concepts;

            var canonical = string.IsNullOrEmpty(version) ? system : $"{system}|{version}";
            var codeSystem = await Settings.ValueSetSource.AsAsync().FindCodeSystemAsync(canonical).ConfigureAwait(false);
            if (codeSystem is null) return concepts;

            return concepts.Where(c => c.Code is null || codeSystem.Concept.FindCode(c.Code) is not { } definition || !definition.IsNotSelectable(codeSystem)).ToList();
        }

        // Turns code system concepts into expansion entries, in the shape the request asks for.
        private IEnumerable<ValueSet.ContainsComponent> toContainsComponents(IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, CodeSystem codeSystem, ExpandParameters parameters)
        {
            var designations = includeDesignations(parameters);
            var notForUI = excludeNotForUI(parameters);

            return expandFlat(parameters)
                ? flatContainsComponents(concepts, codeSystem, designations, notForUI)
                : concepts.SelectMany(c => nestedContainsComponents(c, codeSystem, designations, notForUI));
        }

        // Walks the concept tree in pre-order (depth-first) and yields each concept as a top-level entry without children.
        // The walk is lazy, so addCapped stops reading as soon as the maximum expansion size is exceeded.
        private static IEnumerable<ValueSet.ContainsComponent> flatContainsComponents(IEnumerable<CodeSystem.ConceptDefinitionComponent> concepts, CodeSystem codeSystem, bool includeDesignations, bool excludeNotForUI)
        {
            var stack = new Stack<IEnumerator<CodeSystem.ConceptDefinitionComponent>>();
            stack.Push(concepts.GetEnumerator());

            while (stack.Count > 0)
            {
                var level = stack.Peek();
                if (!level.MoveNext())
                {
                    stack.Pop();
                    continue;
                }

                var concept = level.Current;
                if (!(excludeNotForUI && concept.IsNotSelectable(codeSystem)))
                    yield return concept.ToContainsComponent(codeSystem, includeDesignations, includeChildren: false);

                if (concept.Concept.Any())
                    stack.Push(concept.Concept.GetEnumerator());
            }
        }

        // Builds the nested entries for a concept. A concept left out as not-for-UI is replaced by its children.
        private static IEnumerable<ValueSet.ContainsComponent> nestedContainsComponents(CodeSystem.ConceptDefinitionComponent concept, CodeSystem codeSystem, bool includeDesignations, bool excludeNotForUI)
        {
            if (!excludeNotForUI)
                return [concept.ToContainsComponent(codeSystem, includeDesignations, includeChildren: true)];

            var children = concept.Concept.SelectMany(c => nestedContainsComponents(c, codeSystem, includeDesignations, excludeNotForUI)).ToList();
            if (concept.IsNotSelectable(codeSystem))
                return children;

            var contains = concept.ToContainsComponent(codeSystem, includeDesignations, includeChildren: false);
            contains.Contains.AddRange(children);
            return [contains];
        }

        /// <summary>
        /// Returns the entries and all their nested entries as a flat list, in pre-order (depth-first). Entries that have
        /// children are copied without them, so the source is not changed.
        /// </summary>
        internal static IEnumerable<ValueSet.ContainsComponent> FlattenContains(IEnumerable<ValueSet.ContainsComponent> contains)
        {
            var stack = new Stack<IEnumerator<ValueSet.ContainsComponent>>();
            stack.Push(contains.GetEnumerator());

            while (stack.Count > 0)
            {
                var level = stack.Peek();
                if (!level.MoveNext())
                {
                    stack.Pop();
                    continue;
                }

                var entry = level.Current;
                if (entry.Contains.Any())
                {
                    yield return entry.WithoutChildren();
                    stack.Push(entry.Contains.GetEnumerator());
                }
                else
                    yield return entry;
            }
        }

        /// <summary>
        /// Leaves out the entries that are not for user interfaces: entries with <c>abstract = true</c> and entries without a code.
        /// The children of a left-out entry take its place, so no selectable code is lost. The source is not changed.
        /// </summary>
        internal static IEnumerable<ValueSet.ContainsComponent> RemoveNotForUI(IEnumerable<ValueSet.ContainsComponent> contains)
        {
            foreach (var entry in contains)
            {
                var notForUI = entry.Abstract == true || entry.Code is null;

                if (!entry.Contains.Any())
                {
                    if (!notForUI) yield return entry;
                    continue;
                }

                var children = RemoveNotForUI(entry.Contains).ToList();
                if (notForUI)
                {
                    foreach (var child in children) yield return child;
                    continue;
                }

                var copy = entry.WithoutChildren();
                copy.Contains.AddRange(children);
                yield return copy;
            }
        }

        // Counts the entries that carry a code, nested ones included; grouping entries without a code are not concepts.
        internal static int CountCodes(IEnumerable<ValueSet.ContainsComponent> contains) =>
            contains.Sum(c => (c.Code is null ? 0 : 1) + CountCodes(c.Contains));

        // Adds the source entries to dest, counting every code (nested ones included) against the maximum expansion size.
        private void addCapped(List<ValueSet.ContainsComponent> dest, IEnumerable<ValueSet.ContainsComponent> source, string message)
        {
            var capacityLeft = Settings.MaxExpansionSize - CountCodes(dest);
            var added = new List<ValueSet.ContainsComponent>();
            var count = 0;

            foreach (var entry in source)
            {
                count += (entry.Code is null ? 0 : 1) + CountCodes(entry.Contains);
                if (count > capacityLeft)
                    throw new ValueSetExpansionTooBigException(message);

                added.Add(entry);
            }

            dest.AddRange(added);
        }

        private async Tasks.Task handleInclude(ValueSet source, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            if (source.Compose?.Include.Any() != true) return;

            int csIndex = 0;
            foreach (var include in source.Compose.Include)
            {
                var includedConcepts = await processConceptSet(include, parameters, inclusionChain).ConfigureAwait(false);

                // Yes, exclusion could make this smaller again, but alas, before we have processed those we might have run out of memory
                addCapped(source.Expansion!.Contains, includedConcepts, $"Inclusion of {includedConcepts.CountConcepts()} concepts from conceptset #{csIndex}' to  " +
                        $"valueset '{source.Url}' ({source.Expansion.Contains.CountConcepts()} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                csIndex += 1;
            }
        }

        private async Tasks.Task handleExclude(ValueSet source, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            if (source.Compose?.Exclude.Any() != true) return;

            // Excluded codes are matched on system and code, whatever their shape, and a not-for-UI code that is excluded
            // must still be removed, so the exclusions are processed with neutral parameters.
            var excludeParameters = importParameters();

            foreach (var exclude in source.Compose.Exclude)
            {
                var excludedConcepts = await processConceptSet(exclude, excludeParameters, inclusionChain).ConfigureAwait(false);

                source.Expansion!.Contains.Remove(excludedConcepts);
            }
        }

        private async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> getExpansionForValueSet(string uri, ExpandParameters parameters, Stack<string> inclusionChain)
        {
            if (inclusionChain.Contains(uri))
                throw new TerminologyServiceException($"ValueSet expansion encountered a cycling dependency from {inclusionChain.Peek()} back to {uri}.");

            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve valueset '{uri}', so the expansion cannot be completed.");

            var importedVs = await Settings.ValueSetSource.AsAsync().FindValueSetAsync(uri).ConfigureAwait(false)
                ?? throw new ValueSetUnknownException($"The ValueSet expander cannot find valueset '{uri}', so the expansion cannot be completed.");

            // When the request asks for designations other than the settings, the import is expanded on a copy with that
            // option, so the request-specific result is not stored in the resolved value set.
            var designations = includeDesignations(parameters);
            if (designations != Settings.IncludeDesignations && importedVs.Compose is not null)
            {
                var copy = (ValueSet)importedVs.DeepCopy();
                await expandAsync(copy, new ExpandParameters { IncludeDesignations = new FhirBoolean(designations) }, inclusionChain).ConfigureAwait(false);
                return copy.Expansion!.Contains;
            }

            if (!importedVs.HasExpansion) await expandAsync(importedVs, importParameters(), inclusionChain).ConfigureAwait(false);

            return importedVs.HasExpansion
                ? importedVs.Expansion!.Contains
                : throw new ValueSetUnknownException($"Expansion returned neither an error, nor an expansion for ValueSet with canonical reference '{uri}'");
        }

        private async Tasks.Task<IEnumerable<ValueSet.ContainsComponent>> getAllConceptsFromCodeSystem(string uri, ExpandParameters parameters)
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

            return toContainsComponents(importedCs.Concept, importedCs, parameters);
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

        internal static ValueSet.ContainsComponent ToContainsComponent(this CodeSystem.ConceptDefinitionComponent source, CodeSystem system, ValueSetExpanderSettings settings) =>
            source.ToContainsComponent(system, settings.IncludeDesignations, includeChildren: true);

        internal static ValueSet.ContainsComponent ToContainsComponent(this CodeSystem.ConceptDefinitionComponent source, CodeSystem system, bool includeDesignations, bool includeChildren)
        {
            var newContains = new ValueSet.ContainsComponent
            {
                System = system.Url,
                Version = system.Version,
                Code = source.Code,
                Display = source.Display
            };

            if (includeDesignations)
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

            if (includeChildren && source.Concept.Any())
                newContains.Contains.AddRange(
                    source.Concept.Select(c => c.ToContainsComponent(system, includeDesignations, includeChildren: true)));

            return newContains;
        }

        /// <summary>
        /// Whether the code system marks the concept as not selectable (its <c>notSelectable</c> property is <c>true</c>).
        /// </summary>
        internal static bool IsNotSelectable(this CodeSystem.ConceptDefinitionComponent concept, CodeSystem system) =>
            concept.ListConceptProperties(system, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).Any(p => p.Value is FhirBoolean { Value: true });

        /// <summary>
        /// A copy of the entry without its nested entries. The lists are new, the element values in them are shared with the source.
        /// </summary>
        internal static ValueSet.ContainsComponent WithoutChildren(this ValueSet.ContainsComponent source)
        {
            var copy = new ValueSet.ContainsComponent
            {
                ElementId = source.ElementId,
                Extension = [..source.Extension],
                ModifierExtension = [..source.ModifierExtension],
                SystemElement = source.SystemElement,
                AbstractElement = source.AbstractElement,
                InactiveElement = source.InactiveElement,
                VersionElement = source.VersionElement,
                CodeElement = source.CodeElement,
                DisplayElement = source.DisplayElement,
                Designation = [..source.Designation],
            };
#if !STU3
            copy.Property = [..source.Property];
#endif
            return copy;
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