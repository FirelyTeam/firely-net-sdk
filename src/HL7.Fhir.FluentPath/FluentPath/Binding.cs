using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath
{
    internal class Binding
    {
        //public delegate IEnumerable<IFluentPathValue> NoParams(IEnumerable<IFluentPathValue> focus);
        //public delegate IEnumerable<IFluentPathValue> OneParam(IEnumerable<IFluentPathValue> focus, object param1);
        //public delegate IEnumerable<IFluentPathValue> TwoParams(IEnumerable<IFluentPathValue> focus, object param1, object param2);

        //public delegate IEnumerable<IFluentPathValue> FpFunction(IEnumerable<IFluentPathValue> focus, IEvaluationContext context);


        //// Function with "no" parameters: just the focus (input) and the result
        //// private Func<IEnumerable<IFluentPathValue>, IEnumerable<IFluentPathValue>> _noParams;
        //private NoParams _noParams;

        //// Function with one parameter: the focus (input), the single parameter and the result
        ////private Func<IEnumerable<IFluentPathValue>, object, IEnumerable<IFluentPathValue>> _oneParam;
        //private OneParam _oneParam;

        //// Function with two parameters: the focus (input), the two parameters and the result
        ////private Func<IEnumerable<IFluentPathValue>, object, object, IEnumerable<IFluentPathValue>> _twoParams;
        //private TwoParams _twoParams;

        //public string Name { get; private set;  }

        //public int NumOptional { get; private set; }

        //public string[] ParamNames { get; private set; }

        //public Binding(string name, NoParams func)
        //{
        //    _noParams = func;
        //    Name = name;
        //    ParamNames = new string[] { };
        //    NumOptional = 0;
        //}

        //public Binding(string name, OneParam func, string paramName1, bool isOptional)
        //{
        //    _oneParam = func;
        //    Name = name;
        //    ParamNames = new[] { paramName1 };
        //    NumOptional = isOptional ? 1 : 0;

        //}

        //public Binding(string name, TwoParams func, string paramName1, string paramName2, int numOptional)
        //{
        //    _twoParams = func;
        //    Name = name;
        //    ParamNames = new[] { paramName1, paramName2 };
        //    NumOptional = numOptional;
        //}

        //public IEnumerable<IFluentPathValue> Invoke(IEnumerable<IFluentPathValue> focus, IEnumerable<object> paramList)
        //{
        //    if (_noParams != null)
        //        return invoke(focus, _noParams, paramList);
        //    if (_oneParam != null)
        //        return invoke(focus, _oneParam, paramList);
        //    if (_twoParams != null)
        //        return invoke(focus, _twoParams, paramList);
        //    return null;
        //}

        //private IEnumerable<IFluentPathValue> invoke(IEnumerable<IFluentPathValue> focus, NoParams func, IEnumerable<object> paramList)
        //{
        //    if (!paramList.Any())
        //        return func(focus);
        //    else
        //        throw Error.Argument("Function '{0}' takes no parameters".FormatWith(Name));
        //}

        //private IEnumerable<IFluentPathValue> invoke(IEnumerable<IFluentPathValue> focus, OneParam func, IEnumerable<object> paramList)
        //{
        //    if (paramList.Count() == 1)
        //        return func(focus, paramList.Single());
        //    else if (paramList.Count() == 0 && NumOptional == 1)
        //        return func(focus, null);
        //    else
        //        throw Error.Argument("Function '{0}' takes {1} parameter '{2}'".
        //            FormatWith(Name, (NumOptional == 1 ? "one optional" : "exactly one"), ParamNames[0]));
        //}

        //private IEnumerable<IFluentPathValue> invoke(IEnumerable<IFluentPathValue> focus, TwoParams func, IEnumerable<object> paramList)
        //{
        //    if (paramList.Count() == 2)
        //        return func(focus, paramList.First(), paramList.Skip(1).First());
        //    else if (paramList.Count() == 1 && NumOptional == 1)
        //        return func(focus, paramList.First(), null);
        //    else if (paramList.Count() == 0 && NumOptional == 2)
        //        return func(focus, null, null);
        //    else
        //        throw Error.Argument("Function '{0}' takes {1} parameter '{2}' and {3} parameter '{4}'".
        //            FormatWith(Name, (NumOptional == 0) ? "one" : "one optional", ParamNames[0],
        //                    (NumOptional == 0) ? "one" : (NumOptional == 1) ? "one optional" : "one", ParamNames[1]));
        //}

        private static Dictionary<string, Binding> _functions = new Dictionary<string, Binding>();

        public static IReadOnlyDictionary<string, Binding> Functions
        {
            get { return _functions; }
        }


        public string Name { get; private set; }
        public Invokee Function { get; private set; }

        public ArgCountChecker ArgCountCheckers { get; private set; }

        public IEnumerable<ArgumentChecker> ArgumentCheckers { get; private set; }
       
        public delegate void ArgCountChecker(FunctionCallExpression func);
        public delegate void ArgumentChecker(Expression argument);

        static Binding()
        {
            add("not", (f, _) => f.Not(), None());
            add("empty", (f, _) => f.IsEmpty(), None());
            add("exists", (f, _) => f.Exists(), None());
            add("builtin.children", (f, a) => f.Children(a[0].AsString()), Exactly(1), OfType("name", TypeInfo.String));
            add("count", (f, _) => f.CountItems(), None());
            add("builtin.=", (f, a) => f.IsEqualTo(a[0]), Exactly(1));
            add("builtin.+", (f, a) => f.Add(a[0]), Exactly(1));
        }


        private static void add(string name, Invokee func, ArgCountChecker countChecker, params ArgumentChecker[] checkers)
        {
            _functions.Add(name, new Binding(name, func, countChecker, checkers));
        }


        private Binding(string name, Invokee function, ArgCountChecker countChecker, params ArgumentChecker[] checkers)
        {
            Name = name;
            Function = function;
            ArgCountCheckers = countChecker; 
            ArgumentCheckers = checkers;
        }
        
      
        public delegate IEnumerable<IFluentPathValue> Invokee(IEnumerable<IFluentPathValue> focus, IList<IEnumerable<IFluentPathValue>> arguments);


        public void Validate(FunctionCallExpression expression)
        {
            ArgCountCheckers(expression);

            expression.Arguments.Zip((IEnumerable<ArgumentChecker>)ArgumentCheckers, (a, c) => { c(a); return true; });
        }

    
        internal static ArgumentChecker OfType(string name, TypeInfo type)
        {
            return arg =>
            {
                if (arg.ExpressionType != type && arg.ExpressionType != TypeInfo.Any)
                {
                    throw Error.Argument("Argument {0} must be of type {1}".FormatWith(name, type.Name));
                }
            };
        }

        internal static ArgCountChecker None()
        {
            return func =>
            {
                if (func.Arguments.Any())
                {
                    throw Error.Argument("Function '{0}' does not take any parameters".FormatWith(func.FunctionName));
                }
            };
        }

        internal static ArgCountChecker Exactly(int count)
        {
            return func =>
            {
                if (func.Arguments.Count() != count)
                {
                    throw Error.Argument("Function '{0}' takes exactly {1} parameter{2}."
                                .FormatWith(func.FunctionName, count, count == 1 ? "" : "s"));
                }               
            };
        }

        internal static ArgCountChecker Min(int count)
        {
            return func =>
            {
                if (func.Arguments.Count() < count)
                {
                    throw Error.Argument("Function '{0}' takes at least {1} parameters".FormatWith(func.FunctionName, count));
                }
            };
        }


        internal static ArgCountChecker Max(int count)
        {
            return func =>
            {
                if (func.Arguments.Count() > count)
                {
                    throw Error.Argument("Function '{0}' takes at most {1} parameters".FormatWith(func.FunctionName, count));
                }
            };
        }


        //public static Evaluator Infix(this Evaluator left, Operator op, Evaluator right)
        //{
        //    return (f,c) =>
        //    {
        //        var leftNodes = left(f,c);
        //        var rightNodes = right(f,c);

        //        IEnumerable<IFluentPathValue> result = null;

        //        switch (op)
        //        {
        //            case Operator.Equal:
        //                result = leftNodes.IsEqualTo(rightNodes); break;
        //            case Operator.Equivalent:
        //                result = leftNodes.IsEquivalentTo(rightNodes); break;
        //            case Operator.GreaterThan:
        //                result = leftNodes.GreaterThan(rightNodes); break;
        //            case Operator.GreaterOrEqual:
        //                result = leftNodes.GreaterOrEqual(rightNodes); break;
        //            case Operator.LessThan:
        //                result = leftNodes.LessThan(rightNodes); break;
        //            case Operator.LessOrEqual:
        //                result = leftNodes.LessOrEqual(rightNodes); break;
        //            case Operator.Add:
        //                result = leftNodes.Add(rightNodes); break;
        //            case Operator.Sub:
        //                result = leftNodes.Sub(rightNodes); break;
        //            case Operator.Mul:
        //                result = leftNodes.Mul(rightNodes); break;
        //            case Operator.Div:
        //                result = leftNodes.Div(rightNodes); break;
        //            case Operator.And:
        //                result = leftNodes.And(rightNodes); break;
        //            case Operator.Or:
        //                result = leftNodes.Or(rightNodes); break;
        //            case Operator.Xor:
        //                result = leftNodes.Xor(rightNodes); break;
        //            case Operator.Implies:
        //                result = leftNodes.Implies(rightNodes); break;
        //            case Operator.Union:
        //                result = leftNodes.Union(rightNodes); break;
        //            case Operator.Concat:
        //                result = leftNodes.Add(rightNodes); break;  // should only work for strings ;-)                        
        //            case Operator.In:
        //                result = leftNodes.SubsetOf(rightNodes); break;
        //            default:
        //                throw Error.NotImplemented("Infix operator '{0}' is not yet implemented".FormatWith(op));
        //        }

        //public static readonly Parser<Evaluator> Where = CreateFunctionParser("where", "criterium", Eval.Where);
        //public static readonly Parser<Evaluator> All = CreateFunctionParser("all", "criterium", Eval.All);
        //public static readonly Parser<Evaluator> Any = CreateFunctionParser("any", "criterium", Eval.Any, optional:true);
        //public static readonly Parser<Evaluator> Item = CreateFunctionParser("item", "index", Eval.Item);
        //public static readonly Parser<Evaluator> First = CreateFunctionParser("first", Eval.First);
        //public static readonly Parser<Evaluator> Last = CreateFunctionParser("last", Eval.Last);
        //public static readonly Parser<Evaluator> Tail = CreateFunctionParser("tail", Eval.Tail);
        //public static readonly Parser<Evaluator> Skip = CreateFunctionParser("skip", "num", Eval.Skip);
        //public static readonly Parser<Evaluator> Take = CreateFunctionParser("take", "num", Eval.Take);
        //public static readonly Parser<Evaluator> Count = CreateFunctionParser("count", Eval.Count);
        //public static readonly Parser<Evaluator> AsInteger = CreateFunctionParser("asInteger", Eval.AsInteger);
        //public static readonly Parser<Evaluator> StartsWith = CreateFunctionParser("startsWith", "prefix", Eval.StartsWith);
        //public static readonly Parser<Evaluator> Log = CreateFunctionParser("log", "argument", Eval.Log);
        //public static readonly Parser<Evaluator> Today = CreateFunctionParser("today", Eval.Today);
        ////public static readonly Parser<Evaluator> Resolve = CreateFunctionParser("resolve", Eval.Resolve);
        //public static readonly Parser<Evaluator> Length = CreateFunctionParser("length", Eval.Length);
        //public static readonly Parser<Evaluator> Distinct = CreateFunctionParser("distinct", Eval.Distinct);
        //public static readonly Parser<Evaluator> Contains = CreateFunctionParser("contains", "substring", Eval.Contains);
        //public static readonly Parser<Evaluator> Matches = CreateFunctionParser("matches", "regexp", Eval.Matches);
        //public static readonly Parser<Evaluator> Extension = CreateFunctionParser("extension", "url", Eval.Extension);
        //public static readonly Parser<Evaluator> Substring = CreateFunctionParser("substring", "start", "length", Eval.Substring, numOptional:1);
        //public static readonly Parser<Evaluator> Select = CreateFunctionParser("select", "mapper", Eval.Select);
    }
}
