/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;
using NotSupportedException = System.NotSupportedException;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Deserializes XML into FHIR POCO objects.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/xml.html. </remarks>
public class BaseFhirXmlDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirXmlDeserializer(ModelInspector inspector) : this(inspector, new DeserializerSettings())
    {
        // nothing
    }


    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirXmlDeserializer(ModelInspector inspector, DeserializerSettings? settings)
    {
        Settings = settings ?? new DeserializerSettings();
        _inspector = inspector;
    }

    /// <summary>
    /// The settings that were passed to the constructor.
    /// </summary>
    public DeserializerSettings Settings { get; set; }

    private readonly ModelInspector _inspector;

    // Caches DeserializerSettings.UsesStrictCaseBinding(), which is invoked for every element, per
    // settings instance. The settings' relevant properties are init-only, so as long as the same
    // instance is assigned to Settings, the cached answer remains valid.
    private (DeserializerSettings settings, bool strict)? _caseBindingCache;

    private bool usesStrictCaseBinding()
    {
        if (_caseBindingCache is { } cached && ReferenceEquals(cached.settings, Settings)) return cached.strict;

        var strict = Settings.UsesStrictCaseBinding();
        _caseBindingCache = (Settings, strict);
        return strict;
    }

    /// <summary>
    /// Deserialize the FHIR xml from the reader and create a new POCO resource containing the data from the reader.
    /// </summary>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
    /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
    /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
    /// <remarks>The <see cref="ParserSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeResource(XmlReader reader, [NotNullWhen(true)] out Resource? instance, out IEnumerable<CodedException> issues)
    {
        var state = createState();

        // If the stream has just been opened, move to the first token. (skip processing instructions, comments, whitespaces etc.)
        reader.MoveToContent(state);

        if (reader.Settings is not null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;

        instance = DeserializeResourceInternal(reader, state);

        // Whatever is left after the root element has been read are the comments trailing the document.
        if (instance is not null)
            addSourceComments(instance, documentEnd: state.Comments?.Consume());

        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    /// <summary>
    /// Reads a (subtree) of serialzed FHIR Json data into a POCO object.
    /// </summary>
    /// <param name="targetType">The type of POCO to construct and deserialize</param>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
    /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
    /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
    /// <remarks>The <see cref="ParserSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeElement(Type targetType, XmlReader reader, [NotNullWhen(true)] out Base? instance, out IEnumerable<CodedException> issues)
    {
        var state = createState();

        // If the stream has just been opened, move to the first token. (skip processing instructions, comments, whitespaces etc.)
        reader.MoveToContent(state);

        if (reader.Settings is not null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;

        instance = DeserializeElementInternal(targetType, reader, state);

        // Whatever is left after the root element has been read are the comments trailing the document.
        addSourceComments(instance, documentEnd: state.Comments?.Consume());

        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    private PocoDeserializerState createState() => new() { Comments = Settings.RetainComments ? new() : null };

    internal Resource? DeserializeResourceInternal(XmlReader reader, PocoDeserializerState state)
    {
        var resourceMapping = determineClassMappingFromInstance(reader, _inspector, state);
        if (!resourceMapping.IsResource) return null;

        var newResource = resourceMapping.CreateInstance();

        try
        {
            state.EnterResource(newResource.TypeName);
            int nErrorCount = state.Errors.Count;
            
            DeserializeElementInto(newResource, resourceMapping, reader, state);

            if (Settings.AnnotateResourceParseExceptions && state.Errors.Count > nErrorCount)
            {
                List<CodedException> resourceErrs = state.Errors.Skip(nErrorCount).ToList();
                ((Resource)newResource).SetAnnotation(resourceErrs);
            }
            return (Resource)newResource;
        }
        finally
        {
            state.ExitResource();
        }
    }

    private static void verifyOpeningElement(XmlReader reader)
    {
        if (reader.NodeType != XmlNodeType.Element)
            throw new InvalidOperationException($"Xml node of type '{reader.NodeType}' is unexpected at this point.");
    }

    private static void validateNameSpace(XmlReader reader, PocoDeserializerState state)
    {
        if (string.IsNullOrEmpty(reader.NamespaceURI))
        {
            state.Errors.Add(ERR.EMPTY_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath()));
        }
        else if (reader.NamespaceURI != XmlNs.FHIR)
        {
            state.Errors.Add(ERR.INCORRECT_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath(), reader.NamespaceURI));
        }
    }

    internal Base DeserializeElementInternal(Type targetType, XmlReader reader, PocoDeserializerState state)
    {
        var mapping = _inspector.FindOrImportClassMapping(targetType) ??
                      throw new ArgumentException($"Type '{targetType}' does not have the required FHIR metadata " +
                                                  $"and therefore not be used for deserialization. ", nameof(targetType));

        // If we have at least a mapping, let's try to continue
        var newDatatype = mapping.CreateInstance();
        DeserializeElementInto(newDatatype, mapping, reader, state);
        return newDatatype;
    }

    /// <summary>
    /// Reads a complex element from a reader.
    /// </summary>
    /// <remarks>Reader should be at the open tag of the complex element.
    /// When done, the reader will be at the next token after this element or end of the file.</remarks>
    internal void DeserializeElementInto(Base target, ClassMapping mapping, XmlReader reader, PocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();
        var hasValueAttribute = reader.GetAttribute("value") != null;

        // The comments collected since the previous element was closed are the ones preceding this element.
        var commentsBefore = state.Comments?.Consume();

        if (Settings.AnnotateLineInfo)
            target.AddAnnotation(new XmlSerializationDetails { LineNumber = lineNumber, LinePosition = position });

        //check if on opening tag
        verifyOpeningElement(reader);

        validateNameSpace(reader, state);

        readAttributes(target, mapping, reader, state);

        string[]? closingComments = null;

        //Empty elements have no children e.g. <foo value="bar/>)
        if (!reader.IsEmptyElement)
        {
            //read the next element child.
            reader.ReadToContent(state);

            PropertyMapping? highestOrder = null;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var (propMapping, propValueMapping) = getMappingForElement(mapping, reader.LocalName, state, reader);

                if(propMapping.SerializationHint is not (XmlRepresentation.None or XmlRepresentation.XmlElement or  XmlRepresentation.XHtml))
                    state.Errors.Add(ERR.ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE(reader, state.Path.GetInstancePath(), reader.LocalName));

                if(!propMapping.RepresentsValueElement)
                    state.EnterElement(propMapping.Name);

                if (propMapping.IsCollection) state.SetIndex(0);

                highestOrder = checkOrder(reader, state, highestOrder, propMapping);

                try
                {
                    deserializeChildElement(target, reader, state, propMapping, propValueMapping);
                }
                finally
                {
                    if (!propMapping.RepresentsValueElement)
                        state.ExitElement();
                }
            }

            // We are on the closing tag now, so anything collected after the last child closes this element.
            // This has to be taken here, before the read at the end of this method moves past the closing tag
            // and starts collecting the comments that precede our next sibling.
            closingComments = state.Comments?.Consume();
        }

        addSourceComments(target, commentsBefore, closingComments);

        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath,
                lineNumber, position,
                Settings.NarrativeValidation);

            state.Errors.Add(Settings.Validator.ValidateObject(target, mapping, context));
        }

        // Read to next element (or closing of parent)
        reader.ReadToContent(state);
    }

    private static PropertyMapping checkOrder(XmlReader reader, PocoDeserializerState state, PropertyMapping? highestOrder, PropertyMapping propMapping)
    {
        //check if element is in the correct order.
        if (highestOrder is null || propMapping.Order is null || highestOrder.Order is null ||
            propMapping.Order >= highestOrder.Order)
        {
            highestOrder = propMapping;
        }
        else
        {
            state.Errors.Add(ERR.ELEMENT_OUT_OF_ORDER(reader, state.Path.GetInstancePath(), propMapping.Name, highestOrder.Name));
        }

        return highestOrder;
    }

    private void deserializeChildElement(Base target, XmlReader reader, PocoDeserializerState state,
        PropertyMapping propMapping, ClassMapping propValueMapping)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        var elementName = reader.LocalName;

        var targetListMapping = _inspector.FindOrImportClassMapping(propMapping.ImplementingType)!;
        var targetList = targetListMapping.CreateList();
        var elementIndex = 0;

        // Read the element, and any of its direct neighbours into a list.
        while (reader.LocalName == elementName && reader.NodeType != XmlNodeType.EndElement)
        {
            if (propMapping.IsCollection)
            {
                state.SetIndex(elementIndex);
                elementIndex += 1;
            }

            var newEntry = deserializeSingleValue(propValueMapping, propMapping, reader, state);
            addToList(targetList, newEntry);
        }

        // If the element did not repeat, and is not a list, then it is a single item after all
        object newElement = targetList.Count == 1 && !propMapping.IsCollection
            ? targetList[0]!
            : targetList;

        var newPropValue = setPropertyWithRepeating(target, propMapping.Name, propValueMapping, newElement, state, reader);

        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath,
                lineNumber, position,
                Settings.NarrativeValidation)
            {
                MemberName = propMapping.NativeProperty?.Name
            };

            state.Errors.Add(Settings.Validator.ValidateProperty(elementName, newPropValue, propMapping, context));
        }
    }

    /// <summary>
    /// Set a property on the target object. If the property is already present, turn it into a collection.
    /// </summary>
    private static object setPropertyWithRepeating(Base target, string name, ClassMapping propValueMapping,
        object newValue, PocoDeserializerState state, XmlReader reader)
    {
        object result = newValue;

        if(target.TryGetValue(name, out var prop))
        {
            // The property was already set, this means we're finding an element that we saw before,
            // but not consecutively.
            state.Errors.Add(ERR.ELEMENT_NOT_IN_SEQUENCE(reader, state.Path.GetInstancePath(), name));

            // single into repeating, otherwise prop is already == result
            if (prop is not IList l)
            {
                l = propValueMapping.CreateList();
                l.Add(prop);
            }

            addToList(l, newValue);
            result = l;
        }

        target.SetValue(name, result);
        return result;
    }

    private static XHtml readXhtml(XmlReader reader, PocoDeserializerState state)
    {
        var xhtml = reader.ReadOuterXml();
        reader.MoveToContent(state);
        return new XHtml(xhtml);
    }

    /// <summary>
    /// Annotates the comments found around <paramref name="target"/> in the source data onto it, merging
    /// them into the annotation when one is already present.
    /// </summary>
    /// <remarks>Only used when <see cref="DeserializerSettings.RetainComments"/> is on - without it there
    /// are no comments to annotate, since none are collected.</remarks>
    private static void addSourceComments(Base? target, string[]? before = null, string[]? closing = null, string[]? documentEnd = null)
    {
        if (target is null || (before is null && closing is null && documentEnd is null)) return;

        var comments = target.Annotation<SourceComments>();

        if (comments is null)
        {
            comments = new SourceComments();
            target.AddAnnotation(comments);
        }

        if (before is not null) comments.CommentsBefore = combine(comments.CommentsBefore, before);
        if (closing is not null) comments.ClosingComments = combine(comments.ClosingComments, closing);
        if (documentEnd is not null) comments.DocumentEndComments = combine(comments.DocumentEndComments, documentEnd);

        static string[] combine(string[]? existing, string[] added) =>
            existing is not { Length: > 0 } ? added : [.. existing, .. added];
    }

    private static void addToList(IList target, object oneOrMoreThings)
    {
        if(oneOrMoreThings is Base)
            target.Add(oneOrMoreThings);
        else if(oneOrMoreThings is IEnumerable<Base> blist)
            foreach(var thing in blist) target.Add(thing);
        else throw new InvalidOperationException($"Cannot add something of type {oneOrMoreThings.GetType()}.");
    }

    private IReadOnlyCollection<Base> deserializeSingleValue(ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, PocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();
        
        if (propMapping.Choice == ChoiceType.ResourceChoice)
        {
            validateNameSpace(reader, state);
            return deserializeResourceContainer(reader, state);
        }

        if (propMapping.SerializationHint == XmlRepresentation.XHtml)
        {
            if (reader.NamespaceURI != XmlNs.XHTML)
            {
                state.Errors.Add(ERR.INCORRECT_XHTML_NAMESPACE(reader, state.Path.GetInstancePath()));
            }

            // Take the comments preceding the xhtml before reading on: readXhtml() moves past the div,
            // and the comments it collects while doing so belong to whatever comes after it.
            var commentsBefore = state.Comments?.Consume();

            var xhtml = readXhtml(reader, state);
            if (Settings.AnnotateLineInfo)
                xhtml.AddAnnotation(new XmlSerializationDetails { LineNumber = lineNumber, LinePosition = position });

            addSourceComments(xhtml, commentsBefore);

            return [xhtml];
        }

        var newDatatype = propValueMapping.CreateInstance();
        DeserializeElementInto(newDatatype, propValueMapping, reader, state);
        return [newDatatype];
    }

    private IReadOnlyCollection<Resource> deserializeResourceContainer(XmlReader reader, PocoDeserializerState state)
    {
        // we are currently at the resource container (e.g. <contained>)
        if (reader.HasAttributes)
        {
            reader.MoveToFirstAttribute();
            state.Errors.Add(ERR.NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath(), reader.LocalName));
            reader.MoveToElement();
        }

        List<Resource> result = [];

        if(!reader.IsEmptyElement)
        {
            // let's move to the actual resource
            reader.ReadToContent(state);

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var containedResource = DeserializeResourceInternal(reader, state);
                if(containedResource is not null) result.Add(containedResource);
            }

            // Comments between the resource and the closing tag of its container would otherwise leak into
            // the element following the container, so treat them as closing the resource itself.
            if (state.Comments?.Consume() is { } trailing && result.Count > 0)
                addSourceComments(result[^1], closing: trailing);
        }

        switch (result.Count)
        {
            case 0:
                state.Errors.Add(ERR.EMPTY_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath()));
                break;
            case > 1:
                state.Errors.Add(ERR.MULTIPLE_ELEMENTS_IN_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath()));
                break;
        }

        //we move out of the container to the next element.
        reader.ReadToContent(state);
        return result;
    }

    private void readAttributes(Base target, ClassMapping parentMapping, XmlReader reader, PocoDeserializerState state)
    {
        if(!reader.MoveToFirstAttribute())
            return;

        try
        {
            do
            {
                if (reader.LocalName == "xmlns" || reader.Prefix == "xmlns")
                {
                    //Do nothing: checked before
                }
                else if (reader is { LocalName: "schemaLocation", NamespaceURI: "http://www.w3.org/2001/XMLSchema-instance" })
                {
                    if(Settings.DisallowXsiAttributesOnRoot)
                        state.Errors.Add(ERR.SCHEMALOCATION_DISALLOWED(reader, state.Path.GetInstancePath()));
                }
                else
                {
                    var propMapping = parentMapping.FindMappedElementByName(reader.LocalName) ??
                                      new PropertyMapping(parentMapping, reader.LocalName, typeof(FhirString)) { SerializationHint = XmlRepresentation.XmlAttr };

                    if(!propMapping.RepresentsValueElement)
                        state.EnterElement(reader.LocalName);
                    if(propMapping.IsCollection)
                        state.SetIndex(0);

                    readAttribute(target, propMapping, reader.LocalName, reader, state);

                    if(!propMapping.RepresentsValueElement)
                        state.ExitElement();
                }
            } while (reader.MoveToNextAttribute());
        }
        finally
        {
            //move reader back to element so it can continue later
            reader.MoveToElement();
        }
    }

    ///Parse current attribute value to set the value property of the target.
    private void readAttribute(Base target, PropertyMapping propMapping, string attributeName, XmlReader reader, PocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        if (!string.IsNullOrEmpty(reader.NamespaceURI) && reader.NamespaceURI != XmlNs.FHIR)
            state.Errors.Add(ERR.INCORRECT_ATTRIBUTE_NAMESPACE(reader, state.Path.GetInstancePath(), reader.NamespaceURI));

        // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading and
        // trailing whitespace when reading attribute values (for XML schema conformance)"
        var originalValue = reader.Value;
        string trimmedValue = originalValue.Trim();
        if (originalValue != trimmedValue)
        {
            state.Errors.Add(ERR.STRING_SHOULD_NOT_HAVE_LEADING_TRAILING_WHITESPACE(reader, state.Path.GetInstancePath(), attributeName));
        }

        var parsedValue = parsePrimitiveValue(trimmedValue, propMapping.ImplementingType);

        if (target is PrimitiveType primitive && attributeName == "value")
        {
            primitive.JsonValue = parsedValue;

            // Validator should not be called on the primitive values, this will
            // be handled by the Primitive's ValidateInstance.
        }
        else
        {
            // We're in a situation where the target is not a primitive (often: Extension or Element),
            // and we encounter an attribute representing an element on that complex (often: url, id).
            // If this is a primitive, or not "url" or "id", the element will end up in the overflow.
            // Note, you can set "Patient.active" this way using <Patient active=true>, we might want
            // to prevent that.

            if (propMapping.SerializationHint != XmlRepresentation.XmlAttr)
                state.Errors.Add(ERR.ATTRIBUTE_SHOULD_HAVE_BEEN_AN_ELEMENT(reader, state.Path.GetInstancePath(), reader.LocalName));

            var targetElementMapping =
                _inspector.FindOrImportClassMapping(propMapping.GetInstantiableType())!;
            var targetElement = (PrimitiveType)targetElementMapping.CreateInstance();

            // If this is an unknown property, we have to keep track of the fact that it was serialized
            // as an attribute.
            if(propMapping.NativeProperty is null)
                targetElement.AddAnnotation(new XmlRepresentationAnnotation(XmlRepresentation.XmlAttr));

            if (Settings.AnnotateLineInfo)
                targetElement.AddAnnotation(new XmlSerializationDetails { LineNumber = lineNumber, LinePosition = position });

            targetElement.JsonValue = parsedValue;

            // Handle atomic-types-as-primitives, Element.id, Extension.url etc.
            var newPropValue = setPropertyWithRepeating(target, attributeName, targetElementMapping, targetElement, state, reader);

            if (Settings.Validator is not null)
            {
                var context = new PocoValidationContext(
                    targetElement,
                    _inspector,
                    state.Path.GetInstancePath,
                    lineNumber, position,
                    Settings.NarrativeValidation);
                state.Errors.Add(Settings.Validator.ValidateObject(targetElement, targetElementMapping, context));

                context = new PocoValidationContext(
                    target,
                    _inspector,
                    state.Path.GetInstancePath,
                    lineNumber, position,
                    Settings.NarrativeValidation)
                {
                    MemberName = propMapping.NativeProperty?.Name
                };
                state.Errors.Add(Settings.Validator.ValidateProperty(attributeName, newPropValue, propMapping, context));
            }
        }
    }

    private static object parsePrimitiveValue(string trimmedValue, Type implementingType)
    {
        // bool, int and decimal are the only three types that are used in ObjectValue (and the json serialization)
        if (implementingType == typeof(bool))
        {
            return ElementModel.Types.Boolean.TryParse(trimmedValue, out var parsed)
                ? parsed.Value : trimmedValue;
        }
        if (implementingType == typeof(int))
        {
            return ElementModel.Types.Integer.TryParse(trimmedValue, out var parsed)
                ? parsed.Value : trimmedValue;
        }
        if (implementingType == typeof(decimal))
        {
            return ElementModel.Types.Decimal.TryParse(trimmedValue, out var parsed)
                ? parsed.Value : trimmedValue;
        }

        // Keep it unparsed, as a string.
        return trimmedValue;
    }

    /// <summary>
    /// Returns the <see cref="ClassMapping" /> for the object to be deserialized using the root property.
    /// </summary>
    /// <remarks>Assumes the reader is on the start of an object.</remarks>
    private ClassMapping determineClassMappingFromInstance(XmlReader reader, ModelInspector inspector, PocoDeserializerState state)
    {
        var resourceType = reader.LocalName;

        var resourceMapping = inspector.FindClassMapping(resourceType);
        if (resourceMapping is null or { IsResource: false })
            return new ClassMapping(inspector, resourceType, typeof(DynamicResource));

        // The inspector finds resource types case-insensitively, so the data can still be parsed
        // into the correct POCO, but a wrong-cased resource type name violates the spec and is
        // reported. This is a model-level validation, so it is skipped when the validator is off.
        if (Settings.Validator is not null && !string.Equals(resourceMapping.Name, resourceType, StringComparison.Ordinal))
        {
            var (line, pos) = reader.GenerateLineInfo();
            state.Errors.Add(COVE.WRONG_CASED_RESOURCE_TYPE(state.Path.GetInstancePath(), line, pos, resourceType, resourceMapping.Name));
        }

        return resourceMapping;
    }

    /// <summary>
    /// Given a possibly suffixed property name (as encountered in the serialized form), lookup the
    /// mapping for the property and the mapping for the value of the property.
    /// </summary>
    /// <remarks>In case the name is a choice type, the type suffix will be used to determine the returned
    /// <see cref="ClassMapping"/>, otherwise the <see cref="PropertyMapping.ImplementingType"/> is used.
    /// </remarks>
    private PropertyValueMapping getMappingForElement(
        ClassMapping parentMapping,
        string elementName,
        PocoDeserializerState state,
        XmlReader reader)
    {
        PropertyMapping? definedMapping = null;

        if (parentMapping.TryFindElement(elementName) is { } lookup)
        {
            // The lookup also finds names that differ from a defined element name only by casing.
            // Whether such a wrong-cased name is still bound to the element it nearly matches is
            // decided by DeserializerSettings.UsesStrictCaseBinding (see there for the rationale):
            // - lenient: bind, so the data ends up in the typed property where it is most useful;
            // - strict: only bind exactly-cased names. A wrong-cased name falls through to
            //   getUnknownPropMapping() below, so the data is preserved under its original name in
            //   the overflow, and the validator reports it (WRONG_CASED_ELEMENT + UNKNOWN_ELEMENT).
            if (lookup.IsExactCase || !usesStrictCaseBinding())
                definedMapping = lookup.Mapping;
        }

        var propertyMapping = definedMapping ?? getUnknownPropMapping();

        ClassMapping propertyValueMapping = propertyMapping.Choice switch
        {
            // A custom type mapping is used directly: resolving it via the .NET type (below) would
            // lose the identity of the custom type, since custom types share the same dynamic .NET type.
            ChoiceType.None or ChoiceType.ResourceChoice when propertyMapping.PropertyTypeMapping is { IsCustomMapping: true } customType =>
                customType,
            ChoiceType.None or ChoiceType.ResourceChoice =>
                parentMapping.Inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ??
                throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which" +
                                                    $" no mapping was found in the model assemblies."),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported.")
        };

        return new PropertyValueMapping(propertyMapping, propertyValueMapping);

        ClassMapping getChoiceClassMapping()
        {
            var typeSuffix = elementName.AsSpan(propertyMapping.Name.Length);

            if (!typeSuffix.IsEmpty)
            {
                // Span-based lookup avoids allocating the suffix substring for the common case
                // where the suffix resolves to a known type.
                var foundChoiceMapping = parentMapping.Inspector.FindClassMapping(typeSuffix)
                                         ?? new ClassMapping(_inspector, typeSuffix.ToString(), getDynamicTypeMapping());

                return foundChoiceMapping;
            }

            var path = state.Path.GetInstancePath();
            state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(reader, path, elementName));

            return new ClassMapping(_inspector, $"UnknownType_{path}", getDynamicTypeMapping());
        }
        
        Type getDynamicTypeMapping() =>
            reader.GetAttribute("value") != null
                ? typeof(DynamicPrimitive)
                : typeof(DynamicDataType);

        PropertyMapping getUnknownPropMapping() => new (parentMapping, elementName, getDynamicTypeMapping());
    }
}