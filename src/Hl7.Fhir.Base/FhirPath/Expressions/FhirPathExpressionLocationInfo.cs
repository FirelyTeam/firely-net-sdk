/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Language.Debugging;
using System.Diagnostics;

namespace Hl7.FhirPath.Expressions
{
    /// <summary>
    /// Position information provided when parsing a FhirPath expression
    /// This is typically associated with an <see cref="Expression">Expression</see> component or <see cref="SubToken">SubToken</see> while parsing
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class FhirPathExpressionLocationInfo : ISourcePositionInfo
    {
        internal FhirPathExpressionLocationInfo()
        {
        }

        public Source Source { get; init; }

        /// <summary>
        /// The stating Line number within the expression (1-based)
        /// </summary>
        public int LineNumber { get; init; }

        /// <summary>
        /// The position within the line in the expression indicated by <see cref="LineNumber">LineNumber</see> (1-based)
        /// </summary>
        public int LinePosition { get; init; }

        /// <summary>
        /// The raw position within the fhirpath expression from the start (0-based)
        /// It can be used as a direct index to the location within the fhirpath expression
        /// </summary>
        public int RawPosition { get; init; }

        /// <summary>
        /// How long the expression component or SubToken is
        /// </summary>
        public int Length { get; init; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get
            {
                return $"Position=\"{RawPosition}\" Length=\"{Length}\"";
            }
        }
        public override string ToString() => $"({DebuggerDisplay})";
    }
}
