using Hl7.Fhir.Support;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath.Grammar
{
    internal static class Functions
    {
        internal static Parser<IEnumerable<Evaluator>> createFunctionParser(string name)
        {
            return
                from n in Parse.String(name).Token()
                from lparen in Parse.Char('(')
                from paramList in Parse.Ref(() => Expression.Expr.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
                from rparen in Parse.Char(')')
                select paramList.GetOrElse(Enumerable.Empty<Evaluator>());
        }

        internal static Parser<Evaluator> CreateScalarFunctionParser(string name, Func<ScalarEvaluator> func)
        {
            return createFunctionParser(name).Select(p => invoke(() => Eval.CastToCollection(func()), p, name));
        }

        internal static Parser<Evaluator> CreateScalarFunctionParser(string name, string paramName, Func<Evaluator, ScalarEvaluator> func)
        {
            return createFunctionParser(name).Select(p => invoke(paramEval => Eval.CastToCollection(func(paramEval)), p, name, paramName));
        }

        internal static Parser<Evaluator> CreateFunctionParser(string name, Func<Evaluator> func)
        {
            return createFunctionParser(name).Select(p => invoke(func, p, name));
        }

        internal static Parser<Evaluator> CreateFunctionParser(string name, string paramName, Func<Evaluator,Evaluator> func)
        {
            return createFunctionParser(name).Select(p => invoke(func, p, name, paramName));
        }


        internal static Evaluator invoke(Func<Evaluator> func, IEnumerable<Evaluator> paramList, string name)
        {
            if (!paramList.Any())
                return func();
            else
                throw Error.Argument("Function '{0}' takes no parameters".FormatWith(name));
        }

        private static Evaluator invoke(Func<Evaluator, Evaluator> func, IEnumerable<Evaluator> paramList, string name, string paramName)
        {
            if (paramList.Count() == 1)
            {
                return func(paramList.Single());
            }
            else
                throw Error.Argument("Function '{0}' takes exactly one parameter '{1}'".FormatWith(name, paramName));
        }

        public static readonly Parser<Evaluator> Not = CreateScalarFunctionParser("not", Eval.Not);
        public static readonly Parser<Evaluator> Empty = CreateScalarFunctionParser("empty", Eval.Empty);
        public static readonly Parser<Evaluator> Where = CreateFunctionParser("where", "criterium", Eval.Where);
        public static readonly Parser<Evaluator> All = CreateScalarFunctionParser("all", "criterium", Eval.All);

        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;
        public static readonly Parser<Evaluator> OtherFunction =
            from name in Lexer.Id.Token()
            from lparen in Parse.Char('(')
            from paramList in Parse.Ref(() => Expression.Expr.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
            from rparen in Parse.Char(')')
            select Eval.Function(name, paramList.GetOrElse(Enumerable.Empty<Evaluator>()));


        public static readonly Parser<Evaluator> Function = Not.Or(Empty).Or(Where).Or(OtherFunction);
    }
}
