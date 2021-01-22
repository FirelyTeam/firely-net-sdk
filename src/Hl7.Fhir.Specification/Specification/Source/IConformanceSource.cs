/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for browsing and resolving FHIR conformance resources.</summary>
    public interface IConformanceSource : IResourceResolver
    {
        // [WMR 20171204] We could convert the following members to extension methods (ConformanceSourceExtensions)
        // Breaking change, but decreases interface implementer burden

        /// <summary>List all resource uris for the resources managed by the source, optionally filtered by type.</summary>
        /// <param name="filter">A <see cref="ResourceType"/> enum value.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of uri strings.</returns>
        /// <remarks>The returned uris are the physical uris for the resources, not the canonical uris.</remarks>
        IEnumerable<string> ListResourceUris(ResourceType? filter = null);

        /// <summary>
        /// Find a <see cref="CodeSystem"/> resource by a <see cref="ValueSet"/> canonical url that contains all codes from that codesystem.
        /// </summary>
        /// <param name="valueSetUri">The canonical uri of a <see cref="ValueSet"/> resource.</param>
        /// <returns>A <see cref="CodeSystem"/> resource, or <c>null</c>.</returns>
        /// <remarks>
        /// It is very common for valuesets to represent all codes from a specific/smaller code system.
        /// These are indicated by he CodeSystem.valueSet element, which is searched here.
        /// </remarks>
        CodeSystem FindCodeSystemByValueSet(string valueSetUri);

        /// <summary>Find <see cref="ConceptMap"/> resources which map from the given source to the given target.</summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns>A sequence of <see cref="ConceptMap"/> resources.</returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        IEnumerable<ConceptMap> FindConceptMaps(string sourceUri=null, string targetUri=null);

        /// <summary>Finds a <see cref="NamingSystem"/> resource by matching any of a system's UniqueIds.</summary>
        /// <param name="uniqueId">The unique id of a <see cref="NamingSystem"/> resource.</param>
        /// <returns>A <see cref="NamingSystem"/> resource, or <c>null</c>.</returns>
        NamingSystem FindNamingSystem(string uniqueId);
    }

}
