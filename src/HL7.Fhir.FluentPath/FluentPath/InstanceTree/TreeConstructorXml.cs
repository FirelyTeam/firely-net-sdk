using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    public static partial class TreeConstructor
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
        private static readonly IEnumerable<INodeConversionStrategy<XObject>> STRATEGIES = new INodeConversionStrategy<XObject>[] 
        {
            new XDocumentConversionStrategy(),
            new XWhitespaceStrategy(),
            new XElementNestedResourceConversionStrategy(), new XElementConversionStrategy(),
            new XElementDivConversionStrategy(),
            new XCommentStrategy(), new XAttributeConversionStrategy()
        };

        public static FhirInstanceTree FromXml(XmlReader reader)
        {
            if (reader == null) Error.ArgumentNull("reader");

            var doc = xDocumentFromReader(reader); 

            return createTreeNodeFromDocNode(doc, null);
        }


        public static FhirInstanceTree FromXml(string xml)
        {
            if (xml == null) Error.ArgumentNull("xml");

            return FromXml(XmlReader.Create(new StringReader(SerializationUtil.SanitizeXml(xml))));
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

        private static FhirInstanceTree createTreeNodeFromDocNode(XObject docNode, FhirInstanceTree parent)
        {
            var handled = false;
            FhirInstanceTree result = null;

            foreach (var strategy in STRATEGIES)
            {
                if(strategy.HandlesDocNode(docNode))
                {
                    handled = true;
                    result = strategy.ConstructTreeNode(docNode, parent);

                    if (result != null)
                    {
                        var children = strategy.SelectChildren(docNode, result);

                        if (children != null && children.Any())
                        {
                            foreach (var child in children)
                            {
                                createTreeNodeFromDocNode(child, result);
                            }
                        }

                        strategy.PostProcess(result);
                    }

                    break;                   
                }
            }

            if(!handled)
                throw Error.Format("Encountered unexpected node {0}, starts with \"{1}\")".
                                    FormatWith( getNodeDescription(docNode), docNode.ToString().Trim().Shorten(100)), XmlLineInfoWrapper.Wrap(docNode));

            return result;         
        }

       
        private static XDocument xDocumentFromReader(XmlReader reader)
        {
            XDocument doc;

            try
            {
                doc = XDocument.Load(wrapXmlReader(reader), LoadOptions.SetLineInfo);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message, null);
            }

            return doc;
        }


        private static XmlReader wrapXmlReader(XmlReader xmlReader)
        {
            var settings = new XmlReaderSettings();

            //settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            //settings.IgnoreWhitespace = true;
#if PORTABLE45
            settings.DtdProcessing = DtdProcessing.Ignore;
#else
            settings.DtdProcessing = DtdProcessing.Parse;
#endif

            return XmlReader.Create(xmlReader, settings);
        }


    }
}
