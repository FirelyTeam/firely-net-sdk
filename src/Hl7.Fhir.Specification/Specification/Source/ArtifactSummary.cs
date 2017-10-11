/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System.Collections.Generic;
using System;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Source
{
    // [WMR 20171010] TODO: Allow consumers to create specialized subclasses

    // [WMR 20171010] TODO
    // * ReadOnlyDictionary? (supported by all platforms?)
    //   DirectorySource needs to publicly expose this information => no public setters!
    // * Create common base class for all artifacts
    //   Create specialized subclasses for specific resource types, e.g. StructureDefinition
    public class ArtifactSummary : Dictionary<string,string>
    {
        public ArtifactSummary(string url, IElementNavigator input)
        {
            // TODO
            ResourceUri = url;
            ResourceType = input.Name;
        }

        protected void Set(string key, string value) => throw new NotImplementedException();

        protected string GetOrDefault(string key) => TryGetValue(key, out string value) ? value : null;


        /// <summary>Represents the original location of the artifact.</summary>
        public string Origin { get; set; }

        /// <summary>The resource type.</summary>
        public string ResourceType { get; set; }

        /// <summary>The resource Uri.</summary>
        public string ResourceUri { get; set; }

        // Conformance resources

        /// <summary>The canonical resource url.</summary>
        public string Canonical { get; set; }

        public string ValueSetSystem { get; set; }

        public string[] UniqueIds { get; set; }

        public string ConceptMapSource { get; set; }

        public string ConceptMapTarget { get; set; }

        public override string ToString()
            => $"{ResourceType} resource with uri {ResourceUri ?? "(unknown)"} (canonical {Canonical ?? "(unknown)"}), read from {Origin}";
    }

}
