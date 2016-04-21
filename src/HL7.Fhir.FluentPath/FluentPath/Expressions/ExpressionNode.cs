using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath.Expressions
{
    public enum FluentPathType
    {
        Bool,
        String,
        Integer,
        Decimal,
        DateTime,
        Time,
        Any
    }

    public abstract class Expression
    {
        protected const string OP_PREFIX = "builtin.";
        protected static readonly int OP_PREFIX_LEN = OP_PREFIX.Length;

        protected Expression(FluentPathType type)
        {
            ExpressionType = type;
        }
        public FluentPathType ExpressionType { get; protected set; }

        public abstract void Accept(ExpressionVisitor visitor);

        public override bool Equals(object obj)
        {
            if (obj is Expression && obj != null)
            {
                return ((Expression)obj).ExpressionType == ExpressionType;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)ExpressionType;
        }

    }


    public class ConstantExpression : Expression
    {
        public ConstantExpression(object value, FluentPathType type) : base(type)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = value;
        }

        public ConstantExpression(object value) : base(FluentPathType.Any)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = ConstantValue.ToFluentPathValue(value);

            if (Value is bool)
                ExpressionType = FluentPathType.Bool;
            else if (Value is string)
                ExpressionType = FluentPathType.String;
            else if (Value is Int64)
                ExpressionType = FluentPathType.Integer;
            else if (Value is Decimal)
                ExpressionType = FluentPathType.Decimal;
            else if (Value is PartialDateTime)
                ExpressionType = FluentPathType.DateTime;
            else if (Value is Time)
                ExpressionType = FluentPathType.Time;
            else
                throw Error.InvalidOperation("Internal logic error: encountered unmappable Value of type " + Value.GetType().Name);
        }

        public object Value { get; private set; }

        public override void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitConstant(this);
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
        public FunctionCallExpression(Expression focus, string name, FluentPathType type, params Expression[] arguments) : this(focus, name, type, (IEnumerable<Expression>) arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, FluentPathType type, IEnumerable<Expression> arguments) : base(type)
        {
            if (focus == null) throw Error.ArgumentNull("focus");
            if (String.IsNullOrEmpty(name)) throw Error.ArgumentNull("name");
            if (arguments == null) throw Error.ArgumentNull("arguments");

            Focus = focus;
            FunctionName = name;
            Arguments = arguments;
        }

        public Expression Focus { get; private set; }
        public string FunctionName { get; private set; }

        public IEnumerable<Expression> Arguments { get; private set; }

        public override void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitFunctionCall(this);
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
        public ChildExpression(Expression focus, string name) : base(focus, OP_PREFIX+"children", FluentPathType.Any, new ConstantExpression(name, FluentPathType.String))
        {
        }

        public string ChildName
        {
            get { return (string)((ConstantExpression)Arguments.First()).Value; }
        }
    }


    public class IndexerExpression : FunctionCallExpression
    {
        public IndexerExpression(Expression collection, Expression index) : base(collection, OP_PREFIX+"item", FluentPathType.Any, index)
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
        public BinaryExpression(char op, Expression left, Expression right) : base(left, OP_PREFIX + op, FluentPathType.Any, right)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right) : base(left, OP_PREFIX+op, FluentPathType.Any, right)
        {
        }
        public string Op
        {
            get
            {
                return FunctionName.Substring(OP_PREFIX_LEN);
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
        public UnaryExpression(char op, Expression operand) : base(operand, OP_PREFIX + op, FluentPathType.Any)
        {
        }

        public UnaryExpression(string op, Expression operand) : base(operand, OP_PREFIX + op, FluentPathType.Any)
        {
        }
        public string Op
        {
            get
            {
                return FunctionName.Substring(OP_PREFIX_LEN);
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

    public class LambdaExpression : Expression
    {
        public LambdaExpression(Expression body) : base(body.ExpressionType)
        {
            if (body == null) throw Error.ArgumentNull("body");

            Body = body;
        }
        public Expression Body { get; private set;  }

        public override void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitLambda(this);
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is LambdaExpression)
            {
                var f = (LambdaExpression)obj;

                return Object.Equals(f.Body,Body);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Body.GetHashCode();
        }

    }

    public class NewNodeListInitExpression : Expression
    {
        public NewNodeListInitExpression(IEnumerable<Expression> contents) : base(FluentPathType.Any)
        {
            if (contents == null) throw Error.ArgumentNull("contents");

            Contents = contents;
        }

        public IEnumerable<Expression> Contents { get; private set;  }

        public override void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitNewNodeListInit(this);
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
        public VariableRefExpression(string name) : base(FluentPathType.Any)
        {
            if (name == null) throw Error.ArgumentNull("name");

            Name = name;
        }

        public string Name { get; private set; }

        public override void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitVariableRef(this);
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
    }

}
