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

public class BaseFhirJsonPocoDeserializer : BaseFhirJsonParser
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


/// <summary>
/// Deserializes Json into FHIR POCO objects.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/json.html. </remarks>
public class BaseFhirJsonParser
{
    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    public BaseFhirJsonParser(ModelInspector inspector) : this(inspector, new ParserSettings())
    {
        // nothing
    }

    /// <summary>
    /// Initializes an instance of the deserializer.
    /// </summary>
    /// <param name="inspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="settings">A settings object to be used by this instance.</param>
    public BaseFhirJsonParser(ModelInspector inspector, ParserSettings? settings)
    {
        Settings = settings ?? new ParserSettings();
        _inspector = inspector;
    }

    /// <summary>
    /// The settings that were passed to the constructor.
    /// </summary>
    public ParserSettings Settings { get; set; }

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

        FhirJsonPocoDeserializerState state = new();

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

        var state = new FhirJsonPocoDeserializerState();
        deserializeObjectInto(result, mapping, ref reader, DeserializedObjectKind.Complex, state, stayOnLastToken: true);

        instance = result;
        issues = Settings.ExceptionFilter is { } filter
            ? state.Errors.Remove(filter)
            : state.Errors;

        return !issues.Any();
    }

    internal Resource? DeserializeResourceInternal(ref Utf8JsonReader reader, FhirJsonPocoDeserializerState state, bool stayOnLastToken)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            state.Errors.Add(ERR.EXPECTED_START_OF_OBJECT(ref reader, state.Path.GetInstancePath(), reader.TokenType));
            reader.Recover();  // skip to the end of the construct encountered (value or array)
            return null;
        }

        (ClassMapping? resourceMapping, FhirJsonException? error) = DetermineClassMappingFromInstance(ref reader, _inspector, state.Path);

        if (resourceMapping is not null)
        {
            // If we have at least a mapping, let's try to continue
            var newResource = (Base)resourceMapping.Factory();

            try
            {
                state.Path.EnterResource(resourceMapping.Name);
                int nErrorCount = state.Errors.Count;
                deserializeObjectInto(newResource, resourceMapping, ref reader, DeserializedObjectKind.Resource, state, stayOnLastToken);

                if (!resourceMapping.IsResource)
                {
                    state.Errors.Add(ERR.RESOURCE_TYPE_NOT_A_RESOURCE(ref reader, state.Path.GetInstancePath(), resourceMapping.Name));
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
            state.Errors.Add(error!);

            // Read past the end of this object to recover.
            reader.Recover();

            return null;
        }
    }

    /// <summary>
    /// The kind of object we need to deserialize into, which will influence subtly
    /// how the <see cref="deserializeObjectInto{T}(T, ClassMapping, ref Utf8JsonReader, DeserializedObjectKind, FhirJsonPocoDeserializerState, bool)" />
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
        FhirJsonPocoDeserializerState state,
        bool stayOnLastToken = false) where T : Base
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            state.Errors.Add(ERR.EXPECTED_START_OF_OBJECT(ref reader, state.Path.GetInstancePath(), reader.TokenType));
            reader.Recover();  // skip to the end of the construct encountered (value or array)
            return;
        }

        // read past start of object into first property or end of object
        reader.Read();

        var empty = true;
        var objectParsingState = new ObjectParsingState();
        var oldErrorCount = state.Errors.Count;
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
            var (propMapping, propValueMapping, error) = tryGetMappedElementMetadata(_inspector, mapping, ref reader, state.Path, currentPropertyName);

            if (error is not null)
            {
                state.Errors.Add(error);

                // try to recover by skipping to the next property.
                reader.SkipTo(JsonTokenType.PropertyName);
            }
            else
            {
                // read past the property name into the value
                reader.Read();

                try
                {
                    state.Path.EnterElement(propMapping!.Name, !propMapping.IsCollection ? null : 0, propMapping.IsPrimitive);
                    deserializePropertyValueInto(target, currentPropertyName, propMapping, propValueMapping!, ref reader, objectParsingState, state);
                }
                finally
                {
                    state.Path.ExitElement();
                }
            }
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
        if (Settings.Validator is not null && kind != DeserializedObjectKind.FhirPrimitive && (Settings.ValidateOnFailedParse || state.Errors.Count == oldErrorCount))
        {
            var context = new InstanceDeserializationContext(state.Path, line, pos, mapping, Settings.NarrativeValidation);
            PocoDeserializationHelper.RunInstanceValidation(target, Settings.Validator, context, state.Errors);
        }
    }

    /// <summary>
    /// Reads the value of a json property.
    /// </summary>
    /// <param name="target">The target POCO which property will be set/updated during deserialization. If null, it will be
    /// be created based on the <paramref name="propertyMapping"/>, otherwise it will be updated.</param>
    /// <param name="propertyName">The literal name of the property in the json serialization.</param>
    /// <param name="propertyMapping">The cached metadata for the property we are setting.</param>
    /// <param name="propertyValueMapping">The cached metadata for the type of value we are setting the property to.</param>
    /// <param name="reader">The reader to deserialize from.</param>
    /// <param name="delayedValidations">Validations to be delayed until the target has been fully deserialized.
    /// This function will add to this list if necessary.</param>
    /// <param name="state">Object used to track all parsing state.</param>
    ///
    /// <remarks>Expects the reader to be positioned on the property value.
    /// Reader will be on the first token after the property value upon return.</remarks>
    private void deserializePropertyValueInto(
        Base target,
        string propertyName,
        PropertyMapping propertyMapping,
        ClassMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        ObjectParsingState delayedValidations,
        FhirJsonPocoDeserializerState state
    )
    {
        object? result;
        var oldErrorCount = state.Errors.Count;
        var (line, pos) = reader.CurrentState.GetLocation();

        // There might be an existing value, since FhirPrimitives may be spread out over two properties
        // (one with, and one without the '_')
        var existingValue = propertyMapping.GetValue(target);

        if (propertyValueMapping.IsFhirPrimitive)
        {
            // fix for https://github.com/FirelyTeam/firely-net-sdk/issues/2701 - use the known native type if it is in the list
            var fhirType = propertyMapping.FhirType.Contains(propertyValueMapping.NativeType)
                ? propertyValueMapping.NativeType
                : propertyMapping.FhirType.FirstOrDefault();

            // Note that the POCO model will always allocate a new list if the property had not been set before,
            // so there is always an existingValue for IList
            result = propertyMapping.IsCollection ?
                deserializeFhirPrimitiveList((IList)existingValue!, propertyName, propertyValueMapping, fhirType, ref reader, delayedValidations, state) :
                DeserializeFhirPrimitive(existingValue as PrimitiveType, propertyName, propertyValueMapping, fhirType, ref reader, delayedValidations, state);
        }
        else
        {
            // This is not a FHIR primitive, so we should not be dealing with these weird _name members.
            if (propertyName[0] == '_')
                state.Errors.Add(ERR.USE_OF_UNDERSCORE_ILLEGAL(ref reader, state.Path.GetInstancePath(), propertyMapping.Name, propertyName));

            // Note that repeating simple elements (like Extension.url) do not currently exist in the FHIR serialization
            if (propertyMapping.IsCollection)
            {
                var l = (IList)existingValue!;
                // if the list is already populated, a property with an identical key was encountered earlier
                if (l.Count > 0)
                {
                    state.Path.IncrementIndex(l.Count);
                    state.Errors.Add(ERR.DUPLICATE_ARRAY(ref reader, state.Path.GetInstancePath()));
                }
                result = deserializeNormalList(l, propertyValueMapping, ref reader, state);
            }
            else
            {
                // if the property already has a value, its key must have been encountered before
                if (existingValue is not null)
                {
                    state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
                }
                result = deserializeSingleValue(ref reader, propertyValueMapping, state);
            }
        }

        // Only do validation when no parse errors were encountered, otherwise we'll just
        // produce spurious messages.
        if (Settings.Validator is not null && (Settings.ValidateOnFailedParse || oldErrorCount == state.Errors.Count))
        {
            var deserializationContext = new PropertyDeserializationContext(
                target,
                state.Path,
                propertyName,
                line, pos,
                propertyMapping,
                Settings.NarrativeValidation);

            // If this is a FhirPrimitive, make sure we delay validation until we had the
            // chance to encounter both the `name` and `_name` property.
            if (propertyValueMapping.IsFhirPrimitive)
            {
                var elementName = propertyMapping.Name;

                delayedValidations.ScheduleDelayedValidation(
                    elementName + PROPERTY_VALIDATION_KEY_SUFFIX,
                    () =>
                    {
                        deserializationContext.PathStack.EnterElement(elementName, null,
                            propertyValueMapping.IsPrimitive);
                        PocoDeserializationHelper.RunPropertyValidation(result, Settings.Validator!,
                            deserializationContext, state.Errors);
                        deserializationContext.PathStack.ExitElement();
                    });
            }
            else
                PocoDeserializationHelper.RunPropertyValidation(result, Settings.Validator!, deserializationContext, state.Errors);
        }

        propertyMapping.SetValue(target, result);
    }

    /// <summary>
    /// Reads the content of a list with non-FHIR-primitive content (so, no name/_name pairs to be dealt with). Note
    /// that the contents can only be complex in the current FHIR serialization, but we'll be prepared and handle
    /// other situations (e.g. repeating Extension.url's, if they would ever exist).
    /// </summary>
    private IList deserializeNormalList(
        IList? existingList,
        ClassMapping propertyValueMapping,
        ref Utf8JsonReader reader,
        FhirJsonPocoDeserializerState state)
    {
        // Create a list of the type of this property's value.
        IList listInstance = existingList ?? propertyValueMapping.ListFactory();

        // if true, we have encountered a single value where we expected an array.
        // we need to recover by creating an array with that single value.
        bool oneshot = false;

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            state.Errors.Add(ERR.EXPECTED_START_OF_ARRAY(ref reader, state.Path.GetInstancePath()));
            oneshot = true;
        }
        else
        {
            // Read past start of array
            reader.Read();

            if (reader.TokenType == JsonTokenType.EndArray)
                state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));
        }

        // Can't make an iterator because of the ref readers struct, so need
        // to simply create a list by Adding(). Not the fastest approach :-(
        while (reader.TokenType != JsonTokenType.EndArray)
        {
            var result = deserializeSingleValue(ref reader, propertyValueMapping, state);
            listInstance.Add(result);
            state.Path.IncrementIndex();

            if (oneshot) break;
        }

        // Read past end of array
        if (!oneshot) reader.Read();

        return listInstance;
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
        Type? fhirType,
        ref Utf8JsonReader reader,
        ObjectParsingState delayedValidations,
        FhirJsonPocoDeserializerState state
    )
    {
        // if true, we have encountered a single value where we expected an array.
        // we need to recover by creating an array with that single value.
        bool oneshot = false;

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            state.Errors.Add(ERR.EXPECTED_START_OF_ARRAY(ref reader, state.Path.GetInstancePath()));
            oneshot = true;
        }
        else
        {
            // read into array
            reader.Read();

            if (reader.TokenType == JsonTokenType.EndArray)
                state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));
        }

        int originalSize = existingList.Count;

        // Can't make an iterator because of the ref readers struct, so need
        // to simply create a list by Adding(). Not the fastest approach :-(
        bool? onlyNulls = null;
        int elementIndex = delayedValidations.GetPropertyIndex(propertyName);
        if (elementIndex > 0)
        {
            state.Path.IncrementIndex(elementIndex);
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
            else
            {
                existingList[elementIndex] ??= propertyValueMapping.Factory();
                onlyNulls = false;
                _ = DeserializeFhirPrimitive((PrimitiveType)existingList[elementIndex]!, propertyName, propertyValueMapping, fhirType, ref reader, delayedValidations, state);

                delayedValidations.SetPropertyIndex(propertyName, existingList.Count);
            }

            elementIndex += 1;
            state.Path.IncrementIndex();

            if (oneshot) break;
        }

        if (onlyNulls == true)
            state.Errors.Add(ERR.PRIMITIVE_ARRAYS_ONLY_NULL(ref reader, state.Path.GetInstancePath()));

        //[EK 20221027] - According to the new R5 spec, these arrays need not be of the same size, and
        //we need to fill out missing elements with null values.
        //if (originalSize > 0 && elementIndex != originalSize)
        //    state.Errors.Add(ERR.PRIMITIVE_ARRAYS_INCOMPAT_SIZE.With(ref reader));

        // read past array to next property or end of object
        if (!oneshot) reader.Read();

        return existingList;
    }

    /// <summary>
    /// Deserializes a FHIR primitive, which can be a name or _name property.
    /// </summary>
    /// <remarks>Upon completion, reader will be located at the next token after the FHIR primitive.</remarks>
    internal PrimitiveType DeserializeFhirPrimitive(
        PrimitiveType? existingPrimitive,
        string propertyName,
        ClassMapping propertyValueMapping,
        Type? fhirType,
        ref Utf8JsonReader reader,
        ObjectParsingState? parsingState,
        FhirJsonPocoDeserializerState state
    )
    {
        var targetPrimitive = existingPrimitive ?? (PrimitiveType)propertyValueMapping.Factory();
        var oldErrorCount = state.Errors.Count;
        var (line, pos) = reader.CurrentState.GetLocation();

        if (propertyName[0] != '_')
        {
            // No underscore, dealing with the 'value' property here.
            var primitiveValueProperty = propertyValueMapping.PrimitiveValueProperty ??
                                         throw new InvalidOperationException($"All subclasses of {nameof(PrimitiveType)} should have a property representing the value element, " +
                                                                             $"but {propertyValueMapping.Name} has not. " + reader.GenerateLocationMessage());

            try
            {
                state.Path.EnterElement("value", 0, true);

                var (result, error) = DeserializePrimitiveValue(ref reader, primitiveValueProperty.ImplementingType, state.Path);

                if (error is not null)
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
        else
        {
            // The complex part of a primitive - read the object's primitives into the target
            if (targetPrimitive.Extension.Any() ||
                targetPrimitive.ElementId is not null)
            {
                state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));
            }
            deserializeObjectInto(targetPrimitive, propertyValueMapping, ref reader, DeserializedObjectKind.FhirPrimitive, state, stayOnLastToken: false);
        }

        // Only do validation on this instance when no parse errors were encountered, otherwise we'll just
        // produce spurious messages. Also, delay validation of this instance until we have processed both
        // the `name` and `_name` property.
        if (Settings.Validator is not null && (Settings.ValidateOnFailedParse || oldErrorCount == state.Errors.Count))
        {
            var context = new InstanceDeserializationContext(state.Path, line, pos, propertyValueMapping, Settings.NarrativeValidation);
            if (parsingState is null)
                PocoDeserializationHelper.RunInstanceValidation(targetPrimitive, Settings.Validator, context, state.Errors);
            else
            {
                var elementName = state.Path.GetLastPart();
                parsingState.ScheduleDelayedValidation(
                    elementName + INSTANCE_VALIDATION_KEY_SUFFIX,
                    () =>
                    {
                        context.PathStack.EnterElement(elementName, null,
                            propertyValueMapping.IsPrimitive);
                        PocoDeserializationHelper.RunInstanceValidation(targetPrimitive, Settings.Validator, context,
                            state.Errors);
                        context.PathStack.ExitElement();
                    });
            }
        }

        return targetPrimitive;
    }

    /// <summary>
    /// Deserializes a single object, either a resource, a FHIR primitive or a primitive value.
    /// </summary>
    /// <remarks>Upon completion, reader will be located at the next token afther the value.</remarks>
    private object? deserializeSingleValue(ref Utf8JsonReader reader, ClassMapping propertyValueMapping, FhirJsonPocoDeserializerState state)
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
    internal (object?, FhirJsonException?) DeserializePrimitiveValue(ref Utf8JsonReader reader, Type valuePropertyType,
        PathStack pathStack)
    {
        // Check for unexpected non-value types.
        if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.StartArray)
        {
            var exception = reader.TokenType == JsonTokenType.StartObject
                ? ERR.EXPECTED_PRIMITIVE_NOT_OBJECT(ref reader, pathStack.GetInstancePath())
                : ERR.EXPECTED_PRIMITIVE_NOT_ARRAY(ref reader, pathStack.GetInstancePath());
            reader.Recover();
            return (null, exception);
        }

        // Check for value types
        (object? partial, ERR? error) result = reader.TokenType switch
        {
            JsonTokenType.Null => (null, ERR.EXPECTED_PRIMITIVE_NOT_NULL(ref reader, pathStack.GetInstancePath())),
            JsonTokenType.String when string.IsNullOrWhiteSpace(reader.GetString()) => (reader.GetString(), ERR.PROPERTY_MAY_NOT_BE_EMPTY(ref reader, pathStack.GetInstancePath())),
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
    private static object tryGetMatchingNumber(ref Utf8JsonReader reader, Type implementingType)
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
    internal static (ClassMapping?, FhirJsonException?) DetermineClassMappingFromInstance(ref Utf8JsonReader reader, ModelInspector inspector, PathStack path)
    {
        var (resourceType, error) = determineResourceType(ref reader);

        if (resourceType is null) return (null, error);

        var resourceMapping = inspector.FindClassMapping(resourceType);

        return resourceMapping is not null ?
            (new(resourceMapping, null)) :
            (new(null, ERR.UNKNOWN_RESOURCE_TYPE(ref reader, path.GetInstancePath(), resourceType)));
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
    private static (PropertyMapping? propMapping, ClassMapping? propValueMapping, FhirJsonException? error) tryGetMappedElementMetadata(
        ModelInspector inspector,
        ClassMapping parentMapping,
        ref Utf8JsonReader reader,
        PathStack path,
        string propertyName)
    {
        bool startsWithUnderscore = propertyName[0] == '_';
        var elementName = startsWithUnderscore ? propertyName.Substring(1) : propertyName;

        var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                              ?? parentMapping.FindMappedElementByChoiceName(elementName);

        if (propertyMapping is null)
            return (null, null, ERR.UNKNOWN_PROPERTY_FOUND(ref reader, path.GetInstancePath(), propertyName));

        (ClassMapping? propertyValueMapping, FhirJsonException? error) = propertyMapping.Choice switch
        {
            ChoiceType.None or ChoiceType.ResourceChoice =>
                inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) is { } m
                    ? (m, null)
                    : throw new InvalidOperationException($"Encountered property type {propertyMapping.ImplementingType} for which no mapping was found in the model assemblies. " + reader.GenerateLocationMessage()),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(ref reader),
            _ => throw new NotImplementedException("Unknown choice type in property mapping. " + reader.GenerateLocationMessage())
        };

        return (propertyMapping, propertyValueMapping, error);

        (ClassMapping?, FhirJsonException?) getChoiceClassMapping(ref Utf8JsonReader r)
        {
            string typeSuffix = elementName[propertyMapping.Name.Length..];

            return string.IsNullOrEmpty(typeSuffix)
                ? (null, ERR.CHOICE_ELEMENT_HAS_NO_TYPE(ref r, path.GetInstancePath(), propertyMapping.Name))
                : inspector.FindClassMapping(typeSuffix) is { } cm
                    ? (cm, null)
                    : (null, ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE(ref r, path.GetInstancePath(), propertyMapping.Name, typeSuffix));
        }
    }
}

internal class FhirJsonPocoDeserializerState
{
    public readonly ExceptionAggregator Errors = new();
    public readonly PathStack Path = new();
}