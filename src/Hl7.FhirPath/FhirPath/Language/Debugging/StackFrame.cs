/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Language.Debugging
{
    /// <summary>
    /// Source location and other details about a stackframe.
    /// </summary>
    public class StackFrame : ISourcePositionInfo
    {
        /// <summary>
        /// An identifier for the stack frame. 
        /// </summary>
        /// <remarks>
        /// It must be unique across all threads. This id can be used to retrieve the scopes of the frame with the 
        /// 'scopesRequest' or to restart the execution of a stackframe.
        /// </remarks>
        public int Id { get; set; }

        /// <summary>
        /// The name of the stack frame, typically a method name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The optional source of the frame.
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// The line within the file of the frame. 
        /// </summary>
        /// <remarks>
        /// If source is null or doesn't exist, line is 0 and must be ignored.
        /// </remarks>
        public int Line { get; set; }

        /// <summary>
        /// The column within the line.
        /// </summary>
        /// <remarks>
        /// If source is null or doesn't exist, column is 0 and must be ignored.
        /// </remarks>
        public int Column { get; set; }

        Source ISourcePositionInfo.Source => Source;

        int IPositionInfo.LineNumber => Line;

        int IPositionInfo.LinePosition => Column;

        public override string ToString() => $"{Name}[{Id}]";
    }
}