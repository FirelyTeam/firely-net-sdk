/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.FhirPath.Parser;
using Hl7.FhirPath.Sprache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class FhirPathLexerTest
    {
        [TestMethod]
        public void FhirPath_Lex_QualifiedIdentifier()
        {
            var parser = Lexer.QualifiedIdentifier.End();

            AssertParser.SucceedsMatch(parser, "name");
            AssertParser.SucceedsMatch(parser, "name.name2");
            AssertParser.SucceedsMatch(parser, "name.name2.name3");
            AssertParser.SucceedsMatch(parser, "name.`name2`", "name.name2");
            AssertParser.SucceedsMatch(parser, "`name`.`name2`", "name.name2");
            AssertParser.SucceedsMatch(parser, "name.\"name2\"", "name.name2");   // should still support the pre-normative syntax
            AssertParser.SucceedsMatch(parser, "\"name\".\"name2\"", "name.name2");
        }

        private void succeedsPrefixString(Parser<string> parser, string expr)
        {
            AssertParser.SucceedsMatch(parser, expr, expr.Substring(1));
        }


        [TestMethod]
        public void FhirPath_Lex_Const()
        {
            var parser = Lexer.ExternalConstant.End();

            succeedsPrefixString(parser, "%c");
            succeedsPrefixString(parser, "%const");
            succeedsPrefixString(parser, "%a1");
            succeedsPrefixString(parser, "%a__1");
            AssertParser.SucceedsMatch(parser, "%`forbidden-characters-1234`", "forbidden-characters-1234");
            AssertParser.SucceedsMatch(parser, "%\"forbidden-characters-1234\"", "forbidden-characters-1234"); // still support the old syntax
            AssertParser.SucceedsMatch(parser, "%'forbidden-characters-1234'", "forbidden-characters-1234"); // new normative %<STRING>

            AssertParser.FailsMatch(parser, "%0");
            AssertParser.FailsMatch(parser, "%0123");
            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "%%");
            AssertParser.FailsMatch(parser, "%%a");
            AssertParser.FailsMatch(parser, "%-");
            AssertParser.FailsMatch(parser, "%*");
        }

        [TestMethod]
        public void FhirPath_Lex_Identifier()
        {
            var parser = Lexer.Identifier.End();

            AssertParser.SucceedsMatch(parser, "A34", "A34");
            AssertParser.FailsMatch(parser, "34");
            AssertParser.FailsMatch(parser, "'Hello'");
            AssertParser.FailsMatch(parser, "@2013");
        }


        [TestMethod]
        public void FhirPath_Lex_DateTime()
        {
            var parser = Lexer.DateTime.End();

            accept("@2015-01T");
            accept("@2015-01-02T12:34:00Z");
            accept("@2015-01-03T12:34:34+02:30");
            accept("@2015-01-03T12:34:34");
            accept("@2015-01-01T23");
            accept("@2015-01-01T");
            accept("@2015-01-01T23Z");
            accept("@2015-01-01T+01:00");

            reject("@2015-01");  // must have a T suffix
            reject("@2015-01-02");  // since this is a date, not a dateTime
            reject("@2015-01-02+01:00");  // since this is a date, not a dateTime

            reject("T12:34:34+02:30");
            reject("12:34:34+02:30");
            reject("@T12:34:34+02:30");
            reject("@12:34:34+02:30");
            reject("@20150103T12:34:34+02:30");
            reject("@-2015-01");

            void accept(string s) => AssertParser.SucceedsMatch(parser, s, P.DateTime.Parse(Lexer.CleanupDateTimeLiteral(s)));
            void reject(string s) => AssertParser.FailsMatch(parser, s);
        }

        [TestMethod]
        public void FhirPath_Lex_Time()
        {
            var parser = Lexer.Time.End();

            accept("@T12:34:34.345674");
            accept("@T12:34:34");
            accept("@T12:35");
            accept("@T12");

            reject("@T12:34:34+02:30");
            reject("@T12:34:00Z");
            reject("2001-01-01T12:34:34+02:30");
            reject("@2001-01-01T12:34:34+02:30");
            reject("T12:34:34+02:30");
            reject("12:34:34+02:30");
            reject("@12:34:34+02:30");
            reject("@T12:34:34+48:30");

            void accept(string s) => AssertParser.SucceedsMatch(parser, s, P.Time.Parse(s.Substring(2)));
            void reject(string s) => AssertParser.FailsMatch(parser, s);
        }

        [TestMethod]
        public void FhirPath_Lex_Date()
        {
            var parser = Lexer.Date.End();

            accept("@2018-04-05");
            accept("@2018-04");
            accept("@2018");

            reject("@2018-04-05T");
            reject("@2018-04-05TZ");
            reject("@2018-04-05Z");
            reject("@2018-04-05T10:00:00");
            reject("@2018-04-05T10:00:00Z");

            void accept(string s) => AssertParser.SucceedsMatch(parser, s, P.Date.Parse(s.Substring(1)));
            void reject(string s) => AssertParser.FailsMatch(parser, s);
        }

        [TestMethod]
        public void FhirPath_Lex_Axis()
        {
            var parser = Lexer.Axis.End();

            AssertParser.SucceedsMatch(parser, "$this", "this");
            AssertParser.SucceedsMatch(parser, "$index", "index");
            AssertParser.SucceedsMatch(parser, "$total", "total");
            AssertParser.FailsMatch(parser, "$that");
        }

        [TestMethod]
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

        [TestMethod]
        public void FhirPath_Lex_QuotedIdentifier()
        {
            var parser = Lexer.DelimitedIdentifier.End();

            succeedsDelimitedString(parser, "`2a`");
            succeedsDelimitedString(parser, "`_`");
            succeedsDelimitedString(parser, "`_Abcdef_ghijklmnopqrstuvwxyz_`");
            succeedsDelimitedString(parser, "`Hi \uface`");
            succeedsDelimitedString(parser, "`@#$%^&*().'`");
            succeedsDelimitedString(parser, "`3.1415`");

            AssertParser.SucceedsMatch(parser, "`A\uface%$#34`", "A\uface%$#34");
            AssertParser.SucceedsMatch(parser, "`A\"quote`", "A\"quote");
            AssertParser.SucceedsMatch(parser, "\"A`quote\"", "A`quote");
            AssertParser.SucceedsMatch(parser, "`A\\`quote`", "A`quote");
            AssertParser.SucceedsMatch(parser, "\"A\\\"quote\"", "A\"quote");

            AssertParser.FailsMatch(parser, "NoQuotes");
            AssertParser.FailsMatch(parser, @"'wrong es\qape'");

        }

        [TestMethod]
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


        [TestMethod]
        public void FhirPath_Lex_Escape()
        {
            var parser = Lexer.Escape.End();

            AssertParser.SucceedsMatch(parser, @"\uface", "龜");
            AssertParser.SucceedsMatch(parser, @"\'", @"'");
            AssertParser.SucceedsMatch(parser, @"\`", "`");
            AssertParser.SucceedsMatch(parser, "\\\"", "\"");  // \" === "
            AssertParser.SucceedsMatch(parser, @"\\", @"\");
            AssertParser.SucceedsMatch(parser, @"\/", "/");
            AssertParser.SucceedsMatch(parser, @"\f", "\f");
            AssertParser.SucceedsMatch(parser, @"\n", "\n");
            AssertParser.SucceedsMatch(parser, @"\r", "\r");
            AssertParser.SucceedsMatch(parser, @"\t", "\t");

            AssertParser.FailsMatch(parser, @"\");
            AssertParser.FailsMatch(parser, @"\ugdef");
            AssertParser.FailsMatch(parser, @"\u01234");
            AssertParser.FailsMatch(parser, @"\x");
            AssertParser.FailsMatch(parser, @"\b");
        }

        private void succeedsDelimitedString(Parser<string> parser, string s)
        {
            AssertParser.SucceedsMatch(parser, s, s.Substring(1, s.Length - 2));
        }

        [TestMethod]
        public void FhirPath_Lex_String()
        {
            var parser = Lexer.String.End();

            succeedsDelimitedString(parser, @"'single quotes'");
            succeedsDelimitedString(parser, @"'""single quotes with doubles""'");
            AssertParser.SucceedsMatch(parser, @"'single \' quotes'", @"single ' quotes");
            succeedsDelimitedString(parser, @"''");

            AssertParser.SucceedsMatch(parser, @"'xxx \u0040 yyy \\\/\f\n\r\t zzz !@#$%^&*()_-=+[]{}|;:,.<>?`~'",
                            "xxx @ yyy \\/\f\n\r\t zzz " + @"!@#$%^&*()_-=+[]{}|;:,.<>?`~");
            AssertParser.SucceedsMatch(parser, @"'\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b'",
                            @"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b");

            AssertParser.FailsMatch(parser, @"'\q incorrect escape'");
            AssertParser.FailsMatch(parser, @"""double quotes""");
            AssertParser.FailsMatch(parser, @"no quotes");
            AssertParser.FailsMatch(parser, @"""mixed quotes'");
        }

        [TestMethod]
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


        [TestMethod]
        public void FhirPath_Lex_Int()
        {
            var parser = Lexer.IntegerNumber.End();

            AssertParser.SucceedsMatch(parser, "0", 0);
            AssertParser.SucceedsMatch(parser, "01", 1);

            for (int i = 1; i < 100; i++)
            {
                AssertParser.SucceedsMatch(parser, i.ToString(), i);

            }

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "a0");
            AssertParser.FailsMatch(parser, "0.1");
            AssertParser.FailsMatch(parser, "-3");      // use unary '-' operator to make negative
        }

        [TestMethod]
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

        [TestMethod]
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
