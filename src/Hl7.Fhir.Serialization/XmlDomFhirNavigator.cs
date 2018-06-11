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
        private XmlDomFhirNavigator(NavigatorPosition<XObject> current, SerializationInfoCache definition, IModelMetadataProvider provider)
        {
            _current = current;
            _definition = definition;
            _parentPath = null;
            _nameIndex = 0;

            Provider = provider;

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
            var elementType = provider?.GetSerializationInfoForStructure(type);

            var current = NavigatorPosition<XObject>.ForRoot(root, elementType, root.Name.LocalName);
            var definition = current.IsTracking ?
                SerializationInfoCache.ForRoot(current.SerializationInfo) : SerializationInfoCache.Empty;

            return new XmlDomFhirNavigator(current, definition, provider);
        }

        public event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;

        public IElementNavigator Clone() => this;        // the struct will be copied upon return

        private NavigatorPosition<XObject> _current;

        private int _nameIndex;
        private string _parentPath;

        private SerializationInfoCache _definition;
        public IModelMetadataProvider Provider { get; private set; }

        public string Name => _current.Name;

        public string Type
        {
            get
            {
                // This is an optimization: instead of determining the type upfront, we only
                // do it when it's being asked for, this way we can avoid doing the next few
                // steps every single time we navigate.
                if (_current.Node is XElement element)
                {
                    // Make sure we only get the type directly from the typename when we encounter a root,
                    // otherwise get it from the first nested element under the current element
                    if (_parentPath == null && element.Name.IsResourceName()) return element.Name.LocalName;
                    if (_parentPath != null && element.TryGetContainedResourceName(out var name)) return name;
                }

                // If it is not a contained resource, we've already figured out the type when navigating
                return _current.InstanceType;
            }
        }

        public object Value
        {
            get
            {
                string literal = _current.Node.GetValue(Type);

                // Without type info, just return a string
                if (literal == null || !_current.IsTracking)
                    return literal;
                else
                    return PrimitiveTypeConverter.FromSerializedValue(literal, Type);
            }
        }

        private static bool tryMatch(SerializationInfoCache dis, XObject current, string name, out NavigatorPosition<XObject> match)
        {
            var scan = current;

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
                    var found = dis.Find(scanName.LocalName, out var elementInfo, out var instanceType);
                    var targetName = found ? elementInfo.ElementName : scanName.LocalName;

                    bool isMatch = name == null ||      // no name filter -> any match is ok
                        targetName == name;    // else, filter on the actual name

                    if (isMatch)
                    {
                        match = new NavigatorPosition<XObject>(scan, elementInfo, targetName, instanceType);
                        return true;
                    }
                }

                scan = scan.NextSibling();
            }

            match = null;
            return false;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            // don't move into xhtml
            if (_current.Node.AtXhtmlDiv()) return false;

            XObject firstChild = null;

            // If the child is a contained resource (the element name looks like a Resource name)
            // move one level deeper
            if (_current.Node is XElement xe && tryGetNestedResourceName(xe, out string containedType))
            {
                // todo: check this is in sync with firstChildDef, which should now also be
                // a resource definition
                firstChild = xe.Elements().First().FirstChild();
            }
            else
                firstChild = _current.Node.FirstChild();

            if (firstChild == null) return false;

            var firstChildDef = down();

            if (!tryMatch(firstChildDef, firstChild, nameFilter, out var match)) return false;

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _nameIndex = 0;
            _current = match;
            _definition = firstChildDef;

            return true;
        }


        private SerializationInfoCache down()
        {
            if (!_current.IsTracking) return SerializationInfoCache.Empty;

            IComplexTypeSerializationInfo childType = null;

            // If this is a backbone element, the child type is the nested complex type
            if (_current.SerializationInfo.Type[0] is IComplexTypeSerializationInfo be)
                childType = be;
            else
                childType = Provider.GetSerializationInfoForStructure(this.Type);

            return SerializationInfoCache.ForType(childType);
        }



        public bool MoveToNext(string nameFilter)
        {
            var nextChild = _current.Node.NextSibling();
            if (nextChild == null) return false;

            if (!tryMatch(_definition, nextChild, nameFilter, out var match)) return false;

            // store the current name before proceeding to detect repeating
            // element names and count them
            var currentName = Name;

            _current = match;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            return true;
        }


        public string Location
        {
            get
            {
                if (_parentPath == null)
                    return Name;
                else
                {
                    if (_current.IsTracking && _current.SerializationInfo.MayRepeat == false)
                        return $"{_parentPath}.{Name}";
                    else
                        return $"{_parentPath}.{Name}[{_nameIndex}]";
                }
            }
        }

        private static bool isReservedAttribute(XAttribute attr) => attr.IsNamespaceDeclaration || attr.Name == "value";

        public override string ToString() => _current.ToString();

        internal XName XmlName => _current.Node.Name();

        public int LineNumber => (_current.Node as IXmlLineInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current.Node as IXmlLineInfo)?.LinePosition ?? -1;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(SourceComments))
            {
                return new[]
                {
                    new SourceComments()
                    {
                        CommentsBefore = commentsBefore(_current.Node),
                        ClosingComments = closingComment(_current.Node),
                        DocumentEndComments = docEndComments(_current.Node)
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
            if (type == typeof(ElementSerializationInfo) && _current.IsTracking)
            {
                return new[] { new ElementSerializationInfo(_current.SerializationInfo) };
            }
            if (type == typeof(PositionInfo))
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
                        NodeType = _current.Node.NodeType,
                        Namespace = XmlName.NamespaceName,
                        NodeText = _current.Node.Text(),
                        IsNamespaceDeclaration = (_current.Node is XAttribute xa) ? xa.IsNamespaceDeclaration : false
                    }
                };
            }
            else
                return null;
        }
    }
}
