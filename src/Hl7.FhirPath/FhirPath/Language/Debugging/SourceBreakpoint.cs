/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

namespace Hl7.Fhir.Language.Debugging
{
    /// <summary>
    /// Properties of a breakpoint or logpoint passed to the setBreakpoints request.
    /// </summary>
    public class SourceBreakpoint
    {
        /// <summary>
        /// The source line of the breakpoint or logpoint.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// An optional source column of the breakpoint.
        /// </summary>
        public int? Column { get; set; }

        /// <summary>
        /// An optional expression for conditional breakpoints.
        /// </summary>
        public string Condition { get; set; }
    }
}