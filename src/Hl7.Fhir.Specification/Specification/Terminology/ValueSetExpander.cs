/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
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
            source.Expansion = new ValueSet.ExpansionComponent();
            source.Expansion.TimestampElement = FhirDateTime.Now();
            source.Expansion.IdentifierElement = Uuid.Generate().AsUri();

            if(source.CodeSystem != null)
                handleDefine(source);

            if (source.Compose != null)
                handleCompose(source.Compose);
        }

        private void handleDefine(ValueSet source)
        {
            if (source.CodeSystem == null) return;

            if (source.CodeSystem.Version != null)
            {
                source.Expansion.Parameter.Add(new ValueSet.ParameterComponent
                {
                    Name = "version",
                    Value = new FhirUri($"{source.Url}?version={source.CodeSystem.Version}")
                });
            }

            if (source.Expansion.Total == null)
                source.Expansion.Total = 0;

            source.Expansion.Total = addDefinedCodes(source.CodeSystem.System, source.CodeSystem.Concept, source.Expansion.Contains);
        }

        private int addDefinedCodes(String system, IEnumerable<ValueSet.ConceptDefinitionComponent> concepts, List<ValueSet.ContainsComponent> dest)
        {
            int added = 0;

            foreach (var concept in concepts)
            {
                bool isDeprecated = concept.GetDeprecated() ?? false;

                if (!isDeprecated)
                {
                    var newContains = new ValueSet.ContainsComponent();
                    newContains.System = system;
                    newContains.Abstract = concept.Abstract;
                    newContains.Code = concept.Code;
                    newContains.Display = concept.Display;
                    dest.Add(newContains);
                    added += 1;

                    if (concept.Concept != null && concept.Concept.Any())
                        added += addDefinedCodes(system, concept.Concept, newContains.Contains);
                }
            }

            return added;
        }


        private void handleCompose(ValueSet.ComposeComponent compose)
        {
            throw new NotSupportedException("ValueSet is too complex to expand: encountered a ValueSet.compose");
        }
    }



    public struct ValueSetExpanderSettings
    {
        public static ValueSetExpanderSettings Default = new ValueSetExpanderSettings();

        // no settings yet
    }
}
