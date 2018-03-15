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
    public partial struct XmlDomFhirNavigator : IElementNavigator, IAnnotated, IPositionInfo, IOutcomeProvider
    {
        internal XmlDomFhirNavigator(XObject current, IModelMetadataProvider metadataProvider)
        {
            _current = current;
            _metadataProvider = metadataProvider;
            _nameIndex = 0;
            _parentPath = null;
        }


        public IElementNavigator Clone()
        {
            var copy = new XmlDomFhirNavigator(_current, _metadataProvider)
            {
                _nameIndex = this._nameIndex,
                _parentPath = this._parentPath,
            };

            return copy;
        }

        private XObject _current;
        private readonly IModelMetadataProvider _metadataProvider;
        private int _nameIndex;
        private string _parentPath;

        public string Name => XmlName?.LocalName;

        public string Type => null;

        public object Value
        {
            get
            {
                if (AtXhtmlDiv)
                    return ((XElement)_current).ToString(SaveOptions.DisableFormatting);
                else if (_current is XElement xelem)
                    return xelem.Attribute("value")?.Value;
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

            // We've moved position, re-initialize some of the navigator status
            _parentPath = Location;
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


        //public const int FORMAT_VALUE_AND_TEXT_NODE = 1001;
        //public const int FORMAT_REPEATED_NESTED_RESOURCES = 1002;
        //public const int FORMAT_ATTRIBUTES_ON_NESTED_RESOURCE = 1002;
        //public const string SYSTEM_NAME = nameof(XmlDomFhirNavigator);

        public IEnumerable<Outcome> GetIssues() => Enumerable.Empty<Outcome>();

        internal static IElementNavigator Create(string xml, object @default)
        {
            throw new NotImplementedException();
        }
    }
}
