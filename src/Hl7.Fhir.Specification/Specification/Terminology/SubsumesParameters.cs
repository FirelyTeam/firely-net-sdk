/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class SubsumesParameters
    {
        /// <summary>
        /// The "A" code that is to be tested. If a code is provided, a system must be provided.
        /// </summary>
        public string CodeA { get; set; }
        /// <summary>
        /// The "B" code that is to be tested. If a code is provided, a system must be provided.
        /// </summary>
        public string CodeB { get; set; }
        /// <summary>
        /// The code system in which subsumption testing is to be performed. 
        /// This must be provided unless the operation is invoked on a code system instance.
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// The version of the code system, if one was provided in the source data.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The "A" Coding that is to be tested.
        /// </summary>
        public Coding CodingA { get; set; }
        /// <summary>
        /// The "B" Coding that is to be tested.
        /// </summary>
        public Coding CodingB { get; set; }
    }
}
