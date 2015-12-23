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

namespace Hl7.Fhir.FhirPath.Grammar
{
    internal class Lexer
    {
        // recurse: '*';
        public static readonly Parser<string> Recurse =
            Parse.Char('*').Once().Text().Named("Recurse");

        // root_spec: '$context' | '$resource' | '$parent';
        public static readonly Parser<string> RootSpec =
            (from first in Parse.Char('$')
             from spec in (Parse.String("context")
                 .Or(Parse.String("resource"))
                 .Or(Parse.String("parent"))
                 .Or(Parse.String("focus")))
             select new String(spec.ToArray())).Named("RootSpec");

        // axis_spec: '*' | '**' | '$context' | '$resource' | '$parent' | '$focus';
        public static readonly Parser<string> AxisSpec =
            Parse.Char('*').Repeat(1, 2).Text()
                .XOr(RootSpec)
                .Named("AxisSpec");

        //ID: ALPHA ALPHANUM* ;
        //fragment ALPHA: [a-zA-Z];
        //fragment ALPHANUM: ALPHA | [0-9];
        public static readonly Parser<string> Id =
            Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).Named("Id");


        // CHOICE: '[x]';
        public static readonly Parser<string> Choice =
            Parse.String("[x]").Text().Named("Choice");

        // element: ID CHOICE?;
        public static readonly Parser<string> Element =
            (from id in Id
            from choice in Choice.Optional()
            select id + choice.GetOrDefault()).Named("Element");

        // CONST: '%' ALPHANUM(ALPHANUM | [\-.])*;
        public static readonly Parser<string> Const =
            (from perc in Parse.Char('%')
            from name in Parse.LetterOrDigit.Once().Concat(
                    Parse.LetterOrDigit
                        .XOr(Parse.Chars("-.")).Many())
                    .Text()
            select name).Named("Const");

        // NB: This regex has been modified so it REQUIRES a month to be present (otherwise we cannot distinguish a number from a year-only date
        // This needs to be fixed in the FhirPath spec.
        // Original: public const string DATETIME_REGEX = @"-?[0-9]{4}(-(0[1-9]|1[0-2])(-(0[0-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00)))?)?)?";
        public const string DATETIME_REGEX = @"-?[0-9]{4}(-(0[1-9]|1[0-2])(-(0[0-9]|[1-2][0-9]|3[0-1])(T([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00)))?)?)";

        public static readonly Parser<PartialDateTime> DateTime = Parse.Regex(DATETIME_REGEX)
            .Select(s => PartialDateTime.Parse(s));
    
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
            .Text().Named("Comp");

        // LOGIC: 'and' | 'or' | 'xor';
        public static readonly Parser<string> Logic =
            Parse.String("and")
            .XOr(Parse.String("or"))
            .XOr(Parse.String("xor"))
            .XOr(Parse.String("implies"))
            .Text().Named("Logic");


        public static readonly Parser<decimal> Number =
            Parse.DecimalInvariant.Select(s => Decimal.Parse(s));

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
        public static readonly Parser<bool> Bool =
            Parse.String("true").XOr(Parse.String("false")).Text().Select(s => Boolean.Parse(s));

    }
}
