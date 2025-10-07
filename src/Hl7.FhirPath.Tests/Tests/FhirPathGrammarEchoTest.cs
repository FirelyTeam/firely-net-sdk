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

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class FhirPathEchoTest
    {
        [TestMethod]
        public void FhirPath_Echo_Literal()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsEcho(parser, "/*A*/'hi there'/*B*/ ");
            AssertParser.SucceedsEcho(parser, " /*A*/ 3/*B*/");
            AssertParser.SucceedsEcho(parser, "/*A*/ 3.14/*B*/");
            AssertParser.SucceedsEcho(parser, " /*A*/true/*B*/ " );
            AssertParser.SucceedsEcho(parser, "/*A*/@2013-12 /* B */");
            AssertParser.SucceedsEcho(parser, "/*A*/@2013-12T");
            AssertParser.SucceedsEcho(parser, "/*A*/@T12:23:34/*B*/ ");
            AssertParser.SucceedsEcho(parser, "/*A*/@2014-12-13T12:00:00+02:00// Blah\n/*groan*/");
        }

        [TestMethod]
        public void FhirPath_Echo_Quantity()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsEcho(parser, "10 'mg'");
            AssertParser.SucceedsEcho(parser, " 10.1 'mg' ");
            AssertParser.SucceedsEcho(parser, " 4 'day' ");
            AssertParser.SucceedsEcho(parser, "4 days");
            AssertParser.SucceedsEcho(parser, "10.1 /* some weird comment inside a quantity */  'mg'");
        }

        [TestMethod]
        public void FhirPath_Echo_EmptySet()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsEcho(parser, "{}");
            AssertParser.SucceedsEcho(parser, "{} ");
            AssertParser.SucceedsEcho(parser, " {}");
            // Note: Doesn't support having spaces or comments between the braces
            AssertParser.SucceedsEcho(parser, " { // Some internal notes\n } ");
        }

        [TestMethod]
        public void FhirPath_Echo_AxisFunctionArgs()
        {
            var parser = Grammar.InvocationExpression.End();
            AssertParser.SucceedsEcho(parser, " $this");
            AssertParser.SucceedsEcho(parser, "$this");
            AssertParser.SucceedsEcho(parser, "doSomething(3,  $this   , $index)");
        }

        [TestMethod, Ignore]
        public void FhirPath_Echo_DelimittedIdentifier()
        {
            var parser = Grammar.InvocationExpression.End();
            AssertParser.SucceedsEcho(parser, "`child-name`");
            AssertParser.SucceedsEcho(parser, "`childname`");
        }

        [TestMethod]
        public void FhirPath_Echo_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsEcho(parser, "childname");
            AssertParser.SucceedsEcho(parser, "`child-name`");
            AssertParser.SucceedsEcho(parser, "`child-\\`name`");
            AssertParser.SucceedsEcho(parser, "$this");

            AssertParser.SucceedsEcho(parser, "doSomething()");
            AssertParser.SucceedsEcho(parser, "doSomething ( ) ");
            AssertParser.SucceedsEcho(parser, "doSomething ( 3.14 )");

            AssertParser.SucceedsEcho(parser, "as(Patient)");
            AssertParser.SucceedsEcho(parser, "doSomething('hi', 3.14, 3, somethingElse(true))");
        }

        [TestMethod]
        public void FhirPath_Echo_Term()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsEcho(parser, "doSomething  ()");
            AssertParser.SucceedsEcho(parser, "childname");
            AssertParser.SucceedsEcho(parser, "$this");
            AssertParser.SucceedsEcho(parser, "doSomething()");
            AssertParser.SucceedsEcho(parser, "doSomething('hi', 3.14)");
            AssertParser.SucceedsEcho(parser, "%external");
            AssertParser.SucceedsEcho(parser, "@2013-12");
            AssertParser.SucceedsEcho(parser, "@2013-12T");
            AssertParser.SucceedsEcho(parser, "3");
            AssertParser.SucceedsEcho(parser, "true");
            AssertParser.SucceedsEcho(parser, "(3)");
            AssertParser.SucceedsEcho(parser, "{}");
            AssertParser.SucceedsEcho(parser, "@2014-12-13T12:00:00+02:00");
            AssertParser.SucceedsEcho(parser, "78 'kg'");
            AssertParser.SucceedsEcho(parser, "10.1 'mg'");
        }

        [TestMethod]
        public void FhirPath_Echo_Term_ExternalRef()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsEcho(parser, "%`ext-11179-de-is-data-element-concept`");
            AssertParser.SucceedsEcho(parser, "%`vs-administrative-gender`");
        }

        [TestMethod]
        public void FhirPath_Echo_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsEcho(parser, "Patient. name");
            AssertParser.SucceedsEcho(parser, "Patient.name.doSomething(true)");
            AssertParser.SucceedsEcho(parser, "  Patient . name");
            AssertParser.SucceedsEcho(parser, "Patient .name");
            AssertParser.SucceedsEcho(parser, "/*A*/Patient /*B*/ ./*C*/name");
        }

        [TestMethod]
        public void FhirPath_Echo_Expression_Indexer()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsEcho(parser, "$this[4].name");
            AssertParser.SucceedsEcho(parser, "Patient.name [4]");
            AssertParser.SucceedsEcho(parser, "name [4]");
            AssertParser.SucceedsEcho(parser, "name[ 4]");
            AssertParser.SucceedsEcho(parser, "name[4 ]");
        }

        [TestMethod]
        public void FhirPath_Echo_Expression_Polarity()
        {
            var parser = Grammar.Expression.End();

            AssertParser.SucceedsEcho(parser, " 4");
            AssertParser.SucceedsEcho(parser, " -4 ");
            AssertParser.SucceedsEcho(parser, " - 4 ");

            AssertParser.SucceedsEcho(parser, "-Patient.name");
            AssertParser.SucceedsEcho(parser, "+Patient.name");
        }


        [TestMethod]
        public void FhirPath_Echo_Mul()
        {
            var parser = Grammar.MulExpression.End();

            AssertParser.SucceedsEcho(parser, "4* Patient.name");
            AssertParser.SucceedsEcho(parser, "Patient.name");
            AssertParser.SucceedsEcho(parser, "5  div 6");
        }

        [TestMethod]
        public void FhirPath_Echo_Add()
        {
            var parser = Grammar.Expression.End();

            AssertParser.SucceedsEcho(parser, "  -4");
            AssertParser.SucceedsEcho(parser, "4+  6");
        }

        [TestMethod]
        public void FhirPath_Echo_StringWithDelimiters()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsEcho(parser, @"id.replaceMatches('example','\\\nA')");
        }

        [TestMethod]
        public void FhirPath_Echo_Type()
        {
            var parser = Grammar.TypeExpression.End();

            AssertParser.SucceedsEcho(parser, "4 is integer");
            AssertParser.SucceedsEcho(parser, " 8 as notoddbuteven");
            AssertParser.SucceedsEcho(parser, "8 as notoddbuteven ");
            AssertParser.SucceedsEcho(parser, "8  as notoddbuteven ");
            AssertParser.SucceedsEcho(parser, "/*A*/ 8 /*B*/ as /*C*/ notoddbuteven /*D*/");
        }

        [TestMethod]
        public void FhirPath_Echo_Union()
        {
            var parser = Grammar.UnionExpression.End();

            AssertParser.SucceedsEcho(parser, "a|b");
            AssertParser.SucceedsEcho(parser, " a|b");
            AssertParser.SucceedsEcho(parser, "a |b");
            AssertParser.SucceedsEcho(parser, "a| b");
        }

        [TestMethod]
        public void FhirPath_Echo_Sort()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsEcho(parser, " sort ( name  /* blah */ asc )");
            AssertParser.SucceedsEcho(parser, "sort()");
            AssertParser.SucceedsEcho(parser, " sort()");
            AssertParser.SucceedsEcho(parser, " sort(name)");
            AssertParser.SucceedsEcho(parser, "sort(name desc)");
            AssertParser.SucceedsEcho(parser, " sort(name asc)");
            AssertParser.SucceedsEcho(parser, " sort(name asc, tel desc)");
        }

        [TestMethod]
        public void FhirPath_Echo_Bracket()
        {
            var parser = Grammar.TypeExpression.End();
            AssertParser.SucceedsEcho(parser, " ( /*Smile*/8 /* 5 */) // stuiff");
            AssertParser.SucceedsEcho(parser, "(8.as(notoddbuteven))");
            AssertParser.SucceedsEcho(parser, " ( (/*Smile*/ 8.as(notoddbuteven) )\t)");
        }

        [TestMethod]
        public void FhirPath_Echo_InEq()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsEcho(parser, "4 < 5 and 5 > 4 or 4 <= 6 xor 6 >= 5");
        }


        [TestMethod]
        public void FhirPath_Echo_Eq()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsEcho(parser, "4 = 4 implies 4 != 5 and ('h' ~ 'H' or 'a' !~ 'b')");
        }
    }
}
