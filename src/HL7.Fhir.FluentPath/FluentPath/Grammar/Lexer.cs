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
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.FluentPath.Grammar
{
    internal partial class Lexer
    {
        // IDENTIFIER
        //   : ([A-Za-z] | '_')([A-Za-z0-9] | '_')*            // Added _ to support CQL (FHIR could constrain it out)
        //   ;
        public static readonly Parser<string> Id =
            Parse.Identifier(Parse.Letter.XOr(Parse.Char('_')), Parse.LetterOrDigit.XOr(Parse.Char('_'))).Named("Identifier");

        //  QUOTEDIDENTIFIER
        //      : '"' (ESC | ~[\\"])* '"'
        //      ;
        public static readonly Parser<string> QuotedIdentifier =
            from openQ in Parse.Char('\"')
            from id in Parse.CharExcept('\"').Many().Text().XOr(Escape).Many()
            from closeQ in Parse.Char('\"')
            select string.Concat(id);

        // identifier
        //  : IDENTIFIER
        //  | QUOTEDIDENTIFIER
        //  ;
        public static readonly Parser<string> Identifier =
            Id.XOr(QuotedIdentifier);

        // externalConstant
        //  : '%' identifier
        //  ;
        public static readonly Parser<string> ExternalConstant =
            Parse.Char('%').Then(c => Identifier).Named("ExternalConstant");

        // DATETIME
        //      : '@'  ....
        // Note: I used a different regex from the spec, since this one is more complete (not allowing 99 as month for example),
        // but disallowing partial datetimes with just the hour. EK
        public static readonly Regex DateTimeRegEx = new Regex(
                                @"@[0-9]{4}             # Year
                                (
                                    -(0[1-9]|1[0-2])                # Month
                                    (
                                        -(0[0-9]|[1-2][0-9]|3[0-1])         #Day
                                        (
                                            T
                                            ([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?   #Time
                                            (Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?  #Timezone
                                        )?
                                    )?
                                )?", RegexOptions.IgnorePatternWhitespace);     

        public static readonly Parser<PartialDateTime> DateTime = 
            Parse.Regex(DateTimeRegEx).Select(s => PartialDateTime.Parse(s.Substring(1)));

        // TIME
        //      : '@T'  ....
        // Note: I used a different regex from the spec, since this one is more complete (not allowing 99 as an hour for example),
        // but disallowing partial times with just the hour. EK
        public static readonly Regex TimeRegEx = new Regex(
                                @"@T
                                            ([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9](\.[0-9]+)?   #Time
                                            (Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?  #Timezone
                                 ", RegexOptions.IgnorePatternWhitespace);

        public static readonly Parser<Hl7.Fhir.FluentPath.Time> Time =
            Parse.Regex(TimeRegEx).Select(s => Hl7.Fhir.FluentPath.Time.Parse(s.Substring(1)));

        // NUMBER
        //   : [0-9]+('.' [0-9]+)?
        //   ;
        public static readonly Parser<Int64> IntegerNumber =
            Parse.Number.Select(s => Int64.Parse(s)).Named("IntegerNumber");

        public static readonly Parser<decimal> DecimalNumber =
                   from num in Parse.Number
                   from dot in Parse.Char('.')
                   from fraction in Parse.Number
                   select XmlConvert.ToDecimal(num + dot + fraction);


        // STRING:  '\'' (ESC | ~[\'\\])* '\'';         // ' delineated string
        // fragment ESC: '\\' (["'\\/fnrt] | UNICODE);    // allow \", \', \\, \/, \b, etc. and \uXXX
        // fragment UNICODE: 'u' HEX HEX HEX HEX;
        // fragment HEX: [0-9a-fA-F];
        public static readonly Parser<string> Unicode =
            from u in Parse.Char('u')
            from hex in Parse.Chars("0123456789ABCDEFabcdef").Repeat(4).Text()
            select u + hex;

        public static readonly Parser<string> Escape =
            from backslash in Parse.Char('\\')
            from escUnicode in
                Parse.Chars("\"'\\/fnrt").Once().Text().XOr(Unicode)
            select backslash + escUnicode;

        public static Parser<string> makeStringContentParser(char delimiter)
        {
            return Parse.CharExcept(delimiter + "\\").Many().Text().XOr(Escape)
                .Many().Select(ss => ss.Aggregate(string.Empty, (a, b) => a + b))
                .Contained(Parse.Char(delimiter), Parse.Char(delimiter));
        }

        public static readonly Parser<string> String = makeStringContentParser('\'');

        // BOOL: 'true' | 'false';
        public static readonly Parser<bool> Bool =
            Parse.String("true").XOr(Parse.String("false")).Text().Select(s => Boolean.Parse(s));

        // literal
        //  : ('true' | 'false')                                    #booleanLiteral
        //  | STRING                                                #stringLiteral
        //  | NUMBER                                                #numberLiteral
        //  | DATETIME                                              #dateTimeLiteral
        //  | TIME                                                  #timeLiteral
        //  ;
        public static readonly Parser<IFluentPathValue> Literal =
            Lexer.String.Select(v => new ConstantValue(v))
                .XOr(Lexer.DateTime.Select(v=> new ConstantValue(v)))
                .XOr(Lexer.Time.Select(v => new ConstantValue(v)))
                .XOr(Lexer.Bool.Select(v => new ConstantValue(v)))
                .Or(Lexer.DecimalNumber.Select(v => new ConstantValue(v)))
                .Or(Lexer.IntegerNumber.Select(v => new ConstantValue(v)));

        //qualifiedIdentifier
        //   : identifier ('.' identifier)*
        //   ;
        public static readonly Parser<string> QualifiedIdentifier =
            Parse.ChainOperator(Parse.Char('.'), Identifier, (op, a, b) => a + "." + b);
    }
}
