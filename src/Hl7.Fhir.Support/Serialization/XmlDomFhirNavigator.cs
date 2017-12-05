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
    public partial struct XmlDomFhirNavigator : IElementNavigator, IAnnotated, IPositionInfo
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
                _nameIndex = this._nameIndex,
                _parentPath = this._parentPath,
            };

            return copy;
        }


        private XObject _current;
        private int _nameIndex;
        private string _parentPath;

        public string Name => XName?.LocalName;


        public string Type
        {
            get
            {
                // We only know the type in two occasions:
                // 1. We are on a root element have the same name as a resource (e.g. <Patient>....</Patient>)
                // 2. We are on an element that contains a nested resource (e.g. <contained><Patient>...</Patient></contained>)

                if (_current is XElement element)
                {
                    if (isResourceNameElement(element.Name))
                        return element.Name.LocalName;

                    if (element.HasElements)
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

        private XObject nextMatch (XObject root, string name, XObject startAfter = null)
        {
            var scan = startAfter == null ? root.FirstChild() : startAfter.NextChild();
        
            while(scan != null)
            {
                XName scanName;

                if (scan.NodeType == XmlNodeType.Element && scan is XElement element)
                    scanName = element.Name;
                else if (scan.NodeType == XmlNodeType.Attribute && scan is XAttribute attr && !isReservedAttribute(attr))
                    scanName = attr.Name;
                else
                    scanName = null;

                if (scanName != null)
                {
                    if (name == null || scanName?.LocalName == name)
                        return scan;
                }

                scan = scan.NextChild();
            }

            return null;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            // don't move into xhtml
            if (isXhtmlDiv(_current))
                return false;

            var found = nextMatch(_current, nameFilter);
            if (found == null) return false;

            _parentPath = Location;

            // Move the _current position to the newly found element,
            // unless that is the (xml) type name in a contained resource, in which case we move one level deeper
            if (found is XElement xe && isResourceNameElement(xe.Name))
                _current = xe.FirstNode;
            else
                _current = found;

            _nameIndex = 0;

            return true;
        }

        public bool MoveToNext(string nameFilter)
        {
            var found = nextMatch(_current, nameFilter, _current);
            if (found == null) return false;

            var currentName = Name;
             _current = found;  // this will change property Name

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            return true;            
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

        public int LineNumber => (_current as IXmlLineInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current as IXmlLineInfo)?.LinePosition ?? -1;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(XmlSerializationDetails))
            {
                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        NodeType = _current.NodeType,
                        Name = XName,
                        IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false,
                        LineNumber = this.LineNumber,
                        LinePosition = this.LinePosition
                    }
                };
            }
            else
                return null;
        }

    }
}


