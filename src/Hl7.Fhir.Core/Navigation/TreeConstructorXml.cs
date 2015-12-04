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

    public struct XmlRenderHints
    {
        public bool IsXhtmlDiv;

        public bool IsXmlAttribute;

        public string NestedResourceName;

        public bool HasNestedResource {  get { return !String.IsNullOrEmpty(NestedResourceName);  } }

        public override string ToString()
        {
            return IsXhtmlDiv ? "Is XHTML <div>" : (IsXmlAttribute ? "Is Xml attribute" : (HasNestedResource ? "Contains nested resource " + NestedResourceName :  "(no hints)"));
        }
    }


    public static partial class TreeConstructor
    {


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


        private static string getNodeDescription(XObject docNode)
        {
            if(docNode is XAttribute)
            {
                return "'@{0}'".FormatWith(((XAttribute)docNode).Name.LocalName);
            }
            else if(docNode is XElement)
            {
                var name = ((XElement)docNode).Name;
                return "'{0}' in namespace {1}".FormatWith(name.LocalName,name.NamespaceName);
            }
            else
            {
                return "(" + docNode.NodeType.ToString() + ")";
            }
        }

        private static FhirNavigationTree createTreeNodeFromDocNode(XObject docNode, FhirNavigationTree parent)
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

                Note: XDocumentType, XProcessingInstruction appear only on XDocument and can be disregarded
            */

            var strategies = new INodeConversionStrategy<XObject>[] { new XElementNestedResourceConversionStrategy(), new XElementConversionStrategy(),
                new XElementDivConversionStrategy(), new XAttributeConversionStrategy() };
            var handled = false;
            FhirNavigationTree result = null;

            foreach (var strategy in strategies)
            {
                if(strategy.HandlesDocNode(docNode))
                {
                    handled = true;
                    result = strategy.ConstructTreeNode(docNode, parent);
                    var children = strategy.SelectChildren(docNode, result);

                    if (children != null && children.Any())
                    {
                        children = strategy.PostProcessChildren(children, result);
                        foreach(var child in children)
                        {
                            createTreeNodeFromDocNode(child, result);
                        }
                    }

                    break;                   
                }
            }

            if(!handled)
                throw Error.Format("Encountered unexpected node {0}, starts with \"{1}\")".
                                    FormatWith( getNodeDescription(docNode), docNode.ToString().Trim().Shorten(100)), XmlLineInfoWrapper.Wrap(docNode));

            return result;         
        }
    }
}
