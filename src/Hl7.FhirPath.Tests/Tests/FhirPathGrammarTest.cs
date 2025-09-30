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
using System.Linq;
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

            AssertParser.SucceedsMatch(parser, "`child-name`", new ChildExpression(AxisExpression.That, "child-name"));
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

            AssertParser.SucceedsMatch(parser, "as(Patient)", new FunctionCallExpression(AxisExpression.That, "as", TypeSpecifier.Any, new IdentifierExpression("Patient")));

            var fexRaw = parser.Parse("as(Patient)");
            if (fexRaw is FunctionCallExpression fex)
            {
                Assert.AreEqual("as", fex.FunctionName);
                var arg = fex.Arguments.FirstOrDefault();
                Assert.AreEqual("IdentifierExpression", arg?.GetType().Name);
                if (arg is IdentifierExpression id)
                {
                    Assert.AreEqual("Patient", id.Value);
                }
            }

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
            AssertParser.SucceedsMatch(parser, "(3)", new BracketExpression(new SubToken("("), new SubToken(")"), new ConstantExpression(3)));
            AssertParser.SucceedsMatch(parser, "{}", NewNodeListInitExpression.Empty);
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(P.DateTime.Parse("2014-12-13T12:00:00+02:00")));
            AssertParser.SucceedsMatch(parser, "78 'kg'", new ConstantExpression(new P.Quantity(78m, "kg")));
            AssertParser.SucceedsMatch(parser, "10.1 'mg'", new ConstantExpression(new P.Quantity(10.1m, "mg")));
        }

        FhirPathExpressionLocationInfo SetLoc(int lineNo, int linePos, int rawPos, int length)
        {
            return new FhirPathExpressionLocationInfo()
            {
                LineNumber = lineNo,
                LinePosition = linePos,
                RawPosition = rawPos,
                Length = length,
            };
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Child()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.This, "childname", SetLoc(1, 1, 0, 9)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_This()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "$this", new AxisExpression("this", SetLoc(1,1,0,5)));
        }

        [TestMethod]
        public void FhirPath_Location_Type()
        {
            var parser = Grammar.TypeExpression.End();

            AssertParser.SucceedsMatch(parser, "/*A*/ 8 /*B*/ as /*C*/ notoddbuteven /*D*/", new BinaryExpression(
                "as",
                new ConstantExpression(8, SetLoc(1, 7, 6, 1)),
                new IdentifierExpression("notoddbuteven", SetLoc(1, 24, 23, 13)),
                SetLoc(1, 15, 14, 2)));
            AssertParser.SucceedsEcho(parser, "/*A*/ 8 /*B*/ as /*C*/ notoddbuteven /*D*/");
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Function()
        {
            var parser = Grammar.Term.End();
            // The length of the function includes all the way to the end of the closing brackets (not just the function name)
            AssertParser.SucceedsMatch(parser, "today()",
                new FunctionCallExpression(
                    AxisExpression.This,
                    "today",
                    new SubToken('(', SetLoc(1, 14, 13, 1)),
                    new SubToken(')', SetLoc(1, 15, 14, 1)),
                    TypeSpecifier.Any,
                    new Expression[] { },
                    SetLoc(1, 1, 0, 5)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Function2()
        {
            var parser = Grammar.Expression.End();
            // The length of the function includes all the way to the end of the closing brackets (not just the function name)
            AssertParser.SucceedsMatch(parser, "today().toString()",
                new FunctionCallExpression(
                    new FunctionCallExpression(
                        AxisExpression.This,
                        "today",
                        new SubToken('(', SetLoc(1, 14, 13, 1)),
                        new SubToken(')', SetLoc(1, 15, 14, 1)),
                        TypeSpecifier.Any,
                        new Expression[] { },
                        SetLoc(1, 1, 0, 5)
                    ),
                    "toString",
                    new SubToken('(', SetLoc(1, 14, 13, 1)),
                    new SubToken(')', SetLoc(1, 15, 14, 1)),
                    TypeSpecifier.Any,
                    new Expression[] { },
                    SetLoc(1, 9, 8, 8))
                );
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Function3()
        {
            var parser = Grammar.Expression.End();
            // The length of the function includes all the way to the end of the closing brackets (not just the function name)
            AssertParser.SucceedsMatch(parser, "given.join(' ')",
                new FunctionCallExpression(
                    new ChildExpression(
                        AxisExpression.This,
                        "given",
                        SetLoc(1, 1, 0, 5)
                    ),
                    "join",
                    new SubToken('(', SetLoc(1, 14, 13, 1)),
                    new SubToken(')', SetLoc(1, 15, 14, 1)),
                    TypeSpecifier.Any,
                    new Expression[] { new ConstantExpression(" ", SetLoc(1, 12, 11, 3)) },
                    SetLoc(1, 7, 6, 4))
                );
        }

        [TestMethod]
        public void FhirPath_LocationInfo_FunctionWithParams()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14)",
                    new FunctionCallExpression(
                        AxisExpression.This,
                        "doSomething",
                        new SubToken('(', SetLoc(1, 1, 0, 23)),
                        new SubToken(')', SetLoc(1, 1, 0, 23)),
                        TypeSpecifier.Any,
                        new[] { new ConstantExpression("hi", SetLoc(1, 13, 12, 4)), new ConstantExpression(3.14m, SetLoc(1, 19, 18, 4)) }
                        , SetLoc(1, 1, 0, 11))
                    );
        }

        [TestMethod]
        public void FhirPath_LocationInfo_VariableRef()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "%external", new VariableRefExpression("external", SetLoc(1, 1, 0, 9)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantDate()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(P.Date.Parse("2013-12"), SetLoc(1, 1, 0, 8)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantDateTime()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "@2013-12T", new ConstantExpression(P.DateTime.Parse("2013-12"), SetLoc(1, 1, 0, 9)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantInt()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3, SetLoc(1, 1, 0, 1)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantBoolean()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true, SetLoc(1, 1, 0, 4)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Union()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, " 1 | 50 ", new BinaryExpression(
                '|',
                new ConstantExpression(1, SetLoc(1, 2, 1, 1)),
                new ConstantExpression(50, SetLoc(1, 6, 5, 2)),
                SetLoc(1, 4, 3, 1)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Brackets()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "  ( 3 ) ",
                new BracketExpression(
                    new ConstantExpression(3, SetLoc(1, 2, 2, 1)),
                    SetLoc(1, 3, 2, 5)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_EmptySet()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "{}", new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 1, 0, 2)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_EmptySet2()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "{}|{}",
                new BinaryExpression('|',
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 1, 0, 2)),
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 4, 3, 2)),
                    SetLoc(1, 3, 2, 1)
                ));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_EmptySet3()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "{}| {}",
                new BinaryExpression('|',
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 1, 0, 2)),
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 5, 4, 2)),
                    SetLoc(1, 3, 2, 1)
                ));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_EmptySet4()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "{} |{}",
                new BinaryExpression('|',
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 1, 0, 2)),
                    new NewNodeListInitExpression(Enumerable.Empty<Expression>(), SetLoc(1, 5, 4, 2)),
                    SetLoc(1, 4, 3, 1)
                ));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantInstant()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(P.DateTime.Parse("2014-12-13T12:00:00+02:00"), SetLoc(1, 1, 0, 26)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantIntQuant()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "78 'kg'", new ConstantExpression(new P.Quantity(78m, "kg"), SetLoc(1, 1, 0, 7)));

            // and sub-token checks too (for comments and units)
            AssertParser.SucceedsMatch(parser, " 78 /* smile */ days ", new ConstantExpression(new P.Quantity(78m, "day", Hl7.Fhir.ElementModel.Types.QuantityUnitSystem.CalendarDuration), SetLoc(1, 2, 1, 19)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_ConstantDecimalQuant()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "10.1 'mg'", new ConstantExpression(new P.Quantity(10.1m, "mg"), SetLoc(1, 1, 0, 9)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Identifier()
        {
            var parser = Grammar.Term.End();
            AssertParser.SucceedsMatch(parser, "ofType(Patient)",
                new FunctionCallExpression(
                    AxisExpression.This,
                    "ofType",
                    new SubToken('(', SetLoc(1, 7, 6, 1)),
                    new SubToken(')', SetLoc(1, 15, 14, 1)),
                    TypeSpecifier.Any,
                    new[] { new IdentifierExpression("Patient", SetLoc(1, 8, 7, 7)) },
                    SetLoc(1, 1, 0, 6)
                ));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_Indexer()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "name[10]", new IndexerExpression(
                new ChildExpression(AxisExpression.This, "name", SetLoc(1, 1, 0, 4)),
                new ConstantExpression(10, SetLoc(1, 6, 5, 2)),
                SetLoc(1, 5, 4, 4)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_IndexerWithComments()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "/*A*/name/*B*/[/*C*/10/*D*/]/*E*/", new IndexerExpression(
                new ChildExpression(AxisExpression.This, "name", SetLoc(1, 1, 0, 4)),
                new ConstantExpression(10, SetLoc(1, 21, 20, 2)),
                SetLoc(1, 15, 14, 14)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_BinaryExpr()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "1+50", new BinaryExpression(
                '+',
                new ConstantExpression(1, SetLoc(1, 1, 0, 1)),
                new ConstantExpression(50, SetLoc(1, 3, 2, 2)),
                SetLoc(1, 2, 1, 1)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_BinaryExprTrailingWhitespace()
        {
            var parser = Grammar.AddExpression.End();
            AssertParser.SucceedsMatch(parser, " 1 + 50 ", new BinaryExpression(
                '+',
                new ConstantExpression(1, SetLoc(1, 2, 1, 1)),
                new ConstantExpression(50, SetLoc(1, 6, 5, 2)),
                SetLoc(1, 4, 3, 1)));
        }

        [TestMethod]
        public void FhirPath_LocationInfo_UnaryExpr()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "-10", new UnaryExpression(
                '-',
                new ConstantExpression(10, SetLoc(1, 2, 1, 2)),
                SetLoc(1, 1, 0, 1)));
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

            AssertParser.SucceedsMatch(parser, "78 'kg'", new ConstantExpression(new P.Quantity(78m, "kg")));
            AssertParser.SucceedsMatch(parser, "78.0 'kg'", new ConstantExpression(new P.Quantity(78m, "kg")));
            AssertParser.SucceedsMatch(parser, "78.0'kg'", new ConstantExpression(new P.Quantity(78m, "kg")));
            AssertParser.SucceedsMatch(parser, "4 months", new ConstantExpression(P.Quantity.ForCalendarDuration(4m, "month")));
            AssertParser.SucceedsMatch(parser, "4 'mo'", new ConstantExpression(new P.Quantity(4m, "mo")));
            AssertParser.SucceedsMatch(parser, "1 '1'", new ConstantExpression(new P.Quantity(1m, P.Quantity.UCUM_UNIT)));

            AssertParser.FailsMatch(parser, "78");   // still a integer
            AssertParser.FailsMatch(parser, "78.0");   // still a decimal
            AssertParser.FailsMatch(parser, "78 kg");
            AssertParser.FailsMatch(parser, "four 'kg'");
            AssertParser.FailsMatch(parser, "4 decennia");
        }

        [TestMethod]
        public void FhirPath_Gramm_Sort()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "sort()", new FunctionCallExpression(AxisExpression.This, "sort", TypeSpecifier.Any));
            AssertParser.SucceedsMatch(parser, "sort(given)", new FunctionCallExpression(AxisExpression.This, "sort", TypeSpecifier.Any, [new ChildExpression(AxisExpression.This, "given")]));
            AssertParser.SucceedsMatch(parser, "sort(given desc)", new FunctionCallExpression(AxisExpression.This, "sort", TypeSpecifier.Any, [new SortDirectionExpression("desc", new ChildExpression(AxisExpression.This, "given"))]));
        }

        [TestMethod]
        public void FhirPath_Gramm_SortAsc()
        {
            var parser = Grammar.Expression.End();
            AssertParser.SucceedsMatch(parser, "sort(given asc)", new FunctionCallExpression(AxisExpression.This, "sort", TypeSpecifier.Any, [new SortDirectionExpression("asc", new ChildExpression(AxisExpression.This, "given"))]));
        }

        [TestMethod]
        public void FhirPath_Gramm_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name.doSomething(true)",
                    new FunctionCallExpression(PATIENTNAME, "doSomething", TypeSpecifier.Any, new ConstantExpression(true)));
            AssertParser.SucceedsMatch(parser, "\'ewout\'.indexOf(\'o\', 2)", new FunctionCallExpression(new ConstantExpression("ewout"), "indexOf", TypeSpecifier.Any, [new ConstantExpression("o"), new ConstantExpression(2)]));

            AssertParser.FailsMatch(parser, "Patient.");
            //AssertParser.FailsMatch(parser, "Patient. name");     //oops
            //AssertParser.FailsMatch(parser, "Patient . name");
            //AssertParser.FailsMatch(parser, "Patient .name");
        }

        [TestMethod]
        public void FhirPath_Gramm_Expression_Indexer()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient . name", PATIENTNAME);
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

        [TestMethod]
        public void FhirPath_Gramm_Bracket()
        {
            var parser = Grammar.TypeExpression.End();

            AssertParser.SucceedsMatch(parser, "(8.as(notoddbuteven))", new BracketExpression( new FunctionCallExpression(new ConstantExpression(8), "as", TypeSpecifier.Any, new IdentifierExpression("notoddbuteven"))));
        }

        [TestMethod]
        public void FhirPath_Gramm_Comment()
        {
            var parser = Grammar.Expression.End();

            System.Diagnostics.Trace.WriteLine("");
            var t1 = parser.Parse("(name[0].family | %glarb | {} | @2023-01) is string // sdf");
            // DumpLocations(t1);
            System.Diagnostics.Trace.WriteLine("");

            var t2 = parser.Parse("  -8 * 3.pow(2)// sdf");
            // DumpLocations(t2);
            System.Diagnostics.Trace.WriteLine("");

            t2 = parser.Parse("  8 \n  *  323 // testme");
            // t2 = parser.Parse("8 /* sdf */ //\n * 323 // testme");
            DumpLocations(t2);
            System.Diagnostics.Trace.WriteLine("");

            AssertParser.SucceedsMatch(parser, "8 // blah", new ConstantExpression(8));
        }

        private void DumpLocations(Expression ex, int tabs = 0)
        {
            System.Diagnostics.Trace.WriteLine(ex.Dump());
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
                    constOp("!=", 4, 5), new BracketExpression(new BinaryExpression("or", constOp("~", 'h', 'H'), constOp("!~", 'a', 'b'))))));

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
