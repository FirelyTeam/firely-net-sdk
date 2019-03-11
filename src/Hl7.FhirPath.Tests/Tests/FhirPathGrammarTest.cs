/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Hl7.FhirPath;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Sprache;

using Xunit;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Tests
{
    public class FhirPathExpressionTest
    {

        [Fact]
        public void FhirPath_Gramm_Literal()
        {
            var parser = Grammar.Literal.End();

            AssertParser.SucceedsMatch(parser, "'hi there'", new ConstantExpression("hi there"));
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3L));
            AssertParser.SucceedsMatch(parser, "3.14", new ConstantExpression(3.14m));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(PartialDateTime.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "@T12:23:34Z", new ConstantExpression(PartialTime.Parse("12:23:34Z")));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true));
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(PartialDateTime.Parse("2014-12-13T12:00:00+02:00")));

            AssertParser.FailsMatch(parser, "%constant");
            AssertParser.FailsMatch(parser, "\"quotedstring\"");
            AssertParser.FailsMatch(parser, "A23identifier");
        }

        [Fact]
        public void FhirPath_Gramm_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.That, "childname"));
           // AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);

            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeInfo.Any));
            AssertParser.SucceedsMatch(parser, "doSomething ( ) ", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeInfo.Any));
            AssertParser.SucceedsMatch(parser, "doSomething ( 3.14 ) ", new FunctionCallExpression(AxisExpression.That, "doSomething", TypeInfo.Any,
                                new ConstantExpression(3.14m)));

            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14, 3, $this, somethingElse(true))", new FunctionCallExpression(AxisExpression.That,"doSomething", TypeInfo.Any,
                        new ConstantExpression("hi"), new ConstantExpression(3.14m), new ConstantExpression(3L),
                        AxisExpression.This,
                        new FunctionCallExpression(AxisExpression.That, "somethingElse", TypeInfo.Any, new ConstantExpression(true))));

            AssertParser.SucceedsMatch(parser, "as(Patient)", new FunctionCallExpression(AxisExpression.That, "as", TypeInfo.Any, new ConstantExpression("Patient")));

            AssertParser.FailsMatch(parser, "$that");
       //     AssertParser.FailsMatch(parser, "as(Patient.identifier)");
            AssertParser.FailsMatch(parser, "as('Patient')");
            AssertParser.FailsMatch(parser, "doSomething(");
        }

        [Fact]
        public void FhirPath_Gramm_Term()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsMatch(parser, "childname", new ChildExpression(AxisExpression.This,"childname"));
            AssertParser.SucceedsMatch(parser, "$this", AxisExpression.This);
            AssertParser.SucceedsMatch(parser, "doSomething()", new FunctionCallExpression(AxisExpression.This, "doSomething", TypeInfo.Any));
            AssertParser.SucceedsMatch(parser, "doSomething('hi', 3.14)", new FunctionCallExpression(AxisExpression.This, "doSomething", TypeInfo.Any,
                        new ConstantExpression("hi"), new ConstantExpression(3.14m)));
            AssertParser.SucceedsMatch(parser, "%external", new VariableRefExpression("external"));
            AssertParser.SucceedsMatch(parser, "@2013-12", new ConstantExpression(PartialDateTime.Parse("2013-12")));
            AssertParser.SucceedsMatch(parser, "3", new ConstantExpression(3L));
            AssertParser.SucceedsMatch(parser, "true", new ConstantExpression(true));
            AssertParser.SucceedsMatch(parser, "(3)", new ConstantExpression(3L));
            AssertParser.SucceedsMatch(parser, "{}", NewNodeListInitExpression.Empty);
            AssertParser.SucceedsMatch(parser, "@2014-12-13T12:00:00+02:00", new ConstantExpression(PartialDateTime.Parse("2014-12-13T12:00:00+02:00")));

        }

        [Fact]
        public void FhirPath_Gramm_Term_ExternalRef()
        {
            var parser = Grammar.Term.End();

            AssertParser.SucceedsMatch(parser, "%\"ext-11179-de-is-data-element-concept\"",
                new FunctionCallExpression(AxisExpression.That, "builtin.coreexturl", TypeInfo.String,
                            new ConstantExpression("11179-de-is-data-element-concept")));

            AssertParser.SucceedsMatch(parser, "%\"vs-administrative-gender\"",
                new FunctionCallExpression(AxisExpression.That, "builtin.corevsurl", TypeInfo.String, 
                    new ConstantExpression("administrative-gender")));
        }


        private static readonly Expression patientName = new ChildExpression(new ChildExpression(AxisExpression.This, "Patient"), "name");

        [Fact]
        public void FhirPath_Gramm_Expression_Invocation()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name.doSomething(true)",
                    new FunctionCallExpression(patientName, "doSomething", TypeInfo.Any, new ConstantExpression(true)));

            AssertParser.FailsMatch(parser, "Patient.");
            //AssertParser.FailsMatch(parser, "Patient. name");     //oops
            //AssertParser.FailsMatch(parser, "Patient . name");
            //AssertParser.FailsMatch(parser, "Patient .name");
        }
        
        [Fact]
        public void FhirPath_Gramm_Expression_Indexer()
        {
            var parser = Grammar.InvocationExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name", patientName);
            AssertParser.SucceedsMatch(parser, "Patient.name [4 ]",
                    new IndexerExpression(patientName, new ConstantExpression(4)));
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

        [Fact]
        public void FhirPath_Gramm_Expression_Polarity()
        {
            var parser = Grammar.PolarityExpression.End();

            AssertParser.SucceedsMatch(parser, "4", new ConstantExpression(4));
            AssertParser.SucceedsMatch(parser, "-4", new UnaryExpression('-', new ConstantExpression(4)));

            AssertParser.SucceedsMatch(parser, "-Patient.name", new UnaryExpression('-', patientName));
            AssertParser.SucceedsMatch(parser, "+Patient.name", new UnaryExpression('+', patientName));
        }


        [Fact]
        public void FhirPath_Gramm_Mul()
        {
            var parser = Grammar.MulExpression.End();

            AssertParser.SucceedsMatch(parser, "Patient.name", patientName);
            AssertParser.SucceedsMatch(parser, "4* Patient.name", new BinaryExpression('*', new ConstantExpression(4),patientName));
            AssertParser.SucceedsMatch(parser, "5 div 6", constOp("div", 5, 6));

            AssertParser.FailsMatch(parser,"4*");
            // AssertParser.FailsMatch(parser, "5div6");    oops
        }

        [Fact]
        public void FhirPath_Gramm_Add()
        {
            var parser = Grammar.AddExpression.End();

            AssertParser.SucceedsMatch(parser, "-4", new UnaryExpression('-', new ConstantExpression(4)));
            AssertParser.SucceedsMatch(parser, "4 + 6", constOp("+", 4, 6));

            AssertParser.FailsMatch(parser, "4+");
            // AssertParser.FailsMatch(parser, "5div6");    oops
        }


        [Fact]
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

        [Fact]
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

   
        [Fact]
        public void FhirPath_Gramm_Eq()
        {
            var parser = Grammar.Expression.End();

            AssertParser.SucceedsMatch(parser, "4=4 implies 4 != 5 and ('h' ~ 'H' or 'a' !~ 'b')",
                new BinaryExpression("implies", constOp("=", 4, 4),
                  new BinaryExpression("and",
                    constOp("!=", 4, 5), new BinaryExpression("or", constOp("~", 'h', 'H'), constOp("!~", 'a', 'b')))));

            AssertParser.FailsMatch(parser, "true implies false and 4 != 5 and 4 <> 6 and ('h' ~ 'H' or 'a' !~ 'b')");
        }

        private void SucceedsConstantValueMatch(Parser<ConstantExpression> parser, string expr, object value, TypeInfo expected)
        {
            AssertParser.SucceedsWith(parser, expr,
                    v =>
                        {
                            Assert.Equal(v.Value, value);
                            Assert.Equal(v.ExpressionType, expected);
                        });
        }

    }
}
