using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal static class XObjectParseExtensions
    {
        public static PositionInfo GetPositionInfo(this XObject node)
        {
            var lineInfo = (IXmlLineInfo)node;

            if (lineInfo.HasLineInfo())
                return new PositionInfo() { LineNumber = lineInfo.LineNumber, LinePosition = lineInfo.LinePosition };
            else
                return new PositionInfo() { LineNumber = -1, LinePosition = -1 };
        }
            

        public static bool IsResourceName(this XName elementName) =>
            Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;

        public static bool TryGetContainedResource(this XElement xe, out XElement contained)
        {
            contained = null;

            if (xe.HasElements)
            {
                var candidate = xe.Elements().First();

                if (candidate.Name.IsResourceName())
                {
                    contained = candidate;
                    return true;
                }
            }

            // Not on a resource, no name to be found
            return false;
        }

        public static XObject NextElementOrAttribute(this XObject current)
        {
            var scan = current.NextSibling();
            return scanToNextRelevantNode(scan);
        }

        public static XObject FirstChildElementOrAttribute(this XObject current)
        {
            var scan = current.FirstChild();
            return scanToNextRelevantNode(scan);
        }


        private static XObject scanToNextRelevantNode(this XObject scan)
        {
            while (scan != null)
            {
                if (isRelevantNode(scan))
                    break;
                scan = scan.NextSibling();
            }

            return scan;
        }

        private static bool isRelevantNode(this XObject scan)
        {
            return scan.NodeType == XmlNodeType.Element ||
                   (scan.NodeType == XmlNodeType.Attribute &&
                   scan is XAttribute attr && !isReservedAttribute(attr));

            //bool isReservedAttribute(XAttribute a) => a.IsNamespaceDeclaration || a.Name == "value";
            bool isReservedAttribute(XAttribute a) => a.IsNamespaceDeclaration;
        }


        public static string GetValue(this XObject current)
        {
            if (current.AtXhtmlDiv())
                return ((XElement)current).ToString(SaveOptions.DisableFormatting);

            return current is XElement xelem ?
                    xelem.Attribute("value")?.Value.Trim() : current.Value();
        }
    }
}
