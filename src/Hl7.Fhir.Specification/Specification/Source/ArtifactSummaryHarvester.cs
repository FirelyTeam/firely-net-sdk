/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    // [WMR 20171011] Note:
    // - Derive from ArtifactSummary to define additional custom properties
    // - Derive from ArtifactSummaryHarvester to actually initialize the custom properties

    /// <summary>
    /// Default factory for <see cref="ArtifactSummary"/> records.
    /// Extracts a concrete set of summary information from a raw FHIR artifact,
    /// independent of the underlying serialization format, by calling the
    /// generic <see cref="IElementNavigator"/> interface.
    /// 
    /// Consumers can implement support for additional custom resource summary information
    /// by defining two specialized classes that derive from <see cref="ArtifactSummary"/>
    /// and <see cref="ArtifactSummaryHarvester"/>.
    /// </summary>
    public class ArtifactSummaryHarvester
    {
        /// <summary>Returns the global default instance.</summary>
        public static ArtifactSummaryHarvester Default { get; } = new ArtifactSummaryHarvester();

        /// <summary>ctor</summary>
        public ArtifactSummaryHarvester() { }

        //internal IEnumerable<ArtifactSummary> Harvest(IEnumerable<IElementNavigator> input)
        //{
        //    foreach (var entry in input)
        //    {
        //        yield return Harvest(entry);
        //    }
        //}

        /// <summary>
        /// Extract summary information from a sequence of resources via the <see cref="IElementNavigator"/>
        /// interface, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="input">A sequence of <see cref="IElementNavigator"/> instances for a set of (bundle) resources.</param>
        /// <returns>A sequence of <see cref="ArtifactSummary"/> instances for each of the target resources.</returns>
        internal IEnumerable<ArtifactSummary> Generate(INavigatorStream input)
        {
            while (input.MoveNext())
            {
                yield return Generate(input.Path, input.Position, input.Current);
            }
        }

        /// <summary>
        /// Creates and initializes a new <see cref="ArtifactSummary"/> record by extracting
        /// a concrete set of summary information from a raw FHIR resource file via the specified
        /// <see cref="IElementNavigator"/>, independent of the actual resource serialization format.
        /// Derived classes can override this method to harvest additional custom summary information.
        /// </summary>
        /// <param name="path">The full path specification the current resource container file.</param>
        /// <param name="url">The fully qualified uri of the current resource.</param>
        /// <param name="input">An <see cref="IElementNavigator"/> to access the raw resource.</param>
        /// <returns>A new <see cref="ArtifactSummary"/> record.</returns>
        protected virtual ArtifactSummary Generate(string path, string url, IElementNavigator input)
        {
            // TODO: Dynamically create subtypes depending on resource type
            // return new ArtifactSummary(path, url, input);

            var resType = EnumUtility.ParseLiteral<ResourceType>(input.Name);
            if (resType == null) { return new ArtifactSummary(path, url, input); }
            else if (resType == ResourceType.ConceptMap) { return new ConceptMapSummary(path, url, input); }
            else if (resType == ResourceType.NamingSystem) { return new NamingSystemSummary(path, url, input); }
            else if (resType == ResourceType.ValueSet) { return new ValueSetSummary(path, url, input); }
            else if (ModelInfo.IsConformanceResource(input.Name)) { return new ConformanceResourceSummary(path, url, input); }
            else return new ArtifactSummary(path, url, input);
        }
    }

}
