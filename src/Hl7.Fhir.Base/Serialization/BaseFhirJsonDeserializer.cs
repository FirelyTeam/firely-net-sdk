/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

#nullable enable

namespace Hl7.Fhir.Serialization;


/// <summary>
/// Deserializes Json into FHIR POCO objects.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/json.html. </remarks>
public class BaseFhirJsonDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirJsonDeserializer(ModelInspector inspector) : this(inspector, new DeserializerSettings())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirJsonDeserializer(ModelInspector inspector, DeserializerSettings? settings)
    {
        Settings = settings ?? new DeserializerSettings();
        _inspector = inspector;
    }

    /// <summary>
    /// The settings that were passed to the constructor.
    /// </summary>
    public DeserializerSettings Settings { get; set; }

    private const string INSTANCE_VALIDATION_KEY_SUFFIX = ":instance";
    private const string PROPERTY_VALIDATION_KEY_SUFFIX = ":property";
    private readonly ModelInspector _inspector;

    /// <summary>
    /// Deserialize the FHIR Json from the reader and create a new POCO object containing the data from the reader.
    /// </summary>
    /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
    /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
    /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
    /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
    /// <remarks>The <see cref="ParserSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeResource(ref Utf8JsonReader reader, [NotNullWhen(true)] out Resource? instance, out IEnumerable<CodedException> issues)
    {
        if (reader.CurrentState.Options.CommentHandling is not JsonCommentHandling.Skip and not JsonCommentHandling.Disallow)
            throw new InvalidOperationException("The reader must be set to ignore or refuse comments.");

        // If the stream has just been opened, move to the first token.
        if (reader.TokenType == JsonTokenType.None) reader.Read();

        PocoDeserializerState state = new();

        instance = (Resource)createNewObjectInstance(ref reader, ClassMapping.Resource, state, out var classMapping);

        deserializeObjectInto(ref reader, instance, classMapping, state, stayOnLastToken: true);
        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    /// <summary>
    /// Reads a (subtree) of serialized FHIR Json data into a POCO object.
    /// </summary>
    /// <param name="targetType">The type of POCO to construct and deserialize</param>
    /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
    /// <param name="instance">The result of deserialization. May be incomplete when there are issues.</param>
    /// <param name="issues">Issues encountered while deserializing. Will be empty when the function returns true.</param>
    /// <returns><c>false</c> if there are issues, <c>true</c> otherwise.</returns>
    /// <remarks>The <see cref="ParserSettings.ExceptionFilter"/> influences which issues are returned.</remarks>
    public bool TryDeserializeObject(Type targetType, ref Utf8JsonReader reader, [NotNullWhen(true)] out Base? instance, out IEnumerable<CodedException> issues)
    {
        if (reader.CurrentState.Options.CommentHandling is not JsonCommentHandling.Skip and not JsonCommentHandling.Disallow)
            throw new InvalidOperationException("The reader must be set to ignore or refuse comments.");

        // If the stream has just been opened, move to the first token.
        if (reader.TokenType == JsonTokenType.None) reader.Read();

        var mapping = _inspector.FindOrImportClassMapping(targetType) ??
                      throw new ArgumentException($"Type '{targetType}' could not be located and can " +
                                                  $"therefore not be used for deserialization. " + reader.GenerateLocationMessage(), nameof(targetType));

        var state = new PocoDeserializerState();
        instance = createNewObjectInstance(ref reader, mapping, state, out _);
        deserializeObjectInto(ref reader, instance, mapping, state, stayOnLastToken: true);

        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    /// <summary>
    /// Reads a json object into an existing instance of a POCO.
    /// </summary>
    /// <param name="reader">Thereader to read the json tokens from.</param>
    /// <param name="target">The POCO to read the contents of the json object into.</param>
    /// <param name="mapping">The <see cref="ClassMapping"/> for the instance to parse.</param>
    /// <param name="state">The parsing state for this parsing run.</param>
    /// <param name="stayOnLastToken">Normally, the reader will be on the first token *after* the object, however,
    /// System.Text.Json converters expect the readers on the last token of the object. Since all logic
    /// in this class assumes the first case, we make a special case for the outermost call to this function
    /// done by the <see cref="TryDeserializeObject(Type, ref Utf8JsonReader, out Base?, out IEnumerable{CodedException})"/> function, which is in its
    /// turn called by System.Text.Json upon a <see cref="FhirJsonConverter{F}.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" /></param>.
    /// <remarks>Reader will be on the first token after the object upon return, but see <paramref name="stayOnLastToken"/>.</remarks>
    private void deserializeObjectInto(
        ref Utf8JsonReader reader,
        Base target,
        ClassMapping mapping,
        PocoDeserializerState state,
        bool stayOnLastToken = false)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new InvalidOperationException($"deserializeObjectInto should only be called on JSON objects: " +
                                                $"Current token is {reader.TokenType}.");
        
        reader.Read();

        var empty = true;
        var objectParsingState = new ObjectParsingState();
        var (line, pos) = reader.GetLocation();

        if (mapping.IsResource)
            state.Path.EnterResource(mapping.Name);

        int nErrorCount = state.Errors.Count;

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            var currentPropertyName = reader.GetString()!;

            // The resourceType property on the level of a resource is used to determine
            // the type and should otherwise be skipped when processing a resource.
            if (currentPropertyName == "resourceType" && mapping.IsResource)
            {
                reader.SkipTo(JsonTokenType.PropertyName);
                continue;
            }

            empty = false;

            // move past property name
            reader.Read();
            
            deserializePropertyInto(target, mapping, currentPropertyName, ref reader, state, objectParsingState);
        }

        if (mapping.IsResource)
            state.Path.ExitResource();

        // Now after having deserialized all properties we can run the validations that needed to be
        // postponed until after all properties have been seen (e.g. Instance and Property validations for
        // primitive properties, since they may be composed from two properties `name` and `_name` in json
        // and should only be validated when both have been processed, even if megabytes apart in the json file).
        objectParsingState.RunDelayedValidation();

        // read past object, unless this is the last EndObject in the top-level Deserialize call
        if (!stayOnLastToken) reader.Read();

        // do not allow empty complex objects.
        if (empty) state.Errors.Add(ERR.OBJECTS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));

        // If we need to validate & this is not a fhir primitive, run validation immediately on the object.
        // If this is a FHIR primitive, we will run the validation later, when we have both the `name` and `_name` properties.
        // This is done through the "delayed validation" mechanism.
        if (Settings.Validator is not null && !mapping.IsFhirPrimitive)
        {
            var context = new PocoValidationContext(target, _inspector, state.Path.GetInstancePath, line,pos, Settings.NarrativeValidation);
            state.Errors.Add(Settings.Validator.ValidateObject(target, mapping, context));
        }

        // If we have been parsing a resource, annotate any new validation errors that were encountered (if endaled).
        if (mapping.IsResource && Settings.AnnotateResourceParseExceptions && state.Errors.Count > nErrorCount)
        {
            List<CodedException> resourceErrs = state.Errors.Skip(nErrorCount).ToList();
            target.SetAnnotation(resourceErrs);
        }
    }

    private Base createNewObjectInstance(ref Utf8JsonReader reader, ClassMapping mapping, PocoDeserializerState state, out ClassMapping usedMapping)
    {
        if (mapping.IsResource)
        {
            usedMapping = DetermineResourceClassMappingFromInstance(ref reader, _inspector, state);
            return usedMapping.CreateInstance();
        }

        // If this is not a resource, we can create a new instance of the class mapping.
        if (mapping.CreateInstance() is not Base result)
            throw new ArgumentException(
                $"Can only deserialize into subclasses of class {nameof(Base)}. " + reader.GenerateLocationMessage(),
                nameof(mapping));

        usedMapping = mapping;
        return result;
    }

    /// <summary>
    /// Reads a property into the target object. Will try to determine the most appropriate mapping for the property,
    /// fetch the current value (if any) and deserialize the value from the reader into the property. After parsing,
    /// sets the value on the target object and runs property validation.
    /// </summary>
    private void deserializePropertyInto(
        Base target,
        ClassMapping parentMapping,
        string propertyName,
        ref Utf8JsonReader reader,
        PocoDeserializerState state,
        ObjectParsingState delayedValidations)
    {
        var (line, pos) = reader.GetLocation();

        // Lookup the metadata for this property by its name to determine the expected type of the value
        var metadata = getMappingForElement(parentMapping, propertyName, state, delayedValidations, ref reader);
        var propertyMapping = metadata.PropertyMapping;
        var name = propertyMapping.Name;

        target.TryGetValue(name, out var existingValue);

        if (metadata.PropertyMapping.IsCollection || existingValue is IList)
            throw new NotImplementedException("Don't handle lists yet.");

        var result = (Base?)existingValue ?? createNewObjectInstance(ref reader, metadata.ValueMapping, state, out _);

        // TODO: logic to create a new instance of the property if it is not present.
        // and/or warn about duplicate properties. Maybe this is what is going to be
        // handled with deserializeNewResourceOrComplex?

        try
        {
            state.Path.EnterElement(name, propertyMapping.IsCollection ? 0 : null, propertyMapping.IsPrimitive);
            deserializeJsonValueInto(ref reader, result, propertyName, name, metadata.ValueMapping, state);
        }
        finally
        {
            state.Path.ExitElement();
        }

        // If it turns out this property is now a list (since we encountered a new instance of it)
        // let's update our local property mappings to reflect that.
        if (result is IList && !propertyMapping.IsCollection)
        {
            var updatedPropMapping = metadata.PropertyMapping.PromoteToList();
            delayedValidations.LocalPropertyMappings[name] = updatedPropMapping;
        }

        target.SetValue(name, result);

        if (Settings.AnnotateLineInfo && result is Base b)
            b.AddAnnotation(new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos });

        if (Settings.Validator is not null)
        {
            var elementName = propertyMapping.Name;
            var deserializationContext = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath,
                line, pos,
                Settings.NarrativeValidation
            ) { MemberName = elementName };

            // If this is a FhirPrimitive, make sure we delay validation until we had the
            // chance to encounter both the `name` and `_name` property.
            if (metadata.ValueMapping.IsFhirPrimitive)
            {
                delayedValidations.ScheduleDelayedValidation(
                    name + PROPERTY_VALIDATION_KEY_SUFFIX,
                    () =>
                    {
                        state.Path.EnterElement(name, null, propertyMapping.IsPrimitive);
                        state.Errors.Add(Settings.Validator.ValidateProperty(name, result, propertyMapping,
                            deserializationContext));
                        state.Path.ExitElement();
                    });
            }
            else
                state.Errors.Add(Settings.Validator.ValidateProperty(propertyName, result, propertyMapping,
                    deserializationContext));
        }
    }

    /// <summary>
    /// The core of the parsing logic. Will "upgrade" existing instances to lists if needed, hence the
    /// returned type is object, which can be a single Base or a list of Base.
    /// </summary>
    private void deserializeJsonValueInto(ref Utf8JsonReader reader,
        Base existingValue,
        string propertyName, string elementName,
        ClassMapping propertyValueMapping,
        PocoDeserializerState state)
    {
        bool onUnderscoreProperty = propertyName[0] == '_';

        if(onUnderscoreProperty && !propertyValueMapping.IsFhirPrimitive)
            state.Errors.Add(ERR.USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName, propertyName));

        if (isOnJsonPrimitiveToken(ref reader))
        {
            if (onUnderscoreProperty)
                state.Errors.Add(ERR.UNDERSCORE_SHOULD_BE_OBJECT(ref reader, state.Path.GetInstancePath(), propertyName));
            // This else is important to avoid duplicative error messages.
            else if(!propertyValueMapping.IsFhirPrimitive)
                state.Errors.Add(ERR.UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName));

            deserializePrimitiveInto(ref reader, existingValue, propertyValueMapping);
        }
        else if (isOnJsonObject(ref reader))
        {
            if(propertyValueMapping.IsFhirPrimitive)
                state.Errors.Add(ERR.UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName));

            deserializeObjectInto(ref reader, existingValue, propertyValueMapping, state);
        }
        else if (reader.TokenType is JsonTokenType.Null)
        {
            state.Errors.Add(ERR.EXPECTED_PRIMITIVE_NOT_NULL(ref reader, state.Path.GetInstancePath()));
        }
        else
        {
            // Completely unexpected tokens (e.g. StartArray, EndArray, comments, EndObject, etc.)
            throw new InvalidOperationException($"Encountered unexpected token {reader.TokenType} " +
                                                $"while parsing a primitive or object for property '{propertyName}'.");
        }

        // else if (reader.TokenType == JsonTokenType.StartObject)
        // {
        //     // Handles 1) name: {<complex>} or 2) _name: {} where name is not known.
        //     // When this is an unknown complex `name`, map it to a dynamic datatype.
        //     propertyValueMapping ??= new ClassMappingDynamic(ClassMapping.DynamicDataType, null);
        //
        //     // We found the same property twice. This is illegal in json, but since we're using a forward
        //     // reader, it's up to us to detect it. Note that we will overwrite the old value with the new one,
        //     // so this is a data loss situation.
        //     if(existingValue is Base)
        //         state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
        //
        //     result = deserializeNewResourceOrComplex(ref reader, propertyValueMapping, state);
        // }
        // else if (reader.TokenType == JsonTokenType.StartArray)
        // {
        //     // Handle arrays. Can be name or _name.
        //     // If we don't have a mapping, we need to guess the type of the first element.
        //     if (propertyValueMapping is null)
        //     {
        //         var peekedType = getFhirTypeForToken(peekArrayElementToken(ref reader));
        //         propertyValueMapping = new ClassMappingDynamic(_inspector.FindClassMapping(peekedType)!, null);
        //     }
        //
        //     var listFactory = propertyMapping is not null
        //         ? _inspector.FindOrImportClassMapping(propertyMapping.ImplementingType)!
        //         : propertyValueMapping.Original;
        //
        //     IList primitiveList;
        //     if (existingValue is not IList list)
        //     {
        //         primitiveList = listFactory.ListFactory();
        //         if (existingValue is not null)
        //             primitiveList.Add(existingValue);
        //     }
        //     else
        //         primitiveList = list;
        //
        //     result = propertyValueMapping.Original.IsFhirPrimitive
        //         ? deserializeFhirPrimitiveList(primitiveList, propertyName, propertyValueMapping, ref reader, parsingState, state)
        //         : deserializeNormalList(primitiveList, propertyValueMapping, ref reader, state);
        // }
    }

    /// <summary>
    /// Look for the first token in an array that is not a null or a nested array, so
    /// the first "real" element with content.
    /// </summary>
    private static JsonTokenType peekArrayElementToken(ref Utf8JsonReader reader)
    {
        var peekCopy = reader;
        while (peekCopy.TokenType is JsonTokenType.StartArray or JsonTokenType.Null)
        {
            peekCopy.Read();
        }

        return peekCopy.TokenType;
    }
    

    private static bool isOnJsonPrimitiveToken(ref Utf8JsonReader reader) =>
        reader.TokenType is
            JsonTokenType.String or JsonTokenType.Number
            or JsonTokenType.False or JsonTokenType.True;

    private static bool isOnJsonObject(ref Utf8JsonReader reader) =>
        reader.TokenType is JsonTokenType.StartObject;

    /// <summary>
    /// Reads the content of a list with non-FHIR-primitive content (so, no name/_name pairs to be dealt with). Note
    /// that the contents can only be complex in the current FHIR serialization, but we'll be prepared and handle
    /// other situations (e.g. repeating Extension.url's, if they would ever exist).
    /// </summary>
    private IList deserializeNormalList(
        IList existingList,
        PropertyValueMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        PocoDeserializerState state)
    {
        throw new NotImplementedException();

//         if (reader.TokenType != JsonTokenType.StartArray)
//             throw new InvalidOperationException($"deserializeNormalList should only be called on JSON array: " +
//                                                 $"Current token is {reader.TokenType}.");
//         //   bool hasUnexpectedElements = false;
//         // Read past start of array
//         reader.Read();
//
//         if (reader.TokenType == JsonTokenType.EndArray)
//             state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));
//
//         if (existingList.Count > 0)
//         {
//             state.Path.IncrementIndex(existingList.Count);
//             state.Errors.Add(ERR.DUPLICATE_ARRAY(ref reader, state.Path.GetInstancePath()));
//         }
//
//         //TODO: catch only nulls?
//
//         // Can't make an iterator because of the ref readers struct, so need
//         // to simply create a list by Adding(). Not the fastest approach :-(
//         while (reader.TokenType != JsonTokenType.EndArray)
//         {
//             var (line, pos) = reader.GetLocation();
//             if (reader.TokenType == JsonTokenType.StartObject)
//             {
//                 var result = deserializeNewResourceOrComplex(ref reader, propertyValueMapping, state);
//                 existingList.Add(result);
//             }
//             else if(reader.TokenType == JsonTokenType.Null)
//             {
//                 existingList.Add(null);
//                 reader.Read();
//             }
//             else if(reader.TokenType == JsonTokenType.StartArray)
//             {
//                 state.Errors.Add(ERR.NESTED_ARRAY(ref reader, state.Path.GetInstancePath()));
//                 _ = deserializeNormalList(existingList, propertyValueMapping, ref reader, state);
//             }
//             else
//             {
//                 // This is a primitive token, which we cannot
//                 var elem = propertyValueMapping.CreateInstance();
//
//                 deserializePropertyInto(elem, "value", ref reader, state, new(), forceDelayedValidation: true);
//                 existingList.Add(elem);
// //                hasUnexpectedElements = true;
//             }
//
//             if (Settings.AnnotateLineInfo && existingList[^1] is Base b)
//                 b.AddAnnotation(new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos, ArrayIndex = existingList.Count - 1});
//
//             state.Path.IncrementIndex();
//         }
//
//         // Read past end of array
//         reader.Read();
//
//         // encountered invalid data, convert whole list to List<DynamicDataType>
//     //    if(hasUnexpectedElements)
//     //        return existingList.ToDynamicDataType();
//
//         return existingList;
    }

    internal class ObjectParsingState
    {
        private readonly Dictionary<string, Action> _validations = new();
        private readonly Dictionary<string, int> _parsedPropValue = new();
        public Dictionary<string, PropertyMapping> LocalPropertyMappings = new();

        public int GetPropertyIndex(string memberName)
        {
            if (_parsedPropValue.TryGetValue(memberName, out int propertyIndex))
                return propertyIndex;
            _parsedPropValue.Add(memberName, 0);
            return 0;
        }

        public void SetPropertyIndex(string memberName, int count)
        {
            _parsedPropValue[memberName] = count;
        }

        public void ScheduleDelayedValidation(string key, Action validation)
        {
            // Add or overwrite the entry for the given key.
            if (_validations.ContainsKey(key)) _validations.Remove(key);
            _validations[key] = validation;
        }

        public void RunDelayedValidation()
        {
            foreach (var validation in _validations.Values) validation();
        }
    }

    /// <summary>
    /// Reads a list of FHIR primitives (either from a name or _name property).
    /// </summary>
    /// <remarks>Upon completion, reader will be located at the next token afther the list.</remarks>
    private IList deserializeFhirPrimitiveList(
        IList existingList,
        string propertyName,
        PropertyValueMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        ObjectParsingState delayedValidations,
        PocoDeserializerState state)
    {
        throw new NotImplementedException();

     //    if (reader.TokenType != JsonTokenType.StartArray)
     //        throw new InvalidOperationException($"deserializeFhirPrimitiveList should only be called on JSON array: " +
     //                                            $"Current token is {reader.TokenType}.");
     //
     //    // read into array
     //    reader.Read();
     //
     //    if (reader.TokenType == JsonTokenType.EndArray)
     //        state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));
     //
     //    int originalSize = existingList.Count;
     //
     //    // Can't make an iterator because of the ref readers struct, so need
     //    // to simply create a list by Adding(). Not the fastest approach :-(
     //    bool? onlyNulls = null;
     //    int elementIndex = delayedValidations.GetPropertyIndex(propertyName);
     //    if (elementIndex > 0)
     //    {
     //        state.Path.IncrementIndex(elementIndex);
     //        state.Errors.Add(ERR.DUPLICATE_ARRAY(ref reader, state.Path.GetInstancePath()));
     //    }
     //
     //    parseListElements(ref reader);
     //
     //    if(onlyNulls is true)
     //        state.Errors.Add(ERR.PRIMITIVE_ARRAYS_ONLY_NULL(ref reader, state.Path.GetInstancePath()));
     //
     //    delayedValidations.SetPropertyIndex(propertyName, existingList.Count);
     //
     //    return existingList;
     //
     //    void parseListElements(ref Utf8JsonReader reader)
     //    {
     //        while (reader.TokenType != JsonTokenType.EndArray)
     //        {
     //
     //          	var (line, pos) = reader.GetLocation();
     //            if (reader.TokenType == JsonTokenType.Null)
     //            {
     //                onlyNulls ??= true;
     //
     //                if (elementIndex >= originalSize)
     //                    existingList.Add(null);
     //
     //                if (Settings.AnnotateLineInfo && existingList[elementIndex] is Base b)
     //                b.AddAnnotation(new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos, ArrayIndex = elementIndex});
     //
     //
     //                elementIndex += 1;
     //                state.Path.IncrementIndex();
     //
     //                // don't read any new data into the primitive here
     //                reader.Read();
     //            }
     //            else if(reader.TokenType == JsonTokenType.StartArray)
     //            {
     //                // Nested list
     //                onlyNulls = false;
     //                //delayedValidations.SetPropertyIndex(propertyName, existingList.Count - 1);
     //                state.Errors.Add(ERR.NESTED_ARRAY(ref reader, state.Path.GetInstancePath()));
     //                reader.Read();
     //                parseListElements(ref reader);
     //
     //                     if (Settings.AnnotateLineInfo && existingList[elementIndex] is Base b)
     //            b.AddAnnotation(new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos, ArrayIndex = elementIndex});
     //            }
     //            else
     //            {
     //                if (elementIndex >= originalSize)
     //                    existingList.Add(propertyValueMapping.CreateInstance());
     //
     //                onlyNulls = false;
     //                existingList[elementIndex] = DeserializeFhirPrimitive((PrimitiveType)existingList[elementIndex]!, propertyName, propertyValueMapping, ref reader, delayedValidations, state);
     //
     // if (Settings.AnnotateLineInfo && existingList[elementIndex] is Base b)
     //            b.AddAnnotation(new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos, ArrayIndex = elementIndex});
     //
     //                elementIndex += 1;
     //                state.Path.IncrementIndex();
     //
     //            }
     //
     //        }
     //
     //        // read past array to next property or end of object
     //        reader.Read();
     //    }
    }

    /// <summary>
    /// Deserializes a primitive Json token into an existing POCO instance. If the instance
    /// is a primitive, it will set its Value property, otherwise it will add an error and
    /// add a "value" property to the instance with the value of the primitive token.
    /// </summary>
    /// <remarks>Expects the current token to be a primitive. Upon completion,
    /// reader will be located at the next token after the FHIR primitive.</remarks>
    private void deserializePrimitiveInto(
        ref Utf8JsonReader reader,
        Base existing,
        ClassMapping propertyValueMapping
    )
    {
        var primitiveValue = readPrimitiveValue(ref reader, propertyValueMapping.PrimitiveValueProperty?.ImplementingType);

        if(existing is PrimitiveType existingPrimitive)
            existingPrimitive.JsonValue = primitiveValue;
        else
        {
            var preservedValue = pocoFromPrimitive(primitiveValue);
            existing.SetValue("Value", preservedValue);
        }

        static Base pocoFromPrimitive(object value)
        {
            return value switch
            {
                int i => new Integer(i),
                bool b => new FhirBoolean(b),
                decimal d => new FhirDecimal(d),
                string s => new FhirString(s),
                _ => new DynamicPrimitive { JsonValue = value }
            };
        }
    }



    /// <summary>
    /// Does a best-effort parse of the data available at the reader, given the required type of the property the
    /// data needs to be read into.
    /// </summary>
    /// <returns>A value without an error if the data could be parsed to the required type, and a value with an error if the
    /// value could not be parsed - in which case the value returned is the raw value coming in from the reader.</returns>
    /// <remarks>Upon completion, the reader will be positioned on the token after the primitive.</remarks>
    private object readPrimitiveValue(ref Utf8JsonReader reader, Type? valuePropertyType)
    {
        object value = reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString()!,
            JsonTokenType.Number => tryGetMatchingNumber(ref reader, valuePropertyType),
            JsonTokenType.True or JsonTokenType.False => reader.GetBoolean(),
            var other =>
                // This would be an internal logic error, since our callers should have made sure we're
                // on the primitive value after the property name (and the Utf8JsonReader would have complained about any
                // other token that one that is a value).
                throw new InvalidOperationException($"Unexpected token type {other} while parsing a primitive value. " +
                                                    reader.GenerateLocationMessage()),
        };

        // Read past the value
        reader.Read();

        return value;
    }

    /// <summary>
    /// This function tries to map from the json-format "generic" number to the kind of numeric type defined in the POCO.
    /// </summary>
    /// <remarks>Reader must be positioned on a number token. This function will not move the reader to the next token.</remarks>
    private static object tryGetMatchingNumber(ref Utf8JsonReader reader, Type? implementingTypeHint)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new InvalidOperationException($"Cannot read a numeric when reader is on a {reader.TokenType}. " +
                                                reader.GenerateLocationMessage());

        // Decimal and integer are the only two types in FHIR where we are using Json native numbers
        if (implementingTypeHint == typeof(decimal) && reader.TryGetDecimal(out decimal dec))
            return dec;
        if (implementingTypeHint == typeof(int) && reader.TryGetInt32(out int i32))
            return i32;

        // Ok, an incorrect number, let's try to get it by polling which type of number it is.
        if (reader.TryGetInt32(out i32))
            return i32;
        if (reader.TryGetDecimal(out dec))
            return dec;

        // Ok, whatever, grab the raw stuff.
        return reader.GetRawText();
    }

    /// <summary>
    /// Scans for the `resourceType` property in the current object and returns
    /// the <see cref="ClassMapping" /> for it. If anything is wrong (resourceType not found,
    /// or not a resource), the appropriate dynamic mapping will be returned.
    /// </summary>
    internal static ClassMapping DetermineResourceClassMappingFromInstance(ref Utf8JsonReader reader, ModelInspector inspector, PocoDeserializerState state)
    {
        var resourceType = scanForResourceType(ref reader, state);
        var path = state.Path.GetInstancePath();
        if (resourceType is null) return new ClassMapping(inspector, $"UnknownResource_{path}", typeof(DynamicResource));

        var resourceMapping = inspector.FindClassMapping(resourceType);

        if (resourceMapping is null)
        {
            resourceMapping = new ClassMapping(inspector, resourceType, typeof(DynamicResource));
            state.Errors.Add(ERR.UNKNOWN_RESOURCE_TYPE(ref reader, state.Path.GetInstancePath(), resourceType));
        }
        else if (!resourceMapping.IsResource)
        {
            state.Errors.Add(ERR.RESOURCE_TYPE_NOT_A_RESOURCE(ref reader, state.Path.GetInstancePath(), resourceType));
            resourceMapping = new ClassMapping(inspector, resourceType, typeof(DynamicResource));
        }

        return resourceMapping;
    }

    private static string? scanForResourceType(ref Utf8JsonReader reader, PocoDeserializerState state)
    {
        var originalReader = reader;    // copy the struct so we can "rewind"
        var atDepth = reader.CurrentDepth + 1;

        try
        {
            while (reader.Read() && reader.CurrentDepth >= atDepth)
            {
                if (reader.TokenType != JsonTokenType.PropertyName || reader.CurrentDepth != atDepth) continue;

                var propName = reader.GetString();
                if (propName != "resourceType") continue;

                reader.Read();
                if (reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }
                else
                {
                    state.Errors.Add(ERR.RESOURCETYPE_SHOULD_BE_STRING(ref reader, state.Path.GetInstancePath(), reader.TokenType,
                        reader.GetRawText()));
                    return reader.GetRawText();
                }
            }

            state.Errors.Add(ERR.NO_RESOURCETYPE_PROPERTY(ref reader, ""));
            return null;
        }
        finally
        {
            reader = originalReader;
        }
    }

    /// <summary>
    /// Given a possibly suffixed property name (as encountered in the serialized form), lookup the
    /// mapping for the property and the mapping for the value of the property.
    /// </summary>
    /// <remarks>In case the name is a choice type, the type suffix will be used to determine the returned
    /// <see cref="ClassMapping"/>, otherwise the <see cref="PropertyMapping.ImplementingType"/> is used. As well,
    /// since the property name is from the serialized form it may also be prefixed by '_'.
    /// </remarks>
    private PropertyValueMapping getMappingForElement(
        ClassMapping parentMapping,
        string propertyName,
        PocoDeserializerState state,
        ObjectParsingState objectParsingState,
        ref Utf8JsonReader reader
        )
    {
        bool startsWithUnderscore = propertyName[0] == '_';
        var elementName = startsWithUnderscore ? propertyName[1..] : propertyName;

        var propertyMapping = objectParsingState.LocalPropertyMappings.GetValueOrDefault(elementName)
                              ?? parentMapping.FindMappedElementByName(elementName)
                              ?? parentMapping.FindMappedElementByChoiceName(elementName)
                              ?? getUnknownPropMapping(ref reader, startsWithUnderscore);

        ClassMapping propertyValueMapping = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                parentMapping.Inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ??
                    throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which" +
                                                        $" no mapping was found in the model assemblies."),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(ref reader),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported.")
        };

        return new PropertyValueMapping(propertyMapping, propertyValueMapping);

        ClassMapping getChoiceClassMapping(ref Utf8JsonReader r)
        {
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            if (!string.IsNullOrEmpty(typeSuffix))
            {
                var foundChoiceMapping = parentMapping.Inspector.FindClassMapping(typeSuffix);

                if (foundChoiceMapping is null)
                {
                    state.Errors.Add(ERR.CHOICE_ELEMENT_HAS_UNKNOWN_TYPE(ref r, state.Path.GetInstancePath(),
                        propertyMapping.Name, typeSuffix));
                    foundChoiceMapping = new ClassMapping(_inspector, typeSuffix, typeof(DynamicDataType));
                }

                return foundChoiceMapping;
            }

            var path = state.Path.GetInstancePath();
            state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(ref r, path, elementName));

            return new ClassMapping(_inspector, $"UnknownType_{path}", typeof(DynamicDataType));
        }

        // If the property is unknown, scan the reader for the first token. If that is a Json primitive,
        // derive the correct primitive datatype. If it is the start of an object, we assume this is some
        // complex datatype. If the name starts with an underscore, we assume this is a primitive (since
        // we have no more information to go on).
        //
        // TODO: This does mean that depending on the order of the properties,
        // primitives will either become DynamicPrimitive (if the _name was encountered first) or the
        // right FHIR Primitive (derived from the primitive value encountered). Is this desirable?
        // XML will always make it a FhirString - which is nicely predictable. What to do?
        PropertyMapping getUnknownPropMapping(ref Utf8JsonReader r, bool hasUnderscore)
        {
            var customPropertyMapping = r.TokenType switch
            {
                JsonTokenType.StartArray =>
                    new PropertyMapping(parentMapping, elementName, getCustomMappingTypeForToken(peekArrayElementToken(ref r))).PromoteToList(),
                _ =>
                    new PropertyMapping(parentMapping, elementName, getCustomMappingTypeForToken(r.TokenType))
            };

            objectParsingState.LocalPropertyMappings.Add(elementName, customPropertyMapping);

            return customPropertyMapping;

            Type getCustomMappingTypeForToken(JsonTokenType tokenType)
            {
                return tokenType switch
                {
                    JsonTokenType.String  => typeof(FhirString),
                    JsonTokenType.Number => typeof(FhirDecimal),
                    JsonTokenType.True or JsonTokenType.False => typeof(FhirBoolean),
                    JsonTokenType.StartObject when !hasUnderscore => typeof(DynamicDataType),
                    _ when hasUnderscore => typeof(DynamicPrimitive),
                    _ => typeof(DynamicDataType)
                };
            }
        }
    }
}