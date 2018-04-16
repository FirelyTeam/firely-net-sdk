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
    public partial struct XmlDomFhirNavigator : IElementNavigator, IAnnotated, IPositionInfo, IExceptionSource
    {
        internal static XmlDomFhirNavigator ForRoot(XElement root, IComplexTypeSerializationInfo rootType)
        {
            return new XmlDomFhirNavigator()
            {
                OnExceptionRaised = null,
                _current = root,
                _nameIndex = 0,
                _parentPath = null,
                _definition = rootType != null ? SerializationInfoNavigator.ForRoot(rootType) : SerializationInfoNavigator.Empty(),
                Type = rootType.TypeName
            };
        }

        public event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;

        public IElementNavigator Clone()
        {
            return this;        // the struct will be copied upon return
        }

        private XObject _current;
        private SerializationInfoNavigator _definition;

        private int _nameIndex;
        private string _parentPath;


        // Could check namespaces too
        public string Name => _definition.IsTracking ? _definition.DefinedName : XmlName.LocalName;

        public string Type { get; private set; }
        //{
        //    get
        //    {
        //        if (currentTypeInfo == null)
        //        {
        //            // try to get resource type from current node (or nested node within (e.g. contained))
        //            if (_current is XElement element && tryGetResourceName(element, out var name))
        //                return name;
        //            else
        //                // Else, no type information available
        //                return null;
        //        }
        //        else
        //        {
        //            if (currentTypeInfo.Type.Length > 1)
        //            {
        //                // choice type
        //                var instanceType = XmlName.LocalName.Substring(currentTypeInfo.ElementName.Length);
        //                var choice = currentTypeInfo.Type.FirstOrDefault(t => String.Compare(t.TypeName, instanceType, StringComparison.OrdinalIgnoreCase) == 0);
        //                return choice?.TypeName;
        //            }
        //            else
        //                return currentTypeInfo.Type[0].TypeName;
        //        }
        //    }
        //}

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
                    // complain if resoure has siblings
                    name = candidate.Name.LocalName;
                    return true;
                }
            }

            // Not on a resource, no name to be found
            return false;

            // Should use typeinfo if available -> change interface to contain info about this
            bool isResourceName(XName elementName) =>
                Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;
        }

        public object Value
        {
            get
            {
                if (AtXhtmlDiv)
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);

                var literal = _current is XElement xelem ? 
                        xelem.Attribute("value")?.Value : _current.Value();

                // Without type info, just return a string
                if (literal == null || Type == null)
                    return literal;
                else
                    return PrimitiveTypeConverter.FromSerializedValue(literal, Type);
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
                {
                    if(_definition.IsTracking && _definition.Current.MayRepeat == true || _nameIndex > 0)
                        return $"{_parentPath}.{Name}[{_nameIndex}]";
                    else
                        return $"{_parentPath}.{Name}";
                }
            }
        }

        private XObject nextMatch(XObject root, string name, XObject startAfter = null)
        {
            XObject scan;

            if (startAfter == null)
            {
                scan = root.FirstChild();
                _definition = _definition.Down();
            }
            else
            {
                scan = startAfter.NextChild();
            }

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
                    _definition = _definition.MoveTo(scanName.LocalName);

                    // If no specific next child is sought, return immediately
                    if (name == null)
                        return scan;
                    else if (_definition.IsTracking && _definition.DefinedName == name)
                        return scan;
                    else
                    {
                        // fall back -> if current name is unknown in definition, do a direct match
                        if (scanName.LocalName == name)
                            return scan;
                    }
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

            // We've moved position
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

        private bool isReservedAttribute(XAttribute attr) => attr.IsNamespaceDeclaration || attr.Name == "value";

        public override string ToString() => _current.ToString();

        internal XName XmlName => _current.Name();

        public int LineNumber => (_current as IXmlLineInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current as IXmlLineInfo)?.LinePosition ?? -1;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(SourceComments))
            {
                return new[]
                {
                    new SourceComments()
                    {
                        CommentsBefore = commentsBefore(_current),
                        ClosingComments = closingComment(_current),
                        DocumentEndComments = docEndComments(_current)
                    }
                };

                string[] commentsBefore(XObject current) =>
                        current is XNode xn ?
                            filterComments(xn.PreviousNodes()) : new string[0];

                string[] closingComment(XObject current)
                {
                    if (current is XContainer xc && xc.LastNode != null)
                        return filterComments(cons(xc.LastNode, xc.LastNode.PreviousNodes()));
                    return new string[0];
                }

                string[] docEndComments(XObject current) =>
                    current is XNode xn && current.Parent is null ?
                        filterComments(xn.NodesAfterSelf())
                        : new string[0];

                string[] filterComments(IEnumerable<XNode> source) =>
                    source.TakeWhile(n => n.NodeType != XmlNodeType.Element)
                            .OfType<XComment>().Select(c => c.Value).Reverse().ToArray();

                IEnumerable<XNode> cons(XNode header, IEnumerable<XNode> tail) =>
                    header == null ? tail : new[] { header }.Union(tail);


            }
            if (type == typeof(XmlSerializationDetails))
            {
                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        // Add: "value in attribute"
                        NodeType = _current.NodeType,
                        Name = XmlName,
                        NodeText = _current.Text(),
                        LineNumber = this.LineNumber,
                        LinePosition = this.LinePosition,
                        IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false
                    }
                };
            }
            else
                return null;
        }
    }
}
