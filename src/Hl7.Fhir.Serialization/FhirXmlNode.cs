/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
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
    public partial class FhirXmlNode : ISourceNode, IResourceTypeSupplier, IAnnotated, IExceptionSource
    {  
        internal FhirXmlNode(XObject node, FhirXmlParsingSettings settings) 
        {
            Current = node;
            Location = Name;
            _settings = settings?.Clone() ?? new FhirXmlParsingSettings();
            _atRoot = true;
        }

        private FhirXmlNode(FhirXmlNode parent, XObject node, string location)
        {
            Current = node;
            Location = location ?? Name;
            _settings = parent._settings;
            ExceptionHandler = parent.ExceptionHandler;
            _atRoot = false;
        }

        public readonly XObject Current;
        private readonly FhirXmlParsingSettings _settings;
        private readonly bool _atRoot = false;

        public XNamespace[] AllowedExternalNamespaces => _settings.AllowedExternalNamespaces;
        public bool DisallowSchemaLocation => _settings.DisallowSchemaLocation;
        public bool PermissiveParsing => _settings.PermissiveParsing;

#if !NETSTANDARD1_1
        public bool ValidateFhirXhtml => _settings.ValidateFhirXhtml;
#endif

        private XElement _containedResource;

        public string Name => Current.Name()?.LocalName;

        public string Text
        {
            get
            {
                // The MoveNext()/MoveFirst() method will already have complained about empty attributes, 
                // so make sure we nicely recover here by returning null.
                var val = Current.GetValue();
                return !String.IsNullOrWhiteSpace(val) ? val : null;
            }
        }


        private static readonly XElement NO_CONTAINED_FOUND = new XElement("dummy");


        private XElement contained
        {
            get
            {
                if (_containedResource == null)
                {
                    if (Current is XElement xe && xe.TryGetContainedResource(out XElement contained))
                    {
                        bool errorEncountered = verifyContained(contained, this, PermissiveParsing);

                        _containedResource = PermissiveParsing && errorEncountered ? NO_CONTAINED_FOUND : contained;
                    }
                    else
                        _containedResource = NO_CONTAINED_FOUND;
                }

                return _containedResource == NO_CONTAINED_FOUND ? null : _containedResource;
            }
        }

        private static void verifyXObject(XObject node, XNamespace[] AllowedExternalNamespaces, object source, IExceptionSource ies)
        {
            var allowedNs = AllowedExternalNamespaces ?? new XNamespace[0];

            if (node is XAttribute xa)
            {
                if (xa.Name.NamespaceName != "" && !allowedNs.Contains(xa.Name.NamespaceName))
                    raiseFormatError(source, ies, $"The attribute '{xa.Name.LocalName}' in element '{xa.Parent.Name.LocalName}' uses the namespace '{xa.Name.NamespaceName}', which is not allowed.", node);

                if (String.IsNullOrWhiteSpace(xa.Value))
                    raiseFormatError(source, ies, $"The attribute '{xa.Name.LocalName}' in element '{xa.Parent.Name.LocalName}' has an empty value, which is not allowed.", node);
            }
            else if (node is XElement xe)
            {
                if (xe.Name.Namespace != XmlNs.XFHIR && xe.Name != XmlNs.XHTMLDIV && !allowedNs.Contains(xe.Name.Namespace))
                {
                    var ns = xe.Name.Namespace?.NamespaceName;
                    if (String.IsNullOrEmpty(ns))
                    {
                        raiseFormatError(source, ies, $"The element '{xe.Name.LocalName}' has no namespace, " +
                            $"expected the HL7 FHIR namespace (http://hl7.org/fhir)", node);
                    }
                    else
                        raiseFormatError(source, ies, $"The element '{xe.Name.LocalName}' uses the namespace '{xe.Name.NamespaceName}', which is not allowed.", node);
                }
            }
            else
                raiseFormatError(source, ies, $"Xml node of type '{node.NodeType}' is unexpected at this point", node);
        }

        public string Location { get; private set; }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        // If we're on the root, the root is the resource type,
        // otherwise we should have looked at a nested node.
        public string ResourceType => !_atRoot ?
                            contained?.Name()?.LocalName : Current.Name().LocalName;


        public IEnumerable<ISourceNode> Children(string name = null)
        {
            // don't move into xhtml
            if (Current.AtXhtmlDiv())
            {
#if !NETSTANDARD1_1
                if (!PermissiveParsing && ValidateFhirXhtml)
                    ValidateXhtml(new XDocument(Current), this, this);
#endif
                yield break;
            }

            // can't move into anything that's not an XElement
            if (!(Current is XElement element)) yield break;

            // If the child is a contained resource (the element name looks like a Resource name)
            // move one level deeper
            var parent = contained ?? element;
            var schemaAttr = parent.Attribute(XmlNs.XSCHEMALOCATION);
            if (schemaAttr != null && DisallowSchemaLocation)
                raiseFormatError(this, this, "The 'schemaLocation' attribute is disallowed.", schemaAttr);

            XObject firstChild = parent.FirstChildElementOrAttribute();

            if (firstChild == null)
            {
                if (!PermissiveParsing)
                    raiseFormatError(this, this, $"Element '{parent.Name().LocalName}' must have child elements and/or a value attribute", Current);

                yield break;
            }

            foreach(var child in enumerateChildren(firstChild, name)) yield return child;
        }

        private IEnumerable<FhirXmlNode> enumerateChildren(XObject first, string name = null)
        {
            var _names = new Dictionary<string, int>();

            var scan = first;
            string previousName = null;

            do
            {
                if (!PermissiveParsing) verifyXObject(scan, AllowedExternalNamespaces, this, this);
                
                if (scan.Name() != "value")
                {
                    var scanName = scan.Name().LocalName;
                    bool isMatch = scanName.MatchesPrefix(name);

                    if (isMatch)
                    {                        
                        if (_names.ContainsKey(scanName))
                        {
                            _names[scanName] += 1;
                            if (previousName != scanName && !PermissiveParsing)
                            {
                                // the name appeared before, but not contiguously
                                raiseFormatError(this, this, $"Element with name '{scanName}' was found multiple times, but not in sequence.", scan);
                            }
                        }
                        else
                        {
                            _names[scanName] = 1;
                            previousName = scanName;
                        }


                        var path = $"{Location}.{scanName}[{_names[scanName] - 1}]";
                        var newChild = new FhirXmlNode(this, scan, path);
                        yield return newChild;
                    }
                }

                scan = scan.NextElementOrAttribute();
            }
            while (scan != null);
        }


        public bool MoveToNext(string nameFilter = null) => throw new NotImplementedException();

        public bool MoveToFirstChild(string nameFilter = null) => throw new NotImplementedException();

        public ISourceNode Clone() => throw new NotImplementedException();

        public override string ToString() => Current.ToString();

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(FhirXmlNode) || type == typeof(ISourceNode) || type == typeof(IResourceTypeSupplier))
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
                        CommentsBefore = commentsBefore(Current),
                        ClosingComments = closingComment(Current),
                        DocumentEndComments = docEndComments(Current)
                    }
                };

                string[] commentsBefore(XObject current) =>
                        current is XNode xn ?
                            filterComments(xn.PreviousNodes()) : new string[0];

                string[] closingComment(XObject current)
                {
                    return current is XContainer xc && xc.LastNode != null
                        ? filterComments(cons(xc.LastNode, xc.LastNode.PreviousNodes()))
                        : (new string[0]);
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
                var (lineNumber, linePosition) = getPosition(Current);

                return new[]
                {
                    new XmlSerializationDetails()
                    {
                        // Add: "value in attribute"
                        NodeType = Current.NodeType,
                        Namespace = Current.Name().NamespaceName,
                        NodeText = Current.Text(),
                        IsNamespaceDeclaration = (Current is XAttribute xa) ? xa.IsNamespaceDeclaration : false,
                        OriginalValue = Current.Value(),
                        LineNumber = lineNumber,
                        LinePosition = linePosition,
                        SchemaLocation = getSchemaLocation(),
                        IsXhtml = Current.AtXhtmlDiv()
                    }
                };

                string getSchemaLocation()
                {
                    if (Current is XElement slparent)
                    {
                        var sl = slparent.Attribute(XmlNs.XSCHEMALOCATION);
                        if (sl != null) return sl.Value;
                    }

                    return null;
                }
            }
            else
                return Enumerable.Empty<object>();
        }

