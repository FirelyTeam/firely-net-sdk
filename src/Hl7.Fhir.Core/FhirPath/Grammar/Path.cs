/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Sprache;
using System.Linq;

// The FhirPath parser is using Sprache, a monad-based approach to parsing
// Useful pointers:
// http://ericlippert.com/2013/02/21/monads-part-one/
// http://blogs.msdn.com/b/wesdyer/archive/2008/01/11/the-marvels-of-monads.aspx
// https://github.com/louthy/csharp-monad
// http://www.codeproject.com/Articles/649989/Monad-like-programming-with-Csharp


namespace Hl7.Fhir.FhirPath.Grammar
{

    internal class Path
    {
        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;
        public static readonly Parser<Evaluator> Function =
            from name in Lexer.Id.Token()
            from lparen in Parse.Char('(')
            from paramList in Parse.Ref(() => Expression.Expr.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
            from rparen in Parse.Char(')')
            select Eval.Function(name, paramList.GetOrElse(Enumerable.Empty<Evaluator>()));

        // item: element recurse? | function | axis_spec | '(' expr ')';
        public static readonly Parser<Evaluator> ElementPath =
            from element in Lexer.Element
            from recurse in Lexer.Recurse.Optional()
                //            select element + recurse.GetOrDefault();
            select Eval.Invoke(l => l.Children(element));

        //public static readonly Parser<string> Item =
        //    Function
        //    .Or(ElementPath)
        //    .XOr(Lexer.AxisSpec)
        //    .XOr(Parse.Ref(() => Expression.BracketExpr));
        public static readonly Parser<Evaluator> Item =
            Function
            .Or(ElementPath)
            //.XOr(Lexer.AxisSpec)
            .XOr(Parse.Ref(() => Expression.BracketExpr))
            .Token();

        // predicate: item ('.' item)* ;
        public static readonly Parser<Evaluator> Predicate =
            from itemList in Item.DelimitedBy(Parse.Char('.'))
            select Eval.Chain(itemList);
    }
}
