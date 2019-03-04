/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Model.Primitives;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.FhirPath.Parser
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
            from id in Parse.CharExcept(@"""\").Many().Text().Or(Escape).Many()
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
                                            ([01][0-9]|2[0-3])(:[0-5][0-9](:[0-5][0-9](\.[0-9]+)?)?)?   #Time
                                            (Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?  #Timezone
                                 ", RegexOptions.IgnorePatternWhitespace);

        public static readonly Parser<PartialTime> Time =
            Parse.Regex(TimeRegEx).Select(s => PartialTime.Parse(s.Substring(2)));

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
            select hex;

        public static readonly Parser<string> Escape =
            from backslash in Parse.Char('\\')
            from escUnicode in
                Parse.Chars("\"'\\/fnrt").Once().Unescape().Or(Unicode.Unescape())
            select escUnicode;

        public static readonly Parser<string> String =
            from openQ in Parse.Char('\'')
            from str in Parse.CharExcept("\'\\").Many().Text().Or(Escape).Many()
            from closeQ in Parse.Char('\'')
            select string.Concat(str);

  
        // BOOL: 'true' | 'false';
        public static readonly Parser<bool> Bool =
            Parse.String("true").XOr(Parse.String("false")).Text().Select(s => Boolean.Parse(s));

        //qualifiedIdentifier
        //   : identifier ('.' identifier)*
        //   ;
        public static readonly Parser<string> QualifiedIdentifier =
            Parse.ChainOperator(Parse.Char('.'), Identifier, (op, a, b) => a + "." + b);

        public static readonly Parser<string> Axis =
            Parse.Char('$').Then(q => Parse.String("this")).Text().Select(v => v);
    }


    public static class ParserExtensions
    {
        public static Parser<string> Unescape(this Parser<IEnumerable<char>> c)
        {
            return c.Select(chr => new String( Unescape(chr.Single()), 1));
        }

        public static char Unescape(char c)
        {
            // return the escaped character after a '\'
            switch (c)
            {
                case 'f': return '\f';
                case 'n': return '\n';
                case 'r': return '\r';
                case 't': return '\t';
                default: return c;
            }
        }

        public static Parser<string> Unescape(this Parser<string> c)
        {
            return c.Select(s => new String(Unescape(s), 1));
        }

        public static char Unescape(string unicodeHex)
        {
            return ((char)int.Parse(unicodeHex, NumberStyles.HexNumber));
        }

    }
}
