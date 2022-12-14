/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Specification
{
    /// <summary>
    /// The major FHIR publication releases.
    /// </summary>
    /// <remarks>Note: this is set is ordered, so "older release" is less than "newer release".</remarks>
    public enum FhirRelease
    {
        /// <summary>
        /// FHIR Release DSTU1
        /// http://hl7.org/fhir/DSTU1
        /// </summary>
        DSTU1 = 100,
        /// <summary>
        /// FHIR Release DSTU2 
        /// http://hl7.org/fhir/DSTU2
        /// </summary>
        DSTU2 = 200,
        /// <summary>
        /// FHIR Release STU3 
        /// http://hl7.org/fhir/STU3
        /// </summary>
        STU3 = 300,
        /// <summary>
        /// FHIR Release 4 
        /// http://hl7.org/fhir/R4
        /// </summary>
        R4 = 400,
        /// <summary>
        /// FHIR Release 5 
        /// </summary>
        R5 = 600,
        /// <summary>
        /// FHIR Release 4B 
        /// </summary>
        R4B = 500,
    }
}
