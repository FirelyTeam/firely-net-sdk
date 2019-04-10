/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.FhirPath;
using Hl7.FhirPath.Sprache;
using Hl7.FhirPath.Parser;
using Xunit;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Tests
{
    public class FhirPathLexerTest
    {
        [Fact]
        public void FhirPath_Lex_QualifiedIdentifier()
        {
            var parser = Lexer.QualifiedIdentifier.End();

            AssertParser.SucceedsMatch(parser, "name");
            AssertParser.SucceedsMatch(parser, "name.name2");
            AssertParser.SucceedsMatch(parser, "name.name2.name3");
            AssertParser.SucceedsMatch(parser, "name.\"name2\"", "name.name2");
            AssertParser.SucceedsMatch(parser, "\"name\".\"name2\"", "name.name2");
        }

        private void SucceedsPrefixString(Parser<string> parser, string expr)
        {
            AssertParser.SucceedsMatch(parser, expr, expr.Substring(1));
        }


        [Fact]
        public void FhirPath_Lex_Const()
        {
            var parser = Lexer.ExternalConstant.End();

            SucceedsPrefixString(parser, "%c");
            SucceedsPrefixString(parser, "%const");
            SucceedsPrefixString(parser, "%a1");
            SucceedsPrefixString(parser, "%a__1");
            AssertParser.SucceedsMatch(parser, "%\"forbidden-characters-1234\"", "forbidden-characters-1234");

            AssertParser.FailsMatch(parser, "%0");
            AssertParser.FailsMatch(parser, "%0123");
            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "%%");
            AssertParser.FailsMatch(parser, "%%a");
            AssertParser.FailsMatch(parser, "%-");
            AssertParser.FailsMatch(parser, "%*");
        }

        [Fact]
        public void FhirPath_Lex_Identifier()
        {
            var parser = Lexer.Identifier.End();

            AssertParser.SucceedsMatch(parser, "A34", "A34");
            AssertParser.SucceedsMatch(parser, "\"A\uface%$#34\"", "A\uface%$#34");
            AssertParser.FailsMatch(parser, "34");
            AssertParser.FailsMatch(parser, "'Hello'");
            AssertParser.FailsMatch(parser, "@2013");
            //AssertParser.FailsMatch(parser, "true"); - this is an identifier, parser will only call in right context so no ambiguity
        }


        private void SucceedsPartialDateTime(Parser<PartialDateTime> parser, string s)
        {
            AssertParser.SucceedsMatch(parser, s, PartialDateTime.Parse(s.Substring(1)));
        }

        [Fact]
        public void FhirPath_Lex_DateTime()
        {
            var parser = Lexer.DateTime.End();

            SucceedsPartialDateTime(parser, "@2015-01");
            SucceedsPartialDateTime(parser, "@2015-01-02T12:34:00Z");
            SucceedsPartialDateTime(parser, "@2015-01-03T12:34:34+02:30");
            SucceedsPartialDateTime(parser, "@2015-01-03T12:34:34");
      //      SucceedsPartialDateTime(parser, "@2015-01-01T23");  TODO: Make this work
            AssertParser.FailsMatch(parser, "@2015-32-02T12:34:00Z");
            AssertParser.FailsMatch(parser, "@2015-01-02T28:34:00Z");
            AssertParser.FailsMatch(parser, "T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@20150103T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@-2015-01");

        }

        private void SucceedsTime(Parser<PartialTime> parser, string s)
        {
            AssertParser.SucceedsMatch(parser, s, PartialTime.Parse(s.Substring(2)));
        }

        [Fact]
        public void FhirPath_Lex_Time()
        {
            var parser = Lexer.Time.End();

            SucceedsTime(parser, "@T12:34:00Z");
            SucceedsTime(parser, "@T12:34:34+02:30");
            SucceedsTime(parser, "@T12:34:34");

            // SucceedsTime(parser, "@T12:35");     TODO: make this work
            // SucceedsTime(parser, "@T12");

            AssertParser.FailsMatch(parser, "2001-01-01T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@2001-01-01T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "T12:34:34+02:30");
            AssertParser.FailsMatch(parser, "12:34:34+02:30");
            AssertParser.FailsMatch(parser, "@12:34:34+02:30");

            AssertParser.FailsMatch(parser, "@T12:34:34+48:30");
        }

            [Fact]
        public void FhirPath_Lex_Id()
        {
            var parser = Lexer.Id.End();

            AssertParser.SucceedsMatch(parser, "a");
            AssertParser.SucceedsMatch(parser, "_");
            AssertParser.SucceedsMatch(parser, "__");
            AssertParser.SucceedsMatch(parser, "Abcdefghijklmnopqrstuvwxyz");
            AssertParser.SucceedsMatch(parser, "_Abcdef_ghijklmnopqrstuvwxyz_");
            AssertParser.SucceedsMatch(parser, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AssertParser.SucceedsMatch(parser, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AssertParser.SucceedsMatch(parser, "a0123456789");
            AssertParser.SucceedsMatch(parser, "_a0123456789");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "@");
            AssertParser.FailsMatch(parser, "#");
            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "$");
            AssertParser.FailsMatch(parser, "*");
            AssertParser.FailsMatch(parser, ".");
            AssertParser.FailsMatch(parser, "0");
            AssertParser.FailsMatch(parser, "0123456789");
            AssertParser.FailsMatch(parser, "2A");
        }

        [Fact]
        public void FhirPath_Lex_QuotedIdentifier()
        {
            var parser = Lexer.QuotedIdentifier.End();

            SucceedsDelimitedString(parser, "\"2a\"");
            SucceedsDelimitedString(parser, "\"_\"");
            SucceedsDelimitedString(parser, "\"_Abcdef_ghijklmnopqrstuvwxyz_\"");
            SucceedsDelimitedString(parser, "\"Hi \uface\"");
            SucceedsDelimitedString(parser, "\"@#$%^&*().'\"");
            SucceedsDelimitedString(parser, "\"3.1415\"");

            AssertParser.FailsMatch(parser, "NoQuotes");
            AssertParser.FailsMatch(parser, @"'wrong es\qape'");
        }


        [Fact]
        public void FhirPath_Lex_Unicode()
        {
            var parser = Lexer.Unicode.End();

            AssertParser.SucceedsMatch(parser, @"uface", "face");
            AssertParser.SucceedsMatch(parser, "u0000", "0000");
            AssertParser.SucceedsMatch(parser, "u09af", "09af");
            AssertParser.SucceedsMatch(parser, "uffff", "ffff");

            AssertParser.FailsMatch(parser, "u");
            AssertParser.FailsMatch(parser, "u0");
            AssertParser.FailsMatch(parser, "u00");
            AssertParser.FailsMatch(parser, "u000");
            AssertParser.FailsMatch(parser, "u00000");
            AssertParser.FailsMatch(parser, "ugggg");
        }

        [Fact]
        public void FhirPath_Lex_Escape()
        {
            var parser = Lexer.Escape.End();

            AssertParser.SucceedsMatch(parser, @"\uface", "龜");
            AssertParser.SucceedsMatch(parser, @"\'", @"'");
            AssertParser.SucceedsMatch(parser, @"\""", "\"");
            AssertParser.SucceedsMatch(parser, @"\\", @"\");
            AssertParser.SucceedsMatch(parser, @"\/", "/");
            //AssertParser.SucceedsMatch(parser, @"\b"); - removed in STU3
            AssertParser.SucceedsMatch(parser, @"\f", "\f");
            AssertParser.SucceedsMatch(parser, @"\n", "\n");
            AssertParser.SucceedsMatch(parser, @"\r", "\r");
            AssertParser.SucceedsMatch(parser, @"\t", "\t");

            AssertParser.FailsMatch(parser, @"\");
            AssertParser.FailsMatch(parser, @"\ugdef");
            AssertParser.FailsMatch(parser, @"\u01234");
            AssertParser.FailsMatch(parser, @"\x");
        }

        private void SucceedsDelimitedString(Parser<string> parser, string s)
        {
            AssertParser.SucceedsMatch(parser, s, s.Substring(1, s.Length - 2));
        }

        [Fact]
        public void FhirPath_Lex_String()
        {
            var parser = Lexer.String.End();            

            SucceedsDelimitedString(parser, @"'single quotes'");
            SucceedsDelimitedString(parser, @"'""single quotes with doubles""'");
            AssertParser.SucceedsMatch(parser, @"'single \' quotes'", @"single ' quotes");
            SucceedsDelimitedString(parser, @"''");

            AssertParser.SucceedsMatch(parser, @"'xxx \u0040 yyy \\\/\f\n\r\t zzz !@#$%^&*()_-=+[]{}|;:,.<>?`~'",
                            "xxx @ yyy \\/\f\n\r\t zzz " + @"!@#$%^&*()_-=+[]{}|;:,.<>?`~");
            AssertParser.SucceedsMatch(parser, @"'\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b'",
                            @"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b");

            AssertParser.FailsMatch(parser, @"'\q incorrect escape'");
            AssertParser.FailsMatch(parser, @"""double quotes""");
            AssertParser.FailsMatch(parser, @"no quotes");
            AssertParser.FailsMatch(parser, @"""mixed quotes'");
        }

        [Fact]
        public void FhirPath_Lex_Bool()
        {
            var parser = Lexer.Bool.End();

            AssertParser.SucceedsMatch(parser, "true", true);

            AssertParser.SucceedsMatch(parser, "false", false);

            AssertParser.Fails(parser, "");
            AssertParser.Fails(parser, "True");
            AssertParser.Fails(parser, "TRUE");
            AssertParser.Fails(parser, "False");
            AssertParser.Fails(parser, "FALSE");
            AssertParser.Fails(parser, "xyz");
            AssertParser.Fails(parser, "1");
            AssertParser.Fails(parser, "0");
        }


        [Fact]
        public void FhirPath_Lex_Int()
        {
            var parser = Lexer.IntegerNumber.End();

            AssertParser.SucceedsMatch(parser, "0", 0);
            AssertParser.SucceedsMatch(parser, "01", 1);

            for (long i = 1; i < 100; i++)
            {
                AssertParser.SucceedsMatch(parser, i.ToString(), i);

            }

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "a0");
            AssertParser.FailsMatch(parser, "0.1");
            AssertParser.FailsMatch(parser, "-3");      // use unary '-' operator to make negative
        }

        [Fact]
        public void FhirPath_Lex_Decimal()
        {
            var parser = Lexer.DecimalNumber.End();

            AssertParser.SucceedsMatch(parser, "3.4", 3.4m);
            // FHIR allows leading zeroes since STU3
            AssertParser.SucceedsMatch(parser, "01.00", 1.00m);
            AssertParser.SucceedsMatch(parser, "3.1415926535897932384626433", 3.1415926535897932384626433m);

            // Shall not accept integer values
            AssertParser.FailsMatch(parser, "3");

            // Test invalid values
            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "a0");
            AssertParser.FailsMatch(parser, "0d");
            AssertParser.FailsMatch(parser, "0x0");
            AssertParser.FailsMatch(parser, "0.314+E01");
        }

        [Fact]
        public void FhirPath_Lex_Mul()
        {
            var parser = Lexer.MulOperator.End();

            AssertParser.SucceedsMatch(parser, "*");
            AssertParser.SucceedsMatch(parser, "/");
            AssertParser.SucceedsMatch(parser, "div");
            AssertParser.SucceedsMatch(parser, "mod");

            AssertParser.FailsMatch(parser, "*/");
            AssertParser.FailsMatch(parser, "Div");
            AssertParser.FailsMatch(parser, "");
        }
    }
}
