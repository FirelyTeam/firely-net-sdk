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
    /// A Source is a descriptor for source code. It is returned from the debug adapter as part of a StackFrame and 
    /// it is used by clients when specifying breakpoints.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// The short name of the source.
        /// </summary>
        /// <remarks>
        /// Every source returned from the debug adapter has a name. When sending a source to the debug adapter this name is optional.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        ///  The path of the source to be shown in the UI.
        /// </summary>
        /// <remarks>It is only used to locate and load the content of the source if no sourceReference is specified (or its value is 0).
        /// </remarks>
        public string Path { get; set; }

        /// <summary>
        /// Id for the source.
        /// </summary>
        /// <remarks>
        /// If sourceReference > 0 the contents of the source must be retrieved through the SourceRequest (even if a path is specified).
        /// A sourceReference is only valid for a session, so it must not be used to persist a source.
        /// </remarks>
        public int SourceReference { get; set; }
    }
}