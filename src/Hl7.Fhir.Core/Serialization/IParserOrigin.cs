using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Gets data to be converted to FHIR objects via a parser
    /// </summary>
    public interface IParserOrigin
    {
        /// <summary>
        /// Set the handler for error. Called once before starting processing data.
        /// </summary>
        /// <param name="errorHandler"></param>
        void SetErrorHandler(ParserErrorHandler errorHandler);

        /// <summary>
        /// Tries to read a string value. Does not move the current position in the input.
        /// </summary>
        bool TryReadStringValue(out string value);

        /// <summary>
        /// Tries to read a boolean value. Does not move the current position in the input.
        /// </summary>
        bool TryReadBooleanValue(out bool value);

        /// <summary>
        /// Tries to read an integer value. Does not move the current position in the input.
        /// </summary>
        bool TryReadIntegerValue(out int value);

        /// <summary>
        /// Tries to read a decimal value. Does not move the current position in the input.
        /// </summary>
        bool TryReadDecimalValue(out decimal value);

        /// <summary>
        /// Tries to read a DateTimeOffset (instant) value. Does not move the current position in the input.
        /// </summary>
        bool TryReadDateTimeOffsetValue(out DateTimeOffset value);

        /// <summary>
        /// Tries to read a byte array (binary data) value. Does not move the current position in the input.
        /// </summary>
        bool TryReadBytesValue(out byte[] value);

        /// <summary>
        /// Tries to read an XHTML fragment. CAN move the current position in the input past the fragment.
        /// </summary>
        bool TryReadXHtmlElement(out string value);

        /// <summary>
        /// Enumerate the next resource in the input, returning its resource type.
        /// It returns either zero or one item, positioning the input on that one element when it is returned.
        /// The returned resource type can be null if the resource is actually a data type.
        /// The parser enumerates always to the end to be sure that the input gets positioned correctly after the resource.
        /// </summary>
        IEnumerable<string> EnumerateResource();

        /// <summary>
        /// Enumerates all the attributes at the current input position, returning the name of each attribute
        /// Possible attributes are the element id ('id'), the extension URL ('url') and a primitive element value ('value')
        /// </summary>
        IEnumerable<string> EnumerateAttributes();

        /// <summary>
        /// Enumerates all the elements at the current input position, returning the name of each element
        /// </summary>
        IEnumerable<string> EnumerateElements();

        /// <summary>
        /// Enumerates all the elements in the list at the current input position, returning their index.
        /// </summary>
        IEnumerable<int> EnumerateList();

        /// <summary>
        /// Skip past the current element, called when an error prevent processing the current element
        /// </summary>
        void Skip();

        /// <summary>
        /// Gets the 0-based line number of the current position in the input. Null if the input does not have lines.
        /// </summary>
        long? GetLineNumber();

        /// <summary>
        /// Gets the 0-based current position withing the current line in the input. Null if the input does not have lines.
        /// </summary>
        long? GetBytePositionInLine();
    }

    /// <summary>
    /// Possible error categories passed to <see cref="ParserErrorHandler"/>
    /// </summary>
    public enum ParserErrorCategory
    {
        /// <summary>
        /// The input is malformed / cannot be interpreted. Non recoverable.
        /// </summary>
        MalformedInput,

        /// <summary>
        /// The input contains an unexpected token or value. Recoverable.
        /// </summary>
        UnexpectedInput,

        /// <summary>
        /// The input contains an invalid value - e.g. an alphabetical value where an integer is expected. Recoverable.
        /// </summary>
        InvalidValue,

        /// <summary>
        /// The input contains an empty attribute or element. Recoverable.
        /// </summary>
        EmptyMember,

        /// <summary>
        /// The input contains an unknown attribute or element. Recoverable.
        /// </summary>
        UnknownMember,
    }

    /// <summary>
    /// Handle errors occurred while getting data
    /// </summary>
    /// <param name="category">Overall category of the error</param>
    /// <param name="message">Error message</param>
    /// <param name="lineNumber">Optional line number where the error occurred in the source file. 0-based</param>
    /// <param name="bytePositionInLine">Optional position within the line where the error occurred in the source file. 0-based.</param>
    public delegate void ParserErrorHandler(ParserErrorCategory category, string message, long? lineNumber, long? bytePositionInLine);
}
