/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Language;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath.Sprache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class FhirPathExpressionTest
    {
        [TestMethod]
        public void FhirPath_Gramm_Literal()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsMatch(parser, "'hi there'", new ConstantExpression("hi there"));
            var m = new ConstantExpression(3);
            AssertParser.SucceedsMatch(parser, "3", m);
            AssertParser.SucceedsMatch(parser, "3.14", new ConstantExpression(3.14m));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(P.Date.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "@2013-12T", new ConstantExpression(P.DateTime.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "@T12:23:34", new ConstantExpression(P.Time.Parse("12:23:34")));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true));
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(P.DateTime.Parse("2014-12-13T12:00:00+02:00")));

            AssertParser.FailsMatch(parser, "%constant");
            AssertParser.FailsMatch(parser, "`quotedstring`");
            AssertParser.FailsMatch(parser, "A23identifier");
        }

        [TestMethod]
        public void FhirPath_Gramm_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.That, "childname"));
            // AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);

            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeSpecifier.Any));
            AssertParser.SucceedsMatch(parser, "doSomething ( ) ", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeSpecifier.Any));
            AssertParser.SucceedsMatch(parser, "doSomething ( 3.14 ) ", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeSpecifier.Any,
                                new ConstantExpression(3.14m)));

            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14, 3, $this, $index, somethingElse(true))", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeSpecifier.Any,
                        new ConstantExpression("hi"), new ConstantExpression(3.14m), new ConstantExpression(3),
                        AxisExpression.This, AxisExpression.Index,
                        new FunctionCallExpression(AxisExpression.That, "somethingElse", TypeSpecifier.Any, new ConstantExpression(true))));

            AssertParser.SucceedsMatch(parser, "as(Patient)", new FunctionCallExpression(AxisExpression.That, "as", TypeSpecifier.Any, new ConstantExpression("Patient")));

            AssertParser.FailsMatch(parser, "$that");
            //     AssertParser.FailsMatch(parser, "as(Patient.identifier)");
            AssertParser.FailsMatch(parser, "as('Patient')");
            AssertParser.FailsMatch(parser, "doSomething(");
        }

        [TestMethod]
        public void FhirPath_Gramm_Term()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.This, "childname"));
            AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);
            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.This, "doSomething", TypeSpecifier.Any));
            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14)", new FunctionCallExpression(AxisExpression.This, "doSomething", TypeSpecifier.Any,
                        new ConstantExpression("hi"), new ConstantExpression(3.14m)));
            AssertParser.SucceedsMatch(parser, "%external", new VariableRefExpression("external"));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(P.Date.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "@2013-12T", new ConstantExpression(P.DateTime.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true));
            AssertParser.SucceedsMatch(parser, "(3)", new ConstantExpression(3));
            AssertParser.SucceedsMatch(parser, "{}", NewNodeListInitExpression.Empty);
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(P.DateTime.Parse("2014-12-13T12:00:00+02:00")));
            AssertParser.SucceedsMatch(parser, "78 'kg'", new ConstantExpression(new P.Quantity(78m, "kg")));
            AssertParser.SucceedsMatch(parser, "10.1 'mg'", new ConstantExpression(new P.Quantity(10.1m, "mg")));
        }

        [TestMethod]
        public void FhirPath_Gramm_Term_ExternalRef()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsMatch(parser, "%`ext-11179-de-is-data-element-concept`",
                new FunctionCallExpression(AxisExpression.That, "builtin.coreexturl", TypeSpecifier.String,
                            new ConstantExpression("11179-de-is-data-element-concept")));

            AssertParser.SucceedsMatch(parser, "%`vs-administrative-gender`",
                new FunctionCallExpression(AxisExpression.That, "builtin.corevsurl", TypeSpecifier.String,
                    new ConstantExpression("administrative-gender")));
        }


        private static readonly Expression PATIENTNAME = new ChildExpression(new ChildExpression(AxisExpression.This, "Patient"), "name");

        [TestMethod]
        public void FhirPath_Gramm_Quantity()
        {
            var parser = Grammar.Quantity.End();

            AssertParser.SucceedsMatch(parser, "78 'kg'", new P.Quantity(78m, "kg"));
            AssertParser.SucceedsMatch(parser, "78.0 'kg'", new P.Quantity(78m, "kg"));
            AssertParser.SucceedsMatch(parser, "78.0'kg'", new P.Quantity(78m, "kg"));
            AssertParser.SucceedsMatch(parser, "4 months", P.Quantity.ForCalendarDuration(4m, "month"));
            AssertParser.SucceedsMatch(parser, "4 'mo'", new P.Quantity(4m, "mo"));
            AssertParser.SucceedsMatch(parser, "1 '1'", new P.Quantity(1m, P.Quantity.UCUM_UNIT));

            AssertParser.FailsMatch(parser, "78");   // still a integer
            AssertParser.FailsMatch(parser, "78.0");   // still a decimal
            AssertParser.FailsMatch(parser, "78 kg");
            AssertParser.FailsMatch(parser, "four 'kg'");
            AssertParser.FailsMatch(parser, "4 decennia");
        }

        [TestMethod]
        public void FhirPath_Gramm_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name.doSomething(true)",
                    new FunctionCallExpression(PATIENTNAME, "doSomething", TypeSpecifier.Any, new ConstantExpression(true)));

            AssertParser.FailsMatch(parser, "Patient.");
            //AssertParser.FailsMatch(parser, "Patient. name");     //oops
            //AssertParser.FailsMatch(parser, "Patient . name");
            //AssertParser.FailsMatch(parser, "Patient .name");
        }

        [TestMethod]
        public void FhirPath_Gramm_Expression_Indexer()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name", PATIENTNAME);
            AssertParser.SucceedsMatch(parser, "Patient.name [4 ]",
                    new IndexerExpression(PATIENTNAME, new ConstantExpression(4)));
            AssertParser.SucceedsMatch(parser, "$this[4].name",
                new ChildExpression(
                    new IndexerExpression(AxisExpression.This, new ConstantExpression(4)),
                    "name"));

            AssertParser.FailsMatch(parser, "Patient.name[");
            AssertParser.FailsMatch(parser, "Patient.name]");
            AssertParser.FailsMatch(parser, "Patient.name[]");
            AssertParser.FailsMatch(parser, "Patient.name[4,]");
            AssertParser.FailsMatch(parser, "Patient.name[4,5]");

        }

        [TestMethod]
        public void FhirPath_Gramm_Expression_Polarity()
        {
            var parser = Grammar.PolarityExpression.End();

            AssertParser.SucceedsMatch(parser, "4", new ConstantExpression(4));
            AssertParser.SucceedsMatch(parser, "-4", new UnaryExpression('-', new ConstantExpression(4)));

            AssertParser.SucceedsMatch(parser, "-Patient.name", new UnaryExpression('-', PATIENTNAME));
            AssertParser.SucceedsMatch(parser, "+Patient.name", new UnaryExpression('+', PATIENTNAME));
        }


        [TestMethod]
        public void FhirPath_Gramm_Mul()
        {
            var parser = Grammar.MulExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name", PATIENTNAME);
            AssertParser.SucceedsMatch(parser, "4* Patient.name", new BinaryExpression('*', new ConstantExpression(4), PATIENTNAME));
            AssertParser.SucceedsMatch(parser, "5 div 6", constOp("div", 5, 6));

            AssertParser.FailsMatch(parser, "4*");
            // AssertParser.FailsMatch(parser, "5div6");    oops
        }

        [TestMethod]
        public void FhirPath_Gramm_Add()
        {
            var parser = Grammar.AddExpression.End();

            AssertParser.SucceedsMatch(parser, "-4", new UnaryExpression('-', new ConstantExpression(4)));
            AssertParser.SucceedsMatch(parser, "4 + 6", constOp("+", 4, 6));

            AssertParser.FailsMatch(parser, "4+");
            // AssertParser.FailsMatch(parser, "5div6");    oops
        }


        [TestMethod]
        public void FhirPath_Gramm_Type()
        {
            var parser = Grammar.TypeExpression.End();

            AssertParser.SucceedsMatch(parser, "4 is integer", new BinaryExpression("is", new ConstantExpression(4), new ConstantExpression("integer")));
            AssertParser.SucceedsMatch(parser, "8 as notoddbuteven", new BinaryExpression("as", new ConstantExpression(8), new ConstantExpression("notoddbuteven")));

            AssertParser.FailsMatch(parser, "4 is 5");
            // AssertParser.FailsMatch(parser, "5div6");    oops
        }

        private Expression constOp(string op, object left, object right)
        {
            return new BinaryExpression(op, new ConstantExpression(left), new ConstantExpression(right));
        }

        [TestMethod]
        public void FhirPath_Gramm_InEq()
        {
            var parser = Grammar.Expression.End();

            AssertParser.SucceedsMatch(parser, "4 < 5 and 5 > 4 or 4 <= 6 xor 6 >= 5",
                new BinaryExpression("xor",
                    new BinaryExpression("or",
                        new BinaryExpression("and", constOp("<", 4, 5), constOp(">", 5, 4)),
                        constOp("<=", 4, 6)),
                    constOp(">=", 6, 5)));

            AssertParser.FailsMatch(parser, "<>");
        }


        [TestMethod]
        public void FhirPath_Gramm_Eq()
        {
            var parser = Grammar.Expression.End();

            AssertParser.SucceedsMatch(parser, "4=4 implies 4 != 5 and ('h' ~ 'H' or 'a' !~ 'b')",
                new BinaryExpression("implies", constOp("=", 4, 4),
                  new BinaryExpression("and",
                    constOp("!=", 4, 5), new BinaryExpression("or", constOp("~", 'h', 'H'), constOp("!~", 'a', 'b')))));

            AssertParser.FailsMatch(parser, "true implies false and 4 != 5 and 4 <> 6 and ('h' ~ 'H' or 'a' !~ 'b')");
        }

        [TestMethod]
        public void FhirPath_Expression_Equals()
        {
            Expression x = new ConstantExpression("hi there");
            Expression y = new VariableRefExpression("hi there");

            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(x == y);
        }
    }
}
