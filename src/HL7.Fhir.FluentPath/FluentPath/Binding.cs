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

        ////public static readonly Binding Not = new Binding("not", IFluentPathValueListExtensions.Not);
        ////public static readonly Binding Empty = new Binding("empty", IFluentPathValueListExtensions.IsEmpty);


        public static Evaluator Not(Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            arguments.None();

            return invoke(focus, arguments, (f,_) => f.Not());                       
        }

        public static Evaluator Empty(Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            arguments.None();

            return invoke(focus, arguments, (f,_) => f.IsEmpty());
        }

        public static Evaluator Exists(Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            arguments.None();

            return invoke(focus, arguments, (f,_) => f.Exists());
        }

        public static Evaluator Builtin_Children(Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            arguments.Exactly(1);

            return invoke(focus, arguments, (f,a) => f.Children(a[0].AsString()));
        }


        delegate IEnumerable<IFluentPathValue> Invokee(IEnumerable<IFluentPathValue> focus, IList<IEnumerable<IFluentPathValue>> arguments);


        private static Evaluator invoke(Evaluator focus, IEnumerable<Evaluator> arguments, Invokee func)
        {
            return ctx =>
                {
                    var focusNodes = focus(ctx);
                    var argNodes = arguments.Select(arg => arg(ctx)).ToList();

                    try
                    {
                        ctx.FocusStack.Push(focusNodes);
                        return func(focusNodes, argNodes);
                    }
                    finally
                    {
                        ctx.FocusStack.Pop();
                    }
                };
        }


        public static Evaluator Bind(string name, Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            try
            {
                if (name == "not") return Not(focus, arguments);
                if (name == "empty") return Empty(focus, arguments);
                if (name == "exists") return Exists(focus, arguments);
                if (name == Expression.OP_PREFIX + "children") return Builtin_Children(focus, arguments);
            }
            catch(ArgumentException e)
            {
                throw Error.Argument("Cannot bind to function " + name + ": " + e.Message);
            }

            return ctx => ctx.InvokeExternalFunction(name, arguments.Select(arg => arg(ctx)));
        }

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

    internal static class ArgumentAssertionExtensions
    {
        internal static void None(this IEnumerable<Evaluator> args)
        {
            if(args != null && args.Any())
            {
                throw Error.Argument("Function does not take any parameters");
            }
        }

        internal static void Exactly(this IEnumerable<Evaluator> args, int count)
        {
            if (args == null || args.Count() != count)
            {
                throw Error.Argument("Function takes exactly {0} parameter{1}."
                            .FormatWith(count, count == 1 ? "" : "s"));
            }

        }

        internal static void Min(this IEnumerable<Evaluator> args, int count)
        {
            if (args == null || args.Count() < count)
            {
                throw Error.Argument("Function takes at least {0} parameters".FormatWith(count));
            }
        }


        internal static void Max(this IEnumerable<Evaluator> args, int count)
        {
            if (args == null || args.Count() > count)
            {
                throw Error.Argument("Function takes at most {0} parameters".FormatWith(count));
            }
        }
    }


}
