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
    internal class Lexer
    {
        // recurse: '*';
        public static readonly Parser<string> Recurse =
            Parse.Char('*').Once().Text();

        // axis_spec: '*' | '**' ;
        public static readonly Parser<string> AxisSpec =
            Parse.Char('*').Repeat(1, 2).Text();

        // root_spec: '$context' | '$resource' | '$parent';
        public static readonly Parser<string> RootSpec =
            from first in Parse.Char('$')
            from spec in (Parse.String("context")
                .Or(Parse.String("resource"))
                .Or(Parse.String("parent")))
            select new String(spec.ToArray());


        //ID: ALPHA ALPHANUM* ;
        //fragment ALPHA: [a-zA-Z];
        //fragment ALPHANUM: ALPHA | [0-9];
        public static readonly Parser<string> Id =
            Parse.Identifier(Parse.Letter, Parse.LetterOrDigit);

        // CHOICE: '[x]';
        public static readonly Parser<string> Choice =
            Parse.String("[x]").Text();

        // element: ID CHOICE?;
        public static readonly Parser<string> Element =
            from id in Id
            from choice in Choice.Optional()
            select id + choice.GetOrDefault();

        // CONST: '%' ALPHANUM(ALPHANUM | [\-.])*;
        public static readonly Parser<string> Const =
            from perc in Parse.Char('%')
            from name in Parse.LetterOrDigit.Once().Concat(
                    Parse.LetterOrDigit
                        .XOr(Parse.Chars("-.")).Many())
                    .Text()
            select name;

        // COMP: '=' | '~' | '!=' | '!~' | '>' | '<' | '<=' | '>=' | 'in';
        public static readonly Parser<string> Comp =
            Parse.Chars("=~").Select(s => new char[] { s })
            .Or(Parse.String("!="))
            .Or(Parse.String("!~"))
            .Or(Parse.String("<="))
            .Or(Parse.String(">="))
            .Or(Parse.String(">"))
            .Or(Parse.String("<"))
            .Or(Parse.String("in"))
            .Text();

        // LOGIC: 'and' | 'or' | 'xor';
        public static readonly Parser<string> Logic =
            Parse.String("and")
            .XOr(Parse.String("or"))
            .XOr(Parse.String("xor"))
            .Text();

        // NUMBER: INT '.' [0-9]+ EXP? | INT EXP | INT;
        // fragment INT: '0' | [1-9][0-9]*;
        // fragment EXP: [Ee][+\-]? INT;
        public static readonly Parser<string> Int =
            (from first in Parse.Chars("123456789").Once().Text()
             from rest in Parse.Digit.Many().Text()
             select first + rest)
            .XOr(Parse.Char('0').Once().Text());


        public static readonly Parser<string> Number = Int;   

        // STRING: '"' (ESC | ~["\\])* '"' |           // " delineated string
        //         '\'' (ESC | ~[\'\\])* '\'';         // ' delineated string
        // fragment ESC: '\\' (["'\\/bfnrt] | UNICODE);    // allow \", \', \\, \/, \b, etc. and \uXXX
        // fragment UNICODE: 'u' HEX HEX HEX HEX;
        // fragment HEX: [0-9a-fA-F];
        public static readonly Parser<string> Unicode =
            from u in Parse.Char('u')
            from hex in Parse.Chars("0123456789ABCDEFabcdef").Repeat(4).Text()
            select u + hex;

        public static readonly Parser<string> Escape =
            from backslash in Parse.Char('\\')
            from escUnicode in
                Parse.Chars("\"'\\/bfnrt").Once().Text().XOr(Unicode)
            select backslash + escUnicode;

        public static Parser<string> makeStringContentParser(char delimiter)
        {
            return Parse.CharExcept(delimiter + "\\").Many().Text().XOr(Escape)
                .Many().Select(ss => ss.Aggregate(string.Empty, (a, b) => a + b))
                .Contained(Parse.Char(delimiter), Parse.Char(delimiter));

        }

        public static readonly Parser<string> String =
            makeStringContentParser('"').XOr(makeStringContentParser('\''));

        // BOOL: 'true' | 'false';
        public static readonly Parser<string> Bool =
            Parse.String("true").Text().XOr(Parse.String("false")).Text();

    }
}
