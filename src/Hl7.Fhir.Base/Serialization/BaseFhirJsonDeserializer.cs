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
using COVE = Hl7.Fhir.Validation.CodedValidationException;
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

        deserializeSingleValueInto(instance, ref reader, "(root)", "(root)", null, classMapping, state, stayOnLastToken: true);
        //deserializeJsonObjectInto(ref reader, instance, classMapping, state, stayOnLastToken: true);
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
        instance = createNewObjectInstance(ref reader, mapping, state, out var actualClassMapping);
        deserializeSingleValueInto(instance, ref reader, "(root)", "(root)", null, actualClassMapping, state, stayOnLastToken: true);
       // deserializeJsonObjectInto(ref reader, instance, actualClassMapping, state, stayOnLastToken: true);

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
    private void deserializeJsonObjectInto(
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

        if (mapping.IsResource)
            state.EnterResource(mapping.Name);
        state.EnterObjectContext();

        int nErrorCount = state.Errors.Count;
        var empty = true;

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            // The resourceType property on the level of a resource is used to determine
            // the type and should otherwise be skipped when processing a resource.
            if (reader.ValueTextEquals("resourceType"u8) && mapping.IsResource)
            {
                reader.SkipTo(JsonTokenType.PropertyName);
                continue;
            }

            empty = false;

            deserializePropertyInto(target, mapping, ref reader, state);
        }

        if (mapping.IsResource)
            state.ExitResource();

        // Now after having deserialized all properties we can run the validations that needed to be
        // postponed until after all properties have been seen (e.g. Instance and Property validations for
        // primitive properties, since they may be composed from two properties `name` and `_name` in json
        // and should only be validated when both have been processed, even if megabytes apart in the json file).
        runDelayedValidations(state);
        state.LeaveObjectContext();

        // read past object, unless this is the last EndObject in the top-level Deserialize call
        if (!stayOnLastToken) reader.Read();

        // do not allow empty complex objects.
        if (empty) state.Errors.Add(ERR.OBJECTS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));

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
            usedMapping = determineResourceClassMappingFromInstance(ref reader, state);
            return usedMapping.CreateInstance();
        }

        // If this is not a resource, we can create a new instance of the class mapping.
        if (mapping.CreateInstance() is not { } result)
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
        ref Utf8JsonReader reader,
        PocoDeserializerState state)
    {
        if(reader.TokenType != JsonTokenType.PropertyName)
            throw new InvalidOperationException(
                $"deserializePropertyInto should only be called on JSON properties: " +
                $"Current token is {reader.TokenType}.");

        var (line, pos) = reader.GetLocation();
        var propertyName = reader.GetString()!;

        // move past property name
        reader.Read();

        // Lookup the metadata for this property by its name to determine the expected type of the value
        var metadata = getMappingForElement(parentMapping, propertyName, state, ref reader);
        var propertyMapping = metadata.PropertyMapping;
        var elementName = propertyMapping.Name;

        // Since we're a forward-only reader, we have to keep track of repeating properties ourselves.
        // This includes choice properties, which may differ in suffix, but are still the same property.
        var usesUnderscore = propertyName.StartsWith('_');
        var propertyNameWithoutTypeSuffix = (usesUnderscore ? "_" : "") + propertyMapping.Name;
        if(state.GetObjectContext().HitProperty(propertyNameWithoutTypeSuffix))
            state.Errors.Add(ERR.DUPLICATE_PROPERTY(ref reader, state.Path.GetInstancePath(), propertyName));

        if(usesUnderscore && !metadata.ValueMapping.IsFhirPrimitive)
            state.Errors.Add(ERR.USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName, propertyName));

        state.EnterElement(elementName);
        if(propertyMapping.IsCollection)
            state.SetIndex(0);

        target.TryGetValue(elementName, out var existingValue);
        var result = deserializeRhs(existingValue, ref reader, propertyName, metadata, state);
        target.SetValue(elementName, result);

        // Pass the name as encountered in the serialized form (without the '_' prefix), so the
        // validator can also detect names that only differ from the defined name by casing.
        doPropertyValidation(target, usesUnderscore ? propertyName[1..] : propertyName, result, metadata, state, line, pos);

        state.ExitElement();
    }

    private void doPropertyValidation(Base target, string serializedName, object propertyValue, PropertyValueMapping metadata, PocoDeserializerState state, long line, long pos)
    {
        if (Settings.Validator is null) return;

        var context = new PocoValidationContext(target, _inspector, state.Path.PathProducer,
                line, pos, Settings.NarrativeValidation)
        {
            MemberName = metadata.PropertyMapping.NativeProperty?.Name
        };

        // If this is a FHIR primitive (or underscore property), we will need to delay validation,
        // when we have had a chance to see both the `name` and `_name` properties.
        if (metadata.ValueMapping.IsFhirPrimitive)
            state.GetObjectContext().ScheduleDelayedValidation(metadata.PropertyMapping.Name,
                DelayedValidation.ForProperty(serializedName, propertyValue, metadata.PropertyMapping, context));
        else
            runPropertyValidation(serializedName, propertyValue, metadata.PropertyMapping, context, state);
    }

    private void runPropertyValidation(string serializedName, object? propertyValue, PropertyMapping propertyMapping,
        PocoValidationContext context, PocoDeserializerState state) =>
        state.Errors.Add(Settings.Validator!.ValidateProperty(serializedName, propertyValue, propertyMapping, context));

    private void doObjectValidation(Base poco, ClassMapping classMapping, PocoDeserializerState state, long line, long pos, string? memberName = null)
    {
        if(Settings.Validator is null) return;

        var context =
            new PocoValidationContext(poco, _inspector, state.Path.PathProducer, line, pos, Settings.NarrativeValidation)
            {
                MemberName = memberName
            };

        // If this is a FHIR primitive (or underscore property), we will need to delay validation,
        // when we have had a chance to see both the `name` and `_name` properties.
        if (classMapping.IsFhirPrimitive)
            state.GetObjectContext().ScheduleDelayedValidation(poco, DelayedValidation.ForObject(poco, classMapping, context));
        else
            runObjectValidation(poco, classMapping, context, state);
    }

    private void runObjectValidation(Base poco, ClassMapping classMapping, PocoValidationContext context, PocoDeserializerState state)
    {
        var nErrorCount = state.Errors.Count;

        state.Errors.Add(Settings.Validator!.ValidateObject(poco, classMapping, context));

        // If we have been parsing a resource, annotate any new validation errors that were encountered (if endaled).
        if (classMapping.IsResource && Settings.AnnotateResourceParseExceptions && state.Errors.Count > nErrorCount)
        {
            List<CodedException> resourceErrs = state.Errors.Skip(nErrorCount).ToList();
            poco.SetAnnotation(resourceErrs);
        }
    }

    /// <summary>
    /// A validation that has been scheduled to run once the enclosing object has been deserialized
    /// completely (see <see cref="ObjectParsingState.ScheduleDelayedValidation"/>).
    /// </summary>
    /// <remarks>This is a struct, and the validations are dispatched by <see cref="Run"/> rather than by a
    /// delegate, so that scheduling one costs nothing: FHIR primitives - which is what delayed validation is
    /// for - make up the bulk of the elements of a resource, and every one of them would otherwise allocate a
    /// closure plus a delegate for a validation that (nearly) always succeeds.</remarks>
    internal readonly struct DelayedValidation
    {
        private readonly bool _isObjectValidation;
        private readonly PocoValidationContext _context;

        // Used for object validation only.
        private readonly Base? _instance;
        private readonly ClassMapping? _classMapping;

        // Used for property validation only.
        private readonly string? _serializedName;
        private readonly object? _propertyValue;
        private readonly PropertyMapping? _propertyMapping;

        private DelayedValidation(bool isObjectValidation, PocoValidationContext context, Base? instance,
            ClassMapping? classMapping, string? serializedName, object? propertyValue, PropertyMapping? propertyMapping)
        {
            _isObjectValidation = isObjectValidation;
            _context = context;
            _instance = instance;
            _classMapping = classMapping;
            _serializedName = serializedName;
            _propertyValue = propertyValue;
            _propertyMapping = propertyMapping;
        }

        public static DelayedValidation ForProperty(string serializedName, object? propertyValue,
            PropertyMapping propertyMapping, PocoValidationContext context) =>
            new(false, context, null, null, serializedName, propertyValue, propertyMapping);

        public static DelayedValidation ForObject(Base instance, ClassMapping classMapping, PocoValidationContext context) =>
            new(true, context, instance, classMapping, null, null, null);

        public void Run(BaseFhirJsonDeserializer deserializer, PocoDeserializerState state)
        {
            if (_isObjectValidation)
                deserializer.runObjectValidation(_instance!, _classMapping!, _context, state);
            else
                deserializer.runPropertyValidation(_serializedName!, _propertyValue, _propertyMapping!, _context, state);
        }
    }

    /// <summary>
    /// Runs the validations that were postponed until all properties of the current object have been seen.
    /// </summary>
    private void runDelayedValidations(PocoDeserializerState state)
    {
        foreach (var validation in state.GetObjectContext().DelayedValidations)
            validation.Run(this, state);
    }

    private void deserializeListInto(IList existingList, ref Utf8JsonReader reader, string propertyName, PropertyValueMapping metadata,
        PocoDeserializerState state)
    {
        int originalSize = existingList.Count;
        int elementIndex = 0;

        parseListElements(ref reader);

        void parseListElements(ref Utf8JsonReader reader)
        {
            // Read past start of array
            reader.Read();

            if (reader.TokenType == JsonTokenType.EndArray)
                state.Errors.Add(ERR.ARRAYS_CANNOT_BE_EMPTY(ref reader, state.Path.GetInstancePath()));

            bool? onlyNulls = null;

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    onlyNulls ??= true;

                    if (elementIndex >= originalSize)
                        existingList.Add(null);

                    elementIndex += 1;
                    state.SetIndex(elementIndex);

                    // skip the null, nothing to do.
                    reader.Read();
                }
                else if(reader.TokenType == JsonTokenType.StartArray)
                {
                    onlyNulls = false;

                    // Nested list, report error and continue as if it is not nested.
                    state.Errors.Add(ERR.NESTED_ARRAY(ref reader, state.Path.GetInstancePath()));
                    parseListElements(ref reader);
                }
                else
                {
                    onlyNulls = false;

                    if (elementIndex >= originalSize)
                        existingList.Add(null);

                    ClassMapping actualClassMapping = metadata.ValueMapping;
                    if(existingList[elementIndex] is null)
                        existingList[elementIndex] = createNewObjectInstance(ref reader, metadata.ValueMapping, state, out actualClassMapping);

                    if(existingList[elementIndex] is not Base existingBase)
                        throw new InvalidOperationException($"Expected existing element at index {elementIndex} to be a Base, but it is {existingList[elementIndex]?.GetType().Name ?? "null"}.");

                    deserializeSingleValueInto(existingBase, ref reader, propertyName, metadata.PropertyMapping.Name, metadata.PropertyMapping.NativeProperty?.Name, actualClassMapping, state);

                    elementIndex += 1;
                    state.SetIndex(elementIndex);
                }
            }

            //state.LeaveArray();

            if(onlyNulls == true)
                state.Errors.Add(ERR.PRIMITIVE_ARRAYS_ONLY_NULL(ref reader, state.Path.GetInstancePath()));

            // read past array to next property or end of object
            reader.Read();
        }
    }

    private object deserializeRhs(object? existingValue, ref Utf8JsonReader reader, string propertyName, PropertyValueMapping metadata,
        PocoDeserializerState state)
    {
        switch (existingValue)
        {
            case IList existingList:
            {
                // deserialize a normal list
                deserializeListInto(existingList, ref reader, propertyName, metadata, state);
                return existingList;
            }
            case Base existingBase when reader.TokenType == JsonTokenType.StartArray:
            {
                // upgrade current existing value to a list
                var list = metadata.CreateList();
                list.Add(existingValue);
                deserializeListInto(list, ref reader, propertyName, metadata, state);

                return existingBase;
            }
            case Base existingBase:
            {
                // deserialize a primitive value into the existing Base
                deserializeSingleValueInto(existingBase, ref reader, propertyName, metadata.PropertyMapping.Name, metadata.PropertyMapping.NativeProperty?.Name, metadata.ValueMapping, state);
                return existingBase;
            }
            case null when reader.TokenType == JsonTokenType.StartArray:
            {
                // create a new list
                var list = metadata.CreateList();
                deserializeListInto(list, ref reader, propertyName, metadata, state);
                return list;
            }
            case null:
            {
                // create a new instance of the value type and deserialize into it.
                var newValue = createNewObjectInstance(ref reader, metadata.ValueMapping, state, out var actualClassMapping);
                deserializeSingleValueInto(newValue, ref reader, propertyName, metadata.PropertyMapping.Name, metadata.PropertyMapping.NativeProperty?.Name, actualClassMapping, state);
                return newValue;
            }
            default:
                throw new ArgumentException("Existing value is supposed to by a Base or IList of Base.", nameof(existingValue));
        }
    }

    // This deserializes a single value (primitive or object) into an existing Base instance, by adding members
    // or settings the Value (if this is a primitive).
    private void deserializeSingleValueInto(
        Base existingValue,
        ref Utf8JsonReader reader,
        string propertyName, string elementName, string? nativeMemberName,
        ClassMapping propertyValueMapping,
        PocoDeserializerState state,
        bool stayOnLastToken = false)
    {
        var (line, pos) = reader.GetLocation();
        bool usesUnderscore = propertyName[0] == '_';

        if (IsOnJsonPrimitiveToken(ref reader))
        {
            if (usesUnderscore)
                state.Errors.Add(ERR.UNDERSCORE_SHOULD_BE_OBJECT(ref reader, state.Path.GetInstancePath(), propertyName));
            // This else is important to avoid duplicative error messages.
            else if(!propertyValueMapping.IsFhirPrimitive)
                state.Errors.Add(ERR.UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName));

            deserializePrimitiveInto(ref reader, existingValue, propertyValueMapping);
        }
        else if (isOnJsonObject(ref reader))
        {
            if(propertyValueMapping.IsFhirPrimitive && !usesUnderscore)
                state.Errors.Add(ERR.UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE(ref reader, state.Path.GetInstancePath(), elementName));

            deserializeJsonObjectInto(ref reader, existingValue, propertyValueMapping, state, stayOnLastToken);
        }
        else if (reader.TokenType is JsonTokenType.Null)
        {
            state.Errors.Add(ERR.EXPECTED_PRIMITIVE_NOT_NULL(ref reader, state.Path.GetInstancePath()));

            // Skip the null token, since we don't want to deserialize it.
            reader.Read();
        }
        else
        {
            // Completely unexpected tokens (e.g. StartArray, EndArray, comments, EndObject, etc.)
            throw new InvalidOperationException($"Encountered unexpected token {reader.TokenType} " +
                                                $"while parsing a primitive or object for property '{propertyName}'.");
        }

        // Set the position info on this object, if enabled.
        if (Settings.AnnotateLineInfo)
        {
            var annotation = new JsonSerializationDetails { LineNumber = (int)line, LinePosition = (int)pos };
            // Prioritize having the line/pos of the property itself, not the _property.
            if (!usesUnderscore)
                existingValue.SetAnnotation(annotation);
            else if (!existingValue.HasAnnotation<JsonSerializationDetails>())
                existingValue.AddAnnotation(annotation);
        }

        // Validate the value we have just deserialized.
        doObjectValidation(existingValue, propertyValueMapping, state, line, pos, nativeMemberName);
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
    

    internal static bool IsOnJsonPrimitiveToken(ref Utf8JsonReader reader) =>
        reader.TokenType is
            JsonTokenType.String or JsonTokenType.Number
            or JsonTokenType.False or JsonTokenType.True;

    private static bool isOnJsonObject(ref Utf8JsonReader reader) =>
        reader.TokenType is JsonTokenType.StartObject;

    internal class ObjectParsingState
    {
        private Dictionary<object, DelayedValidation>? _validations;
        private readonly HashSet<string> _propertiesEncountered = [];

        // Mappings fabricated on the fly for elements that are not part of the class being parsed.
        // Created lazily, since a well-formed resource has none of these.
        private Dictionary<string, PropertyMapping>? _localPropertyMappings;

        public bool HitProperty(string propertyName) => !_propertiesEncountered.Add(propertyName);

        public PropertyMapping? FindLocalPropertyMapping(string propertyName) =>
            _localPropertyMappings?.GetValueOrDefault(propertyName);

        public void AddLocalPropertyMapping(string propertyName, PropertyMapping mapping) =>
            (_localPropertyMappings ??= new Dictionary<string, PropertyMapping>()).Add(propertyName, mapping);

        public void ScheduleDelayedValidation(object key, DelayedValidation validation)
        {
            _validations ??= new Dictionary<object, DelayedValidation>();

            // Add or overwrite the entry for the given key.
            _validations.Remove(key);
            _validations[key] = validation;
        }

        public Dictionary<object, DelayedValidation>.ValueCollection DelayedValidations =>
            _validations?.Values ?? EMPTY_VALIDATIONS.Values;

        private static readonly Dictionary<object, DelayedValidation> EMPTY_VALIDATIONS = new();
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

        if (existing is PrimitiveType existingPrimitive)
        {
            // Note, this loses information, hence the repeated property is a fatal error.
            existingPrimitive.JsonValue ??= primitiveValue;
        }
        else
        {
            var preservedValue = pocoFromPrimitive(primitiveValue);
            existing.SetValue("value", preservedValue);
        }

        return;

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
    private ClassMapping determineResourceClassMappingFromInstance(ref Utf8JsonReader reader, PocoDeserializerState state)
    {
        var resourceType = scanForResourceType(ref reader, state);
        if (resourceType is null) return makeUnnamedResourceMapping(state.Path.GetInstancePath());

        var resourceMapping = _inspector.FindClassMapping(resourceType);
        if (resourceMapping is null or { IsResource: false })
            return new ClassMapping(_inspector, resourceType, typeof(DynamicResource));

        // The inspector finds resource types case-insensitively, so the data can still be parsed
        // into the correct POCO, but a wrong-cased resource type name violates the spec and is
        // reported. This is a model-level validation, so it is skipped when the validator is off.
        if (Settings.Validator is not null && !string.Equals(resourceMapping.Name, resourceType, StringComparison.Ordinal))
        {
            var (line, pos) = reader.GetLocation();
            state.Errors.Add(COVE.WRONG_CASED_RESOURCE_TYPE(state.Path.GetInstancePath(), line, pos, resourceType, resourceMapping.Name));
        }

        return resourceMapping;
    }

    private const string UNNAMED_RESOURCE_NAME_PREFIX = "UnnamedResource_";

    private ClassMapping makeUnnamedResourceMapping(string path) =>
        new(_inspector, $"{UNNAMED_RESOURCE_NAME_PREFIX}{path}", typeof(DynamicResource));

    internal static bool IsUnnamedResourceMapping(ClassMapping c) => c.Name.StartsWith(UNNAMED_RESOURCE_NAME_PREFIX);

    private static string? scanForResourceType(ref Utf8JsonReader reader, PocoDeserializerState state)
    {
        var originalReader = reader;    // copy the struct so we can "rewind"
        var atDepth = reader.CurrentDepth + 1;

        try
        {
            while (reader.Read() && reader.CurrentDepth >= atDepth)
            {
                if (reader.TokenType != JsonTokenType.PropertyName || reader.CurrentDepth != atDepth) continue;

                if (!reader.ValueTextEquals("resourceType"u8)) continue;

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
        ref Utf8JsonReader reader
        )
    {
        bool startsWithUnderscore = propertyName[0] == '_';
        var propNameWithoutUnderscore = startsWithUnderscore ? propertyName[1..] : propertyName;

        // In FHIR primitives, the "value" property is a special case and should not appear as a
        // separate "value" property in Json. If it does, treat it like an unknown property.
        bool isUnexpectedValueProperty = parentMapping.IsFhirPrimitive && propNameWithoutUnderscore == "value";

        var localMapping = state.GetObjectContext().FindLocalPropertyMapping(propNameWithoutUnderscore);
        PropertyMapping? definedMapping = null;

        if (localMapping is null && !isUnexpectedValueProperty
            && parentMapping.TryFindElement(propNameWithoutUnderscore) is { } lookup)
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

        var propertyMapping = localMapping
            ?? definedMapping
            ?? getUnknownPropMapping(ref reader, startsWithUnderscore);

        var propertyValueMapping = propertyMapping.Choice switch
        {
            // A custom type mapping is used directly: resolving it via the .NET type (below) would
            // lose the identity of the custom type, since custom types share the same dynamic .NET type.
            ChoiceType.None or ChoiceType.ResourceChoice when propertyMapping.PropertyTypeMapping is { IsCustomMapping: true } customType =>
                customType,
            ChoiceType.None or ChoiceType.ResourceChoice =>
                parentMapping.Inspector.FindOrImportClassMapping(propertyMapping.GetInstantiableType()) ??
                    throw new InvalidOperationException($"Encountered property type {propertyMapping.GetInstantiableType()} for which" +
                                                        $" no mapping was found in the model assemblies."),
            ChoiceType.DatatypeChoice => getChoiceClassMapping(ref reader),
            _ => throw new NotSupportedException($"ChoiceType '{propertyMapping.Choice}' is not supported.")
        };

        return new PropertyValueMapping(propertyMapping, propertyValueMapping);

        // The path of the element we are resolving the mapping for. We have not actually moved into the
        // element yet (the caller does that), so this simulates that move - only when a diagnostic below
        // needs it, which keeps a path part per element out of the happy path.
        string elementPath() => state.Path.EnterElement(propertyMapping.Name).GetInstancePath();

        ClassMapping getChoiceClassMapping(ref Utf8JsonReader r)
        {
            var typeSuffix = propNameWithoutUnderscore.AsSpan(propertyMapping.Name.Length);

            if (!typeSuffix.IsEmpty)
            {
                // Span-based lookup avoids allocating the suffix substring for the common case
                // where the suffix resolves to a known type.
                var foundChoiceMapping = parentMapping.Inspector.FindClassMapping(typeSuffix);

                if (foundChoiceMapping is not null)
                    return foundChoiceMapping;

                var guessedDynamicType = getUnknownPropMapping(ref r, startsWithUnderscore).ImplementingType;
                return new ClassMapping(_inspector, typeSuffix.ToString(), guessedDynamicType);
            }
            else
            {
                var path = elementPath();
                state.Errors.Add(ERR.CHOICE_ELEMENTS_MUST_HAVE_SUFFIX(ref r, path, propNameWithoutUnderscore));

                var guessedDynamicType = getUnknownPropMapping(ref r, startsWithUnderscore).ImplementingType;
                return new ClassMapping(_inspector, $"UnknownType_{path}", guessedDynamicType);
            }
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
                    new PropertyMapping(parentMapping, propNameWithoutUnderscore, getCustomMappingTypeForToken(peekArrayElementToken(ref r))).PromoteToList(),
                _ =>
                    new PropertyMapping(parentMapping, propNameWithoutUnderscore, getCustomMappingTypeForToken(r.TokenType))
            };

            state.GetObjectContext().AddLocalPropertyMapping(propNameWithoutUnderscore, customPropertyMapping);

            return customPropertyMapping;

            Type getCustomMappingTypeForToken(JsonTokenType tokenType)
            {
                return tokenType switch
                {
                    JsonTokenType.String or JsonTokenType.True or JsonTokenType.False or JsonTokenType.Number => typeof(DynamicPrimitive),
                    JsonTokenType.StartObject when !hasUnderscore => typeof(DynamicDataType),
                    _ when hasUnderscore => typeof(DynamicPrimitive),
                    _ => typeof(DynamicDataType)
                };
            }
        }
    }
}