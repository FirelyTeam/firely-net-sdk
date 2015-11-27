/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.FhirPath;
using Sprache;
using System.Linq;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
    public class FhirPathLexerTest
    {
        // TODO: Implement .Named() method to provide error info - also unit test!

        [TestMethod]
        public void FhirPath_Lex_Identifier()
        {
            var parser = Lexer.Id.End();

            AssertParser.SucceedsMatch(parser, "a");
            AssertParser.SucceedsMatch(parser, "abcdefghijklmnopqrstuvwxyz");
            AssertParser.SucceedsMatch(parser, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AssertParser.SucceedsMatch(parser, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AssertParser.SucceedsMatch(parser, "a0123456789");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "_");
            AssertParser.FailsMatch(parser, "@");
            AssertParser.FailsMatch(parser, "#");
            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "$");
            AssertParser.FailsMatch(parser, "*");
            AssertParser.FailsMatch(parser, ".");
            AssertParser.FailsMatch(parser, "0");
            AssertParser.FailsMatch(parser, "0123456789");
        }


        [TestMethod]
        public void FhirPath_Lex_Element()
        {
            var parser = Lexer.Element.End();

            AssertParser.SucceedsMatch(parser, "e");
            AssertParser.SucceedsMatch(parser, "element");
            AssertParser.SucceedsMatch(parser, "Element");
            AssertParser.SucceedsMatch(parser, "ELEMENT");
            AssertParser.SucceedsMatch(parser, "Element0123456789");
            AssertParser.SucceedsMatch(parser, "Element[x]");

            AssertParser.FailsMatch(parser, "0element");
            AssertParser.FailsMatch(parser, "_element");
            AssertParser.FailsMatch(parser, "[x]");
            AssertParser.FailsMatch(parser, ".");
            AssertParser.FailsMatch(parser, "*");
            AssertParser.FailsMatch(parser, "**");
        }

        private void SucceedsPrefixString(Parser<string> parser, string expr)
        {
            AssertParser.SucceedsMatch(parser, expr, expr.Substring(1));
        }

        [TestMethod]
        public void FhirPath_Lex_Const()
        {
            var parser = Lexer.Const.End();

            SucceedsPrefixString(parser, "%c");
            SucceedsPrefixString(parser, "%const");
            SucceedsPrefixString(parser, "%0");
            SucceedsPrefixString(parser, "%0123");
            SucceedsPrefixString(parser, "%a-1");
            SucceedsPrefixString(parser, "%a----1");

            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "%%");
            AssertParser.FailsMatch(parser, "%%a");
            AssertParser.FailsMatch(parser, "%-");
            AssertParser.FailsMatch(parser, "%*");
        }

        [TestMethod]
        public void FhirPath_Lex_Unicode()
        {
            var parser = Lexer.Unicode.End();

            //SucceedsPrefixString(parser, "u0000");
            //SucceedsPrefixString(parser, "u09af");
            //SucceedsPrefixString(parser, "uffff");

            AssertParser.SucceedsMatch(parser, "u0000");
            AssertParser.SucceedsMatch(parser, "u09af");
            AssertParser.SucceedsMatch(parser, "uffff");

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

            AssertParser.SucceedsMatch(parser, @"\uface");
            AssertParser.SucceedsMatch(parser, @"\'");
            AssertParser.SucceedsMatch(parser, @"\""");
            AssertParser.SucceedsMatch(parser, @"\\");
            AssertParser.SucceedsMatch(parser, @"\/");
            AssertParser.SucceedsMatch(parser, @"\b");
            AssertParser.SucceedsMatch(parser, @"\f");
            AssertParser.SucceedsMatch(parser, @"\n");
            AssertParser.SucceedsMatch(parser, @"\r");
            AssertParser.SucceedsMatch(parser, @"\t");

            AssertParser.FailsMatch(parser, @"\");
            AssertParser.FailsMatch(parser, @"\ugdef");
            AssertParser.FailsMatch(parser, @"\u01234");
            AssertParser.FailsMatch(parser, @"\x");
        }

        private void SucceedsDelimitedString(Parser<string> parser, string s)
        {
            AssertParser.SucceedsMatch(parser, s, s.Substring(1, s.Length - 2));
        }

        [TestMethod]
        public void FhirPath_Lex_String()
        {
            var parser = Lexer.String.End();

            SucceedsDelimitedString(parser, @"'single quotes'");
            SucceedsDelimitedString(parser, @"'single \' quotes'");
            SucceedsDelimitedString(parser, @"''");

            SucceedsDelimitedString(parser, @"""double quotes""");
            SucceedsDelimitedString(parser, @"""double \"" quotes""");
            SucceedsDelimitedString(parser, @"""""");

            SucceedsDelimitedString(parser, @"'xxx \u0123 yyyy \\\/\b\f\n\r\t zzz !@#$%^&*()_-=+[]{}|;:,.<>?`~'");

            AssertParser.FailsMatch(parser, @"no quotes");
            AssertParser.FailsMatch(parser, @"""mixed quotes'");
        }

        [TestMethod]
        public void FhirPath_Lex_Bool()
        {
            var parser = Lexer.Bool.End();

            AssertParser.SucceedsMatch(parser, "true");
            AssertParser.SucceedsMatch(parser, "false");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "True");
            AssertParser.FailsMatch(parser, "TRUE");
            AssertParser.FailsMatch(parser, "False");
            AssertParser.FailsMatch(parser, "FALSE");
            AssertParser.FailsMatch(parser, "xyz");
            AssertParser.FailsMatch(parser, "1");
            AssertParser.FailsMatch(parser, "0");
        }

        [TestMethod]
        public void FhirPath_Lex_Int()
        {
            var parser = Lexer.Number.End();

            for (int i = 0; i < 100; i++)
            {
                AssertParser.SucceedsMatch(parser, i.ToString());
                // TODO: Negative values
                AssertParser.SucceedsMatch(parser, "-" + i.ToString());
            }

            // Very large integers - how to cast to (32-bit) int?
            // [WMR] Suggestion: match all digits, but validate during conversion to int
            AssertParser.SucceedsMatch(parser, "100000000000000000000000000000000");
            AssertParser.SucceedsMatch(parser, "999999999999999999999999999999999");
            AssertParser.SucceedsMatch(parser, "-100000000000000000000000000000000");
            AssertParser.SucceedsMatch(parser, "-999999999999999999999999999999999");

            // FHIR disallows leading zeroes
            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "01");
            AssertParser.FailsMatch(parser, "a0");
            AssertParser.FailsMatch(parser, "0.1");
        }

        [TestMethod]
        public void FhirPath_Lex_Logic()
        {
            var parser = Lexer.Logic;

            AssertParser.SucceedsMatch(parser, "and");
            AssertParser.SucceedsMatch(parser, "or");
            AssertParser.SucceedsMatch(parser, "xor");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "AND");
            AssertParser.FailsMatch(parser, "And");
            AssertParser.FailsMatch(parser, "OR");
            AssertParser.FailsMatch(parser, "Or");
            AssertParser.FailsMatch(parser, "XOR");
            AssertParser.FailsMatch(parser, "Xor");

            AssertParser.FailsMatch(parser, "not");
        }

        [TestMethod]
        public void FhirPath_Lex_Comp()
        {
            var parser = Lexer.Comp.End();

            AssertParser.SucceedsMatch(parser, "=");
            AssertParser.SucceedsMatch(parser, "~");
            AssertParser.SucceedsMatch(parser, "!=");
            AssertParser.SucceedsMatch(parser, "!~");
            AssertParser.SucceedsMatch(parser, ">");
            AssertParser.SucceedsMatch(parser, "<");
            AssertParser.SucceedsMatch(parser, ">=");
            AssertParser.SucceedsMatch(parser, "<=");
            AssertParser.SucceedsMatch(parser, "in");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "In");
            AssertParser.FailsMatch(parser, "IN");
            AssertParser.FailsMatch(parser, "==");
            AssertParser.FailsMatch(parser, "!==");
            AssertParser.FailsMatch(parser, "=!=");
            AssertParser.FailsMatch(parser, "<<");
            AssertParser.FailsMatch(parser, ">>");
        }

        [TestMethod]
        public void FhirPath_Lex_RootSpec()
        {
            var parser = Lexer.RootSpec.End();

            SucceedsPrefixString(parser, "$context");
            SucceedsPrefixString(parser, "$resource");
            SucceedsPrefixString(parser, "$parent");

            AssertParser.FailsMatch(parser, "");
            AssertParser.FailsMatch(parser, "$test");
            AssertParser.FailsMatch(parser, "$$context");
            AssertParser.FailsMatch(parser, "$Context");
            AssertParser.FailsMatch(parser, "$Resource");
            AssertParser.FailsMatch(parser, "$Parent");
        }

        [TestMethod]
        public void FhirPath_Lex_AxisSpec()
        {
            var parser = Lexer.AxisSpec.End();

            AssertParser.FailsMatch(parser, "");
            AssertParser.SucceedsMatch(parser, "*");
            AssertParser.SucceedsMatch(parser, "**");
            AssertParser.FailsMatch(parser, "***");
            AssertParser.FailsMatch(parser, "#");
            AssertParser.FailsMatch(parser, "abc");
        }

        [TestMethod]
        public void FhirPath_Lex_Recurse()
        {
            var parser = Lexer.Recurse.End();

            AssertParser.FailsMatch(parser, "");
            AssertParser.SucceedsMatch(parser, "*");
            AssertParser.FailsMatch(parser, "**");
            AssertParser.FailsMatch(parser, "***");
            AssertParser.FailsMatch(parser, "#");
            AssertParser.FailsMatch(parser, "abc");
        }
    }
}
