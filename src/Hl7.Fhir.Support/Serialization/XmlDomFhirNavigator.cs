/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Support.Model;
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

        public string Name => XmlName?.LocalName;


        public string Type
        {
            get
            {
                // We only know the type in a few occasions:
                // 1. We are on a root element have the same name as a resource (e.g. <Patient>....</Patient>)
                // 2. We are on an element that contains a nested resource (e.g. <contained><Patient>...</Patient></contained>)
                // 3. We are on an xhtml <div>

                if (AtXhtmlDiv)
                    return "xhtml";
                else if (_current is XElement element && tryGetResourceName(element, out var name))
                    return name;
                else
                    // Else, no type information available
                    return null;
            }
        }

        private static bool tryGetResourceName(XElement xe, out string name)
        {
            name = null;

            if (isResourceName(xe.Name))
            {
                name = xe.Name.LocalName;
                return true;
            }

            // We might still be on a resource if this elements contains a
            // contained resource
            if (xe.HasElements)
            {
                var candidate = xe.Elements().First();

                if (isResourceName(candidate.Name))
                {
                    name = candidate.Name.LocalName;
                    return true;
                }
            }

            // Not on a resource, no name to be found
            return false;
        }

        public bool AtResource => _current is XElement xe && tryGetResourceName(xe, out var dummy);

        private static bool isResourceName(XName elementName) =>
                Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;

        public object Value
        {
            get
            {
                if (AtXhtmlDiv)
                {
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);
                }
                else if (_current is XElement xelem)
                {
                    // Special case, "value" attribute under the element
                    // If both are available, value will be given precedence,
                    // and the nested text is available in the XmlSerializationDetails
                    return xelem.Attribute("value")?.Value ?? 
                        String.Concat(xelem.Text());
                }
                else
                    return _current.Value();
            }
        }

        public bool AtXhtmlDiv => (_current as XElement)?.Name == XmlNs.XHTMLDIV;

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

        private XObject nextMatch(XObject root, string name, XObject startAfter = null)
        {
            var scan = startAfter == null ? root.FirstChild() : startAfter.NextChild();

            while (scan != null)
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
            if (AtXhtmlDiv) return false;

            var found = nextMatch(_current, nameFilter);
            if (found == null) return false;

            _parentPath = Location;

            // Move the _current position to the newly found element,
            // unless that is the (xml) type name in a contained resource, in which case we move one level deeper
            if (found is XElement xe && isResourceName(xe.Name))
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
            attr.IsNamespaceDeclaration || attr.Name == "value";

        public override string ToString() => _current.ToString();

        internal XName XmlName => _current.Name();

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
                        Name = XmlName,
                        NodeText = _current.Text(),
                        //IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false,
                        LineNumber = this.LineNumber,
                        LinePosition = this.LinePosition,

                        CommentsAfter = commentsAfter(_current),
                        OpeningComments = openingComments(_current),
                        DocumentStartComments = docComments(_current)
                    }
                };

                string[] commentsAfter(XObject current) =>
                    current is XNode xn ?
                        filterComments(xn.NodesAfterSelf()) : new string[0];

                string[] openingComments(XObject current) =>
                    current is XContainer xc ?
                        filterComments(xc.Nodes()): new string[0];

                string[] docComments(XObject current) =>
                    current.Parent is null ?
                        openingComments(current.Document)
                        : new string[0];

                string[] filterComments(IEnumerable<XNode> source) =>
                    source.TakeWhile(n => n.NodeType != XmlNodeType.Element)
                            .OfType<XComment>().Select(c => c.Value).ToArray();
            }
            else if(type == typeof(BasicTypeInfo))
            {
                return new[]
                {
                    new BasicTypeInfo
                    {
                        IsResource = AtResource,
                        Type = this.Type
                    }
                };
            }
            else
                return null;
        }

    }
}


