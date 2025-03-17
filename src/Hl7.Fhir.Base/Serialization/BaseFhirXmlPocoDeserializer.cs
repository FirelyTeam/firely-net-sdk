#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;

namespace Hl7.Fhir.Serialization;

public class BaseFhirXmlPocoDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    public BaseFhirXmlPocoDeserializer(Assembly assembly) : this(assembly, new FhirXmlPocoDeserializerSettings())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirXmlPocoDeserializer(ModelInspector inspector) : this(inspector, new())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirXmlPocoDeserializer(Assembly assembly, FhirXmlPocoDeserializerSettings settings)
    {
        Settings = settings;
        _inspector = ModelInspector.ForAssembly(assembly ?? throw new ArgumentNullException(nameof(assembly)));
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirXmlPocoDeserializer(ModelInspector inspector, FhirXmlPocoDeserializerSettings settings)
    {
        Settings = settings;
        _inspector = inspector;
    }

    /// <summary>
    /// The settings that were passed to the constructor.
    /// </summary>
    public FhirXmlPocoDeserializerSettings Settings { get; }

    private readonly ModelInspector _inspector;

    /// <summary>
    /// Deserialize the FHIR xml from the reader and create a new POCO resource containing the data from the reader.
    /// </summary>
    /// <param name="reader">An xml reader positioned on the first element, or the beginning of the stream.</param>
    /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
    /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
    /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
    /// <remarks>The <see cref="FhirXmlPocoDeserializerSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeResource(XmlReader reader, [NotNullWhen(true)] out Resource? instance, out IEnumerable<CodedException> issues)
    {
        FhirXmlPocoDeserializerState state = new();

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
    /// <remarks>The <see cref="FhirXmlPocoDeserializerSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeElement(Type targetType, XmlReader reader, [NotNullWhen(true)] out Base? instance, out IEnumerable<CodedException> issues)
    {
        FhirXmlPocoDeserializerState state = new();

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

    internal Resource? DeserializeResourceInternal(XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        //check if we are actually on an opening element.
        verifyOpeningElement(reader, state);

        (ClassMapping? resourceMapping, FhirXmlException? error) = determineClassMappingFromInstance(reader, _inspector, state.Path);

        state.Errors.Add(error);

        if (resourceMapping is not null)
        {
            validateNameSpace(reader, state, null);

            // If we have at least a mapping, let's try to continue
            var newResource = (Base)resourceMapping.Factory();

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
        else
        {
            return null;
        }
    }

    private static void verifyOpeningElement(XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        //If not skip all non-content and check again.
        reader.MoveToContent();
        if (reader.NodeType != XmlNodeType.Element)
        {
            //if we are still not at an opening element, throw user-error.
            state.Errors.Add(ERR.EXPECTED_OPENING_ELEMENT(reader, state.Path.GetInstancePath(), reader.NodeType.GetLiteral()));
            //try to recover
            while (reader.NodeType != XmlNodeType.Element || !reader.EOF)
            {
                reader.ReadToContent(state);
            }
        }
    }

    private static bool validateNameSpace(XmlReader reader, FhirXmlPocoDeserializerState state, PropertyMapping? propMapping)
    {
        if (string.IsNullOrEmpty(reader.NamespaceURI))
        {
            state.Errors.Add(ERR.EMPTY_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName));
        }
        else if (propMapping?.SerializationHint == Specification.XmlRepresentation.XHtml)
        {
            if (reader.NamespaceURI != XmlNs.XHTML)
            {
                state.Errors.Add(ERR.INCORRECT_XHTML_NAMESPACE(reader, state.Path.GetInstancePath()));
            }
        }
        else if (reader.NamespaceURI != XmlNs.FHIR)
        {
            state.Errors.Add(ERR.INCORRECT_ELEMENT_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName, reader.NamespaceURI));
            return false; // the only case we want to NOT process the content for anyway
        }
        return true;
    }

    internal Base DeserializeElementInternal(Type targetType, XmlReader reader, FhirXmlPocoDeserializerState state)
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

    // We expect to start at the open tag op the element. When done, the reader will be at the next token after this element or end of the file.
    internal void DeserializeElementInto(Base target, ClassMapping mapping, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        var oldErrors = state.Errors.Count;
        var (lineNumber, position) = reader.GenerateLineInfo();
        var hasValueAttribute = reader.GetAttribute("value") != null;
        var depth = reader.Depth;
        var name = reader.LocalName;

        //check if on opening tag
        if (reader.NodeType != XmlNodeType.Element)
            throw new InvalidOperationException($"Xml node of type '{reader.NodeType}' is unexpected at this point");

        if (reader.HasAttributes)
            readAttributes(_inspector, target, mapping, reader, state);

        //Empty elements have no children e.g. <foo value="bar/>)
        if (!reader.IsEmptyElement)
        {
            //read the next object that has content
            reader.ReadToContent(state);

            if (!(hasValueAttribute || (reader.Depth > depth)))
            {
                //previous element didn't have a value and the current value is not a child of the previous element.
                //error is thrown with the location and the name of the previous element.
                var locationMessage = XmlReaderExtensions.GenerateLocationMessage(lineNumber, position);
                state.Errors.Add(ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN(state.Path.GetInstancePath(), lineNumber, position, locationMessage, name));
            }

            int highestOrder = 0;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var (propMapping, propValueMapping, error) = tryGetMappedElementMetadata(_inspector, mapping, reader, state.Path, reader.LocalName);

                if (error is not null)
                {
                    // report the error anyway
                    state.Errors.Add(error);
                    
                    // we don't know this property: Try to parse anyway and throw it into dynamic and overflow
                    deserializeUnknownPropertyValue(target, reader, state);
                    
                    // don't process further
                    while (reader.ShouldSkipNodeType(state))
                    {
                        reader.Skip();
                    }
                    continue;
                }

                bool validNamespace = true;
                if (propMapping is not null)
                    validNamespace = validateNameSpace(reader, state, propMapping);

                if (error is null && propMapping is not null && validNamespace)
                {
                    state.Path.EnterElement(propMapping.Name, !propMapping.IsCollection ? null : 0, propMapping.IsPrimitive);
                    var (order, incorrectOrder) = checkOrder(reader, state, highestOrder, propMapping);
                    highestOrder = order;

                    try
                    {
                        deserializePropertyValue(target, reader, state, oldErrors, incorrectOrder, propMapping, propValueMapping);
                    }
                    finally
                    {
                        state.Path.ExitElement();
                    }
                }
                else
                {
                    // we don't know this property: error is already thrown in "tryGetMappedElementMetadata(_inspector, mapping, reader, name)";
                    // So skip processing this node
                    reader.Skip();
                    // And continue to skip while the node is something we don't care about such as whitespace
                    while (reader.ShouldSkipNodeType(state))
                    {
                        reader.Skip();
                    }
                }
            }
        }
        else if (!hasValueAttribute)
        {
            //This empty element no children and no value attribute;
            state.Errors.Add(ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN(reader, state.Path.GetInstancePath(), reader.LocalName));
        }

        if (Settings.Validator is not null && (Settings.ValidateOnFailedParse || oldErrors == state.Errors.Count))
        {
            var context = new InstanceDeserializationContext(
                state.Path,
                lineNumber, position,
                mapping,
                Settings.NarrativeValidation);

            PocoDeserializationHelper.RunInstanceValidation(target, Settings.Validator, context, state.Errors);
        }

        reader.ReadToContent(state);
    }

    private void deserializeUnknownPropertyValue(Base target, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();
        
        var (propertyName, choiceType) = tryDetectChoiceTypeFromName(reader.LocalName);

        var mapping = choiceType ?? _inspector.FindClassMapping(nameof(DynamicDataType));

        var primitive = (mapping!.Factory() as Base)!;

        DeserializeElementInto(primitive, mapping, reader, state);
        
        setPropertyWithRepeating(target, propertyName, mapping, primitive);
        
        
        (string name, ClassMapping? choiceType) tryDetectChoiceTypeFromName(string propertyName)
        {
            var span = propertyName.AsSpan();
            for(int i = 0; i < span.Length; i++)
            {
                if (!char.IsUpper(span[i])) 
                    continue;

                var subSpan = span.Slice(i);
                if (subSpan.IsEmpty)
                    break;
                
                var choiceMapping = _inspector.FindClassMapping(subSpan.ToString());
                if (choiceMapping is not null)
                    return (span[..i].ToString(), choiceMapping);
            }
            return (propertyName, null);
        }
    }

    private static void parseUnknownAttributeValue(ModelInspector inspector, XmlReader reader, FhirXmlPocoDeserializerState state, Base target)
    {
        var attrName = reader.LocalName;
        var type = reader.ValueType;
        var trimmedVal = reader.Value.Trim();
        var val = parsePrimitiveValue(trimmedVal, type, state.Path);

        var baseVal = new DynamicPrimitive() { ObjectValue = val };
                
        setPropertyWithRepeating(target, attrName, inspector.FindClassMapping(typeof(DynamicPrimitive))!, baseVal);
    }

    private static (int highestOrder, bool incorrectOrder) checkOrder(XmlReader reader, FhirXmlPocoDeserializerState state, int highestOrder, PropertyMapping propMapping)
    {
        var incorrectOrder = false;
        //check if element is in the correct order.
        if (propMapping.Order >= highestOrder)
        {
            highestOrder = propMapping.Order;
        }
        else
        {
            state.Errors.Add(ERR.ELEMENT_OUT_OF_ORDER(reader, state.Path.GetInstancePath(), reader.LocalName));
            incorrectOrder = true;
        }
        return (highestOrder, incorrectOrder);
    }

    private void deserializePropertyValue(Base target, XmlReader reader, FhirXmlPocoDeserializerState state, int oldErrors, bool incorrectOrder, PropertyMapping propMapping, ClassMapping? propValueMapping)
    {
        var (lineNumber, position) = reader.GenerateLineInfo();
        var name = reader.LocalName;

        object? result = propMapping.IsCollection
            ? addToList(target, propValueMapping!, propMapping, reader, state)
            : readSingleValue(propValueMapping!, propMapping, reader, state);

        if (!propMapping.IsCollection && propMapping.GetValue(target) != null)
        {
            state.Errors.Add(ERR.INVALID_DUPLICATE_PROPERTY(reader, state.Path.GetInstancePath(), propMapping.Name));
        }

        if (Settings.Validator is not null && (Settings.ValidateOnFailedParse || oldErrors == state.Errors.Count))
        {
            var context = new PropertyDeserializationContext(
                target,
                state.Path, // should this path GetPath or this?
                name,
                lineNumber, position,
                propMapping,
                Settings.NarrativeValidation);

            PocoDeserializationHelper.RunPropertyValidation(result, Settings.Validator, context, state.Errors);
        }

        setPropertyWithRepeating(target, propMapping.Name, propValueMapping!, result);        
        try
        {
            _ = propMapping.GetValue(target);
        }
        catch (CodedValidationException ex)
        {
            state.Errors.Add(new CodedValidationException(ex.ErrorCode, ex.Message, state.Path.GetInstancePath(), lineNumber, position, ex.IssueSeverity, ex.IssueType));
        }
    }

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
        return new XHtml(xhtml);
    }

    //Will create a new list, or adds encountered values to an already existing list (and reports a user error).
    private IList addToList(Base target, ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        // We know our POCOs will generate the correct, non-null list.
        var currentList = (IList)propMapping.GetValue(target)!;

        readIntoList(currentList, propValueMapping, propMapping, reader, state);
        return currentList;
    }

    //When done, the reader will be at the next token after the last element of the list or end of the file.
    private void readIntoList(IList targetList, ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, FhirXmlPocoDeserializerState state)
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
            targetList.Add(newEntry);
            state.Path.IncrementIndex();
        }
    }

    private object? readSingleValue(ClassMapping propValueMapping, PropertyMapping propMapping, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        if (propMapping.Choice == ChoiceType.ResourceChoice)
        {
            return deserializeResourceContainer(reader, state);
        }
        else if (propMapping.SerializationHint == Specification.XmlRepresentation.XHtml)
        {
            return readXhtml(reader);
        }
        else
        {
            var newDatatype = (Base)propValueMapping.Factory();
            DeserializeElementInto(newDatatype, propValueMapping, reader, state);
            return newDatatype;
        }
    }

    private object? deserializeResourceContainer(XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        var depth = reader.Depth;
        // we are currently at the resource container (e.g. <contained>)
        if (reader.HasAttributes)
        {
            state.Errors.Add(ERR.NO_ATTRIBUTES_ALLOWED_ON_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath(), reader.LocalName));
        }
        // let's move to the actual resource
        reader.ReadToContent(state);
        var result = DeserializeResourceInternal(reader, state);
        // now we should be at the closing element of the resource container (e.g. </contained>). We should check that and maybe fix that.)
        if (reader.Depth != depth && reader.NodeType != XmlNodeType.EndElement)
        {
            state.Errors.Add(ERR.UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER(reader, state.Path.GetInstancePath(), reader.LocalName));

            // skip until we're back at the closing of the </contained>
            while (!(reader.Depth == depth && reader.NodeType == XmlNodeType.EndElement))
            {
                reader.Read();
            }
        }

        //we move out of the container to the next element.
        reader.ReadToContent(state);
        return result;
    }

    private static void readAttributes(ModelInspector inspector, Base target, ClassMapping propValueMapping, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        var elementName = reader.LocalName;
        //move into first attribute
        if (reader.MoveToFirstAttribute())
        {
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
                        state.Errors.Add(ERR.SCHEMALOCATION_DISALLOWED(reader, state.Path.GetInstancePath()));
                    }
                    else
                    {
                        var propMapping = propValueMapping.FindMappedElementByName(reader.LocalName);
                        if (propMapping is not null)
                        {
                            state.Path.EnterElement(propMapping.Name, !propMapping.IsCollection ? null : 0, propMapping.IsPrimitive);
                            try
                            {
                                readAttribute(target, propMapping!, elementName, reader, state);
                            }
                            finally
                            {
                                state.Path.ExitElement();
                            }
                        }
                        else
                        {   
                            state.Path.EnterElement(elementName, 0, true);
                            try
                            {
                                // handle unknown property
                                parseUnknownAttributeValue(inspector, reader, state, target);
                            }
                            finally
                            {
                                state.Path.ExitElement();
                            }
                            // state.Errors.Add(ERR.UNKNOWN_ATTRIBUTE(reader, state.Path.GetInstancePath(), reader.LocalName));
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
    }

    ///Parse current attribute value to set the value property of the target.
    private static void readAttribute(Base target, PropertyMapping propMapping, string elementName, XmlReader reader, FhirXmlPocoDeserializerState state)
    {
        if (!string.IsNullOrEmpty(reader.NamespaceURI) && reader.NamespaceURI != XmlNs.FHIR)
            state.Errors.Add(ERR.INCORRECT_ATTRIBUTE_NAMESPACE(reader, state.Path.GetInstancePath(), reader.LocalName, elementName, reader.NamespaceURI));

        // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading and
        // trailing whitespace when reading attribute values (for XML schema conformance)"
        string trimmedValue = reader.Value.TrimEnd().TrimStart();

        if (string.IsNullOrEmpty(trimmedValue))
            state.Errors.Add(ERR.ATTRIBUTE_HAS_EMPTY_VALUE(reader, state.Path.GetInstancePath()));

        var parsedValue = parsePrimitiveValue(trimmedValue, propMapping.ImplementingType, state.Path);

        if (target is PrimitiveType primitive && propMapping.Name == "value")
        {
            primitive.ObjectValue = parsedValue;
        }
        else
        {
            // Handle atomic-types-as-primitives, Element.id, Extension.url etc.
            propMapping.SetValue(target, parsedValue);
        }
    }

    private static object parsePrimitiveValue(string trimmedValue, Type implementingType, PathStack pathStack)
    {
        if (implementingType == typeof(FhirString))
            return new FhirString(trimmedValue);
        if (implementingType == typeof(FhirUri))
            return new FhirUri(trimmedValue);

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

        return trimmedValue;
    }

    /// <summary>
    /// Returns the <see cref="ClassMapping" /> for the object to be deserialized using the root property.
    /// </summary>
    /// <remarks>Assumes the reader is on the start of an object.</remarks>
    private static (ClassMapping?, FhirXmlException?) determineClassMappingFromInstance(XmlReader reader, ModelInspector inspector, PathStack path)
    {
        var resourceMapping = inspector.FindClassMapping(reader.LocalName);

        resourceMapping ??= inspector.FindClassMapping(nameof(DynamicResource));

        return resourceMapping is not null ?
            (new(resourceMapping, null)) :
            (new(null, ERR.UNKNOWN_RESOURCE_TYPE(reader, path.GetInstancePath(), reader.LocalName)));
    }

    /// <summary>
    /// Given a possibly suffixed property name (as encountered in the serialized form), lookup the
    /// mapping for the property and the mapping for the value of the property.
    /// </summary>
    /// <remarks>In case the name is a choice type, the type suffix will be used to determine the returned
    /// <see cref="ClassMapping"/>, otherwise the <see cref="PropertyMapping.ImplementingType"/> is used.
    /// </remarks>
    private static (PropertyMapping? propMapping, ClassMapping? propValueMapping, FhirXmlException? error) tryGetMappedElementMetadata(
        ModelInspector inspector,
        ClassMapping parentMapping,
        XmlReader reader,
        PathStack path,
        string propertyName)
    {
        var propertyMapping = parentMapping.FindMappedElementByName(propertyName)
                              ?? parentMapping.FindMappedElementByChoiceName(propertyName);

        if (propertyMapping is null)
        {
            return (null, null, ERR.UNKNOWN_ELEMENT(reader, path.GetInstancePath(), propertyName));
        }

        (ClassMapping? propertyValueMapping, FhirXmlException? error) = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) is { } m
                    ? (m, null)
                    : throw new InvalidOperationException($"Encountered property type {propertyMapping.ImplementingType} for which no mapping was found in the model assemblies. " + reader.GenerateLocationMessage()),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(reader),
            _ => throw new NotImplementedException("Unknown choice type in property mapping. " + reader.GenerateLocationMessage())
        };

        return (propertyMapping, propertyValueMapping, error);

        (ClassMapping?, FhirXmlException?) getChoiceClassMapping(XmlReader r)
        {
            ClassMapping? choiceMapping = null;
            string typeSuffix = propertyName[propertyMapping.Name.Length..];
            
            if(!string.IsNullOrEmpty(typeSuffix))
                choiceMapping = inspector.FindClassMapping(typeSuffix);

            choiceMapping ??= inspector.FindClassMapping(nameof(DynamicDataType));
            
            if(choiceMapping is not null)
                return (choiceMapping, null);
            
            return (null, ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE(r, path.GetInstancePath(), propertyMapping.Name, typeSuffix));
        }
    }
}

internal class FhirXmlPocoDeserializerState
{
    public readonly ExceptionAggregator Errors = new();
    public readonly PathStack Path = new();
}