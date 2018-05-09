/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
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
    public class PositionInfo : IPositionInfo
    {
        public int LineNumber { get; internal set; }

        public int LinePosition { get; internal set; }
    }

    public class XmlSerializationDetails
    {
        public XmlNodeType NodeType;
        public XNamespace Namespace;

        public bool IsNamespaceDeclaration;

        /// <summary>
        /// All child text nodes
        /// </summary>
        public string NodeText;
    }   
}