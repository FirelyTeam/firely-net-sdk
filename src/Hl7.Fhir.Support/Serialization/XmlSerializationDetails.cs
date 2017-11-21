/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.Utility;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class XmlSerializationDetails : IPositionInfo
    {
        public XmlNodeType NodeType;
        public XName Name;


        /// <summary>
        /// All child text nodes
        /// </summary>
        public string NodeText;

        /// <summary>
        /// Comments encountered after this but before the next element in the document
        /// </summary>
        public string[] CommentsAfter;

        /// <summary>
        /// Comments encountered before the first child in this element
        /// </summary>
        public string[] OpeningComments;

        /// <summary>
        /// Comments encountered before the root element of the document
        /// </summary>
        public string[] DocumentStartComments;

        public int LineNumber { get; internal set; }

        public int LinePosition { get; internal set; }
    }   
}