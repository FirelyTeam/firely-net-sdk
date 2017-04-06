/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct XmlDomFhirNavigator : IElementNavigator, IAnnotated
    {
        internal XmlDomFhirNavigator(XObject current)
        {
            _current = current;
            _nameIndex = 0;
            _parentPath = null;
        }


        public IElementNavigator Clone()
        {
            var copy = new XmlDomFhirNavigator(_current)
            {
                _nameIndex = _nameIndex,
                _parentPath = _parentPath
            };

            return copy;
        }


        private XObject _current;
        private int _nameIndex;
        private string _parentPath;

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

        public string Type
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

                // Else, no type information available
                return null;
            }
        }
     

        private static bool isResourceNameElement(XName elementName)
        {
            return Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;
        }

        public object Value
        {
            get
            {
                if (isXhtmlDiv(_current))
                {
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);
                }
                else if (_current is XElement xelem)
                {
                    // Special case, "value" attribute under the element
                    // TODO: this will hide the nested text value of the element, thus making it
                    // impossible to have both
                    var attrVal = xelem.Attribute("value")?.Value;
                   
                    return attrVal;
                }
                else
                    return _current.Value();
            }
        }

        private static bool isXhtmlDiv(XObject node)
        {
            return (node as XElement)?.Name == XmlNs.XHTMLDIV;
        }

        public string Location
        {
            get
            {
                if (_parentPath == null)
                    return Name;
                else
                    return $"{_parentPath}.{Name}[{_nameIndex}]";
            }
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
                    _parentPath = Location;

                    // If this is a nested resource, move one level deeper
                    if (isResourceNameElement(element.Name))
                        scan = element.FirstNode;

                    _current = scan;
                    _nameIndex = 0;
                    return true;
                }
                else if (scan.NodeType == XmlNodeType.Attribute && !isReservedAttribute((XAttribute)scan))
                {
                    _parentPath = Location;
                    _current = scan;
                    _nameIndex = 0;
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
                if (scan.NodeType == XmlNodeType.Element || (scan.NodeType == XmlNodeType.Attribute && !isReservedAttribute((XAttribute)scan)))
                {                    
                    var currentName = Name;

                    _current = scan;

                    if (currentName == Name)
                        _nameIndex += 1;
                    else
                        _nameIndex = 0;

                    return true;
                }

                scan = scan.NextChild();
            }

            return false;

            
        }

        private bool isReservedAttribute(XAttribute attr) => 
            attr.Name == (XName)"xmlns" || attr.Name.NamespaceName == XmlNs.XMLNS || attr.Name == "value";

        public override string ToString()
        {
            return _current.ToString();
        }

        internal XName XName
        {
            get
            {
                if (_current is XElement elem)
                    return elem.Name;
                else if (_current is XAttribute attr)
                    return attr.Name;
                else
                    return null;
            }
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(XmlSerializationDetails))
            {
                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        NodeType = _current.NodeType,
                        Namespace = XName.NamespaceName
                    }
                };
            }
            else
                return null;
        }

    }
}


