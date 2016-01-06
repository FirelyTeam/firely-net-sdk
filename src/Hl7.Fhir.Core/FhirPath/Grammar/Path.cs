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

        // element recurse?
        public static readonly Parser<Evaluator> ElementPath =
            from element in Lexer.Element
            from recurse in Lexer.Recurse.Optional()
                //            select element + recurse.GetOrDefault();
            select Eval.Children(element);

        // item: element recurse? | function | axis_spec
        public static readonly Parser<Evaluator> Invoc =
            Function
            .Or(ElementPath)
            //.XOr(Lexer.AxisSpec)
            .Token();
    }
}
