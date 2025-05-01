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
        PocoDeserializerState state = new();

        // If the stream has just been opened, move to the first token. (skip processing instructions, comments, whitespaces etc.)
        reader.MoveToContent();

        if (reader.Settings is not null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;

        instance = DeserializeResourceInternal(reader, state);
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
        PocoDeserializerState state = new();

        // If the stream has just been opened, move to the first token. (skip processing instructions, comments, whitespaces etc.)
        reader.MoveToContent();

        if (reader.Settings is not null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;

        instance = DeserializeElementInternal(targetType, reader, state);
        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    internal Resource? DeserializeResourceInternal(XmlReader reader, PocoDeserializerState state)
    {
        var resourceMapping = determineClassMappingFromInstance(reader, _inspector, state);

        // If we have at least a mapping, let's try to continue
        var newResource = resourceMapping.CreateInstance();

        try
        {
            state.Path.EnterResource(newResource.TypeName);
            int nErrorCount = state.Errors.Count;
            DeserializeElementInto(newResource, resourceMapping.Original, reader, state);

            if (!resourceMapping.Original.IsResource)
            {
                state.Errors.Add(ERR.RESOURCE_TYPE_NOT_A_RESOURCE(reader, state.Path.GetInstancePath(), resourceMapping.Original.Name));
                return null;
            }
            else
            {
                if (Settings.AnnotateResourceParseExceptions && state.Errors.Count > nErrorCount)
                {
                    List<CodedException> resourceErrs = state.Errors.Skip(nErrorCount).ToList();
                    ((Resource)newResource).SetAnnotation(resourceErrs);
                }
                return (Resource)newResource;
            }
        }
        finally
        {
            state.Path.ExitResource();
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
        var newDatatype = (Base)mapping.Factory();
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
        bool hasChildElements = false;

        //check if on opening tag
        verifyOpeningElement(reader);

        validateNameSpace(reader, state);

        readAttributes(target, mapping, reader, state);

        //Empty elements have no children e.g. <foo value="bar/>)
        if (!reader.IsEmptyElement)
        {
            //read the next element child.
            reader.ReadToContent(state);

            PropertyMapping? highestOrder = null;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                hasChildElements = true;

                var (propMapping, propValueMapping) = getMappingsForElement(_inspector, mapping, reader.LocalName, state, reader);

                if(propMapping is not null && propMapping.SerializationHint is not (XmlRepresentation.None or XmlRepresentation.XmlElement or  XmlRepresentation.XHtml))
                    state.Errors.Add(ERR.ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE(reader, state.Path.GetInstancePath(), reader.LocalName));

                state.Path.EnterElement(propMapping?.Name ?? reader.LocalName,
                    propMapping?.IsCollection == true ? 0 : null, propValueMapping.Original.IsFhirPrimitive);
                highestOrder = checkOrder(reader, state, highestOrder, propMapping);

                try
                {
                    deserializeChildElement(target, reader, state, propMapping, propValueMapping);
                }
                finally
                {
                    state.Path.ExitElement();
                }
            }
        }

        if (!hasValueAttribute && !hasChildElements)
        {
            //previous element didn't have a value and the current value is not a child of the previous element.
            //error is thrown with the location and the name of the previous element.
            state.Errors.Add(ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN(reader, state.Path.GetInstancePath(), reader.LocalName));
        }

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

    private static PropertyMapping? checkOrder(XmlReader reader, PocoDeserializerState state, PropertyMapping? highestOrder, PropertyMapping? propMapping)
    {
        if (propMapping is null) return highestOrder;

        //check if element is in the correct order.
        if (highestOrder is null || propMapping.Order >= highestOrder.Order)
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
        PropertyMapping? propMapping, ClassMappingDynamic propValueMapping)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        var elementName = reader.LocalName;

        var listFactory = propMapping is not null
            ? _inspector.FindOrImportClassMapping(propMapping.ImplementingType)!
            : propValueMapping.Original;
        var targetList = listFactory.ListFactory();

        // Read the element, and any of its direct neighbours into a list.
        while (reader.LocalName == elementName && reader.NodeType != XmlNodeType.EndElement)
        {
            var newEntry = readSingleValue(propValueMapping, propMapping, reader, state);
            addToList(targetList, newEntry);

            if(propMapping?.IsCollection != false)
                state.Path.IncrementIndex();
        }

        // If the element did not repeat, and is not a list, then it is a single item after all
        object newElement = targetList.Count == 1 && propMapping?.IsCollection != true
            ? targetList[0]!
            : targetList;

        var propName = propMapping?.Name ?? elementName;
        var newPropValue = setPropertyWithRepeating(target, propName, propValueMapping.Original, newElement, state, reader);

        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath, // should this path GetPath or this?
                lineNumber, position,
                Settings.NarrativeValidation);

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
                l = propValueMapping.ListFactory();
                l.Add(prop);
            }

            addToList(l, newValue);
            result = l;
        }

        target.SetValue(name, result);
        return result;
    }

    private static XHtml readXhtml(XmlReader reader)
    {
        var xhtml = reader.ReadOuterXml();
        reader.MoveToContent();
        return new XHtml(xhtml);
    }

    private static void addToList(IList target, object oneOrMoreThings)
    {
        if(oneOrMoreThings is Base)
            target.Add(oneOrMoreThings);
        else if(oneOrMoreThings is IEnumerable<Base> blist)
            foreach(var thing in blist) target.Add(thing);
        else throw new InvalidOperationException($"Cannot add something of type {oneOrMoreThings.GetType()}.");
    }

    private IReadOnlyCollection<Base> readSingleValue(ClassMappingDynamic propValueMapping, PropertyMapping? propMapping, XmlReader reader, PocoDeserializerState state)
    {
        if (propMapping?.Choice == ChoiceType.ResourceChoice)
        {
            validateNameSpace(reader, state);
            return deserializeResourceContainer(reader, state);
        }

        if (propMapping?.SerializationHint == XmlRepresentation.XHtml)
        {
            if (reader.NamespaceURI != XmlNs.XHTML)
            {
                state.Errors.Add(ERR.INCORRECT_XHTML_NAMESPACE(reader, state.Path.GetInstancePath()));
            }

            return [readXhtml(reader)];
        }

        var newDatatype = propValueMapping.CreateInstance();
        DeserializeElementInto(newDatatype, propValueMapping.Original, reader, state);
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

    private void readAttributes(Base target, ClassMapping propValueMapping, XmlReader reader, PocoDeserializerState state)
    {
        if (!reader.MoveToFirstAttribute()) return;

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
                    var propMapping = propValueMapping.FindMappedElementByName(reader.LocalName);

                    state.Path.EnterElement(reader.LocalName, propMapping?.IsCollection == true ? 0 : null, propMapping?.IsPrimitive ?? true);
                    try
                    {
                        readAttribute(target, propMapping, reader.LocalName, reader, state);
                    }
                    finally
                    {
                        state.Path.ExitElement();
                    }
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
    private void readAttribute(Base target, PropertyMapping? propMapping, string attributeName, XmlReader reader, PocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        if (!string.IsNullOrEmpty(reader.NamespaceURI) && reader.NamespaceURI != XmlNs.FHIR)
            state.Errors.Add(ERR.INCORRECT_ATTRIBUTE_NAMESPACE(reader, state.Path.GetInstancePath(), reader.NamespaceURI));

        // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading and
        // trailing whitespace when reading attribute values (for XML schema conformance)"
        string trimmedValue = reader.Value.Trim();

        var parsedValue = parsePrimitiveValue(trimmedValue, propMapping?.ImplementingType ?? typeof(string));

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

            if (propMapping is not null && propMapping.SerializationHint != XmlRepresentation.XmlAttr)
                state.Errors.Add(ERR.ATTRIBUTE_SHOULD_HAVE_BEEN_AN_ELEMENT(reader, state.Path.GetInstancePath(), reader.LocalName));

            var targetElementMapping =
                _inspector.FindOrImportClassMapping(propMapping?.GetInstantiableType() ?? typeof(FhirString))!;
            var targetElement = (PrimitiveType)targetElementMapping.Factory();

            // If this is an unknown property, we have to keep track of the fact that it was serialized
            // as an attribute.
            if(propMapping is null)
                targetElement.AddAnnotation(new XmlRepresentationAnnotation(XmlRepresentation.XmlAttr));

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
                    Settings.NarrativeValidation);
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
    private static ClassMappingDynamic determineClassMappingFromInstance(XmlReader reader, ModelInspector inspector, PocoDeserializerState state)
    {
        var resourceMapping = inspector.FindClassMapping(reader.LocalName);

        if (resourceMapping is null)
            state.Errors.Add(ERR.UNKNOWN_RESOURCE_TYPE(reader, state.Path.GetInstancePath(), reader.LocalName));

        return new ClassMappingDynamic(resourceMapping ?? ClassMapping.DynamicResource, reader.LocalName);
    }

    /// <summary>
    /// Given a possibly suffixed property name (as encountered in the serialized form), lookup the
    /// mapping for the property and the mapping for the value of the property.
    /// </summary>
    /// <remarks>In case the name is a choice type, the type suffix will be used to determine the returned
    /// <see cref="ClassMapping"/>, otherwise the <see cref="PropertyMapping.ImplementingType"/> is used.
    /// </remarks>
    private static (PropertyMapping? propMapping, ClassMappingDynamic propValueMapping) getMappingsForElement(
        ModelInspector inspector,
        ClassMapping parentMapping,
        string elementName,
        PocoDeserializerState state,
        XmlReader reader)
    {
        var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                              ?? parentMapping.FindMappedElementByChoiceName(elementName);

        if (propertyMapping is null)
        {
            return reader.GetAttribute("value") != null
                ? (null, new ClassMappingDynamic(ClassMapping.FhirString, null))
                : (null, new ClassMappingDynamic(ClassMapping.DynamicDataType, null));
        }

        ClassMappingDynamic propertyValueMapping = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                new ClassMappingDynamic(inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ?? throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which no mapping was found in the model assemblies."), null),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported")
        };

        return (propertyMapping, propertyValueMapping);

        ClassMappingDynamic getChoiceClassMapping()
        {
            ClassMappingDynamic choiceMapping;
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            if (!string.IsNullOrEmpty(typeSuffix))
            {
                var foundChoiceMapping = inspector.FindClassMapping(typeSuffix);
                if (foundChoiceMapping is null)
                {
                    state.Errors.Add(ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE(reader, state.Path.GetInstancePath(), propertyMapping.Name, typeSuffix));
                    choiceMapping = new ClassMappingDynamic(ClassMapping.DynamicDataType, typeSuffix);
                }
                else
                {
                    choiceMapping = new ClassMappingDynamic(foundChoiceMapping, null);
                }
            }
            else
            {
                state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(reader, state.Path.GetInstancePath()));
                choiceMapping = new ClassMappingDynamic(ClassMapping.DynamicDataType,null);
            }

            return choiceMapping;
        }
    }
}

internal record ClassMappingDynamic(ClassMapping Original, string? DynamicName)
{
    public Base CreateInstance()
    {
        var result = (Base)Original.Factory();
        if(result is IDynamicType dt) dt.DynamicTypeName = DynamicName;

        return result;
    }
}