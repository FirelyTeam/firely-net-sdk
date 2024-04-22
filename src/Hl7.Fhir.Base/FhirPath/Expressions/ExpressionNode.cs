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
    public abstract class Expression : IEquatable<Expression>, Sprache.IPositionAware<Expression>
    {
        internal const string OP_PREFIX = "builtin.";
        internal static readonly int OP_PREFIX_LEN = OP_PREFIX.Length;

        protected Expression(TypeSpecifier type)
        {
            ExpressionType = type;
        }

        // [Obsolete("This will be removed... this happens through the SetPos()", false)]
        protected Expression(TypeSpecifier type, ISourcePositionInfo location) : this(type)
        {
            Location = location;
        }

        public ISourcePositionInfo Location { get; private set; }

        public TypeSpecifier ExpressionType { get; protected set; }

        public abstract T Accept<T>(ExpressionVisitor<T> visitor);

        public override bool Equals(object obj) => Equals(obj as Expression);
        public bool Equals(Expression other)
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
    /// to the core FHIR Path implementation.
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

    // Discuss: Reduce function? - Skip this in the compile stage? - Update the visitor to skip the bracket too
    // public class BracketExpression : FunctionCallExpression, Sprache.IPositionAware<BracketExpression>, Sprache.IPositionAware<Expression>
    public class BracketExpression : CustomExpression, Sprache.IPositionAware<BracketExpression>
    {
        public BracketExpression(Expression operand, ISourcePositionInfo location = null) : base(operand.ExpressionType, location)
        {
            Operand = operand;
        }
        
        public Expression Operand { get; private set; }

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
            if (value is P.Any && (value is P.Boolean || value is P.Decimal || value is P.Integer || value is P.Long || value is P.String))
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

        public object Value { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitConstant(this);

        public override bool Equals(object obj)
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

        public IEnumerable<Expression> Arguments { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitFunctionCall(this);

        public override bool Equals(object obj)
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
        public ChildExpression(Expression focus, string name, ISourcePositionInfo location = null) : base(focus, OP_PREFIX + "children", TypeSpecifier.Any,
                new ConstantExpression(name, TypeSpecifier.String), location)
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
        public IndexerExpression(Expression collection, Expression index, ISourcePositionInfo location = null) : base(collection, OP_PREFIX + "item", TypeSpecifier.Any, index, location)
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


        public BinaryExpression(char op, Expression left, Expression right, ISourcePositionInfo location = null) : this(new string(op, 1), left, right, location)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right, ISourcePositionInfo location = null) : base(AxisExpression.That, BIN_PREFIX + op, TypeSpecifier.Any, new[] { left, right }, location)
        {
        }
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


    public class UnaryExpression : FunctionCallExpression, Sprache.IPositionAware<UnaryExpression>
    {
        internal const string URY_PREFIX = "unary.";
        internal static readonly int URY_PREFIX_LEN = URY_PREFIX.Length;

        public UnaryExpression(char op, Expression operand, ISourcePositionInfo location = null) : this(new string(op, 1), operand, location)
        {
        }

        public UnaryExpression(string op, Expression operand, ISourcePositionInfo location = null) : base(AxisExpression.That, URY_PREFIX + op, TypeSpecifier.Any, operand, location)
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

    //public class LambdaExpression : Expression
    //{
    //    public LambdaExpression(Expression body) : base(body.ExpressionType)
    //    {
    //        if (body == null) throw Error.ArgumentNull("body");

    //        Body = body;
    //    }
    //    public Expression Body { get; private set;  }

    //    public override T Accept<T>(ExpressionVisitor<T> visitor)
    //    {
    //        return visitor.VisitLambda(this);
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        if (base.Equals(obj) && obj is LambdaExpression)
    //        {
    //            var f = (LambdaExpression)obj;

    //            return Object.Equals(f.Body,Body);
    //        }
    //        else
    //            return false;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode() ^ Body.GetHashCode();
    //    }

    //}

    public class NewNodeListInitExpression : Expression, Sprache.IPositionAware<NewNodeListInitExpression>
    {
        public NewNodeListInitExpression(IEnumerable<Expression> contents, ISourcePositionInfo location = null) : base(TypeSpecifier.Any, location)
        {
            Contents = contents ?? throw Error.ArgumentNull("contents");
        }

        public IEnumerable<Expression> Contents { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor) => visitor.VisitNewNodeListInit(this);
        public override bool Equals(object obj)
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
        public override bool Equals(object obj)
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
        public AxisExpression(string axisName, ISourcePositionInfo location = null) : base(OP_PREFIX + axisName, location)
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
