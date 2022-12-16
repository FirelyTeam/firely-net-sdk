/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Deserializes a byte stream into FHIR POCO objects.
    /// </summary>
    /// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/json.html. </remarks>
    public class FhirJsonPocoDeserializer
    {
        /// <summary>
        /// Initializes an instance of the deserializer.
        /// </summary>
        /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
        public FhirJsonPocoDeserializer(Assembly assembly) : this(assembly, new())
        {
            // nothing
        }

        /// <summary>
        /// Initializes an instance of the deserializer.
        /// </summary>
        /// <param name="assembly">Assembly containing the POCO classes to be used for deserialization.</param>
        /// <param name="settings">A settings object to be used by this instance.</param>
        public FhirJsonPocoDeserializer(Assembly assembly, FhirJsonPocoDeserializerSettings settings)
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            Settings = settings;
            _inspector = ModelInspector.ForAssembly(assembly);
        }

        /// <summary>
        /// Assembly containing the POCO classes the deserializer will use to deserialize data into.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// The settings that were passed to the constructor.
        /// </summary>
        public FhirJsonPocoDeserializerSettings Settings { get; }

        private const string INSTANCE_VALIDATION_KEY_SUFFIX = ":instance";
        private const string PROPERTY_VALIDATION_KEY_SUFFIX = ":property";
        private readonly ModelInspector _inspector;

        /// <summary>
        /// Deserialize the FHIR Json from the reader and create a new POCO object containing the data from the reader.
        /// </summary>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public Resource DeserializeResource(ref Utf8JsonReader reader)
        {
            if (reader.CurrentState.Options.CommentHandling is not JsonCommentHandling.Skip and not JsonCommentHandling.Disallow)
                throw new InvalidOperationException("The reader must be set to ignore or refuse comments.");

            // If the stream has just been opened, move to the first token.
            if (reader.TokenType == JsonTokenType.None) reader.Read();

            FhirJsonPocoDeserializerState state = new();

            var result = DeserializeResourceInternal(ref reader, state, stayOnLastToken: true);

            return !state.Errors.HasExceptions
                ? result!
                : throw new DeserializationFailedException(result, state.Errors);
        }

        /// <summary>
        /// Reads a (subtree) of serialzed FHIR Json data into a POCO object.
        /// </summary>
        /// <param name="targetType">The type of POCO to construct and deserialize</param>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public Base DeserializeObject(Type targetType, ref Utf8JsonReader reader)
        {
            if (reader.CurrentState.Options.CommentHandling is not JsonCommentHandling.Skip and not JsonCommentHandling.Disallow)
                throw new InvalidOperationException("The reader must be set to ignore or refuse comments.");

            // If the stream has just been opened, move to the first token.
            if (reader.TokenType == JsonTokenType.None) reader.Read();

            var mapping = _inspector.FindOrImportClassMapping(targetType) ??
                throw new ArgumentException($"Type '{targetType}' could not be located in model assembly '{Assembly}' and can " +
                    $"therefore not be used for deserialization. " + reader.GenerateLocationMessage(), nameof(targetType));

            // Create a new instance of the object to read the members into.
            if (mapping.Factory() is Base result)
            {
                var state = new FhirJsonPocoDeserializerState();
                deserializeObjectInto(result, mapping, ref reader, DeserializedObjectKind.Complex, state, stayOnLastToken: true);
                return !state.Errors.HasExceptions
                    ? result
                    : throw new DeserializationFailedException(result, state.Errors);
            }
            else
                throw new ArgumentException($"Can only deserialize into subclasses of class {nameof(Base)}. " + reader.GenerateLocationMessage(), nameof(targetType));
        }

        /// <summary>
        /// Reads a (subtree) of serialzed FHIR Json data into a POCO object.
        /// </summary>
        /// <typeparam name="T">The type of POCO to construct and deserialize</typeparam>
        /// <param name="reader">A json reader positioned on the first token of the object, or the beginning of the stream.</param>
        /// <returns>A fully initialized POCO with the data from the reader.</returns>
        public T DeserializeObject<T>(ref Utf8JsonReader reader) where T : Base => (T)DeserializeObject(typeof(T), ref reader);

        internal Resource? DeserializeResourceInternal(ref Utf8JsonReader reader, FhirJsonPocoDeserializerState state, bool stayOnLastToken)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                state.Errors.Add(ERR.EXPECTED_START_OF_OBJECT.With(ref reader, reader.TokenType));
                reader.Recover();  // skip to the end of the construct encountered (value or array)
                return null;
            }

            (ClassMapping? resourceMapping, FhirJsonException? error) = DetermineClassMappingFromInstance(ref reader, _inspector);

            if (resourceMapping is not null)
            {
                // If we have at least a mapping, let's try to continue
                var newResource = (Base)resourceMapping.Factory();

                try
                {
                    state.Path.EnterResource(resourceMapping.Name);
                    deserializeObjectInto(newResource, resourceMapping, ref reader, DeserializedObjectKind.Resource, state, stayOnLastToken);

                    if (!resourceMapping.IsResource)
                    {
                        state.Errors.Add(ERR.RESOURCE_TYPE_NOT_A_RESOURCE.With(ref reader, resourceMapping.Name));
                        return null;
                    }
                    else
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
            /// completely, includin its value part.
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
        /// done by the <see cref="DeserializeObject(Type, ref Utf8JsonReader)"/> function, which is in its
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
                state.Errors.Add(ERR.EXPECTED_START_OF_OBJECT.With(ref reader, reader.TokenType));
                reader.Recover();  // skip to the end of the construct encountered (value or array)
                return;
            }

            // read past start of object into first property or end of object
            reader.Read();

            var empty = true;
            var delayedValidations = new DelayedValidations();
            var oldErrorCount = state.Errors.Count;
            var (line, pos) = reader.GetLocation();

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                var currentPropertyName = reader.GetString()!;

                if (currentPropertyName == "resourceType")
                {
                    if (kind != DeserializedObjectKind.Resource) state.Errors.Add(ERR.RESOURCETYPE_UNEXPECTED.With(ref reader));
                    reader.SkipTo(JsonTokenType.PropertyName);  // skip to next property
                    continue;
                }

                empty = false;

                // Lookup the metadata for this property by its name to determine the expected type of the value
                var (propMapping, propValueMapping, error) = tryGetMappedElementMetadata(_inspector, mapping, ref reader, currentPropertyName);

                if (error is not null)
                {
                    state.Errors.Add(error);

                    // try to recover by skipping to the next property.
                    reader.SkipTo(JsonTokenType.PropertyName);
                    continue;
                }
                else
                {
                    // read past the property name into the value
                    reader.Read();

                    try
                    {
                        state.Path.EnterElement(propMapping!.Name);
                        deserializePropertyValueInto(target, currentPropertyName, propMapping, propValueMapping!, ref reader, delayedValidations, state);
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
            delayedValidations.Run();

            // read past object, unless this is the last EndObject in the top-level Deserialize call
            if (!stayOnLastToken) reader.Read();

            // do not allow empty complex objects.
            if (empty) state.Errors.Add(ERR.OBJECTS_CANNOT_BE_EMPTY.With(ref reader));

            // Only run instance validation when deserialization yielded no errors
            // to avoid spurious error messages.
            if (Settings.Validator is not null && kind != DeserializedObjectKind.FhirPrimitive && state.Errors.Count == oldErrorCount)
            {
                var context = new InstanceDeserializationContext(state.Path.GetPath(), line, pos, mapping);
                PocoDeserializationHelper.RunInstanceValidation(target, Settings.Validator, context, state.Errors);
            }

            return;
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
            DelayedValidations delayedValidations,
            FhirJsonPocoDeserializerState state
            )
        {
            object? result;
            var oldErrorCount = state.Errors.Count;
            var (line, pos) = reader.CurrentState.GetLocation();

            if (propertyValueMapping.IsFhirPrimitive)
            {
                // There might be an existing value, since FhirPrimitives may be spread out over two properties
                // (one with, and one without the '_')
                var existingValue = propertyMapping.GetValue(target);

                // Note that the POCO model will always allocate a new list if the property had not been set before,
                // so there is always an existingValue for IList
                result = propertyMapping.IsCollection ?
                    deserializeFhirPrimitiveList((IList)existingValue!, propertyName, propertyValueMapping, ref reader, delayedValidations, state) :
                    DeserializeFhirPrimitive(existingValue as PrimitiveType, propertyName, propertyValueMapping, ref reader, delayedValidations, state);
            }
            else
            {
                // This is not a FHIR primitive, so we should not be dealing with these weird _name members.
                if (propertyName[0] == '_')
                    state.Errors.Add(ERR.USE_OF_UNDERSCORE_ILLEGAL.With(ref reader, propertyMapping.Name, propertyName));

                // Note that repeating simple elements (like Extension.url) do not currently exist in the FHIR serialization
                result = propertyMapping.IsCollection
                    ? deserializeNormalList(propertyValueMapping, ref reader, state)
                    : deserializeSingleValue(ref reader, propertyValueMapping, state);
            }

            // Only do validation when no parse errors were encountered, otherwise we'll just
            // produce spurious messages.
            if (Settings.Validator is not null && oldErrorCount == state.Errors.Count)
            {
                var deserializationContext = new PropertyDeserializationContext(
                    state.Path.GetPath(),
                    propertyName,
                    line, pos,
                    propertyMapping);

                // If this is a FhirPrimitive, make sure we delay validation until we had the
                // chance to encounter both the `name` and `_name` property.
                if (delayedValidations is not null && propertyValueMapping.IsFhirPrimitive)
                {
                    delayedValidations.Schedule(
                        propertyMapping.Name + PROPERTY_VALIDATION_KEY_SUFFIX,
                        () => PocoDeserializationHelper.RunPropertyValidation(ref result, Settings.Validator!, deserializationContext, state.Errors));
                }
                else
                    PocoDeserializationHelper.RunPropertyValidation(ref result, Settings.Validator!, deserializationContext, state.Errors);
            }

            propertyMapping.SetValue(target, result);

            return;
        }

        /// <summary>
        /// Reads the content of a list with non-FHIR-primitive content (so, no name/_name pairs to be dealt with). Note
        /// that the contents can only be complex in the current FHIR serialization, but we'll be prepared and handle
        /// other situations (e.g. repeating Extension.url's, if they would ever exist).
        /// </summary>
        private IList? deserializeNormalList(
            ClassMapping propertyValueMapping,
            ref Utf8JsonReader reader,
            FhirJsonPocoDeserializerState state)
        {
            // Create a list of the type of this property's value.
            var listInstance = propertyValueMapping.ListFactory();

            // if true, we have encountered a single value where we expected an array.
            // we need to recover by creating an array with that single value.
            bool oneshot = false;

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                state.Errors.Add(ERR.EXPECTED_START_OF_ARRAY.With(ref reader));
                oneshot = true;
            }
            else
            {
                // Read past start of array
                reader.Read();

                if (reader.TokenType == JsonTokenType.EndArray)
                    state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY.With(ref reader));
            }

            // Can't make an iterator because of the ref readers struct, so need
            // to simply create a list by Adding(). Not the fastest approach :-(
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var result = deserializeSingleValue(ref reader, propertyValueMapping, state);
                listInstance.Add(result);

                if (oneshot) break;
            }

            // Read past end of array
            if (!oneshot) reader.Read();

            return listInstance;
        }

        internal class DelayedValidations
        {
            private readonly Dictionary<string, Action> _validations = new();

            public void Schedule(string key, Action validation)
            {
                // Add or overwrite the entry for the given key.
                if (_validations.ContainsKey(key)) _validations.Remove(key);
                _validations[key] = validation;
            }

            //public CodedValidationException[] Run() => _validations.Values.SelectMany(delayed => delayed()).ToArray();
            public void Run()
            {
                foreach (var validation in _validations.Values) validation();
            }
        }

        /// <summary>
        /// Reads a list of FHIR primitives (either from a name or _name property).
        /// </summary>
        /// <remarks>Upon completion, reader will be located at the next token afther the list.</remarks>
        private IList? deserializeFhirPrimitiveList(
            IList existingList,
            string propertyName,
            ClassMapping propertyValueMapping,
            ref Utf8JsonReader reader,
            DelayedValidations delayedValidations,
            FhirJsonPocoDeserializerState state
            )
        {
            // if true, we have encountered a single value where we expected an array.
            // we need to recover by creating an array with that single value.
            bool oneshot = false;

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                state.Errors.Add(ERR.EXPECTED_START_OF_ARRAY.With(ref reader));
                oneshot = true;
            }
            else
            {
                // read into array
                reader.Read();

                if (reader.TokenType == JsonTokenType.EndArray)
                    state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY.With(ref reader));
            }

            int originalSize = existingList.Count;

            // Can't make an iterator because of the ref readers struct, so need
            // to simply create a list by Adding(). Not the fastest approach :-(
            int elementIndex = 0;
            bool? onlyNulls = null;

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
                    _ = DeserializeFhirPrimitive((PrimitiveType)existingList[elementIndex]!, propertyName, propertyValueMapping, ref reader, delayedValidations, state);
                }

                elementIndex += 1;

                if (oneshot) break;
            }

            if (onlyNulls == true)
                state.Errors.Add(ERR.PRIMITIVE_ARRAYS_ONLY_NULL.With(ref reader));

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
            ref Utf8JsonReader reader,
            DelayedValidations? delayedValidations,
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

                state.Path.EnterElement("value");
                try
                {

                    var (result, error) = DeserializePrimitiveValue(ref reader, primitiveValueProperty.ImplementingType);

                    // Only do validation when no parse errors were encountered, otherwise we'll just
                    // produce spurious messages.
                    if (error is not null)
                        state.Errors.Add(error);
                    else if (Settings.Validator is not null)
                    {

                        var propertyValueContext = new PropertyDeserializationContext(
                            state.Path.GetPath(),
                            "value",
                            line, pos,
                            primitiveValueProperty);

                        PocoDeserializationHelper.RunPropertyValidation(ref result, Settings.Validator, propertyValueContext, state.Errors);
                    }
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
                deserializeObjectInto(targetPrimitive, propertyValueMapping, ref reader, DeserializedObjectKind.FhirPrimitive, state, stayOnLastToken: false);
            }

            // Only do validation on this instance when no parse errors were encountered, otherwise we'll just
            // produce spurious messages. Also, delay validation of this instance until we have processed both
            // the `name` and `_name` property.
            if (Settings.Validator is not null && oldErrorCount == state.Errors.Count)
            {
                var context = new InstanceDeserializationContext(state.Path.GetPath(), line, pos, propertyValueMapping);
                if (delayedValidations is null)
                    PocoDeserializationHelper.RunInstanceValidation(targetPrimitive, Settings.Validator, context, state.Errors);
                else
                    delayedValidations.Schedule(
                        propertyName.TrimStart('_') + INSTANCE_VALIDATION_KEY_SUFFIX,
                        () => PocoDeserializationHelper.RunInstanceValidation(targetPrimitive, Settings.Validator, context, state.Errors));
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

            // primitive values (not FHIR primitives, real primitives, like Element.id)
            // Note: 'value' attributes for FHIR primitives are handled elsewhere, since that logic
            // needs to handle PrimitiveType.ObjectValue & dual properties.
            else if (propertyValueMapping.IsPrimitive)
            {
                var (result, error) = DeserializePrimitiveValue(ref reader, propertyValueMapping.NativeType);

                if (error is not null && result is not null)
                {
                    // Signal the fact that we're throwing away data here, as we cannot put
                    // "raw" data into a simple property like Id and Url.
                    state.Errors.Add(ERR.INCOMPATIBLE_SIMPLE_VALUE.With(ref reader, error, error.Message));
                    return null;
                }
                else
                {
                    state.Errors.Add(error);
                    return result;
                }
            }

            // "normal" complex types & backbones
            else
            {
                var newComplex = (Base)propertyValueMapping.Factory();
                deserializeObjectInto(newComplex, propertyValueMapping, ref reader, DeserializedObjectKind.Complex, state, stayOnLastToken: false);
                return newComplex;
            }
        }

        /// <summary>
        /// Does a best-effort parse of the data available at the reader, given the required type of the property the
        /// data needs to be read into.
        /// </summary>
        /// <returns>A value without an error if the data could be parsed to the required type, and a value with an error if the
        /// value could not be parsed - in which case the value returned is the raw value coming in from the reader.</returns>
        /// <remarks>Upon completion, the reader will be positioned on the token after the primitive.</remarks>
        internal (object?, FhirJsonException?) DeserializePrimitiveValue(ref Utf8JsonReader reader, Type requiredType)
        {
            // Check for unexpected non-value types.
            if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.StartArray)
            {
                var exception = reader.TokenType == JsonTokenType.StartObject
                    ? ERR.EXPECTED_PRIMITIVE_NOT_OBJECT.With(ref reader)
                    : ERR.EXPECTED_PRIMITIVE_NOT_ARRAY.With(ref reader);
                reader.Recover();
                return (null, exception);
            }

            // Check for value types
            (object? partial, FhirJsonException? error) result = reader.TokenType switch
            {
                JsonTokenType.Null => new(null, ERR.EXPECTED_PRIMITIVE_NOT_NULL.With(ref reader)),
                JsonTokenType.String when requiredType == typeof(string) => new(reader.GetString(), null),
                JsonTokenType.String when requiredType == typeof(byte[]) =>
                                !Settings.DisableBase64Decoding ? readBase64(ref reader) : new(reader.GetString(), null),
                JsonTokenType.String when requiredType == typeof(DateTimeOffset) => readDateTimeOffset(ref reader),
                JsonTokenType.String when requiredType.IsEnum => new(reader.GetString(), null),
                //JsonTokenType.String when requiredType.IsEnum => readEnum(ref reader, requiredType),
                JsonTokenType.String => unexpectedToken(ref reader, reader.GetString(), requiredType.Name, "string"),
                JsonTokenType.Number => tryGetMatchingNumber(ref reader, requiredType),
                JsonTokenType.True or JsonTokenType.False when requiredType == typeof(bool) => new(reader.GetBoolean(), null),
                JsonTokenType.True or JsonTokenType.False => unexpectedToken(ref reader, reader.GetRawText(), requiredType.Name, "boolean"),

                _ =>
                    // This would be an internal logic error, since our callers should have made sure we're
                    // on the primitive value after the property name (and the Utf8JsonReader would have complained about any
                    // other token that one that is a value).
                    // EK: I think 'Comment' is the only possible non-expected option here....
                    throw new InvalidOperationException($"Unexpected token type {reader.TokenType} while parsing a primitive value. " +
                        reader.GenerateLocationMessage()),
            };

            // If there is a failure, and we have a handler installed, call it
            if (Settings.OnPrimitiveParseFailed is not null && result.error is not null)
                result = Settings.OnPrimitiveParseFailed(ref reader, requiredType, result.partial, result.error);

            // Read past the value
            reader.Read();

            return result;

            static (object?, FhirJsonException?) readBase64(ref Utf8JsonReader reader) =>
                reader.TryGetBytesFromBase64(out var bytesValue) ?
                    new(bytesValue, null) :
                    new(reader.GetString(), ERR.INCORRECT_BASE64_DATA.With(ref reader));

            static (object?, FhirJsonException?) readDateTimeOffset(ref Utf8JsonReader reader)
            {
                var contents = reader.GetString()!;

                return ElementModel.Types.DateTime.TryParse(contents, out var parsed) ?
                    new(parsed.ToDateTimeOffset(TimeSpan.Zero), null) :
                    new(contents, ERR.STRING_ISNOTAN_INSTANT.With(ref reader, contents));
            }

            // Validation is now done using POCO validation, so have removed it here.
            // Keep code around in case I make my mind up before publication.
            //static (object?, FhirJsonException?) readEnum(ref Utf8JsonReader reader, Type enumType)
            //{
            //    var contents = reader.GetString()!;
            //    var enumValue = EnumUtility.ParseLiteral(contents, enumType);

            //    return enumValue is not null
            //        ? (contents, null)
            //        : (contents, ERR.CODED_VALUE_NOT_IN_ENUM.With(ref reader, contents, EnumUtility.GetName(enumType)));
            //}
        }

        private static (object?, FhirJsonException) unexpectedToken(ref Utf8JsonReader reader, string? value, string expected, string actual) =>
            new(value, ERR.UNEXPECTED_JSON_TOKEN.With(ref reader, expected, actual, value));

        /// <summary>
        /// This function tries to map from the json-format "generic" number to the kind of numeric type defined in the POCO.
        /// </summary>
        /// <remarks>Reader must be positioned on a number token. This function will not move the reader to the next token.</remarks>
        private static (object?, FhirJsonException?) tryGetMatchingNumber(ref Utf8JsonReader reader, Type requiredType)
        {
            if (reader.TokenType != JsonTokenType.Number)
                throw new InvalidOperationException($"Cannot read a numeric when reader is on a {reader.TokenType}. " +
                    reader.GenerateLocationMessage());

            object? value = null;
            bool success;

            if (requiredType == typeof(decimal))
                success = reader.TryGetDecimal(out decimal dec) && (value = dec) is { };
            else if (requiredType == typeof(int))
                success = reader.TryGetInt32(out int i32) && (value = i32) is { };
            else if (requiredType == typeof(uint))
                success = reader.TryGetUInt32(out uint ui32) && (value = ui32) is { };
            else if (requiredType == typeof(long))
                success = reader.TryGetInt64(out long i64) && (value = i64) is { };
            else if (requiredType == typeof(ulong))
                success = reader.TryGetUInt64(out ulong ui64) && (value = ui64) is { };
            else if (requiredType == typeof(float))
                success = reader.TryGetSingle(out float si) && si.IsNormal() && (value = si) is { };
            else if (requiredType == typeof(double))
                success = reader.TryGetDouble(out double dbl) && dbl.IsNormal() && (value = dbl) is { };
            else
            {
                var rawValue = reader.GetRawText();
                return unexpectedToken(ref reader, rawValue, requiredType.Name, "number");
            }

            // We expected a number, we found a json number, but they don't match (e.g. precision etc)
            if (success)
            {
                return new(value, null);
            }
            else
            {
                var rawValue = reader.GetRawText();
                return new(rawValue, ERR.NUMBER_CANNOT_BE_PARSED.With(ref reader, rawValue, requiredType.Name));
            }
        }

        /// <summary>
        /// Returns the <see cref="ClassMapping" /> for the object to be deserialized using the `resourceType` property.
        /// </summary>
        /// <remarks>Assumes the reader is on the start of an object.</remarks>
        internal static (ClassMapping?, FhirJsonException?) DetermineClassMappingFromInstance(ref Utf8JsonReader reader, ModelInspector inspector)
        {
            var (resourceType, error) = determineResourceType(ref reader);

            if (resourceType is not null)
            {
                var resourceMapping = inspector.FindClassMapping(resourceType);

                return resourceMapping is not null ?
                    (new(resourceMapping, null)) :
                    (new(null, ERR.UNKNOWN_RESOURCE_TYPE.With(ref reader, resourceType)));
            }
            else
                return new(null, error);
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
                                new(null, ERR.RESOURCETYPE_SHOULD_BE_STRING.With(ref reader, reader.TokenType));
                        }
                    }
                }

                return new(null, ERR.NO_RESOURCETYPE_PROPERTY.With(ref reader));
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
            string propertyName)
        {
            bool startsWithUnderscore = propertyName[0] == '_';
            var elementName = startsWithUnderscore ? propertyName.Substring(1) : propertyName;

            var propertyMapping = parentMapping.FindMappedElementByName(elementName)
                ?? parentMapping.FindMappedElementByChoiceName(elementName);

            if (propertyMapping is null)
                return (null, null, ERR.UNKNOWN_PROPERTY_FOUND.With(ref reader, propertyName));

            (ClassMapping? propertyValueMapping, FhirJsonException? error) = propertyMapping.Choice switch
            {
                ChoiceType.None or ChoiceType.ResourceChoice =>
                    inspector.FindOrImportClassMapping(propertyMapping.ImplementingType) is ClassMapping m
                        ? (m, null)
                        : throw new InvalidOperationException($"Encountered property type {propertyMapping.ImplementingType} for which no mapping was found in the model assemblies. " + reader.GenerateLocationMessage()),
                ChoiceType.DatatypeChoice => getChoiceClassMapping(ref reader),
                _ => throw new NotImplementedException("Unknown choice type in property mapping. " + reader.GenerateLocationMessage())
            };

            return (propertyMapping, propertyValueMapping, error);

            (ClassMapping?, FhirJsonException?) getChoiceClassMapping(ref Utf8JsonReader r)
            {
                string typeSuffix = elementName.Substring(propertyMapping.Name.Length);

                return string.IsNullOrEmpty(typeSuffix)
                    ? (null, ERR.CHOICE_ELEMENT_HAS_NO_TYPE.With(ref r, propertyMapping.Name))
                    : inspector.FindClassMapping(typeSuffix) is ClassMapping cm
                        ? (cm, null)
                        : (default, ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE.With(ref r, propertyMapping.Name, typeSuffix));
            }
        }
    }

    internal class FhirJsonPocoDeserializerState
    {
        public readonly ExceptionAggregator Errors = new();
        public readonly PathStack Path = new();
    }
}

#nullable restore
#endif