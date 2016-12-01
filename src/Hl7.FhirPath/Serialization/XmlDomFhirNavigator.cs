/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Furore.Support;
using Hl7.ElementModel;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct XmlDomFhirNavigator : ISerializedSourceNavigator, IPositionProvider
    {
        internal XmlDomFhirNavigator(XObject current)
        {
            _current = current;
        }


        private XObject _current;

        public string Name
        {
            get
            {
                if (_current.NodeType == XmlNodeType.Element)
                {
                    return ((XElement)_current).Name.LocalName;
                }
                else if (_current.NodeType == XmlNodeType.Attribute)
                {
                    return ((XAttribute)_current).Name.LocalName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string TypeName
        {
            get
            {
                // We only know the type in two occasions:
                // 1. We are on a root element have the same name as a resource (e.g. <Patient>....</Patient>)
                // 2. We are on an element that contains a nested resource (e.g. <contained><Patient>...</Patient></contained>)
                var element = _current as XElement;

                if (element != null)
                {
                    if (isResourceNameElement(element.Name))
                        return element.Name.LocalName;
                
                    if(element.HasElements)
                    {
                        var candidate = element.Elements().First();
                        if (isResourceNameElement(candidate.Name))
                            return candidate.Name.LocalName;
                    }
                }

                return null;
            }
        }
     

        private static bool isResourceNameElement(XName elementName)
        {
            return Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;
        }

        public string Text
        {
            get
            {
                if(isXhtmlDiv(_current))
                {
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);
                }
                else
                    return _current.Value();
            }
        }

        private static bool isXhtmlDiv(XObject node)
        {
            return (node as XElement)?.Name == XmlNs.XHTMLDIV;
        }

        public string Path
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ISerializedSourceNavigator Clone()
        {
            return new XmlDomFhirNavigator(_current);
        }

        public bool MoveToFirstChild()
        {
            // don't move into xhtml
            if (isXhtmlDiv(_current))
                return false;

            var scan = _current.FirstChild();

            // Make this into a "move to the first child that's an element or attribute"
            while (scan != null)
            {
                if (scan.NodeType == XmlNodeType.Element)
                {                    
                    var element = (XElement)scan;

                    // If this is a nested resource, move one level deeper
                    if (isResourceNameElement(element.Name))
                        scan = element.FirstNode;

                    _current = scan;
                    return true;
                }
                else if (scan.NodeType == XmlNodeType.Attribute)
                {
                    _current = scan;
                    return true;
                }

                scan = scan.NextChild();
            }

            return false;
        }

        public bool MoveToNext()
        {
            var scan = _current.NextChild();

            // Make this into a "move to the next child that's an element or attribute"
            while (scan != null)
            {
                if (scan.NodeType == XmlNodeType.Element || scan.NodeType == XmlNodeType.Attribute)
                {
                    _current = scan;
                    return true;
                }

                scan = scan.NextChild();
            }

            return false;
        }


        public int LineNumber
        {
            get
            {
                var li = (IXmlLineInfo)_current;

                if (!li.HasLineInfo())
                    return -1;

                return li.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                var li = (IXmlLineInfo)_current;

                if (!li.HasLineInfo())
                    return -1;

                return li.LinePosition;
            }
        }


        public override string ToString()
        {
            return _current.ToString();
        }

        public T GetSerializationDetails<T>() where T:class
        {
            if (typeof(T) == typeof(XmlSerializationDetails))
            {
                var result = new XmlSerializationDetails();

                result.NodeType = _current.NodeType;

                if (_current.NodeType == XmlNodeType.Element)
                    result.Namespace = ((XElement)_current).Name.Namespace;
                if (_current.NodeType == XmlNodeType.Attribute)
                    result.Namespace = ((XAttribute)_current).Name.Namespace;

                result.LineNumber = LineNumber;
                result.LinePosition = LinePosition;
                    
                return result as T;
            }
            else
                return null;
        }
    }
}


