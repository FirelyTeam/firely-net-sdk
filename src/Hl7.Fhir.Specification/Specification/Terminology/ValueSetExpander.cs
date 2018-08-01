/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
                handleDefine(source);
                handleCompose(source);
            }
            catch(Exception e)
            {
                // Expansion failed - remove (partial) expansion
                source.Expansion = null;
                throw e;
            }
        }

        private void handleDefine(ValueSet source)
        {
            if (source.CodeSystem == null) return;

            source.Expansion.Total = copyToExpansion(source.CodeSystem.System, source.CodeSystem.Version, source.CodeSystem.Concept, source.Expansion.Contains);

            if (source.CodeSystem.Version != null)
            {
                source.Expansion.Parameter.Add(new ValueSet.ParameterComponent
                {
                    Name = "version",
                    Value = new FhirUri($"{source.Url}?version={source.CodeSystem.Version}")
                });
            }
        }

        private ValueSet expand(string uri)
        {
            var importedVs = Settings.ValueSetSource.FindValueSet(uri);
            if (importedVs == null) throw new ResourceReferenceNotFoundException(uri, $"Cannot resolve canonical reference '{uri}' to ValueSet");

            if (!importedVs.HasExpansion) Expand(importedVs);

            return importedVs;
        }

        private int copyToExpansion(string system, string version, IEnumerable<ValueSet.ConceptDefinitionComponent> source, List<ValueSet.ContainsComponent> dest)
        {
            int added = 0;

            foreach (var concept in source)
            {
                bool isDeprecated = concept.GetDeprecated() ?? false;

                if (!isDeprecated)
                {
                    var newContains = addToExpansion(system, version, concept.Code, concept.Display, concept.Abstract, dest);
                    added += 1;

                    if (concept.Concept != null && concept.Concept.Any())
                        added += copyToExpansion(system, version, concept.Concept, newContains.Contains);
                }
            }

            return added;
        }

        private void handleCompose(ValueSet source)
        {
            if (source.Compose == null) return;

            handleImport(source);
            handleInclude(source);
            handleExclude(source);
        }

        private void handleImport(ValueSet source)
        {
            if (!source.Compose.Import.Any()) return;

            if (Settings.ValueSetSource == null)
                throw Error.InvalidOperation("No terminology service available to resolve Compose.import, set ValueSetExpander.Settings.ValueSetSource to fix.");

            foreach (var uri in source.Compose.Import)
            {
                var importedVs = expand(uri);

                if (importedVs.HasExpansion)
                {
                    if (importedVs.Expansion.Total + source.Expansion.Total > Settings.MaxExpansionSize)
                        throw new ValueSetExpansionTooBigException($"Import of valueset '{importedVs.Url}' ({importedVs.Expansion.Total} concepts) into " +
                            $"valueset '{source.Url}' ({source.Expansion.Total} concepts) would be larger than the set maximum size ({Settings.MaxExpansionSize})");

                    source.ImportExpansion(importedVs);
                }
                else
                    throw Error.InvalidOperation($"Expansion returned neither an error, nor an expansion for ValueSet with canonical reference '{uri}'");
            }
        }

        private void handleInclude(ValueSet source)
        {
            if (!source.Compose.Include.Any()) return;

            foreach(var include in source.Compose.Include)
            {
                if (!include.Concept.Any())
                    throw new ValueSetExpansionTooComplexException($"Expansion for ValueSet '{source.Url}' uses an include with just a system ('{include.System}') and no enumerated concepts to include.");

                if(include.Filter.Any())
                    throw new ValueSetExpansionTooComplexException($"Expansion for ValueSet '{source.Url}' uses a filter to include concepts, which is not supported.");

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

        private void handleExclude(ValueSet source)
        {
            if (!source.Compose.Exclude.Any()) return;

            foreach (var exclude in source.Compose.Exclude)
            {
                if (!exclude.Concept.Any())
                    throw new ValueSetExpansionTooComplexException($"Expansion for ValueSet '{source.Url}' uses an exclude with just a system ('{exclude.System}') and no enumerated concepts to exclude.");

                if (exclude.Filter.Any())
                    throw new ValueSetExpansionTooComplexException($"Expansion for ValueSet '{source.Url}' uses a filter to exclude concepts, which is not supported.");

                foreach (var concept in exclude.Concept)
                {
                    removeFromExpansion(exclude.System, concept.Code, source.Expansion.Contains);
                }

                var original = source.Expansion.Total ?? 0;
                source.Expansion.Total = original + exclude.Concept.Count;
            }
        }

        private ValueSet.ContainsComponent addToExpansion(string system, string version, string code, string display, bool? isAbstract, List<ValueSet.ContainsComponent> dest)
        {
            var newContains = new ValueSet.ContainsComponent()
            {
                System = system,
                Code = code,
                Display = display,
                Version = version
            };
            if (isAbstract != null)
                newContains.Abstract = isAbstract;

            dest.Add(newContains);

            return newContains;
        }

        private int removeFromExpansion(string system, string code, List<ValueSet.ContainsComponent> dest)
        {
            return dest.RemoveAll(c => c.System == system && c.Code == code);
        }

    }


    public class ValueSetExpanderSettings
    {
        public static ValueSetExpanderSettings Default = new ValueSetExpanderSettings();

        public IResourceResolver ValueSetSource { get; set; }

        public int MaxExpansionSize { get; set; } = 500;
    }
}
