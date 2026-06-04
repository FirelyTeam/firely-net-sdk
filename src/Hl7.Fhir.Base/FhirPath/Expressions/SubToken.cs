/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Language.Debugging;
using Hl7.Fhir.Utility;
using Hl7.FhirPath.Sprache;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.FhirPath.Expressions
{
    /// <summary>
    /// SubTokens are used to represent fragments within a fhirpath expression
    /// that are a sub-part of the grammar, and are not otherwise present in the
    /// expression tree as an Expression of some sort.<br/>
    /// Such as: <code>, . () {} [] '' `` as is</code>
    /// These do not include functions or binary expressions, which are a part of the execution tree<br/>
    /// E.g. `as` is a fragment of the typeExpression
    /// </summary>
    public class SubToken : IPositionAware<SubToken>
    {
        public SubToken(string value, ISourcePositionInfo location = null)
        {
            Value = value;
            Location = location;
        }

        public SubToken(char value, ISourcePositionInfo location = null)
        {
            Value = $"{value}";
            Location = location;
        }

        /// <summary>
        /// The parsed value of this SubToken
        /// </summary>
        /// <remarks>
        /// Derived classes may provide additional information about the value,
        /// such as the type of whitespace (e.g. comments)
        /// </remarks>
        public string Value { get; private set; }

        /// <summary>
        /// Position information of this token within the complete expression that was parsed
        /// </summary>
        public ISourcePositionInfo Location { get; private set; }

        /// <summary>
        /// Any leading whitespace or comments encountered immediately before this SubToken
        /// </summary>
        public IEnumerable<WhitespaceSubToken> LeadingWhitespace { get; internal set; }

        /// <summary>
        /// Any trailing whitespace or comments encountered immediately after this SubToken
        /// </summary>
        public IEnumerable<WhitespaceSubToken> TrailingWhitespace { get; internal set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (LeadingWhitespace?.Any() == true)
                sb.Append(System.String.Join("", LeadingWhitespace.Select(ws => ws.ToString())));
            sb.Append(Value);
            if (TrailingWhitespace?.Any() == true)
                sb.Append(System.String.Join("", TrailingWhitespace.Select(ws => ws.ToString())));
            return sb.ToString();
        }

        SubToken IPositionAware<SubToken>.SetPos(Position startPos, int length) => SetPos<SubToken>(startPos, length);

        protected internal T SetPos<T>(Position startPos, int length)
            where T : SubToken
        {
            Location = new FhirPathExpressionLocationInfo() { LinePosition = startPos.Column, LineNumber = startPos.Line, RawPosition = startPos.Pos, Length = length };
            return this as T;
        }
    }

    /// <summary>
    /// A SubToken that represents whitespace
    /// </summary>
    [DebuggerDisplay(@"Value: \{{Value}}")]
    public class WhitespaceSubToken : SubToken, Sprache.IPositionAware<WhitespaceSubToken>
    {
        public WhitespaceSubToken(string value, ISourcePositionInfo location = null) : base(value, location)
        {
        }
        WhitespaceSubToken IPositionAware<WhitespaceSubToken>.SetPos(Position startPos, int length) => SetPos<WhitespaceSubToken>(startPos, length);
    }

    /// <summary>
    /// A SubToken that represents a comment
    /// </summary>
    [DebuggerDisplay(@"Value: \{{Value}}")]
    public class CommentSubToken : WhitespaceSubToken, Sprache.IPositionAware<CommentSubToken>
    {
        /// <summary>
        /// A SubToken that represents a comment
        /// </summary>
        /// <param name="value">The raw value of the SubToken as parsed</param>
        /// <param name="block">Is this a block comment (/* true */) or an end-of-line comment (// false)</param>
        /// <param name="location">Parsing position information</param>
        public CommentSubToken(string value, bool block, ISourcePositionInfo location = null) : base(value, location)
        {
            this.block = block;
        }

        /// <summary>
        /// This comment is a block comment (i.e. /* ... */) when false this is a line comment (i.e. // ...)
        /// </summary>
        public bool block { get; init; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (LeadingWhitespace?.Any() == true)
                sb.Append(System.String.Join("", LeadingWhitespace.Select(ws => ws.ToString())));
            if (block)
                sb.Append("/*");
            else
                sb.Append("//");
            sb.Append(Value);
            if (block)
                sb.Append("*/");
            if (TrailingWhitespace?.Any() == true)
                sb.Append(System.String.Join("", TrailingWhitespace.Select(ws => ws.ToString())));
            return sb.ToString();
        }

        CommentSubToken IPositionAware<CommentSubToken>.SetPos(Position startPos, int length) => SetPos<CommentSubToken>(startPos, length);
    }
}
