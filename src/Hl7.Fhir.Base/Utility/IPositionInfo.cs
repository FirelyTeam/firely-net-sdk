/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// Represents an object that can provide line/position information within data being processed.
    /// </summary>
    public interface IPositionInfo
    {
        /// <summary>
        /// The line number within the data, starting with 1 for the first line.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// The position within the line, starting with 1 for the first column.
        /// </summary>
        int LinePosition { get; }
    }

    /// <summary>
    /// A class representing line/position information within data being processed.
    /// </summary>
    public class PositionInfo : IPositionInfo
    {
        public PositionInfo(int lineNumber, int linePosition)
        {
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        /// <inheritdoc />
        public int LineNumber { get; }

        /// <inheritdoc />
        public int LinePosition { get; }
    }
}

#nullable restore