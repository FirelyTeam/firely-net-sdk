/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class XmlDomFhirReader : IFhirReader
    {
        XObject _current;

        public XmlDomFhirReader(XmlReader reader)
        {
            var settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
#if PORTABLE45
            settings.DtdProcessing = DtdProcessing.Ignore;
#else
            settings.DtdProcessing = DtdProcessing.Parse; 
#endif

            var internalReader = XmlReader.Create(reader, settings);
            XDocument doc;

            try
            {
                doc = XDocument.Load(internalReader, LoadOptions.SetLineInfo);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message, null);
            }

            setRoot(doc);
        }

        internal XmlDomFhirReader(XObject root)
        {
            setRoot(root);
        }

        private void setRoot(XObject root)
        {
            if (root is XDocument)
                _current = ((XDocument)root).Root;
            else
                _current = root;
        }

        public string GetResourceTypeName()
        {
            if (_current is XElement)
                return ((XElement)_current).Name.LocalName;
            else
                throw Error.Format("Cannot get resource type name: reader not at an element", this);
        }


        private static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";

        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (!(_current is XElement))
                throw Error.Format("Cannot get members: reader not at an element", this);

            var rootElem = (XElement)_current;
            var result = new List<Tuple<string, IFhirReader>>();

            // First, any attributes
            foreach(var attr in rootElem.Attributes()) //.Where(xattr => xattr.Name.LocalName != "xmlns"))
            {
                if (attr.IsNamespaceDeclaration) continue;      // skip xmlns declarations
                if (attr.Name == XName.Get("{http://www.w3.org/2000/xmlns/}xsi") && !SerializationConfig.EnforceNoXsiAttributesOnRoot ) continue;   // skip xmlns:xsi declaration
                if (attr.Name == XName.Get("{http://www.w3.org/2001/XMLSchema-instance}schemaLocation") && !SerializationConfig.EnforceNoXsiAttributesOnRoot) continue;     // skip schemaLocation

                if (attr.Name.NamespaceName == "")
                    result.Add(Tuple.Create(attr.Name.LocalName, (IFhirReader)new XmlDomFhirReader(attr)));
                else
                    throw Error.Format("Encountered unsupported attribute {0}", this, attr.Name);
            }
                
            foreach(var node in rootElem.Nodes())
            {
                if(node is XText)
                {
                    // A nested text node (the content attribute of a Binary)
                    result.Add(Tuple.Create(SerializationConfig.BINARY_CONTENT_MEMBER_NAME, (IFhirReader)new XmlDomFhirReader(node)));
                }
                else if(node is XElement)
                {
                    var elem = (XElement)node;
                        
                    // All normal FHIR elements
                    if (elem.Name.NamespaceName == XmlNs.FHIR)
                    {
                        var value = elem;
                            
                        // special case - nested resources -> the value is the nested resource in the element, not
                        // the current element itself.
                        if (elem.FirstNode as XElement != null && ModelInfo.IsKnownResource(((XElement)elem.FirstNode).Name.LocalName))
                            value = (XElement)elem.FirstNode;

                        result.Add(Tuple.Create(elem.Name.LocalName, (IFhirReader)new XmlDomFhirReader(value)));
                    }

                    // The special xhtml div element
                    else if (elem.Name == XHTMLDIV)
                        result.Add(Tuple.Create(XHTMLDIV.LocalName,
                            (IFhirReader)new XmlDomFhirReader(buildDivXText(elem))));

                    else
                        throw Error.Format("Encountered element '{0}' from unsupported namespace '{1}'", this, elem.Name.LocalName, elem.Name.NamespaceName);
                }
                else if(node is XComment)
                {
                    result.Add(Tuple.Create("fhir_comments", (IFhirReader)new XmlDomFhirReader(node)));
                }
                else
                    throw Error.Format("Encountered unexpected element member of type {0}", this, node.GetType().Name);
            }

            return result;           
        }

        private XText buildDivXText(XElement elem)
        {
 	        return new XText(elem.ToString(SaveOptions.DisableFormatting));
        }


        public IEnumerable<IFhirReader> GetArrayElements()
        {
            // Xml does not support arrays like Json. This method won't be called if CurrentToken is never set to Array
            throw new NotImplementedException();
        }

        public object GetPrimitiveValue()
        {
            if (_current is XAttribute)
                return ((XAttribute)_current).Value;

            else if (_current is XText)
                return ((XText)_current).Value;

            else if (_current is XComment)
                return ((XComment)_current).Value;

            else
                throw Error.Format("Parser is not at a primitive value", this);
        }


        public static IPostitionInfo GetLineInfo(XObject obj)
        {
            return new XmlDomFhirReader(obj);
        }

        public int LineNumber
        {
            get 
            {
                var li = (IXmlLineInfo)_current;

                if(!li.HasLineInfo())
                    throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

                return li.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                var li = (IXmlLineInfo)_current;

                if (!li.HasLineInfo())
                    throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

                return li.LinePosition;
            }
        }
    }
}
