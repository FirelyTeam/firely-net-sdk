/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Language;
using Hl7.Fhir.Language.Debugging;
using Hl7.Fhir.Utility;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Expressions
{
    /// <summary>
    /// The base class for all components within a fhirpath expression<br/>
    /// </summary>
    /// <remarks>
    /// Each component (represented by an derived instance of this class) may also
    /// contain several Sub-Components (<see cref="SubToken"/>) that do not impact execution<br/>
    /// There may also be <see cref="CustomExpression"/> such as <see cref="BracketExpression"/> that
    /// have recently been added which are in the grammar, however are not required by the execution
    /// as they are implied by operator precedence which is already handled by the execution tree.<br/>
    /// These are removed from the expression tree before execution via the <see cref="CustomExpression.Reduce"/> method
    /// in the <see cref="ExpressionVisitor{T}.VisitCustomExpression(CustomExpression)">expression visitor</see>.<br/>
    /// <br/>
    /// Across all the classes derived from Expression, there is a constructor that has an optional ISourcePositionInfo
    /// parameter, this is used to permit the position information for the node. This is only used in Unit Tests to
    /// provide the information, the parser uses the <see cref="SetPos{T}(Position, int)"/> method.
    /// </remarks>
    public abstract class Expression : IEquatable<Expression>, Sprache.IPositionAware<Expression>
    {
        internal const string OP_PREFIX = "builtin.";
        internal static readonly int OP_PREFIX_LEN = OP_PREFIX.Length;

        protected Expression(TypeSpecifier type)
        {
            ExpressionType = type;
        }

        protected Expression(TypeSpecifier type, ISourcePositionInfo location) : this(type)
        {
            Location = location;
        }

        /// <summary>
        /// Location information for this expression component in the parsed complete fhirpath expression
        /// </summary>
        public ISourcePositionInfo Location { get; private set; }

        /// <summary>
        /// Any leading whitespace or comments encountered immediately before this Expression
        /// </summary>
        public IEnumerable<WhitespaceSubToken> LeadingWhitespace { get; internal set; }

        /// <summary>
        /// Any trailing whitespace or comments encountered immediately after this Expression
        /// </summary>
        public IEnumerable<WhitespaceSubToken> TrailingWhitespace { get; internal set; }

        /// <summary>
        /// The DataType that would be returned be evaluating this ExpressionNode
        /// </summary>
        /// <remarks>
        /// If multiple types are possible, it will return <see cref="TypeSpecifier.Any"/>
        /// </remarks>
        public TypeSpecifier ExpressionType { get; protected set; }

        public abstract T Accept<T>(ExpressionVisitor<T> visitor);

        public override bool Equals(object obj) => Equals(obj as Expression);
        public virtual bool Equals(Expression other)
        {
            if (other == null)
                return false;
            if (!EqualityComparer<TypeSpecifier>.Default.Equals(ExpressionType, other.ExpressionType))
                return false;

            // Also check the location information if it is present in the other
            // This is the new functionality added for the unit testing
            // (previously location data was never present, and thus never compared)
            if (Location != null)
            {
                if (other.Location == null)
                    return false;
                var label = (ISourcePositionInfo posinfo) =>
                {
                    var pi = (FhirPathExpressionLocationInfo)posinfo;
                    return $"Line: {pi.LineNumber}, LinePos: {pi.LinePosition}, RawPos: {pi.RawPosition}, Length: {pi.Length}";
                };
                if (label(other.Location) != label(Location))
                {
                    Console.WriteLine($"Expected: {label(Location)}\r\nActual:   {label(other.Location)}");
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode() => -28965461 + EqualityComparer<TypeSpecifier>.Default.GetHashCode(ExpressionType);

        Expression IPositionAware<Expression>.SetPos(Position startPos, int length) => SetPos<Expression>(startPos, length);

        protected internal T SetPos<T>(Position startPos, int length)
            where T : Expression
        {
            Location = new FhirPathExpressionLocationInfo() { LinePosition = startPos.Column, LineNumber = startPos.Line, RawPosition = startPos.Pos, Length = length };
            return this as T;
        }

        public static bool operator ==(Expression left, Expression right) => EqualityComparer<Expression>.Default.Equals(left, right);
        public static bool operator !=(Expression left, Expression right) => !(left == right);
    }

    /// <summary>
    /// The CustomExpression class permits the inclusion of addition expression nodes/types not known
    /// to the core FHIR Path implementation, however they must be able to be reduced to an existing
    /// expression type to be executed.<br/>
    /// These will be handled via the visitor pattern, and the Reduce() method will be called to
    /// convert it to a known expression type.
    /// The initial use of this is for the BracketExpression so it can be included in the tree to
    /// permit easier round-tripping and visualization based on the actual expression that was parsed.
    /// </summary>
    public abstract class CustomExpression : Expression
    {
        protected CustomExpression(TypeSpecifier type, ISourcePositionInfo location = null) : base(type, location)
        {
        }

        public abstract Expression Reduce();
    }

    public class IdentifierExpression : ConstantExpression, Sprache.IPositionAware<IdentifierExpression>
    {
        public IdentifierExpression(object value, TypeSpecifier type, ISourcePositionInfo location = null)
            : base(value, type, location)
        {
        }

        public IdentifierExpression(object value, ISourcePositionInfo location = null)
            : base(value, location)
        {
        }

        public new string Value { get => base.Value as string; }

        IdentifierExpression IPositionAware<IdentifierExpression>.SetPos(Position startPos, int length) => SetPos<IdentifierExpression>(startPos, length);
    }

    public class BracketExpression : CustomExpression, Sprache.IPositionAware<BracketExpression>
    {
        public BracketExpression(Expression operand, ISourcePositionInfo location = null) : base(operand.ExpressionType, location)
        {
            Operand = operand;
        }

        public BracketExpression(SubToken leftBrace, SubToken rightBrace, Expression operand, ISourcePositionInfo location = null) : base(operand.ExpressionType, location)
        {
            Operand = operand;
            LeftBrace = leftBrace;
            RightBrace = rightBrace;
        }

        public Expression Operand { get; private set; }

        /// <summary>
        /// Raw parsed left brace token
        /// </summary>
        public SubToken LeftBrace { get; private set; }

        /// <summary>
        /// Raw parsed right brace token
        /// </summary>
        public SubToken RightBrace { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitCustomExpression(this);

        public override Expression Reduce()
        {
            return Operand;
        }
        BracketExpression IPositionAware<BracketExpression>.SetPos(Position startPos, int length) => SetPos<BracketExpression>(startPos, length);
    }

    public class ConstantExpression : Expression, Sprache.IPositionAware<ConstantExpression>
    {
        public ConstantExpression(object value, TypeSpecifier type, ISourcePositionInfo location = null) : base(type, location)
        {
            if (value == null) Error.ArgumentNull("value");
            if (value is P.Any and (P.Boolean or P.Decimal or P.Integer or P.Long or P.String))
                throw new ArgumentException("Internal error: not yet ready to handle Any-based primitives in FhirPath.");

            Value = value;
        }

        public ConstantExpression(object value, ISourcePositionInfo location = null) : base(TypeSpecifier.Any, location)
        {
            if (value == null) Error.ArgumentNull("value");

            if (ElementNode.TryConvertToElementValue(value, out var systemValue))
            {
                Value = systemValue;
                ExpressionType = TypeSpecifier.ForNativeType(value.GetType());
            }
            else
                throw Error.InvalidOperation("Internal logic error: encountered unmappable Value of type " + Value.GetType().Name);
        }

        /// <summary>
        /// Constructor used for Quantity type constants (so that the parsed unit token is preserved)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit">Raw unit token as parsed</param>
        /// <param name="location"></param>
        public ConstantExpression(object value, SubToken unit, ISourcePositionInfo location = null) : base(TypeSpecifier.Quantity, location)
        {
            if (value == null) Error.ArgumentNull("value");
            Unit = unit;

            if (ElementNode.TryConvertToElementValue(value, out var systemValue))
            {
                Value = systemValue;
                ExpressionType = TypeSpecifier.ForNativeType(value.GetType());
            }
            else
                throw Error.InvalidOperation("Internal logic error: encountered unmappable Value of type " + Value.GetType().Name);
        }

        public object Value { get; private set; }

        /// <summary>
        /// If this is a quantity type, then this is the unit of the quantity as parsed
        /// </summary>
        public SubToken Unit { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitConstant(this);

        public override bool Equals(object obj) => Equals(obj as ConstantExpression);
        public override bool Equals(Expression obj)
        {
            if (base.Equals(obj) && obj is ConstantExpression ce)
            {
                var c = ce;
                return Object.Equals(c.Value, Value);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Value.GetHashCode();
        }
        ConstantExpression IPositionAware<ConstantExpression>.SetPos(Position startPos, int length) => SetPos<ConstantExpression>(startPos, length);
    }

    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class FunctionCallExpression : Expression, Sprache.IPositionAware<FunctionCallExpression>
    {
        public FunctionCallExpression(Expression focus, string name, TypeSpecifier type, params Expression[] arguments) : this(focus, name, type, (IEnumerable<Expression>)arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, TypeSpecifier type, IEnumerable<Expression> arguments, ISourcePositionInfo location = null) : base(type, location)
        {
            if (string.IsNullOrEmpty(name)) throw Error.ArgumentNull(nameof(name));
            Focus = focus;
            FunctionName = name;
            Arguments = arguments != null ? arguments.ToArray() : throw Error.ArgumentNull("arguments");
        }

        public FunctionCallExpression(Expression focus, string name, SubToken leftBrace, SubToken rightBrace, TypeSpecifier type, params Expression[] arguments) : this(focus, name, leftBrace, rightBrace, type, (IEnumerable<Expression>)arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, SubToken leftBrace, SubToken rightBrace, TypeSpecifier type, IEnumerable<Expression> arguments, ISourcePositionInfo location = null) : base(type, location)
        {
            if (string.IsNullOrEmpty(name)) throw Error.ArgumentNull(nameof(name));
            Focus = focus;
            FunctionName = name;
            Arguments = arguments != null ? arguments.ToArray() : throw Error.ArgumentNull(nameof(arguments));
            LeftBrace = leftBrace;
            RightBrace = rightBrace;
        }

        public FunctionCallExpression(Expression focus, string name, TypeSpecifier type, Expression argument, ISourcePositionInfo location = null) : base(type, location)
        {
            if (string.IsNullOrEmpty(name)) throw Error.ArgumentNull(nameof(name));
            if (argument == null) throw Error.ArgumentNull(nameof(argument));
            Focus = focus;
            FunctionName = name;
            Arguments = new[] { argument };
        }

        public Expression Focus { get; private set; }
        public string FunctionName { get; private set; }

        /// <summary>
        /// Raw parsed left brace token
        /// </summary>
        public SubToken LeftBrace { get; private set; }

        /// <summary>
        /// Raw parsed right brace token
        /// </summary>
        public SubToken RightBrace { get; private set; }

        public IEnumerable<Expression> Arguments { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitFunctionCall(this);

        public override bool Equals(object obj) => Equals(obj as FunctionCallExpression);
        public override bool Equals(Expression obj)
        {
            if (base.Equals(obj) && obj is FunctionCallExpression fce)
            {
                var f = fce;

                return f.FunctionName == FunctionName && Arguments.SequenceEqual(f.Arguments);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ FunctionName.GetHashCode() ^ Arguments.GetHashCode();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string DebuggerDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{FunctionName}(");
                for (int n = 0; n < this.Arguments.Count(); n++)
                    sb.Append(",");
                sb.Append(")");
                return sb.ToString();
            }
        }
        FunctionCallExpression IPositionAware<FunctionCallExpression>.SetPos(Position startPos, int length) => SetPos<FunctionCallExpression>(startPos, length);
    }


    public class ChildExpression : FunctionCallExpression, Sprache.IPositionAware<ChildExpression>
    {
        public ChildExpression(Expression focus, string name) : base(focus, OP_PREFIX + "children", TypeSpecifier.Any,
                new IdentifierExpression(name, TypeSpecifier.String))
        {
        }

        public ChildExpression(Expression focus, string name, ISourcePositionInfo location) : base(focus, OP_PREFIX + "children", TypeSpecifier.Any,
                new IdentifierExpression(name, TypeSpecifier.String), location)
        {
        }

        public ChildExpression(Expression focus, ConstantExpression name) : base(focus, OP_PREFIX + "children", null, null, TypeSpecifier.Any, name)
        {
        }

        public string ChildName
        {
            get
            {
                // We know, because of the constructor, that there will be one argument, which is a P.String,
                // that contains the name of the child.
                var arg1 = (ConstantExpression)Arguments.First();
                //var arg1Value = arg1.Value as P.String;
                var arg1Value = arg1.Value as string;
                return arg1Value;
            }
        }
        ChildExpression IPositionAware<ChildExpression>.SetPos(Position startPos, int length) => SetPos<ChildExpression>(startPos, length);
    }


    public class IndexerExpression : FunctionCallExpression, Sprache.IPositionAware<IndexerExpression>
    {
        public IndexerExpression(Expression collection, Expression index) : base(collection, OP_PREFIX + "item", TypeSpecifier.Any, index)
        {
        }

        public IndexerExpression(Expression collection, Expression index, ISourcePositionInfo location) : base(collection, OP_PREFIX + "item", TypeSpecifier.Any, index, location)
        {
        }

        public IndexerExpression(Expression collection, Expression index, SubToken leftSqBrace, SubToken rightSqBrace, ISourcePositionInfo location = null)
            : base(collection, OP_PREFIX + "item", leftSqBrace, rightSqBrace, TypeSpecifier.Any, new[] { index }, location)
        {
        }

        public Expression Index
        {
            get
            {
                return Arguments.First();
            }
        }
        IndexerExpression IPositionAware<IndexerExpression>.SetPos(Position startPos, int length) => SetPos<IndexerExpression>(startPos, length);
    }

    public class BinaryExpression : FunctionCallExpression, Sprache.IPositionAware<BinaryExpression>
    {
        internal const string BIN_PREFIX = "binary.";
        internal static readonly int BIN_PREFIX_LEN = BIN_PREFIX.Length;


        public BinaryExpression(char op, Expression left, Expression right) : this(new string(op, 1), left, right, null)
        {
        }

        public BinaryExpression(char op, Expression left, Expression right, ISourcePositionInfo location) : this(new string(op, 1), left, right, location)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right) : base(AxisExpression.That, BIN_PREFIX + op, TypeSpecifier.Any, new[] { left, right }, null)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right, ISourcePositionInfo location) : base(AxisExpression.That, BIN_PREFIX + op, TypeSpecifier.Any, new[] { left, right }, location)
        {
        }

        public BinaryExpression(SubToken op, Expression left, Expression right, ISourcePositionInfo location = null) : base(AxisExpression.That, BIN_PREFIX + op.Value, TypeSpecifier.Any, new[] { left, right }, location)
        {
            OpToken = op;
        }

        /// <summary>
        /// The Raw token parsed for the Operator (that contains the location information)
        /// </summary>
        public SubToken OpToken { get; private set; }

        /// <summary>
        /// The String representation of the operator
        /// </summary>
        /// <remarks>
        /// This is internally stored in the FunctionName property with the prefix "binary."
        /// </remarks>
        public string Op
        {
            get
            {
                return FunctionName.Substring(BIN_PREFIX_LEN);
            }
        }

        public Expression Left
        {
            get
            {
                return Focus;
            }
        }

        public Expression Right
        {
            get
            {
                return Arguments.First();
            }
        }
        BinaryExpression IPositionAware<BinaryExpression>.SetPos(Position startPos, int length) => SetPos<BinaryExpression>(startPos, length);
    }

    public class SortDirectionExpression : FunctionCallExpression, Sprache.IPositionAware<SortDirectionExpression>
    {
        internal const string URY_PREFIX = "unary.";
        internal static readonly int URY_PREFIX_LEN = URY_PREFIX.Length;

        public SortDirectionExpression(char op, Expression operand) : this(new string(op, 1), operand)
        {
        }

        public SortDirectionExpression(char op, Expression operand, ISourcePositionInfo location) : this(new string(op, 1), operand, location)
        {
        }

        public SortDirectionExpression(string op, Expression operand) : base(AxisExpression.This, URY_PREFIX + op, TypeSpecifier.Any, operand)
        {
        }

        public SortDirectionExpression(string op, Expression operand, ISourcePositionInfo location) : base(AxisExpression.This, URY_PREFIX + op, TypeSpecifier.Any, operand, location)
        {
        }

        public string Op
        {
            get
            {
                return FunctionName.Substring(URY_PREFIX_LEN);
            }
        }

        public Expression Operand
        {
            get
            {
                return Focus;
            }
        }
        SortDirectionExpression IPositionAware<SortDirectionExpression>.SetPos(Position startPos, int length) => SetPos<SortDirectionExpression>(startPos, length);
    }

    public class UnaryExpression : FunctionCallExpression, Sprache.IPositionAware<UnaryExpression>
    {
        internal const string URY_PREFIX = "unary.";
        internal static readonly int URY_PREFIX_LEN = URY_PREFIX.Length;

        public UnaryExpression(char op, Expression operand) : this(new string(op, 1), operand)
        {
        }

        public UnaryExpression(char op, Expression operand, ISourcePositionInfo location) : this(new string(op, 1), operand, location)
        {
        }

        public UnaryExpression(string op, Expression operand) : base(AxisExpression.That, URY_PREFIX + op, TypeSpecifier.Any, operand)
        {
        }

        public UnaryExpression(string op, Expression operand, ISourcePositionInfo location) : base(AxisExpression.That, URY_PREFIX + op, TypeSpecifier.Any, operand, location)
        {
        }

        public string Op
        {
            get
            {
                return FunctionName.Substring(URY_PREFIX_LEN);
            }
        }

        public Expression Operand
        {
            get
            {
                return Focus;
            }
        }
        UnaryExpression IPositionAware<UnaryExpression>.SetPos(Position startPos, int length) => SetPos<UnaryExpression>(startPos, length);
    }

    public class NewNodeListInitExpression : Expression, Sprache.IPositionAware<NewNodeListInitExpression>
    {
        public NewNodeListInitExpression(IEnumerable<Expression> contents) : base(TypeSpecifier.Any, null)
        {
            Contents = contents ?? throw Error.ArgumentNull("contents");
        }

        public NewNodeListInitExpression(IEnumerable<Expression> contents, ISourcePositionInfo location) : base(TypeSpecifier.Any, location)
        {
            Contents = contents ?? throw Error.ArgumentNull("contents");
        }

        public NewNodeListInitExpression(SubToken leftBrace, SubToken rightBrace, ISourcePositionInfo location = null) : base(TypeSpecifier.Any, location)
        {
            LeftBrace = leftBrace;
            RightBrace = rightBrace;
            Contents = Enumerable.Empty<Expression>();
        }

        // This isn't a set constructor, it's an empty set, so why the contents?
        public IEnumerable<Expression> Contents { get; private set; }

        /// <summary>
        /// Raw parsed left brace token
        /// </summary>
        public SubToken LeftBrace { get; private set; }

        /// <summary>
        /// Raw parsed right brace token
        /// </summary>
        public SubToken RightBrace { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitNewNodeListInit(this);
        public override bool Equals(object obj) => Equals(obj as NewNodeListInitExpression);
        public override bool Equals(Expression obj)
        {
            if (base.Equals(obj) && obj is NewNodeListInitExpression ne)
            {
                var f = ne;

                return f.Contents.SequenceEqual(Contents);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Contents.GetHashCode();
        }

        public static readonly NewNodeListInitExpression Empty = new NewNodeListInitExpression(Enumerable.Empty<Expression>());
        NewNodeListInitExpression IPositionAware<NewNodeListInitExpression>.SetPos(Position startPos, int length) => SetPos<NewNodeListInitExpression>(startPos, length);
    }

    public class VariableRefExpression : Expression, Sprache.IPositionAware<VariableRefExpression>
    {
        public VariableRefExpression(string name, ISourcePositionInfo location = null) : base(TypeSpecifier.Any, location)
        {
            Name = name ?? throw Error.ArgumentNull("name");
        }

        public string Name { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor)
        {
            return visitor.VisitVariableRef(this);
        }
        public override bool Equals(object obj) => Equals(obj as VariableRefExpression);
        public override bool Equals(Expression obj)
        {
            if (base.Equals(obj) && obj is VariableRefExpression expression)
            {
                var f = expression;

                return f.Name == Name;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Name.GetHashCode();
        }
        VariableRefExpression IPositionAware<VariableRefExpression>.SetPos(Position startPos, int length) => SetPos<VariableRefExpression>(startPos, length);
    }

    public class AxisExpression : VariableRefExpression, Sprache.IPositionAware<AxisExpression>
    {
        public AxisExpression(string axisName) : base(OP_PREFIX + axisName, null)
        {
            if (axisName == null) throw Error.ArgumentNull("axisName");
        }

        public AxisExpression(string axisName, ISourcePositionInfo location) : base(OP_PREFIX + axisName, location)
        {
            if (axisName == null) throw Error.ArgumentNull("axisName");
        }

        public string AxisName
        {
            get
            {
                return Name.Substring(OP_PREFIX_LEN);
            }
        }

        public static readonly AxisExpression Index = new AxisExpression("index");
        public static readonly AxisExpression This = new AxisExpression("this");
        public static readonly AxisExpression That = new AxisExpression("that");
        AxisExpression IPositionAware<AxisExpression>.SetPos(Position startPos, int length) => SetPos<AxisExpression>(startPos, length);
    }
}