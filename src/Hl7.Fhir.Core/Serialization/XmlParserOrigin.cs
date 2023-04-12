using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    internal class XmlParserOrigin : IParserOrigin
    {
        public XmlParserOrigin(XmlReader reader, bool disallowXsiAttributesOnRoot)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _disallowXsiAttributesOnRoot = disallowXsiAttributesOnRoot;
            _atBeginning = true;
        }

        public void SetErrorHandler(ParserErrorHandler errorHandler)
        {
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
        }

        public bool TryReadStringValue(out string value)
        {
            value = GetTrimmedValue();
            return true;
        }

        public bool TryReadBooleanValue(out bool value)
        {
            var valueString = GetTrimmedValue();
            switch (valueString)
            {
                case "true":
                    value = true;
                    break;
                case "false":
                    value = false;
                    break;
                default:
                    OnError(ParserErrorCategory.InvalidValue, $"'{valueString}' is not a valid boolean");
                    value = default;
                    return false;
            }
            return true;
        }

        public bool TryReadIntegerValue(out int value)
        {
            var valueString = GetTrimmedValue();
            if (!int.TryParse(valueString, out value))
            {
                OnError(ParserErrorCategory.InvalidValue, $"'{valueString}' is not a valid integer");
                return false;
            }
            return true;
        }

        public bool TryReadDecimalValue(out decimal value)
        {
            var valueString = GetTrimmedValue();
            if (!decimal.TryParse(valueString, out value))
            {
                OnError(ParserErrorCategory.InvalidValue, $"'{valueString}' is not a valid decimal");
                return false;
            }
            return true;
        }

        public bool TryReadDateTimeOffsetValue(out DateTimeOffset value)
        {
            var valueString = GetTrimmedValue();
            if (!SourceHelpers.TryParseFhirInstant(valueString, out value))
            {
                OnError(ParserErrorCategory.InvalidValue, $"'{valueString}' is not a valid instant");
                return false;
            }
            return true;
        }

        public bool TryReadBytesValue(out byte[] value)
        {
            var valueString = GetTrimmedValue();
            try
            {
                value = Convert.FromBase64String(valueString);
                return true;
            }
            catch (FormatException)
            {
                OnError(ParserErrorCategory.InvalidValue, $"'{SourceHelpers.Truncate(valueString)}' is not a valid base64 binary");
                value = null;
                return false;
            }
        }

        public bool TryReadXHtmlElement(out string value)
        {
            // We cannot use ReadOuterXml() because we want to convert \n to \r\n
            var stringWriter = new StringWriter(CultureInfo.InvariantCulture);
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true
            };
            try
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlWriter.WriteNode(_reader, defattr: false);
                }
                value = stringWriter.ToString();
                // Never empty: at the very least we have the root element
                return true;
            }
            catch  (XmlException xmlException)
            {
                OnError(xmlException);
                value = null;
                return false;
            }
        }

        public IEnumerable<string> EnumerateResource()
        {
            if (!_atBeginning)
            {
                return EnumerateInnerResource();
            }

            _atBeginning = false;
            return EnumerateRootResource();

            IEnumerable<string> EnumerateRootResource()
            {
                if (ReaderMoveToContent() != XmlNodeType.Element)
                {
                    OnError(ParserErrorCategory.MalformedInput, $"Unexpected {_reader.NodeType} node");
                }
                else if (_reader.NamespaceURI != _fhirNamespaceURI)
                {
                    var message = string.IsNullOrEmpty(_reader.NamespaceURI) ?
                        $"The element '{_reader.LocalName}' has no namespace, expected the HL7 FHIR namespace ({_fhirNamespaceURI})" :
                        $"The element '{_reader.LocalName}' uses the namespace '{_reader.NamespaceURI}', expected the HL7 FHIR namespace ({_fhirNamespaceURI})";
                    OnError(ParserErrorCategory.MalformedInput, message);
                }
                else
                {
                    yield return _reader.LocalName;
                }
            }

            IEnumerable<string> EnumerateInnerResource()
            {
                // We have: <element><resource> . . .</resource></element> and we are on <element>

                // <element> should not have attributes:

                foreach (var attributeName in EnumerateAttributes())
                {
                    OnError(ParserErrorCategory.UnknownMember, $"Unknown attribute '{attributeName}'");
                }    

                // Move to <resource> enumerating through all the child elements of <element>:

                var first = true;
                foreach (var elementName in EnumerateElements())
                {
                    if (!first)
                    {
                        OnError(ParserErrorCategory.UnexpectedInput, $"Unexpected element '{elementName}'");
                        Skip();
                    }
                    else
                    {
                        yield return elementName;
                        first = false;
                    }
                }
                if (first)
                {
                    OnError(ParserErrorCategory.EmptyMember, "Empty elements are not allowed");
                }
            }
        }

        public IEnumerable<string> EnumerateAttributes()
        {
            if (ReaderMoveToFirstAttribute())
            {
                do
                {
                    if (string.IsNullOrEmpty(_reader.NamespaceURI))
                    {
                        var attributeName = _reader.LocalName;
                        if (IsValidAttributeName(attributeName))
                        {
                            yield return attributeName;
                        }
                    }
                    else if (_disallowXsiAttributesOnRoot && _reader.Depth == 1 && _reader.NamespaceURI == _xsiNamespaceURI)
                    {
                        OnError(ParserErrorCategory.UnexpectedInput, $"The '{_reader.LocalName}' attribute is not allowed");
                        yield break;
                    }
                } while (ReaderMoveToNextAttribute());
                ReaderMoveToElement();
            }
        }

        public IEnumerable<string> EnumerateElements()
        {
            if (!_reader.IsEmptyElement)
            {
                ReaderRead();
                while (MoveToValidElement())
                {
                    yield return _reader.LocalName;
                }
            }
            ReaderRead();
        }

        public IEnumerable<int> EnumerateList()
        {
            var elementName = _reader.LocalName;
            var index = 0;
            do
            {
                yield return index++;
            } while (MoveToValidElement() && _reader.LocalName == elementName && _reader.NamespaceURI == _fhirNamespaceURI);
        }

        public void Skip()
        {
            if (_reader.NodeType == XmlNodeType.Element)
            {
                ReaderSkip();
            }
        }

        public long? GetLineNumber()
        {
            if (_reader is IXmlLineInfo lineInfo && lineInfo.HasLineInfo())
            {
                return lineInfo.LineNumber - 1;
            }
            return null;
        }

        public long? GetBytePositionInLine()
        {
            if (_reader is IXmlLineInfo lineInfo && lineInfo.HasLineInfo())
            {
                return lineInfo.LinePosition - 1;
            }
            return null;
        }

        private bool MoveToValidElement()
        {
            while (true)
            {
                switch (ReaderMoveToContent())
                {
                    case XmlNodeType.None:
                    case XmlNodeType.EndElement:
                        return false;
                    case XmlNodeType.Element:
                        if (IsOnValidElement())
                        {
                            return true;
                        }
                        ReaderSkip();
                        break;
                    default:
                        OnError(ParserErrorCategory.UnexpectedInput, $"Unexpected {_reader.NodeType} node");
                        ReaderSkip();
                        break;
                }
            }
        }

        private bool ReaderMoveToFirstAttribute()
        {
            try
            {
                return _reader.MoveToFirstAttribute();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
                return false;
            }
        }

        private bool ReaderMoveToNextAttribute()
        {
            try
            {
                return _reader.MoveToNextAttribute();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
                return false;
            }
        }

        private bool ReaderMoveToElement()
        {
            try
            {
                return _reader.MoveToElement();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
                return false;
            }
        }

        private bool ReaderRead()
        {
            try
            {
                return _reader.Read();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
                return false;
            }
        }

        private XmlNodeType ReaderMoveToContent()
        {
            try
            {
                return _reader.MoveToContent();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
                return XmlNodeType.None;
            }
        }

        private void ReaderSkip()
        {
            try
            {
                _reader.Skip();
            }
            catch (XmlException xmlException)
            {
                OnError(xmlException);
            }
        }

        private bool IsOnValidElement()
        {
            return _reader.NodeType == XmlNodeType.Element
                && _reader.NamespaceURI == _fhirNamespaceURI || _reader.NamespaceURI == _xhtmlNamespaceURI && _reader.LocalName == "div";
        }

        private static bool IsValidAttributeName(string attributeName)
        {
            return attributeName != "xmlns";
        }

        private string GetTrimmedValue()
        {
            return _reader.Value?.Trim();
        }

        private void OnError(XmlException xmlException)
        {
            OnError(ParserErrorCategory.MalformedInput, $"Invalid XML: {xmlException.Message}", xmlException.LineNumber - 1, xmlException.LinePosition - 1);
        }

        private void OnError(ParserErrorCategory category, string message, long? lineNumber = null, long? bytePositionInLine = null)
        {
            _errorHandler?.Invoke(category, message, lineNumber ?? GetLineNumber(), bytePositionInLine ?? GetBytePositionInLine());
        }

        const string _fhirNamespaceURI = "http://hl7.org/fhir";
        const string _xsiNamespaceURI = "http://www.w3.org/2001/XMLSchema-instance";
        const string _xhtmlNamespaceURI = "http://www.w3.org/1999/xhtml";

        private readonly XmlReader _reader;
        private readonly bool _disallowXsiAttributesOnRoot;
        private bool _atBeginning;
        private ParserErrorHandler _errorHandler;
    }
}