#if !NETSTANDARD1_1
        //public static void ValidateXhtml(string xmlText, IExceptionSource ies, object source)
        //{
        //    reportOnValidation(() =>
        //       SerializationUtil.RunFhirXhtmlSchemaValidation(xmlText), ies, source);
        //}

        public static void ValidateXhtml(XDocument doc, IExceptionSource ies, object source)
        {
            // TODO: When this is moved out of FhirXmlNode to a later validation phase,
            // update the error reporting too.
            string[] messages = SerializationUtil.RunFhirXhtmlSchemaValidation(doc);

            if (messages.Any())
            {
                var problems = String.Join(", ", messages);
                if (source is XObject xo)
                    raiseFormatError(source, ies, "Encountered narrative with incorrect Xhtml. Xsd validation reported: " + problems, xo);
                else
                    ies.NotifyOrThrow(source, ExceptionNotification.Error(
                        Error.Format("Parser: Encountered narrative with incorrect Xhtml. Xsd validation reported: " + problems)));
            }
        }
#endif

        private static void raiseFormatError(object source, IExceptionSource ies, string message, XObject position)
        {
            var (lineNumber, linePosition) = getPosition(position);
            ies.NotifyOrThrow(source, ExceptionNotification.Error(Error.Format("Parser: " + message, lineNumber, linePosition)));
        }

        private static (int lineNumber, int linePosition) getPosition(XObject node) => 
            node is IXmlLineInfo xli ? (xli.LineNumber, xli.LinePosition) : (-1, -1);

        private static bool verifyContained(XElement contained, IExceptionSource ies, bool permissive)
        {
            bool errorEncountered = false;
            XElement container = contained.Parent;

            if (container.HasRelevantAttributes() && !permissive)
            {
                raiseFormatError(container, ies, $"The element '{container.Name.LocalName}' has a contained resource and therefore should not have attributes.", container.Attributes().First());
                errorEncountered = true;
            }

            if (contained.HasRelevantAttributes() && !permissive)
            {
                raiseFormatError(contained, ies, $"The contained resource '{contained.Name.LocalName}' in container '{container.Name.LocalName}' should not have attributes.", contained.Attributes().First());
                errorEncountered = true;
            }

            if (contained.NextNode != null && !permissive)
            {
                raiseFormatError(container, ies, $"The element '{container.Name.LocalName}' has a contained resource and therefore should only have one child.", contained.NextNode);
                errorEncountered = true;
            }

            return errorEncountered;
        }

        private struct OrderRuleState
        {
            public string Name;
            public int Order;
        }
