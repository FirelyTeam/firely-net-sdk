/*
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System.Xml;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;

namespace Hl7.Fhir.Serialization;

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

    internal static bool ReadToContent(this XmlReader reader, PocoDeserializerState state)
    {
        if (reader.Read())
        {
            while (reader.ShouldSkipNodeType(state))
            {
                if (reader.NodeType == XmlNodeType.Comment)
                    state.AddComment(reader.Value);

                reader.Skip();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Moves the reader to the next content node, like <see cref="XmlReader.MoveToContent"/> does, but collects
    /// the comments encountered on the way into <paramref name="state"/> instead of silently skipping them.
    /// </summary>
    internal static void MoveToContentCapturingComments(this XmlReader reader, PocoDeserializerState state)
    {
        if (!state.RetainComments)
        {
            reader.MoveToContent();
            return;
        }

        // Like MoveToContent(), move back to the owning element when positioned on an attribute.
        if (reader.NodeType == XmlNodeType.Attribute)
        {
            reader.MoveToElement();
            return;
        }

        // These are the node types MoveToContent() considers content and stops at. Since it skips the
        // comments in between without reporting them, we have to walk to the next content node ourselves.
        while (!reader.EOF && reader.NodeType is not (XmlNodeType.Element or XmlNodeType.EndElement
                   or XmlNodeType.Text or XmlNodeType.CDATA or XmlNodeType.EntityReference or XmlNodeType.EndEntity))
        {
            if (reader.NodeType == XmlNodeType.Comment)
                state.AddComment(reader.Value);

            if (!reader.Read()) break;
        }
    }

    internal static bool ShouldSkipNodeType(this XmlReader reader, PocoDeserializerState state)
    {
        var nodeType = reader.NodeType;

        if (nodeType is XmlNodeType.Comment or XmlNodeType.Whitespace or XmlNodeType.XmlDeclaration or XmlNodeType.SignificantWhitespace)
            return true;

        if (nodeType is XmlNodeType.CDATA or XmlNodeType.ProcessingInstruction or XmlNodeType.DocumentType or XmlNodeType.EntityReference or XmlNodeType.Text)
        {
            state.Errors.Add(ERR.DISALLOWED_NODE_TYPE(reader, state.Path.GetInstancePath(), nodeType.GetLiteral()));
            return true;
        }

        return false;
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