using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal ref struct JsonSource
    {
        public const string ResourceTypePropertyName = "resourceType";

        public JsonSource(ref Utf8JsonReader reader, ParserSettings settings)
        {
            _reader = reader;
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _model = ModelInfos.Get(_settings.Version);
            _states = new Stack<State>();
        }

        public void GetReader(ref Utf8JsonReader reader)
        {
            reader = _reader;
        }

        public bool IsVersion(Model.Version versions)
        {
            return (versions & _settings.Version) != 0;
        }

        public SourceException CreateWrongResourceTypeException(Type expectedType, Resource actualResource)
        {
            var expectedResourceType = _model.GetFhirTypeNameForType(expectedType) ?? expectedType.Name;
            return CreateWrongResourceTypeException(expectedResourceType, actualResource.TypeName);
        }

        public void CheckResourceType(string expectedResourceType)
        {
            if (_reader.TokenType != JsonTokenType.String)
            {
                throw CreateNotAStringException();
            }
            var resourceType = _reader.GetString();
            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw CreateEmptyStringException();
            }
            if (_model.GetTypeForFhirType(resourceType) == null)
            {
                throw CreateUnknownResourceTypeException(resourceType);
            }
            if (resourceType != expectedResourceType)
            {
                throw CreateWrongResourceTypeException(expectedResourceType, resourceType);
            }
            SetHasNonEmptyElements();
        }

        public string GetElementId()
        {
            if (TryGetNonEmptyString(out var id))
            {
                SetHasNonEmptyElements();
                return id;
            }
            return null;
        }

        public string GetExtensionUrl()
        {
            if (TryGetNonEmptyString(out var url))
            {
                SetHasNonEmptyElements();
                return url;
            }
            return null;
        }

        public string GetXHtml()
        {
            if (TryGetNonEmptyString(out var xml))
            {
                if (!_settings.PermissiveParsing)
                {
                    try
                    {
                        SerializationUtil.XDocumentFromXmlText(xml);
                    }
                    catch (FormatException ex)
                    {
                        throw CreateException(ex.Message);
                    }

                    // The old parser optionally validates the XML against the XHtml schema, with code like this:
                    //
                    //     if (doc != null && _settings.ValidateFhirXhtml)
                    //     {
                    //         var errrorMessages = SerializationUtil.RunFhirXhtmlSchemaValidation(doc);
                    //         if (errorMessages.Any()) throw .....
                    //     }
                    //
                    // where 'doc' is the result of XDocumentFromXmlText().
                    //
                    // ValidateFhirXhtml is by default false and cannot be set via ParserSettings though
                }
                SetHasNonEmptyElements();
                return xml;
            }
            return null;
        }

        public Resource GetResource(Type targetType = null)
        {
            string resourceType;
            if (targetType == null || targetType.IsAbstract)
            {
                resourceType = DetermineResourceType();
            }
            else
            {
                resourceType = _model.GetFhirTypeNameForType(targetType);
                if (resourceType == null)
                {
                    throw CreateUnknownResourceTypeException(targetType.Name);
                }
            }
            var result = _model.CreateResource(resourceType);
            if (result == null)
            {
                throw CreateUnknownResourceTypeException(resourceType);
            }
            var isRoot = !_states.Any();
            if (PopulateBase(result, isRoot))
            {
                if (!isRoot)
                {
                    SetHasNonEmptyElements();
                }
                return result;
            }
            return null;
        }

        public void PopulateListItem(List<Resource> items, int index)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (index != items.Count) throw new ArgumentOutOfRangeException(nameof(index));
            var resource = GetResource();
            if (resource != null)
            {
                items.Add(resource);
            }
        }

        public void PopulateListItem<TItem>(List<TItem> items, int index) where TItem : Base, new()
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (index != items.Count) throw new ArgumentOutOfRangeException(nameof(index));
            var item = Populate((TItem)null);
            if (item != null)
            {
                items.Add(item);
            }
        }

        public void PopulatePrimitiveListItemValue(List<Base64Binary> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Code> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue<TEnum>(List<Code<TEnum>> items, int index) where TEnum : struct
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<FhirBoolean> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Date> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<FhirDateTime> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Instant> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Time> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<FhirString> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<FhirUri> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Url> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Uuid> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Oid> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Id> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Canonical> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Markdown> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<Integer> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<PositiveInt> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<UnsignedInt> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItemValue(List<FhirDecimal> items, int index)
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, PopulateValue(items[index]));
            }
        }

        public void PopulatePrimitiveListItem<TItem>(List<TItem> items, int index) where TItem : Primitive, new()
        {
            if (ShouldSetPrimitiveListItem(items, index))
            {
                SetPrimitiveListItem(items, index, Populate(items[index]));
            }
        }

        public void CheckDuplicates<T>(Element element, string rootName)
        {
            if (!(element is null || element is T)
                && !_settings.PermissiveParsing)
            {
                throw CreateRepeatedElementException($"{rootName}[x]");
            }
        }

        public Base64Binary PopulateValue(Base64Binary fhirBase64Binary)
        {
            if (IsTokenTypeString())
            {
                if (!_reader.TryGetBytesFromBase64(out var value))
                {
                    ThrowIfStrictParsing($"'\"{SourceHelpers.Truncate(GetTokenAsString())}\"' is not a valid base64 binary");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirBase64Binary == null)
                    {
                        fhirBase64Binary = new Base64Binary();
                    }
                    fhirBase64Binary.Value = value;
                }
            }
            return fhirBase64Binary;
        }

        public Code PopulateValue(Code fhirCode)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirCode == null)
                {
                    fhirCode = new Code();
                }
                fhirCode.Value = value;
            }
            return fhirCode;
        }

        public Code<TEnum> PopulateValue<TEnum>(Code<TEnum> fhirCode) where TEnum : struct
        {
            if (TryGetNonEmptyString(out var code))
            {
                var codeValue = EnumUtility.ParseLiteral<TEnum>(code);
                if (codeValue == null)
                {
                    if (!_settings.AllowUnrecognizedEnums)
                    {
                        throw CreateException($"'{code}' is not a valid {EnumUtility.GetName<TEnum>()}");
                    }
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirCode == null)
                    {
                        fhirCode = new Code<TEnum>();
                    }
                    fhirCode.Value = codeValue;
                }
            }
            return fhirCode;
        }

        public FhirBoolean PopulateValue(FhirBoolean fhirBoolean)
        {
            if (TryGetBoolean(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirBoolean == null)
                {
                    fhirBoolean = new FhirBoolean();
                }
                fhirBoolean.Value = value;
            }
            return fhirBoolean;
        }

        public Date PopulateValue(Date fhirDate)
        {
            if (TryGetString(out var value))
            {
                if (!SourceHelpers.IsValidDate(value))
                {
                    ThrowIfStrictParsing($"'{value}' is not a valid date");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirDate == null)
                    {
                        fhirDate = new Date();
                    }
                    fhirDate.Value = value;
                }
            }
            return fhirDate;
        }

        public FhirDateTime PopulateValue(FhirDateTime fhirDateTime)
        {
            if (TryGetString(out var value))
            {
                if (!SourceHelpers.IsValidDate(value)
                    && !SourceHelpers.TryParseFhirInstant(value, out var _))
                {
                    ThrowIfStrictParsing($"'{value}' is not a valid date-time");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirDateTime == null)
                    {
                        fhirDateTime = new FhirDateTime();
                    }
                    fhirDateTime.Value = value;
                }
            }
            return fhirDateTime;
        }

        public Instant PopulateValue(Instant fhirInstant)
        {
            if (TryGetString(out var valueString))
            {
                if (!SourceHelpers.TryParseFhirInstant(valueString, out var value))
                {
                    ThrowIfStrictParsing($"'{valueString}' is not a valid instant");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirInstant == null)
                    {
                        fhirInstant = new Instant();
                    }
                    fhirInstant.Value = value;
                }
            }
            return fhirInstant;
        }

        public Time PopulateValue(Time fhirTime)
        {
            if (TryGetString(out var value))
            {
                if (!SourceHelpers.IsValidTime(value))
                {
                    ThrowIfStrictParsing($"'{value}' is not a valid time");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirTime == null)
                    {
                        fhirTime = new Time();
                    }
                    fhirTime.Value = value;
                }
            }
            return fhirTime;
        }

        public FhirString PopulateValue(FhirString fhirString)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirString == null)
                {
                    fhirString = new FhirString();
                }
                fhirString.Value = value;
            }
            return fhirString;
        }

        public Markdown PopulateValue(Markdown fhirMarkdown)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirMarkdown == null)
                {
                    fhirMarkdown = new Markdown();
                }
                fhirMarkdown.Value = value;
            }
            return fhirMarkdown;
        }

        public FhirUri PopulateValue(FhirUri fhirUri)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirUri == null)
                {
                    fhirUri = new FhirUri();
                }
                fhirUri.Value = value;
            }
            return fhirUri;
        }

        public Url PopulateValue(Url fhirUrl)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirUrl == null)
                {
                    fhirUrl = new Url();
                }
                fhirUrl.Value = value;
            }
            return fhirUrl;
        }

        public Uuid PopulateValue(Uuid fhirUuid)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirUuid == null)
                {
                    fhirUuid = new Uuid();
                }
                fhirUuid.Value = value;
            }
            return fhirUuid;
        }

        public Oid PopulateValue(Oid fhirOid)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirOid == null)
                {
                    fhirOid = new Oid();
                }
                fhirOid.Value = value;
            }
            return fhirOid;
        }

        public Canonical PopulateValue(Canonical fhirCanonical)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirCanonical == null)
                {
                    fhirCanonical = new Canonical();
                }
                fhirCanonical.Value = value;
            }
            return fhirCanonical;
        }

        public Integer PopulateValue(Integer fhirInteger)
        {
            if (IsTokenTypeNumber())
            {
                if (!_reader.TryGetInt32(out var value))
                {
                    ThrowIfStrictParsing($"'{GetTokenAsString()}' is not a valid integer");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirInteger == null)
                    {
                        fhirInteger = new Integer();
                    }
                    fhirInteger.Value = value;
                }
            }
            return fhirInteger;
        }

        public PositiveInt PopulateValue(PositiveInt fhirPositiveInt)
        {
            if (IsTokenTypeNumber())
            {
                if (!_reader.TryGetInt32(out var value) || value <= 0)
                {
                    ThrowIfStrictParsing($"'{GetTokenAsString()}' is not a valid positive integer");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirPositiveInt == null)
                    {
                        fhirPositiveInt = new PositiveInt();
                    }
                    fhirPositiveInt.Value = value;
                }
            }
            return fhirPositiveInt;
        }

        public UnsignedInt PopulateValue(UnsignedInt fhirUnsignedInt)
        {
            if (IsTokenTypeNumber())
            {
                if (!_reader.TryGetInt32(out var value) || value < 0)
                {
                    ThrowIfStrictParsing($"'{GetTokenAsString()}' is not a valid unsigned integer");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirUnsignedInt == null)
                    {
                        fhirUnsignedInt = new UnsignedInt();
                    }
                    fhirUnsignedInt.Value = value;
                }
            }
            return fhirUnsignedInt;
        }

        public FhirDecimal PopulateValue(FhirDecimal fhirDecimal)
        {
            if (IsTokenTypeNumber())
            {
                if (!_reader.TryGetDecimal(out var value))
                {
                    ThrowIfStrictParsing($"'{GetTokenAsString()}' is not a valid decimal");
                }
                else
                {
                    SetHasNonEmptyElements();
                    if (fhirDecimal == null)
                    {
                        fhirDecimal = new FhirDecimal();
                    }
                    fhirDecimal.Value = value;
                }
            }
            return fhirDecimal;
        }

        public Id PopulateValue(Id fhirId)
        {
            if (TryGetNonEmptyString(out var value))
            {
                SetHasNonEmptyElements();
                if (fhirId == null)
                {
                    fhirId = new Id();
                }
                fhirId.Value = value;
            }
            return fhirId;
        }

        public T Populate<T>(T element) where T: Base, new()
        {
            var elementToPopulate = element ?? new T();
            if (PopulateBase(elementToPopulate, isRoot: false))
            {
                SetHasNonEmptyElements();
                return elementToPopulate;
            }
            return element;
        }

        public void SetList(Base element, string jsonPropertyName)
        {
            var elementName = GetElementName(jsonPropertyName, out var _);
            var currentState = _states.Peek();
            var elementsWithNull = currentState.ListElementsWithNull;
            var hadNulls = elementsWithNull.ContainsKey(elementName);

            if (_reader.TokenType != JsonTokenType.StartArray)
            {
                if (!_settings.PermissiveParsing)
                {
                    throw CreateUnexpectedTokenTypeException("'['");
                }
                element.SetListElementFromJson(jsonPropertyName, 0, ref this);
            }
            else
            {
                var index = 0;
                while (_reader.Read() && _reader.TokenType != JsonTokenType.EndArray)
                {
                    currentState.CurrentArrayIndex = index;
                    element.SetListElementFromJson(jsonPropertyName, index, ref this);
                    index++;
                }
                currentState.CurrentArrayIndex = null;
            }

            if (hadNulls)
            {
                ProcessListElementWithNulls(elementsWithNull, elementName, hasMatchingProperty: true);
                elementsWithNull.Remove(elementName);
            }
        }

        private bool PopulateBase(Base element, bool isRoot)
        {
            if (_reader.TokenType != JsonTokenType.StartObject)
            {
                if (isRoot || !_settings.PermissiveParsing)
                {
                    throw CreateUnexpectedTokenTypeException("'{'");
                }
                _reader.Skip();
                return false;
            }
            var seenProperties = new HashSet<string>();
            var state = new State();
            _states.Push(state);
            while (_reader.Read() && _reader.TokenType == JsonTokenType.PropertyName)
            {
                var jsonPropertyName = _reader.GetString();
                _reader.Read();
                if (!seenProperties.Contains(jsonPropertyName))
                {
                    seenProperties.Add(jsonPropertyName);
                }
                else if (!_settings.PermissiveParsing)
                {
                    var elementName = GetElementName(jsonPropertyName, out var _);
                    throw CreateRepeatedElementException(elementName);
                }
                state.CurrentPropertyName = jsonPropertyName;
                if (!element.SetElementFromJson(jsonPropertyName, ref this))
                {
                    if (jsonPropertyName == "fhir_comments")
                    {
                        if (_settings.Version != Model.Version.DSTU2)
                        {
                            ThrowIfStrictParsing("The 'fhir_comments' feature is disabled");
                        }
                    }
                    else if (!_settings.AcceptUnknownMembers)
                    {
                        throw CreateException($"Unrecognized element '{jsonPropertyName}'");
                    }
                    _reader.Skip();
                }
            }
            var elementsWithNull = state.ListElementsWithNull;
            foreach (var elementName in elementsWithNull.Keys)
            {
                ProcessListElementWithNulls(elementsWithNull, elementName, hasMatchingProperty: false);
            }
            elementsWithNull.Clear();
            _states.Pop();
            if (!state.HasNonEmptyElements)
            {
                if (isRoot || !_settings.PermissiveParsing)
                {
                    throw CreateException("Empty objects are not allowed");
                }
                return false;
            }
            return true;
        }

        private bool ShouldSetPrimitiveListItem<TItem>(List<TItem> items, int index) where TItem : Primitive
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (index < 0 || index > items.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (_reader.TokenType == JsonTokenType.Null)
            {
                if (index == items.Count)
                {
                    items.Add(null);
                    CurrentElementHasNull(items);
                }
                else if (items[index] == null && !_settings.PermissiveParsing)
                {
                    throw CreateUnexpectedTokenTypeException("a value");
                }
                return false;
            }

            if (index == items.Count)
            {
                items.Add(null);
            }
            return true;
        }

        private void SetPrimitiveListItem<TItem>(List<TItem> items, int index, TItem item) where TItem: Primitive
        {
            if (item != null)
            {
                items[index] = item;
            }
            else
            {
                CurrentElementHasNull(items);
            }
        }

        private void CurrentElementHasNull<TItem>(List<TItem> items)
        {
            var state = _states.Peek();
            var elementName = GetElementName(state.CurrentPropertyName, out var isShadowProperty);
            var elementsWithNull = _states.Peek().ListElementsWithNull;
            if (!elementsWithNull.ContainsKey(elementName))
            {
                elementsWithNull[elementName] = new ListElementDescription(items, isShadowProperty);
            }
        }

        private static string GetElementName(string jsonPropertyName, out bool isShadowProperty)
        {
            isShadowProperty = jsonPropertyName[0] == '_';
            return isShadowProperty ?
                jsonPropertyName.Substring(1) :
                jsonPropertyName;
        }

        private void ProcessListElementWithNulls(Dictionary<string, ListElementDescription> listElementsWithNulls, string elementName, bool hasMatchingProperty)
        {
            var propertyDescription = listElementsWithNulls[elementName];
            var items = propertyDescription.Items;
            // Check if the element still has nulls - normally they should all have been populated after both 'x' and '_x' properties have been processed
            if (items.Contains(null))
            {
                if (!_settings.PermissiveParsing)
                {
                    // We still have nulls, if we are not in permissive parsing mode that is an error
                    var jsonPropertyWithExtraNulls = propertyDescription.IsShadow ?
                        "_" + elementName :
                        elementName;
                    var jsonMatchingProperty = propertyDescription.IsShadow ?
                        elementName :
                        "_" + elementName;
                    var message = hasMatchingProperty ?
                        $"The '{jsonPropertyWithExtraNulls}' property has one or more 'null'(s) not matched by values in the '{jsonMatchingProperty}' property" :
                        $"The '{jsonPropertyWithExtraNulls}' property has one or more 'null'(s) and no matching '{jsonMatchingProperty}' property";
                    throw CreateException(message);
                }
                // . . .in permissive parsing mode we do not throw an error and we just remove the nulls
                for (var i = items.Count - 1; i >= 0; i--)
                {
                    if (items[i] == null)
                    {
                        items.RemoveAt(i);
                    }
                }
            }
        }

        private string DetermineResourceType()
        {
            var savedReader = _reader;
            try
            {
                var atDepth = _reader.CurrentDepth + 1;

                while (_reader.Read() && _reader.CurrentDepth >= atDepth)
                {
                    if (_reader.TokenType == JsonTokenType.PropertyName && _reader.CurrentDepth == atDepth)
                    {
                        var propName = _reader.GetString();

                        if (propName == ResourceTypePropertyName)
                        {
                            _reader.Read();
                            if (_reader.TokenType != JsonTokenType.String)
                            {
                                throw CreateNotAStringException();
                            }
                            var result = _reader.GetString();
                            if (string.IsNullOrWhiteSpace(result))
                            {
                                throw CreateEmptyStringException();
                            }
                            return result;
                        } 
                    }
                }
                throw CreateException($"Missing '{ResourceTypePropertyName}' property");
            }
            finally
            {
                _reader = savedReader;
            }
        }

        private void SetHasNonEmptyElements()
        {
            _states.Peek().HasNonEmptyElements = true;
        }

        private bool TryGetNonEmptyString(out string value)
        {
            if (!TryGetString(out value))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!_settings.PermissiveParsing)
                {
                    throw CreateEmptyStringException();
                }
                return false;
            }
            return true;
        }

        private bool TryGetString(out string value)
        {
            if (IsTokenTypeString())
            {
                value = _reader.GetString();
                return true;
            }
            value = null;
            return false;
        }

        private bool TryGetBoolean(out bool value)
        {
            switch (_reader.TokenType)
            {
                case JsonTokenType.True:
                    value = true;
                    return true;
                case JsonTokenType.False:
                    value = false;
                    return true;
            }
            if (!_settings.PermissiveParsing)
            {
                throw CreateUnexpectedTokenTypeException("a boolean");
            }

            _reader.Skip();
            value = default;
            return false;
        }

        private bool IsTokenTypeString()
        {
            if (_reader.TokenType == JsonTokenType.String)
            {
                return true;
            }

            if (!_settings.PermissiveParsing)
            {
                throw CreateNotAStringException();
            }

            _reader.Skip();
            return false;
        }

        private bool IsTokenTypeNumber()
        {
            if (_reader.TokenType == JsonTokenType.Number)
            {
                return true;
            }

            if (!_settings.PermissiveParsing)
            {
                throw CreateUnexpectedTokenTypeException("a number");
            }

            _reader.Skip();
            return false;
        }

        private void ThrowIfStrictParsing(string message)
        {
            if (!_settings.PermissiveParsing)
            {
                throw CreateException(message);
            }
        }

        private SourceException CreateWrongResourceTypeException(string expectedResourceType, string actualResourceType)
        {
            return CreateException($"Expected a {expectedResourceType} but found a {actualResourceType}");
        }

        private SourceException CreateUnknownResourceTypeException(string resourceType)
        {
            return CreateException($"Unknown resource type '{resourceType}'");
        }

        private SourceException CreateNotAStringException()
        {
            return CreateUnexpectedTokenTypeException("a string");
        }

        private SourceException CreateUnexpectedTokenTypeException(string expected)
        {
            return CreateException($"Expected {expected} but found {TokenDescription()}");
        }

        private SourceException CreateEmptyStringException()
        {
            return CreateException("Empty strings are not allowed");
        }

        private SourceException CreateRepeatedElementException(string elementName)
        {
            return CreateException($"Element '{elementName}' must not repeat");
        }

        private SourceException CreateException(string message)
        {
            return new SourceException(message, GetCurrentPath(), GETLINENUMBER.Value(_reader.CurrentState), GETPOSITION.Value(_reader.CurrentState) );
        }

        private string GetCurrentPath()
        {
            var result = string.Empty;
            foreach (var state in _states.Reverse())
            {
                if (state.CurrentPropertyName != null)
                {
                    if (result.Length > 0)
                    {
                        result += ".";
                    }
                    result += state.CurrentPropertyName;
                }
                if (state.CurrentArrayIndex != null)
                {
                    result += $"[{state.CurrentArrayIndex}]";
                }
            }
            return result;
        }

        private string TokenDescription()
        {
            switch (_reader.TokenType)
            {
                case JsonTokenType.String:
                    return $"a string ('\"{SourceHelpers.Truncate(_reader.GetString())}\"')";
                case JsonTokenType.Number:
                    return $"a number ('{GetTokenAsString()}')";
                case JsonTokenType.True:
                    return $"a boolean ('true')";
                case JsonTokenType.False:
                    return $"a boolean ('false')";
                case JsonTokenType.Null:
                    return $"'null'";
                case JsonTokenType.StartObject:
                    return "'{'";
                case JsonTokenType.EndObject:
                    return "'}'";
                case JsonTokenType.StartArray:
                    return "'['";
                case JsonTokenType.EndArray:
                    return "']'";
                case JsonTokenType.Comment:
                    return $"a comment ('{SourceHelpers.Truncate(_reader.GetComment())}')";
                case JsonTokenType.PropertyName:
                    return $"a property ('{_reader.GetString()}')";
                default:
                    return _reader.TokenType.ToString();
            }
        }

        private string GetTokenAsString()
        {
            var span = _reader.HasValueSequence ?
                _reader.ValueSequence.ToArray() :
                _reader.ValueSpan;
            return Encoding.UTF8.GetString(span.ToArray());
        }

        private class State
        {
            public Dictionary<string, ListElementDescription> ListElementsWithNull { get; } = new Dictionary<string, ListElementDescription>();

            public string CurrentPropertyName { get; set; } = null;

            public int? CurrentArrayIndex { get; set; } = null;

            public bool HasNonEmptyElements { get; set; } = false;
        }

        private struct ListElementDescription
        {
            public ListElementDescription(System.Collections.IList items, bool isShadow)
            {
                Items = items ?? throw new ArgumentNullException(nameof(items));
                IsShadow = isShadow;
            }

            public System.Collections.IList Items { get; }
            public bool IsShadow { get; }
        }

        // While we are waiting for this https://github.com/dotnet/runtime/issues/28482,
        // there's no other option than to just force our way to these valuable properties.
        private static readonly Lazy<Func<JsonReaderState, long>> GETLINENUMBER =
            new Lazy<Func<JsonReaderState, long>>(() => GetField<JsonReaderState, long>("_lineNumber"));
        private static readonly Lazy<Func<JsonReaderState, long>> GETPOSITION =
            new Lazy<Func<JsonReaderState, long>>(() => GetField<JsonReaderState, long>("_bytePositionInLine"));

        private static Func<C, T> GetField<C, T>(string fieldName)
        {
            var field = typeof(C).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field is null) throw new ArgumentException($"Cannot find field {fieldName} in type {typeof(C).Name}.", nameof(fieldName));
            return getField;

            T getField(C instance) => (T)field.GetValue(instance);
        }

        private Utf8JsonReader _reader;
        private readonly ParserSettings _settings;
        private readonly IModelInfo _model;
        private readonly Stack<State> _states;
    }
}