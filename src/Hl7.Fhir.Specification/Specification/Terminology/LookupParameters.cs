/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class LookupParameters
    {
        /// <summary>
        /// The code that is to be located. If a code is provided, a system must be provided.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The system for the code that is to be located.
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// The version of the system, if one was provided in the source data.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// A coding to look up.
        /// </summary>
        public Coding Coding { get; set; }
        /// <summary>
        /// The date for which the information should be returned.
        /// </summary>
        public DateTimeOffset? Date { get; set; }
        /// <summary>
        /// The requested language for display.
        /// </summary>
        public string DisplayLanguage { get; set; }
        /// <summary>
        /// A property that the client wishes to be returned in the output.
        /// </summary>
        /// <remarks>If no properties are specified, the server chooses what to return.</remarks>
        public List<string> Property { get; set; }
    }
}
