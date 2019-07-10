/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Language.Debugging;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    public abstract class Expression : IEquatable<Expression>
    {
        internal const string OP_PREFIX = "builtin.";
        internal static readonly int OP_PREFIX_LEN = OP_PREFIX.Length;

        protected Expression(TypeInfo type)
        {
            ExpressionType = type;
        }

        protected Expression(TypeInfo type, ISourcePositionInfo location) : this(type)
        {
            Location = location;           
        }

        public ISourcePositionInfo Location { get; }

        public TypeInfo ExpressionType { get; protected set; }

        public abstract T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope);

        public override bool Equals(object obj) => Equals(obj as Expression);
        public bool Equals(Expression other) => other != null && EqualityComparer<TypeInfo>.Default.Equals(ExpressionType, other.ExpressionType);
        public override int GetHashCode() => -28965461 + EqualityComparer<TypeInfo>.Default.GetHashCode(ExpressionType);
        public static bool operator ==(Expression left, Expression right) => EqualityComparer<Expression>.Default.Equals(left, right);
        public static bool operator !=(Expression left, Expression right) => !(left == right);
    }


    public class ConstantExpression : Expression
    {
        public ConstantExpression(object value, TypeInfo type, ISourcePositionInfo location = null) : base(type, location)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = value;
        }

        public ConstantExpression(object value, ISourcePositionInfo location = null) : base(TypeInfo.Any, location)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = Primitives.ConvertToPrimitiveValue(value);

            if (Value is bool)
                ExpressionType = TypeInfo.Boolean;
            else if (Value is string)
                ExpressionType = TypeInfo.String;
            else if (Value is Int64)
                ExpressionType = TypeInfo.Integer;
            else if (Value is Decimal)
                ExpressionType = TypeInfo.Decimal;
            else if (Value is PartialDateTime)
                ExpressionType = TypeInfo.DateTime;
            else if (Value is PartialTime)
                ExpressionType = TypeInfo.Time;
            else
                throw Error.InvalidOperation("Internal logic error: encountered unmappable Value of type " + Value.GetType().Name);
        }

        public object Value { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitConstant(this, scope);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is ConstantExpression)
            {
                var c = (ConstantExpression)obj;
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
        public FunctionCallExpression(Expression focus, string name, TypeInfo type, params Expression[] arguments) : this(focus, name, type, (IEnumerable<Expression>) arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, TypeInfo type, IEnumerable<Expression> arguments, ISourcePositionInfo location = null) : base(type, location)
        {
            if (String.IsNullOrEmpty(name)) throw Error.ArgumentNull("name");
            Focus = focus;
            FunctionName = name;
            Arguments = arguments ?? throw Error.ArgumentNull("arguments");
        }

        public Expression Focus { get; private set; }
        public string FunctionName { get; private set; }

        public IEnumerable<Expression> Arguments { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitFunctionCall(this, scope);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is FunctionCallExpression)
            {
                var f = (FunctionCallExpression)obj;

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
        public ChildExpression(Expression focus, string name) : base(focus, OP_PREFIX+"children", TypeInfo.Any, new ConstantExpression(name, TypeInfo.String))
        {
        }

        public string ChildName
        {
            get { return (string)((ConstantExpression)Arguments.First()).Value; }
        }
    }


    public class IndexerExpression : FunctionCallExpression
    {
        public IndexerExpression(Expression collection, Expression index) : base(collection, OP_PREFIX+"item", TypeInfo.Any, index)
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


        public BinaryExpression(char op, Expression left, Expression right) : this(new String(op,1), left, right)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right) : base(AxisExpression.That, BIN_PREFIX + op, TypeInfo.Any, left, right)
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

        public UnaryExpression(char op, Expression operand) : this(new String(op,1), operand)
        {
        }

        public UnaryExpression(string op, Expression operand) : base(AxisExpression.That, URY_PREFIX + op, TypeInfo.Any, operand)
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
        public NewNodeListInitExpression(IEnumerable<Expression> contents) : base(TypeInfo.Any)
        {
            Contents = contents ?? throw Error.ArgumentNull("contents");
        }

        public IEnumerable<Expression> Contents { get; private set;  }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitNewNodeListInit(this, scope);
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is NewNodeListInitExpression)
            {
                var f = (NewNodeListInitExpression)obj;

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
        public VariableRefExpression(string name, ISourcePositionInfo location = null) : base(TypeInfo.Any, location)
        {
            Name = name ?? throw Error.ArgumentNull("name");
        }

        public string Name { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitVariableRef(this, scope);
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is VariableRefExpression)
            {
                var f = (VariableRefExpression)obj;

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
        public AxisExpression(string axisName) : base(OP_PREFIX+axisName)
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


        public static readonly AxisExpression This = new AxisExpression("this");
        public static readonly AxisExpression That = new AxisExpression("that");
    }
}
