/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Language.Debugging
{
    /// <summary>
    /// The event indicates that the debuggee has produced some output.
    /// </summary>
    public class OutputEventArgs : EventArgs
    {
        /// <summary>
        /// The output category.
        /// </summary>
        public OutputCategory Category { get; set; }

        /// <summary>
        /// The output to report.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// An optional source location where the output was produced.
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// An optional source location line where the output was produced.
        /// </summary>
        public int? Line { get; set; }

        /// <summary>
        /// An optional source location column where the output was produced.
        /// </summary>
        public int? Column { get; set; }
    }

    public enum OutputCategory
    {
        Console,
        StdOut,
        StdErr,
        Telemetry
    }
}