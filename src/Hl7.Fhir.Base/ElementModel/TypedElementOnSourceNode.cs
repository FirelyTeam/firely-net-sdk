/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel
{
    internal class TypedElementOnSourceNode : ITypedElement, IAnnotated, IExceptionSource, IShortPathGenerator
    {
        private const string XHTML_INSTANCETYPE = "xhtml";
        private const string XHTML_DIV_TAG_NAME = "div";

        public TypedElementOnSourceNode(ISourceNode source, string type, IStructureDefinitionSummaryProvider provider, TypedElementSettings settings = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));

            Provider = provider ?? throw Error.ArgumentNull(nameof(provider));
            _settings = settings ?? new TypedElementSettings();

            if (source is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);

            ShortPath = source.Name;
            _source = source;
            (InstanceType, Definition) = buildRootPosition(type);
        }

        private (string instanceType, IElementDefinitionSummary definition) buildRootPosition(string type)
        {
            var rootType = type ?? _source.GetResourceTypeIndicator();
            if (rootType == null)
            {
                if (_settings.ErrorMode == TypedElementSettings.TypeErrorMode.Report)
                    throw Error.Format(nameof(type), $"Cannot determine the type of the root element at '{_source.Location}', " +
                        $"please supply a type argument.");
                else
                    return (rootType, null);
            }

            var elementType = Provider.Provide(rootType);

            if (elementType == null)
            {
                if (_settings.ErrorMode == TypedElementSettings.TypeErrorMode.Report)
                    throw Error.Format(nameof(type), $"Cannot locate type information for type '{rootType}'");
                else
                    return (rootType, null);
            }

            if (elementType.IsAbstract)
                throw Error.Argument(nameof(elementType), $"The type of a node must be a concrete type, '{elementType.TypeName}' is abstract.");

            var rootTypeDefinition = ElementDefinitionSummary.ForRoot(elementType, _source.Name);
            return (rootType, rootTypeDefinition);
        }


        private TypedElementOnSourceNode(TypedElementOnSourceNode parent, ISourceNode source, IElementDefinitionSummary definition, string instanceType, string prettyPath)
        {
            _source = source;
            ShortPath = prettyPath;
            Provider = parent.Provider;
            ExceptionHandler = parent.ExceptionHandler;
            Definition = definition;
            InstanceType = instanceType;
            _settings = parent._settings;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private void raiseTypeError(string message, object source, bool warning = false, string location = null)
        {
            var exMessage = $"Type checking the data: {message}";
            if (!string.IsNullOrEmpty(location))
                exMessage += $" (at {location})";

            var exc = new StructuralTypeException(exMessage);
            var notification = warning ?
                ExceptionNotification.Warning(exc) :
                ExceptionNotification.Error(exc);

            ExceptionHandler.NotifyOrThrow(source, notification);
        }

        public string InstanceType { get; private set; }

        private readonly ISourceNode _source;

        public readonly IStructureDefinitionSummaryProvider Provider;

        private readonly TypedElementSettings _settings;

        public IElementDefinitionSummary Definition { get; private set; }

        public string Name => Definition?.ElementName ?? _source.Name;

        // [EK, 20190822] This is a temporary fix - it brings information in the ElementModel about which
        // FHIR primitive type uses which system type, so a mapping from FHIR.* -> System.*
        // This knowledge is probably needed elsewhere too, and conversely, ElementMode should
        // not be so tightly bound to FHIR here.  If we are going to support V2 or other models,
        // we'd need the same mapping for V2.* -> System.*, so this should actually be pluggable.
        // Maybe get this from the IStructureDefinitionSummaryProvider?  That machine knows what
        // model it is dealing with, so should have knowledge of these mappings.
        // The same holds true for our documentation in the header of ITypedElement, this shows
        // only these mappings, but it should not be bound to a single model.
        //
        // In fact one could derive this dynamically from the structure definition, by looking
        // at the type of the "value" element (e.g. String.value). Although this differs in 
        // R3 and R4, these value (and url and id elements by the way) will indicate which type
        // of system types there are, implicitly specifying the mapping between primitive
        // FHIR types and system types.
        private static Type tryMapFhirPrimitiveTypeToSystemType(string fhirType)
        {
            switch (fhirType)
            {
                case "boolean":
                    return typeof(P.Boolean);
                case "integer":
                case "unsignedInt":
                case "positiveInt":
                    return typeof(P.Integer);
                case "integer64":
                    return typeof(P.Long);
                case "time":
                    return typeof(P.Time);
                case "date":
                    return typeof(P.Date);
                case "instant":
                case "dateTime":
                    return typeof(P.DateTime);
                case "decimal":
                    return typeof(P.Decimal);
                case "string":
                case "code":
                case "id":
                case "uri":
                case "oid":
                case "uuid":
                case "canonical":
                case "url":
                case "markdown":
                case "base64Binary":
                case "xhtml":
                    return typeof(P.String);
                default:
                    return null;
            }
        }

        private object valueFactory()
        {
            string sourceText = _source.Text;

            if (sourceText == null) return null;

            // If we don't have type information (no definition was found
            // for current node), all we can do is return the underlying string value
            if (InstanceType == null) return sourceText;

            // For performance reasons, let's try the normal case first - a non logical type
            var ts = tryMapFhirPrimitiveTypeToSystemType(InstanceType);

            // If that failed, the type might still be a custom "primitive" type for logical models
            if (ts is null && Uri.IsWellFormedUriString(InstanceType, UriKind.Absolute))
            {
                // Get the definition of the logical type
                var summary = Provider.Provide(InstanceType);

                // And find the `value` attribute, which all primitives should have, even in logical models
                var valueType = summary?.GetElements().FirstOrDefault(e => e.ElementName.Equals("value"))?.Type.FirstOrDefault()?.GetTypeName();
                ts = valueType is { } ? tryMapFhirPrimitiveTypeToSystemType(valueType) : null;

                // Still, even this failed, we need to bail out.
                if (ts is null)
                    throw new InvalidOperationException($"Cannot figure out what the primitive type is for the value of logical type '{InstanceType}'.");
            }

            if (ts == null)
            {
                raiseTypeError($"Since type {InstanceType} is not a primitive, it cannot have a value", _source, location: _source.Location);
                return null;
            }

            // Finally, we have a (potentially) unparsed string + type info
            // parse this primitive into the desired type
            if (P.Any.TryParse(sourceText, ts, out var val))
                return val;
            else
            {
                // Check for the exception we have made to allow 1.x behaviour
                // where datetime's were considered acceptable for date elements.
                // In addition the TruncateDateTimeToDate will also "correct" the
                // datetime values to correct just-date values while parsing here.
#pragma warning disable CS0618 // Type or member is obsolete
                if (_settings.TruncateDateTimeToDate && ts == typeof(P.Date))
#pragma warning restore CS0618 // Type or member is obsolete
                {
                    if (P.Any.TryParse(sourceText, typeof(P.DateTime), out var dateTimeVal))
                    {
                        // TruncateToDate converts 1991-02-03T11:22:33Z to 1991-02-03+00:00 which is not a valid date! 
                        var date = (dateTimeVal as P.DateTime).TruncateToDate();
                        // so we cut off timezone by converting it to timeoffset and cast back to date.
                        return P.Date.FromDateTimeOffset(date.ToDateTimeOffset(0, 0, 0, TimeSpan.Zero));
                    }
                }

                raiseTypeError($"Literal '{sourceText}' cannot be parsed as a {InstanceType}.", _source, location: _source.Location);
                return sourceText;
            }
        }

        private object _value;
        private bool _valueInitialized = false;
        private static object _initializationLock = new();

        public object Value => LazyInitializer.EnsureInitialized(ref _value, ref _valueInitialized, ref _initializationLock, valueFactory);

        private string deriveInstanceType(ISourceNode current, IElementDefinitionSummary info)
        {
            var resourceTypeIndicator = current.GetResourceTypeIndicator();

            // First, handle the case where this (appears to be) a resource.
            if (info.IsResource)
            {
                if (resourceTypeIndicator == null) raiseTypeError($"Element '{current.Name}' should contain a resource, but does not actually contain one", current, location: current.Location);
                return resourceTypeIndicator;
            }
            else if (resourceTypeIndicator != null)
            {
                raiseTypeError($"Element '{current.Name}' is not a contained resource, but seems to contain a resource of type '{resourceTypeIndicator}'.", current, location: current.Location);
                return resourceTypeIndicator;
            }

            // Now, it's a data element. But is a choice, so we need to take
            // a look at the suffix on the element's name to figure out what we
            // are dealing with.
            if (info.IsChoiceElement)
            {
                var suffix = current.Name.Substring(info.ElementName.Length);

                if (string.IsNullOrEmpty(suffix))
                {
                    raiseTypeError($"Choice element '{current.Name}' is not suffixed with a type.", current, location: current.Location);
                    return null;
                }

                suffix = normalizeSuffix(suffix);

                // If any of the types is the abstract 'DataType' type, we'll just
                // accept whatever type is mentioned, we have no real list to go on =>
                // An example of where this happens is Extension.value[x], which is
                // a POCO that is common to all (FHIR) datamodels, and since value[x] is an
                // "open" types, we have no idea which types are allowed.
                if (info.Type.Any(t => t.GetTypeName() == "DataType"))
                    return suffix;
                else
                {

                    var isListed = info.Type
                        .OfType<IStructureDefinitionReference>()
                        .Select(t => t.ReferredType)
                        .Any(t => t == suffix);

                    if (!isListed)
                        raiseTypeError($"Choice element '{current.Name}' is suffixed with unexpected type '{suffix}'", current, location: current.Location);

                    return suffix;
                }

                string normalizeSuffix(string s)
                {
                    // Unfortunately, we decided to nicely capitalize the suffix, even for
                    // primitives - luckily then, there's just a single list of primitives,
                    // so we can "correct" them
                    return _suffixMap.TryGetValue(s, out var corrected) ? corrected : s;
                };
            }
            else if (info.Representation == XmlRepresentation.TypeAttr) // May be used by models other then FHIR, e.g. CCDA represented by a StructureDefinition
            {
                if (info.Type.Count() == 1)
                {
                    return info.Type.Single().GetTypeName();
                }
                else
                {
                    var typeName = current.Children("type").FirstOrDefault()?.Text;
                    return typeName != null
                        ? info.Type.Where(type => typeFromLogicalModelCanonical(type).Equals(typeName)).FirstOrDefault()?.GetTypeName()
                        : info.DefaultTypeName;
                }
            }

            var tp = info.Type.Single();
            return tp.GetTypeName();
        }

        private static readonly Dictionary<string, string> _suffixMap = new Dictionary<string, string>()
        {
            { "Boolean", "boolean" },
            { "Integer", "integer" },
            { "Integer64", "integer64" },
            { "UnsignedInt", "unsignedInt" },
            { "PositiveInt","positiveInt" },
            { "Time","time" },
            { "Date","date" },
            { "Instant","instant" },
            { "DateTime", "dateTime" },
            { "Decimal","decimal" },
            { "String", "string" },
            { "Code","code" },
            { "Id","id" },
            { "Uri","uri" },
            { "Oid","oid" },
            { "Uuid", "uuid" },
            { "Canonical","canonical" },
            { "Url", "url" },
            { "Markdown", "markdown" },
            { "Base64Binary", "base64Binary" },
            { "Xhtml","xhtml" }
        };


        private string typeFromLogicalModelCanonical(ITypeSerializationInfo info)
        {
            var type = info.GetTypeName();
            var pos = type.LastIndexOf('/'); // Match the "raw" type name from the complete type name of the logical model type (absolute url) 
            return pos > -1 ? type.Substring(pos + 1) : type;
        }

        private bool tryGetBySuffixedName(Dictionary<string, IElementDefinitionSummary> dis, string name, out IElementDefinitionSummary info)
        {
            // Simplest case, one on one match between name and element name
            if (dis.TryGetValue(name, out info))
                return true;

            // Now, check the choice elements for a match          
            var matches = dis.Where(kvp => name.StartsWith(kvp.Key) && kvp.Value.IsChoiceElement).ToList();

            // this will loop over all matches and return the longest match.
            if (matches.Any())
            {
                info = (matches.Count == 1)
                        ? matches[0].Value
                        : matches.Aggregate((l, r) => l.Key.Length > r.Key.Length ? l : r).Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerable<TypedElementOnSourceNode> enumerateElements(Dictionary<string, IElementDefinitionSummary> dis, ISourceNode parent, string name)
        {
            IEnumerable<ISourceNode> childSet;

            // no name filter: work on all the parent's children
            if (name == null)
                childSet = parent.Children();
            else
            {
                var hit = dis.TryGetValue(name, out var info);
                childSet = hit
                    ? (info.IsChoiceElement ? parent.Children(name + "*") : parent.Children(name))
                    : Enumerable.Empty<ISourceNode>();
            }

            string lastName = null;
            int _nameIndex = 0;

            foreach (var scan in childSet)
            {
                var hit = tryGetBySuffixedName(dis, scan.Name, out var info);
                string instanceType = info == null ? null :
                    deriveInstanceType(scan, info);

                // If we have definitions for the children, but we didn't find definitions for this 
                // child in the instance, complain
                if (dis.Any() && info == null)
                {
                    raiseTypeError($"Encountered unknown element '{scan.Name}' at location '{scan.Location}' while parsing", this,
                              warning: _settings.ErrorMode != TypedElementSettings.TypeErrorMode.Report);

                    // don't include member, unless we are explicitly told to let it pass
                    if (_settings.ErrorMode != TypedElementSettings.TypeErrorMode.Passthrough)
                        continue;
                }

                if (lastName == scan.Name)
                    _nameIndex += 1;
                else
                {
                    _nameIndex = 0;
                    lastName = scan.Name;
                }

                var prettyPath =
                 hit && !info.IsCollection ? $"{ShortPath}.{info.ElementName}" : $"{ShortPath}.{scan.Name}[{_nameIndex}]";

                // Special condition for ccda.
                // If we encounter a xhtml node in a ccda document we will flatten all childnodes
                // and use their content to build up the xml.
                // The xml will be put in this node and children will be ignored.
                if (instanceType == XHTML_INSTANCETYPE && info.Representation == XmlRepresentation.CdaText)
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    var xmls = scan.Children().Select(c => c.Annotation<ICdaInfoSupplier>()?.XHtmlText);
#pragma warning restore CS0618 // Type or member is obsolete

                    var source = SourceNode.Valued(scan.Name, string.Join(string.Empty, xmls));
                    yield return new TypedElementOnSourceNode(this, source, info, instanceType, prettyPath);
                    continue;
                }

                yield return new TypedElementOnSourceNode(this, scan, info, instanceType, prettyPath);
            }
        }

        public IEnumerable<ITypedElement> Children(string name = null)
        {
            // If we have an xhtml typed node and there is not a div tag around the content
            // then we will not enumerate through the children of this node, since there will be no types
            // matching the html tags.
            if (this.InstanceType == XHTML_INSTANCETYPE && Name != XHTML_DIV_TAG_NAME)
            {
                return Enumerable.Empty<ITypedElement>();
            }

            var childElementDefs = this.ChildDefinitions(Provider).ToDictionary(c => c.ElementName);

            if (Definition != null && !childElementDefs.Any())
            {
                // No type information available for the type representing the children....

                if (InstanceType != null && _settings.ErrorMode == TypedElementSettings.TypeErrorMode.Report)
                    raiseTypeError($"Encountered unknown type '{InstanceType}'", _source, location: _source.Location);

                // Don't go on with the (untyped) children, unless explicitly told to do so
                if (_settings.ErrorMode != TypedElementSettings.TypeErrorMode.Passthrough)
                    return Enumerable.Empty<ITypedElement>();
                else
                    // Ok, pass through the untyped members, but since there is no type information, 
                    // don't bother to run the additional rules
                    return enumerateElements(childElementDefs, _source, name);
            }
            else
                return runAdditionalRules(enumerateElements(childElementDefs, _source, name));
        }

        private IEnumerable<ITypedElement> runAdditionalRules(IEnumerable<ITypedElement> children)
        {
#pragma warning disable 612, 618
            var additionalRules = _source.Annotations(typeof(AdditionalStructuralRule));
            var stateBag = new Dictionary<AdditionalStructuralRule, object>();
            foreach (var child in children)
            {
                foreach (var rule in additionalRules.Cast<AdditionalStructuralRule>())
                {
                    stateBag.TryGetValue(rule, out object state);
                    state = rule(child, this, state);
                    if (state != null) stateBag[rule] = state;
                }

                yield return child;
            }
#pragma warning restore 612, 618
        }

        public string Location => _source.Location;

        public string ShortPath { get; private set; }

        public override string ToString() =>
            $"{(InstanceType != null ? ($"[{InstanceType}] ") : "")}{_source}";

        public IEnumerable<object> Annotations(Type type)
        {
#pragma warning disable IDE0046 // Convert to conditional expression
            if (type == typeof(TypedElementOnSourceNode) || type == typeof(ITypedElement) || type == typeof(IShortPathGenerator))
#pragma warning restore IDE0046 // Convert to conditional expression
                return new[] { this };
            else
                return _source.Annotations(type);
        }
    }

    [Obsolete("This class is used for internal purposes and is subject to change without notice. Don't use.")]
    public delegate object AdditionalStructuralRule(ITypedElement node, IExceptionSource ies, object state);
}

