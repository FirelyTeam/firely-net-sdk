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

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
    public class FhirPathLexerTest
    {
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

        private void SucceedsConst(Parser<string> parser, string expr) => AssertParser.SucceedsMatch(parser, expr, expr.Substring(1));

        [TestMethod]
        public void FhirPath_Lex_Const()
        {
            var parser = Lexer.Const.End();

            SucceedsConst(parser, "%c");
            SucceedsConst(parser, "%const");
            SucceedsConst(parser, "%0");
            SucceedsConst(parser, "%0123");
            SucceedsConst(parser, "%a-1");
            SucceedsConst(parser, "%a----1");

            AssertParser.FailsMatch(parser, "%");
            AssertParser.FailsMatch(parser, "%%");
            AssertParser.FailsMatch(parser, "%%a");
            AssertParser.FailsMatch(parser, "%-");
            AssertParser.FailsMatch(parser, "%*");
        }

        //private void SucceedsUnicode(Parser<string> parser, string expr) => AssertParser.SucceedsMatch(parser, expr, expr.Substring(1));

        [TestMethod]
        public void FhirPath_Lex_Unicode()
        {
            var parser = Lexer.Unicode.End();

            //SucceedsUnicode(parser, "u0000");
            //SucceedsUnicode(parser, "u09af");
            //SucceedsUnicode(parser, "uffff");

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

        private void SucceedsString(Parser<string> parser, string s) => AssertParser.SucceedsMatch(parser, s, s.Substring(1, s.Length - 2));

        [TestMethod]
        public void FhirPath_Lex_String()
        {
            var parser = Lexer.String.End();

            SucceedsString(parser, @"'single quotes'");
            SucceedsString(parser, @"'single \' quotes'");
            SucceedsString(parser, @"''");

            SucceedsString(parser, @"""double quotes""");
            SucceedsString(parser, @"""double \"" quotes""");
            SucceedsString(parser, @"""""");

            SucceedsString(parser, @"'xxx \u0123 yyyy \\\/\b\f\n\r\t zzz !@#$%^&*()_-=+[]{}|;:,.<>?`~'");

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
    }
}

