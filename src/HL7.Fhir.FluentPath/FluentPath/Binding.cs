using Hl7.Fhir.Support;
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
        private Func<object> _noParams;
        private Func<object, object> _oneParam;
        private Func<object, object, object> _twoParams;

        public string Name { get; private set;  }

        public int NumOptional { get; private set; }

        public string[] ParamNames { get; private set; }

        public Binding(string name, Func<object> func)
        {
            _noParams = func;
            Name = name;
            ParamNames = new string[] { };
            NumOptional = 0;
        }

        public Binding(string name, Func<object, object> func, string paramName1, bool isOptional)
        {
            _oneParam = func;
            Name = name;
            ParamNames = new[] { paramName1 };
            NumOptional = isOptional ? 1 : 0;

        }

        public Binding(string name, Func<object, object, object> func, string paramName1, string paramName2, int numOptional)
        {
            _twoParams = func;
            Name = name;
            ParamNames = new[] { paramName1, paramName2 };
            NumOptional = numOptional;
        }

        public object Invoke(IEnumerable<object> paramList)
        {
            if (_noParams != null)
                return invoke(_noParams, paramList);
            if (_oneParam != null)
                return invoke(_oneParam, paramList);
            if (_twoParams != null)
                return invoke(_twoParams, paramList);
            return null;
        }

        private object invoke(Func<object> func, IEnumerable<object> paramList)
        {
            if (!paramList.Any())
                return func();
            else
                throw Error.Argument("Function '{0}' takes no parameters".FormatWith(Name));
        }

        private object invoke(Func<object, object> func, IEnumerable<object> paramList)
        {
            if (paramList.Count() == 1)
                return func(paramList.Single());
            else if (paramList.Count() == 0 && NumOptional == 1)
                return func(null);
            else
                throw Error.Argument("Function '{0}' takes {1} parameter '{2}'".
                    FormatWith(Name, (NumOptional == 1 ? "one optional" : "exactly one"), ParamNames[0]));
        }

        private object invoke(Func<object, object, object> func, IEnumerable<object> paramList)
        {
            if (paramList.Count() == 2)
                return func(paramList.First(), paramList.Skip(1).First());
            else if (paramList.Count() == 1 && NumOptional == 1)
                return func(paramList.First(), null);
            else if (paramList.Count() == 0 && NumOptional == 2)
                return func(null, null);
            else
                throw Error.Argument("Function '{0}' takes {1} parameter '{2}' and {3} parameter '{4}'".
                    FormatWith(Name, (NumOptional == 0) ? "one" : "one optional", ParamNames[0],
                            (NumOptional == 0) ? "one" : (NumOptional == 1) ? "one optional" : "one", ParamNames[1]));
        }

        public static readonly Binding Not = new Binding("not", Eval.Not);
        //public static readonly Binding Empty = new Binding("empty", Eval.Empty);
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
