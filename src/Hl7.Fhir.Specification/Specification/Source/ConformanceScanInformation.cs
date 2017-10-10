/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Collections;
using System;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Source
{
    internal class SummaryHarvester
    { 
        public Action<IElementNavigator, ArtifactSummary> Generator { get; set; }

        public ArtifactSummary Harvest(IElementNavigator input) => throw new NotImplementedException();

        public IEnumerable<ArtifactSummary> Harvest(IEnumerable<IElementNavigator> input) => throw new NotImplementedException();
    
    }

    internal class ArtifactSummary : Dictionary<string,string>
    {
        public ArtifactSummary()
        {
        }

        protected void Set(string key, string value) => throw new NotImplementedException();

        protected string GetOrDefault(string key) => TryGetValue(key, out string value) ? value : null;
        
        public string ResourceType { get; set; }

        public string ResourceUri { get; set; }

        public string Canonical { get; set; }

        public string ValueSetSystem { get; set; }

        public string[] UniqueIds { get; set; }

        public string ConceptMapSource { get; set; }

        public string ConceptMapTarget { get; set; }

        public string Origin { get; set; }

        public override string ToString()
        {
            return "{0} resource with uri {1} (canonical {2}), read from {2}"
                .FormatWith(ResourceType, ResourceUri ?? "(unknown)", Canonical ?? "(unknown)", Origin);
        }
    }
}
