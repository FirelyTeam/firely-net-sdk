/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Support.Utility;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct UntypedXmlDomFhirNavigator : IElementNavigator, IAnnotated, IPositionInfo, IExceptionSource
    {
        internal UntypedXmlDomFhirNavigator(XElement root, Configuration config)
        {
            _current = root;
            _parentPath = null;
            _nameIndex = 0;
            _containedResource = null;

            Sink = config?.Get<IExceptionSink>();
        }

        public IElementNavigator Clone() => this;        // the struct will be copied upon return

        private XObject _current;
        private int _nameIndex;
        private string _parentPath;
        private XElement _containedResource;

        public string Name => _current.Name()?.LocalName;

        public string Type => throw new NotImplementedException("This untyped reader does not support reading the Type property.");

        public object Value => _current.GetValue();

        private static bool tryFindByName(XObject current, string name, out XObject match)
        {
            var scan = current;

            do
            {
                bool isMatch =
                    name == null ||      // no name filter -> any match is ok
                    scan.Name().LocalName == name;    // else, filter on the actual name

                if (isMatch)
                {
                    match = scan;
                    return true;
                }

                scan = scan.NextElementOrAttribute();
            }
            while (scan != null);

            match = null;
            return false;
        }

        public bool MoveToFirstChild(string name = null)
        {
            // don't move into xhtml
            if (_current.AtXhtmlDiv()) return false;

            // If the child is a contained resource (the element name looks like a Resource name)
            // move one level deeper
            XObject firstChild = Contained != null ?
                Contained.FirstElementOrAttribute() : _current.FirstElementOrAttribute();

            if (firstChild == null) return false;
            if (!tryFindByName(firstChild, name, out var match)) return false;

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _nameIndex = 0;
            _current = match;
            _containedResource = null;

            return true;
        }

        private static readonly XElement NO_CONTAINED_FOUND = new XElement("dummy");

        private void raiseException(ExceptionRaisedEventArgs e) => Sink?.Raise(e);

        private void raiseFormatError(string message, IElementNavigator current) =>
                 raiseException(ExceptionRaisedEventArgs.Error(
                         Error.Format(message, current as IPositionInfo)));

        private XElement Contained
        {
            get
            {
                if (_containedResource == null)
                {
                    if (_current is XElement xe && xe.TryGetContainedResource(out XElement contained))
                    {
                        if (contained.NextSibling() != null)
                            raiseFormatError("Contained resources should not have sibling elements", this);

                        if (contained.HasAttributes)
                            raiseFormatError("The root of a contained resources cannot have attributes.", this);

                        _containedResource = contained;
                    }
                    else
                        _containedResource = NO_CONTAINED_FOUND;
                }

                if (_containedResource == NO_CONTAINED_FOUND)
                    return null;
                else
                    return _containedResource; 
            }
        }

        public bool MoveToNext(string nameFilter)
        {
            var nextChild = _current.NextElementOrAttribute();
            if (nextChild == null) return false;

            if (!tryFindByName(nextChild, nameFilter, out var match)) return false;

            // store the current name before proceeding to detect repeating
            // element names and count them
            var currentName = Name;

            _current = match;

            if (currentName == Name)
                _nameIndex += 1;
            else
                _nameIndex = 0;

            _containedResource = null;
            return true;
        }


        public string Location => _parentPath == null ? Name : $"{_parentPath}.{Name}[{_nameIndex}]";

        public override string ToString() => _current.ToString();

        public int LineNumber => (_current as IXmlLineInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current as IXmlLineInfo)?.LinePosition ?? -1;

        public IExceptionSink Sink { get; set; }

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
                        NodeType = _current.NodeType,
                        Namespace = _current.Name().NamespaceName,
                        NodeText = _current.Text(),
                        IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false,
                    }
                };
            }
            if (type == typeof(ResourceTypeIndicator))
            {
                return new[]
                {
                    new ResourceTypeIndicator
                    {
                        // If we're on the root, the root is the resource type,
                        // otherwise we should have looked at a nested node.
                        ResourceType = _parentPath != null ?
                            Contained?.Name()?.LocalName : _current.Name().LocalName
                    }
                };
            }
            else
                return Enumerable.Empty<object>();
        }
    }
}
