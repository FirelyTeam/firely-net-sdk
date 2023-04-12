using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal class ParserSource
    {
        public ParserSource(IParserOrigin origin, ParserSettings settings)
        {
            _origin = origin ?? throw new ArgumentNullException(nameof(origin));
            _origin.SetErrorHandler(ErrorHandler);
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _model = ModelInfos.Get(_settings.Version);
            _states = new Stack<State>();
        }

        public bool IsVersion(Model.Version versions)
        {
            return (versions & _settings.Version) != 0;
        }

        public string GetElementId()
        {
            return GetNonEmptyString();
        }

        public string GetExtensionUrl()
        {
            return GetNonEmptyString();
        }

        public string GetXHtml()
        {
            if (!_origin.TryReadXHtmlElement(out var xHtml))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return xHtml;
        }

        public byte[] GetBase64BinaryValue()
        {
            if (!_origin.TryReadBytesValue(out var value))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetCodeValue()
        {
            return GetNonEmptyString();
        }

        public TEnum? GetCodeValue<TEnum>() where TEnum : struct
        {
            if (!TryGetNonEmptyString(out var code))
            {
                return null;
            }
            var codeValue = EnumUtility.ParseLiteral<TEnum>(code);
            if (codeValue == null)
            {
                if (!_settings.AllowUnrecognizedEnums)
                {
                    throw CreateException($"'{code}' is not a valid {EnumUtility.GetName<TEnum>()}");
                }
                return null;
            }
            SetHasNonEmptyElements();
            return codeValue;
        }

        public bool? GetFhirBooleanValue()
        {
            if (!_origin.TryReadBooleanValue(out var value))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetDateValue()
        {
            if (!TryGetNonEmptyString(out var value))
            {
                return null;
            }
            if (!SourceHelpers.IsValidDate(value))
            {
                ThrowIfStrictParsing($"'{value}' is not a valid date");
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetFhirDateTimeValue()
        {
            if (!TryGetNonEmptyString(out var value))
            {
                return null;
            }
            if (!SourceHelpers.IsValidDate(value)
                && !SourceHelpers.TryParseFhirInstant(value, out var _))
            {
                ThrowIfStrictParsing($"'{value}' is not a valid date-time");
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public DateTimeOffset? GetInstantValue()
        {
            if (!_origin.TryReadDateTimeOffsetValue(out var value))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetTimeValue()
        {
            if (!TryGetNonEmptyString(out var value))
            {
                return null;
            }
            if (!SourceHelpers.IsValidTime(value))
            {
                ThrowIfStrictParsing($"'{value}' is not a valid time");
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetFhirStringValue()
        {
            return GetNonEmptyString();
        }

        public string GetMarkdownValue()
        {
            return GetNonEmptyString();
        }

        public string GetFhirUriValue()
        {
            return GetNonEmptyString();
        }

        public string GetUrlValue()
        {
            return GetNonEmptyString();
        }

        public string GetUuidValue()
        {
            return GetNonEmptyString();
        }

        public string GetOidValue()
        {
            return GetNonEmptyString();
        }

        public string GetCanonicalValue()
        {
            return GetNonEmptyString();
        }

        public int? GetIntegerValue()
        {
            if (!_origin.TryReadIntegerValue(out var value))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public int? GetPositiveIntValue()
        {
            if (!_origin.TryReadIntegerValue(out var value))
            {
                return null;
            }
            if (value <= 0)
            {
                ThrowIfStrictParsing($"'{value}' is not a valid positive integer");
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public int? GetUnsignedIntValue()
        {
            if (!_origin.TryReadIntegerValue(out var value))
            {
                return null;
            }
            if (value < 0)
            {
                ThrowIfStrictParsing($"'{value}' is not a valid unsigned integer");
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public decimal? GetFhirDecimalValue()
        {
            if (!_origin.TryReadDecimalValue(out var value))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return value;
        }

        public string GetIdValue()
        {
            return GetNonEmptyString();
        }

        public TBase Get<TBase>() where TBase : Base, new()
        {
            var result = new TBase();
            if (PopulateBaseCheckEmpty(result))
            {
                return result;
            }
            return null;
        }

        public List<TBase> GetList<TBase>() where TBase : Base, new()
        {
            return GetListPrimitive(
                () => Get<TBase>()
            );
        }

        public Resource GetResource()
        {
            Resource result = null;
            foreach (var resourceType in _origin.EnumerateResource())
            {
                result = CreateResource(resourceType);
                if (!PopulateBaseCheckEmpty(result))
                {
                    result = null;
                }
            }
            return result;
        }

        public Base GetRoot(Type targetType)
        {
            Base result = null;
            foreach (var resourceType in _origin.EnumerateResource())
            {
                if (targetType == null || targetType.IsAbstract || typeof(Resource).IsAssignableFrom(targetType))
                {
                    result = CreateResource(resourceType);
                    if (targetType != null && !targetType.IsAssignableFrom(result.GetType()))
                    {
                        var expectedType = _model.GetFhirTypeNameForType(targetType) ?? targetType.Name;
                        throw CreateException($"Expected a {expectedType} but found a {resourceType}");
                    }
                }
                else
                {
                    if (!typeof(Base).IsAssignableFrom(targetType) || _model.GetFhirTypeNameForType(targetType) == null)
                    {
                        throw CreateException($"Unknown resource type or data type '{targetType.Name}'");
                    }
                    // This is slow but it is a very rare case that we want to create directly a data type
                    result = (Base)Activator.CreateInstance(targetType);
                }
                // We accept root empty element (as we do for JSON because we consider the resourceType property enough to make it non-empty)
                PopulateBase(result);
            }
            return result;
        }

        public List<Resource> GetResourceList()
        {
            return GetListPrimitive(
                () => GetResource()
            );
        }

        public void CheckDuplicates<TElement>(Element element, string rootName) where TElement : Element
        {
            if (!(element is null || element is TElement)
                && !_settings.PermissiveParsing)
            {
                throw CreateException($"Element '{rootName}[x]' must not repeat");
            }
        }

        private string GetNonEmptyString()
        {
            if (!TryGetNonEmptyString(out var result))
            {
                return null;
            }
            SetHasNonEmptyElements();
            return result;
        }

        private bool TryGetNonEmptyString(out string value)
        {
            if (!_origin.TryReadStringValue(out value))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!_settings.PermissiveParsing)
                {
                    throw CreateException("Empty strings are not allowed");
                }
                return false;
            }
            return true;
        }

        private Resource CreateResource(string resourceType)
        {
            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw CreateException($"Missing resource type");
            }
            return _model.CreateResource(resourceType)
                ?? throw CreateException($"Unknown resource type '{resourceType}'");
        }

        private List<TBase> GetListPrimitive<TBase>(Func<TBase> get) where TBase : Base
        {
            var result = new List<TBase>();
            var currentState = _states.Peek();
            foreach (var index in _origin.EnumerateList())
            {
                currentState.CurrentListIndex = index;
                var item = get();
                if (item != null)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private bool PopulateBaseCheckEmpty(Base element)
        {
            if (!PopulateBase(element))
            {
                ThrowEmptyNotAllowedIfStrictParsing();
                return false;
            }
            SetHasNonEmptyElements();
            return true;
        }

        private bool PopulateBase(Base element)
        {
            var state = new State();
            _states.Push(state);
            foreach (var attributeName in _origin.EnumerateAttributes())
            {
                var elementName = $"@{attributeName}";
                if (!SetElementFromSource(elementName))
                {
                    if (!_settings.AcceptUnknownMembers)
                    {
                        throw CreateException($"Unknown attribute '{attributeName}'");
                    }
                    _origin.Skip();
                }
            }
            foreach (var elementName in _origin.EnumerateElements())
            {
                if (!SetElementFromSource(elementName))
                {
                    if (!_settings.AcceptUnknownMembers)
                    {
                        throw CreateException($"Encountered unknown element '{elementName}'");
                    }
                    _origin.Skip();
                }
            }
            _states.Pop();
            return state.HasNonEmptyElements;

            bool SetElementFromSource(string elementName)
            {
                state.CurrentElementName = elementName;
                return element.SetElementFromSource(elementName, this);
            }
        }

        private void ErrorHandler(ParserErrorCategory category, string message, long? lineNumber, long? bytePositionInLine)
        {
            if (!IgnoreError())
            {
                throw new SourceException(message, GetCurrentPath(), lineNumber, bytePositionInLine);
            }

            bool IgnoreError()
            {
                switch (category)
                {
                    case ParserErrorCategory.InvalidValue:
                    case ParserErrorCategory.UnexpectedInput:
                    case ParserErrorCategory.EmptyMember:
                        return _settings.PermissiveParsing;
                    case ParserErrorCategory.UnknownMember:
                        return _settings.AcceptUnknownMembers;
                    case ParserErrorCategory.MalformedInput:
                        return false;
                    default:
                        throw new ArgumentException($"Unknown or not supported {nameof(ParserErrorCategory)} '{category}'", nameof(category));
                }
            }
        }

        private void ThrowEmptyNotAllowedIfStrictParsing()
        {
            ThrowIfStrictParsing("Empty elements are not allowed");
        }

        private void ThrowIfStrictParsing(string message)
        {
            if (!_settings.PermissiveParsing)
            {
                throw CreateException(message);
            }
        }

        private SourceException CreateException(string message)
        {
            return new SourceException(message, GetCurrentPath(), _origin.GetLineNumber(), _origin.GetBytePositionInLine());
        }

        private void SetHasNonEmptyElements()
        {
            _states.Peek().HasNonEmptyElements = true;
        }

        private string GetCurrentPath()
        {
            var result = string.Empty;
            foreach (var state in _states.Reverse())
            {
                if (state.CurrentElementName != null)
                {
                    if (result.Length > 0)
                    {
                        result += ".";
                    }
                    result += state.CurrentElementName;
                }
                if (state.CurrentListIndex != null)
                {
                    result += $"[{state.CurrentListIndex}]";
                }
            }
            return result;
        }

        private class State
        {
            public string CurrentElementName { get; set; } = null;

            public int? CurrentListIndex { get; set; } = null;

            public bool HasNonEmptyElements { get; set; } = false;
        }

        private readonly IParserOrigin _origin;
        private readonly ParserSettings _settings;
        private readonly IModelInfo _model;
        private readonly Stack<State> _states;
    }
}
