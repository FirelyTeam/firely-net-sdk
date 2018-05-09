/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
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
        private XmlDomFhirNavigator(XElement current, SerializationInfoCache definition)
        {
            _current = current;
            _definition = definition;

            _parentPath = null;
            _nameIndex = 0;
            OnExceptionRaised = null;
        }

        internal static XmlDomFhirNavigator ForRoot(XElement root, IModelMetadataProvider provider)
        {
            // namespace?
            var rootType = root.Name.LocalName;

            return ForElement(root, rootType, provider);
        }

        internal static XmlDomFhirNavigator ForElement(XElement root, string type, IModelMetadataProvider provider)
        {
            var elementType = provider?.GetSerializationInfoForType(type);

            // Note: this will generate an Empty SerializationInfoNavigator in case there is no provider, or the type is unknown
            // in the last case we may also prefer to report an error at this moment?
            var definition = elementType != null ? SerializationInfoCache.ForRoot(elementType, root.Name.LocalName, provider) : SerializationInfoCache.Empty;

            return new XmlDomFhirNavigator(root, definition);
        }

        public event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;

        public IElementNavigator Clone()
        {
            return this;        // the struct will be copied upon return
        }

        private XObject _current;
        private SerializationInfoCache _definition;

        private int _nameIndex;
        private string _parentPath;


        // Could check namespaces too
        public string Name => _definition.IsTracking ? _definition.DefinedName : XmlName.LocalName;

        public string Type
        {
            get
            {
                if (_current is XElement element)
                {
                    // Make sure we only get the type directly from the typename when we encounter a root,
                    // otherwise get it from the first nested element under the current element
                    if(_parentPath == null && isResourceName(element.Name)) return element.Name.LocalName;
                    if(_parentPath != null && tryGetNestedResourceName(element, out var name)) return name;
                }

                // Note, this is done last, since TypeName might well be the abstract Resource type on 
                // a contained resource, so if it is not a contained resource, you can safely return the
                // real type (not that _definition.TypeName does take type prefixes under consideration when
                // dealing with choice types, so here the actual type used in the instance is being returned).
                return _definition.IsTracking? _definition.TypeName: null;
            }
        }


        private static bool tryGetNestedResourceName(XElement xe, out string name)
        {
            name = null;

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

        private static bool isResourceName(XName elementName) =>
            Char.IsUpper(elementName.LocalName, 0) && elementName.Namespace == XmlNs.XFHIR;

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
                    if (_definition.IsTracking && _definition.Current.MayRepeat == false)
                        return $"{_parentPath}.{Name}";
                    else
                        return $"{_parentPath}.{Name}[{_nameIndex}]";
                }
            }
        }

        private static bool tryMatch((XObject, SerializationInfoCache) current, string name, out (XObject, SerializationInfoCache)? next)
        {
            var instance = current.Item1;
            var def = current.Item2;

            while (instance != null)
            {
                XName scanName;

                if (instance.NodeType == XmlNodeType.Element && instance is XElement element)
                    scanName = element.Name;
                else if (instance.NodeType == XmlNodeType.Attribute && instance is XAttribute attr && !isReservedAttribute(attr))
                    scanName = attr.Name;
                else
                    scanName = null;

                if (scanName != null)
                {
                    def = def.MoveTo(scanName.LocalName);

                    // If no specific next child is sought, return immediately
                    bool isMatch = name == null ||      // no name filter -> any match is ok
                        def.IsTracking && def.DefinedName == name ||    // else, filter on the defined name is available
                        scanName.LocalName == name;  // else, just match the element name on the filter

                    if (isMatch)
                    {
                        next = (instance, def);
                        return true;
                    }
                }

                instance = instance.NextChild();
            }

            next = null;
            return false;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            // don't move into xhtml
            if (AtXhtmlDiv) return false;

            XObject firstChild = null;

            string containedType = null;

            // If the child is a contained resource (the element name looks like a Resource name)
            // move one level deeper
            if (_current is XElement xe && tryGetNestedResourceName(xe, out containedType))
            {
                // todo: check this is in sync with firstChildDef, which should now also be
                // a resource definition
                firstChild = xe.Elements().First().FirstChild();
            }
            else
                firstChild = _current.FirstChild();

            if (firstChild == null) return false;

            var firstChildDef = down();

            if (!tryMatch((firstChild, firstChildDef), nameFilter, out var match)) return false;

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _nameIndex = 0;
            _current = match.Value.Item1;
            _definition = match.Value.Item2;

            return true;
        }


        private SerializationInfoCache down()
        {
            if (!_definition.IsTracking) return SerializationInfoCache.Empty;

            IComplexTypeSerializationInfo childType = null;

            // If this is a backbone element, the child type is the nested complex type
            if (_definition.Current.Type[0] is IComplexTypeSerializationInfo be)
                childType = be;

            else 
                childType = _definition.Provider.GetSerializationInfoForType(this.Type);

            return SerializationInfoCache.ForType(childType, _definition.Provider);
        }



        public bool MoveToNext(string nameFilter)
        {
            var nextChild = _current.NextChild();
            if (nextChild == null) return false;

            if (!tryMatch((nextChild, _definition), nameFilter, out var match)) return false;

            // store the current name before proceeding to detect repeating
            // element names and count them
            var currentName = Name;

            _current = match.Value.Item1;
            _definition = match.Value.Item2;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            return true;
        }

        private static bool isReservedAttribute(XAttribute attr) => attr.IsNamespaceDeclaration || attr.Name == "value";

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
            if (type == typeof(ElementSerializationInfo) && _definition.IsTracking)
            {
                return new[] { new ElementSerializationInfo(_definition.Current) };
            }
            if(type == typeof(PositionInfo))
            {
                return new[] { new PositionInfo { LineNumber = this.LineNumber, LinePosition = this.LinePosition } };
            }
            if (type == typeof(XmlSerializationDetails))
            {
                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        // Add: "value in attribute"
                        NodeType = _current.NodeType,
                        Namespace = XmlName.NamespaceName,
                        NodeText = _current.Text(),
                        IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false
                    }
                };
            }
            else
                return null;
        }
    }
}
