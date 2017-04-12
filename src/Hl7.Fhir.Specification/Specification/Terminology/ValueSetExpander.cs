/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ValueSetExpander
    {
        public ValueSetExpanderSettings Settings { get; }
        public ValueSetExpander(ValueSetExpanderSettings settings)
        {
            Settings = settings;
        }

        public ValueSetExpander() : this(ValueSetExpanderSettings.Default)
        {
            // nothing
        }


        public void Expand(ValueSet source)
        {
            // Note we are expanding the valueset in-place, so it's up to the caller to decide whether
            // to clone the valueset, depending on store and performance requirements.

            source.Expansion = ValueSet.ExpansionComponent.Create();

            try
            {
                handleCompose(source);
            }
            catch(Exception e)
            {
                // Expansion failed - remove (partial) expansion
                source.Expansion = null;
                throw e;
            }
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

        private void handleCompose(ValueSet source)
        {
            if (source.Compose == null) return;

            // handleImport(source);
            handleInclude(source);
            handleExclude(source);
        }


        private IEnumerable<ValueSet.ContainsComponent> collectConcepts(ValueSet.ConceptSetComponent conceptSet)
        {
            if (conceptSet.Filter.Any())
                throw Error.NotSupported($"ConceptSets with a filter are not yet supported.");

            List<ValueSet.ContainsComponent> result = new List<ValueSet.ContainsComponent>();

            if (conceptSet.System != null)
            {
                if (conceptSet.Concept.Any())
                {
                    foreach (var concept in conceptSet.Concept)
                    {
                        // We'd probably really have to look this code up in the original ValueSet (by system) to know something about 'abstract'
                        // and what would we do with a hierarchy if we encountered that in the include?
                        result.Add(conceptSet.System, conceptSet.Version, concept.Code, concept.Display);
                    }
                }
                else
                {
                    // import the whole system
                    throw Error.NotSupported($"ConceptSet specifies just a 'system' ('{conceptSet.System}') and no enumerated concepts to include.");
                }
            }
            else if (conceptSet.ValueSet != null)
            {
                if (conceptSet.ValueSet.Count() > 1)
                    throw Error.NotSupported($"ConceptSets with multiple valuesets are not yet supported.");

                if (importedVs.Expansion.Total + source.Expansion.Total > Settings.MaxExpansionSize)
                    throw new ValueSetExpansionTooBigException($"Import of valueset '{importedVs.Url}' ({importedVs.Expansion.Total} concepts) into " +
                        $"valueset '{source.Url}' ({source.Expansion.Total} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

            }
            else
                throw Error.InvalidOperation($"Encountered a ConceptSet with neither a 'system' nor a 'valueset'");

            return result;
        }

        private void handleInclude(ValueSet source)
        {
            if (!source.Compose.Include.Any()) return;

            foreach (var include in source.Compose.Include)
            {
                if (!include.Concept.Any())
                    throw Error.NotSupported($"Expansion for ValueSet '{source.Url}' uses an include with just a system ('{include.System}') and no enumerated concepts to include.");

                if (include.Filter.Any())
                    throw Error.NotSupported($"Expansion for ValueSet '{source.Url}' uses a filter to include concepts, which is not supported.");

                // Yes, exclusion could make this smaller again, but alas, before we have processed those we might have run out of memory
                if (source.Expansion.Total + include.Concept.Count > Settings.MaxExpansionSize)
                    throw new ValueSetExpansionTooBigException($"Inclusion of {include.Concept.Count} concepts from '{include.System}' to  " +
                        $"valueset '{source.Url}' ({source.Expansion.Total} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                foreach (var concept in include.Concept)
                {
                    // We'd probably really have to look this code up in the original ValueSet (by system) to know something about 'abstract'
                    // and what would we do with a hierarchy if we encountered that in the include?
                    addToExpansion(include.System, include.Version, concept.Code, concept.Display, null, source.Expansion.Contains);
                }

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original + include.Concept.Count;
            }
        }


        private List<ValueSet.ContainsComponent> getExpansionForValueSet(string uri)
        {
            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No valueset resolver available to valueset '{uri}', " +
                        "set ValueSetExpander.Settings.ValueSetSource to fix.");

            var importedVs = Settings.ValueSetSource.FindValueSet(uri);
            if (importedVs == null) throw new ResourceReferenceNotFoundException(uri, $"Cannot resolve canonical reference '{uri}' to ValueSet");

            if (!importedVs.HasExpansion) Expand(importedVs);

            if (importedVs.HasExpansion)
                return importedVs.Expansion.Contains;
            else
                throw Error.InvalidOperation($"Expansion returned neither an error, nor an expansion for ValueSet with canonical reference '{uri}'");
        }

        private List<ValueSet.ContainsComponent> getConceptsFromCodeSystem(string uri)
        {
            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation($"No terminology service available to resolve references to codesystem '{uri}', " +
                        "set ValueSetExpander.Settings.ValueSetSource to fix.");

            var importedCs = Settings.ValueSetSource.FindCodeSystem(uri);
            if (importedCs == null) throw new ResourceReferenceNotFoundException(uri, $"Cannot resolve canonical reference '{uri}' to CodeSystem");

            var result = new List<ValueSet.ContainsComponent>();
            result.AddRange(importedCs.Concept.Select(c => c.ToContainsComponent(importedCs.Url, importedCs.Version)));
            return result;
        }


        //private void handleExclude(ValueSet source)
        //{
        //    if (!source.Compose.Exclude.Any()) return;

        //    foreach (var exclude in source.Compose.Exclude)
        //    {
        //        if (!exclude.Concept.Any())
        //            throw Error.NotSupported($"Expansion for ValueSet '{source.Url}' uses an exclude with just a system ('{exclude.System}') and no enumerated concepts to exclude.");

        //        if (exclude.Filter.Any())
        //            throw Error.NotSupported($"Expansion for ValueSet '{source.Url}' uses a filter to exclude concepts, which is not supported.");

        //        foreach (var concept in exclude.Concept)
        //        {
        //            removeFromExpansion(exclude.System, concept.Code, source.Expansion.Contains);
        //        }

        //        var original = source.Expansion.Total ?? 0;
        //        source.Expansion.Total = original + exclude.Concept.Count;
        //    }
        //}


    }


    public class ValueSetExpanderSettings
    {
        public static ValueSetExpanderSettings Default = new ValueSetExpanderSettings();

        public IResourceResolver ValueSetSource { get; set; }

        public int MaxExpansionSize { get; set; } = 500;
    }

    internal static class ContainsSetExtensions
    {
        public static ValueSet.ContainsComponent Add(this List<ValueSet.ContainsComponent> dest, string system, string version, string code, string display, IEnumerable<ValueSet.ContainsComponent> children = null)
        {
            var newContains = new ValueSet.ContainsComponent();

            newContains.System = system;
            newContains.Code = code;
            newContains.Display = display;
            newContains.Version = version;

            if(children != null)
                newContains.Contains = new List<ValueSet.ContainsComponent>(children);

            dest.Add(newContains);

            return newContains;
        }

        public static ValueSet.ContainsComponent Add(this List<ValueSet.ContainsComponent> dest, CodeSystem.ConceptDefinitionComponent definition, string system, string version)
        {
            var newContains = definition.ToContainsComponent(system, version);
            dest.Add(newContains);

            return newContains;
        }

        public static void Remove(List<ValueSet.ContainsComponent> dest, string system, string code)
        {
            dest.RemoveAll(c => c.System == system && c.Code == code);
        }


        public static ValueSet.ContainsComponent ToContainsComponent(this CodeSystem.ConceptDefinitionComponent source, string system, string version = null)
        {
            var newContains = new ValueSet.ContainsComponent();

            newContains.System = system;
            newContains.Version = version;
            newContains.Code = source.Code;
            newContains.Display = source.Display;

            if (source.Concept.Any())
                newContains.Children.Concat(source.Concept.Select(c => c.ToContainsComponent(system, version)));

            return newContains;
        }
    }
}
