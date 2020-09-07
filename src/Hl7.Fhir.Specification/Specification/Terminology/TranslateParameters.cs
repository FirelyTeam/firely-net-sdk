/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class TranslateParameters
    {
        /// <summary>
        /// A canonical URL for a concept map.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The concept map is provided directly as part of the request.
        /// </summary>
        public ConceptMap ConceptMap { get; set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the concept map to be used for the translation.
        /// </summary>
        public string ConceptMapVersion { get; set; }
        /// <summary>
        /// The code that is to be translated. If a code is provided, a system must be provided.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The system for the code that is to be translated
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// The version of the system, if one was provided in the source data.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Identifies the value set used when the concept (system/code pair) was chosen. May be a logical id, or an absolute or relative location.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// A coding to translate
        /// </summary>
        public Coding Coding { get; set; }
        /// <summary>
        /// A full codeableConcept to validate.
        /// </summary>
        public CodeableConcept CodeableConcept { get; set; }
        /// <summary>
        /// Identifies the value set in which a translation is sought.
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// identifies a target code system in which a mapping is sought. This parameter is an alternative to the target parameter - only one is required.
        /// </summary>
        public string TargetSystem { get; set; }
        ///// <summary>
        ///// Another element that may help produce the correct mapping
        ///// </summary>
        //public List<OfSomething> Dependency { get; set; }
        /// <summary>
        /// If this is true, then the operation should return all the codes that might be mapped to this code. This parameter reverses the meaning of the source and target parameters
        /// </summary>
        public bool? Reverse { get; set; }
    }
}
