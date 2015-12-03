using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    public partial class TreeConstructor
    {
        public static readonly XName XNAME_VALUE = XName.Get("value");

        public static FhirNavigationTree FromXml(XmlReader reader)
        {
            var doc = SerializationUtil.XDocumentFromReader(reader);
            if (doc == null) Error.ArgumentNull("doc");
            var docRoot = doc.Root;

            var treeRootRoot = FhirNavigationTree.Create("root");

            createTreeNodeFromDocNode(docRoot, treeRootRoot);

            return treeRootRoot.FirstChild;
        }

        public static FhirNavigationTree FromXml(string xml)
        {
            return FromXml(SerializationUtil.XmlReaderFromXmlText(xml));
        }

        private static FhirNavigationTree createTreeNodeFromDocNode(XElement docNode, FhirNavigationTree parent)
        {
            var newNodeName = docNode.Name.LocalName;       // ignore namespace
            FhirNavigationTree newNode = null;

            bool hasValue = false;
            var value = tryGetValue(docNode, out hasValue);

            if(hasValue)
                newNode = parent.AddLastChild(newNodeName,value);
            else
                newNode = parent.AddLastChild(newNodeName);

            foreach(var attr in docNode.Attributes())
            {
                if (attr.Name != XNAME_VALUE)
                    createTreeNodeFromDocAttribute(attr, newNode);
            }

            foreach (var elem in docNode.Elements())
                createTreeNodeFromDocNode(elem, newNode);

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
            var valueAttribute = docNode.Attribute(XNAME_VALUE);

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
    }
}
