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

        if (reader.Settings?.DtdProcessing == DtdProcessing.Parse)
        {
            state.Errors.Add(ERR.ENCOUNTERED_DTD_REFERENCES(reader, state.Path.GetInstancePath()));
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;
        }

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

        if (reader.Settings?.DtdProcessing == DtdProcessing.Parse)
        {
            state.Errors.Add(ERR.ENCOUNTERED_DTD_REFERENCES(reader, state.Path.GetInstancePath()));
            reader.Settings.DtdProcessing = DtdProcessing.Prohibit;
        }

        instance = DeserializeElementInternal(targetType, reader, state);
        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    internal Resource? DeserializeResourceInternal(XmlReader reader, PocoDeserializerState state)
    {
        //check if we are actually on an opening element.
        verifyOpeningElement(reader, state);

        var resourceMapping = determineClassMappingFromInstance(reader, _inspector, state);

        validateNameSpace(reader, state, null);

        // If we have at least a mapping, let's try to continue
        var newResource = (Base)resourceMapping.Factory();

        // if we're on dynamic, the type is not recognized, so we should set dynamic to report it
        if(newResource is DynamicResource dr)
        {
            dr.DynamicTypeName = reader.LocalName;
        }

        try
        {
            state.Path.EnterResource(resourceMapping.Name);
            int nErrorCount = state.Errors.Count;
            DeserializeElementInto(newResource, resourceMapping, reader, state);

            if (!resourceMapping.IsResource)
            {
                state.Errors.Add(ERR.RESOURCE_TYPE_NOT_A_RESOURCE(reader, state.Path.GetInstancePath(), resourceMapping.Name));
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

    private static void verifyOpeningElement(XmlReader reader, PocoDeserializerState state)
    {
        //If not skip all non-content and check again.
        reader.MoveToContent();
        if (reader.NodeType != XmlNodeType.Element)
        {
            //if we are still not at an opening element, throw user-error.
            state.Errors.Add(ERR.EXPECTED_OPENING_ELEMENT(reader, state.Path.GetInstancePath(), reader.NodeType.GetLiteral()));
            //try to recover
            while (reader.NodeType != XmlNodeType.Element && !reader.EOF)
            {
                reader.ReadToContent(state);
            }
        }
    }

    private static void validateNameSpace(XmlReader reader, PocoDeserializerState state, PropertyMapping? propMapping)
    {
        if (string.IsNullOrEmpty(reader.NamespaceURI))
        {
            state.Errors.Add(ERR.EMPTY_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName));
        }
        else if (propMapping?.SerializationHint == XmlRepresentation.XHtml)
        {
            if (reader.NamespaceURI != XmlNs.XHTML)
            {
                state.Errors.Add(ERR.INCORRECT_XHTML_NAMESPACE(reader, state.Path.GetInstancePath()));
            }
        }
        else if (reader.NamespaceURI != XmlNs.FHIR)
        {
            state.Errors.Add(ERR.INCORRECT_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName, reader.NamespaceURI));
        }
    }

    internal Base DeserializeElementInternal(Type targetType, XmlReader reader, PocoDeserializerState state)
    {
        var mapping = _inspector.FindOrImportClassMapping(targetType) ??
                      throw new ArgumentException($"Type '{targetType}' could not be located and can " +
                                                  $"therefore not be used for deserialization. " + reader.GenerateLocationMessage(), nameof(targetType));

        //check if we are at an opening element.
        verifyOpeningElement(reader, state);

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
        var name = reader.LocalName;
        bool hasChildElements = false;

        //check if on opening tag
        if (reader.NodeType != XmlNodeType.Element)
            throw new InvalidOperationException($"Xml node of type '{reader.NodeType}' is unexpected at this point");

        readAttributes(target, mapping, reader, state);

        //Empty elements have no children e.g. <foo value="bar/>)
        if (!reader.IsEmptyElement)
        {
            //read the next object that has content
            reader.ReadToContent(state);

            int highestOrder = 0;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                hasChildElements = true;

                var (propMapping, propValueMapping) = getMappingsForElement(_inspector, mapping, reader.LocalName, state, reader);

                validateNameSpace(reader, state, propMapping);

                if (propMapping is null)
                {
                    // we don't know this property: Try to parse anyway and throw it into dynamic and overflow
                    deserializeUnknownPropertyValue(target, reader, state);
                    continue;
                }

                if(!(propMapping.SerializationHint is XmlRepresentation.None or XmlRepresentation.XmlElement or  XmlRepresentation.XHtml))
                    state.Errors.Add(ERR.ELEMENT_SHOULD_HAVE_BEEN_AN_ATTRIBUTE(reader, state.Path.GetInstancePath(), reader.LocalName));

                state.Path.EnterElement(propMapping.Name, !propMapping.IsCollection ? null : 0, propMapping.IsPrimitive);
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
            var locationMessage = XmlReaderExtensions.GenerateLocationMessage(lineNumber, position);
            state.Errors.Add(ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN(state.Path.GetInstancePath(), lineNumber, position, locationMessage, name));
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

        reader.ReadToContent(state);
    }

    private void deserializeUnknownPropertyValue(Base target, XmlReader reader, PocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();
        var elementName = reader.LocalName;

        var mapping = ClassMapping.DynamicDataType;
        var propertyValue = (Base)mapping.Factory();

        // primitive with value in content - not allowed in FHIR, but let's handle it.
        if (reader.NodeType == XmlNodeType.Text && string.IsNullOrEmpty(elementName))
        {
            state.Errors.Add(ERR.INVALID_TEXT_NODE(reader, state.Path.GetInstancePath()));
            elementName = "nodeText";
            propertyValue = new FhirString(reader.ReadString().Trim());
        }
        else
        {
            DeserializeElementInto(propertyValue, mapping, reader, state);

            // if we have primitive rather than datatype, convert it to primitive
            if (propertyValue.TryGetValue("value", out _))
            {
                propertyValue = propertyValue.ToDynamicPrimitive();
            }
        }

        setPropertyWithRepeating(target, elementName, mapping, propertyValue);

        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath, // should this path GetPath or this?
                lineNumber, position,
                Settings.NarrativeValidation);

            state.Errors.Add(Settings.Validator.ValidateProperty(elementName, propertyValue, null, context));
        }
    }

    private static int checkOrder(XmlReader reader, PocoDeserializerState state, int highestOrder, PropertyMapping propMapping)
    {
        //check if element is in the correct order.
        if (propMapping.Order >= highestOrder)
        {
            highestOrder = propMapping.Order;
        }
        else
        {
            state.Errors.Add(ERR.ELEMENT_OUT_OF_ORDER(reader, state.Path.GetInstancePath(), reader.LocalName));
        }

        return highestOrder;
    }

    private void deserializeChildElement(Base target, XmlReader reader, PocoDeserializerState state, PropertyMapping propMapping, ClassMapping? propValueMapping)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();

        object? result = propMapping.IsCollection
            ? addToList(target, propValueMapping!, propMapping, reader, state)
            : readSingleValue(propValueMapping!, propMapping, reader, state);

        if(result is not null)
            setPropertyWithRepeating(target, propMapping.Name, propValueMapping!, result);

        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath, // should this path GetPath or this?
                lineNumber, position,
                Settings.NarrativeValidation);

            state.Errors.Add(Settings.Validator.ValidateProperty(reader.LocalName, result, propMapping, context));
        }
    }

    /// <summary>
    /// Set a property on the target object. If the property is already present, turn it into a collection.
    /// </summary>
    private static void setPropertyWithRepeating(Base target, string name, ClassMapping propValueMapping, object? result)
    {
        if(target.TryGetValue(name, out var prop))
        {
            // single into repeating, otherwise prop is already == result
            if (prop is not IList)
            {
                var list = propValueMapping.ListFactory();

                list.Add(prop);
                list.Add(result);

                result = list;
            }
        }
        target.SetValue(name, result);
    }

    private static XHtml readXhtml(XmlReader reader)
    {
        var xhtml = reader.ReadOuterXml();
        reader.MoveToContent();
        return new XHtml(xhtml);
    }

    //Will create a new list, or adds encountered values to an already existing list (and reports a user error).
    private IList addToList(Base target, ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, PocoDeserializerState state)
    {
        // We know our POCOs will generate the correct, non-null list.
        var currentList = (IList)propMapping.GetValue(target)!;

        readIntoList(currentList, propValueMapping, propMapping, reader, state);
        return currentList;
    }

    //When done, the reader will be at the next token after the last element of the list or end of the file.
    private void readIntoList(IList targetList, ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, PocoDeserializerState state)
    {
        //There was already a list created previously -> User error!
        if (targetList.Count > 0)
        {
            state.Path.IncrementIndex(targetList.Count);
            state.Errors.Add(ERR.ELEMENT_NOT_IN_SEQUENCE(reader, state.Path.GetInstancePath(), reader.LocalName));
        }

        var name = reader.LocalName;

        while (reader.LocalName == name && reader.NodeType != XmlNodeType.EndElement)
        {
            var newEntry = readSingleValue(propValueMapping, propMapping, reader, state);

            if (newEntry is not null)
            {
                targetList.Add(newEntry);
            }

            state.Path.IncrementIndex();
        }
    }

    private object? readSingleValue(ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, PocoDeserializerState state)
    {
        if (propMapping.Choice == ChoiceType.ResourceChoice)
        {
            return deserializeResourceContainer(reader, state);
        }

        if (propMapping.SerializationHint == XmlRepresentation.XHtml)
        {
            return readXhtml(reader);
        }

        var newDatatype = (Base)propValueMapping.Factory();
        DeserializeElementInto(newDatatype, propValueMapping, reader, state);
        return newDatatype;
    }

    private object? deserializeResourceContainer(XmlReader reader, PocoDeserializerState state)
    {
        var depth = reader.Depth;

        // we are currently at the resource container (e.g. <contained>)
        if (reader.HasAttributes)
            state.Errors.Add(ERR.NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath(), reader.LocalName));

        // let's move to the actual resource
        if(!reader.IsEmptyElement) reader.ReadToContent(state);
        object? result;

        if (reader.IsEmptyElement || reader.NodeType == XmlNodeType.EndElement)
        {
            state.Errors.Add(ERR.EMPTY_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath()));
            result = null;
        }
        else
        {
            result = DeserializeResourceInternal(reader, state);
            // now we should be at the closing element of the resource container (e.g. </contained>). We should check that and maybe fix that.)
            if (reader.Depth != depth && reader.NodeType != XmlNodeType.EndElement)
            {
                state.Errors.Add(ERR.DISALLOWED_ELEMENT_IN_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath(),
                    reader.LocalName));

                // skip until we're back at the closing of the </contained>
                while (!(reader.Depth == depth && reader.NodeType == XmlNodeType.EndElement))
                {
                    reader.Read();
                }
            }
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

                    state.Path.EnterElement(reader.LocalName, propMapping?.IsCollection == false ? null : 0, propMapping?.IsPrimitive ?? true);
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
            state.Errors.Add(ERR.INCORRECT_ATTRIBUTE_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName, attributeName, reader.NamespaceURI));

        // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading and
        // trailing whitespace when reading attribute values (for XML schema conformance)"
        string trimmedValue = reader.Value.Trim();

        var parsedValue = parsePrimitiveValue(trimmedValue, propMapping?.ImplementingType ?? typeof(string));

        if (target is PrimitiveType primitive && attributeName == "value")
        {
            primitive.ObjectValue = parsedValue;

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

            var targetElementMapping = propMapping?.PropertyTypeMapping is { IsFhirPrimitive: true } ptm
                ? ptm : _inspector.FindClassMapping(typeof(FhirString))!;
            var targetElement = (PrimitiveType)targetElementMapping.Factory();

            // If this is an unknown property, we have to keep track of the fact that it was serialized
            // as an attribute.
            if(propMapping is null)
                targetElement.AddAnnotation(new XmlRepresentationAnnotation(XmlRepresentation.XmlAttr));

            targetElement.ObjectValue = parsedValue;

            // Handle atomic-types-as-primitives, Element.id, Extension.url etc.
            setPropertyWithRepeating(target, attributeName, targetElementMapping, targetElement);

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
                state.Errors.Add(Settings.Validator.ValidateProperty(attributeName, parsedValue, propMapping, context));
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
        var resourceMapping = inspector.FindClassMapping(reader.LocalName);

        if (resourceMapping is null)
            state.Errors.Add(ERR.UNKNOWN_RESOURCE_TYPE(reader, state.Path.GetInstancePath(), reader.LocalName));

        return resourceMapping ?? ClassMapping.DynamicResource;
    }

    /// <summary>
    /// Given a possibly suffixed property name (as encountered in the serialized form), lookup the
    /// mapping for the property and the mapping for the value of the property.
    /// </summary>
    /// <remarks>In case the name is a choice type, the type suffix will be used to determine the returned
    /// <see cref="ClassMapping"/>, otherwise the <see cref="PropertyMapping.ImplementingType"/> is used.
    /// </remarks>
    private static (PropertyMapping? propMapping, ClassMapping propValueMapping) getMappingsForElement(
        ModelInspector inspector,
        ClassMapping parentMapping,
        string elementName,
        PocoDeserializerState state,
        XmlReader reader)
    {
        var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                              ?? parentMapping.FindMappedElementByChoiceName(elementName);

        if (propertyMapping is null)
            return (null, null ?? ClassMapping.DynamicDataType);

        ClassMapping propertyValueMapping = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ?? throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which no mapping was found in the model assemblies."),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported")
        };

        return (propertyMapping, propertyValueMapping);

        ClassMapping getChoiceClassMapping()
        {
            ClassMapping? choiceMapping = null;
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            if (!string.IsNullOrEmpty(typeSuffix))
            {
                choiceMapping = inspector.FindClassMapping(typeSuffix);
                if (choiceMapping is null)
                {
                    state.Errors.Add(ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE(reader, state.Path.GetInstancePath(), propertyMapping.Name, typeSuffix));
                    choiceMapping = ClassMapping.DynamicDataType;
                }
            }
            else
            {
                state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(reader, state.Path.GetInstancePath()));
                choiceMapping = ClassMapping.DynamicDataType;
            }

            return choiceMapping;
        }
    }
}