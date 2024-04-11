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
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Expressions
{
    public abstract class Expression : IEquatable<Expression>
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

        public ISourcePositionInfo Location { get; }

        public TypeSpecifier ExpressionType { get; protected set; }

        public abstract T Accept<T>(ExpressionVisitor<T> visitor);

        public override bool Equals(object obj) => Equals(obj as Expression);
        public bool Equals(Expression other) => other != null && EqualityComparer<TypeSpecifier>.Default.Equals(ExpressionType, other.ExpressionType);
        public override int GetHashCode() => -28965461 + EqualityComparer<TypeSpecifier>.Default.GetHashCode(ExpressionType);
        public static bool operator ==(Expression left, Expression right) => EqualityComparer<Expression>.Default.Equals(left, right);
        public static bool operator !=(Expression left, Expression right) => !(left == right);
    }


    public class ConstantExpression : Expression
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
    }

    public class FunctionCallExpression : Expression
    {
        public FunctionCallExpression(Expression focus, string name, TypeSpecifier type, params Expression[] arguments) : this(focus, name, type, (IEnumerable<Expression>)arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, TypeSpecifier type, IEnumerable<Expression> arguments, ISourcePositionInfo location = null) : base(type, location)
        {
            if (string.IsNullOrEmpty(name)) throw Error.ArgumentNull("name");
            Focus = focus;
            FunctionName = name;
            Arguments = arguments != null ? arguments.ToArray() : throw Error.ArgumentNull("arguments");
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
    }


    public class ChildExpression : FunctionCallExpression
    {
        public ChildExpression(Expression focus, string name) : base(focus, OP_PREFIX + "children", TypeSpecifier.Any,
                new ConstantExpression(name, TypeSpecifier.String))
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
    }


    public class IndexerExpression : FunctionCallExpression
    {
        public IndexerExpression(Expression collection, Expression index) : base(collection, OP_PREFIX + "item", TypeSpecifier.Any, index)
        {
        }

        public Expression Index
        {
            get
            {
                return Arguments.First();
            }
        }
    }

    public class BinaryExpression : FunctionCallExpression
    {
        internal const string BIN_PREFIX = "binary.";
        internal static readonly int BIN_PREFIX_LEN = BIN_PREFIX.Length;


        public BinaryExpression(char op, Expression left, Expression right) : this(new string(op, 1), left, right)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right) : base(AxisExpression.That, BIN_PREFIX + op, TypeSpecifier.Any, left, right)
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
    }


    public class UnaryExpression : FunctionCallExpression
    {
        internal const string URY_PREFIX = "unary.";
        internal static readonly int URY_PREFIX_LEN = URY_PREFIX.Length;

        public UnaryExpression(char op, Expression operand) : this(new string(op, 1), operand)
        {
        }

        public UnaryExpression(string op, Expression operand) : base(AxisExpression.That, URY_PREFIX + op, TypeSpecifier.Any, operand)
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

    public class NewNodeListInitExpression : Expression
    {
        public NewNodeListInitExpression(IEnumerable<Expression> contents) : base(TypeSpecifier.Any)
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
    }

    public class VariableRefExpression : Expression
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
    }

    public class AxisExpression : VariableRefExpression
    {
        public AxisExpression(string axisName) : base(OP_PREFIX + axisName)
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
    }
}
