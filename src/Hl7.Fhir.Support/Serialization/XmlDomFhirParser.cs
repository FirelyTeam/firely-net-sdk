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
    public partial struct XmlDomFhirParser : INavigatingParser, IAnnotated, IPositionInfo, IExceptionSource
    {
        internal XmlDomFhirParser(XObject current)
        {
            OnExceptionRaised = null;
            _current = current;
            _nameIndex = 0;
            _parentPath = null;
        }

        public event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;

        public INavigatingParser Clone()
        {
            return new XmlDomFhirParser(_current)
            {
                _nameIndex = this._nameIndex,
                _parentPath = this._parentPath,
                OnExceptionRaised = this.OnExceptionRaised
            };
        }

        private XObject _current;
        private int _nameIndex;
        private string _parentPath;

        internal XName XmlName => _current.Name();
        public string Name => XmlName.LocalName;

        public string Literal
        {
            get
            {
                if (AtXhtmlDiv)
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);

                return _current.Value();
            }
        }

        public bool AtXhtmlDiv => (_current as XElement)?.Name == XmlNs.XHTMLDIV;

        public string Location => _parentPath == null ? Name : $"{_parentPath}.{Name}[{_nameIndex}]";

        private XObject nextMatch(XObject root, string name, XObject startAfter = null)
        {
            var scan = startAfter == null ? root.FirstChild() : startAfter.NextChild();

            while (scan != null)
            {
                if (scan.NodeType == XmlNodeType.Attribute || scan.NodeType == XmlNodeType.Element)
                {
                    // If no name filter, return this next node
                    if (name == null) return scan;

                    // otherwise only return this as the next node if the name matches
                    XName scanName = scan.Name();
                    if (scanName?.LocalName == name) return scan;
                }

                // no match, move on
                scan = scan.NextChild();
            }

            // nothing found
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
            _nameIndex = 0;
            _current = found;

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

        public override string  ToString() => _current.ToString();

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
