/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using Hl7.Fhir.Navigation;
using Hl7.Fhir.Support;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    internal class Path
    {
        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;
        //public static readonly Parser<string> Function =
        //    (from id in Lexer.Id
        //     from lparen in Parse.Char('(')
        //     from paramlist in Parse.Ref(() => Expression.Expr.Named("parameter")).XDelimitedBy(Parse.Char(','))
        //     from rparen in Parse.Char(')')
        //     select id + "(" + String.Join(",", paramlist) + ")")
        //    .Named("Function");

        // item: element recurse? | function | axis_spec | '(' expr ')';
        public static readonly Parser<string> ElementPath =
            from element in Lexer.Element
            from recurse in Lexer.Recurse.Optional()
//            from nolparen in Parse.Not(Parse.Char('('))
            select element + recurse.GetOrDefault();

        //public static readonly Parser<string> Item =
        //    Function
        //    .Or(ElementPath)
        //    .XOr(Lexer.AxisSpec)
        //    .XOr(Parse.Ref(() => Expression.BracketExpr));

        public static readonly Parser<Evaluator> Item =
            //Function
            ElementPath.Select(path => Eval.ChildrenWithName(path));
        //.XOr(Lexer.AxisSpec)
        //.XOr(Parse.Ref(() => Expression.BracketExpr));

        // predicate: item ('.' item)* ;
        public static readonly Parser<Evaluator> Predicate =
            from itemList in Item.XDelimitedBy(Parse.Char('.'))
            select Eval.Chain(itemList);



    }
}
