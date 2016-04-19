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
        protected Expression(FluentPathType type)
        {
            ExpressionType = type;
        }
        public FluentPathType ExpressionType { get; protected set; }

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

        public object Value { get; private set; }

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
        public ChildExpression(Expression focus, string name) : base(focus, "children", FluentPathType.Any, new ConstantExpression(name, FluentPathType.String))
        {
        }

        public string ChildName
        {
            get { return (string)((ConstantExpression)Arguments.First()).Value; }
        }
    }

    public class BinaryExpression : Expression
    {
        public BinaryExpression(Operator op, Expression left, Expression right) : base(FluentPathType.Any)
        {
            if (left == null) throw Error.ArgumentNull("left");
            if (right == null) throw Error.ArgumentNull("right");

            Op = op;
            Left = left;
            Right = right;
        }
        public Operator Op { get; private set; }

        public Expression Left { get; private set; }

        public Expression Right { get; private set; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is BinaryExpression)
            {
                var f = (BinaryExpression)obj;

                return f.Op == Op && Object.Equals(f.Left,Left) && Object.Equals(f.Right,Right);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Op.GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
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

    public class AxisExpression : Expression
    {
        public AxisExpression(string axisName) : base(FluentPathType.Any)
        {
            if (axisName == null) throw Error.ArgumentNull("axisName");

            AxisName = axisName;
        }

        public string AxisName { get; private set; }

        public static readonly AxisExpression This = new AxisExpression("this");

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is AxisExpression)
            {
                var f = (AxisExpression)obj;

                return f.AxisName == AxisName;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ AxisName.GetHashCode();
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

    public class ExternalConstantExpression : Expression
    {
        public ExternalConstantExpression(string name) : base(FluentPathType.Any)
        {
            if (name == null) throw Error.ArgumentNull("name");

            ExternalName = name;
        }

        public string ExternalName { get; private set; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is ExternalConstantExpression)
            {
                var f = (ExternalConstantExpression)obj;

                return f.ExternalName == ExternalName;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ ExternalName.GetHashCode();
        }


    }
}
