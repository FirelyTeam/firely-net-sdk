/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Determine the kind of narrative validation is done.
    /// </summary>
    public enum NarrativeValidationKind
    {
        /// <summary>
        /// No validation is performed.
        /// </summary>
        None,

        /// <summary>
        /// Value is validated to be well-formed xml.
        /// </summary>
        Xml,

        /// <summary>
        /// Value is well-formed Xml and is valid against the FHIR rules for Narrative Xhtml.
        /// </summary>
        FhirXhtml
    }
}

#nullable restore