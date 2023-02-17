/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for browsing <see cref="CodeSystem"/> resources.</summary>
    public interface ICommonConformanceSource : IResourceResolver
    {
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
    }
}