#pragma warning disable 612, 618
        private IEnumerable<AdditionalStructuralRule> additionalTypeRules()
        {
            yield return checkRepresentation;
            yield return checkOrder;

            object checkOrder(ITypedElement node, IExceptionSource ies, object state)
            {
                var sdSummary = node.Definition;
                if (sdSummary == null) return null;

                if (state is OrderRuleState ors)
                {
                    var (lastName, lastOrder) = (ors.Name, ors.Order);

                    if (sdSummary.Order < lastOrder)
                        ies.ExceptionHandler.NotifyOrThrow(node, buildException($"Element '{node.Name}' is not in the correct order and should come before element '{lastName}'."));
                }

                if (sdSummary.Representation == XmlRepresentation.XmlElement)
                    return new OrderRuleState() { Name = node.Name, Order = sdSummary.Order };
                else
                    // No change in last order, since we're not an element
                    return state;
            }

            object checkRepresentation(ITypedElement node, IExceptionSource ies, object _)
            {
                var sdSummary = node.Definition;
                var serializationDetails = node.GetXmlSerializationDetails();
                if (sdSummary == null || serializationDetails == null) return null;

                var representation = sdSummary.Representation;

                switch (representation)
                {
                    case XmlRepresentation.XHtml when !serializationDetails.IsXhtml:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should be an XHTML element.")));
                        break;
                    case XmlRepresentation.XmlAttr when serializationDetails.NodeType != XmlNodeType.Attribute:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should be an XML attribute.")));
                        break;
                    case XmlRepresentation.XmlElement when serializationDetails.NodeType != XmlNodeType.Element:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should be an XML element.")));
                        break;
                    case XmlRepresentation.XmlText when serializationDetails.NodeType != XmlNodeType.Text:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should use XML node text.")));
                        break;
                    case XmlRepresentation.CdaText when !serializationDetails.IsCDAText:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should use CDA text.")));
                        break;
                    case XmlRepresentation.TypeAttr when !serializationDetails.IsXsiType:
                        ies.NotifyOrThrow(node, buildException(
                            buildMessage(node.Name, serializationDetails.NodeType, "should use an xsi:type attribute.")));
                        break;
                }
                string buildMessage(string name, XmlNodeType actualType, string message) =>
                    $"{actualType} '{name}' {message}";

                return null;
            }
        }

        private ExceptionNotification buildException(string message) => ExceptionNotification.Error(
                new StructuralTypeException("Parser: " + message));
    }
}
