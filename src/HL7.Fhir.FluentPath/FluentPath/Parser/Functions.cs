using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Parser
{
    internal static class Functions
    {
        public static Parser<Expression> Function(Expression context)
        {
            return 
                from n in Lexer.Identifier.Select(name => name)
                from lparen in Parse.Char('(')
                from paramList in Parse.Ref(() => Grammar.Expression.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
                from rparen in Parse.Char(')')
                select new FunctionCallExpression(context, n, TypeInfo.Any, paramList.GetOrElse(Enumerable.Empty<Expression>()));
        }


        //internal static Parser<IEnumerable<Evaluator>> createFunctionParser(string name)
        //{
        //    return
        //        from n in Parse.String(name).Token()
        //        from lparen in Parse.Char('(')
        //        from paramList in Parse.Ref(() => Grammar.Expr.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
        //        from rparen in Parse.Char(')')
        //        select paramList.GetOrElse(Enumerable.Empty<Evaluator>());
        //}

        internal static Parser<Evaluator> CreateFunctionParser(string name, Func<Evaluator> func)
        {
            return null;
         //   return createFunctionParser(name).Select(p => invoke(func, p, name));
        }

        internal static Parser<Evaluator> CreateFunctionParser(string name, string paramName, Func<Evaluator,Evaluator> func, bool optional=false)
        {
            return null;
            // return createFunctionParser(name).Select(p => invoke(func, p, name, paramName, optional));
        }

        internal static Parser<Evaluator> CreateFunctionParser(string name, string paramName1, string paramName2, Func<Evaluator, Evaluator, Evaluator> func, int numOptional=0)
        {
            return null;

            // return createFunctionParser(name).Select(p => invoke(func, p, name, paramName1, paramName2, numOptional));
        }

        internal static Evaluator invoke(Func<Evaluator> func, IEnumerable<Evaluator> paramList, string name)
        {
            if (!paramList.Any())
                return func();
            else
                throw Error.Argument("Function '{0}' takes no parameters".FormatWith(name));
        }

        private static Evaluator invoke(Func<Evaluator, Evaluator> func, IEnumerable<Evaluator> paramList, string name, string paramName, bool optional)
        {
            if (paramList.Count() == 1)
                return func(paramList.Single());
            else if (paramList.Count() == 0 && optional)
                return func(null);
            else
                throw Error.Argument("Function '{0}' takes {1} parameter '{2}'".
                    FormatWith(name, (optional ? "one optional" : "exactly one"), paramName));
        }

        private static Evaluator invoke(Func<Evaluator, Evaluator, Evaluator> func, IEnumerable<Evaluator> paramList, string name, string paramName1, string paramName2, int numOptional)
        {
            if (paramList.Count() == 2)
                return func(paramList.First(), paramList.Skip(1).First());
            else if (paramList.Count() == 1 && numOptional == 1)
                return func(paramList.First(), null);
            else if (paramList.Count() == 0 && numOptional == 2)
                return func(null, null);
            else
                throw Error.Argument("Function '{0}' takes {1} parameter '{2}' and {3} parameter '{4}'".
                    FormatWith(name, (numOptional == 0) ? "one" : "one optional", paramName1,
                            (numOptional == 0) ? "one" : (numOptional == 1) ? "one optional" : "one", paramName2));
        }

        public static readonly Parser<Evaluator> Not = CreateFunctionParser("not", Eval.Not);
        public static readonly Parser<Evaluator> Empty = CreateFunctionParser("empty", Eval.Empty);
        public static readonly Parser<Evaluator> Where = CreateFunctionParser("where", "criterium", Eval.Where);
        public static readonly Parser<Evaluator> All = CreateFunctionParser("all", "criterium", Eval.All);
        public static readonly Parser<Evaluator> Any = CreateFunctionParser("any", "criterium", Eval.Any, optional:true);
        public static readonly Parser<Evaluator> Item = CreateFunctionParser("item", "index", Eval.Item);
        public static readonly Parser<Evaluator> First = CreateFunctionParser("first", Eval.First);
        public static readonly Parser<Evaluator> Last = CreateFunctionParser("last", Eval.Last);
        public static readonly Parser<Evaluator> Tail = CreateFunctionParser("tail", Eval.Tail);
        public static readonly Parser<Evaluator> Skip = CreateFunctionParser("skip", "num", Eval.Skip);
        public static readonly Parser<Evaluator> Take = CreateFunctionParser("take", "num", Eval.Take);
        public static readonly Parser<Evaluator> Count = CreateFunctionParser("count", Eval.Count);
        public static readonly Parser<Evaluator> AsInteger = CreateFunctionParser("asInteger", Eval.AsInteger);
        public static readonly Parser<Evaluator> StartsWith = CreateFunctionParser("startsWith", "prefix", Eval.StartsWith);
        public static readonly Parser<Evaluator> Log = CreateFunctionParser("log", "argument", Eval.Log);
        public static readonly Parser<Evaluator> Today = CreateFunctionParser("today", Eval.Today);
        //public static readonly Parser<Evaluator> Resolve = CreateFunctionParser("resolve", Eval.Resolve);
        public static readonly Parser<Evaluator> Length = CreateFunctionParser("length", Eval.Length);
        public static readonly Parser<Evaluator> Distinct = CreateFunctionParser("distinct", Eval.Distinct);
        public static readonly Parser<Evaluator> Contains = CreateFunctionParser("contains", "substring", Eval.Contains);
        public static readonly Parser<Evaluator> Matches = CreateFunctionParser("matches", "regexp", Eval.Matches);
        public static readonly Parser<Evaluator> Extension = CreateFunctionParser("extension", "url", Eval.Extension);
        public static readonly Parser<Evaluator> Substring = CreateFunctionParser("substring", "start", "length", Eval.Substring, numOptional:1);
        public static readonly Parser<Evaluator> Select = CreateFunctionParser("select", "mapper", Eval.Select);

        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;
        //public static readonly Parser<Evaluator> OtherFunction =
        //    from name in Lexer.Id.Token()
        //    from lparen in Parse.Char('(')
        //    from paramList in Parse.Ref(() => Grammar.Expr.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
        //    from rparen in Parse.Char(')')
        //    select Eval.Function(name, paramList.GetOrElse(Enumerable.Empty<Evaluator>()));


        //public static readonly Parser<Evaluator> Function = Not.Or(Empty).Or(Where).Or(All).Or(Any).Or(Item)
        //                .Or(First).Or(Last).Or(Tail).Or(Skip).Or(Take).Or(Count).Or(AsInteger).Or(StartsWith)
        //                //.Or(Log).Or(Resolve).Or(Length).Or(Distinct).Or(Contains).Or(Matches).Or(Extension)
        //                .Or(Log).Or(Length).Or(Distinct).Or(Contains).Or(Matches).Or(Extension)
        //                .Or(Substring).Or(Select).Or(Today)
        //                .Or(OtherFunction);
    }
}
