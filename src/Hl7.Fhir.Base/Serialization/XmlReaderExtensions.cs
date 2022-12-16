#nullable enable

using Hl7.Fhir.Utility;
using System.Xml;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;

namespace Hl7.Fhir.Serialization
{
    internal static class XmlReaderExtensions
    {
        internal static string GenerateLocationMessage(this XmlReader reader)
        {
            return GenerateLocationMessage(reader, out var _, out var _);
        }

        internal static string GenerateLocationMessage(this XmlReader reader, out long lineNumber, out long position)
        {
            (lineNumber, position) = GenerateLineInfo(reader);
            return GenerateLocationMessage(lineNumber, position);
        }

        internal static string GenerateLocationMessage(long lineNumber, long position)
        {
            return $"At line {lineNumber}, position {position}.";
        }

        internal static (int lineNumber, int position) GenerateLineInfo(this XmlReader reader)
        {
            IXmlLineInfo xmlInfo = (IXmlLineInfo)reader;
            return (xmlInfo.LineNumber, xmlInfo.LinePosition);
        }

        internal static bool ReadToContent(this XmlReader reader, FhirXmlPocoDeserializerState state)
        {
            if (reader.Read())
            {
                while (reader.ShouldSkipNodeType(state))
                {
                    reader.Skip();
                }
                return true;
            }
            return false;
        }
        internal static bool ShouldSkipNodeType(this XmlReader reader, FhirXmlPocoDeserializerState state)
        {
            var nodeType = reader.NodeType;

            if (nodeType == XmlNodeType.Comment || nodeType == XmlNodeType.Whitespace || nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.SignificantWhitespace)
                return true;
            else if (nodeType == XmlNodeType.CDATA || nodeType == XmlNodeType.ProcessingInstruction || nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.EntityReference)
            {
                state.Errors.Add(ERR.UNALLOWED_NODE_TYPE.With(reader, nodeType.GetLiteral()));
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static bool HasValueAttributeOrChildren(this XmlReader reader)
        {
            return reader.GetAttribute("value") != null
                || reader.HasChildren();
        }

        internal static bool HasChildren(this XmlReader reader)
        {
            var subtree = reader.ReadSubtree();
            if (subtree != null)
            {
                subtree.Read();
                var parentDepth = subtree.Depth;
                return subtree.Read()
                    && (subtree.Depth != parentDepth);
            }
            else
            {
                return false;
            }

        }
    }
}
#nullable restore
