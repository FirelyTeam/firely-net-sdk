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
        
        if (Settings.AnnotateLineInfo)
            target.AddAnnotation(new XmlSerializationDetails { LineNumber = lineNumber, LinePosition = position });

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
                var (propMapping, propValueMapping) = getMappingForElement(mapping, reader.LocalName, state, reader);

                if(propMapping.SerializationHint is not (XmlRepresentation.None or XmlRepresentation.XmlElement or  XmlRepresentation.XHtml))
                    state.Errors.Add(ERR.ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE(reader, state.Path.GetInstancePath(), reader.LocalName));

                if(!propMapping.RepresentsValueElement)
                    state.EnterElement(propMapping.Name);

                if (propMapping.IsCollection) state.SetIndex(0);

                highestOrder = checkOrder(reader, state, highestOrder, propMapping);

                deserializeChildElement(target, reader, state, propMapping, propValueMapping);
                if(!propMapping.RepresentsValueElement)
                    state.ExitElement();
            }
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
                { MemberName = propMapping.Name };

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

            var xhtml = readXhtml(reader);
            if (Settings.AnnotateLineInfo)
                xhtml.AddAnnotation(new XmlSerializationDetails { LineNumber = lineNumber, LinePosition = position });
            
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
    private static ClassMapping determineClassMappingFromInstance(XmlReader reader, ModelInspector inspector, PocoDeserializerState state)
    {
        var resourceType = reader.LocalName;

        return inspector.FindClassMapping(resourceType) switch
        {
            null or { IsResource: false } => new ClassMapping(inspector, resourceType, typeof(DynamicResource)),
            { } resourceMapping => resourceMapping,
        };
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
        var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                                    ?? parentMapping.FindMappedElementByChoiceName(elementName)
                                    ?? getUnknownPropMapping();

        ClassMapping propertyValueMapping = propertyMapping.Choice switch
        {
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
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            if (!string.IsNullOrEmpty(typeSuffix))
            {
                var foundChoiceMapping = parentMapping.Inspector.FindClassMapping(typeSuffix);

                if (foundChoiceMapping is null)
                {
                    foundChoiceMapping = new ClassMapping(_inspector, typeSuffix, getDynamicTypeMapping());
                }

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