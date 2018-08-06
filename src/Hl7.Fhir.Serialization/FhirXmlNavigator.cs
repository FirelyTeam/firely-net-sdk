/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial class FhirXmlNavigator : ISourceNavigator, IAnnotated, IExceptionSource
    {
        public FhirXmlNavigator(XElement root, FhirXmlNavigatorSettings settings = null)
        {
            _current = root;
            _parentPath = null;
            _containedResource = null;
            _names = new Dictionary<string, int>();
            _settings = settings?.Clone();
        }

        private FhirXmlNavigator() { }      // for Clone()

        public ISourceNavigator Clone()
        {
            return new FhirXmlNavigator()
            {
                _current = this._current,
                _parentPath = this._parentPath,
                _containedResource = this._containedResource,
                _names = new Dictionary<string, int>(this._names),
                _settings = _settings,
                ExceptionHandler = this.ExceptionHandler,
            };
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private FhirXmlNavigatorSettings _settings;
        private XObject _current;
        private string _parentPath;
        private XElement _containedResource;
        private Dictionary<string,int> _names;

        public XNamespace[] AllowedExternalNamespaces => _settings?.AllowedExternalNamespaces;
        public bool DisallowSchemaLocation => _settings?.DisallowSchemaLocation ?? false;
        public bool PermissiveParsing => _settings?.PermissiveParsing ?? false;

#if NET_XSD_SCHEMA
        public bool ValidateFhirXhtml => _settings?.ValidateFhirXhtml ?? false;
#endif

        public string Name => _current.Name()?.LocalName;

        public string Text
        {
            get
            {
                // The MoveNext()/MoveFirst() method will already have complained about empty attributes, 
                // so make sure we nicely recover here by returning null.
                var val = _current.GetValue();
                return !String.IsNullOrWhiteSpace(val) ? val : null;
            }
        }

        private bool tryFindByName(XObject current, string name, out XObject match)
        {
            var scan = current;

            do
            {
                if (!PermissiveParsing) verifyXObject(scan);

                var scanName = scan.Name();
                if (scanName != "value")
                {
                    bool isMatch =
                    name == null ||      // no name filter -> any match is ok
                        scanName.LocalName == name;    // else, filter on the actual name

                    if (isMatch)
                    {
                        match = scan;
                        return true;
                    }
                }

                scan = scan.NextElementOrAttribute();
            }
            while (scan != null);

            match = null;
            return false;
        }

        private void verifyXObject(XObject node)
        {
            var allowedNs = AllowedExternalNamespaces ?? new XNamespace[0];

            if (node is XAttribute xa)
            {
                if (xa.Name.NamespaceName != "" && !allowedNs.Contains(xa.Name.NamespaceName))
                    raiseFormatError($"The attribute '{xa.Name.LocalName}' in element '{xa.Parent.Name.LocalName}' uses the namespace '{xa.Name.NamespaceName}', which is not allowed.", node);

                if (String.IsNullOrWhiteSpace(xa.Value))
                    raiseFormatError($"The attribute '{xa.Name.LocalName}' in element '{xa.Parent.Name.LocalName}' has an empty value, which is not allowed.", node);
            }
            else if (node is XElement xe)
            {
                if (xe.Name.Namespace != XmlNs.XFHIR && xe.Name != XmlNs.XHTMLDIV && !allowedNs.Contains(xe.Name.Namespace))
                {
                    var ns = xe.Name.Namespace?.NamespaceName;
                    if (String.IsNullOrEmpty(ns))
                    {
                        raiseFormatError($"The element '{xe.Name.LocalName}' has no namespace, " +
                            $"expected the HL7 FHIR namespace (http://hl7.org/fhir)", node);
                    }
                    else
                        raiseFormatError($"The element '{xe.Name.LocalName}' uses the namespace '{xe.Name.NamespaceName}', which is not allowed.", node);
                }
            }
            else
                raiseFormatError($"Xml node of type '{node.NodeType}' is unexpected at this point", node);
        }

        public bool MoveToFirstChild(string name = null)
        {
            // don't move into xhtml
            if (_current.AtXhtmlDiv())
            {
#if NET_XSD_SCHEMA
                if (!PermissiveParsing && ValidateFhirXhtml)
                    ValidateXhtml(new XDocument(_current), this, this);
#endif
                return false;
            }

            // can't move into anything that's not an XElement
            if (!(_current is XElement element)) return false;

            // If the child is a contained resource (the element name looks like a Resource name)
            // move one level deeper
            var parent = Contained ?? element;
            var schemaAttr = parent.Attribute(XmlNs.XSCHEMALOCATION);
            if (schemaAttr != null && DisallowSchemaLocation)
                raiseFormatError($"The 'schemaLocation' attribute is disallowed.", schemaAttr);

            XObject firstChild = parent.FirstChildElementOrAttribute();

            if (firstChild == null)
            {
                if (!PermissiveParsing)
                    raiseFormatError($"Element '{parent.Name().LocalName}' must have child elements and/or a value attribute", _current);

                return false;
            }

            if (!tryFindByName(firstChild, name, out var match)) return false;

            // Found a match, so we can alter the current position of the navigator.
            // Modify _parentPath to be the current path before we do that
            _parentPath = Location;
            _current = match;
            _names = new Dictionary<string, int>() { { Name, 1 } };
            _containedResource = null;

            return true;
        }

#if NET_XSD_SCHEMA
        public static void ValidateXhtml(string xmlText, IExceptionSource ies, object source)
        {
            reportOnValidation(() =>
               SerializationUtil.RunFhirXhtmlSchemaValidation(xmlText), ies, source);
        }

        public static void ValidateXhtml(XDocument doc, IExceptionSource ies, object source)
        {
            reportOnValidation(() =>
                   SerializationUtil.RunFhirXhtmlSchemaValidation(doc), ies, source);
        }


        private static void reportOnValidation(Func<string[]> validator, IExceptionSource ies, object source)
        {
            var messages = validator();
            if (messages.Any())
            {
                var problems = String.Join(", ", messages);
                ies.ExceptionHandler.NotifyOrThrow(source, ExceptionNotification.Error(
                    Error.Format("The XHTML for the narrative is not valid. XSD validation reported: " + problems)));
            }
        }
#endif

        private static readonly XElement NO_CONTAINED_FOUND = new XElement("dummy");

        private void raiseFormatError(string message, XObject position)
        {
            var (lineNumber, linePosition) = getPosition(_current);
            ExceptionHandler.NotifyOrThrow(this, ExceptionNotification.Error(Error.Format(message, lineNumber, linePosition)));
        }

        private XElement Contained
        {
            get
            {
                if (_containedResource == null)
                {
                    if (_current is XElement xe && xe.TryGetContainedResource(out XElement contained))
                    {
                        bool errorEncountered = verifyContained(contained);

                        if (PermissiveParsing && errorEncountered)
                            _containedResource = NO_CONTAINED_FOUND;
                        else
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

        private bool verifyContained(XElement contained)
        {
            bool errorEncountered = false;
            XElement container = contained.Parent;

            if (container.HasRelevantAttributes() && !PermissiveParsing)
            {
                raiseFormatError($"The element '{container.Name.LocalName}' has a contained resource and therefore should not have attributes.", container.Attributes().First());
                errorEncountered = true;
            }

            if (contained.HasRelevantAttributes() && !PermissiveParsing)
            {
                raiseFormatError($"The contained resource '{contained.Name.LocalName}' in container '{container.Name.LocalName}' should not have attributes.", contained.Attributes().First());
                errorEncountered = true;
            }

            if (contained.NextNode != null && !PermissiveParsing)
            {
                raiseFormatError($"The element '{container.Name.LocalName}' has a contained resource and therefore should only have one child.", contained.NextNode);
                errorEncountered = true;
            }

            return errorEncountered;
        }

        public bool MoveToNext(string nameFilter)
        {
            var nextChild = _current.NextElementOrAttribute();
            if (nextChild == null) return false;

            if (!tryFindByName(nextChild, nameFilter, out var match)) return false;

            // store the current name before proceeding to detect repeating
            // element names and count them
            var previousName = Name;

            _current = match;
            if (_names.TryGetValue(Name, out int occurs))
            {
                _names[Name] += 1;
                if (previousName != Name && !PermissiveParsing)
                {
                    // the name appeared before, but not contiguously
                    raiseFormatError($"Element with name '{Name}' was found multiple times, but not in sequence.", match);
                }
            }
            else
                _names[Name] = 1;

            _containedResource = null;
            return true;
        }


        public string Location => _parentPath == null ? Name : $"{_parentPath}.{Name}[{_names[Name] - 1}]";

        public override string ToString() => _current.ToString();

        private (int lineNumber, int linePosition) getPosition(XObject node)
        {
            if (node is IXmlLineInfo xli)
                return (xli.LineNumber, xli.LinePosition);
            else
                return (-1, -1);
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(FhirXmlNavigator))
                return new[] { this };
#pragma warning disable 612, 618
            else if (type == typeof(AdditionalStructuralRule) && !PermissiveParsing)
                return additionalTypeRules();
#pragma warning restore 612, 618
            else if (type == typeof(SourceComments))
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
                var (lineNumber, linePosition) = getPosition(_current);

                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        // Add: "value in attribute"
                        NodeType = _current.NodeType,
                        Namespace = _current.Name().NamespaceName,
                        NodeText = _current.Text(),
                        IsNamespaceDeclaration = (_current is XAttribute xa) ? xa.IsNamespaceDeclaration : false,
                        OriginalValue = _current.Value(),
                        LineNumber = lineNumber,
                        LinePosition = linePosition,
                        SchemaLocation = getSchemaLocation(),
                        IsXhtml = _current.AtXhtmlDiv()
                    }
                };

                string getSchemaLocation()
                {
                    if (_current is XElement slparent)
                    {
                        var sl = slparent.Attribute(XmlNs.XSCHEMALOCATION);
                        if (sl != null) return sl.Value;
                    }

                    return null;
                }
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

#pragma warning disable 612, 618
        private IEnumerable<AdditionalStructuralRule> additionalTypeRules()
        {
            yield return checkRepresentation;
            yield return checkOrder;

            void checkOrder(TypedNavigator nav, IExceptionSource ies)
            {
                var sdSummary = ((IElementNavigator)nav).GetElementDefinitionSummary();
                if (sdSummary == null || nav.LastOrder == null) return;

                var (lastName, lastOrder) = nav.LastOrder.Value;

                if (sdSummary.Order < lastOrder)
                    ies.ExceptionHandler.NotifyOrThrow(nav, buildException($"Element '{nav.Name}' is not in the correct order and should come before element '{lastName}'."));
            }

            void checkRepresentation(IElementNavigator nav, IExceptionSource ies)
            {
                var sdSummary = nav.GetElementDefinitionSummary();
                var serializationDetails = nav.GetXmlSerializationDetails();
                if (sdSummary == null || serializationDetails == null) return;

                var representation = sdSummary.Representation;

                switch (representation)
                {
                    case XmlRepresentation.XHtml when !serializationDetails.IsXhtml:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should use an XHTML element.")));
                        break;
                    case XmlRepresentation.XmlAttr when serializationDetails.NodeType != XmlNodeType.Attribute:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should be an XML attribute.")));
                        break;
                    case XmlRepresentation.XmlElement when serializationDetails.NodeType != XmlNodeType.Element:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should be an XML element.")));
                        break;
                    case XmlRepresentation.XmlText when serializationDetails.NodeType != XmlNodeType.Text:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should use XML node text.")));
                        break;
                    case XmlRepresentation.CdaText when !serializationDetails.IsCDAText:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should use CDA text.")));
                        break;
                    case XmlRepresentation.TypeAttr when !serializationDetails.IsXsiType:
                        ies.ExceptionHandler.NotifyOrThrow(nav, buildException(
                            buildMessage(nav.Name, serializationDetails.NodeType, "should use an xsi:type attribute.")));
                        break;
                }
                string buildMessage(string name, XmlNodeType actualType, string message) =>
                    $"{actualType} '{name}' {message}";
            }
        }

        private ExceptionNotification buildException(string message) => ExceptionNotification.Error(
                new StructuralTypeException(message));
#pragma warning restore 612, 618

    }
}
