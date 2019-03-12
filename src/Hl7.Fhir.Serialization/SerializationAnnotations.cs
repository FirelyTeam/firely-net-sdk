/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


namespace Hl7.Fhir.Serialization
{
    public class SourceComments
    {
        /// <summary>
        /// Comments encountered before this node, but after the previous sibling
        /// </summary>
        public string[] CommentsBefore;

        /// <summary>
        /// Comments encountered after the last child of this element
        /// </summary>
        public string[] ClosingComments;

        /// <summary>
        /// Comments encountered after the root element of the document
        /// </summary>
        public string[] DocumentEndComments;
    }
}