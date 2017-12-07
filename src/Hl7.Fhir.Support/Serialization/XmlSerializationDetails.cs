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

        public bool IsNamespaceDeclaration;

        public string[] CommentBefore;
        public string[] CommentAfter;

        public int LineNumber { get; internal set; }

        public int LinePosition { get; internal set; }
    }   
}