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
using T=System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ValueSetExpander
    {

//ValueSetExpander keeps throwing TerminologyService Exceptions to not change the public interface.
#pragma warning disable 0618

        public ValueSetExpanderSettings Settings { get; }

        public ValueSetExpander(ValueSetExpanderSettings settings)
        {
            Settings = settings;
        }

        public ValueSetExpander() : this(ValueSetExpanderSettings.CreateDefault())
        {
            // nothing
        }

        [Obsolete("ValueSetExpander now works best with asynchronous resolvers. Use ExpandAsync() instead.")]
        public void Expand(ValueSet source) => TaskHelper.Await(() => ExpandAsync(source));


        public async T.Task ExpandAsync(ValueSet source)
        {
            // Note we are expanding the valueset in-place, so it's up to the caller to decide whether
            // to clone the valueset, depending on store and performance requirements.
            source.Expansion = ValueSet.ExpansionComponent.Create();
            setExpansionParameters(source);

            try
            {
                await handleCompose(source).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Expansion failed - remove (partial) expansion
                source.Expansion = null;
                throw;
            }
            
        }

        private void setExpansionParameters(ValueSet vs)
        {
            vs.Expansion.Parameter = new List<ValueSet.ParameterComponent>();
            if(Settings.IncludeDesignations)
            {
                vs.Expansion.Parameter.Add(new ValueSet.ParameterComponent
                {
                    Name = "includeDesignations",
                    Value = new FhirBoolean(true)
                }); 
            }
            //TODO add more parameters to the valuset here when we implement them.
        }


        //private int copyToExpansion(string system, string version, IEnumerable<ValueSet.ConceptDefinitionComponent> source, List<ValueSet.ContainsComponent> dest)
        //{
        //    int added = 0;

        //    foreach (var concept in source)
        //    {
        //        bool isDeprecated = concept.GetDeprecated() ?? false;

        //        if (!isDeprecated)
        //        {
        //            var newContains = addToExpansion(system, version, concept.Code, concept.Display, concept.Abstract, dest);
        //            added += 1;

        //            if (concept.Concept != null && concept.Concept.Any())
        //                added += copyToExpansion(system, version, concept.Concept, newContains.Contains);
        //        }
        //    }

        //    return added;
        //}

        private async T.Task handleCompose(ValueSet source)
        {
            if (source.Compose == null) return;

            // handleImport(source);
            await handleInclude(source).ConfigureAwait(false);
            await handleExclude(source).ConfigureAwait(false);
        }


        private async T.Task<List<ValueSet.ContainsComponent>> collectConcepts(ValueSet.ConceptSetComponent conceptSet)
        {
            List<ValueSet.ContainsComponent> result = new List<ValueSet.ContainsComponent>();

            if (!conceptSet.ValueSet.Any() && conceptSet.System == null)
                throw Error.InvalidOperation($"Encountered a ConceptSet with neither a 'system' nor a 'valueset'");

            if (conceptSet.System != null)
            {
                if (conceptSet.Filter.Any())
                    throw new ValueSetExpansionTooComplexException($"ConceptSets with a filter are not yet supported.");

                if (conceptSet.Concept.Any())
                {
                    foreach (var concept in conceptSet.Concept)
                    {
                        // We'd probably really have to look this code up in the original ValueSet (by system) to know something about 'abstract'
                        // and what would we do with a hierarchy if we encountered that in the include?
                        if(Settings.IncludeDesignations)
                        {
                            result.Add(conceptSet.System, conceptSet.Version, concept.Code, concept.Display, concept.Designation);
                        }
                        else
                        {
                            result.Add(conceptSet.System, conceptSet.Version, concept.Code, concept.Display);
                        }
                       
                    }
                }
                else
                {
                    // Do a full import of the codesystem
                    var importedConcepts = await getConceptsFromCodeSystem(conceptSet.System).ConfigureAwait(false);
                    import(result, importedConcepts, conceptSet.System);
                }
            }

            if (conceptSet.ValueSet.Any())
            {
                if (conceptSet.ValueSet.Count() > 1)
                    throw new ValueSetExpansionTooComplexException($"ConceptSets with multiple valuesets are not yet supported.");
                if (conceptSet.System != null)
                    throw new ValueSetExpansionTooComplexException($"ConceptSets with combined 'system' and 'valueset'(s) are not yet supported.");

                var importedVs = conceptSet.ValueSet.Single();
                var concepts = await getExpansionForValueSet(importedVs).ConfigureAwait(false);
                import(result, concepts, importedVs);
            }
            
            return result;

            void import(List<ValueSet.ContainsComponent> dest, List<ValueSet.ContainsComponent> source, string importeeUrl)
            {
                if (dest.Count + source.Count > Settings.MaxExpansionSize)
                    throw new ValueSetExpansionTooBigException($"Import of '{importeeUrl}' ({source.Count} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                dest.AddRange(source);
            }
        }

        private async T.Task handleInclude(ValueSet source)
        {
            if (!source.Compose.Include.Any()) return;

            int csIndex = 0;
            foreach (var include in source.Compose.Include)
            {
                var includedConcepts = await collectConcepts(include).ConfigureAwait(false);

                // Yes, exclusion could make this smaller again, but alas, before we have processed those we might have run out of memory
                if (source.Expansion.Total + includedConcepts.Count > Settings.MaxExpansionSize)
                    throw new ValueSetExpansionTooBigException($"Inclusion of {includedConcepts.Count} concepts from conceptset #{csIndex}' to  " +
                        $"valueset '{source.Url}' ({source.Expansion.Total} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                source.Expansion.Contains.AddRange(includedConcepts);

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original + includedConcepts.CountConcepts();
                csIndex += 1;
            }
        }

        private async T.Task handleExclude(ValueSet source)
        {
            if (!source.Compose.Exclude.Any()) return;

            foreach (var exclude in source.Compose.Exclude)
            {
                var excludedConcepts = await collectConcepts(exclude).ConfigureAwait(false);

                source.Expansion.Contains.Remove(excludedConcepts);

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original - excludedConcepts.CountConcepts();
            }
        }


        private async T.Task<List<ValueSet.ContainsComponent>> getExpansionForValueSet(string uri)
        {
            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to resolve valueset '{uri}', " +
                        "set ValueSetExpander.Settings.ValueSetSource to fix.");

            var importedVs = await Settings.ValueSetSource.AsAsync().FindValueSetAsync(uri).ConfigureAwait(false);
            if (importedVs == null) throw new ValueSetUnknownException($"Cannot resolve canonical reference '{uri}' to ValueSet");

            if (!importedVs.HasExpansion) await ExpandAsync(importedVs).ConfigureAwait(false);

            if (importedVs.HasExpansion)
                return importedVs.Expansion.Contains;
            else
                throw new ValueSetUnknownException($"Expansion returned neither an error, nor an expansion for ValueSet with canonical reference '{uri}'");
        }

        private async T.Task<List<ValueSet.ContainsComponent>> getConceptsFromCodeSystem(string uri)
        {
            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No terminology service available to resolve references to codesystem '{uri}', " +
                        "set ValueSetExpander.Settings.ValueSetSource to fix.");

            var importedCs = await Settings.ValueSetSource.AsAsync().FindCodeSystemAsync(uri).ConfigureAwait(false);
            if (importedCs == null) throw new ValueSetUnknownException($"Cannot resolve canonical reference '{uri}' to CodeSystem");

            var result = new List<ValueSet.ContainsComponent>();
            result.AddRange(importedCs.Concept.Select(c => c.ToContainsComponent(importedCs, Settings)));

            return result;
        }
    }


    public static class ContainsSetExtensions
    {
        public static ValueSet.ContainsComponent Add(this List<ValueSet.ContainsComponent> dest, string system, string version, string code, string display, List<ValueSet.DesignationComponent> designations = null, IEnumerable<ValueSet.ContainsComponent> children = null)
        {
            var newContains = new ValueSet.ContainsComponent
            {
                System = system,
                Code = code,
                Display = display,
                Version = version
            };

            newContains.System = system;
            newContains.Code = code;
            newContains.Display = display;
            newContains.Version = version;
            newContains.Designation = designations;

            if (children != null)
                newContains.Contains = new List<ValueSet.ContainsComponent>(children);

            dest.Add(newContains);

            return newContains;
        }

        public static void Remove(this List<ValueSet.ContainsComponent> dest, string system, string code)
        {
            dest.RemoveAll(c => c.System == system && c.Code == code);

            // Look for this code in children too
            foreach (var component in dest)
            {
                if (component.Contains.Any())
                    component.Contains.Remove(system, code);
            }
        }

        public static void Remove(this List<ValueSet.ContainsComponent> dest, List<ValueSet.ContainsComponent> source)
        {
            //TODO: Pretty unclear what to do with child concepts in the source - they all need to be removed from dest?
            foreach (var sourceConcept in source)
                dest.Remove(sourceConcept.System, sourceConcept.Code);
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
            if(settings.IncludeDesignations)            
                newContains.Designation = source.Designation.ToValueSetDesignations();                    

            var abstractProperty = source.ListConceptProperties(system, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).SingleOrDefault();
            if (abstractProperty?.Value is FhirBoolean isAbstract)
                newContains.Abstract = isAbstract.Value;

            var inactiveProperty = source.ListConceptProperties(system, CodeSystem.CONCEPTPROPERTY_STATUS).SingleOrDefault();
            if (inactiveProperty?.Value is FhirBoolean isInactive)
                newContains.Inactive = isInactive.Value;

            if (source.Concept.Any())
                newContains.Contains.AddRange(
                    source.Concept.Select(c => c.ToContainsComponent(system, settings)));

            return newContains;
        }

        private static List<ValueSet.DesignationComponent> ToValueSetDesignations(this List<CodeSystem.DesignationComponent> csDesignations)
        {
            var vsDesignations = new List<ValueSet.DesignationComponent>();
            csDesignations.ForEach(d => vsDesignations.Add(d.ToValueSetDesignation()));           
            return vsDesignations;
        }

        private static ValueSet.DesignationComponent ToValueSetDesignation(this CodeSystem.DesignationComponent csDesignation)
        {
            return new ValueSet.DesignationComponent
            {
                Language = csDesignation.Language,
                Use = csDesignation.Use,
                Value = csDesignation.Value
            };
        }

    }
    #pragma warning restore
}
