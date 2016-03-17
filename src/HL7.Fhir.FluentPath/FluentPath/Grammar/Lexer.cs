/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.FluentPath.Grammar
{
    internal class Lexer
    {
        // recurse: '*';
        public static readonly Parser<string> Recurse =
            Parse.Char('*').Once().Text().Named("Recurse");

        // root_spec: '$context' | '$resource' | '$parent';
        public static readonly Parser<Axis> RootSpec =
            (from first in Parse.Char('$')
             from spec in Parse.String("context").Return(Axis.Context)
                 .Or(Parse.String("resource").Return(Axis.Resource))
                 .Or(Parse.String("parent").Return(Axis.Parent))
                 .Or(Parse.String("focus").Return(Axis.Focus))
             select spec).Named("RootSpec");

        // axis_spec: '*' | '**' | '$context' | '$resource' | '$parent' | '$focus';
        public static readonly Parser<Axis> AxisSpec =
                Parse.Char('*').Repeat(1, 2).Select(s => s.Count() == 1 ? Axis.Children : Axis.Descendants)
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


        internal static Parser<InfixOperator> Operator(string op, InfixOperator opType)
        {
            return Parse.String(op).Token().Return(opType);
        }

        //internal static readonly Parser<InfixOperator> Invoke = Operator(".", InfixOperator.Invoke);

        internal static readonly Parser<InfixOperator> Mul = Operator("*", InfixOperator.Mul);
        internal static readonly Parser<InfixOperator> Div = Operator("/", InfixOperator.Div);
        internal static readonly Parser<InfixOperator> Add = Operator("+", InfixOperator.Add);
        internal static readonly Parser<InfixOperator> Sub = Operator("-", InfixOperator.Sub);
        internal static readonly Parser<InfixOperator> Union = Operator("|", InfixOperator.Union);
        internal static readonly Parser<InfixOperator> Concat = Operator("&", InfixOperator.Concat);

        internal static readonly Parser<InfixOperator> Equal = Operator("=", InfixOperator.Equals);
        internal static readonly Parser<InfixOperator> Equivalent = Operator("~", InfixOperator.Equivalent);
        internal static readonly Parser<InfixOperator> NotEqual = Operator("!=", InfixOperator.NotEqual);
        internal static readonly Parser<InfixOperator> NotEquivalent = Operator("!~", InfixOperator.NotEquivalent);
        internal static readonly Parser<InfixOperator> GreaterThan = Operator(">", InfixOperator.GreaterThan);
        internal static readonly Parser<InfixOperator> LessThan = Operator("<", InfixOperator.LessThan);
        internal static readonly Parser<InfixOperator> LessOrEqual = Operator("<=", InfixOperator.LessOrEqual);
        internal static readonly Parser<InfixOperator> GreaterOrEqual = Operator(">=", InfixOperator.GreaterOrEqual);
        internal static readonly Parser<InfixOperator> In = Operator("in", InfixOperator.In);

        internal static readonly Parser<InfixOperator> And = Operator("and", InfixOperator.And);
        internal static readonly Parser<InfixOperator> Or = Operator("or", InfixOperator.Or);
        internal static readonly Parser<InfixOperator> Xor = Operator("xor", InfixOperator.Xor);
        internal static readonly Parser<InfixOperator> Implies = Operator("implies", InfixOperator.Implies);

        // COMP: '=' | '~' | '!=' | '!~' | '>' | '<' | '<=' | '>=' | 'in';
        // NOTE: ORDER MATTERS, since otherwise shorter ops will be recognized before longer ones!
        public static readonly Parser<InfixOperator> Comp =
            Equal
            .Or(Equivalent)
            .Or(NotEqual)
            .Or(NotEquivalent)
            .Or(LessOrEqual)
            .Or(GreaterOrEqual)
            .Or(GreaterThan)
            .Or(LessThan)
            .Or(In)
            .Named("Comp");

       // LOGIC: 'and' | 'or' | 'xor' | 'implies';
        public static readonly Parser<InfixOperator> Logic =
            And
            .Or(Or)
            .Or(Xor)
            .Or(Implies)
            .Named("Logic");


        public static readonly Parser<Int64> Number =
            Parse.Number.Select(s => Int64.Parse(s));

        public static readonly Parser<decimal> DecimalNumber =
                   from num in Parse.Number
                   from dot in Parse.Char('.')
                   from fraction in Parse.Number
                   select XmlConvert.ToDecimal(num + dot + fraction);

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
