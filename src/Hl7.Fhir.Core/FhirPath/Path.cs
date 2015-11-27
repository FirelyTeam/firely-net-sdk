/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using Sprache;
using System;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    internal class Path
    {
        // function: ID '(' param_list? ')';
        // param_list: expr(',' expr)*;
        public static readonly Parser<string> Function =
            (from id in Lexer.Id
             from lparen in Parse.Char('(')
             from paramlist in Parse.Ref(() => Expression.Expr.Named("parameter")).XDelimitedBy(Parse.Char(','))
             from rparen in Parse.Char(')')
             select id + "(" + String.Join(",", paramlist) + ")")
            .Named("Function");

        // item: element recurse? | function | axis_spec | '(' expr ')';
        public static readonly Parser<string> Item =
            Function
            .Or(from element in Lexer.Element
                 from recurse in Lexer.Recurse.Optional()
                 select element + recurse.GetOrDefault())
            .XOr(Lexer.AxisSpec)
            .XOr(Parse.Ref(() => Expression.BracketExpr));         
            
        // predicate: (root_spec | item) ('.' item)* ;
        public static readonly Parser<string> Predicate =
            from root in (Lexer.RootSpec.Or(Item))
            from after in (
                from dot in Parse.Char('.')
                from item in Item
                select item).Many()
            select root + "." + String.Join(".",after);                                
    }
}
