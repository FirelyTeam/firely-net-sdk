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
    public class FhirPathRoundtripTest
    {
        [TestMethod]
        public void FhirPath_Roundtrip_Literal()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsRoundTrip(parser, "/*A*/'hi there'/*B*/", "'hi there'");
            AssertParser.SucceedsRoundTrip(parser, " /*A*/ 3/*B*/", "3");
            AssertParser.SucceedsRoundTrip(parser, "/*A*/ 3.14/*B*/", "3.14");
            AssertParser.SucceedsRoundTrip(parser, " /*A*/true/*B*/", "true");
            AssertParser.SucceedsRoundTrip(parser, "/*A*/@2013-12/*B*/", "@2013-12");
            AssertParser.SucceedsRoundTrip(parser, "/*A*/@2013-12T/*B*/", "@2013-12T");
            AssertParser.SucceedsRoundTrip(parser, "/*A*/@T12:23:34/*B*/", "@T12:23:34");
            AssertParser.SucceedsRoundTrip(parser, "/*A*/@2014-12-13T12:00:00+02:00/*B*/", "@2014-12-13T12:00:00+02:00");
            AssertParser.SucceedsRoundTrip(parser, "10 'mg'");
            AssertParser.SucceedsRoundTrip(parser, " 4 'day' ", "4 'day'");
            AssertParser.SucceedsRoundTrip(parser, "4 days", "4 'day'");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "childname");
            AssertParser.SucceedsRoundTrip(parser, "`childname`", "childname");
            AssertParser.SucceedsRoundTrip(parser, "`child-name`");
            AssertParser.SucceedsRoundTrip(parser, "`child-\\`name`");
            AssertParser.SucceedsRoundTrip(parser, "$this");

            AssertParser.SucceedsRoundTrip(parser, "doSomething()");
            AssertParser.SucceedsRoundTrip(parser, "doSomething ( ) ", "doSomething()");
            AssertParser.SucceedsRoundTrip(parser, "doSomething ( 3.14 ) ", "doSomething(3.14)");

            AssertParser.SucceedsRoundTrip(parser, "doSomething('hi', 3.14, 3, $this, $index, somethingElse(true))");
            AssertParser.SucceedsRoundTrip(parser, "as(Patient)");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Term()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsRoundTrip(parser, "childname");
            AssertParser.SucceedsRoundTrip(parser, "$this");
            AssertParser.SucceedsRoundTrip(parser, "doSomething()");
            AssertParser.SucceedsRoundTrip(parser, "doSomething('hi', 3.14)");
            AssertParser.SucceedsRoundTrip(parser, "%external");
            AssertParser.SucceedsRoundTrip(parser, "@2013-12");
            AssertParser.SucceedsRoundTrip(parser, "@2013-12T");
            AssertParser.SucceedsRoundTrip(parser, "3");
            AssertParser.SucceedsRoundTrip(parser, "true");
            AssertParser.SucceedsRoundTrip(parser, "(3)");
            AssertParser.SucceedsRoundTrip(parser, "{}");
            AssertParser.SucceedsRoundTrip(parser, "@2014-12-13T12:00:00+02:00");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Quantity()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsRoundTrip(parser, "78");
            AssertParser.SucceedsRoundTrip(parser, "78 'kg'");
            AssertParser.SucceedsRoundTrip(parser, "10.1 'mg'");
            AssertParser.SucceedsRoundTrip(parser, " 10.1 'mg' ", "10.1 'mg'");
            AssertParser.SucceedsRoundTrip(parser, "10.1 /* some weird comment inside a quantity */  'mg'", "10.1 'mg'");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Term_ExternalRef()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsRoundTrip(parser, "%`ext-11179-de-is-data-element-concept`");
            AssertParser.SucceedsRoundTrip(parser, "%`vs-administrative-gender`");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "Patient.name", "Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "Patient .name", "Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "Patient. name", "Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "Patient . name", "Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "Patient.name.doSomething(true)");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Expression_Indexer()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "$this[4].name");
            AssertParser.SucceedsRoundTrip(parser, "Patient . name", "Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "Patient.name [4 ]", "Patient.name[4]");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Expression_Polarity()
        {
            var parser = Grammar.PolarityExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "4");
            AssertParser.SucceedsRoundTrip(parser, "-4");

            AssertParser.SucceedsRoundTrip(parser, "-Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "+Patient.name");
        }


        [TestMethod]
        public void FhirPath_Roundtrip_Mul()
        {
            var parser = Grammar.MulExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "4* Patient.name", "4 * Patient.name");
            AssertParser.SucceedsRoundTrip(parser, "5 div 6");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Add()
        {
            var parser = Grammar.AddExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "-4");
            AssertParser.SucceedsRoundTrip(parser, "4 + 6");
        }


        [TestMethod]
        public void FhirPath_Roundtrip_Type()
        {
            var parser = Grammar.TypeExpression.End();

            AssertParser.SucceedsRoundTrip(parser, "4 is integer");
            AssertParser.SucceedsRoundTrip(parser, "8 as notoddbuteven");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_Bracket()
        {
            var parser = Grammar.TypeExpression.End();
            AssertParser.SucceedsRoundTrip(parser, "(8.as(notoddbuteven))");
            AssertParser.SucceedsRoundTrip(parser, " ( (/*Smile*/ 8.as(notoddbuteven) )\t)", "((8.as(notoddbuteven)))");
        }

        [TestMethod]
        public void FhirPath_Roundtrip_InEq()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsRoundTrip(parser, "4 < 5 and 5 > 4 or 4 <= 6 xor 6 >= 5");
        }


        [TestMethod]
        public void FhirPath_Roundtrip_Eq()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsRoundTrip(parser, "4 = 4 implies 4 != 5 and ('h' ~ 'H' or 'a' !~ 'b')");
        }
    }
}
