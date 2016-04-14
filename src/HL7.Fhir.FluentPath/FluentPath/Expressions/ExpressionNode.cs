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
        protected Expression( FluentPathType type)
        {
            ExpressionType = type;
        }
        public FluentPathType ExpressionType { get; protected set; }
    }


    public class ConstantExpression : Expression
    {
        public ConstantExpression(object value, FluentPathType type) : base(type)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = value;
        }

        public object Value { get; private set; }
    }

    public class FunctionCallExpression : Expression
    {
        public FunctionCallExpression(Expression input, string name, FluentPathType type, params Expression[] arguments) : base(type)
        {
            Input = input;
            FunctionName = name;
            Arguments = arguments;
        }
        public Expression Input { get; private set; }

        public string FunctionName { get; private set; }

        public IEnumerable<Expression> Arguments { get; private set; }
    }

    public class ChildExpression : FunctionCallExpression
    {
        public ChildExpression(Expression input, string name) : base(input, "children", FluentPathType.Any, new ConstantExpression(name, FluentPathType.String))
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
            Op = op;
            Left = left;
            Right = right;
        }
        public Operator Op { get; private set; }

        public Expression Left { get; private set; }

        public Expression Right { get; private set; }
    }

    public class LambdaExpression : Expression
    {
        public LambdaExpression(Expression body) : base(body.ExpressionType)
        {
            Body = body;
        }
        public Expression Body { get; private set;  }
    }
}
