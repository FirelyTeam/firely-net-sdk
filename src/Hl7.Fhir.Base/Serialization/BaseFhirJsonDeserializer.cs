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
using System.Reflection;
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

        instance = DeserializeResourceInternal(ref reader, state, stayOnLastToken: true);
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

        // Create a new instance of the object to read the members into.
        if (mapping.Factory() is not Base result)
            throw new ArgumentException(
                $"Can only deserialize into subclasses of class {nameof(Base)}. " + reader.GenerateLocationMessage(),
                nameof(targetType));

        var state = new PocoDeserializerState();
        deserializeObjectInto(result, mapping, ref reader, DeserializedObjectKind.Complex, state, stayOnLastToken: true);

        instance = result;
        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    internal Resource? DeserializeResourceInternal(ref Utf8JsonReader reader, PocoDeserializerState state, bool stayOnLastToken)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new InvalidOperationException($"DeserializeResourceInternal should only be called on JSON objects: " +
                                                $"Current token is {reader.TokenType}.");

        var (resourceMapping, error, resourceType) = DetermineClassMappingFromInstance(ref reader, _inspector, state.Path);

        state.Errors.Add(error);
        
        if (resourceMapping is not null)
        {
            // If we have at least a mapping, let's try to continue
            var newResource = (Base)resourceMapping.Factory();
            
            // if we're on dynamic, the type is not recognized, so we should set dynamic to report it
            if(resourceType is not null && newResource is DynamicResource dr)
            {
                dr.DynamicTypeName = resourceType;
            }

            try
            {
                state.Path.EnterResource(resourceMapping.Name);
                int nErrorCount = state.Errors.Count;
                deserializeObjectInto(newResource, resourceMapping, ref reader, DeserializedObjectKind.Resource, state, stayOnLastToken);

                if (Settings.AnnotateResourceParseExceptions && state.Errors.Count > nErrorCount)
                {
                    List<CodedException> resourceErrs = state.Errors.Skip(nErrorCount).ToList();
                    ((Resource)newResource).SetAnnotation(resourceErrs);
                }
                return (Resource)newResource;
            }
            finally
            {
                state.Path.ExitResource();
            }
        }
        else
        {
            state.Errors.Add(error!);

            // Read past the end of this object to recover.
            reader.Recover();

            return null;
        }
    }

    /// <summary>
    /// The kind of object we need to deserialize into, which will influence subtly
    /// how the <see cref="deserializeObjectInto{T}(T, ClassMapping, ref Utf8JsonReader, DeserializedObjectKind, PocoDeserializerState, bool)" />
    /// function will operate.
    /// </summary>
    private enum DeserializedObjectKind
    {
        /// <summary>
        /// Deserialize into a complex datatype, and complain about the presence of
        /// a resourceType element.
        /// </summary>
        Complex,

        /// <summary>
        /// Deserialize into a resource
        /// </summary>
        Resource,

        /// <summary>
        /// Deserialize the non-value part of a FhirPrimitive, and do not call validation of
        /// the instance yet, since it will be done when the FhirPrimitive has been constructed
        /// completely, including its value part.
        /// </summary>
        FhirPrimitive
    }

    /// <summary>
    /// Reads a complex object into an existing instance of a POCO.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="mapping"></param>
    /// <param name="reader"></param>
    /// <param name="kind"></param>
    /// <param name="state"></param>
    /// <param name="stayOnLastToken">Normally, the reader will be on the first token *after* the object, however,
    /// System.Text.Json converters expect the readers on the last token of the object. Since all logic
    /// in this class assumes the first case, we make a special case for the outermost call to this function
    /// done by the <see cref="TryDeserializeObject(Type, ref Utf8JsonReader, out Base?, out IEnumerable{CodedException})"/> function, which is in its
    /// turn called by System.Text.Json upon a <see cref="FhirJsonConverter{F}.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" /></param>.
    /// <remarks>Reader will be on the first token after the object upon return, but see <paramref name="stayOnLastToken"/>.</remarks>
    private void deserializeObjectInto<T>(
        T target,
        ClassMapping mapping,
        ref Utf8JsonReader reader,
        DeserializedObjectKind kind,
        PocoDeserializerState state,
        bool stayOnLastToken = false) where T : Base
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new InvalidOperationException($"deserializeObjectInto should only be called on JSON objects: " +
                                                $"Current token is {reader.TokenType}.");
        
        reader.Read();

        var empty = true;
        var objectParsingState = new ObjectParsingState();
        var (line, pos) = reader.GetLocation();

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            var currentPropertyName = reader.GetString()!;

            // The resourceType property on the level of a resource is used to determine
            // the type and should otherwise be skipped when processing a resource.
            if (currentPropertyName == "resourceType" && kind is DeserializedObjectKind.Resource)
            {
                reader.SkipTo(JsonTokenType.PropertyName);
                continue;
            }

            empty = false;

            // Lookup the metadata for this property by its name to determine the expected type of the value
            bool startsWithUnderscore = currentPropertyName[0] == '_';
            var elementName = startsWithUnderscore ? currentPropertyName.Substring(1) : currentPropertyName;
            var (propMapping, propValueMapping) = tryGetMappedElementMetadata(_inspector, mapping, elementName, state, ref reader);

            // move past property name
            reader.Read();
            
            deserializePropertyInto(target, currentPropertyName, ref reader, state, objectParsingState, stayOnLastToken, propMapping, propValueMapping);
        }

        // Now after having deserialized all properties we can run the validations that needed to be
        // postponed until after all properties have been seen (e.g. Instance and Property validations for
        // primitive properties, since they may be composed from two properties `name` and `_name` in json
        // and should only be validated when both have been processed, even if megabytes apart in the json file).
        objectParsingState.RunDelayedValidation();

        // read past object, unless this is the last EndObject in the top-level Deserialize call
        if (!stayOnLastToken) reader.Read();

        // do not allow empty complex objects.
        if (empty) state.Errors.Add(ERR.OBJECTS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));

        // Only run instance validation when deserialization yielded no errors
        // to avoid spurious error messages.
        if (Settings.Validator is not null && kind != DeserializedObjectKind.FhirPrimitive)
        {
            var context = new PocoValidationContext(target, _inspector, state.Path.GetInstancePath, line,pos, Settings.NarrativeValidation);
            state.Errors.Add(Settings.Validator.ValidateObject(target, mapping, context));
        }
    }
    
    private void deserializePropertyInto<T>(
        T target,
        string propertyName,
        ref Utf8JsonReader reader,
        PocoDeserializerState state,
        ObjectParsingState delayedValidations,
        bool stayOnLastToken = false,
        PropertyMapping? propertyMapping = null,
        ClassMapping? propertyValueSuggestion = null,
        bool forceDelayedValidation = false) where T : Base
    {
        object? result;
        var (line, pos) = reader.CurrentState.GetLocation();
        var (name, propertyValueMapping) = tryDetectNameAndMapping(propertyName, propertyMapping, propertyValueSuggestion);

        target.TryGetValue(name, out var existingValue);
        
        try
        {
            state.Path.EnterElement(name, reader.TokenType == JsonTokenType.StartArray ? 0 : null, propertyValueMapping?.IsPrimitive ?? isOnJsonPrimitiveType(ref reader));
            
            (result, propertyMapping) = deserializeJsonValue(existingValue, propertyName, ref reader, state, delayedValidations, propertyValueMapping, propertyMapping);
        }
        finally
        {
            state.Path.ExitElement();
        }
        
        target.SetValue(propertyMapping is null ? propertyName : name, result);
        
        // Only do validation when no parse errors were encountered, otherwise we'll just
        // produce spurious messages.
        if (Settings.Validator is not null)
        {
            var deserializationContext = new PocoValidationContext(
                target,
                _inspector,
                state.Path.GetInstancePath,
                line, pos,
                Settings.NarrativeValidation
            );

            // If this is a FhirPrimitive, make sure we delay validation until we had the
            // chance to encounter both the `name` and `_name` property.
            if (propertyValueMapping?.IsFhirPrimitive is true || forceDelayedValidation)
            {
                var elementName = propertyMapping?.Name ?? name;

                delayedValidations.ScheduleDelayedValidation(
                    elementName + PROPERTY_VALIDATION_KEY_SUFFIX,
                    () =>
                    {
                        state.Path.EnterElement(elementName, null,
                            propertyValueMapping?.IsPrimitive ?? true);
                        state.Errors.Add(Settings.Validator.ValidateProperty(elementName, result, propertyMapping,
                            deserializationContext));
                        state.Path.ExitElement();
                    });
            }
            else
                state.Errors.Add(Settings.Validator.ValidateProperty(propertyName, result, propertyMapping,
                    deserializationContext));
        }
        
        (string name, ClassMapping? propertyValueMapping) tryDetectNameAndMapping(string pn, PropertyMapping? pm, ClassMapping? pvs)
        {
            // nothing to guess, we have information already
            if (pm is not null && pvs is not null)
                return (pm.Name, pvs);

            return (pn, pvs);
            // var span = propertyName.AsSpan();
            // for(int i = 0; i < span.Length; i++)
            // {
            //     if (!char.IsUpper(span[i])) 
            //         continue;
            //
            //     var subSpan = span.Slice(i);
            //     if (subSpan.IsEmpty)
            //         break;
            //     
            //     var choiceMapping = _inspector.FindClassMapping(subSpan.ToString());
            //     if (choiceMapping is not null)
            //         return (span[..i].ToString(), choiceMapping);
            // }
            // return (propertyName, null);
        }
    }

    private (object?, PropertyMapping?) deserializeJsonValue(object? existingValue, string propertyName, ref Utf8JsonReader reader, PocoDeserializerState state, ObjectParsingState parsingState, ClassMapping? propertyValueSuggestion = null, PropertyMapping? propertyMapping = null)
    {
        object? result = null;
        bool hasErrorData = false;

        var propertyValueMapping = propertyValueSuggestion;

        if (isOnFhirPrimitiveType(ref reader, propertyName) || isOnFhirPrimitiveObject(ref reader, propertyName, propertyMapping))
        {
            var inferType = _inspector.FindClassMapping(getFhirTypeForToken(reader.TokenType))!;
            
            // entering primitive, se ensure valuemapping is primitive as well
            if (propertyValueMapping?.IsFhirPrimitive is false)
                propertyValueMapping = inferType;
            
            propertyValueMapping ??= inferType;

            result = DeserializeFhirPrimitive(existingValue as PrimitiveType, propertyName, propertyValueMapping, ref reader, parsingState, state);
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            propertyValueMapping ??= ClassMapping.DynamicDataType;         
            
            if(existingValue is Base)
                state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
            
            result = deserializeSingleValue(ref reader, propertyValueMapping, state);
        }
        else if (reader.TokenType == JsonTokenType.StartArray)
        {
            if (propertyValueMapping is null)
            {
                var peeked = peekTypeNested(ref reader);

                var primitiveType = getFhirTypeForToken(peeked);

                propertyValueMapping = _inspector.FindClassMapping(primitiveType)!;
            }
            
            ClassMapping? listFactory = null;
            if (propertyMapping?.ImplementingType is { } t)
                listFactory = _inspector.FindClassMapping(t);

            listFactory ??= propertyValueMapping;
            
            IList primitiveList;
            if (existingValue is not IList list)
            {
                primitiveList = listFactory.ListFactory();
                if (existingValue is not null)
                    primitiveList.Add(existingValue);
            }
            else 
                primitiveList = list;

            if (propertyValueMapping.IsFhirPrimitive)
            {
                result = deserializeFhirPrimitiveList(primitiveList, propertyName, propertyValueMapping, ref reader, parsingState, state);
            }
            else
            {
                result = deserializeNormalList(primitiveList, propertyValueMapping, ref reader, state);
            }
        }
        else if (isOnJsonPrimitiveType(ref reader) && propertyName[0] == '_')
        {
            hasErrorData = true;
            
            state.Errors.Add(ERR.USE_OF_UNDERSCORE_ILLEGAL(ref reader, state.Path.GetInstancePath(), propertyMapping?.Name ?? propertyName.Substring(1), propertyName));
            
            var (value, err) = DeserializePrimitiveValue(ref reader, null, state.Path);
            state.Errors.Add(err);

            PrimitiveType primitive = value switch
            {
                int i => new Integer(i),
                bool b => new FhirBoolean(b),
                decimal d => new FhirDecimal(d),
                string s => new FhirString(s),
                _ => new DynamicPrimitive { ObjectValue = value }
            };

            result = primitive;
        }
        else
        {
            // should never get here
            reader.Recover();
        }

        if (hasErrorData)
            return (result, null);
        
        return (result, propertyMapping);
    }

    private static JsonTokenType peekTypeNested(ref Utf8JsonReader reader)
    {
        var peekCopy = reader;
        while (peekCopy.TokenType is JsonTokenType.StartArray or JsonTokenType.Null)
        {
            peekCopy.Read();
        }

        return peekCopy.TokenType;
    }
    
    private static Type getFhirTypeForToken(JsonTokenType tokenType)
    {
        return tokenType switch
        {
            JsonTokenType.String => typeof(FhirString),
            JsonTokenType.Number => typeof(FhirDecimal),
            JsonTokenType.True or JsonTokenType.False => typeof(FhirBoolean),
            JsonTokenType.Null => typeof(DynamicPrimitive),
            _ => typeof(DynamicDataType),
        };
    }

    private static bool isOnJsonPrimitiveType(ref Utf8JsonReader reader)
    {
        return reader.TokenType is JsonTokenType.Null
            or JsonTokenType.String or JsonTokenType.Number 
            or JsonTokenType.False or JsonTokenType.True;
    }
    private static bool isOnFhirPrimitiveType(ref Utf8JsonReader reader, string propertyName)
    {
        return isOnJsonPrimitiveType(ref reader) && propertyName[0] != '_';
    }
    
    private static bool isOnFhirPrimitiveObject(ref Utf8JsonReader reader, string propertyName, PropertyMapping? mapping)
    {
        return propertyName[0] == '_' && reader.TokenType == JsonTokenType.StartObject && mapping is not null;
    }

    /// <summary>
    /// Reads the content of a list with non-FHIR-primitive content (so, no name/_name pairs to be dealt with). Note
    /// that the contents can only be complex in the current FHIR serialization, but we'll be prepared and handle
    /// other situations (e.g. repeating Extension.url's, if they would ever exist).
    /// </summary>
    private IList deserializeNormalList(
        IList existingList,
        ClassMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        PocoDeserializerState state,
        bool nestedArray = false)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new InvalidOperationException($"deserializeNormalList should only be called on JSON array: " +
                                                $"Current token is {reader.TokenType}.");

        bool hasUnexpectedElements = false;
        
        // Read past start of array
        reader.Read();

        if (reader.TokenType == JsonTokenType.EndArray)
            state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));
        
        if (existingList.Count > 0)
        {
            state.Path.IncrementIndex(existingList.Count);
            if(nestedArray)
                state.Errors.Add(ERR.NESTED_ARRAY(ref reader, state.Path.GetInstancePath()));
            else
                state.Errors.Add(ERR.DUPLICATE_ARRAY(ref reader, state.Path.GetInstancePath()));
        }


        // Can't make an iterator because of the ref readers struct, so need
        // to simply create a list by Adding(). Not the fastest approach :-(
        while (reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var result = deserializeSingleValue(ref reader, propertyValueMapping, state);
                existingList.Add(result);
            }
            else if(reader.TokenType == JsonTokenType.Null)
            {
                existingList.Add(null);
                reader.Read();
            }
            else if(reader.TokenType == JsonTokenType.StartArray)
            {
                _ = deserializeNormalList(existingList, propertyValueMapping, ref reader, state, true);
            }
            else
            {
                var elem = (Base)propertyValueMapping.Factory();
                deserializePropertyInto(elem, "value", ref reader, state, new(), forceDelayedValidation: true);
                existingList.Add(elem);
                hasUnexpectedElements = true;
            }

            state.Path.IncrementIndex();
        }

        // Read past end of array
        reader.Read();
        
        // encountered invalid data, convert whole list to List<DynamicDataType>
        if(hasUnexpectedElements)
            return existingList.ToDynamicDataType();

        return existingList;
    }

    internal class ObjectParsingState
    {
        private readonly Dictionary<string, Action> _validations = new();
        private readonly Dictionary<string, int> _parsedPropValue = new();

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
        ClassMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        ObjectParsingState delayedValidations,
        PocoDeserializerState state,
        bool nestedArray = false
    )
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new InvalidOperationException($"deserializeFhirPrimitiveList should only be called on JSON array: " +
                                                $"Current token is {reader.TokenType}.");
        
        // read into array
        reader.Read();

        if (reader.TokenType == JsonTokenType.EndArray)
            state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));

        int originalSize = existingList.Count;

        // Can't make an iterator because of the ref readers struct, so need
        // to simply create a list by Adding(). Not the fastest approach :-(
        bool? onlyNulls = null;
        int elementIndex = delayedValidations.GetPropertyIndex(propertyName);
        if (elementIndex > 0)
        {
            state.Path.IncrementIndex(elementIndex);
            if(nestedArray)
                state.Errors.Add(ERR.NESTED_ARRAY(ref reader, state.Path.GetInstancePath()));
            else
                state.Errors.Add(ERR.DUPLICATE_ARRAY(ref reader, state.Path.GetInstancePath()));
        }

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            if (elementIndex >= originalSize)
                existingList.Add(null);

            if (reader.TokenType == JsonTokenType.Null)
            {
                onlyNulls ??= true;

                // don't read any new data into the primitive here
                reader.Read();
            }
            else if(reader.TokenType == JsonTokenType.StartArray)
            {
                onlyNulls = false;
                delayedValidations.SetPropertyIndex(propertyName, existingList.Count - 1);
                
                _ = deserializeFhirPrimitiveList(existingList, propertyName, propertyValueMapping, ref reader, delayedValidations, state, true);
            }
            else
            {
                existingList[elementIndex] ??= propertyValueMapping.Factory();
                onlyNulls = false;
                _ = DeserializeFhirPrimitive((PrimitiveType)existingList[elementIndex]!, propertyName, propertyValueMapping, ref reader, delayedValidations, state);

                delayedValidations.SetPropertyIndex(propertyName, existingList.Count);
            }
            
            elementIndex += 1;
            state.Path.IncrementIndex();
        }
            
        if(onlyNulls is true)
            state.Errors.Add(ERR.PRIMITIVE_ARRAYS_ONLY_NULL(ref reader, state.Path.GetInstancePath()));

        //[EK 20221027] - According to the new R5 spec, these arrays need not be of the same size, and
        //we need to fill out missing elements with null values.
        //if (originalSize > 0 && elementIndex != originalSize)
        //    state.Errors.Add(ERR.PRIMITIVE_ARRAYS_INCOMPAT_SIZE.With(ref reader));

        // read past array to next property or end of object
        reader.Read();

        return existingList;
    }

    /// <summary>
    /// Deserializes a FHIR primitive, which can be a name (with a primitive token) or
    /// _name property (followed by the an object).
    /// </summary>
    /// <remarks>Caller should handle situations where the first token is not either a primitive token or
    /// an object as described in the summary. Upon completion, reader will be located at the next token
    /// after the FHIR primitive.</remarks>
    internal PrimitiveType DeserializeFhirPrimitive(
        PrimitiveType? existingPrimitive,
        string propertyName,
        ClassMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        ObjectParsingState? parsingState,
        PocoDeserializerState state
    )
    {
        var targetPrimitive = existingPrimitive ?? (PrimitiveType)propertyValueMapping.Factory();
        var (line, pos) = reader.CurrentState.GetLocation();

        if (isOnJsonPrimitiveType(ref reader))
        {
            // "name" situation, with a primitive token.
            // DynamicPrimitive doesn't have PrimitiveValueProperty, but it's also not 100% necessary
            var primitiveImplementingType = propertyValueMapping.PrimitiveValueProperty?.ImplementingType;

            try
            {
                state.Path.EnterElement("value", 0, true);

                var (result, error) = DeserializePrimitiveValue(ref reader, primitiveImplementingType, state.Path);
                state.Errors.Add(error);

                if (targetPrimitive.ObjectValue is not null)
                    state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
                else
                    // Set the value, validation is done in the ObjectValidation of the PrimitiveType's.
                    targetPrimitive.ObjectValue = result;
            }
            finally
            {
                state.Path.ExitElement();
            }
        }
        else if(reader.TokenType == JsonTokenType.StartObject)
        {
            // The complex part of a primitive - read the object's primitives into the target
            if (targetPrimitive.Extension.Any() ||
                targetPrimitive.ElementId is not null)
            {
                state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
            }

            deserializeObjectInto(targetPrimitive, propertyValueMapping, ref reader, DeserializedObjectKind.FhirPrimitive, state, stayOnLastToken: false);
        }
        else
        {
            throw new InvalidOperationException("Function should be called with a `name` with a primitive or a `_name` with an object.");
        }        

        // Only do validation on this instance when no parse errors were encountered, otherwise we'll just
        // produce spurious messages. Also, delay validation of this instance until we have processed both
        // the `name` and `_name` property.
        if (Settings.Validator is not null)
        {
            var context = new PocoValidationContext(targetPrimitive, _inspector, state.Path.GetInstancePath, line, pos, Settings.NarrativeValidation);
            var elementName = propertyName;
            if (parsingState is null)
            {
                state.Errors.Add(Settings.Validator.ValidateObject(targetPrimitive, propertyValueMapping, context));
            }
            else
            {
                var trimName = elementName.TrimStart('_');
                parsingState.ScheduleDelayedValidation(
                    trimName + INSTANCE_VALIDATION_KEY_SUFFIX,
                    () =>
                    {
                        state.Path.EnterElement(trimName, null,
                            propertyValueMapping.IsPrimitive);
                        state.Errors.Add(Settings.Validator.ValidateObject(targetPrimitive, propertyValueMapping, context));
                        state.Path.ExitElement();
                    });
            }
        }

        return targetPrimitive;
    }

    /// <summary>
    /// Deserializes a single object, either a resource, a FHIR primitive or a primitive value.
    /// </summary>
    /// <remarks>Upon completion, reader will be located at the next token afther the value.</remarks>
    private object? deserializeSingleValue(ref Utf8JsonReader reader, ClassMapping propertyValueMapping, PocoDeserializerState state)
    {
        // Resources
        if (propertyValueMapping.IsResource)
        {
            return DeserializeResourceInternal(ref reader, state, stayOnLastToken: false);
        }

        // "normal" complex types & backbones
        var newComplex = (Base)propertyValueMapping.Factory();
        deserializeObjectInto(newComplex, propertyValueMapping, ref reader, DeserializedObjectKind.Complex, state, stayOnLastToken: false);
        return newComplex;
    }

    /// <summary>
    /// Does a best-effort parse of the data available at the reader, given the required type of the property the
    /// data needs to be read into.
    /// </summary>
    /// <returns>A value without an error if the data could be parsed to the required type, and a value with an error if the
    /// value could not be parsed - in which case the value returned is the raw value coming in from the reader.</returns>
    /// <remarks>Upon completion, the reader will be positioned on the token after the primitive.</remarks>
    internal (object?, ERR?) DeserializePrimitiveValue(ref Utf8JsonReader reader,  Type? valuePropertyType, PathStack pathStack)
    {
        (object? partial, ERR? error) result = reader.TokenType switch
        {
            JsonTokenType.Null => (null, ERR.EXPECTED_PRIMITIVE_NOT_NULL(ref reader, pathStack.GetInstancePath())),
            JsonTokenType.String => (reader.GetString(), null),
            JsonTokenType.Number => (tryGetMatchingNumber(ref reader, valuePropertyType), null),
            JsonTokenType.True or JsonTokenType.False => (reader.GetBoolean(), null),

            _ =>
                // This would be an internal logic error, since our callers should have made sure we're
                // on the primitive value after the property name (and the Utf8JsonReader would have complained about any
                // other token that one that is a value).
                // EK: I think 'Comment' is the only possible non-expected option here....
                throw new InvalidOperationException($"Unexpected token type {reader.TokenType} while parsing a primitive value. " +
                                                    reader.GenerateLocationMessage()),
        };

        // Read past the value
        reader.Read();

        return result;
    }

    /// <summary>
    /// This function tries to map from the json-format "generic" number to the kind of numeric type defined in the POCO.
    /// </summary>
    /// <remarks>Reader must be positioned on a number token. This function will not move the reader to the next token.</remarks>
    private static object tryGetMatchingNumber(ref Utf8JsonReader reader, Type? implementingType)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new InvalidOperationException($"Cannot read a numeric when reader is on a {reader.TokenType}. " +
                                                reader.GenerateLocationMessage());

        // Decimal and integer are the only two types in FHIR where we are using Json native numbers
        if (implementingType == typeof(decimal) && reader.TryGetDecimal(out decimal dec))
            return dec;
        if (implementingType == typeof(int) && reader.TryGetInt32(out int i32))
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
    /// Returns the <see cref="ClassMapping" /> for the object to be deserialized using the `resourceType` property.
    /// </summary>
    /// <remarks>Assumes the reader is on the start of an object.</remarks>
    internal static (ClassMapping?, FhirJsonException?, string? resourceType) DetermineClassMappingFromInstance(ref Utf8JsonReader reader, ModelInspector inspector, PathStack path)
    {
        var (resourceType, error) = determineResourceType(ref reader);

        ClassMapping? resourceMapping = null;
        if (resourceType is not null)
            resourceMapping = inspector.FindClassMapping(resourceType);

        // fall back to DynamicResource, if we can't find the resource requested
        // or if the resource type is not a resource to start with
        if (resourceMapping is null || resourceMapping.IsResource != true)
        {
            // also report error
            if (resourceMapping is not null)
                error = ERR.RESOURCE_TYPE_NOT_A_RESOURCE(ref reader, path.GetInstancePath(), resourceMapping.Name);
            else if (resourceType is not null)
                error = ERR.UNKNOWN_RESOURCE_TYPE(ref reader, path.GetInstancePath(), resourceType);

            resourceMapping = ClassMapping.DynamicResource;
        }
        
        return (resourceMapping, error, resourceType);
    }

    private static (string?, FhirJsonException?) determineResourceType(ref Utf8JsonReader reader)
    {
        //TODO: determineResourceType probably won't work with streaming inputs to Utf8JsonReader

        var originalReader = reader;    // copy the struct so we can "rewind"
        var atDepth = reader.CurrentDepth + 1;

        try
        {
            while (reader.Read() && reader.CurrentDepth >= atDepth)
            {
                if (reader.TokenType == JsonTokenType.PropertyName && reader.CurrentDepth == atDepth)
                {
                    var propName = reader.GetString();

                    if (propName == "resourceType")
                    {
                        reader.Read();
                        return (reader.TokenType == JsonTokenType.String) ?
                            new(reader.GetString()!, null) :
                            new(null, ERR.RESOURCETYPE_SHOULD_BE_STRING(ref reader, "", reader.TokenType));
                    }
                }
            }

            return new(null, ERR.NO_RESOURCETYPE_PROPERTY(ref reader, ""));
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
    private static (PropertyMapping? propMapping, ClassMapping? propValueMapping) tryGetMappedElementMetadata(
        ModelInspector inspector,
        ClassMapping parentMapping,
        string elementName,
        PocoDeserializerState state,
        ref Utf8JsonReader reader
        )
    {
        var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                              ?? parentMapping.FindMappedElementByChoiceName(elementName);

        // handled by the unknown type deserialization
        if (propertyMapping is null)
            return (null, null);

        ClassMapping propertyValueMapping = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ?? throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which no mapping was found in the model assemblies."),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(ref reader),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported")
        };

        return (propertyMapping, propertyValueMapping);

        ClassMapping getChoiceClassMapping(ref Utf8JsonReader r)
        {
            ClassMapping? choiceMapping;
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            if (!string.IsNullOrEmpty(typeSuffix))
            {
                choiceMapping = inspector.FindClassMapping(typeSuffix);
                if (choiceMapping is null)
                {
                    state.Errors.Add(ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE(ref r, state.Path.GetInstancePath(), propertyMapping.Name, typeSuffix));
                    choiceMapping = ClassMapping.DynamicDataType;
                }
            }
            else
            {
                state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(ref r, elementName, state.Path.GetInstancePath()));
                choiceMapping = ClassMapping.DynamicDataType;
            }

            return choiceMapping;
        }
    }
}

[Obsolete("Use BaseFhirJsonDeserializer instead.")]
public class BaseFhirJsonPocoDeserializer : BaseFhirJsonDeserializer
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
    [Obsolete("Use the constructor that takes a ModelInspector instead. " +
              "You can find the right ModelInspector for an assembly by calling ModelInspector.ForAssembly(assembly).")]
    public BaseFhirJsonPocoDeserializer(Assembly assembly) : this(ModelInspector.ForAssembly(assembly),
        new FhirJsonConverterOptions())
    {
        // Nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirJsonPocoDeserializer(ModelInspector inspector) : this(inspector, new FhirJsonConverterOptions())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirJsonPocoDeserializer(ModelInspector inspector, FhirJsonConverterOptions settings)
        : base(inspector, settings)
    {
        // nothing
    }
}