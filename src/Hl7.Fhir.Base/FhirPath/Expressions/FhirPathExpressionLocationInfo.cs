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
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class FhirPathExpressionLocationInfo : ISourcePositionInfo
    {
        internal FhirPathExpressionLocationInfo()
        {

        }
        public Source Source { get; init; }

        public int LineNumber { get; init; }

        public int LinePosition { get; init; }

        public int RawPosition { get; init; }
        public int Length { get; init; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string DebuggerDisplay
        {
            get
            {
                return $"Position=\"{RawPosition}\" Length=\"{Length}\"";
            }
        }
        public override string ToString() => $"({DebuggerDisplay})";
    }
}
