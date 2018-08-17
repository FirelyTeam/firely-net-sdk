/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class XmlSerializationDetails : IPositionInfo
    {
        public XmlNodeType NodeType;
        public XNamespace Namespace;

        public bool IsNamespaceDeclaration;
        public bool IsXhtml;

        public string SchemaLocation;

        public bool IsCDAText;
        public bool IsXsiType;

        /// <summary>
        /// All child XText nodes
        /// </summary>
        public string NodeText;

        public string OriginalValue;

        public int LineNumber { get; internal set; }
        public int LinePosition { get; internal set; }
    }


    public static class XmlSerializationDetailsExtensions
    {
        public static XmlSerializationDetails GetXmlSerializationDetails(this IAnnotated ann) =>
            ann.TryGetAnnotation<XmlSerializationDetails>(out var rt) ? rt : null;

        public static XmlSerializationDetails GetXmlSerializationDetails(this ITypedElement node) =>
                node is IAnnotated ann ? ann.GetXmlSerializationDetails() : null;

        public static XmlSerializationDetails GetXmlSerializationDetails(this IElementNavigator navigator) =>
            navigator is IAnnotated ann ? ann.GetXmlSerializationDetails() : null;
    }
}