/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System.Xml;

namespace Hl7.Fhir.Serialization
{

    public struct XmlSerializationDetails : IPositionProvider
    {
        public XmlNodeType NodeType;
        public string Namespace;

        public string[] CommentBefore;
        public string[] CommentAfter;

        public int LineNumber { get; }
        public int LinePosition { get; }
    }

}