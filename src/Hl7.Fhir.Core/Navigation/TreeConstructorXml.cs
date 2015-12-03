using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    internal class WrappedXmlPositionInfo : IPositionInfo
    {
        private IXmlLineInfo _wrapped;

        public WrappedXmlPositionInfo(IXmlLineInfo lineInfo)
        {
            if (!lineInfo.HasLineInfo())
                throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

            _wrapped = lineInfo;
        }

        public int LineNumber
        {
            get
            {
                return _wrapped.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                return _wrapped.LinePosition;
            }
        }
    }

    public static partial class TreeConstructor
    {
        public static readonly XName XVALUE = XName.Get("value");
        public static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";

        public static FhirNavigationTree FromXml(XmlReader reader)
        {
            if (reader == null) Error.ArgumentNull("reader");

            var doc = SerializationUtil.XDocumentFromReader(reader); 
            var docRoot = doc.Root;

            var treeRootRoot = FhirNavigationTree.Create("root");

            createTreeNodeFromDocNode(docRoot, treeRootRoot);

            return treeRootRoot.FirstChild;
        }

        public static FhirNavigationTree FromXml(string xml)
        {
            if (xml == null) Error.ArgumentNull("xml");

            return FromXml(SerializationUtil.XmlReaderFromXmlText(xml));
        }

        private static FhirNavigationTree createTreeNodeFromDocNode(XElement docNode, FhirNavigationTree parent)
        {
            var newNodeName = docNode.Name.LocalName;       // ignore namespace
            FhirNavigationTree newNode = null;

            bool hasValue = false;
            var value = tryGetValue(docNode, out hasValue);

            if (hasValue)
                newNode = parent.AddLastChild(newNodeName,value);
            else
                newNode = parent.AddLastChild(newNodeName);

            foreach(var attr in getFhirNonValueAttributes(docNode))
                createTreeNodeFromDocAttribute(attr, newNode);

            foreach (var elem in docNode.Elements())
            {
                //if (isNestedResource(elem))
                //{
                //    // TODO: Set type of contained node as annotation
                //    // special case - nested resources -> the value is the nested resource in the (only) element, not
                //    // the current element itself.
                //    docNode = elem.Elements().First();
                //}

                createTreeNodeFromDocNode(elem, newNode);
            }

            return newNode;
        }

        private static FhirNavigationTree createTreeNodeFromDocAttribute(XAttribute attr, FhirNavigationTree parent)
        {
            var newNodeName = attr.Name.LocalName;
            var newNode = parent.AddLastChild(newNodeName, attr.Value);

            return newNode;
        }

        private static string tryGetValue(XElement docNode, out bool hasValue)
        {
            var valueAttribute = docNode.Attribute(XVALUE);

            if (valueAttribute != null)
            {
                hasValue = true;
                return valueAttribute.Value;
            }
            else
            {
                hasValue = false;
                return null;
            }
        }


        private static IEnumerable<XNode> getFhirChildNodes(XElement parent)
        {
            foreach (var node in parent.Nodes())
            {
                /* Make sure we handle all possible node types:
                  System.Xml.Linq.XObject
                    System.Xml.Linq.XAttribute
                    System.Xml.Linq.XNode
                        System.Xml.Linq.XComment
                        System.Xml.Linq.XContainer
                            System.Xml.Linq.XDocument
                            System.Xml.Linq.XElement
                    System.Xml.Linq.XDocumentType
                    System.Xml.Linq.XProcessingInstruction
                    System.Xml.Linq.XText   
                */
                if (node is XElement)
                {
                    var elem = (XElement)node;

                    // All normal FHIR elements
                    if (elem.Name.NamespaceName == XmlNs.FHIR)
                    {
                        yield return elem;
                    }

                    // The special xhtml div element
                    else if (elem.Name == XHTMLDIV)
                        yield return elem;

                    //
                    else
                        throw Error.Format("Encountered unknown element '{0}' (from namespace '{1}')"
                                .FormatWith(elem.Name.LocalName, elem.Name.NamespaceName), wrap(elem) );
                }

                // Ignore comments (for now)
                else if (node is XComment)
                    continue;

                // Ignore document types and processing instructions
                else if (node is XDocumentType)
                    continue;
                else if (node is XProcessingInstruction)
                    continue;

                // Throw when encountering a text node
                else if (node is XText)
                {
                    throw Error.Format("Encountered an Xml Text node, which is not supported by the FHIR serialization", wrap(node));
                }

                else
                    throw Error.Format("Encountered unexpected xml node type '{0}'".FormatWith(node.GetType().Name), wrap(node));
            }
        }


        //Try to determine whether this node is a nested resource. This is most probable if
        // a) The node has no siblings
        // b) The node name starts with a capitalized letter
        // c) The node name is one of the known resources
        private static bool isNestedResource(XElement node)
        {
            bool hasSiblings = node.Parent != null && node.Parent.Elements().Count() > 1;

            return !hasSiblings && node is XElement && ModelInfo.IsKnownResource(((XElement)node).Name.LocalName);
        }

        private static IEnumerable<XAttribute> getFhirNonValueAttributes(XElement node)
        {
            foreach (var attr in node.Attributes())
            {
                // skip fhir "value" attribute
                if (attr.Name == XVALUE) continue;

                // skip xmlns declarations
                if (attr.IsNamespaceDeclaration) continue; 
                
                // skip xmlns:xsi declaration
                if (attr.Name == XName.Get("{http://www.w3.org/2000/xmlns/}xsi") && !SerializationConfig.EnforceNoXsiAttributesOnRoot) continue;

                // skip schemaLocation
                if (attr.Name == XName.Get("{http://www.w3.org/2001/XMLSchema-instance}schemaLocation") && !SerializationConfig.EnforceNoXsiAttributesOnRoot) continue;  

                if (attr.Name.NamespaceName == "")
                    yield return attr;
                else
                    throw Error.Format("Encountered unsupported attribute '{0}'.".FormatWith(attr.Name), wrap(attr));
            }
        }

        private static IPositionInfo wrap(XObject node)
        {
            return new WrappedXmlPositionInfo(node);
        }

   

    }
}
